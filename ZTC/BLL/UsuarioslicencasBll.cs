using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZTC.BLL;
using ZTC.Model;
using ZTC.Dal;
using ZTC.Models;
using InterfaceConexao.DAL;


namespace ZTC.Bll
{
    public class UsuarioslicencasBll : BLLBase<Usuarioslicencas>
    {
        protected DataAccessLayer DAL;
        public void Save(Usuarioslicencas o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

                try
                {
                    if (!o.Persisted)
                    {
                        o.Licencas = SessionContext.UsuarioLogado.Licencas;
                        //o.Autor = o.Editor = o.Operador = SessionContext.UsuarioLogado.Usuarios;
                        dao.Insert(o);
                    }
                    else
                    {
                        o.Licencas = SessionContext.UsuarioLogado.Licencas;
                        //o.Editor = o.Operador = SessionContext.UsuarioLogado.Usuarios;
                        dao.Update(o);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(Usuarioslicencas o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

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

        public void GetObjectIdUsuario(int idusuario)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

                try
                {
                    dao.GetObjectIdUsuario(idusuario);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Usuarioslicencas GetObject(Usuarioslicencas o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

                try
                {
                    dal.OpenConnection();
                    var usuarioslicencas = dao.GetObject(o);
                    CompleteRelatedObjects(usuarioslicencas, dal);
                    return usuarioslicencas;
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

        public Usuarioslicencas GetObject(Usuarios o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

                try
                {
                    dal.OpenConnection();
                    var usuarioslicencas = dao.GetObject(o);
                    CompleteRelatedObjects(usuarioslicencas, dal);
                    return usuarioslicencas;
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

        public List<Usuarioslicencas> GetList()
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuarioslicencasDal(dal);

                try
                {
                    var list = dao.GetList();
                    foreach (var item in list)
                    {
                        CompleteRelatedObjects(item, dal);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public override void CompleteObject(Usuarioslicencas o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("usuarioslicencas");

            var dao = new UsuarioslicencasDal(dal);
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

        protected override void CompleteRelatedObjects(Usuarioslicencas o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;

            if (o.Usuarios != null)
            {
                UsuariosBll bll = new UsuariosBll();
                bll.CompleteObject(o.Usuarios, dal);
            }

            if (o.Licencas != null)
            {
                LicencasBll bll = new LicencasBll();
                bll.CompleteObject(o.Licencas, dal);
            }

        }

      

        
    }
}
