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
        public string rating;
        public string forecast; //прогноз/пересмотр
        public string sector;
        public string eregion;
        public string data;
        public ulong inn;
       
        public emitent(int emitid)
        {
            emit = emitid;
            name = "";
            rating = "";
            forecast = "";
            sector = "";
            eregion = "";
            data = "";
            inn = 0;
            
        }
    }
    public class ACRAParser
    {
        //получение общего числа эмитентов из строки вида "Найдено документов: 155":
        public int GetMAX(string URL)
        {
            HtmlAgilityPack.HtmlWeb web2 = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc2 = web2.Load(URL);
            // odl method:
            // int allpage = int.Parse((doc2.DocumentNode.SelectSingleNode("/html/body/div[1]/div[5]/div/section/span")).InnerHtml.Split(' ')[2]);
            HtmlNode tempnode = doc2.DocumentNode.SelectSingleNode("//*[@id='search-results']/div/section/span");
            int allpage = int.Parse(tempnode.InnerHtml.Split(' ')[2]);
            return (allpage);
        }

        //получение числа строк в таблице на странице
       
        public int GetLinesOfPage(string URL)
        {
            HtmlAgilityPack.HtmlWeb webLine = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument docLine = webLine.Load(URL);
            int nline = 0;
            int k = 1;
            
                try
                {
                    do
                    {
                        string farLine = (docLine.DocumentNode.SelectSingleNode("//*[@id='search-results']/div/section/table/tbody/tr[" + k + "]/td/a")).InnerHtml;
                        nline++;
                        k++;
                    }
                    while (k != 200); //сравнение с общим числом эмитентов или с this.GetMAX("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=1&sort=desc")
            }
                
                catch{
                // no actions required just return value
            }
            
         return (nline);
        }

        public emitent GetEmitentData(string idemitent)
        {  
            //определение значения ИНН эмитента с учетом разного вида отображния на странице (указан только ИНН, указан ИНН и БИК)
            emitent Emit = new emitent();
            string XP = "https://www.acra-ratings.ru/ratings/issuers/" + idemitent;

            HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
            web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument doc1 = web1.Load(XP);
            Emit.emit = int.Parse(idemitent);
            try
            {
                Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div/section[2]/div/div/div[1]/div[3]/span[2]").InnerText.Replace(" ", ""));
            }
            catch
            { }
            try
            {
                Emit.inn = ulong.Parse(doc1.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div/section[2]/div/div/div[1]/div[3]/span[2]").InnerText.Replace(" ", ""));
            }
            catch
            { }
            return (Emit);
        }
        
        public emitent[] Start()
        {
            

            emitent[] Emits = new emitent[this.GetMAX("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=1&sort=desc")];//определение размера масива структур (общее количесто эмитентов) 
            int num = 1;
            int i = 1;
            int j = 0;

            this.GetLinesOfPage("https://www.acra-ratings.ru/ratings/issuers");//получение количества строк на первой странице (nline)
            do
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                //URL адрес с изменяемым порядковым номером страницы i
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
                try
                {   //проверка наличия строки "Найдено документов: 155"
                    var far = doc1.DocumentNode.SelectSingleNode("//*[@id='search-results']/div/section/span");
                    string enddo = far.InnerHtml;
                    
#if DEBUG
                    Console.WriteLine("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
#endif
                    //выгрузка данных из таблицы постранично
                    HtmlAgilityPack.HtmlWeb webALL1 = new HtmlWeb();

                    HtmlAgilityPack.HtmlDocument docALL1 = webALL1.Load("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
                                                            
                    do
                    {
                        foreach (HtmlNode row in docALL1.DocumentNode.SelectNodes("//*[@id='search-results']/div[1]/section/table/tbody/tr[" + num + "]"))
                            if (row != null)
                            {
#if DEBUG
                                Console.WriteLine(num);
#endif
                             
                                HtmlNode NumInTable = row.SelectSingleNode("td[1]/a"); //наименование
                                Emits[j].name = NumInTable.InnerText;
                                //Console.WriteLine(NumInTable.InnerText);
                                HtmlNode TimeOfBound = row.SelectSingleNode("td[2]/span[2]");//рейтинг
                                Emits[j].rating = TimeOfBound.InnerText;
                                //Console.WriteLine(TimeOfBound.InnerText);
                                HtmlNode NameOfBound = row.SelectSingleNode("td[3]/span[2]");//прогноз пересмотр
                                Emits[j].forecast = NameOfBound.InnerText;
                                //Console.WriteLine(NameOfBound.InnerText);
                              
                                HtmlNode redemption = row.SelectSingleNode("td[4]/a"); // сектор
                                Emits[j].sector = redemption.InnerText;
                                //Console.WriteLine(redemption.InnerText);
                                HtmlNode YearsToRedemption = row.SelectSingleNode("td[5]/span[2]"); //регион
                                Emits[j].eregion = YearsToRedemption.InnerText;
                                //Console.WriteLine(YearsToRedemption.InnerText);
                                HtmlNode DoxodnosTbParse = row.SelectSingleNode("td[6]/span[2]");// дата
                                Emits[j].data = DoxodnosTbParse.InnerText;
                                //Console.WriteLine(DoxodnosTbParse.InnerText);
                                
                                HtmlNode mhref = row.SelectSingleNode("td[1]/a");// ссылка
                                string INN = mhref.GetAttributeValue("href", null);
                                string resultINN = INN.Split('/')[3];

                                ACRAParser Parser = new ACRAParser();
                                string emitentnamber = resultINN;
                                Emits[j].inn = Parser.GetEmitentData(emitentnamber).inn;
                                //Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);
                                //Console.WriteLine(resultINN);
                                num = num + 1;
                                                                
                            }
                        j++; //счетчик элементов массива ()
                    }

                    while (num <= this.GetLinesOfPage("https://www.acra-ratings.ru/ratings/issuers")); //проверка конца массива строк на странице
                    i = i + 1;//счетчик страниц
                    num = 1;//счетчик строк
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            while (i != 100);//заведомо большое число страниц
           
            return (Emits);
        }

    }
}
