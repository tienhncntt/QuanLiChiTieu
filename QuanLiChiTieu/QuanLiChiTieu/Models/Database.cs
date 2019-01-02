using Prism.Services;
using QuanLiChiTieu.Interface;
using SQLite;
using Xamarin.Forms;
using DependencyService = Xamarin.Forms.DependencyService;

namespace QuanLiChiTieu.Models
{
    public class Database
    {
        readonly SQLiteConnection connection;

        public Database()
        {
            connection = DependencyService.Get<ISQLite>().GetConnection();
            //connection.CreateTable<Money>();
            connection.CreateTable<Form>();
            connection.CreateTable<Category>();
        }
    }
}
