using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SP_parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = UTF8Encoding.UTF8;

            string str = web.DownloadString("https://www.cbr.ru/");
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='widget opened type_table name_rate']/div/table/tbody/tr/td[2]");
            HtmlAgilityPack.HtmlNode node1 = doc.DocumentNode.SelectSingleNode("//div[@class='widget opened type_table name_rate']/div/table/tbody/tr/td[1]/a");
            string s = node.InnerText;
            string d = node1.InnerText;
            string result = d.Trim();
            Console.WriteLine(s);
            string data = result.Split(' ')[5];
            Console.WriteLine(data);


        }
    }
}