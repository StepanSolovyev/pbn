using System;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Linq;
using System.Globalization;

namespace RusBounds
{
    /// <summary>  
    ///  Содержит основные данные по облигациям
    /// </summary> 
    public struct BondRelease
    {
        public string MarketSector;  // сектор рынка
        public string BondName;      // облигация, выпуск
        public string ReleaseStatus; // состояние выпуска
        public string RegistrationNumber; // номер регистрации
        public DateTime? StartOfPlacement;  // начало размещения, тип?
        public DateTime? MaturityDate; // дата погашения
        public ulong  INN;
        //public ulong Nominal;
        //public string NominalCurrency;
        
        public string BondNameFull; // полное имя облигации, выпуска
        public string StateRegistrationData; // данные гос регистрации
        public string ISIN; //в ТЗ тип ulong !!!
        public ulong EmissionVolume; // в шт
        public ulong  VolumeOfIssue;
        public string IssueCurrency;
        public ulong VolumeOutstandingCount; // Объем в обращении шт 
        public ulong VolumeOutstanding;
        public string VolumeOutstandingCurrency;
        public ulong PeriodOfTreatmentDays; // Период обращения. дней 
        public ulong DaysToMaturity; // Дней до погашения
        public DateTime? DateOfTheNearestOffer; // Дата ближайшей оферты
        public ulong FrequencyOfPaymentsPerYear; // Периодичность выплат в год 
        public DateTime? CouponPaymentDate; // Дата выплаты купона
        public float CouponPerAnnum; // Размер % купона годовых 
        public float NKD; // НКД
        public string NKDCurrency;
        // debug field
        public int bond;
    
        /// <summary>  
        ///  Конструктор эмитента заполняет все поля пустыми значениями.
        /// </summary> 
        public BondRelease(int bondid)
        {
            bond = bondid;
            MarketSector = "";
            BondName = "";
            ReleaseStatus = "";
            RegistrationNumber = "";
            StartOfPlacement = null;
            MaturityDate = null;
            //Nominal = 0;
            //NominalCurrency = "";

            BondNameFull = "";
            StateRegistrationData = "";
            ISIN = "";
            EmissionVolume = 0;
            VolumeOfIssue = 0;
            IssueCurrency = "";
            VolumeOutstandingCount = 0;
            VolumeOutstanding = 0;
            VolumeOutstandingCurrency = "";
            PeriodOfTreatmentDays = 0;
            DaysToMaturity = 0;
            DateOfTheNearestOffer = null;
            FrequencyOfPaymentsPerYear = 0;
            CouponPaymentDate = null;
            CouponPerAnnum = 0.0F;
            NKD =  0.0F;
            NKDCurrency = "";
            INN = 0;



            
        }

        public void SetBondDataByHref(string href)
        {
            //СДЕЛАТЬ: ЗАПОЛНЕНИЕ ПОЛЕЙ ОБЛИГАЦИИ
            try {
                    // получает веб страницу с гридом для парсинга
                    HtmlWeb CurrentHTMLPage = new HtmlWeb
                    {
                        OverrideEncoding = Encoding.GetEncoding("Windows-1251")
                    };                
                    HtmlAgilityPack.HtmlDocument CurrentHTMLPageAsDoc = CurrentHTMLPage.Load("http://rusbonds.ru"+href.Replace("\"",""));
                    
                    HtmlNodeCollection tempNodeSet = CurrentHTMLPageAsDoc.DocumentNode.SelectNodes("//html/body/table[4]/tr/td[4]/table[6]//tr");
                    // парсим таблицу с данными по выпуску облигаций построчно
                    // test string
                   foreach(HtmlNode node in tempNodeSet)
                            {
                                string[] SwchValueKeyPair = node.InnerText.Split(':');
                                if(SwchValueKeyPair.Count() > 2 || SwchValueKeyPair.Count() < 2 )continue;
                                string swch = SwchValueKeyPair[0];
                                string value = SwchValueKeyPair[1];
                                
                                switch (swch)
                                {
                                    // table 7 switch


                                    case "Наименование":
                                    this.BondNameFull = value;
                                    break;

                        // already exist at table 6
                        //case "Состояние выпуска":
                        //this.ReleaseStatus = value; break;

                                    case "ISIN код":
                                    this.ISIN = value;
                                    break;

                                    case "Данные госрегистрации":
                                    this.StateRegistrationData = value;
                                    break;

                                    // already exist at table 6
                                    //case "Номинал":
                                    //this.Nominal = ulong.Parse(value); break;
                                    
                                    case "Объем эмиссии, шт.":
                                    this.EmissionVolume = ulong.Parse(value.Replace(" ",String.Empty)); break;

                                    case "Объем эмиссии":
                                    string[] SplittedArray = value.Split("&nbsp;");
                                    this.VolumeOfIssue = ulong.Parse(SplittedArray[0].Replace(" ",String.Empty));
                                    this.IssueCurrency = SplittedArray[1]; break;

                                    case "Объем в обращении, шт":
                                    this.VolumeOutstanding = ulong.Parse(value.Replace(" ", String.Empty)); break;

                                    case "Объем в обращении":
                                    string[] VolumeOutstandingSplittedArray = value.Split("&nbsp;");
                                    this.VolumeOutstandingCount = ulong.Parse(VolumeOutstandingSplittedArray[0].Replace(" ",String.Empty));
                                    this.VolumeOutstandingCurrency = VolumeOutstandingSplittedArray[1]; break;

                                    case "Период обращения, дней":
                                    this.PeriodOfTreatmentDays = ulong.Parse(value); break;

                                    case "Дней до погашения":
                                    this.DaysToMaturity = ulong.Parse(value); break;

                                    case "Дата ближайшей оферты":
                                    try
                                    {
                                    this.DateOfTheNearestOffer = DateTime.Parse(value); 
                                    }
                                    catch { this.DateOfTheNearestOffer = null; }
                                    break;

                                    case "Периодичность выплат в год":
                                    this.FrequencyOfPaymentsPerYear = ulong.Parse(value);
                                    break;

                                    case "Дата выплаты купона":
                                    this.CouponPaymentDate = DateTime.Parse(value);
                                    break;

                                    case "Размер купона, % годовых":
                                    try
                                    {
                                        System.Globalization.CultureInfo[] providers = { new System.Globalization.CultureInfo("en-US"), new System.Globalization.CultureInfo("ru-RU") };
                                        this.CouponPerAnnum = float.Parse(value.Replace(" ", String.Empty), providers[1]); 
                                    }
                                    catch { }
                                    break;

                                    case "НКД":
                                    string[] NKDSplittedArray = value.Split("&nbsp;");
                                    try
                                    {   string NKDSTRING = NKDSplittedArray[0].Replace(" ", String.Empty);
                                        System.Globalization.CultureInfo[] providers = { new System.Globalization.CultureInfo("en-US"), new System.Globalization.CultureInfo("ru-RU") };
                                        this.NKD = float.Parse(NKDSTRING, providers[1]);
                                    }
                                    catch
                                    {}
                                    this.NKDCurrency = NKDSplittedArray[1]; break;

                                    


                                        default:
                                        break;
                                }
                            }
                //navigate to nex table 7 (emitent page)
                this.INN = this.GetEmitentINNByHref(CurrentHTMLPageAsDoc.DocumentNode.SelectSingleNode("//html/body/table[4]/tr/td[4]/table[3]/tr/td[2]/a").GetAttributeValue("href",""));
                    
                }
            catch {}
            


            
        }
        public ulong GetEmitentINNByHref(string href)
        {
            try
            {
                // получает веб страницу для парсинга
                HtmlWeb CurrentHTMLPage = new HtmlWeb
                {
                    OverrideEncoding = Encoding.GetEncoding("Windows-1251")
                };
                HtmlAgilityPack.HtmlDocument CurrentHTMLPageAsDoc = CurrentHTMLPage.Load("http://rusbonds.ru" + href.Replace("\"", ""));

                HtmlNodeCollection tempNodesCollection = CurrentHTMLPageAsDoc.DocumentNode.SelectNodes("//html/body/table[4]/tr/td[4]/table[7]//tr"); 
                ulong TempINN;
                foreach (HtmlNode node in tempNodesCollection)
                {
                    string swch = node.InnerText.Split(':')[0];
                    switch (swch)
                    {
                        case "ИНН":
                            TempINN = ulong.Parse(node.InnerText.Split(':')[1]);
                            return TempINN;
                        default:
                            break;
                    }
                 
                }
                return 0;
            }
            catch { return 0; }
        }
    }

    /// <summary>  
    ///  Этот класс содержит основные функции   
    /// </summary> 
    public class RBParser
    {
        public RBParser()
        {

            // for ubuntu support

            //System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
            /// <summary>  
            /// Получение номера последней страницы с облигациями
            /// </summary>
            /// <param name="URL">URL на страницу с гридом</param>
            public int GetPageCounter(string URL)
        {
            //счетчик тегов /a[i] с интервалами
            int i = 0;

            //счетчик тегов /a[j] с отдельными значениями страниц в интервале
            int j = 0;
            //последнее значение страницы в последнем интервале вида "210...219"
            int MAX = 0;

            //XPath нода с последним интервалом страниц
            string IPath = "";

            //номер последней страницы
            int pageN = 0;
            bool test1;

            //значение интервала страниц
            String allpage = "";

            HtmlAgilityPack.HtmlWeb web2 = new HtmlWeb
            {
                OverrideEncoding = Encoding.GetEncoding("Windows-1251")
            };

            HtmlAgilityPack.HtmlDocument doc2 = web2.Load(URL);
            try
            {
                do
                {
                    i++;
                    HtmlNode page = doc2.DocumentNode.SelectSingleNode("//html/body/div[1]/table[1]/tr/td/table/tr/td[2]/a[" + i + "]");
                    allpage = page.InnerHtml;
                    try
                    {
                        test1 = allpage.Substring(2, 3).Equals("..."); // This is true.  
                        MAX = int.Parse(allpage.Substring(5, 2));
                    }
                    catch
                    {
                        test1 = allpage.Substring(3, 3).Equals("..."); // This is true.  
                        MAX = int.Parse(allpage.Substring(6, 3));
                    }
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
                    //
                    pageN = int.Parse(pageEnd);
                }
                while (j <= MAX);
            }
            catch { }
            //Console.WriteLine("pageN: ");
            Console.WriteLine(pageN);

            return (pageN);//номер последней страницы (int)
        }

        /// <summary>  
        /// Получение числа строк в таблице на странице
        /// </summary>
        /// <param name="URL">URL на странице которого находится грид</param>
        public int GetRowCounter(string URL)
        {
            HtmlAgilityPack.HtmlWeb webLine = new HtmlWeb();
            webLine.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
            HtmlAgilityPack.HtmlDocument docLine = webLine.Load(URL);
            int nline = 0;
            try
            {
               HtmlNodeCollection testc = docLine.DocumentNode.SelectNodes("//table[@class='tbl_data tbl_headgrid']//tbody//tr"); // /html[1]/body[1]/div[1]/table[4]/tr[1] - это таблица со страницами
               nline = testc.Count;
            }
            catch  
            { // nothing to do just return value
            }
            Console.WriteLine("nline: "+nline );
            return (nline);
        }

        /// <summary>  
        ///  Главный метод класса. Возвращает массив структур с заполненными данными по каждой облигации в нём.
        ///  <returns>Возвращает массив emitent[].</returns>
        /// </summary>
        public BondRelease[] Start()
        {
#if DEBUG
           //BondRelease Test = new BondRelease();
            //Test.GetEmitentINNByHref("/ank_org.asp?emit=89952");
#endif
            // определяет кол-во страниц в мультистраничном гриде веб страницы
            int PageCounter = this.GetPageCounter("http://www.rusbonds.ru/srch_simple.asp?go=0&ex=0&s=1&d=1&p=0");

            // определяет кол-во строк на каждой странице грида
            int RowCounter = this.GetRowCounter("http://www.rusbonds.ru/srch_simple.asp?go=0&ex=0&s=1&d=1&p=0");

            //определение масива структур , его размера (общее количесто эмитентов)
            BondRelease[] EmitentArrayToReturn = new BondRelease[RowCounter * PageCounter];

            // определяем начальные значения для дальнейшей итерации по многостраничному гриду и массиву эимтентов
            int CurrentPageIndex = 1;
            int CurrentRowIndex = 1;
            int ArrayCurrentElementIndex = 0; // будущий итератор для EmitentArrayToReturn

#if DEBUG
            CurrentPageIndex = 1;
#endif
            //выгрузка данных из таблицы постранично и построчно из каждой страницы грида
            do
            {
                try
                {
                    HtmlWeb CurrentHTMLPage = new HtmlWeb
                    {
                        OverrideEncoding = Encoding.GetEncoding("Windows-1251")
                    };

                    // получает веб страницу с гридом для парсинга
                    HtmlAgilityPack.HtmlDocument CurrentHTMLPageAsDoc = CurrentHTMLPage.Load("http://www.rusbonds.ru/srch_simple.asp?go=0&ex=0&s=1&d=1&p=" +
                        CurrentPageIndex + "#rslt");

                    do
                    {
                        foreach (HtmlNode row in CurrentHTMLPageAsDoc.DocumentNode.SelectNodes("//table[@class='tbl_data tbl_headgrid']//tbody//tr"))  // td[" + CurrentRowIndex + "]"
                            if (row != null)
                            {
#if DEBUG
                                Console.WriteLine(ArrayCurrentElementIndex + "\t" + Math.Round(((double)ArrayCurrentElementIndex / ((double)RowCounter * (double)PageCounter)) * 100, 2) +
                                    "%\tof " + RowCounter * PageCounter);
#endif
                                HtmlNode CurrentMarketSector = row.SelectSingleNode("td[1]");
                                EmitentArrayToReturn[ArrayCurrentElementIndex].MarketSector = CurrentMarketSector.InnerHtml;
                                //получает из строки грида ссылку на эмитент вида "/ank_obl.asp?tool=27929"
                                HtmlNode CurrentBondName = row.SelectSingleNode("td[2]/a");
                                EmitentArrayToReturn[ArrayCurrentElementIndex].BondName = CurrentBondName.InnerHtml;

                                HtmlNode mhref = row.SelectSingleNode("td[2]");
                                string bondhref = mhref.InnerHtml.Split("\"")[1];

                                EmitentArrayToReturn[ArrayCurrentElementIndex].SetBondDataByHref(bondhref);

                                HtmlNode CurrentReleaseStatus = row.SelectSingleNode("td[3]");
                                EmitentArrayToReturn[ArrayCurrentElementIndex].ReleaseStatus = CurrentReleaseStatus.InnerHtml;

                                HtmlNode CurrentRegistrationNumber = row.SelectSingleNode("td[4]");
                                EmitentArrayToReturn[ArrayCurrentElementIndex].RegistrationNumber = CurrentRegistrationNumber.InnerHtml;

                                HtmlNode CurrentStartOfPlacement = row.SelectSingleNode("td[5]");
                                if (CurrentStartOfPlacement.InnerText == "&nbsp;")
                                { EmitentArrayToReturn[ArrayCurrentElementIndex].StartOfPlacement = null; }
                                else
                                {
                                    EmitentArrayToReturn[ArrayCurrentElementIndex].StartOfPlacement = DateTime.Parse(CurrentStartOfPlacement.InnerHtml);
                                }

                                HtmlNode CurrentMaturityDate = row.SelectSingleNode("td[6]");
                                if (CurrentMaturityDate.InnerText == "&nbsp;")
                                {
                                    EmitentArrayToReturn[ArrayCurrentElementIndex].MaturityDate = null;
                                }
                                else
                                {

                                    EmitentArrayToReturn[ArrayCurrentElementIndex].MaturityDate = DateTime.Parse(CurrentMaturityDate.InnerHtml);
                                }
                                CurrentRowIndex++;

                                ArrayCurrentElementIndex++;
                            }
                    }
                    //проверка конца массива строк на странице - условие перехода на новую страницу
                    while (CurrentRowIndex <= RowCounter);
                    CurrentPageIndex++;

                    //сброс счетчика строк грида при переходе на новую страницу
                    CurrentRowIndex = 1;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message + ex.TargetSite);
#endif
                    break;
                }
            }
            while (CurrentPageIndex != 20000);//заведомо большое число страниц

#if DEBUG
            Console.WriteLine("Total Emitent count is " + EmitentArrayToReturn.Count());
#endif
            #region RemoveEmptyDataFromArray
            // зачищаем массив перед его возвратом
            // критерий - присутствие 2х полей - имя эимтента не пустое и
           // EmitentArrayToReturn = EmitentArrayToReturn.Where(CurrentEmitent => !string.IsNullOrEmpty(CurrentEmitent.Issuer)).ToArray();
            #endregion
#if DEBUG
            Console.WriteLine("Purged Emitent count is " + EmitentArrayToReturn.Count());
#endif
            return (EmitentArrayToReturn);
        }
    }
}


