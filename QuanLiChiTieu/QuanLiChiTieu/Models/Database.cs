using System;
using System.Collections.Generic;
using System.Data.Common;
using QuanLiChiTieu.Interface;
using SQLite;
using DependencyService = Xamarin.Forms.DependencyService;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuanLiChiTieu.Models
{
    public class Database
    {
        private readonly SQLiteConnection _connection;

        public Database()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            CreateTablle();
            CreateForm();
            CreateCategory();
        }

        private void CreateTablle()
        {
            _connection.CreateTable<Money>();
            _connection.CreateTable<Form>();
            _connection.CreateTable<Category>();
        }

        private void CreateForm()
        {
            Form form1 = new Form() { FormID = 1, FormName = "Thu" };
            Form form2 = new Form() { FormID = 2, FormName = "Chi" };
            _connection.Insert(form1);
            _connection.Insert(form2);
        }

        private void CreateCategory()
        {
            Category category1 = new Category(){CategoryID = 1,CategoryName = "Tiền Lương",Form = 1};
            Category category2 = new Category() { CategoryID = 2, CategoryName = "Tiền Thưởng", Form = 1 };
            Category category3 = new Category() { CategoryID = 3, CategoryName = "Trợ Cấp", Form = 1 };
            Category category4 = new Category() { CategoryID = 4, CategoryName = "Quà tặng", Form = 1 };
            Category category5 = new Category() { CategoryID = 5, CategoryName = "Làm Thêm", Form = 1 };
            Category category6 = new Category() { CategoryID = 6, CategoryName = "Kinh Doanh", Form = 1 };
            Category category7 = new Category() { CategoryID = 7, CategoryName = "Bất Động Sản", Form = 1 };
            Category category8 = new Category() { CategoryID = 8, CategoryName = "Cổ Phiếu", Form = 1 };
            Category category9 = new Category() { CategoryID = 9, CategoryName = "Nợ Trả", Form = 1 };
            Category category10 = new Category() { CategoryID = 10, CategoryName = "Lãi Suất", Form = 1 };
            Category category11 = new Category() { CategoryID = 11, CategoryName = "Vay Nợ", Form = 1 };
            Category category12 = new Category() { CategoryID = 12, CategoryName = "Các Khoản Khác", Form = 1 };

            Category category13 = new Category() { CategoryID = 13, CategoryName = "Ăn Uống, Nội Trợ", Form = 2 };
            Category category14 = new Category() { CategoryID = 14, CategoryName = "Quà Tặng", Form = 2 };
            Category category15 = new Category() { CategoryID = 15, CategoryName = "Tiệc Tùng", Form = 2 };
            Category category16 = new Category() { CategoryID = 16, CategoryName = "Giải Trí", Form = 2 };
            Category category17 = new Category() { CategoryID = 17, CategoryName = "Y Tế", Form = 2 };
            Category category18 = new Category() { CategoryID = 18, CategoryName = "Du Lịch", Form = 2 };
            Category category19 = new Category() { CategoryID = 19, CategoryName = "Mua Sắm", Form = 2 };
            Category category20 = new Category() { CategoryID = 20, CategoryName = "Điện Thoại", Form = 2 };
            Category category21 = new Category() { CategoryID = 21, CategoryName = "Phương Tiện Di Chuyển", Form = 2 };
            Category category22 = new Category() { CategoryID = 22, CategoryName = "Gia Dụng", Form = 2 };
            Category category23 = new Category() { CategoryID = 23, CategoryName = "Các Khoản Khác", Form = 2 };

            _connection.Insert(category1);
            _connection.Insert(category2);
            _connection.Insert(category3);
            _connection.Insert(category4);
            _connection.Insert(category5);
            _connection.Insert(category6);
            _connection.Insert(category7);
            _connection.Insert(category8);
            _connection.Insert(category9);
            _connection.Insert(category10);
            _connection.Insert(category11);
            _connection.Insert(category12);
            _connection.Insert(category13);
            _connection.Insert(category14);
            _connection.Insert(category15);
            _connection.Insert(category16);
            _connection.Insert(category17);
            _connection.Insert(category18);
            _connection.Insert(category19);
            _connection.Insert(category20);
            _connection.Insert(category21);
            _connection.Insert(category22);
            _connection.Insert(category23);
        }

        public List<Money> ListRevenue()
        {
            
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 1 && m.Date.Month == DateTime.Now.Month
                                select m;
                    return money.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Money> ListExpenditure()
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 2 && m.Date.Month == DateTime.Now.Month
                                select m;
                    return money.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(Money money)
        {
            try
            {
                using (_connection)
                {
                    _connection.Insert(money);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Money money)
        {
            try
            {
                using (_connection)
                {
                    _connection.Update(money);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int FilterRevenue(DateTime firstDate, DateTime lastDate)
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 1 && m.Date <= lastDate && m.Date >= firstDate
                        select m.Cost;
                    return money.Sum();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int FilterExpenditure(DateTime firstDate, DateTime lastDate)
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 2 && m.Date <= lastDate && m.Date >= firstDate
                        select m.Cost;
                    return money.Sum();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Money> SearchRevenue(string name)
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 1 && m.MoneyName == name
                                select m;
                    return money.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Money> SearchExpenditure(string name)
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 2 && m.MoneyName == name
                        select m;
                    return money.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SumRevenue()
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 1 && m.Date.Month == DateTime.Now.Month
                        select m.Cost;
                    return money.Sum();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SumExpenditure()
        {
            try
            {
                using (_connection)
                {
                    var money = from m in _connection.Table<Money>().ToList()
                        where m.Form == 2 && m.Date.Month == DateTime.Now.Month
                        select m.Cost;
                    return money.Sum();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //public List<ReportOutput> ReportRevenue(DateTime firstDate, DateTime lastDate)
        //{
        //    try
        //    {
        //        using (_connection)
        //        {
        //            List<ReportOutput> returnList = new List<ReportOutput>();
        //            var moneyList = from m in _connection.Table<Money>().ToList()
        //                where m.Form == 1 && m.Date >= firstDate && m.Date <= lastDate
        //                group m by m.Category into newGroup                       
        //                select new{Id=newGroup.Key, Sum = newGroup.Sum(new)};

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

    }
}
