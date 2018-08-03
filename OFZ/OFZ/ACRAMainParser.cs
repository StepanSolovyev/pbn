using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ACRAMain
{
    //public struct emitent
    //{
    //    public int emit;
    //    public string name;//наименование
    //    public string rating;//рейтинг
   //     public string forecast; //прогноз/пересмотр
    //    public string sector; //сектор
   //     public string eregion; //регион
   //     public DateTime data; //дата
                                     //public emitent(int emitid)
                                     //{
                                     //    emit = emitid;
                                     //    name = "";
                                     //    sector = "";
                                     //    eregion = "";
                                     //     inn = 0;

        //}
    }
    //public class ACRAMainParser
    //{

    //}    //HtmlAgilityPack.HtmlWeb webmain = new HtmlWeb();

        //public HtmlAgilityPack.HtmlDocument docmain = webmain.Load("https://www.acra-ratings.ru/ratings/issuers?order=press_release&page=1&sort=desc");

        //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section[2]/div/div/p/text()");
        //*[@id="search-results"]/div/section/table/tbody/tr[1]/td[2]/span[2]
        //   string text = featuredArticle.

        //var featuredArticle = doc.DocumentNode.SelectSingleNode("/html/body/div/div[5]/div/table/tbody//tr");
        //Console.WriteLine(featuredArticle.InnerHtml);
        //Console.ReadKey();
        //}
     // --------------------------  CreateTable----------------------------------
     //   public static void CreateOrderTable()
     //   {
    //        // Create new DataTable.
    //        DataTable orderTable = new DataTable();

    //        // Declare DataColumn and DataRow variables.
     //       DataColumn column;
    //        DataRow row;

            // Create new DataColumn, set DataType, ColumnName
            // and add to DataTable.    
    //        column = new DataColumn();
    //        column.DataType = System.Type.GetType("System.String");
    //        column.ColumnName = "name";
    //        column.ReadOnly = false;
    //        column.Unique = true;

    //        orderTable.Columns.Add(column);

            // Create second column.
    //        column = new DataColumn();
    //        column.DataType = Type.GetType("System.String");
    //        column.ColumnName = "rating";
    //        orderTable.Columns.Add(column);

            // Create 3d column.
    //        column = new DataColumn();
    //        column.DataType = Type.GetType("System.String");
    //        column.ColumnName = "forecast";
     //       orderTable.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    
    //        for (int i = 0; i < 200; i++)
    //        {
    //            row = orderTable.NewRow();
    //            row["name"] = i;
    //            row["rating"] = "rating " + i;
    //            row["forecast"] = "forecast " + i;
    //            orderTable.Rows.Add(row);
    //        }

    //    }
    //    public void InsertOrderDetails(DataTable orderTable)
    //    {
            // Use an Object array to insert all the rows .
            // Values in the array are matched sequentially to the columns, based on the order in which they appear in the table.
    //        Object[] rows = {
    //                             new Object[]{1,"O0001","Mountain Bike",1419.5,36},
    //                             new Object[]{2,"O0001","Road Bike",1233.6,16},
    //                             new Object[]{3,"O0001","Touring Bike",1653.3,32},
    //                             new Object[]{4,"O0002","Mountain Bike",1419.5,24},
    //                             new Object[]{5,"O0002","Road Bike",1233.6,12},
     //                            new Object[]{6,"O0003","Mountain Bike",1419.5,48},
     //                            new Object[]{7,"O0003","Touring Bike",1653.3,8},
     //                        };

    //        foreach (Object[] row in rows)
    //        {
    //            orderTable.Rows.Add(row);
     //       }
//
    //    }


   // }
   // DataTable orderTable = CreateOrderTable();
    //InsertOrders(orderTable);
    //ShowTable(orderTable);
//}




