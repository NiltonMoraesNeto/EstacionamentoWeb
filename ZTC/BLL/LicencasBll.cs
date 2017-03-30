using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZTC.BLL;
using ZTC.Model;
using ZTC.Dal;

using InterfaceConexao.DAL;

namespace ZTC.Bll
{
      public class LicencasBll : BLLBase<Licencas>
      {
         public void Save(Licencas o)
         {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
               var dao = new LicencasDal(dal);

               try
               {
                  if(!o.Persisted)
                  {
                     dao.Insert(o);
                  }
                  else
                  {
                     dao.Update(o);
                  }
               }
               catch (Exception ex)
               {
                  throw ex;
               }
            }
         }

         public void Delete(Licencas o)
         {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
               var dao = new LicencasDal(dal);

               try
               {
                  dao.Delete(o);
               }
               catch (Exception ex)
               {
                  throw ex;
               }
            }
         }

         public Licencas GetObject(Licencas o)
         {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
               var dao = new LicencasDal(dal);

               try
               {
                  dal.OpenConnection();
                  var licencas = dao.GetObject(o);
                  CompleteRelatedObjects(licencas, dal);
                  return licencas;
               }
               catch (Exception ex)
               {
                  throw ex;
               }
               finally
               {
                  dal.CloseConnection();
               }
            }
         }

         public List<Licencas> GetList()
         {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
               var dao = new LicencasDal(dal);

               try
               {
                   var list = dao.GetList();
                   foreach (Licencas l in list)
                   {
                       CompleteRelatedObjects(l,dal);
                   }
                   return list;
               }
               catch (Exception ex)
               {
                  throw ex;
               }
            }
         }

         public override void CompleteObject(Licencas o, DataAccessLayer dal, bool completeRelatedObjects = true)
         {
            if(o == null) throw new ArgumentNullException("licencas");

            var dao = new LicencasDal(dal);
            var connOpened = dal.ConnectionOpened;

            try
            {
               dal.OpenConnection(!connOpened);
               dao.CompleteObject(o);
               CompleteRelatedObjects(o, dal);
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               dal.CloseConnection(!connOpened);
            }
         }

         protected override void CompleteRelatedObjects(Licencas o, DataAccessLayer dal, bool completeRelatedObjects = true)
         {
            if(o == null)
               return;
         }
      }
}
