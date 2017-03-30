using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZTC.Model;
using ZTC.DAL;
using InterfaceConexao.DAL;
using MySql.Data.MySqlClient;

namespace ZTC.Dal
{
    public class UsuariosperfisDal : DALBase<Usuariosperfis>
    {
        protected DataAccessLayer DAL;

        public UsuariosperfisDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Usuariosperfis o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdPerfil", o.IdPerfil));
            parms.Add(new MySqlParameter("@Nome", !String.IsNullOrEmpty(o.Nome) ? o.Nome : (object)DBNull.Value));


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
        public void Insert(Usuariosperfis o)
        {
            String sql = "INSERT INTO Usuariosperfis (Nome)";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
            o.IdPerfil = Convert.ToInt32(parms[0].Value);
            o.Persisted = true;
        }
        public void Update(Usuariosperfis o)
        {
            String sql = "UPDATE Usuariosperfis SET Nome = @Nome WHERE IdPerfil = @IdPerfil ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        public void Delete(Usuariosperfis o)
        {
            String sql = "DELETE FROM Usuariosperfis WHERE IdPerfil = @IdPerfil ";
            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        protected override void LoadObjectInternal(IDataReader dr, Usuariosperfis o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdPerfil = Convert.ToInt32(dr["IdPerfil"]);

            if (dr["Nome"] != DBNull.Value)
                o.Nome = Convert.ToString(dr["Nome"]);

            o.Persisted = true;
        }
        public override void CompleteObject(Usuariosperfis o)
        {
            using (var dr = GetObjectDataReader(o))
            {
                LoadObject(dr, o);
            }
        }
        private IDataReader GetObjectDataReader(Usuariosperfis o)
        {
            String sql = "SELECT * FROM Usuariosperfis WHERE IdPerfil = @IdPerfil ";

            var parms = (new MySqlParameter("@IdPerfil", o.IdPerfil));
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Usuariosperfis GetObject(Usuariosperfis o)
        {
            using (var dr = GetObjectDataReader(o))
            {
                return ConvertToObject(dr);
            }
        }
        private IDataReader GetListDataReader()
        {
            String sql = "SELECT * FROM Usuariosperfis ";

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Usuariosperfis> GetList()
        {
            using (var dr = GetListDataReader())
            {
                return ConvertToList(dr);
            }
        }
    }
}
