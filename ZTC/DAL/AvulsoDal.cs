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
    public class AvulsoDal : DALBase<Avulso>
    {
        protected DataAccessLayer DAL;

        public AvulsoDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Avulso o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdAvulso", o.IdAvulso));
            parms.Add(new MySqlParameter("@Nome", !String.IsNullOrEmpty(o.Nome) ? o.Nome : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@CPF", !String.IsNullOrEmpty(o.CPF) ? o.CPF : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@TelefoneFixo", !String.IsNullOrEmpty(o.TelefoneFixo) ? o.TelefoneFixo : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@TelefoneCelular", !String.IsNullOrEmpty(o.TelefoneCelular) ? o.TelefoneCelular : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Marca", !String.IsNullOrEmpty(o.Marca) ? o.Marca : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Carro", !String.IsNullOrEmpty(o.Carro) ? o.Carro : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Placa", !String.IsNullOrEmpty(o.Placa) ? o.Placa : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Cor", !String.IsNullOrEmpty(o.Cor) ? o.Cor : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Obs", !String.IsNullOrEmpty(o.Obs) ? o.Obs : (object)DBNull.Value));

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
        public void Insert(Avulso o)
        {
            String sql = "INSERT INTO Avulso (Nome, CPF, TelefoneFixo, TelefoneCelular, " +
                         "Marca, Carro, Placa, Cor, Obs)" +
                         " VALUES (@Nome, @CPF, @TelefoneFixo, @TelefoneCelular, " +
                         "@Marca, @Carro, @Placa, @Cor, @Obs);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdAvulso = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Avulso o)
        {
            String sql = "UPDATE Avulso SET Nome = @Nome, CPF = @CPF, " + 
                         "TelefoneFixo = @TelefoneFixo, TelefoneCelular = @TelefoneCelular, " +
                         "Marca = @Marca, Carro = @Carro, Placa = @Placa, Cor = @Cor, Obs = @Obs " +
                         "WHERE IdAvulso = @IdAvulso ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Avulso o)
        {
            String sql = "DELETE";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdAvulso", o.IdAvulso));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Avulso o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdAvulso = Convert.ToInt32(dr["IdAvulso"]);
            if (dr["Nome"] != DBNull.Value)
                o.Nome = Convert.ToString(dr["Nome"]);
            if (dr["CPF"] != DBNull.Value)
                o.CPF = Convert.ToString(dr["CPF"]);
            if (dr["TelefoneFixo"] != DBNull.Value)
                o.TelefoneFixo = Convert.ToString(dr["TelefoneFixo"]);
            if (dr["TelefoneCelular"] != DBNull.Value)
                o.TelefoneCelular = Convert.ToString(dr["TelefoneCelular"]);
            if (dr["Marca"] != DBNull.Value)
                o.Marca = Convert.ToString(dr["Marca"]);
            if (dr["Carro"] != DBNull.Value)
                o.Carro = Convert.ToString(dr["Carro"]);
            if (dr["Placa"] != DBNull.Value)
                o.Placa = Convert.ToString(dr["Placa"]);
            if (dr["Cor"] != DBNull.Value)
                o.Cor = Convert.ToString(dr["Cor"]);
            if (dr["Obs"] != DBNull.Value)
                o.Obs = Convert.ToString(dr["Obs"]);
            o.Persisted = true;
        }
        public override void CompleteObject(Avulso o)
        {
            using (var dr = GetObjectDataReader(o.IdAvulso))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idAvulso)
        {
            String sql = "SELECT * FROM Avulso WHERE IdAvulso = @IdAvulso ";

            var parms = new MySqlParameter("@IdAvulso", idAvulso);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Avulso GetObject(int idAvulso)
        {
            using (var dr = GetObjectDataReader(idAvulso))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Avulso ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Avulso " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Avulso> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Avulso> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
