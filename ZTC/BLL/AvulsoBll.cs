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
    public class AvulsoBll : BLLBase<Avulso>
    {
        public void Save(Avulso o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new AvulsoDal(dal);

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

        public void Delete(Avulso o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new AvulsoDal(dal);

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

        public Avulso GetObject(int idAvulso)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new AvulsoDal(dal);

                try
                {
                    dal.OpenConnection();
                    var avulso = dao.GetObject(idAvulso);
                    CompleteRelatedObjects(avulso, dal);
                    return avulso;
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

        public List<Avulso> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new AvulsoDal(dal);

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


        public List<Avulso> GetList(string conditions = null, bool completeRelatedObjects = false)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new AvulsoDal(dal);

                try
                {
                    var list = dao.GetList(conditions);
                    foreach (Avulso p in list)
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


        public override void CompleteObject(Avulso o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("avulso");

            var dao = new AvulsoDal(dal);
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

        protected override void CompleteRelatedObjects(Avulso o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;


        }

    }
}
