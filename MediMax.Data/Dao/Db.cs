using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Text.RegularExpressions;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Exceptions;
using MediMax.Data.Models;

namespace MediMax.Data.Dao
{
    public abstract class Db<T> : AcessDb
    {
        private MySqlCommand _command;
        private MySqlConnection _connection;
        private readonly IDictionary<string, int> _integerBinds;
        private readonly IDictionary<string, string> _stringBinds;
        private bool _keepConnectionAlive;
        private string _paginatedSql;
        private Pagination _pagination;
        private bool _queryIsPaginated;
        private int _rowsAffected;
        private List<T> _queryResultList;
        private DbDataReader _reader;
        private string _sql;
        protected readonly MediMaxDbContext _dbContext;

        protected Db(IConfiguration configuration, IWebHostEnvironment hostingEnviroment, MediMaxDbContext dbContext) : base(configuration, hostingEnviroment)

        {
            _integerBinds = new Dictionary<string, int>();
            _stringBinds = new Dictionary<string, string>();
            _dbContext = dbContext;
        }
        protected abstract T Mapper(DbDataReader reader);

        protected async Task Connect(bool keepConnectionAlive = false)
        {
            _keepConnectionAlive = keepConnectionAlive;
            try
            {
                _connection = new MySqlConnection(StringConnection);
                await _connection.OpenAsync();
                Console.WriteLine("Connection with the database established.");
            }
            catch (Exception exception)
            {
                throw new DatabaseConnectionErrorException(
                    "Exception of type 'MediMax.Data.Exceptions.DatabaseConnectionErrorException' was thrown.",
                    exception);
            }
        }

        protected MySqlConnection GetConnection()
        {
            return _connection;
        }

        protected async Task<List<T>> GetQueryResultList()
        {
            T mappedObject;
            try
            {
                _queryResultList = new List<T>();
                while (await _reader.ReadAsync())
                {
                    mappedObject = Mapper(_reader);
                    _queryResultList.Add(mappedObject);
                }

                await _reader.CloseAsync();
                if (!_keepConnectionAlive)
                {
                    await Disconnect();
                }

                /*
                    A non-paged query does not clear the binds because these will be used
                    in the second query to return the number of records in the table, whi
                    ch will be just a change to the existing query and, therefore, will n
                    eed the binds to work correctly.
                 */
                if (!_queryIsPaginated)
                {
                    ClearBinds();
                }

                return _queryResultList;
            }
            catch (Exception)
            {
                Console.WriteLine("Failure on reading.");
                await Disconnect();
                throw;
            }
        }

        protected void ClearBinds()
        {
            _integerBinds.Clear();
            _stringBinds.Clear();
        }

        protected async Task<T> GetQueryResultObject()
        {
            T mappedObject;
            mappedObject = default(T);
            try
            {
                if (await _reader.ReadAsync())
                {
                    mappedObject = Mapper(_reader);
                }

                await _reader.CloseAsync();
                if (!_keepConnectionAlive)
                {
                    await Disconnect();
                }

                ClearBinds();
                return mappedObject;
            }
            catch (Exception)
            {
                Console.WriteLine("Failure on reading.");
                await Disconnect();
                throw;
            }
        }

        protected async Task Disconnect()
        {
            try
            {
                Console.WriteLine("Connection with the database closed.");
                await _connection.CloseAsync();
            }
            catch (Exception)
            {

            }

        }

        protected void AddIntegerBind(string key, int value)
        {
            _integerBinds.Add(key, value);
        }

        protected void AddStringBind(string key, string value)
        {
            _stringBinds.Add(key, value);
        }

        private void AddBindsToCommand()
        {
            foreach (KeyValuePair<string, string> bind in _stringBinds)
            {
                _command.Parameters.Add(bind.Key, MySqlDbType.VarChar);
                _command.Parameters["@" + bind.Key].Value = bind.Value;
            }

            foreach (KeyValuePair<string, int> bind in _integerBinds)
            {
                _command.Parameters.Add(bind.Key, MySqlDbType.Int32);
                _command.Parameters["@" + bind.Key].Value = bind.Value;
            }
        }

        protected async Task<bool> Query(string sql)
        {
            string commandSql;
            commandSql = "";
            try
            {
                _sql = sql.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                ;
                if (_queryIsPaginated)
                {
                    AssemblePaginatedSql();
                }

                commandSql = _queryIsPaginated ? _paginatedSql : _sql;
                _command = new MySqlCommand(commandSql, _connection);
                AddBindsToCommand();
                await _command.PrepareAsync();
                _reader = await _command.ExecuteReaderAsync();
                return true;
            }
            catch (Exception exception)
            {
                await Disconnect();
                string message =
                    "Exception of type 'MediMax.Data.Exceptions.DatabaseQueryFailureException' was thrown.";
                message += "SQL: " + commandSql;
                throw new DatabaseQueryFailureException(message, exception);
            }
        }

        protected async Task<bool> PersistQuery(string sql)
        {
            string commandSql;
            commandSql = "";
            try
            {
                _sql = sql.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                commandSql = _sql;
                _command = new MySqlCommand(commandSql, _connection);
                AddBindsToCommand();
                await _command.PrepareAsync();
                _rowsAffected = await _command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception exception)
            {
                await Disconnect();
                string message =
                    "Exception of type 'RoboSou.Data.Exceptions.DatabaseQueryFailureException' was thrown.";
                message += "SQL: " + commandSql;
                throw new DatabaseQueryFailureException(message, exception);
            }
        }

        protected async Task<int> GetRowsAffected()
        {
            if (!_keepConnectionAlive)
            {
                await Disconnect();
            }
            ClearBinds();
            return _rowsAffected;
        }

        protected async Task<int> GetLastInsertedId()
        {
            int id = (int)_command.LastInsertedId;
            if (!_keepConnectionAlive)
            {
                await Disconnect();
            }
            ClearBinds();
            return id;
        }

        private void AssemblePaginatedSql()
        {
            _paginatedSql += _sql + " LIMIT @paginationOffset, @paginationLimit";
            AddIntegerBind("paginationOffset", _pagination.Offset());
            AddIntegerBind("paginationLimit", _pagination.Limit());
        }

        protected void SetPagination(Pagination pagination)
        {
            _pagination = pagination;
            if (pagination.CurrentPage <= 0)
            {
                pagination.CurrentPage = 1;
            }
            if (pagination.PerPage <= 0)
            {
                pagination.PerPage = 1;
            }
            _queryIsPaginated = true;
            _keepConnectionAlive = true;
        }

        public async Task<Pagination> GetPagination()
        {
            string sql;
            string pattern;
            string replacement;
            int total;

            /*
                The idea is to replace the excerpt of the sql query that references the
                fields by a single string that calls the count function to return the nu
                mber of lines, generating a new sql query. 
                
                However, the way the idea was implemented works for all sql queries whose
                field snippet does not contain any subqueries. If there is any subquery be
                fore the "from" directive, the regular expression will understand that the
                "from" of the subquery is the one of the parent query, which leads to unex
                pected behavior.
                
                Therefore, when paging, remember that the sql query must be simple enough
                not to contain any subqueries before the "from" directive.
                
                For cases in which the subquery is essential, paging must be done manually
                in the requesting class.
             */
            pattern = @"(SELECT)(.*)(FROM)";
            replacement = "$1 COUNT(*) AS NumRows $3";
            sql = Regex.Replace(_sql, pattern, replacement, RegexOptions.IgnoreCase);

            _queryIsPaginated = false;
            /*
             * The pagination routine was optimized in order to let the connection that
             * performs the query to get the list of results be the same that gets the
             * number of records on the table. This is why there is no reference to a
             * database connection request in this method.
             */
            await Query(sql);

            total = 0;
            if (await _reader.ReadAsync())
            {
                total = Convert.ToInt32(_reader["NumRows"]);
            }

            await _reader.CloseAsync();
            await Disconnect();
            _pagination.SetTotal(total);
            ClearBinds();
            return _pagination;
        }

        public async Task<Pagination> GetPagination(string sql)
        {
            int total;
            _queryIsPaginated = false;
            /*
             * The pagination routine was optimized in order to let the connection that
             * performs the query to get the list of results be the same that gets the
             * number of records on the table. This is why there is no reference to a
             * database connection request in this method.
             */
            await Query(sql);

            total = 0;
            if (await _reader.ReadAsync())
            {
                total = Convert.ToInt32(_reader["NumRows"]);
            }

            await _reader.CloseAsync();
            await Disconnect();
            _pagination.SetTotal(total);
            ClearBinds();
            return _pagination;
        }

        protected async Task<bool> QueryHasResult()
        {
            bool queryHasResult;

            queryHasResult = false;
            try
            {
                if (await _reader.ReadAsync())
                {
                    queryHasResult = true;
                }

                await _reader.CloseAsync();
                if (!_keepConnectionAlive)
                {
                    await Disconnect();
                }

                ClearBinds();
                return queryHasResult;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failure on reading.");
                Console.WriteLine(exception.Message);
                await Disconnect();
                return false;
            }
        }

        protected async Task<DbDataReader> GetDataReader()
        {
            return _reader;
        }

        protected string GetParametersList(List<int> numbers)
        {
            string sql;
            int length;
            length = numbers.Count;
            switch (length)
            {
                case 0:
                    return "()";
                case 1:
                    return "(" + numbers[0] + ")";
                default:
                    sql = "(";
                    for (int i = 0; i < length; i++)
                    {
                        sql += numbers[i];
                        if (i == length - 1)
                        {
                            continue;
                        }
                        sql += ",";
                    }

                    sql += ")";
                    return sql;
            }
        }

        protected string GetParametersList(List<string> numbers)
        {
            string sql;
            int length;
            length = numbers.Count;
            sql = "(";
            for (int i = 0; i < length; i++)
            {
                sql += $"'{numbers[i]}'";
                if (i == length - 1)
                {
                    continue;
                }

                sql += ",";
            }

            sql += ")";
            return sql;
        }

        protected void AddIntegerParameterToEntityCommand(DbCommand command, string parameterName, int value)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = $"@{parameterName}";
            parameter.DbType = DbType.Int32;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}