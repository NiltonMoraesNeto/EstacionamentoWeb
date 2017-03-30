using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceConexao.DAL
{
    /// <summary>
    /// Tipo do banco de dados
    /// </summary>
    public enum ConnectionType
    {
        //SqlServer = 0,
        SqlServer = 0,
        MySql = 1,
        Odbc = 2,
        OleDb = 3,
        Firebird = 4,
        Oracle = 5,
        DB2 = 6,
        SQLite = 7
    }
}