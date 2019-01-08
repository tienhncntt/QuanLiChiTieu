using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace QuanLiChiTieu.Models
{
    public class NewMoney
    {
        [PrimaryKey, AutoIncrement]
        public int MoneyID { get; set; }
        public string MoneyName { get; set; }
        public int Form { get; set; }
        public int CategoryID { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public byte[] Image { get; set; }
        public string CategoryName { get; set; }

    }
}
