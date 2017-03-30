using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZTC.Bll;
using ZTC.Model;
using ZTC.Models.ENUMS;

namespace ZTC.Models
{
    public class RequiresAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Redireciona caso o usuário não esteja autenticado
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Usar a url corrente para o returnUrl
                string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;

                //Em teste, não tenho certeza se este TempData vai funcionar <span class="wp-smiley wp-emoji wp-emoji-uneasy" title=":\">:\</span>
                //filterContext.Controller.TempData["msg"] = "Sistema finalizado por inatividade!";

                SessionContext.UsuarioLogado = null;

                //Enviar o usuário de volta à página de login
                string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                string loginUrl = "/Usuarios/Login" + redirectUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }

            else
            {
                if (SessionContext.UsuarioLogado == null)
                {
                    var email = filterContext.HttpContext.User.Identity.Name.ToString();
                    var usuario = new UsuariosBll().GetObject(email);
                    SessionContext.UsuarioLogado = new Usuarioslicencas() { Usuarios = usuario };
                }
            }
        }
    }
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Usuarios/Login");
                return;
            }
            else
            {
                if (SessionContext.UsuarioLogado == null)
                {
                    var cpf = filterContext.HttpContext.User.Identity.Name.ToString();
                    var usuario = new UsuariosBll().GetObject(cpf);

                    var licencaBll = new UsuarioslicencasBll();
                    var licenca = new Usuarioslicencas();

                    licenca = licencaBll.GetObject(usuario);

                    //if (usuario.Perfil.Nome.ToLower() == "desenvolvedor")
                    //{
                    //    licenca = new Usuarioslicencas()
                    //    {
                    //        Funcao = new Licencasfuncoes() { Nome = "Desenvolvedor" },
                    //        Status = StatusUsuarioLicenca.Ativo,
                    //        Licencas = new Licencas() { Descricao = "Desenvolvedor" },
                    //        Usuarios = usuario
                    //    };
                    //}
                    //else
                    //{
                    //    licenca = licencaBll.GetObject(usuario);
                    //}


                    SessionContext.UsuarioLogado = licenca;
                }
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Usuarios/Denied");
            }
        }
    }
}