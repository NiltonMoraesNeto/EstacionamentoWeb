using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using ZTC.Bll;
using ZTC.Model;
using ZTC.BLL;
using ZTC.Models.StaticList;
using InterfaceConexao.DAL;

namespace ZTC
{

    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DatabaseConnection.ConnectionString =
              WebConfigurationManager.AppSettings["ConnectionString"];
            DatabaseConnection.TimeOut =
                Convert.ToInt32(WebConfigurationManager.AppSettings["TimeOut"]);
            DatabaseConnection.Type =
                (ConnectionType)Enum.Parse(typeof(ConnectionType),
                                           WebConfigurationManager.AppSettings["Type"]);

            
        }


        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        var formsAuthenticationTicket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                        if (formsAuthenticationTicket != null)
                        {
                            string username = formsAuthenticationTicket.Name;//Cpf
                            string roles = string.Empty;

                            var bll = new UsuariosBll();
                            var usuario = bll.GetObject(username);

                            roles = usuario.Perfil.Nome;

                            //Let us set the Pricipal with our user specific details
                            e.User = new System.Security.Principal.GenericPrincipal(
                                new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                        }
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }

    }


}