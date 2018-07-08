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

namespace GetInflation
{
    class Program
    {
        //decimal GetInflation (DateTime startMonth, DateTime endMonth);

        static void Main(string[] args)
        {
            Console.WriteLine(WebAPI.GetFirstMonth());
            Console.ReadKey();
        }

    }
}


