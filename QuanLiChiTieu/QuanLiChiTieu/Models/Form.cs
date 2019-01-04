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
