using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace KC.HMS.Core.Extensions
{

    public static class DapperDb
    {
        public static IDbConnection Database;
        public static IConfiguration Config;
        public static void Initialize(IDbConnection connection, IConfiguration configuration)
        {
            Config = configuration;
            Database = connection;
        }

        public static string GetConfig(string section)
        {
            return Config.GetSection(section).Value;
        }
         
        public static IEnumerable<T> Query<T>(string sql,
            CommandType cmdType = CommandType.StoredProcedure,
            object parameters = null)
        {
            var result = default(List<T>);

            result = Database.Query<T>(sql, param: parameters, commandType: cmdType).ToList();

            return result;
        }

        public static T QuerySingle<T>(
            string sql,
         CommandType cmdType = CommandType.StoredProcedure, object parameters = null)
        {
            var result = default(T);

            result = Database.Query<T>(sql, param: parameters, commandType: cmdType).FirstOrDefault();

            return result;
        }

        public static void QueryMultiple(string sql,
            Action<SqlMapper.GridReader> callback,
            CommandType cmdType = CommandType.StoredProcedure, object parameters = null)
        {

            var gr = Database.QueryMultiple(sql, param: parameters, commandType: cmdType);

            callback(gr);
        }


        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultiple<T1, T2>(
             string sql,
             CommandType cmdType = CommandType.Text, object parameters = null)
        {

            var gridReader = Database.QueryMultiple(sql, param: parameters);
            var t1List = gridReader.Read<T1>();
            var t2List = gridReader.Read<T2>();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(t1List, t2List);

        }
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultiple<T1, T2, T3>(

           string sql,
           CommandType cmdType = CommandType.Text, object parameters = null)
        {

            var gridReader = Database.QueryMultiple(sql, param: parameters);
            var t1List = gridReader.Read<T1>();
            var t2List = gridReader.Read<T2>();
            var t3List = gridReader.Read<T3>();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(t1List, t2List, t3List);

        }

        //https://mikhail.io/2016/04/t-sql-merge-statement-is-underrated/
        public static void Merge<TEntity>(string connectionString, TEntity entity) where TEntity : class
        {

            var props = entity.GetType().GetProperties().Select(p => p.Name).ToList();
            var names = string.Join(", ", props);
            var values = string.Join(", ", props.Select(n => "@" + n));
            var updates = string.Join(", ", props.Select(n => $"{n} = @{n}"));
            Database.Execute(
                $@"MERGE {entity.GetType().Name} as target
                  USING (VALUES({values}))
                  AS SOURCE ({names})
                  ON target.Id = @Id
                  WHEN matched THEN
                    UPDATE SET {updates}
                  WHEN not matched THEN
                    INSERT({names}) VALUES({values});",
                entity);
        }

        #region DB Query, QuerySingle, Query Multiple 

        public static IEnumerable<T> Query<T>(IDbConnection Db,
            string sql, CommandType
            cmdType = CommandType.Text, object parameters = null)
        {
            var result = default(List<T>);
            result = Db.Query<T>(sql, param: parameters, commandType: cmdType).ToList();
            return result;
        }

        public static T QuerySingle<T>(IDbConnection Db, string sql, CommandType cmdType = CommandType.Text, object parameters = null)
        {
            var entity = default(T);
            entity = Db.Query<T>(sql, param: parameters, commandType: cmdType).FirstOrDefault();
            return entity;
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultiple<T, T1, T2>(
            IDbConnection Db,
            string sql,
            CommandType cmdType = CommandType.Text, object parameters = null)
        {

            var gridReader = Db.QueryMultiple(sql, param: parameters);
            var t1List = gridReader.Read<T1>();
            var t2List = gridReader.Read<T2>();

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(t1List, t2List);

        }

        public static List<T> QueryList<T>(IDbConnection Db, string sql,
            CommandType cmdType = CommandType.Text,
            object parameters = null)
        {
            List<T> result = null;
            result = Db.Query<T>(sql, param: parameters, commandType: cmdType).ToList();
            return result;
        }

        #endregion


        private static void HandleError(Exception exception)
        {
            throw exception;
        }
    }
}
