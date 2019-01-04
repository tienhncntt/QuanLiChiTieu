using System.IO;
using QuanLiChiTieu.Interface;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuanLiChiTieu.Droid.SQLiteService))]
namespace QuanLiChiTieu.Droid
{
    public class SQLiteService : ISQLite
    {
        private string sqliteFilename = "QuanLiChiTieu.db";
        string GetPath()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentsPath, sqliteFilename);
            return path;
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }
    }
}