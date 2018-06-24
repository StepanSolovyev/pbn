using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Web;
using System.Net.Http;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GetInflation
{
    class Program
    {
        //decimal GetInflation (DateTime startMonth, DateTime endMonth);

        static void Main(string[] args)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                "http://www.inflationinrussia.com/DesktopModules/WebServices.asmx/GetFirstMonth");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = @"{}"; ;
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

    }
}




//string myJson = @"{startMonth: '' + '2010/01/01Z' + '\', endMonth : '' + '2010/05/01Z' + '\' }";
//GetPageSizeAsync("http://www.inflationinrussia.com/DesktopModules/WebServices.asmx/GetInflation", myJson).Wait();
//Console.Write("end\n");
//        }
//        private static async Task GetPageSizeAsync(string url, string json)
//{
//    using (var client = new HttpClient())
//    {
//        HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
//        Console.WriteLine(response.Content.ReadAsStringAsync().Wait());
//    }