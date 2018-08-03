using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using ACRA;
using System.Data;
using ACRAMain;

namespace OFZ
{
    class Program
    {
        static void Main(string[] args)
        {
        int num = 1;
        int i = 1;
            //string enddo = "";
            do
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                //URL адрес с изменяемым порядковым номером страницы i
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
                try
                {
                    var far = doc1.DocumentNode.SelectSingleNode("/html/body/div[1]/div[5]/div/section/span");
                    string enddo = far.InnerHtml; //строка с общим количеством документов вида "Найдено документов: 155"
                    //Console.WriteLine(enddo);
                    //Console.WriteLine("https://www.acra-ratings.ru/ratings/issuers?order=date_from&page=" + i + "&sort=desc");
                    //сюда вставить выгрузку данных из таблицы
                    i = i + 1;
                }
                catch (Exception)
                {
                    //Console.WriteLine("End of table, All Page: ", i);
                    break;
                }
            }
            while (i != 100);

            ACRAParser Parser = new ACRAParser();
            string emitentnamber = "54";

            //RusB = featuredArticle.GetAttributeValue("href", null);

            Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);

            Console.ReadKey();

            HtmlAgilityPack.HtmlWeb webALL1 = new HtmlWeb();

            HtmlAgilityPack.HtmlDocument docALL1 = webALL1.Load("https://www.acra-ratings.ru/ratings/issuers?order=press_release&page=1&sort=desc");

            //var featuredArticle = docALL1.DocumentNode.SelectSingleNode("/html/body/div[1]/div[5]/div[1]/section/table/tbody//tr");
            //*[@id="search-results"]/div/section/table/tbody/tr[1]/td[2]/span[2]
            //string text = featuredArticle.

            //все строки таблицы с одной страницы
            //var featuredArticle = docALL1.DocumentNode.SelectSingleNode("/html/body/div[1]/div[5]/div[1]/section/table/tbody");
            HtmlNodeCollection hnNode = docALL1.DocumentNode.SelectNodes("//table[@class='l-search-results__list']");
               if (hnNode != null)
            {
                foreach (var hn in hnNode)
                {
                    string str1 = hn.OuterHtml;
                    //Console.WriteLine(str1);
                    //Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("hnNode вернула null");
            }

            //string str1 = featuredArticle.InnerHtml;
            //Console.WriteLine(str1);
            //Console.ReadKey();

            //*************************************************************************************************************

            //foreach (HtmlNode row1 in docALL1.DocumentNode.SelectNodes("/html/body/div[1]/div[5]/div[1]/section/table/tbody//tr"))
            //{

            //    string number = row1.InnerText;
            //int num = num + 1;

            do
            {
                foreach (HtmlNode row in docALL1.DocumentNode.SelectNodes("/html/body/div[1]/div[5]/div[1]/section/table/tbody/tr[" + num + "]"))
                    if (row != null)
                    {
                        Console.WriteLine(num);
                        HtmlNode NumInTable = row.SelectSingleNode("td[1]/a"); //наименование
                        Console.WriteLine(NumInTable.InnerText);
                        HtmlNode TimeOfBound = row.SelectSingleNode("td[2]/span[2]");//рейтинг
                        Console.WriteLine(TimeOfBound.InnerText);
                        HtmlNode NameOfBound = row.SelectSingleNode("td[3]/span[2]");//прогноз пересмотр
                        Console.WriteLine(NameOfBound.InnerText);
                        //        HtmlNode ID_BoundPase = row.SelectSingleNode("td[3]");
                        //        string h = Convert.ToString(ID_BoundPase.InnerHtml);
                        //        string ID_Bound = h.Substring(18, 12);

                        HtmlNode redemption = row.SelectSingleNode("td[4]/a"); // сектор
                        Console.WriteLine(redemption.InnerText);
                        HtmlNode YearsToRedemption = row.SelectSingleNode("td[5]/span[2]"); //регион
                        Console.WriteLine(YearsToRedemption.InnerText);
                        HtmlNode DoxodnosTbParse = row.SelectSingleNode("td[6]/span[2]");// дата
                        Console.WriteLine(DoxodnosTbParse.InnerText);
                        //        string h1 = Convert.ToString(DoxodnosTbParse.InnerHtml);
                        //        string DoxodnosTb = h1.Trim((new Char[] { '%', }));
                       
                        HtmlNode mhref = row.SelectSingleNode("td[1]/a");// ссылка
                        string INN = mhref.GetAttributeValue("href",null);
                        string resultINN = INN.Split('/')[3];

                        //ACRAParser Parser = new ACRAParser();
                        //string emitentnamber = resultINN;

                        //var emitID = docALL1.DocumentNode.SelectSingleNode("https://www.acra-ratings.ru/ratings/issuers/64");
                        Console.WriteLine(INN);
                        num = num + 1;
                    }
            }
            while (num != 13);
            Console.ReadKey();
            //        HtmlNode GodCupDoxPase = row.SelectSingleNode("td[8]");
            //        string h2 = Convert.ToString(GodCupDoxPase.InnerHtml);
            //        string GodCupDox = h2.Trim((new Char[] { '%', }));


            //        HtmlNode CupDoxPoslParse = row.SelectSingleNode("td[9]");
            //        string h3 = Convert.ToString(CupDoxPoslParse.InnerHtml);
            //        string CupDoxPosl = h3.Trim((new Char[] { '%', }));

            //        HtmlNode Price = row.SelectSingleNode("td[10]");
            //        HtmlNode AmountOfMoney_Million_sourse = row.SelectSingleNode("td[11]");
            //        string ConvertToStringMillion = Convert.ToString(AmountOfMoney_Million_sourse.InnerText);
            //         string AmountOfMoney_Million = ConvertToStringMillion.Trim((new Char[] { ' ','*'}));


            //        HtmlNode CuponPrice = row.SelectSingleNode("td[12]");
            //        HtmlNode FrequencyPerYear = row.SelectSingleNode("td[13]");
            //        HtmlNode NKDrub = row.SelectSingleNode("td[14]");
            //        HtmlNode DurationYears = row.SelectSingleNode("td[15]");
            //        HtmlNode DateOfCoupon = row.SelectSingleNode("td[16]");



            //        string h4 = Convert.ToString(TimeOfBound.InnerHtml);
            //        string TimeOfBoun = h4.Trim((new Char[] { '%', }));
            //        string Date = DateTime.Now.ToString("yyyy.MM.dd");
            //        string BoundTime = Date + " " + TimeOfBoun;

            //        string h5 = Convert.ToString(redemption.InnerText);
            //        string redeptio = h5.Replace("-", "");

            //        string h6 = Convert.ToString(DateOfCoupon.InnerText);
            //        string DateOfCoupo = h6.Replace("-", "");



            // Console.WriteLine(Convert.ToInt32(NumInTable.InnerText));
            //        Console.WriteLine(AmountOfMoney_Million);
            //        Console.WriteLine(CupDoxPosl);
            //        Console.WriteLine();
            //        //OutputBuffer.AddRow();
            //        //OutputBuffer.NumInTable = Convert.ToInt32(NumInTable.InnerText);
            //        //OutputBuffer.TimeOfBound = TimeOfBound.InnerText;
            //        //OutputBuffer.DateOfBound = Date;
            //        //OutputBuffer.DateTimeOfBound = BoundTime;
            //        //OutputBuffer.NameOfBound = NameOfBound.InnerText;
            //        //OutputBuffer.BoundId = ID_Bound;
            //        //OutputBuffer.redemption = redeptio;
            //        //OutputBuffer.YearsToRedemption = YearsToRedemption.InnerText;

            //        //OutputBuffer.DoxodnosTb = DoxodnosTb;
            //        //OutputBuffer.GodCupDox = GodCupDox;
            //        //OutputBuffer.CupDoxPosl = CupDoxPosl;
            //        //OutputBuffer.Price = Price.InnerText;
            //        //OutputBuffer.AmountOfMoneyMillion = AmountOfMoney_Million.InnerText;
            //        //OutputBuffer.CuponPrice = CuponPrice.InnerText;
            //        //OutputBuffer.FrequencyPerYear = FrequencyPerYear.InnerText;
            //        //OutputBuffer.NKDrub = NKDrub.InnerText;
            //        //OutputBuffer.DurationYears = DurationYears.InnerText;
            //        //OutputBuffer.DateOfCoupon = DateOfCoupo;
            //        //OutputBuffer.Offer = Offe;



        }

        //    Console.ReadKey();
        //}

     
     }

}
    

///html/body/div[1]/section[2]/div/div/p/text()