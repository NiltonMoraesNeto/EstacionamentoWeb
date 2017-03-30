using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ZTC.BLL;
using ZTC.DAL;

namespace InterfaceConexao.DAL
{
    public class DataAccessLayer : IDisposable
    {
        #region Constants

        private const string ERRO_100 = "Informe a string de conexão. Ela não pode ser nula ou vazia";
        private const string ERRO_101 = "A conexão já está aberta";
        private const string ERRO_102 = "Ocorreu um erro ao abrir a conexão";
        private const string ERRO_103 = "A conexão já está fechada";
        private const string ERRO_104 = "Ocorreu um erro ao fechar a conexão";
        private const string ERRO_105 = "Ocorreu um erro iniciar a transacao";
        private const string ERRO_106 = "Nenhuma transacao foi iniciada";
        private const string ERRO_107 = "Ocorreu um erro ao comitar a transacao";
        private const string ERRO_108 = "Ocorreu um erro ao rollbackar a transacao";
        private const string ERRO_109 = "Ocorreu um erro ao executar o método";
        private const string ERRO_110 = "A conexao da transacao deve ser a mesma que a conexao informada";

        #endregion

        #region Fields

        private readonly ConnectionType _type;

        #endregion

        #region Properties

        /// <summary>
        /// Objeto de conexão
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// Objeto de transação
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        public int TimeOut { get; set; }

        public ConnectionType Type
        {
            get { return _type; }
        }

        public bool ConnectionOpened
        {
            get { return Connection.State == ConnectionState.Open; }
        }

        public bool TransactionOpened
        {
            get { return Transaction != null; }
        }

        #endregion

        #region Methods

        #region DataAccessObject

        /// <summary>
        /// Construtor da classe de acesso a dados
        /// </summary>
        /// <param name="type">Tipo do banco de dados para acesso</param>
        /// <param name="connectionString">String de conexão</param>
        /// <param name="timeOut">Time out da consulta, em seg</param>
        public DataAccessLayer(ConnectionType type, string connectionString, int timeOut = 90)
        {
            Transaction = null;
            TimeOut = timeOut;
            _type = type;

            switch (_type)
            {
                case ConnectionType.SqlServer:
                    Connection = new SqlConnection(connectionString);
                    break;
                case ConnectionType.MySql:
                    Connection = new MySqlConnection(connectionString);
                    break;
                case ConnectionType.Odbc:
                    Connection = new OdbcConnection(connectionString);
                    break;
                case ConnectionType.OleDb:
                    Connection = new OleDbConnection(connectionString);
                    break;
                /* case ConnectionType.Firebird:
                     Connection = new FbConnection(connectionString);
                     break;
                 case ConnectionType.Oracle:
                     Connection = new OracleConnection(connectionString);
                     break;
                 case ConnectionType.DB2:
                     Connection = new DB2Connection(connectionString);
                     break;
                 case ConnectionType.SQLite:
                     Connection = new SQLiteConnection(connectionString);
                     break;*/
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        #endregion

        /// <summary>
        /// Cria uma instancia da classe que implemensta IDbCommand, a partir de uma conexão
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="connection">Objeto de conexão</param>
        /// <param name="commandType"> </param>
        /// <param name="transaction">Objeto de transação</param>
        /// <param name="parameters">Parametros da consulta</param>
        /// <param name="timeOut"> </param>
        /// <returns></returns>
        private static IDbCommand PrepareCommand(string query, IDbConnection connection, CommandType commandType,
                                                 IDbTransaction transaction,
                                                 IEnumerable<IDbDataParameter> parameters, int timeOut)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (connection == null) throw new ArgumentNullException("connection");

            var command = connection.CreateCommand();
            command.CommandTimeout = timeOut;

            if (transaction != null)
            {
                if (command.Connection == transaction.Connection)
                {
                    command.Transaction = transaction;
                }
                else
                {
                    throw new Exception(ERRO_110);
                }
            }

            command.CommandText = query;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }

                    command.Parameters.Add(parameter);
                }
            }

            

            return command;
        }

        /// <summary>
        /// Cria parametro
        /// </summary>
        /// <param name="parameterName">Nome do parametro</param>
        /// <param name="value">Valor do parametro</param>
        /// <returns>Objeto de parametro</returns>
        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            switch (_type)
            {
                case ConnectionType.SqlServer:
                    return new SqlParameter(parameterName, value);
                case ConnectionType.MySql:
                    return new MySqlParameter(parameterName, value);
                case ConnectionType.Odbc:
                    return new OdbcParameter(parameterName, value);
                case ConnectionType.OleDb:
                    return new OleDbParameter(parameterName, value);
                /* case ConnectionType.Firebird:
                     return new FbParameter(parameterName, value);
                 case ConnectionType.Oracle:
                     return new OracleParameter(parameterName, value);
                 case ConnectionType.DB2:
                     return new DB2Parameter(parameterName, value);
                 case ConnectionType.SQLite:
                     return new SQLiteParameter(parameterName, value);*/
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DbDataAdapter CreateDataAdapter()
        {
            switch (_type)
            {
                case ConnectionType.SqlServer:
                    return new SqlDataAdapter();
                case ConnectionType.MySql:
                    return new MySqlDataAdapter();
                case ConnectionType.Odbc:
                    return new OdbcDataAdapter();
                case ConnectionType.OleDb:
                    return new OleDbDataAdapter();
                /* case ConnectionType.Firebird:
                     return new FbDataAdapter();
                 case ConnectionType.Oracle:
                     return new OracleDataAdapter();
                 case ConnectionType.DB2:
                     return new DB2DataAdapter();
                 case ConnectionType.SQLite:
                     return new SQLiteDataAdapter();*/
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region OpenConnection

        /// <summary>
        /// Abre a conexão com o banco de dados
        /// </summary>
        public void OpenConnection()
        {
            OpenConnection(true);
        }

        public void OpenConnection(ConditionDelegate condition)
        {
            OpenConnection(condition());
        }

        /// <summary>
        /// Abre a conexão com o banco de dados
        /// </summary>
        public void OpenConnection(bool condition)
        {
            OpenConnection(Connection, condition);
        }

        /// <summary>
        /// Abre a conexão com o banco de dados com o objeto passado por parametro
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"> </param>
        private static void OpenConnection(IDbConnection connection, bool condition = true)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(connection.ConnectionString)) throw new Exception(ERRO_100);

            if (condition)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ERRO_102, ex);
                    }
                }
                else
                    throw new Exception(ERRO_101);
            }
        }

        #endregion

        #region CloseConnection

        /// <summary>
        /// Fecha a conexão com o banco de dados
        /// </summary>
        public void CloseConnection()
        {
            CloseConnection(true);
        }

        public void CloseConnection(ConditionDelegate condition)
        {
            CloseConnection(condition());
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados
        /// </summary>
        public void CloseConnection(bool condition)
        {
            CloseConnection(Connection, condition);
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados com o objeto passado por parametro
        /// </summary>
        private static void CloseConnection(IDbConnection connection, bool condition = true)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            if (condition)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ERRO_104, ex);
                    }
                }
                else
                    throw new Exception(ERRO_103);
            }
        }

        #endregion

        #region BeginTransaction

        public void BeginTransaction()
        {
            BeginTransaction(true);
        }

        public void BeginTransaction(ConditionDelegate condition)
        {
            BeginTransaction(condition());
        }

        public void BeginTransaction(bool condition)
        {
            if (Connection == null) throw new ArgumentNullException("_connection");

            if (condition)
            {
                if (!ConnectionOpened)
                {
                    OpenConnection();
                }

                try
                {
                    Transaction = Connection.BeginTransaction();
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_105, ex);
                }
            }
        }

        #endregion

        #region CommitTransaction

        public void CommitTransaction()
        {
            CommitTransaction(true);
        }

        public void CommitTransaction(ConditionDelegate condition)
        {
            CommitTransaction(condition());
        }

        public void CommitTransaction(bool condition)
        {
            if (Connection == null) throw new ArgumentNullException("_connection");

            if (condition)
            {
                if (!TransactionOpened)
                {
                    throw new Exception(ERRO_106);
                }

                try
                {
                    Transaction.Commit();
                    Transaction.Dispose();
                    Transaction = null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_107, ex);
                }
            }
        }

        #endregion

        #region RollbackTransaction

        /// <summary>
        /// Não envia a transação
        /// </summary>
        public void RollbackTransaction()
        {
            RollbackTransaction(true);
        }

        public void RollbackTransaction(ConditionDelegate condition)
        {
            RollbackTransaction(condition());
        }

        /// <summary>
        /// Não envia a transação
        /// </summary>
        public void RollbackTransaction(bool condition)
        {
            if (Connection == null) throw new ArgumentNullException("_connection");

            if (condition)
            {
                if (!TransactionOpened)
                {
                    throw new Exception(ERRO_106);
                }

                try
                {
                    Transaction.Rollback();
                    Transaction.Dispose();
                    Transaction = null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_108, ex);
                }
            }
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Executa uma consulta SQL e retorna as linhas afetadas
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="timeOut"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>Linhas afetadas</returns>
        public int ExecuteNonQuery(string query,
                                   CommandType commandType = CommandType.Text,
                                   params IDbDataParameter[] parameters)
        {
            return ExecuteNonQuery(query, commandType, new List<IDbDataParameter>(parameters));
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna as linhas afetadas
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="timeOut"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <param name="commandType"> </param>
        /// <returns>Linhas afetadas</returns>
        public int ExecuteNonQuery(string query,
                                   CommandType commandType = CommandType.Text,
                                   IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteNonQuery(query, Connection, commandType, Transaction, TimeOut, parameters);
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna as linhas afetadas
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <param name="connection">Objeto de conexão</param>
        /// <param name="transaction">Objeto de transação</param>
        /// <param name="timeOut"> </param>
        /// <returns>Linhas afetadas</returns>
        public static int ExecuteNonQuery(string query,
                                          IDbConnection connection,
                                          CommandType commandType = CommandType.Text,
                                          IDbTransaction transaction = null,
                                          int timeOut = 90,
                                          IEnumerable<IDbDataParameter> parameters = null)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (connection == null) throw new ArgumentNullException("connection");

            var connectionOpened = connection.State == ConnectionState.Open;

            OpenConnection(connection, !connectionOpened);

            try
            {
                using (var cmd = PrepareCommand(query, connection, commandType, transaction, parameters, timeOut))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ConflictedException exC;

                if (ConflictedException.CheckConflictedException(ex, out exC))
                {
                    throw exC;
                }

                throw new Exception(ERRO_109, ex);
            }
            finally
            {
                CloseConnection(connection, !connectionOpened);
            }
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Executa uma consulta SQL e retorna o primeiro valor da primeira coluna
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>O primeiro valor da primeira coluna</returns>
        public object ExecuteScalar(string query,
                                    CommandType commandType = CommandType.Text,
                                    params IDbDataParameter[] parameters)
        {
            return ExecuteScalar(query, commandType, new List<IDbDataParameter>(parameters));
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna o primeiro valor da primeira coluna
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>O primeiro valor da primeira coluna</returns>
        public object ExecuteScalar(string query,
                                    CommandType commandType = CommandType.Text,
                                    IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteScalar(query, Connection, commandType, Transaction, TimeOut, parameters);
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna o primeiro valor da primeira coluna
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="timeOut"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <param name="connection">Objeto de conexão</param>
        /// <param name="commandType"> </param>
        /// <param name="transaction">Objeto de transação</param>
        /// <returns>O primeiro valor da primeira coluna</returns>
        public static object ExecuteScalar(string query,
                                           IDbConnection connection,
                                           CommandType commandType = CommandType.Text,
                                           IDbTransaction transaction = null,
                                           int timeOut = 90,
                                           IEnumerable<IDbDataParameter> parameters = null)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (connection == null) throw new ArgumentNullException("connection");

            var connectionOpened = connection.State == ConnectionState.Open;

            OpenConnection(connection, !connectionOpened);

            try
            {
                using (var cmd = PrepareCommand(query, connection, commandType, transaction, parameters, timeOut))
                {
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_109, ex);
            }
            finally
            {
                CloseConnection(connection, !connectionOpened);
            }
        }

        #endregion

        #region ExecuteDataReader

        /// <summary>
        /// Executa uma consulta SQL e retorna um IDataReader com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>IDataReader com a massa de dados</returns>
        public IDataReader ExecuteDataReader(string query,
                                             CommandType commandType = CommandType.Text,
                                             params IDbDataParameter[] parameters)
        {
            return ExecuteDataReader(query, commandType, new List<IDbDataParameter>(parameters));
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna um IDataReader com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>IDataReader com a massa de dados</returns>
        public IDataReader ExecuteDataReader(string query,
                                             CommandType commandType = CommandType.Text,
                                             IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteDataReader(query, Connection, commandType, Transaction, TimeOut, parameters);
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna um IDataReader com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="timeOut"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <param name="connection">Objeto de conexão</param>
        /// <param name="commandType"> </param>
        /// <param name="transaction">Objeto de transação</param>
        /// <returns>IDataReader com a massa de dados</returns>
        public static IDataReader ExecuteDataReader(string query,
                                                    IDbConnection connection,
                                                    CommandType commandType = CommandType.Text,
                                                    IDbTransaction transaction = null,
                                                    int timeOut = 90,
                                                    IEnumerable<IDbDataParameter> parameters = null)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (connection == null) throw new ArgumentNullException("connection");

            var connectionOpened = connection.State == ConnectionState.Open;

            OpenConnection(connection, !connectionOpened);

            try
            {
                using (var cmd = PrepareCommand(query, connection, commandType, transaction, parameters, timeOut))
                {
                    return cmd.ExecuteReader(!connectionOpened
                                                 ? CommandBehavior.CloseConnection
                                                 : CommandBehavior.Default);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_109, ex);
            }
            finally
            {
                //CloseConnection(connection, !connectionOpened && connection.State == ConnectionState.Open);
            }
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// Executa uma consulta SQL e retorna um DataTable com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>DataTable com a massa de dados</returns>
        public DataTable ExecuteDataTable(string query,
                                          CommandType commandType = CommandType.Text,
                                          params IDbDataParameter[] parameters)
        {
            return ExecuteDataTable(query, commandType, new List<IDbDataParameter>(parameters));
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna um DataTable com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="commandType"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <returns>DataTable com a massa de dados</returns>
        public DataTable ExecuteDataTable(string query,
                                          CommandType commandType = CommandType.Text,
                                          IEnumerable<IDbDataParameter> parameters = null)
        {
            return ExecuteDataTable(query, Connection, commandType, Transaction, TimeOut, parameters);
        }

        /// <summary>
        /// Executa uma consulta SQL e retorna um DataTable com a massa de dados
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="timeOut"> </param>
        /// <param name="parameters">Parametros para a consulta</param>
        /// <param name="connection">Objeto de conexão</param>
        /// <param name="commandType"> </param>
        /// <param name="transaction">Objeto de transação</param>
        /// <returns>DataTable com a massa de dados</returns>
        public static DataTable ExecuteDataTable(string query,
                                                 IDbConnection connection,
                                                 CommandType commandType = CommandType.Text,
                                                 IDbTransaction transaction = null,
                                                 int timeOut = 90,
                                                 IEnumerable<IDbDataParameter> parameters = null)
        {
            if (query == null) throw new ArgumentNullException("query");
            if (connection == null) throw new ArgumentNullException("connection");

            var connectionOpened = connection.State == ConnectionState.Open;

            OpenConnection(connection, !connectionOpened);

            try
            {
                IDataReader result;

                using (var cmd = PrepareCommand(query, connection, commandType, transaction, parameters, timeOut))
                {
                    result = cmd.ExecuteReader();
                }

                var dt = new DataTable();
                dt.Load(result);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_109, ex);
            }
            finally
            {
                CloseConnection(connection, !connectionOpened);
            }
        }

        #endregion

        public bool TestConnection()
        {
            if (!ConnectionOpened)
            {
                OpenConnection();
                CloseConnection();

                return true;
            }

            throw new Exception(ERRO_101);
        }

        public void Dispose()
        {
            if (Transaction != null) Transaction.Dispose();
            if (Connection != null) Connection.Dispose();
        }

        public static DataAccessLayer GetDataAccessObject(ConnectionType type, string connectionString, int timeOut = 90)
        {
            return new DataAccessLayer(type, connectionString, timeOut);
        }

        #endregion
    }
}
