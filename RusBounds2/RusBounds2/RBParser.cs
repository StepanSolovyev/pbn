﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Linq;

namespace RusBounds
{
    /// <summary>  
    ///  Содержит основные данные по эмитенту
    /// </summary> 
    public struct emitent
    {
        public string Issuer;
        public string TypeOfBusiness;
        public string Country;
        public string Region;
        public ulong  INN;
        public string OKPO;
        public string GosRegData;
        public string LowAddress;
        public string MailingAddress;
        public string TypeOfProperty;
        public ulong  CharterCapital;
        public string CharterCapitalCurrency;

        // debug field
        public int    emit;

        /// <summary>  
        ///  Конструктор эмитента заполняет все поля пустыми значениями.
        /// </summary> 
        public emitent(int emitid)
        {
            CharterCapital = 0;
            Country         = "";
            CharterCapitalCurrency = "";
            emit            =  emitid;
            INN             = 0;
            LowAddress     = "";
            Issuer            = "";
            OKPO            = "";
            TypeOfBusiness           = "";
            MailingAddress      = "";
            TypeOfProperty    = "";
            GosRegData         = "";
            Region          = "";
        }
    }

    /// <summary>  
    ///  Этот класс содержит основные функции   
    /// </summary> 
    public class RBParser
    {
        /// <summary>  
        /// Получение общего числа эмитентов
        /// </summary>
        /// <param name="URL">URL на страницу с гридом</param>
        int GetPageCounter(string URL) 
        {
            //счетчик тегов /a[i] с интервалами
            int i = 0;

            //счетчик тегов /a[i] с отдельными значениями страниц
            int j = 0;
            //последнее значение страницы в последнем интервале страниц
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
        /// <summary>  
        /// Получение числа строк в таблице на странице
        /// </summary>
        /// <param name="URL">URL на странице которого находится грид</param>
        int GetRowCounter(string URL)
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
        /// <summary>  
        ///  Получение данных об эмитенте
        /// </summary>
        /// <param name="emithref">Относительный URL на странице которого находится грид с анкетой компании</param>
        emitent GetEmitentData(string emithref)
        {
            emitent Emit = new emitent(int.Parse(emithref.Split('=')[1]));
            string FullEmitentUrl = "http://www.rusbonds.ru" + emithref;
            try
            {
                HtmlAgilityPack.HtmlWeb web1 = new HtmlWeb();
                web1.OverrideEncoding = Encoding.GetEncoding("Windows-1251");
                HtmlAgilityPack.HtmlDocument doc1 = web1.Load(FullEmitentUrl);
                //Emit.emit = int.Parse(idemitent);

                HtmlNodeCollection EmitentInfo = doc1.DocumentNode.SelectNodes("/html//body/table[4]//tbody/tr");
                foreach(HtmlNode node in EmitentInfo)
                {
                    string swch = node.InnerText.Split(':')[0];
                    switch (swch)
                    {
                        case "Наименование":
                            Emit.Issuer = node.InnerText.Split(':')[1];
                            break;
                        case "Основной ОКВЭД":
                            Emit.TypeOfBusiness = node.InnerText.Split(':')[1];
                            break;
                        case "Страна":
                            Emit.Country = node.InnerText.Split(':')[1];
                            break;

                        case "Регион":
                            Emit.Region = node.InnerText.Split(':')[1];
                            break;

                        case "ИНН":
                            Emit.INN = ulong.Parse(node.InnerText.Split(':')[1]);
                            break;

                        case "ОКПО или др.":
                            Emit.OKPO = node.InnerText.Split(':')[1];
                            break;

                        case "Данные госрегистрации":
                            Emit.GosRegData = node.InnerText.Split(':')[1];
                            break;

                        case "Юридический адрес":
                            Emit.LowAddress = node.InnerText.Split(':')[1];
                            break;

                        case "Почтовый адрес":
                            Emit.MailingAddress = node.InnerText.Split(':')[1];
                            break;
                        case "Вид собственности":
                            Emit.TypeOfProperty = node.InnerText.Split(':')[1];
                            break;

                        case "Уставный капитал":
                            string[] U = this.GetCapital(node.InnerText.Split(':')[1]); //вытягиваем число с пробелами из уставного капитала до букв валюты   
                            Emit.CharterCapital = ulong.Parse(U[0].Replace(" ", String.Empty)); // удаляем пробелы из числа

                            string[] V = this.Сurrency(node.InnerText.Split(':')[1]); //вытягиваем валюту из уставного капитала
                            Emit.CharterCapitalCurrency = (V[V.Length - 1].Replace(" ", String.Empty)); // удаляем пробелы из числа
                            break;

                        default:
                            break;
                    }                 
                }                
                
                return (Emit);
            }
            catch(Exception e)
                {
#if DEBUG
                Console.WriteLine(e.Message + " " + FullEmitentUrl);
#endif
                // on error just return data "as is"
                return (Emit);                
                }
            }
        /// <summary>  
        ///  Получение числа уставного капитала до валюты со сплитом строки 
        /// </summary>
        /// <param name="Val">Исходная строка в формате "10 100 000 000 RUR"</param>
        string[] GetCapital(string Val) 
        {
            string pattern = "[A-Z]+";
            string[] result = Regex.Split(Val, pattern,
                                          RegexOptions.IgnoreCase);
            return (result);
        }

        /// <summary>  
        ///  Получение типа валюты уставного капитала
        /// </summary>
        /// <param name="Val1">Исходная строка в формате "10 100 000 000 RUR"</param>
        string[] Сurrency(string Val1)
        {
            string pattern = "[0-9]";
            //string input = "Abc1234Def5678Ghi9012Jklm";
           string[] result1 = Regex.Split(Val1, pattern,
                                          RegexOptions.IgnoreCase);
            
            return (result1);
        }

        /// <summary>  
        ///  Главный метод класса. Возвращает массив структур с заполненными данными по каждому эмитенту в нём.
        ///  <returns>Возвращает массив emitent[].</returns>
        /// </summary>
        public emitent[] Start()
    {
            // определяет кол-во страниц в мультистраничном гриде веб страницы
            int PageCounter = this.GetPageCounter("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt1#rslt");

            // определяет кол-во строк на каждой странице грида
            int RowCounter = this.GetRowCounter("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=1#rslt");

            //определение масива структур , его размера (общее количесто эмитентов)
            emitent[] EmitentArrayToReturn = new emitent[RowCounter * PageCounter];

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
                    HtmlAgilityPack.HtmlDocument CurrentHTMLPageAsDoc = CurrentHTMLPage.Load("http://www.rusbonds.ru/srch_emitent.asp?emit=0&cat=0&rg=0&rate=0&stat=0&go=0&s=5&d=0&p=" + 
                        CurrentPageIndex + "#rslt");
                       
                    do
                    {
                        foreach (HtmlNode row in CurrentHTMLPageAsDoc.DocumentNode.SelectNodes("/html/body/div[1]/table[2]/tbody/tr[" + CurrentRowIndex + "]"))
                            if (row != null)
                            {
#if DEBUG
                                Console.WriteLine(ArrayCurrentElementIndex+"\t"+ Math.Round(((double)ArrayCurrentElementIndex/((double)RowCounter * (double)PageCounter))*100, 2) + 
                                    "%\tof "+ RowCounter * PageCounter);
#endif
                                //получает из строки грида ссылку на эмитент вида "http://www.rusbonds.ru/ank_org.asp?emit=78881"
                                HtmlNode mhref = row.SelectSingleNode("td[2]/a");
                                string emithref = mhref.GetAttributeValue("href", null);
                                EmitentArrayToReturn[ArrayCurrentElementIndex] = this.GetEmitentData(emithref);

                                CurrentRowIndex++;
                            }
                        ArrayCurrentElementIndex++;
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
            while (CurrentPageIndex != 300);//заведомо большое число страниц

#if DEBUG
            Console.WriteLine("Total Emitent count is " + EmitentArrayToReturn.Count());
#endif
            #region RemoveEmptyDataFromArray
            // зачищаем массив перед его возвратом
            // критерий - присутствие 2х полей - имя эимтента не пустое и
            EmitentArrayToReturn = EmitentArrayToReturn.Where(CurrentEmitent => !string.IsNullOrEmpty(CurrentEmitent.Issuer)).ToArray();
            #endregion
#if DEBUG
            Console.WriteLine("Purged Emitent count is " + EmitentArrayToReturn.Count());
#endif
            return (EmitentArrayToReturn);
         }
    }
    
}
