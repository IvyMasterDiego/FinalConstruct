using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FinalProjectConstruct
{
    public static class TextEncrypt
    {
        public static byte[] IV { get; set; }
        public static byte[] Key { get; set; }

        /// <summary>
        /// Este metodo es el que se encarga de encriptar la informacion recibida. 
        /// </summary>
        /// <param name="str"> Se requiere que se le pase la data que se va a encriptar</param>
        /// <returns>string encriptado </returns>
        public static string Encrypttext(this string str)
        {
            try
            {
                if (str == string.Empty)
                    return string.Empty;
                byte[] bytes = new UTF8Encoding().GetBytes(str);
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.IV = TextEncrypt.IV;
                cryptoServiceProvider.Key = TextEncrypt.Key;
                ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor(TextEncrypt.Key,
                    TextEncrypt.IV);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
    }

}
