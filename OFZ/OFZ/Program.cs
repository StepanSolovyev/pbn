using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ACRA;
namespace OFZ
{
    class Program
    {
        static void Main(string[] args)
        {
            ACRAParser Parser = new ACRAParser();
            string emitentnamber = "54";
            
            //RusB = featuredArticle.GetAttributeValue("href", null);
            Console.WriteLine(Parser.GetEmitentData(emitentnamber).inn);
            Console.ReadKey();

            //HtmlAgilityPack.HtmlWeb web = new HtmlWeb();

            //HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.acra-ratings.ru/ratings/issuers?order=press_release&page=1&sort=desc");

            //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section[2]/div/div/p/text()");
            //*[@id="search-results"]/div/section/table/tbody/tr[1]/td[2]/span[2]
            //   string text = featuredArticle.

            //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div/div[5]/div/table/tbody//tr");
            //Console.WriteLine(featuredArticle.InnerHtml);
            //Console.ReadKey();

            //foreach (HtmlNode row1 in doc.DocumentNode.SelectNodes("/html/body/div/div[5]/div/table/tbody//tr"))
            //{

            //    string number = row1.InnerText;
            //   int num = int.Parse(number) + 1;


            //    foreach (HtmlNode row in doc.DocumentNode.SelectNodes("/html[1]/ body[1] / div[1] / div[1] / table[1] / tr[" + num + "]"))
            //    {

            //        HtmlNode NumInTable = row.SelectSingleNode("td[1]");
            //        HtmlNode TimeOfBound = row.SelectSingleNode("td[2]");
            //        HtmlNode NameOfBound = row.SelectSingleNode("td[3]");

            //        HtmlNode ID_BoundPase = row.SelectSingleNode("td[3]");
            //        string h = Convert.ToString(ID_BoundPase.InnerHtml);
            //        string ID_Bound = h.Substring(18, 12);

            //        HtmlNode redemption = row.SelectSingleNode("td[4]");
            //        HtmlNode YearsToRedemption = row.SelectSingleNode("td[5]");

            //        HtmlNode DoxodnosTbParse = row.SelectSingleNode("td[6]");
            //        string h1 = Convert.ToString(DoxodnosTbParse.InnerHtml);
            //        string DoxodnosTb = h1.Trim((new Char[] { '%', }));

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



            //        Console.WriteLine(Convert.ToInt32(NumInTable.InnerText));
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



            //    }

            //    Console.ReadKey();
            //}


        }

    }
}

///html/body/div[1]/section[2]/div/div/p/text()