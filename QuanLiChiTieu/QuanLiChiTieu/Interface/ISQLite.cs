using SQLite;

namespace QuanLiChiTieu.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
