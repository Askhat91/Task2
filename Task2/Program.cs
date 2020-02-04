using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DataAccess;
using Task2.Models;
using Task2.Models.Output;

namespace Task2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Сделать миграцию БД?  y/n");


            string command =  Console.ReadLine();

            if(command == "y")
            {
                AutomigrateData();
                Console.WriteLine("Данные автомиграции записаны в Базу данных!");
                
            }
            else
            {
                var report = GetReport();

                if (report.Count() > 1)
                {
                    var headerRowSet = $"Дата || Наименование аптеки || Категория товара || Наименование производителя || Сумма продаж";
                    Console.WriteLine(headerRowSet);

                    foreach (var rept in report)
                    {
                        var rowset = $"{rept.Date} || {rept.PharmName} || {rept.CategoryName} || {rept.ProducerName} || {rept.Sum}";
                        Console.WriteLine(rowset);
                    }
                }
            }

           

            Console.ReadKey();

        }


        public static List<Report> GetReport()
        {
            try
            {
                using (var db = new MladexContext())
                {
                    var dbResult = (from goods in db.Goods
                                    join goodsCategories in db.GoodsCategories on goods.CategoryId equals goodsCategories.Id
                                    join producers in db.Producers on goods.ProducerId equals producers.Id
                                    join sales in db.Sales on goods.Id equals sales.GoodId
                                    join pharms in db.Pharms on sales.PharmId equals pharms.Id
                                    select new Report
                                    {
                                        Date = sales.Date_Id,
                                        CategoryName = goodsCategories.Name,
                                        PharmName = pharms.Name,
                                        ProducerName = producers.Name,
                                        Sum = sales.Quantity * sales.Price

                                    }
                                   ).Where(x => x.Sum > 100000).OrderByDescending(x => x.Sum).ToList();

                    return dbResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        ///  DbAutomigration
        /// </summary>
        public static void AutomigrateData()
        {
            using (var db = new MladexContext())
            {

                var producers = new List<Producers>
                {
                    new Producers{ Id = 1, Name = "Berlin Hemi"},
                    new Producers{ Id = 2, Name = "Stada Pharm"},
                    new Producers{ Id = 3, Name = "Merck & Co"},
                    new Producers{ Id = 4, Name = "Pfizer"},
                    new Producers{ Id = 5, Name = "Sanofi"},
                };

                var pharms = new List<Pharms>
                {
                    new Pharms { Id = 1, Name = "Аптека 1"},
                    new Pharms { Id = 2, Name = "Аптека 2"},
                    new Pharms { Id = 3, Name = "Аптека 3"},
                    new Pharms { Id = 4, Name = "Аптека 4"},
                    new Pharms { Id = 5, Name = "Аптека 5"},
                    new Pharms { Id = 6, Name = "Аптека 6"}
                };


                var goodsCategory = new List<GoodsCategory>
                {
                    new GoodsCategory{Id = 1, Name = "Таблетки"},
                    new GoodsCategory{Id = 2, Name = "Капли"},
                    new GoodsCategory{Id = 3, Name = "Маски"},
                    new GoodsCategory{Id = 4, Name = "Жидкости"},
                    new GoodsCategory{Id = 5, Name = "Прочее"}
                };

                var goods = new List<Goods>
                {
                    new Goods{Id = 1, Name = "Аспирин", CategoryId = 1, ProducerId = 1},
                    new Goods{Id = 2, Name = "Називин", CategoryId = 1, ProducerId = 2},
                    new Goods{Id = 3, Name = "Бромгексин", CategoryId = 1, ProducerId = 1},
                    new Goods{Id = 4, Name = "Парацетомол", CategoryId = 1, ProducerId = 1},
                    new Goods{Id = 5, Name = "Шприц", CategoryId = 1, ProducerId = 3},
                    new Goods{Id = 6, Name = "Йод", CategoryId = 1, ProducerId = 3},
                    new Goods{Id = 7, Name = "Сприт этиловый", CategoryId = 1, ProducerId = 1},
                    new Goods{Id = 8, Name = "Маска", CategoryId = 1, ProducerId = 4},
                    new Goods{Id = 9, Name = "Платырь", CategoryId = 1, ProducerId = 4},
                    new Goods{Id = 10, Name = "Кардиомагнил", CategoryId = 1, ProducerId = 1},
                    new Goods{Id = 11, Name = "Мукалтин", CategoryId = 1, ProducerId = 5}
                };

                // Заполняем таблицу Sales
                var sales = new List<Sales>
                {
                    new Sales{Id = 1,GoodId =1,Date_Id= 20200131,PharmId=1,Quantity=100,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20190923,PharmId=1,Quantity=101,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20200203,PharmId=1,Quantity=178,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20190924,PharmId=1,Quantity=93,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20190925,PharmId=1,Quantity=85,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20190928,PharmId=1,Quantity=64,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20190913,PharmId=1,Quantity=21,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20190912,PharmId=1,Quantity=157,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190911,PharmId=1,Quantity=185,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20190908,PharmId=1,Quantity=111,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190903,PharmId=1,Quantity=190,Price=3211.1m},

                     new Sales{Id = 1,GoodId =1,Date_Id= 20190204,PharmId=2,Quantity=112,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20200131,PharmId=2,Quantity=323,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20190204,PharmId=2,Quantity=565,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20200131,PharmId=2,Quantity=176,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20190204,PharmId=2,Quantity=134,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20200131,PharmId=2,Quantity=198,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20190204,PharmId=2,Quantity=132,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20200131,PharmId=2,Quantity=123,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190204,PharmId=2,Quantity=176,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20200131,PharmId=2,Quantity=190,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190204,PharmId=2,Quantity=112,Price=3211.1m},

                     new Sales{Id = 1,GoodId =1,Date_Id= 20190204,PharmId=3,Quantity=234,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20190204,PharmId=3,Quantity=263,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20190204,PharmId=3,Quantity=765,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20200131,PharmId=3,Quantity=131,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20190204,PharmId=3,Quantity=154,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20200131,PharmId=3,Quantity=176,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20190204,PharmId=3,Quantity=113,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20200131,PharmId=3,Quantity=567,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190204,PharmId=3,Quantity=785,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20200131,PharmId=3,Quantity=890,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190204,PharmId=3,Quantity=901,Price=3211.1m},

                     new Sales{Id = 1,GoodId =1,Date_Id= 20190204,PharmId=4,Quantity=890,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20200131,PharmId=4,Quantity=532,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20190204,PharmId=4,Quantity=126,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20200131,PharmId=4,Quantity=128,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20190204,PharmId=4,Quantity=376,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20200131,PharmId=4,Quantity=453,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20190204,PharmId=4,Quantity=765,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20200131,PharmId=4,Quantity=743,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190204,PharmId=4,Quantity=234,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20200131,PharmId=4,Quantity=432,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190204,PharmId=4,Quantity=654,Price=3211.1m},

                     new Sales{Id = 1,GoodId =1,Date_Id= 20200131,PharmId=4,Quantity=443,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20190204,PharmId=4,Quantity=543,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20200131,PharmId=4,Quantity=852,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20190204,PharmId=4,Quantity=652,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20200131,PharmId=4,Quantity=525,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20190204,PharmId=4,Quantity=631,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20200131,PharmId=4,Quantity=954,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20190221,PharmId=4,Quantity=643,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190217,PharmId=4,Quantity=854,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20200131,PharmId=4,Quantity=124,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190225,PharmId=4,Quantity=854,Price=3211.1m},

                     new Sales{Id = 1,GoodId =1,Date_Id= 20190212,PharmId=5,Quantity=126,Price=100000},
                    new Sales{Id = 2,GoodId =2,Date_Id= 20200131,PharmId=5,Quantity=842,Price=14000},
                    new Sales{Id = 3,GoodId =3,Date_Id= 20190207,PharmId=5,Quantity=642,Price=20000},
                    new Sales{Id = 4,GoodId =4,Date_Id= 20200131,PharmId=5,Quantity=743,Price=15324.21m},
                    new Sales{Id = 5,GoodId =5,Date_Id= 20190201,PharmId=5,Quantity=10,Price=313123.23m},
                    new Sales{Id = 6,GoodId =6,Date_Id= 20200131,PharmId=5,Quantity=45,Price=100000},
                    new Sales{Id = 7,GoodId =7,Date_Id= 20190207,PharmId=5,Quantity=854,Price=131231.02m},
                    new Sales{Id = 8,GoodId =8,Date_Id= 20200131,PharmId=5,Quantity=113,Price=11221},
                    new Sales{Id = 9,GoodId =9,Date_Id= 20190205,PharmId=5,Quantity=56,Price=15000},
                    new Sales{Id = 10,GoodId =10,Date_Id= 20200131,PharmId=5,Quantity=76,Price=213141},
                    new Sales{Id = 11,GoodId =11,Date_Id= 20190204,PharmId=5,Quantity=73,Price=3211.1m},
                };


                db.Producers.AddRange(producers);
                db.Goods.AddRange(goods);
                db.GoodsCategories.AddRange(goodsCategory);
                db.Pharms.AddRange(pharms);
                db.Sales.AddRange(sales);
                db.SaveChanges();
            }
        }
       
    }
}