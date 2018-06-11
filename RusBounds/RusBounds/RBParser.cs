using System;
using System.Text;
using HtmlAgilityPack;

namespace RusBounds
{
    public struct emitent
    {
        public int emit;
        public string name;
        public string discription;
        public string okved;
        public string country;
        public string eregion;
        public ulong inn;
        public int okpo;
        public string regdata;
        public string juridadress;
        public string postadress;
        public string property;
        public int capital;
        public string currency;
        public emitent(int emitid)
        {
            emit = emitid;
            name = "";
            discription = "";
            okved = "";
            country = "";
            eregion = "";
            inn = 0;
            okpo = 0;
            regdata = "";
            juridadress = "";
            postadress = "";
            property = "";
            capital = 0;
            currency = "";
        }
    }
    public class RBParser
    {
        public emitent GetEmitentData(string idemitent)
           {
            emitent Emit = new emitent();
            string XP = "http://www.rusbonds.ru/ank_org.asp?emit=" + idemitent;
            
            HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
            web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
            Emit.emit = int.Parse(idemitent);
            Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html//body/table[4]//tbody/tr[6]/td[2]").InnerText.Replace(" ", ""));
            return (Emit);
            }
        }

    
}
