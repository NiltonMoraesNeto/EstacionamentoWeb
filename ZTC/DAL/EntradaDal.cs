using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZTC.Models;
using ZTC.DAL;
using InterfaceConexao.DAL;
using MySql.Data.MySqlClient;
using ZTC.DAL;
using InterfaceConexao.DAL;
using ZTC.Model;

namespace ZTC.Dal
{
    public class EntradaDal : DALBase<Entrada>
    {
        protected DataAccessLayer DAL;

        public EntradaDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Entrada o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdEntrada", o.IdEntrada));
            parms.Add(new MySqlParameter("@Nome", !String.IsNullOrEmpty(o.Nome) ? o.Nome : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Placa", !String.IsNullOrEmpty(o.Placa) ? o.Placa : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Carro", !String.IsNullOrEmpty(o.Carro) ? o.Carro : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Servico", !String.IsNullOrEmpty(o.Servico) ? o.Servico : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataHoraEntrada", o.DataHoraEntrada.HasValue ? o.DataHoraEntrada : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Fechado", o.Fechado));

            if (!o.Persisted)
            {
                parms[0].Direction = ParameterDirection.Output;
            }
            else
            {
                parms[0].Direction = ParameterDirection.Input;
            }
            return parms;

        }
        public void Insert(Entrada o)
        {
            String sql = "INSERT INTO Entrada (Nome, Placa, Carro, Servico, " +
                         "DataHoraEntrada, Fechado)" +
                         " VALUES (@Nome, @Placa, @Carro, @Servico, " +
                         "@DataHoraEntrada, @Fechado);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdEntrada = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Entrada o)
        {
            String sql = "UPDATE Entrada SET Nome = @Nome, Placa = @Placa, " +
                         "Carro = @Carro, Servico = @Servico, " +
                         "DataHoraEntrada = @DataHoraEntrada, Fechado = @Fechado " +
                         "WHERE IdEntrada = @IdEntrada ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Entrada o)
        {
            String sql = "DELETE";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdEntrada", o.IdEntrada));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Entrada o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdEntrada = Convert.ToInt32(dr["IdEntrada"]);
            if (dr["Nome"] != DBNull.Value)
                o.Nome = Convert.ToString(dr["Nome"]);
            if (dr["Placa"] != DBNull.Value)
                o.Placa = Convert.ToString(dr["Placa"]);
            if (dr["Carro"] != DBNull.Value)
                o.Carro = Convert.ToString(dr["Carro"]);
            if (dr["Servico"] != DBNull.Value)
                o.Servico = Convert.ToString(dr["Servico"]);
            if (dr["DataHoraEntrada"] != DBNull.Value)
                o.DataHoraEntrada = Convert.ToDateTime(dr["DataHoraEntrada"]);
            if (dr["Fechado"] != DBNull.Value)
                o.Fechado = Convert.ToBoolean(dr["Fechado"]); o.Persisted = true;
        }
        public override void CompleteObject(Entrada o)
        {
            using (var dr = GetObjectDataReader(o.IdEntrada))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idEntrada)
        {
            String sql = "SELECT * FROM Entrada WHERE IdEntrada = @IdEntrada ";

            var parms = new MySqlParameter("@IdEntrada", idEntrada);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Entrada GetObject(int idEntrada)
        {
            using (var dr = GetObjectDataReader(idEntrada))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Entrada ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Entrada " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Entrada> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Entrada> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
