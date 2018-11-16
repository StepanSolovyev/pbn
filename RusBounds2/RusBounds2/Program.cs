using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RusBounds;

namespace RusBounds
{
    class Program
    {
        static void Main(string[] args)
        {
            

            RBParser Parser = new RBParser();
            
            emitent[] Test = Parser.Start();

            foreach (emitent item in Test)
            {
                Console.WriteLine(item.Issuer + " " + item.inn);

            }
            Console.ReadLine();
        }

    }
}

