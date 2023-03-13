using System;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace FreshMVC.Component
{   
    public class Authentication
    {
        public static string Encrypt(string toEncrypt, bool useHashing = true)
        {
            if (toEncrypt == null || toEncrypt == "")
            {
                return "";
            }
            else
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                string key = "!@#$%^*()";

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }


                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        public static string Decrypt(string cipherString, bool useHashing = true)
        {
            if (cipherString == null || cipherString == "")
            {
                return "";
            }
            else
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key = "!@#$%^*()";

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
        }

        public static string MD5Encrypt(string toEncrypt, bool useHashing = true)
        {
            string encodedData = string.Empty;
            if (toEncrypt == null || toEncrypt == "")
            {
                return "";
            }
            else
            {
                byte[] toEncryptArray = System.Text.Encoding.UTF8.GetBytes(toEncrypt);

                byte[] encData_byte = new byte[toEncrypt.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(toEncrypt);
                encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;
            }
        }

        public static string MD5Decrypt(string cipherString, bool useHashing = true)
        {
            if (cipherString == null || cipherString == "")
            {
                return "";
            }
            else
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(cipherString);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
        }

    }
}
