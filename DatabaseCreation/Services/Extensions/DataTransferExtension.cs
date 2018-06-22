using System.Net;
using System.Text;

namespace DatabaseCreation.Services.Extensions
{
    public static class DataTransferExtension
    {
        public static void Post(this string uri, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var postData = Encoding.ASCII.GetBytes(data);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(postData, 0, data.Length);
            }
        }
    }
}