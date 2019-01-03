using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace QuanLiChiTieu.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
