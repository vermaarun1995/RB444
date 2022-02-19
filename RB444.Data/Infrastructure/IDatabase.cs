using RB444.Data.UOW;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace RB444.Data.Infrastructure
{
    public interface IDatabase : IDisposable
    {
        DbConnection NewConnection();
        Task<DbConnection> NewConnectionAsync();
        SqlConnection NewSqlConnection();
        Task<SqlConnection> NewSqlConnectionAsync();
        IUnitOfWork NewUnitOfWork();
        IUnitOfWork NewAsyncUnitOfWork();
    }
}
