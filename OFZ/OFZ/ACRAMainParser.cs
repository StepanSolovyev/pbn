using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OFZ
{
    public struct maintable
    {
        public int emit;
        public string name;//наименование
        public string rating;//рейтинг
        public string forecast; //прогноз/пересмотр
        public string sector; //сектор
        public string eregion; //регион
        public string data; //дата
        public ulong inn;
        //public emitent(int emitid)
        //{
        //    emit = emitid;
        //    name = "";
        //    sector = "";
        //    eregion = "";
        //     inn = 0;

        //}
    }
    class ACRAMainParser
    {
        public maintable GetMainData(string url)
        {
            maintable MTable = new maintable();
            string XP = "https://www.acra-ratings.ru/ratings/issuers/" + idemitent;

            HtmlAgilityPack.HtmlWeb webMT = new HtmlWeb();
            webMT.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument docMT = webMT.Load(XP);
            Emit.emit = int.Parse(idemitent);
            Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div/section[2]/div/div/div[1]/div[3]/span[2]").InnerText.Replace(" ", ""));
            return (Emit);
        }
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
}
