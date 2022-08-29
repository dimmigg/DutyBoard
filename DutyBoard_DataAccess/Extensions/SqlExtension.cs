using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DutyBoard_DataAccess.Extensions
{
    public static class SqlExtension
    {
        public static IEnumerable<T> Execute<T>(this SqlConnection connect, string sql, DynamicParameters param = null, IDbTransaction transaction = null) =>
            connect.Query<T>(sql, param, transaction, commandTimeout: 36000, commandType: CommandType.Text);

        public static IEnumerable<T> ExecuteProcedure<T>(this SqlConnection connect, string sql, DynamicParameters param = null, IDbTransaction transaction = null) =>
            connect.Query<T>(sql, param, transaction, commandTimeout: 36000, commandType: CommandType.StoredProcedure);
    }
}
