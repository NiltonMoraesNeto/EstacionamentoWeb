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
    public class ServicoMensalistaDal : DALBase<ServicoMensalista>
    {
        protected DataAccessLayer DAL;

        public ServicoMensalistaDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(ServicoMensalista o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdServicoMensalista", o.IdServicoMensalista));
            parms.Add(new MySqlParameter("@Nome", !String.IsNullOrEmpty(o.Nome) ? o.Nome : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Placa", !String.IsNullOrEmpty(o.Placa) ? o.Placa : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Carro", !String.IsNullOrEmpty(o.Carro) ? o.Carro : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Servico1", !String.IsNullOrEmpty(o.Servico1) ? o.Servico1 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Observacao1", !String.IsNullOrEmpty(o.Observacao1) ? o.Observacao1 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Servico2", !String.IsNullOrEmpty(o.Servico2) ? o.Servico2 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Observacao2", !String.IsNullOrEmpty(o.Observacao2) ? o.Observacao2 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Servico3", !String.IsNullOrEmpty(o.Servico3) ? o.Servico3 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Observacao3", !String.IsNullOrEmpty(o.Observacao3) ? o.Observacao3 : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataHoraEntrada", o.DataHoraEntrada.HasValue ? o.DataHoraEntrada : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@DataHoraSaida", o.DataHoraSaida.HasValue ? o.DataHoraSaida : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@ValorServico", o.ValorServico));
            parms.Add(new MySqlParameter("@ValorHora", o.ValorHora));
            parms.Add(new MySqlParameter("@TotalHoras", !String.IsNullOrEmpty(o.TotalHoras) ? o.TotalHoras : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@ValorTotal", o.ValorTotal));
            parms.Add(new MySqlParameter("@FormaPagamento", !String.IsNullOrEmpty(o.FormaPagamento) ? o.FormaPagamento : (object)DBNull.Value));
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
        public void Insert(ServicoMensalista o)
        {
            String sql = "INSERT INTO ServicoMensalista (Nome, Placa, Carro, Servico1, Observacao1, Servico2, Observacao2, " +
                         "Servico3, Observacao3, DataHoraEntrada, DataHoraSaida, " +
                         "ValorServico, ValorHora, TotalHoras, ValorTotal, FormaPagamento, Fechado)" +
                         " VALUES (@Nome, @Placa, @Carro, @Servico1, @Observacao1, @Servico2, @Observacao2, " +
                         "@Servico3, @Observacao3, @DataHoraEntrada, @DataHoraSaida, " +
                         "@ValorServico, @ValorHora, @TotalHoras, @ValorTotal, @FormaPagamento, @Fechado);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdServicoMensalista = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;

        }
        public void Update(ServicoMensalista o)
        {
            String sql = "UPDATE ServicoMensalista SET Nome = @Nome, Placa = @Placa, " +
                         "Carro = @Carro, Servico1 = @Servico1, Observacao1 = @Observacao1, " +
                         "Servico2 = @Servico2, Observacao2 = @Observacao2, Servico3 = @Servico3, Observacao3 = @Observacao3," +
                         "DataHoraEntrada = @DataHoraEntrada, DataHoraSaida = @DataHoraSaida, ValorServico = @ValorServico, " +
                         "ValorHora = @ValorHora, TotalHoras = @TotalHoras, ValorTotal = @ValorTotal, " +
                         "FormaPagamento = @FormaPagamento, Fechado = @Fechado " +
                         "WHERE IdServicoMensalista = @IdServicoMensalista ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(ServicoMensalista o)
        {
            String sql = "DELETE";
            DAL.ExecuteNonQuery(sql, CommandType.Text, new MySqlParameter("@IdServicoMensalista", o.IdServicoMensalista));
            o.Persisted = false;
        }

        protected override void LoadObjectInternal(IDataReader dr, ServicoMensalista o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdServicoMensalista = Convert.ToInt32(dr["IdServicoMensalista"]);
            if (dr["Nome"] != DBNull.Value)
                o.Nome = Convert.ToString(dr["Nome"]);
            if (dr["Placa"] != DBNull.Value)
                o.Placa = Convert.ToString(dr["Placa"]);
            if (dr["Carro"] != DBNull.Value)
                o.Carro = Convert.ToString(dr["Carro"]);
            if (dr["Servico1"] != DBNull.Value)
                o.Servico1 = Convert.ToString(dr["Servico1"]);
            if (dr["Observacao1"] != DBNull.Value)
                o.Observacao1 = Convert.ToString(dr["Observacao1"]);
            if (dr["Servico2"] != DBNull.Value)
                o.Servico2 = Convert.ToString(dr["Servico2"]);
            if (dr["Observacao2"] != DBNull.Value)
                o.Observacao2 = Convert.ToString(dr["Observacao2"]);
            if (dr["Servico3"] != DBNull.Value)
                o.Servico3 = Convert.ToString(dr["Servico3"]);
            if (dr["Observacao3"] != DBNull.Value)
                o.Observacao3 = Convert.ToString(dr["Observacao3"]);
            if (dr["DataHoraEntrada"] != DBNull.Value)
                o.DataHoraEntrada = Convert.ToDateTime(dr["DataHoraEntrada"]);
            if (dr["DataHoraSaida"] != DBNull.Value)
                o.DataHoraSaida = Convert.ToDateTime(dr["DataHoraSaida"]);
            if (dr["ValorServico"] != DBNull.Value)
                o.ValorServico = Convert.ToDecimal(dr["ValorServico"]);
            if (dr["ValorHora"] != DBNull.Value)
                o.ValorHora = Convert.ToDecimal(dr["ValorHora"]);
            if (dr["TotalHoras"] != DBNull.Value)
                o.TotalHoras = Convert.ToString(dr["TotalHoras"]);
            if (dr["ValorTotal"] != DBNull.Value)
                o.ValorTotal = Convert.ToDecimal(dr["ValorTotal"]);
            if (dr["FormaPagamento"] != DBNull.Value)
                o.FormaPagamento = Convert.ToString(dr["FormaPagamento"]);
            if (dr["Fechado"] != DBNull.Value)
                o.Fechado = Convert.ToBoolean(dr["Fechado"]); o.Persisted = true;
        }
        public override void CompleteObject(ServicoMensalista o)
        {
            using (var dr = GetObjectDataReader(o.IdServicoMensalista))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(int idServicoMensalista)
        {
            String sql = "SELECT * FROM ServicoMensalista WHERE IdServicoMensalista = @IdServicoMensalista ";

            var parms = new MySqlParameter("@IdServicoMensalista", idServicoMensalista);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public ServicoMensalista GetObject(int idServicoMensalista)
        {
            using (var dr = GetObjectDataReader(idServicoMensalista))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM ServicoMensalista ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }

        private IDataReader GetListDataReader(string conditions)
        {
            String sql = "SELECT * FROM ServicoMensalista " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<ServicoMensalista> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
        public List<ServicoMensalista> GetList(string conditions)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
