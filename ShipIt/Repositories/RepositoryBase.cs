using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web;
using Npgsql;
using ShipIt.Controllers;
using ShipIt.Models.DataModels;

namespace ShipIt.Repositories
{
    //TODO MatEng This wrapper is a bit of a hack.  There should be a way to inject the Connection directly but I'm not sure how with the xml format of Castle Windsor.
    public interface IDatabaseConnectionWrapper
    {
        IDbConnection Connection { get; }
    }

    public abstract class RepositoryBase
    {
        private readonly IDatabaseConnectionWrapper connectionWrapper;

        //TODO A new connection is created every time a query is called.  Ideally just one connection would be used for the request.
        private IDbConnection Connection =>
            new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString);

        protected long QueryForLong(string sqlString)
        {
            using (IDbConnection connection = Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = sqlString;
                connection.Open();
                var reader = command.ExecuteReader();

                try
                {
                    reader.Read();
                    return reader.GetInt64(0);
                }
                finally
                {
                    reader.Close();
                }
            };
        }

        protected T QueryForProduct<T>(string sql, Func<IDataReader, T> create, params NpgsqlParameter[] parameters) 
        {
            using (IDbConnection connection = Connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = sql;
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                connection.Open();
                var reader = command.ExecuteReader();

                try
                {
                    reader.Read();
                    return create(reader);
                }
                finally
                {
                    reader.Close();
                }
            };
        }
    }
}