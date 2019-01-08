using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Models
{
    public class Hashobject
    {


        public string Password { get; set; }

        public Hashobject(string password)
        {
            Password = password;
        }

        public string Hashedstring(string sSourceData)
        {

            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            return ByteArrayToString(tmpHash);

        }



        public static string ByteArrayToString(byte[] arrInput)

        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);

            for (i = 0; i < arrInput.Length; i++)

            {
                sOutput.Append(arrInput[i].ToString("X2"));

            }

            return sOutput.ToString();
        }

    }
}
