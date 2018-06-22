using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OFZ
{
    public struct emitent
    {
        public int emit;
        public string name;//наименование
        public string rating;//рейтинг
        public string forecast; //прогноз/пересмотр
        public string sector; //сектор
        public string eregion; //регион
        public string data; //дата
        //public emitent(int emitid)
        //{
        //    emit = emitid;
        //    name = "";
        //    sector = "";
        //    eregion = "";
       //     inn = 0;

        //}
    }
    //class ACRAMainParser
    //{
       
       // HtmlAgilityPack.HtmlWeb webmain = new HtmlWeb();

       //public HtmlAgilityPack.HtmlDocument docmain = webmain.Load("https://www.acra-ratings.ru/ratings/issuers?order=press_release&page=1&sort=desc");

        //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section[2]/div/div/p/text()");
        //*[@id="search-results"]/div/section/table/tbody/tr[1]/td[2]/span[2]
        //   string text = featuredArticle.

        //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div/div[5]/div/table/tbody//tr");
        //Console.WriteLine(featuredArticle.InnerHtml);
        //Console.ReadKey();
    //}
}
 int i = 1;
            do
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
HtmlAgilityPack.HtmlDocument doc1 = web1.Load("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
var far = doc1.DocumentNode.SelectSingleNode("/html/body/div[1]/div[5]/div/section/span");
string End = far.InnerHtml;
Console.WriteLine(far.InnerHtml);
                Console.WriteLine("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
                i = i + 1;
            }
            while (i != 6);
