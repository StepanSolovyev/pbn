using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using ACRA;
using System.Data;


namespace OFZ
{
    class Program
    {
        static void Main(string[] args)
        {                                
            ACRAParser parser = new ACRAParser();
            emitent[] Test = parser.Start();
            foreach (emitent item in Test)
            {
                Console.WriteLine(item.name +" " + item.inn);
                
            }
            Console.ReadLine();
        }
     }

}
    
