using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

namespace Services.Helper
{
    public partial class MixData
    {
        int _keySize = 256;
        string _keyValue = "bnJTWm9jOFVOdTFiMjZsSDc4VEliQT09LEo2aVVMcEpyOVZCV3dob3VOT0p2a01MaTNhWjBLNWFvVEE0cGh6Z3JIaU09";

        //Method to Encrypt
        public string E(string texto)
        {
            //Text to Byte[]
            byte[] bytes = Encoding.UTF8.GetBytes(texto);

            //Modify originl text
            byte[] newArrayByte = addByteToArray(bytes);

            //Byte[] to text
            string texto2 = Encoding.UTF8.GetString(newArrayByte);

            //Text to base64
            byte[] bytes2 = Encoding.UTF8.GetBytes(texto2);
            string base64 = Convert.ToBase64String(bytes2);

            //Base64 to byte[]
            byte[] bytes3 = Encoding.UTF8.GetBytes(base64);

            //Modify text again
            newArrayByte = addByteToArray(bytes3);

            //Byte[] to text
            string string2 = Encoding.UTF8.GetString(newArrayByte);

            //Text to base64
            bytes2 = Encoding.UTF8.GetBytes(string2);
            base64 = Convert.ToBase64String(bytes2);

            //Encrypt
            string2 = Cifrado(base64, this._keyValue, this._keySize);

            //Encrypt to base64
            bytes2 = Encoding.UTF8.GetBytes(string2);
            base64 = Convert.ToBase64String(bytes2);

            return base64;
        }

        //Method to Decrypt
        public string D(string texto)
        {

            //base64 to Encrypt
            byte[] bytes = Convert.FromBase64String(texto);
            string string1 = Encoding.UTF8.GetString(bytes);

            //Decrypt
            string1 = Decifrado(string1, this._keyValue, this._keySize);

            //base64 to Text
            byte[] bytes2 = Convert.FromBase64String(string1);
            string base64 = Encoding.UTF8.GetString(bytes2);

            //text to Byte[] 
            byte[] bytes3 = Encoding.UTF8.GetBytes(base64);

            //Get text 2
            byte[] newArrayByte = removeByteFromArray(bytes3);

            //byte[] to Base64
            base64 = Encoding.UTF8.GetString(newArrayByte);

            //base64 to Text
            bytes2 = Convert.FromBase64String(base64);
            string textoMod = Encoding.UTF8.GetString(bytes2);

            //text to Byte[] 
            bytes2 = Encoding.UTF8.GetBytes(textoMod);

            //Text 1
            newArrayByte = removeByteFromArray(bytes2);
            //////////////////////////////////////////////////

            //Byte[] to Original text
            string originalText = Encoding.UTF8.GetString(newArrayByte);

            return originalText;
        }

        public byte[] addByteToArray(byte[] bArray)
        {
            byte[] newArray = new byte[bArray.Length + 4];

            newArray[0] = bArray[0];
            newArray[1] = GetRdm();
            newArray[2] = GetRdm();
            newArray[3] = GetRdm();
            newArray[4] = GetRdm();


            for (int i = 1; i < bArray.Length; i++)
            {
                newArray[i + 4] = bArray[i];
            }

            return newArray;
        }

        public byte[] removeByteFromArray(byte[] bArray)
        {
            byte[] newArray = new byte[bArray.Length - 4];

            newArray[0] = bArray[0];

            for (int i = 5; i < bArray.Length; i++)
            {
                newArray[i - 4] = bArray[i];
            }

            return newArray;
        }

        public byte GetRdm()
        {
            Random r = new Random();
            Thread.Sleep(100);
            int i = r.Next(21, 127);

            return (byte)(Convert.ToByte(i));
        }

        private string getString(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// Encrypt
        /// From : www.chapleau.info/blog/2011/01/06/usingsimplestringkeywithaes256encryptioninc.html
        /// </summary>
        private string Cifrado(string iPlainStr, string iCompleteEncodedKey, int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',')[1]);
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(iPlainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Decrypt
        /// From : www.chapleau.info/blog/2011/01/06/usingsimplestringkeywithaes256encryptioninc.html
        /// </summary>
        private string Decifrado(string iEncryptedText, string iCompleteEncodedKey, int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.IV = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',')[0]);
            aesEncryption.Key = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',')[1]);
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64CharArray(iEncryptedText.ToCharArray(), 0, iEncryptedText.Length);
            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        }
    }
}