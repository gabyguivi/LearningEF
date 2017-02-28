using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Challenge.WebApi.Util
{
    //Used to generate application_id, secret and tokens
    public class CryptoHelper
    {
        private static byte[] tdesKey = UTF8Encoding.UTF8.GetBytes("012345678901234567890123"), tdesIV = UTF8Encoding.UTF8.GetBytes("01234567");
        /// <summary>
        /// Encrypts a string with the TripleDES algorithm
        /// </summary>
        /// <param name="text">token to be escrypted</param>
        /// <returns></returns>
        public static string EncryptText(string text)
        {
            byte[] message = UTF8Encoding.UTF8.GetBytes(text);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            ICryptoTransform cripto = tdes.CreateEncryptor(tdesKey, tdesIV);
            byte[] encripted = cripto.TransformFinalBlock(message, 0, message.Length);
            string stringEncripted = Convert.ToBase64String(encripted, 0, encripted.Length);
            return stringEncripted;
        }

        /// <summary>
        /// Dencrypts a string with the TripleDES algorithm
        /// </summary>
        /// <param name="text">string tobe descrypted</param>
        /// <returns></returns>
        public static string DecryptText(string text)
        {
            TripleDES tdes = new TripleDESCryptoServiceProvider();
            ICryptoTransform cripto = tdes.CreateDecryptor(tdesKey, tdesIV);
            byte[] message = Convert.FromBase64String(text);
            byte[] encriptado = cripto.TransformFinalBlock(message, 0, message.Length);
            string textoLimpio = UTF8Encoding.UTF8.GetString(encriptado);
            return textoLimpio;
        }
    }
}