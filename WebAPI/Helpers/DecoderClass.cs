using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WebAPI.Helpers
{
    public class HelperClass
    {
        public static string[] DecodeCredentials(HttpRequestMessage request)
        {
            string encodedCredentials = request.Headers.Authorization.Parameter;
            byte[] credentialBytes = Convert.FromBase64String(encodedCredentials);
            string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');
            return credentials;
        }

        public static string[] DecodeCredentials(string authHeader)
        {
            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            string[] credentials = new string[2];
            credentials[0] = usernamePassword.Substring(0, seperatorIndex);
            credentials[1] = usernamePassword.Substring(seperatorIndex + 1);

            return credentials;
        }
    }
}