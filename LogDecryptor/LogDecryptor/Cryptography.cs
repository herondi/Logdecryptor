using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LogDecryptor
{
    class Cryptography
    {

        public static string DecryptDes(string textToDecrypt)
        {
   
            textToDecrypt = textToDecrypt.Trim(' ');
            string ToReturn = "";
                    #pragma warning disable CS0618 // Type or member is obsolete
                        string publickey = ConfigurationSettings.AppSettings["PublicKeyDes"];
                        string secretkey = ConfigurationSettings.AppSettings["SecretKeyDes"];
                    #pragma warning restore CS0618 // Type or member is obsolete

                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                //To do - create the exception that take care the utf-8 if the file isn´t encrypted and show the message!
                Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;

      
        }
        
    }


}



