using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLiChiTieu.Models
{
    public class Category
    {
        [PrimaryKey]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        public int Form { get; set; }
    }
}
