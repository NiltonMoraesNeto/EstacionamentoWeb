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
      public class LicencasDal : DALBase<Licencas>
      {
         protected DataAccessLayer DAL;

         public LicencasDal(DataAccessLayer dal)
         {
            DAL = dal;
         }

         private List<MySqlParameter> GetParameters(Licencas o)
         {
            var parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@IdLicenca", o.IdLicenca));
            parms.Add(new MySqlParameter("@Descricao", !String.IsNullOrEmpty(o.Descricao) ? o.Descricao : (object)DBNull.Value));
            parms.Add(new MySqlParameter("@Status", o.Status));

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
         public void Insert(Licencas o)
         {
            String sql = "INSERT INTO Licencas (Descricao, Status) VALUES (@Descricao, @Status)"; 

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
            o.IdLicenca= Convert.ToInt32(parms[0].Value);
            o.Persisted = true;
         }
         public void Update(Licencas o)
         {
            String sql = "UPDATE Licencas SET Descricao = @Descricao, Status = @Status WHERE IdLicenca = @IdLicenca "; 

            var parms = GetParameters(o);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
         }
         public void Delete(Licencas o)
         {
            String sql = "DELETE FROM Licencas WHERE IdLicenca = @IdLicenca ";
            var parms = new MySqlParameter("@IdLicenca", o.IdLicenca);
            DAL.ExecuteNonQuery(sql, CommandType.Text, parms);
         }

         protected override void LoadObjectInternal(IDataReader dr, Licencas o)
         {
            if (dr == null) throw new ArgumentNullException("dr");

               o.IdLicenca = Convert.ToInt32(dr["IdLicenca"]);
               if(dr["Descricao"] != DBNull.Value)
                  o.Descricao = Convert.ToString(dr["Descricao"]);
               if(dr["Status"] != DBNull.Value)
                   o.Status = (StatusLicenca)Convert.ToInt32(dr["Status"]);

            o.Persisted = true;
         }
         public override void CompleteObject(Licencas o)
         {
            using (var dr = GetObjectDataReader(o))
            {
               LoadObject(dr, o);
            }
         }
         private IDataReader GetObjectDataReader(Licencas o)
         {
            String sql = "SELECT * FROM Licencas WHERE IdLicenca = @IdLicenca ";

             var parms = new MySqlParameter("@IdLicenca", o.IdLicenca);
            return DAL.ExecuteDataReader(sql, CommandType.Text, parms);
         }
         public Licencas GetObject(Licencas o)
         {
            using (var dr = GetObjectDataReader(o))
            {
               return ConvertToObject(dr);
            }
         }
         private IDataReader GetListDataReader()
         {
            String sql = "SELECT * FROM Licencas "; 

            return DAL.ExecuteDataReader(sql,CommandType.Text);
         }
         public List<Licencas> GetList()
         {
            using (var dr = GetListDataReader())
            {
               return ConvertToList(dr);
            }
         }
      }
}
