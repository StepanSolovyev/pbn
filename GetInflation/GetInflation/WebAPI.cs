using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Xml;
namespace GetInflation
{
    enum SOAPAction : Int32 { GetFirstMonth, GetLastMonth, GetInflation, GetValueChange, GetPriceChange, BaseUri }
    static class WebAPI
    {


        static string SOAPActionGetUri(SOAPAction action)
        {
            switch (action)
            {
                case SOAPAction.GetFirstMonth:
                    return "http://www.inflationinrussia.com/WebServices/GetFirstMonth";
                case SOAPAction.GetLastMonth:
                    return "http://www.inflationinrussia.com/WebServices/GetLastMonth";
                case SOAPAction.GetInflation:
                    return "http://www.inflationinrussia.com/WebServices/GetInflation";
                case SOAPAction.GetValueChange:
                    return "http://www.inflationinrussia.com/WebServices/GetValueChange";
                case SOAPAction.GetPriceChange:
                    return "http://www.inflationinrussia.com/WebServices/GetPriceChange";

                // base uri to perform request
                case SOAPAction.BaseUri:
                    return "http://inflationinrussia.com/DesktopModules/WebServices.asmx";

                // debug purposes
                default:
                    return "error: SOAPActionGetUri returned default case.";
                    ;

            }


        }
        public static decimal GetPriceChange(Decimal startAmount, DateTime startMonth, DateTime endMonth, bool applyDenominationOf1998)
        { return 0; }

        public static decimal GetValueChange(Decimal startAmount, DateTime startMonth, DateTime endMonth, bool applyDenominationOf1998)
        { return 0; }

        public static decimal GetInflation(DateTime startMonth, DateTime endMonth)
        { return 0; }

        public static DateTime GetLastMonth() { return DateTime.Now; }
        public static DateTime GetFirstMonth()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml("<soap:Envelope xmlns: xsi = 'http://www.w3.org/2001/XMLSchema-instance' xmlns: xsd = 'http://www.w3.org/2001/XMLSchema' xmlns: soap = 'http://schemas.xmlsoap.org/soap/envelope/'>" +
                "<soap:Body>" +
                "<GetFirstMonth xmlns = 'http://www.inflationinrussia.com/WebServices'/>" +
                "</soap:Body>" +
                "</soap:Envelope>");

            xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);

            return DateTime.Now;
        }


        static string PerformWebRequest(XmlDocument xml, SOAPAction action)
        {
            WebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(SOAPActionGetUri(SOAPAction.BaseUri));
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("SOAPAction", SOAPActionGetUri(action));

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();

            }
        }
    }
}

/*
вот павершел пример, его на C# потом простопереведем


System.Xml xml = '<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetFirstMonth xmlns = "http://www.inflationinrussia.com/WebServices" />
  </ soap:Body>
</soap:Envelope>'

$headers = @{"SOAPAction" = "http://www.inflationinrussia.com/WebServices/GetFirstMonth"}

$URI = "http://inflationinrussia.com/DesktopModules/WebServices.asmx"
[xml]$out = Invoke-WebRequest $uri -Method post -ContentType 'text/xml; charset=utf-8' -Body $SOAP -Headers $headers

# show result 
$out.DocumentElement.Body.GetFirstMonthResponse.GetFirstMonthResult
*/