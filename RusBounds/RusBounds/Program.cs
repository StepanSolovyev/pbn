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
            Console.ReadKey();

            //IHtmlDocument angle = new HtmlParser(html).Parse();
            //Наименование:Министерство финансов Российской ФедерацииОсновной ОКВЭД:Управление финансовой деятельностью и деятельностью в сфере налогообложенияСтрана:РОССИЯРегион:г.МоскваИНН:7710168360    
        }

    }
}

