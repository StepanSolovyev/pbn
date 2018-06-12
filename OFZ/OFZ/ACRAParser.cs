using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace ACRA

{
    public struct emitent
    {
        public int emit;
        public string name;
        public string sector;
        public string eregion;
        public ulong inn;
        public emitent(int emitid)
        {
            emit = emitid;
            name = "";
            sector = "";
            eregion = "";
            inn = 0;
            
        }
    }
    public class ACRAParser
    {
        public emitent GetEmitentData(string idemitent)
        {
            emitent Emit = new emitent();
            string XP = "https://www.acra-ratings.ru/ratings/issuers/" + idemitent;

            HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
            web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
            Emit.emit = int.Parse(idemitent);
            Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div/section[2]/div/div/div[1]/div[3]/span[2]").InnerText.Replace(" ", ""));
            return (Emit);
        }
    }
}
