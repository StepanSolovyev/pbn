using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OFZ
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            
            HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.rusbonds.ru/srch_emitent.asp");                 
            
            var featuredArticle = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/table[2]/tbody[1]/tr[1]/td[2]/a");        
         
            string XPINN = featuredArticle.GetAttributeValue("href", null);
            //Console.WriteLine(XPINN);
            string XP = "http://www.rusbonds.ru" + XPINN;
            Console.WriteLine(XP);
            //Console.WriteLine(featuredArticle.InnerText);
            //Console.ReadKey();


            //ИНН
            HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
            web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
            //string INN0 = doc1.DocumentNode.InnerText;
            //Console.WriteLine(result);
            //char[] EndINN= {'ИНН' };
            //string resultINN0 = INN0.TrimEnd(EndINN);
            //Console.WriteLine(resultINN0);
            var INN1 = doc1.DocumentNode.SelectSingleNode("/html/body/table[4]/tbody/tr/td[4]/table[7]/tbody/tr[1]/td/b");
            Console.WriteLine(INN1.InnerText);
            Console.ReadKey();

            //IHtmlDocument angle = new HtmlParser(html).Parse();
            //Наименование:Министерство финансов Российской ФедерацииОсновной ОКВЭД:Управление финансовой деятельностью и деятельностью в сфере налогообложенияСтрана:РОССИЯРегион:г.МоскваИНН:7710168360    
        }

    }
}

