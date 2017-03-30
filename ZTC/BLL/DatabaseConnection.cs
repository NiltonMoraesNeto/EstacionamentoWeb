using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfaceConexao.DAL;

namespace ZTC.BLL
{
    public static class DatabaseConnection
    {
        public static string ConnectionString { get; set; }

        public static ConnectionType Type { get; set; }

        public static int TimeOut { get; set; }

        public static DataAccessLayer GetDataAccessLayer()
        {
            return new DataAccessLayer(Type, ConnectionString, TimeOut);
        }
    }
}