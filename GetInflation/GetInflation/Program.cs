using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Web;

namespace GetInflation
{
    class Program
    {
        //decimal GetInflation (DateTime startMonth, DateTime endMonth);

        static void Main(string[] args)
        {
                //soap запрос на получение уровня инфляции
                StringBuilder sb = new StringBuilder();
                sb.Append("<soap:Envelope xmlns: xsi =\"http://www.w3.org/2001/XMLSchema-instance \"");
                sb.Append(" xmlns: xsd =\"http://www.w3.org/2001/XMLSchema \"");
                sb.Append(" xmlns: soap =\"http://schemas.xmlsoap.org/soap/envelope/ \">");
                //sb.Append("<soapenv:Header>");
                //sb.Append("<sas:BasicAuth>");
                //sb.Append("<sas:Name>cccc-admin1</sas:Name>");
                //sb.Append("<sas:Password>!cccConfer*</sas:Password>");
                //sb.Append("</sas:BasicAuth>");
                //sb.Append("</soapenv:Header>");
                sb.Append("<soap:Body>");
                sb.Append("<GetInflation xmlns=http://www.inflationinrussia.com/WebServices >");
                sb.Append("<startMonth>01.01.2018</startMonth>");
                sb.Append("<endMonth>01.02.2018</endMonth>");
                sb.Append("</GetInflation>");
                sb.Append("</soap:Body>");
                sb.Append("</soap:Envelope>");

                //запрос
                WebRequest request = HttpWebRequest.Create("http://www.inflationinrussia.com/WebServices/GetInflation");
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";
                request.ContentLength = sb.Length;
                request.Credentials = CredentialCache.DefaultCredentials;
                //пишем тело
                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(sb.ToString());
                streamWriter.Close();
                //читаем тело
                WebResponse response = request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
                response.Close();
                //result - ответ
                Console.WriteLine(result);
        }
            
    }
}
