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
    public class UsuariosperfisBll : BLLBase<Usuariosperfis>
    {
        public void Save(Usuariosperfis o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosperfisDal(dal);

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

        public void Delete(Usuariosperfis o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosperfisDal(dal);

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

        public Usuariosperfis GetObject(Usuariosperfis o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosperfisDal(dal);

                try
                {
                    dal.OpenConnection();
                    var usuariosperfis = dao.GetObject(o);
                    CompleteRelatedObjects(usuariosperfis, dal);
                    return usuariosperfis;
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

        public List<Usuariosperfis> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosperfisDal(dal);

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

        public override void CompleteObject(Usuariosperfis o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("usuariosperfis");

            var dao = new UsuariosperfisDal(dal);
            var connOpened = dal.ConnectionOpened;

            try
            {
                dal.OpenConnection(!connOpened);
                dao.CompleteObject(o);
                if(completeRelatedObjects)
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

        protected override void CompleteRelatedObjects(Usuariosperfis o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;

        }
    }
}
