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
            //string emitentnamber = "6874";

            //Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);
            Console.WriteLine(Parser.GetPageN("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt"));
            Console.WriteLine(Parser.GetLinesOfPage("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt"));

            Console.ReadKey();
            emitentMain[] Test = Parser.Start();

            foreach (emitentMain item in Test)
            {
                Console.WriteLine(item.Issuer + " " + item.inn);

            }
            Console.ReadLine();
        }

    }
}

