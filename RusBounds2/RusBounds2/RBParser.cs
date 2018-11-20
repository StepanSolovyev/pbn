using System;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace RusBounds
{
    public struct emitent
    {
        public int emit;
        public string name;
        //public string discription;
        public string okved;
        public string country;
        //public string eregion;
        public ulong inn;
        public int okpo;
        public string regdata;
        public string juridadress;
        public string postadress;
        public string property;
        public ulong capital;
        public string currency;

        //public string Industry; // отрасль
        //public string Issuer;   // название эмитента
        public string Region;   // регион
        //public ulong CharterCapital;  // уставной капитал  (1000 RUB - вытягивать 1000)
        //public string CharterCapitalСurrency;  // валюта уставного капитала (1000 RUB - вытягивать RUB)
        //public int DomesticLoansCnt; // внутренние займы(кол-во)
        //public ulong DomesticLoansСurrency; // внутренние займы(объем RUB)
        //public int ForeignLoansCnt; // внешние займы(кол-во)
        //public ulong ForeignLoansСurrency; //внешние займы (объем USD)
        //public string Rating; // рейтинг (!!! по ТЗ bool (0,1) 0- нет, 1 -Есть)
        public emitent(int emitid)
        {
            emit = emitid;
            name = "";
            //discription = "";
            okved = "";
            country = "";
            //eregion = "";
            inn = 0;
            okpo = 0;
            regdata = "";
            juridadress = "";
            postadress = "";
            property = "";
            capital = 0;
            currency = "";

            //Industry = "";
            //Issuer = "";
            Region = "";
            //CharterCapital = 0;
            //CharterCapitalСurrency = "";
            //DomesticLoansCnt = 0;
            //DomesticLoansСurrency = 0;
            //ForeignLoansCnt = 0;
            //ForeignLoansСurrency = 0;
            //Rating = "";
        }
    }

    
    public class RBParser
    {
        public int GetPageN(string URL) //получение общего числа эмитентов
        {
                int i = 0; //счетчик тегов /a[i] с интервалами
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
                //Console.WriteLine("pageN: ");
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
                //Console.WriteLine("nline: ");
                return (nline);
            }

        public emitent GetEmitentData(string emithref) //получение данных эмитента по указанным в switch полям
        {
            emitent Emit = new emitent();
            string XP = "http://www.rusbonds.ru" + emithref;
            //RusB = featuredArticle.GetAttributeValue("href", null);
            try
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
                //Emit.emit = int.Parse(idemitent);

                HtmlNodeCollection EmitentInfo = doc1.DocumentNode.SelectNodes("/html//body/table[4]//tbody/tr");
                foreach(HtmlNode node in EmitentInfo)
                {
                    string swch = node.InnerText.Split(':')[0];
                    switch (swch)
                    {
                        case "Наименование":
                            Emit.name = node.InnerText.Split(':')[1];
                            break;
                        case "Основной ОКВЭД":
                            Emit.okved = node.InnerText.Split(':')[1];
                            break;
                        case "Страна":
                            Emit.country = node.InnerText.Split(':')[1];
                            break;

                        case "Регион":
                            Emit.Region = node.InnerText.Split(':')[1];
                            break;

                        case "ИНН":
                            Emit.inn = ulong.Parse(node.InnerText.Split(':')[1]);
                            break;

                        case "ОКПО или др.":
                            Emit.okpo = int.Parse(node.InnerText.Split(':')[1]);
                            break;

                        case "Данные госрегистрации":
                            Emit.regdata = node.InnerText.Split(':')[1];
                            break;

                        case "Юридический адрес":
                            Emit.juridadress = node.InnerText.Split(':')[1];
                            break;

                        case "Почтовый адрес":
                            Emit.postadress = node.InnerText.Split(':')[1];
                            break;
                        case "Вид собственности":
                            Emit.property = node.InnerText.Split(':')[1];
                            break;

                        case "Уставный капитал":
                            string[] U = this.GetCapital(node.InnerText.Split(':')[1]); //вытягиваем число с пробелами из уставного капитала до букв валюты   
                            Emit.capital = ulong.Parse(U[0].Replace(" ", String.Empty)); // удаляем пробелы из числа

                            string[] V = this.Сurrency(node.InnerText.Split(':')[1]); //вытягиваем валюту из уставного капитала
                            Emit.currency = (V[V.Length - 1].Replace(" ", String.Empty)); // удаляем пробелы из числа
                            break;

                        default:
                            break;
                    }                 
                }                
                
                return (Emit);
            }
            catch(Exception e)
                {
                Emit.inn = 0;
                Console.WriteLine("the page of Emitent not found" + e.Message);
                return (Emit);                
                }
            }

        public string[] GetCapital(string Val) // получение числа уставного капитала до валюты
        {
            string pattern = "[A-Z]+";
            //string input = "Abc1234Def5678Ghi9012Jklm";
            string[] result = Regex.Split(Val, pattern,
                                          RegexOptions.IgnoreCase);
            return (result);
        }

       public string[] Сurrency(string Val1)
        {
            string pattern = "[0-9]";
            //string input = "Abc1234Def5678Ghi9012Jklm";
           string[] result1 = Regex.Split(Val1, pattern,
                                          RegexOptions.IgnoreCase);
            
            return (result1);
        }
        public emitent[] Start()

    {
            int cntpage = this.GetPageN("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt1#rslt"); //определние кол-ва страниц
            int MP = this.GetLinesOfPage("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt");//получение количества строк на первой странице (nline)
            //Console.WriteLine(MP *cntpage);

            emitent[] EmitM = new emitent[MP * cntpage];//определение размера масива структур (общее количесто эмитентов)
            int num = 1;
            int i = 1;

            int j = 0;
            
            do
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                //URL адрес с изменяемым порядковым номером страницы i
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=" + i + "#rslt");
                   
                try
                {   
#if DEBUG
                    // Console.WriteLine("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=" + i + "#rslt");
#endif
                    //выгрузка данных из таблицы постранично
                    HtmlAgilityPack.HtmlWeb webALL1 = new HtmlWeb();
                    webALL1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                    HtmlAgilityPack.HtmlDocument docALL1 = webALL1.Load("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=" + i + "#rslt");
                       
                    do
                    {
                        foreach (HtmlNode row in docALL1.DocumentNode.SelectNodes("/html/body/div[1]/table[2]/tbody/tr[" + num + "]"))
                            if (row != null)
                            {
#if DEBUG
                                Console.WriteLine(num);
#endif
                                
                                //HtmlNode IssuerInTable = row.SelectSingleNode("td[2]/a");//Название Эмитента
                                //EmitM[j].Issuer = IssuerInTable.InnerText;
                                                           
                                HtmlNode mhref = row.SelectSingleNode("td[2]/a");// ссылка  http://www.rusbonds.ru/ank_org.asp?emit=78881
                                string emithref = mhref.GetAttributeValue("href", null);
                                //string resultINN = INN.Split('=')[1];
                                RBParser Parser = new RBParser();
                                //string emitentnamber = emithref;
                                //EmitM[j].inn = 
                                emitent Temp = Parser.GetEmitentData(emithref);
                                EmitM[j].inn = Temp.inn;
                                EmitM[j].capital = Temp.capital;
                                EmitM[j].country = Temp.country;
                                EmitM[j].currency = Temp.currency;
                                //EmitM[j].discription = Temp.discription;
                                //EmitM[j].eregion = Temp.eregion;
                                EmitM[j].juridadress = Temp.juridadress;
                                EmitM[j].name = Temp.name;
                                EmitM[j].okpo = Temp.okpo;
                                EmitM[j].okved = Temp.okved;
                                EmitM[j].postadress = Temp.postadress;
                                EmitM[j].regdata = Temp.regdata;
                                //Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);
                                //Console.WriteLine(resultINN);
                                num = num + 1;
                            }
                        j++; //счетчик элементов массива ()
                    }
                    while (num <= MP); //проверка конца массива строк на странице
                    i = i + 1;//счетчик страниц
                    num = 1;//счетчик строк
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            while (i != 300);//заведомо большое число страниц
            return (EmitM);
         }
    }
    
}
