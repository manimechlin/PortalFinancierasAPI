using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Services.Helper
{
    public enum EncryptionMode
    {
        Encrypt = 0,
        Decrypt = 1

    }
    /// <summary>
    /// Aes 256bit encryption.
    /// </summary>
    public class cryptoLib
    {
        static string varAppKey = null;
        static string varAppIv = null;
        public static IConfiguration Configuration { get; set; }

        public static List<string> GetConnectionString()
        {
            List<string> key = new List<string>();

            key.Add(varAppKey);
            key.Add(varAppIv);
            return key;

        }
        /// <summary>
        /// Encrypt or decrypt ciperText.
        /// </summary>
        /// <param name="cipherText">string: text</param>
        /// <param name="mode">EncryptionMode.Encrypt or EncryptionMode.Decrypt</param>
        /// <returns>string: based on mode used.</returns>
        public static string EncryptDecrypt(string cipherText, EncryptionMode mode, string appkey, string appiv)
        {
            string result = string.Empty;
            //GetConnectionString();
            var AppKey = appkey;
            var AppIv = appiv;

            var keybytes = Encoding.UTF8.GetBytes(AppKey);
            var iv = Encoding.UTF8.GetBytes(AppIv);

            if (mode == EncryptionMode.Encrypt)
            {
                var response = EncryptStringToBytes(cipherText, keybytes, iv);
                result = Convert.ToBase64String(response);
            }
            if (mode == EncryptionMode.Decrypt)
            {
                var encrypted = Convert.FromBase64String(cipherText);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                result = string.Format(decriptedFromJavascript);

            }
            return result;

        }

        public static string EncryptDecryptfromJson(string cipherText, EncryptionMode mode)
        {
            string result = string.Empty;
            GetConnectionString();
            var AppKey = varAppKey;
            var AppIv = varAppIv;

            var keybytes = Encoding.UTF8.GetBytes(AppKey);
            var iv = Encoding.UTF8.GetBytes(AppIv);

            if (mode == EncryptionMode.Encrypt)
            {
                var response = EncryptStringToBytes(cipherText, keybytes, iv);
                result = Convert.ToBase64String(response);
            }
            if (mode == EncryptionMode.Decrypt)
            {
                var encrypted = Convert.FromBase64String(cipherText);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                //result = string.Format(decriptedFromJavascript);
                result = decriptedFromJavascript;

            }
            return result;

        }

        public static string EncryptDecrypt(string cipherText, string key, string IV, EncryptionMode mode)
        {
            string result = string.Empty;

            var AppKey = key;
            var AppIv = IV;

            var keybytes = Encoding.UTF8.GetBytes(AppKey);
            var iv = Encoding.UTF8.GetBytes(AppIv);

            if (mode == EncryptionMode.Encrypt)
            {
                var response = EncryptStringToBytes(cipherText, keybytes, iv);
                result = Convert.ToBase64String(response);
            }
            if (mode == EncryptionMode.Decrypt)
            {
                var encrypted = Convert.FromBase64String(cipherText);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
                result = string.Format(decriptedFromJavascript);

            }
            return result;
        }

        /// <summary>
        /// Use the mehtod to decrypt the encrypted value.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>string</returns>
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        /// <summary>
        /// Use this method to encrypt string.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>return: string in encrypted form.</returns>
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.  
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.  
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }


    }

    public class CryptLib : IDisposable
    {
        private UTF8Encoding _enc;

        private RijndaelManaged _rcipher;

        private byte[] _key;

        private byte[] _pwd;

        private byte[] _ivBytes;

        private byte[] _iv;

        private readonly static char[] CharacterMatrixForRandomIVStringGeneration;

        static CryptLib()
        {
            CryptLib.CharacterMatrixForRandomIVStringGeneration = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '\u005F' };
        }

        public CryptLib()
        {
            this._enc = new UTF8Encoding();
            this._rcipher = new RijndaelManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                BlockSize = 128
            };
            this._key = new byte[32];
            this._iv = new byte[this._rcipher.BlockSize / 8];
            this._ivBytes = new byte[16];
        }

        public string decrypt(string _encryptedText, string _key, string _initVector)
        {
            return this.encryptDecrypt(_encryptedText, _key, CryptLib.EncryptMode.DECRYPT, _initVector);
        }

        public string encrypt(string _plainText, string _key, string _initVector, int? keySize = null)
        {
            return this.encryptDecrypt(_plainText, _key, CryptLib.EncryptMode.ENCRYPT, _initVector, keySize);
        }

        private string encryptDecrypt(string _inputText, string _encryptionKey, CryptLib.EncryptMode _mode, string _initVector, int? keySize = null)
        {
            byte[] numArray;
            string base64String = "";
            if (keySize.HasValue && keySize.Value > 0)
            {
                this._key = new byte[keySize.Value];
            }
            this._pwd = Encoding.UTF8.GetBytes(_encryptionKey);
            this._ivBytes = Encoding.UTF8.GetBytes(_initVector);
            int length = (int)this._pwd.Length;
            if (length > (int)this._key.Length)
            {
                length = (int)this._key.Length;
            }
            int num = (int)this._ivBytes.Length;
            if (num > (int)this._iv.Length)
            {
                num = (int)this._iv.Length;
            }
            Array.Copy(this._pwd, this._key, length);
            Array.Copy(this._ivBytes, this._iv, num);
            this._rcipher.Key = this._key;
            this._rcipher.IV = this._iv;
            if (_mode.Equals(CryptLib.EncryptMode.ENCRYPT))
            {
                numArray = this._rcipher.CreateEncryptor().TransformFinalBlock(this._enc.GetBytes(_inputText), 0, _inputText.Length);
                base64String = Convert.ToBase64String(numArray);
            }
            if (_mode.Equals(CryptLib.EncryptMode.DECRYPT))
            {
                numArray = this._rcipher.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(_inputText), 0, (int)Convert.FromBase64String(_inputText).Length);
                base64String = this._enc.GetString(numArray);
            }
            this._rcipher.Dispose();
            return base64String;
        }

        public string encrypt(string text)
        {
            string iv = "";//ConfigurationManager.AppSettings["DTCAesIV"] == null ? "GHfw0B3d206C3VQ3" :
                           //ConfigurationManager.AppSettings["DTCAesIV"];
            string key = "";//ConfigurationManager.AppSettings["DTCAesKey"] == null ? "R04dTr4ck*EStk568+" :
                            //ConfigurationManager.AppSettings["DTCAesKey"];
            string hashSha256 = CryptLib.getHashSha256(key, 31);
            return encrypt(text, hashSha256, iv);
        }

        public string decrypt(string text)
        {
            string iv = "";//ConfigurationManager.AppSettings["DTCAesIV"] == null ? "GHfw0B3d206C3VQ3" :
                           // ConfigurationManager.AppSettings["DTCAesIV"];
            string key = "";//ConfigurationManager.AppSettings["DTCAesKey"] == null ? "R04dTr4ck*EStk568+" :
                            //ConfigurationManager.AppSettings["DTCAesKey"];
            string hashSha256 = CryptLib.getHashSha256(key, 31);
            return decrypt(text, hashSha256, iv);
        }

        internal static string GenerateRandomIV(int length)
        {
            char[] characterMatrixForRandomIVStringGeneration = new char[length];
            byte[] numArray = new byte[length];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            try
            {
                rNGCryptoServiceProvider.GetBytes(numArray);
            }
            finally
            {
                if (rNGCryptoServiceProvider != null)
                {
                    ((IDisposable)rNGCryptoServiceProvider).Dispose();
                }
            }
            for (int i = 0; i < (int)characterMatrixForRandomIVStringGeneration.Length; i++)
            {
                int num = numArray[i] % (int)CryptLib.CharacterMatrixForRandomIVStringGeneration.Length;
                characterMatrixForRandomIVStringGeneration[i] = CryptLib.CharacterMatrixForRandomIVStringGeneration[num];
            }
            return new string(characterMatrixForRandomIVStringGeneration);
        }

        public static string getHashSha256(string text, int length)
        {
            string str;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] numArray = (new SHA256Managed()).ComputeHash(bytes);
            string empty = string.Empty;
            byte[] numArray1 = numArray;
            for (int i = 0; i < (int)numArray1.Length; i++)
            {
                byte num = numArray1[i];
                empty = string.Concat(empty, string.Format("{0:x2}", num));
            }
            str = (length <= empty.Length ? empty.Substring(0, length) : empty);
            return str;
        }

        private static string MD5Hash(string text)
        {
            MD5 mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(text));
            byte[] hash = mD5CryptoServiceProvider.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < (int)hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }
            Console.WriteLine(string.Concat("md5 hash of they key=", stringBuilder.ToString()));
            return stringBuilder.ToString();
        }

        public void Dispose()
        {
            this._enc = null;
            this._key = null;
            this._iv = null;
            this._ivBytes = null;
            this._rcipher?.Dispose();
        }

        private enum EncryptMode
        {
            ENCRYPT,
            DECRYPT
        }
    }


    public class DESCryptoLib
    {

        public static byte[] Encrypt(string value)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes("4lgunACl4v3Bu3n4Incr3b4n");
            byte[] iV = System.Text.Encoding.UTF8.GetBytes("4lgunACl");

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] message = encoding.GetBytes(value);
            byte[] encryptValue = null;

            System.Security.Cryptography.TripleDESCryptoServiceProvider criptoProvider =
                new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            System.Security.Cryptography.ICryptoTransform criptoTransform =
                criptoProvider.CreateEncryptor(key, iV);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                System.Security.Cryptography.CryptoStream cryptoStream =
                    new System.Security.Cryptography.CryptoStream(memoryStream,
                             criptoTransform,
                             System.Security.Cryptography.CryptoStreamMode.Write);
                cryptoStream.Write(message, 0, message.Length);
                cryptoStream.FlushFinalBlock();
                encryptValue = memoryStream.ToArray();
            }

            return encryptValue;
        }


        /// <summary>
        /// Desencripta una cadena.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(byte[] value)
        {

            byte[] key = System.Text.Encoding.UTF8.GetBytes("4lgunACl4v3Bu3n4Incr3b4n");
            byte[] iV = System.Text.Encoding.UTF8.GetBytes("4lgunACl");

            System.Security.Cryptography.TripleDES cryptoProvider =
                new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            System.Security.Cryptography.ICryptoTransform cryptoTransform =
                cryptoProvider.CreateDecryptor(key, iV);
            System.IO.MemoryStream memoryStream =
                new System.IO.MemoryStream(value);
            System.Security.Cryptography.CryptoStream cryptoStream =
                new System.Security.Cryptography.CryptoStream(memoryStream,
                         cryptoTransform,
                         System.Security.Cryptography.CryptoStreamMode.Read);
            System.IO.StreamReader sr = new System.IO.StreamReader(cryptoStream, true);
            string textoLimpio = sr.ReadToEnd();
            return textoLimpio;
        }
    }
}
