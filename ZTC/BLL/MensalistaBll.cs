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
    public class MensalistaBll : BLLBase<Mensalista>
    {
        public void Save(Mensalista o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new MensalistaDal(dal);

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

        public void Delete(Mensalista o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new MensalistaDal(dal);

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

        public Mensalista GetObject(int idMensalista)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new MensalistaDal(dal);

                try
                {
                    dal.OpenConnection();
                    var mensalista = dao.GetObject(idMensalista);
                    CompleteRelatedObjects(mensalista, dal);
                    return mensalista;
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

        public List<Mensalista> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new MensalistaDal(dal);

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


        public List<Mensalista> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new MensalistaDal(dal);

                try
                {
                    var list = dao.GetList(conditions);
                    foreach (Mensalista p in list)
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


        public override void CompleteObject(Mensalista o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("mensalista");

            var dao = new MensalistaDal(dal);
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

        protected override void CompleteRelatedObjects(Mensalista o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;


        }

    }
}
