using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cinema.Services
{
    public class Hash
    {
        public string GerarMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(texto);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                foreach (var c in hashBytes)
                {
                    sb.Append(c.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}