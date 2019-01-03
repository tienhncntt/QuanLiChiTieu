using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace QuanLiChiTieu.Models
{
    public class Money
    {
        [PrimaryKey, AutoIncrement]
        public int MoneyID { get; set; }
        public  string MoneyName { get; set; }
        public int Form { get; set; }
        public int Category { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public byte[] Image { get; set; }

    }
}
