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
    public class ServicoMensalistaBll : BLLBase<ServicoMensalista>
    {
        public void Save(ServicoMensalista o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ServicoMensalistaDal(dal);

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

        public void Delete(ServicoMensalista o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ServicoMensalistaDal(dal);

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

        public ServicoMensalista GetObject(int idServicoMensalista)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ServicoMensalistaDal(dal);

                try
                {
                    dal.OpenConnection();
                    var servicoMensalista = dao.GetObject(idServicoMensalista);
                    CompleteRelatedObjects(servicoMensalista, dal);
                    return servicoMensalista;
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

        public List<ServicoMensalista> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ServicoMensalistaDal(dal);

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


        public List<ServicoMensalista> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new ServicoMensalistaDal(dal);

                try
                {
                    var list = dao.GetList(conditions);
                    foreach (ServicoMensalista p in list)
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


        public override void CompleteObject(ServicoMensalista o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("servicomensalista");

            var dao = new ServicoMensalistaDal(dal);
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

        protected override void CompleteRelatedObjects(ServicoMensalista o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;


        }

    }
}
