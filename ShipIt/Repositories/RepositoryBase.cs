using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using Npgsql;
using ShipIt.Exceptions;

namespace ShipIt.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDbConnection Connection =>
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

        protected void RunSingleQuery(string sql, string noResultsExceptionMessage, params NpgsqlParameter[] parameters)
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
                    if (reader.RecordsAffected != 1)
                    {
                        throw new NoSuchEntityException(noResultsExceptionMessage);
                    }
                    reader.Read();
                }
                finally
                {
                    reader.Close();
                }
            };
        }

        protected int RunSingleQueryAndReturnRecordsAffected(string sql, params NpgsqlParameter[] parameters)
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
                }
                finally
                {
                    reader.Close();
                }
                return reader.RecordsAffected;
            };
        }

        protected TDataModel RunSingleGetQuery<TDataModel>(string sql, Func<IDataReader, TDataModel> mapToDataModel, string noResultsExceptionMessage, params NpgsqlParameter[] parameters)
        {
            return RunGetQuery(sql, mapToDataModel, noResultsExceptionMessage, parameters).Single();
        }

        protected IEnumerable<TDataModel> RunGetQuery<TDataModel>(string sql, Func<IDataReader, TDataModel> mapToDataModel, string noResultsExceptionMessage, params NpgsqlParameter[] parameters)
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
                    if (!reader.Read())
                    {
                        throw new NoSuchEntityException(noResultsExceptionMessage);
                    }
                    yield return mapToDataModel(reader);

                    while (reader.Read())
                    {
                        yield return mapToDataModel(reader);
                    }
                }
                finally
                {
                    reader.Close();
                }
            };
        }
        protected void RunQuery(string sql, params NpgsqlParameter[] parameters)
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
                }
                finally
                {
                    reader.Close();
                }
            };
        }
    }
}