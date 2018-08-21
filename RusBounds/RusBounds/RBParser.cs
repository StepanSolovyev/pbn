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
        
        public int GetPageN(string URL) //получение общего числа эмитентов
        {
            int i= 0; //счетчик тегов /a[i] с интервалами
            int j = 0;//счетчик тегов /a[i] с отдельными значениями страниц
            int MAX = 0; //последнее значение страницы в последнем интервале страниц
            string IPath = ""; //XPath нода с последним интервалом страниц
            int pageN = 0; //номер последней страницы
            bool test1;
            String allpage = ""; //значение интервала страниц
            HtmlAgilityPack.HtmlWeb web2 = new HtmlWeb();
            web2.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc2 = web2.Load(URL);
            try
            {
                do
                {
                    i++;
                    HtmlNode page = doc2.DocumentNode.SelectSingleNode("//html/body/div[1]/table[1]/tr/td/table/tr/td[2]/a[" + i + "]");
                    allpage = page.InnerHtml;
                    test1 = allpage.Substring(2, 3).Equals("..."); // This is true.                                    
                    MAX = int.Parse(allpage.Substring(5, 2));
                    //Console.WriteLine("MAX: ");
                    //Console.WriteLine(MAX);
                    IPath = page.XPath;
                    j = i;
                }
                while (test1);

            }
            catch { }  
            
            string allpageEnd = (doc2.DocumentNode.SelectSingleNode(IPath)).InnerHtml;
            string allpageEndHref = (doc2.DocumentNode.SelectSingleNode(IPath)).GetAttributeValue("href", null); // ссылка из последнего интервала
            HtmlAgilityPack.HtmlWeb web3 = new HtmlWeb();
            web3.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc3 = web3.Load("http://www.rusbonds.ru" + allpageEndHref);
            try
            { 
                do
                {
                j++;
                String pageEnd = (doc3.DocumentNode.SelectSingleNode("//html/body/div[1]/table[1]/tr/td/table/tr/td[2]/a[" + j + "]")).InnerHtml;
                 pageN = int.Parse(pageEnd);
                }
                while (j <= MAX);
            }
            catch { }
            Console.WriteLine("pageN: ");
          //  Console.WriteLine(pageN);

            return (pageN);//номер последней страницы (int)
        }                 
            
            
        public int GetLinesOfPage(string URL) //получение числа строк в таблице на странице
        {
            HtmlAgilityPack.HtmlWeb webLine = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument docLine = webLine.Load(URL);
            int nline = 0;
            int k = 1;
            try
            {
                do
                {
                    string farLine = (docLine.DocumentNode.SelectSingleNode("/html/body/div[1]/table[2]/tbody/tr[" + k + "]/td[2]/a")).InnerHtml;
                    nline++;
                    k++;
                }
                while (k != 200); //сравнение с общим числом эмитентов или с this.GetMAX("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=1&sort=desc")
            }
            catch { }
            Console.WriteLine("nline: ");
            return (nline);
        } 

        public emitent GetEmitentData(string idemitent)
           {
            emitent Emit = new emitent();
            string XP = "http://www.rusbonds.ru/ank_org.asp?emit=" + idemitent;
            //RusB = featuredArticle.GetAttributeValue("href", null);

            HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
            web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
            Emit.emit = int.Parse(idemitent);
            Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html//body/table[4]//tbody/tr[6]/td[2]").InnerText.Replace(" ", ""));
            return (Emit);
            }
        }

    
}
