using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusBounds3
{//nano edit1
    class Program
    {
        static void Main(string[] args)
        {
            RusBounds.RBParser test = new RusBounds.RBParser();
            test.GetPageCounter("http://www.rusbonds.ru/srch_simple.asp?go=0&ex=0&s=1&d=1&p=0");
            test.GetRowCounter("http://www.rusbonds.ru/srch_simple.asp?go=0&ex=0&s=1&d=1&p=0");
        }
    }
}
