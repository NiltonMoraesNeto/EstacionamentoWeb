using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTC.Model;

namespace ZTC.Models
{
    public static class SessionContext
    {
        /// <summary>
        /// Usuário logado na aplicação.
        /// </summary>
        public static Usuarioslicencas UsuarioLogado
        {
            get
            {
                return (Usuarioslicencas)HttpContext.Current.Session["UsuarioLogadoSistema"];
            }
            set
            {
                HttpContext.Current.Session["UsuarioLogadoSistema"] = value;
            }
        }

    }
}