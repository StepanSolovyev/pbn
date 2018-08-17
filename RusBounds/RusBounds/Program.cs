using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RusBounds;

namespace OFZ
{
    class Program
    {
        static void Main(string[] args)
        {
            

            RBParser Parser = new RBParser();
            string emitentnamber = "6874";

            //RusB = featuredArticle.GetAttributeValue("href", null);
            Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);
            Console.WriteLine(Parser.GetPageN("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt"));
            Console.WriteLine(Parser.GetLinesOfPage("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt"));

            Console.ReadKey();

            //IHtmlDocument angle = new HtmlParser(html).Parse();
            //Наименование:Министерство финансов Российской ФедерацииОсновной ОКВЭД:Управление финансовой деятельностью и деятельностью в сфере налогообложенияСтрана:РОССИЯРегион:г.МоскваИНН:7710168360    
        }

    }
}

