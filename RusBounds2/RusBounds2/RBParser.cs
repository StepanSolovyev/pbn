using System;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

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

    public struct emitentMain
    {
        public int emit;//??
        public string Industry; // отрасль
        public string Issuer;   // название эмитента
        public string Region;   // регион
        public ulong CharterCapital;  // уставной капитал  (1000 RUB - вытягивать 1000)
        public string CharterCapitalСurrency;  // валюта уставного капитала (1000 RUB - вытягивать RUB)
        public int DomesticLoansCnt; // внутренние займы(кол-во)
        public ulong DomesticLoansСurrency; // внутренние займы(объем RUB)
        public int ForeignLoansCnt; // внешние займы(кол-во)
        public ulong ForeignLoansСurrency; //внешние займы (объем USD)
        public string Rating; // рейтинг (!!! по ТЗ bool (0,1) 0- нет, 1 -Есть)
        public ulong inn;
        public emitentMain(int emitidMain)
        {
            emit = emitidMain;
            Industry = "";
            Issuer = "";
            Region = "";
            CharterCapital = 0;
            CharterCapitalСurrency = "";
            DomesticLoansCnt = 0;
            DomesticLoansСurrency = 0;
            ForeignLoansCnt = 0;
            ForeignLoansСurrency = 0;
            Rating = "";
            inn = 0;
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

        public emitent GetEmitentData(string idemitent) //получение ИНН эмитента
        {
            emitent Emit = new emitent();
            string XP = "http://www.rusbonds.ru/ank_org.asp?emit=" + idemitent;
            //RusB = featuredArticle.GetAttributeValue("href", null);
            try
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
                Emit.emit = int.Parse(idemitent);
                //получение Наименования
                try
                {
                    Emit.name = doc1.DocumentNode.SelectSingleNode("/html//body/table[4]//tbody/tr[2]/td[2]");
                }
                catch
                {
                    Emit.name = "";
                    
                }

                //получение 

                try
                {
                    Emit.name = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html//body/table[4]//tbody/tr[6]/td[2]").InnerText.Replace(" ", ""));
                }
                catch
                {
                    if (Emit.inn == 0)
                    {
                        try
                        {
                            Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html//body/table[4]//tbody/tr[7]/td[2]").InnerText.Replace(" ", ""));
                        }
                        catch
                        { Emit.inn = 0; }

                    }
                }
                return (Emit);
            }
            catch {
                Emit.inn = 0;
                Console.WriteLine("the page of Emitent not found");
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
        public emitentMain[] Start()

    {
            int cntpage = this.GetPageN("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt1#rslt"); //определние кол-ва страниц
            int MP = this.GetLinesOfPage("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt");//получение количества строк на первой странице (nline)
            //Console.WriteLine(MP *cntpage);

            emitentMain[] EmitM = new emitentMain[MP * cntpage];//определение размера масива структур (общее количесто эмитентов)
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
                                HtmlNode IndustryInTable = row.SelectSingleNode("td[1]"); //Отрасль
                                EmitM[j].Industry = IndustryInTable.InnerText;
                                //Console.WriteLine(NumInTable.InnerText);
                                HtmlNode IssuerInTable = row.SelectSingleNode("td[2]/a");//Название Эмитента
                                EmitM[j].Issuer = IssuerInTable.InnerText;
                            //Console.WriteLine(TimeOfBound.InnerText);
                            try
                            {
                                HtmlNode RegionInTable = row.SelectSingleNode("td[3]");//Регион
                                EmitM[j].Region = RegionInTable.InnerText;
                                if (RegionInTable.InnerText == "&nbsp;")
                                {
                                    EmitM[j].Region = " ";
                                }
                            }
                            catch
                            {
                                EmitM[j].Region = "";
                            }
                            //Console.WriteLine(NameOfBound.InnerText);
                            try
                            {
                                HtmlNode CharterCapitalInTable = row.SelectSingleNode("td[4]");
                                if (CharterCapitalInTable.InnerText == "&nbsp;")
                                {
                                    EmitM[j].CharterCapital = 0; // Уставной капитал  (1000 RUB - вытягивать 1000)
                                    EmitM[j].CharterCapitalСurrency = ""; // валюта уставного капитала (1000 RUB - вытягивать RUB)}
                                }
                                else
                                {
                                   string[] U = this.GetCapital(CharterCapitalInTable.InnerText); //вытягиваем число с пробелами из уставного капитала до букв валюты   
                                   EmitM[j].CharterCapital = ulong.Parse(U[0].Replace(" ", String.Empty)); // удаляем пробелы из числа
                                   //this.Сurrency(CharterCapitalInTable.InnerText);
                                   string[] V = this.Сurrency(CharterCapitalInTable.InnerText); //вытягиваем валюту из уставного капитала
                                   EmitM[j].CharterCapitalСurrency = (V[V.Length-1].Replace(" ", String.Empty)); // удаляем пробелы из числа
                                    }
                            }
                            catch
                            {
                                  EmitM[j].CharterCapital = 0;
                            }
                            //Console.WriteLine(redemption.InnerText);
                            try
                            {
                                  HtmlNode DLCntInTable = row.SelectSingleNode("td[5]");// внутренние займы(кол-во)
                                  EmitM[j].DomesticLoansCnt = int.Parse(DLCntInTable.InnerText);
                                    
                            }
                            catch
                            {
                                EmitM[j].DomesticLoansCnt = 0;
                            }
                            try
                            {
                                HtmlNode DLСurInTable = row.SelectSingleNode("td[6]");// Внутренние займы(объем RUB)

                                EmitM[j].DomesticLoansСurrency = ulong.Parse(DLСurInTable.InnerText.Replace(" ", string.Empty));                               
                                      
                            }
                            catch
                            { EmitM[j].DomesticLoansСurrency = 0; }
                            try
                            {
                                HtmlNode FLCntInTable = row.SelectSingleNode("td[7]");// внешние займы(кол-во)
                                if (FLCntInTable.InnerText == "&nbsp;")
                                {
                                    EmitM[j].ForeignLoansCnt = 0; // внешние займы(кол-во)
                                }
                                else
                                {
                                    EmitM[j].ForeignLoansCnt = int.Parse(FLCntInTable.InnerText); // удаляем пробелы из числа
                                }
                                   
                            }
                            catch { EmitM[j].ForeignLoansCnt = 0; }
                            try
                            {
                                HtmlNode FLСurInTable = row.SelectSingleNode("td[8]");// внешние займы(объем USD)
                                if (FLСurInTable.InnerText == "&nbsp;")
                                {
                                    EmitM[j].ForeignLoansСurrency = 0;
                                }
                                else
                                {
                                    EmitM[j].ForeignLoansСurrency = ulong.Parse(FLСurInTable.InnerText.Replace(" ", string.Empty));
                                }
                            }
                            catch { EmitM[j].ForeignLoansСurrency = 0; }

                                HtmlNode RatingInTable = row.SelectSingleNode("td[9]"); // рейтинг
                                EmitM[j].Rating = RatingInTable.InnerText;

                                HtmlNode mhref = row.SelectSingleNode("td[2]/a");// ссылка  http://www.rusbonds.ru/ank_org.asp?emit=78881
                                string INN = mhref.GetAttributeValue("href", null);
                                string resultINN = INN.Split('=')[1];
                                RBParser Parser = new RBParser();
                                string emitentnamber = resultINN;
                                EmitM[j].inn = Parser.GetEmitentData(emitentnamber).inn;
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
