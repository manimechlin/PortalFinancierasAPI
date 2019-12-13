using BusinessEntities.Base;
using BusinessEntities.Email;
using BusinessEntities.Massive;
using DataAccess;
using DataAccess.Interfaces;
using IRT.CustomLog;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BusinessEntities.Catalog;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Mime;
using OfficeOpenXml;

namespace Services.SendEmail
{
  public class SendEmailService : ISendEmailService
    {
      
        private string _connectionstring;
        private IDBHelper _dbHelper;
        ApiResponse response = new ApiResponse();
        private ISendmailRepository _ISendmailRepository;
        private IConfiguration _configuration;
        private readonly LogService _LogService = new LogService();
        string connectionLog = null;
        string _AplicationId = null;
        DataSet result = null;
        string smtpHost;
        string smtpPort;
        string smtpUser;
        string smtpPssw;
        string smtpFrom;
        string dominio;
        string fileCsv; 
        string Rutafile;


        public SendEmailService(ISendmailRepository SendmailRepository, IConfiguration Configuration)
        {
            _ISendmailRepository = SendmailRepository;
              _configuration = Configuration;
            connectionLog = _configuration.GetSection("appSettings").GetSection("ConnectionLog").Value;
            _AplicationId = _configuration.GetSection("appSettings").GetSection("AplicationID").Value;
            smtpHost= _configuration.GetSection("appSettings").GetSection("smtpHost").Value;
            smtpPort= _configuration.GetSection("appSettings").GetSection("smtpPort").Value;
            smtpUser= _configuration.GetSection("appSettings").GetSection("smtpUser").Value;
            smtpPssw= _configuration.GetSection("appSettings").GetSection("smtpPssw").Value;
            smtpFrom = _configuration.GetSection("appSettings").GetSection("smtpFrom").Value;            
            fileCsv = _configuration.GetSection("appSettings").GetSection("fileCsv").Value;
            Rutafile = _configuration.GetSection("appSettings").GetSection("Rutafile").Value;
            _connectionstring = _configuration.GetSection("connectionStrings").GetSection("skytex").Value;
        }

        public ApiResponse mail(string file_xml)
        {
          
            DataSet result = null;
            List<MassiveInformation> ListCommand = new List<MassiveInformation>();
            string cadena = string.Empty;
            string sucursal_ = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                //List<MassiveInformation> cmd = new List<MassiveInformation>();
                MassiveInformation cmd = new MassiveInformation();
                using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(file_xml), XmlDictionaryReaderQuotas.Max))
                {
                    XElement xml = XElement.Load(reader);
                    doc.LoadXml(xml.ToString());
                }
                result = _ISendmailRepository.Filexml(doc.InnerXml);
                string json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

                var contentJo = (JObject)JsonConvert.DeserializeObject(json);
                JObject organizationsJArray = (JObject)(contentJo.SelectToken("Table") as JArray).First();                
                
                string user= organizationsJArray.Value<string>("UserId");
                string IdUser = user.Trim();
                var listJArray = contentJo["Table"].Value<JArray>();
                string cc_cve = organizationsJArray.Value<string>("cc_cve");
                //string e_mail = organizationsJArray.Value<string>("email");
                var organizations = listJArray.ToObject<List<MassiveInformation>>();
                string jsonresult = JsonConvert.SerializeObject(organizations, Newtonsoft.Json.Formatting.Indented);
                BranchOfficeModel sucursal = GetBranchOffice("W", cc_cve);
                sucursal_ =Convert.ToString(sucursal.name);
                ListCommand = organizations;
                List<string> command_lst= new List<string>();
                List<MassiveInformation> lst = new List<MassiveInformation>();
                if (ListCommand.Count>0)
                {
                    foreach (MassiveInformation total in ListCommand)
                    {
                        cmd.Command = total.Command;
                        cmd.license_Plate = total.license_Plate;
                        if (cmd.Command == "InMovilizar" || cmd.Command == "Movilizar")
                        {
                            lst.Add(new MassiveInformation()
                            {
                                Command = cmd.Command,
                                license_Plate = cmd.license_Plate

                            });

                        }
                    }
                    string jsonReport = JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented);
                    string dato = null;
                    var dt = JsonConvert.DeserializeObject<System.Data.DataTable>(jsonReport);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    if (dt != null)
                    {
                        GenerateExcelFile(dt, fileCsv, Convert.ToInt32(IdUser), sucursal_);
                    }
                }
                               
                response.Success = true;
                response.Data = jsonresult;
            }
            catch (Exception ex)
            {
                response.Success = false;
                //  registrar en el DB-Log
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "SendEmailService.mail", $"Parameters: empty", "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;

            }
            return response;
        }
        public BranchOfficeModel GetBranchOffice(string cc_tipo, string cc_cve)
        {
            result = null;
            BranchOfficeModel response = null;
            try
            {

                response = new BranchOfficeModel();
                result = _ISendmailRepository.GetBranchOffice(cc_tipo, cc_cve);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.name = row[0].ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                string parameters = "Parameters: cc_tipo: {0}, cc_cve: {1}";
                _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "CatalogService.GetBranchOffice", string.Format(parameters, cc_tipo, cc_cve), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }
        public void Send_mail(string file, int user, string sucursal, int records, int templateId, string mail)
        {
            MailMessage email = new MailMessage();
            string Template = null;

            if (templateId == 1)
            {
                email.To.Add(new MailAddress(smtpFrom));
                string result = Path.GetFileName(file);
                email.Attachments.Add(new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Pdf));
                email.Subject = "Solicitud de inmovilización o movilización de vehículos";
                Template = String.Format("<div style='font-family:Candara; align:justify; width:45px'>Se ha registrado una solicitud de la empresa &nbsp;<b>" + sucursal +
                      "</b>&nbsp; con clave de usuario &nbsp;<b>" + user + "</b>&nbsp; para realizar el proceso de comandos a los vehículos adjuntos.</div>");
            }
            else
            {
                email.To.Add(new MailAddress(mail));
                Template = String.Format("¡Felicitaciones! Carga exitosa portal financiera &nbsp;<b style='color:red'>" +DateTime.Now+ "&nbsp; con &nbsp;" + records +
                   "</b>&nbsp; registros <br/> Este es un mensaje para confirmar la carga exitosa de tus solicitudes no, lo elimines.");
            }
            if (templateId == 2)
            {
                email.Subject = "Confirmación solicitud de Instalación.";
            }
            if (templateId == 3)
            {
                email.Subject = "Confirmación solicitud de Desinstalación.";
            }         
         
          
            email.From = new MailAddress(smtpUser);            
                       
            email.Body = Template;            
            email.IsBodyHtml = true;
            

            SmtpClient servidor = new SmtpClient(smtpHost);
            servidor.Port = Convert.ToInt32(smtpPort);
            
            servidor.EnableSsl = false;
            servidor.UseDefaultCredentials = false;

            NetworkCredential credenciales = new NetworkCredential(smtpUser, smtpPssw);
            servidor.Credentials = credenciales;

            try
            {
                servidor.Send(email);


            }
            catch (Exception ex)
            {
                response.Success = false;
                //  registrar en el DB-Log
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "SendEmailService.Send_mail", $"Parameters: empty", "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;
            }
        }

        public void GenerateExcelFile(System.Data.DataTable dataTable, string filename, int user, string sucursal)
        {
            try
            {
                StringBuilder fileContent = new StringBuilder();

                foreach (var col in dataTable.Columns)
                {
                    fileContent.Append(col.ToString() + ",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (var column in dr.ItemArray)
                    {
                        fileContent.Append("\"" + column.ToString() + "\",");
                    }

                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                }

                if (System.IO.File.Exists(fileCsv))
                {
                    File.Delete(fileCsv);
                }
                if (System.IO.File.Exists(Rutafile))
                {
                    File.Delete(Rutafile);
                }
                System.IO.File.WriteAllText(filename, fileContent.ToString());
                string worksheetsName = "TEST";
                var format = new ExcelTextFormat();
                format.Delimiter = ',';
                format.EOL = "\r";
                bool firstRowIsHeader = false;
                using (ExcelPackage package = new ExcelPackage(new FileInfo(Rutafile)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet.Cells["A1"].LoadFromText(new FileInfo(fileCsv), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
                    package.Save();
                }
                string adj = Rutafile;
                Send_mail(adj, user, sucursal, 0,1,null);



            }
            catch (Exception ex)
            {
               // TextLog("Save File" + ex.Message.ToString());
            }
        }
    }
}
