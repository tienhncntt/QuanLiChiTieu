using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace QuanLiChiTieu.Models
{
    public class Form
    {
        [PrimaryKey]
        public int FormID { get; set; }

        public string FormName { get; set; }
    }
}
