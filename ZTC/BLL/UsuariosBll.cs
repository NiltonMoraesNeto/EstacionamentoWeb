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
    public class UsuariosBll : BLLBase<Usuarios>
    {
        public void Save(Usuarios o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosDal(dal);
                
                
                try
                {
                    if (!o.Persisted)
                    {
                        //o.SenhaExpirada = DateTime.Now.AddDays(o.ValidadeSenha);
                        o.Senha = o.GetMD5Hash();
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

        //public void UpdatePassword(Usuarios o)
        //{
        //    using (var dal = DatabaseConnection.GetDataAccessLayer())
        //    {
        //        var dao = new UsuariosDal(dal);

        //        try
        //        {
        //            o.Senha = o.GetMD5Hash();
        //            dao.UpdatePassword(o);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        //public void Delete(Usuarios o)
        //{
        //    using (var dal = DatabaseConnection.GetDataAccessLayer())
        //    {
        //        var dao = new UsuariosDal(dal);

        //        try
        //        {
        //            dao.Delete(o);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        public Usuarios GetObject(Usuarios o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosDal(dal);

                try
                {
                    dal.OpenConnection();
                    var usuarios = dao.GetObject(o);
                    CompleteRelatedObjects(usuarios, dal);
                    return usuarios;
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
        public Usuarios GetObject(string cpf)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosDal(dal);

                try
                {
                    dal.OpenConnection();
                    var usuarios = dao.GetObject(cpf);
                    CompleteRelatedObjects(usuarios, dal);
                    return usuarios;
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
        public List<Usuarios> GetList(string conditions = null, bool completeRelatedObjects = true)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosDal(dal);

                try
                {
                    var list = dao.GetList(conditions).OrderBy(o=>o.NomeUsuario).Where(o=>o.Excluido == false).ToList();
                    foreach (Usuarios u in list)
                    {
                        if(completeRelatedObjects)
                            CompleteRelatedObjects(u, dal);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public override void CompleteObject(Usuarios o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null) throw new ArgumentNullException("usuarios");

            var dao = new UsuariosDal(dal);
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

        protected override void CompleteRelatedObjects(Usuarios o, DataAccessLayer dal, bool completeRelatedObjects = true)
        {
            if (o == null)
                return;
            if (o.Perfil != null)
            {
                UsuariosperfisBll bll = new UsuariosperfisBll();
                bll.CompleteObject(o.Perfil, dal, false);
            }
            if (o.IdUsuarioLicenca != null)
            {
                UsuarioslicencasBll bll = new UsuarioslicencasBll();
                bll.CompleteObject(o.IdUsuarioLicenca, dal, false);
            }
        }

        public Usuarios Login(Usuarios usuario)
        {

            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                var dao = new UsuariosDal(dal);

                try
                {
                    var usuarioLogin = new Usuarios();
                    usuarioLogin.Senha = usuario.Senha;

                    dao.Login(usuario);
                    if (usuario.IdUsuario > 1 && usuario.Senha == usuarioLogin.Senha)
                        //if (usuario.IdUsuario > 1 && usuario.Senha == usuarioLogin.GetMD5Hash())
                        {
                        CompleteRelatedObjects(usuario, dal);
                        
                    }
                    else
                    {
                        throw new Exception("CPF ou senha incorretos!");
                    }

                    return usuario;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public Usuarios LoginRecuperarSenha(Usuarios usuario)
        //{

        //    using (var dal = DatabaseConnection.GetDataAccessLayer())
        //    {
        //        var dao = new UsuariosDal(dal);

        //        try
        //        {
        //            var usuarioLogin = new Usuarios {Senha = usuario.Senha};

        //            dao.Login(usuario);
                    
        //            return usuario;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
    }
}
