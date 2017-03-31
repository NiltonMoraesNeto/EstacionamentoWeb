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
    public class MensalistaDal : DALBase<Mensalista>
    {
        protected DataAccessLayer DAL;

        public MensalistaDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Mensalista o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdMensalista", o.IdMensalista));
            parms.Add(new MySqlParameter("@Nome", !String.IsNullOrEmpty(o.Nome) ? o.Nome : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@CPF", !String.IsNullOrEmpty(o.CPF) ? o.CPF : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataNascimento", o.DataNascimento.HasValue ? o.DataNascimento : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@TelefoneFixo", !String.IsNullOrEmpty(o.TelefoneFixo) ? o.TelefoneFixo : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@TelefoneCelular", !String.IsNullOrEmpty(o.TelefoneCelular) ? o.TelefoneCelular : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@CEP", !String.IsNullOrEmpty(o.CEP) ? o.CEP : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Rua", !String.IsNullOrEmpty(o.Rua) ? o.Rua : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Bairro", !String.IsNullOrEmpty(o.Bairro) ? o.Bairro : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Cidade", !String.IsNullOrEmpty(o.Cidade) ? o.Cidade : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Estado", !String.IsNullOrEmpty(o.Estado) ? o.Estado : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Complemento", !String.IsNullOrEmpty(o.Complemento) ? o.Complemento : (object)DBNull.Value));




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
        public void Insert(Mensalista o)
        {
            String sql = "INSERT INTO Mensalista (Nome, CPF, DataNascimento, TelefoneFixo, TelefoneCelular, CEP, " +
                         "Rua, Bairro, Cidade, Estado, Complemento)" +
                         " VALUES (@Nome, @CPF, @DataNascimento, @TelefoneFixo, @TelefoneCelular, @CEP, " +
                         "@Rua, @Bairro, @Cidade, @Estado, @Complemento);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdMensalista = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(Mensalista o)
        {
            String sql = "UPDATE Mensalista SET Nome = @Nome, CPF = @CPF, DataNascimento = @DataNascimento, " + 
                         "TelefoneFixo = @TelefoneFixo, TelefoneCelular = @TelefoneCelular, CEP = @CEP, " +
                         "Rua = @Rua, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Complemento = @Complemento" +
                         "WHERE IdMensalista = @IdMensalista ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Mensalista o)
        {
            String sql = "DELETE";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdMensalista", o.IdMensalista));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, Mensalista o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdMensalista = Convert.ToInt32(dr["IdMensalista"]);
            if (dr["Nome"] != DBNull.Value)
                o.Nome = Convert.ToString(dr["Nome"]);
            if (dr["DataNascimento"] != DBNull.Value)
                o.DataNascimento = Convert.ToDateTime(dr["DataNascimento"]);

            o.Persisted = true;
        }
        public override void CompleteObject(Mensalista o)
        {
            using (var dr = GetObjectDataReader(o.IdMensalista))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idMensalista)
        {
            String sql = "SELECT * FROM Mensalista WHERE IdMensalista = @IdMensalista ";

            var parms = new MySqlParameter("@IdMensalista", idMensalista);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Mensalista GetObject(int idMensalista)
        {
            using (var dr = GetObjectDataReader(idMensalista))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Mensalista ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM Mensalista " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Mensalista> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<Mensalista> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
