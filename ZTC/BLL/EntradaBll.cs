using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZTC.BLL;
using ZTC.Dal;
using ZTC.Models;
using ZTC.Model;
using InterfaceConexao.DAL;

namespace ZTC.Bll
{
    public class EntradaBll : BLLBase<Entrada>
    {
        public void Save(Entrada o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new EntradaDal(dal);

                try
                {
                    if (!o.Persisted)
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

        public void Delete(Entrada o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new EntradaDal(dal);

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

        public Entrada GetObject(int idEntrada)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new EntradaDal(dal);

                try
                {
                    dal.OpenConnection();
                    var entrada = dao.GetObject(idEntrada);
                    CompleteRelatedObjects(entrada, dal);
                    return entrada;
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

        public List<Entrada> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new EntradaDal(dal);

                try
                {
                    return dao.GetList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<Entrada> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new EntradaDal(dal);

                try
                {
                    var list = dao.GetList(conditions);
                    foreach (Entrada p in list)
                    {
                        CompleteRelatedObjects(p, dal);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public override void CompleteObject(Entrada o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("entrada");

            var dao = new EntradaDal(dal);
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

        protected override void CompleteRelatedObjects(Entrada o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;


        }

    }
}
