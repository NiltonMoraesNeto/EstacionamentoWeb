using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZTC.Model;
using ZTC.DAL;
using ZTC.Models.ENUMS;
using InterfaceConexao.DAL;
using MySql.Data.MySqlClient;

namespace ZTC.Dal
{
    public class UsuariosDal : DALBase<Usuarios>
    {
        protected DataAccessLayer DAL;

        public UsuariosDal(DataAccessLayer dal)
        {
            DAL = dal;
        }

        private List<MySqlParameter> GetParameters(Usuarios o)
        {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdUsuario", o.IdUsuario));
            parms.Add(new MySqlParameter("@IdPerfil", o.Perfil.IdPerfil));
            parms.Add(new MySqlParameter("@NomeUsuario", !String.IsNullOrEmpty(o.NomeUsuario) ? o.NomeUsuario : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Senha", !String.IsNullOrEmpty(o.Senha) ? o.Senha : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Status", o.Status));
            parms.Add(new MySqlParameter("@Email", !String.IsNullOrEmpty(o.Email) ? o.Email : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@CPF", !String.IsNullOrEmpty(o.CPF) ? o.CPF : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Excluido", o.Excluido));


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
        public void Insert(Usuarios o)
        {
            String sql = "INSERT INTO Usuarios (IdPerfil, NomeUsuario, Senha, Email, CPF)" +
                         " VALUES (@IdPerfil, @NomeUsuario, @Senha, @Email, @CPF);" +
                         "Select LAST_INSERT_ID();";

            var parms = GetParameters(o);
            o.IdUsuario = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;
        }
        public void Update(Usuarios o)
        {
            String sql = "UPDATE Usuarios SET IdPerfil = @IdPerfil, NomeUsuario = @NomeUsuario, " +
                         "Email = @Email, CPF = @CPF WHERE IdUsuario = @IdUsuario ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        public void UpdatePassword(Usuarios o)
        {
            String sql = "UPDATE Usuarios SET Senha = @Senha " +
                         "WHERE IdUsuario = @IdUsuario ";

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        //public void Delete(Usuarios o)
        //{
        //    //String sql = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario ";
        //    //var parms = new MySqlParameter("@IdUsuario", o.IdUsuario);
        //    //DAL.ExecuteNonQuery(sql, CommandType.Text, parms);


        //    //String sql = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario ";
        //    //var parms = (new MySqlParameter("@IdUsuario", o.IdUsuario));
        //    String sql = "UPDATE Usuarios SET  Excluido = @Excluido, Status = @Status WHERE IdUsuario = @IdUsuario ";
            
        //    var parms = new List<MySqlParameter>();

        //    parms.Add(new MySqlParameter("@IdUsuario", o.IdUsuario));
        //    parms.Add(new MySqlParameter("@Excluido", o.Excluido));
        //    parms.Add(new MySqlParameter("@Status", o.Status));

        //    DAL.ExecuteNonQuery(sql, CommandType.Text, parms);

        //}

        


        protected override void LoadObjectInternal(IDataReader dr, Usuarios o)
        {
            if (dr == null) throw new ArgumentNullException("dr");

            o.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            o.Perfil = new Usuariosperfis(Convert.ToInt32(dr["IdPerfil"]));
            if (dr["NomeUsuario"] != DBNull.Value)
                o.NomeUsuario = Convert.ToString(dr["NomeUsuario"]);
            if (dr["Senha"] != DBNull.Value)
                o.Senha = Convert.ToString(dr["Senha"]);
            if (dr["Status"] != DBNull.Value)
                o.Status = (StatusUsuarioLicenca)Convert.ToInt32(dr["Status"]);
            if (dr["Email"] != DBNull.Value)
                o.Email = Convert.ToString(dr["Email"]);
            if (dr["CPF"] != DBNull.Value)
                o.CPF = Convert.ToString(dr["CPF"]);

            o.Excluido = Convert.ToBoolean(dr["Excluido"]);

            o.Persisted = true;
        }
        public override void CompleteObject(Usuarios o)
        {
            using (var dr = GetObjectDataReader(o))
            {
                LoadObject(dr, o);
            }
        }
        public void Login(Usuarios o)
        {
            using (var dr = GetLoginDataReader(o))
            {
                LoadObject(dr, o);
            }
        }

        private IDataReader GetLoginDataReader(Usuarios o)
        {
            //String sql = "SELECT * FROM Usuarios WHERE EmailNotificacao = @EmailNotificacao ";
            String sql = "SELECT * FROM Usuarios WHERE CPF = @CPF ";

            var parms = new MySqlParameter("@CPF", o.CPF);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        private IDataReader GetObjectDataReader(Usuarios o)
        {
            String sql = "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario ";

            var parms = new MySqlParameter("@IdUsuario", o.IdUsuario);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Usuarios GetObject(Usuarios o)
        {
            using (var dr = GetObjectDataReader(o))
            {
                return ConvertToObject(dr);
            }
        }

        private IDataReader GetObjectDataReader(string cpf)
        {
            //String sql = "SELECT * FROM Usuarios WHERE EmailNotificacao = @EmailNotificacao ";
            String sql = "SELECT * FROM Usuarios WHERE CPF = @CPF ";

            var parms = new MySqlParameter("@CPF", cpf);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
        }
        public Usuarios GetObject(string cpf)
        {
            using (var dr = GetObjectDataReader(cpf))
            {
                return ConvertToObject(dr);
            }
        }

        //private IDataReader GetListDataReader(string conditions)
        //{


        //    String sql = "SELECT * FROM " +
        //                "FROM `clarodb_fator`.`usuarios` u " +
        //                "INNER JOIN `clarodb_fator`.`usuarios` uSup ON  uSup.`IdUsuario` = u.`IdUsuario`" +
        //                "WHERE u.`IdUsuario` = u.`IdUsuario`" +
        //                conditions;

        //    return DAL.ExecuteDataReader(sql, CommandType.Text);
        //}


        private IDataReader GetListDataReader(string conditions = null)
        {
            String sql = "SELECT * FROM Usuarios " + conditions;

            return DAL.ExecuteDataReader(sql, CommandType.Text);
        }
        public List<Usuarios> GetList(string conditions = null)
        {
            using (var dr = GetListDataReader(conditions))
            {
                return ConvertToList(dr);
            }
        }
    }
}
