using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTC.Model;
using ZTC.Models.Extensions;
//using ZTC.ViewModel;
using ZTC.Models;

namespace ZTC.CustomHtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString RadioAtividadePadrao(this HtmlHelper helper, int selected)
        {
            var divMid = new TagBuilder("div");
            divMid.AddCssClass("mid");

            var divNavMidId4 = DivNavMid("4", "navmid acao " + (selected == 4 ? "checked" : ""), "border-radius: 3px 0px 0px 3px;", "glyphicon-transfer",
                "Retorno");
            var divNavMidId3 = DivNavMid("3", "navmid acao " + (selected == 3 ? "checked" : ""), "", "glyphicon-earphone", "Contato");

            var divNavMidId6 = DivNavMid("6", "navmid acao " + (selected == 6 ? "checked" : ""), "", "glyphicon-list-alt", "Tarefa");

            var divNavMidId7 = DivNavMid("7", "navmid acao " + (selected == 7 ? "checked" : ""), "", "glyphicon-time", "Compromisso");

            var divNavMidId5 = DivNavMid("5", "navmid acao " + (selected == 5 ? "checked" : ""), "border-radius: 0px 3px 3px 0px;", "glyphicon-user", "Visita");

            var divNavMidId8 = DivNavMid("8", "navmid acao " + (selected == 8 ? "checked" : ""), "border-radius: 0px 3px 3px 0px;", "glyphicon-usd",
                          "Proposta");

            divMid.InnerHtml += divNavMidId4.ToString(TagRenderMode.Normal);
            divMid.InnerHtml += divNavMidId3.ToString(TagRenderMode.Normal);
            divMid.InnerHtml += divNavMidId6.ToString(TagRenderMode.Normal);
            divMid.InnerHtml += divNavMidId7.ToString(TagRenderMode.Normal);
            divMid.InnerHtml += divNavMidId5.ToString(TagRenderMode.Normal);
            divMid.InnerHtml += divNavMidId8.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(divMid.ToString(TagRenderMode.Normal));

        }

        private static TagBuilder DivNavMid(string id, string _class, string style, string icon, string name)
        {
            var divNavMid = new TagBuilder("div");
            divNavMid.Attributes.Add("id", id);
            divNavMid.AddCssClass(_class);
            divNavMid.Attributes.Add("style", style);

            var i = new TagBuilder("i");
            i.AddCssClass("glyphicon " + icon);

            var span = new TagBuilder("span");
            span.SetInnerText(name);

            divNavMid.InnerHtml += i.ToString(TagRenderMode.Normal);
            divNavMid.InnerHtml += span.ToString(TagRenderMode.Normal);

            return divNavMid;
        }



        public static MvcHtmlString MenuSistema(this HtmlHelper helper)
        {
            int tipoUsuario = 0;
            if (SessionContext.UsuarioLogado != null)
            {
                if (SessionContext.UsuarioLogado.Usuarios != null)
                {
                    if (SessionContext.UsuarioLogado.Usuarios.Perfil != null)
                    {
                        tipoUsuario = SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil;
                    }
                }
            }



            //INICIO
            var aside = new TagBuilder("aside");
            var sidebar = new TagBuilder("div");
            sidebar.Attributes.Add("id", "sidebar");
            sidebar.AddCssClass("nav-collapse");

            var sidebar_menu = new TagBuilder("ul");
            sidebar_menu.AddCssClass("sidebar-menu");
            var sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            var tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            //INICIO MENU **CADASTROS**
            var tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_document_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            var span = new TagBuilder("span");
            span.InnerHtml += "Cadastros";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            var sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            var li_sub = new TagBuilder("li");
            var tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Avulso/Index/");
            tagA_sub.InnerHtml += "Avulso";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/Mensalista/Index/");
            tagA_sub.InnerHtml += "Mensalista";

            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PROSPECÇÃO**


            sidebar.InnerHtml += sidebar_menu.ToString(TagRenderMode.Normal);
            aside.InnerHtml += sidebar.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(aside.ToString(TagRenderMode.Normal));
            //FIM
        }

        private static string HasDate(DateTime? date)
        {
            string d = "";
            if (date.HasValue)
                d = date.Value.ToShortDateString();

            return d;
        }

        private static string MoneyBR(decimal valor)
        {
            string d = "";
            d = valor.ToBrDecimal();

            return "R$ " + d;
        }


    }
}