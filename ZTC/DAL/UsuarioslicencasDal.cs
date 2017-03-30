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
      public class UsuarioslicencasDal : DALBase<Usuarioslicencas>
      {
         protected DataAccessLayer DAL;

         public UsuarioslicencasDal(DataAccessLayer dal)
         {
            DAL = dal;
         }

         private List<MySqlParameter> GetParameters(Usuarioslicencas o)
         {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdUsuarioLicenca", o.IdUsuarioLicenca));
            parms.Add(new MySqlParameter("@IdUsuario", o.Usuarios != null ? o.Usuarios.IdUsuario : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@IdLicenca", o.Licencas != null ? o.Licencas.IdLicenca : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Status", Convert.ToInt32(o.Status)));

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
         public void Insert(Usuarioslicencas o)
         {
             String sql = "INSERT INTO Usuarioslicencas (IdUsuarioLicenca, IdUsuario, IdLicenca, Status)" +
                 " VALUES (@IdUsuarioLicenca, @IdUsuario, @IdLicenca, @Status);" +
                          "Select LAST_INSERT_ID();";



            //var parms = GetParameters(o);
            //DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
            //o.IdUsuarioLicenca= Convert.ToInt32(parms[0].Value);
            //o.IdUsuarioLicenca = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            //o.Persisted = true;

            var parms = GetParameters(o);
            o.IdUsuarioLicenca = Convert.ToInt32(DAL.ExecuteScalar(sql, CommandType.Text, parms));
            o.Persisted = true;
         }
         public void Update(Usuarioslicencas o)
         {
            String sql = "UPDATE Usuarioslicencas SET IdUsuarioLicenca = @IdUsuarioLicenca, IdUsuario = @IdUsuario, IdLicenca = @IdLicenca, Status = @Status WHERE IdUsuarioLicenca = @IdUsuarioLicenca "; 

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
         }



         public void Delete(Usuarioslicencas o)
         {
            String sql = "DELETE FROM Usuarioslicencas WHERE IdUsuarioLicenca = @IdUsuarioLicenca ";
            var parms = new MySqlParameter("@IdUsuarioLicenca", o.IdUsuarioLicenca);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
         }

         public void GetObjectIdUsuario(int idUsuario)
         {

             String sql = "SELECT * FROM Usuarioslicencas WHERE IdUsuario = @IdUsuario ";
             var parms = new MySqlParameter("@IdUsuario", idUsuario);
             DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
         }

         protected override void LoadObjectInternal(IDataReader dr, Usuarioslicencas o)
         {
            if (dr == null) throw new ArgumentNullException("dr");

               o.IdUsuarioLicenca = Convert.ToInt32(dr["IdUsuarioLicenca"]);
               if(dr["IdUsuario"] != DBNull.Value)
               o.Usuarios = new Usuarios(Convert.ToInt32((dr["IdUsuario"])));
               if(dr["IdLicenca"] != DBNull.Value)
               o.Licencas = new Licencas(Convert.ToInt32((dr["IdLicenca"])));
               if(dr["Status"] != DBNull.Value)
                   o.Status = (StatusUsuarioLicenca)Convert.ToInt32(dr["Status"]);

            o.Persisted = true;
         }
         public override void CompleteObject(Usuarioslicencas o)
         {
            using (var dr = GetObjectDataReader(o))
            {
               LoadObject(dr, o);
            }
         }
         private IDataReader GetObjectDataReader(Usuarioslicencas o)
         {
            String sql = "SELECT * FROM Usuarioslicencas WHERE IdUsuarioLicenca = @IdUsuarioLicenca ";

            var parms = (new MySqlParameter("@IdUsuarioLicenca", o.IdUsuarioLicenca));
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
         }
         public Usuarioslicencas GetObject(Usuarioslicencas o)
         {
            using (var dr = GetObjectDataReader(o))
            {
               return ConvertToObject(dr);
            }
         }

         private IDataReader GetObjectDataReader(Usuarios o)
         {
             String sql = "SELECT * FROM Usuarioslicencas WHERE IdUsuario = @IdUsuario ";

             var parms = (new MySqlParameter("@IdUsuario", o.IdUsuario));
             return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
         }
         public Usuarioslicencas GetObject(Usuarios o)
         {
             using (var dr = GetObjectDataReader(o))
             {
                 return ConvertToObject(dr);
             }
         }
         private IDataReader GetListDataReader()
         {
            String sql = "SELECT * FROM Usuarioslicencas "; 

            return DAL.ExecuteDataReader(sql,CommandType.Text);
         }
         public List<Usuarioslicencas> GetList()
         {
            using (var dr = GetListDataReader())
            {
               return ConvertToList(dr);
            }
         }
      }
}
