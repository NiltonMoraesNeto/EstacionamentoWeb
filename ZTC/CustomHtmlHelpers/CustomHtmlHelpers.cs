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

            //INICIO MENU **PROSPECÇÃO**
            var tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_document_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            var span = new TagBuilder("span");
            span.InnerHtml += "Prospecção";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            var sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            var li_sub = new TagBuilder("li");
            var tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/ContatoNovo/Index/");
            tagA_sub.InnerHtml += "Contato Novo";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/AcompanharOportunidades/Index/");
            tagA_sub.InnerHtml += "Oportunidades";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PROSPECÇÃO**

            //INICIO MENU **ATIVIDADES**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_desktop");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Atividades";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Atividades/Create/");
            tagA_sub.InnerHtml += "Novo";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/RetornarContato/Index/");
            tagA_sub.InnerHtml += "Gerenciar";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Calendario/Index/");
            tagA_sub.InnerHtml += "Calendário";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **ATIVIDADES**

            //INICIO MENU **EMPRESA**
            var li = new TagBuilder("li");
            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "/Empresa/Index/");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_wallet");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            tagA.InnerHtml += "Empresa";

            li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
            //FIM MENU **EMPRESA**

            //INICIO MENU **CLASSE CLIENTES**
            //li = new TagBuilder("li");
            //tagA = new TagBuilder("a");
            //tagA.Attributes.Add("href", "/Classeclientes/Index/");

            //tagI = new TagBuilder("i");
            //tagI.AddCssClass("icon_clipboard");
            //tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            //tagA.InnerHtml += "Classe Clientes";

            //li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
            //sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
            //FIM MENU **CLASSE CLIENTES**

            //INICIO MENU **PÓS VENDA**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_archive_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Pós Venda";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/PosVenda/Index/");
            tagA_sub.InnerHtml += "Novo";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/AcompanharPosVenda/Index/");
            tagA_sub.InnerHtml += "Gerenciar";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PÓS VENDA**

            //INICIO MENU **PROPOSTA**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_archive");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Proposta";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Proposta/Index/");
            tagA_sub.InnerHtml += "Gerenciar";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PROPOSTA**

            //INICIO MENU **PROPOSTA**
            //li = new TagBuilder("li");
            //tagA = new TagBuilder("a");
            //tagA.Attributes.Add("href", "/Proposta/Index/");

            //tagI = new TagBuilder("i");
            //tagI.AddCssClass("icon_archive");
            //tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            //tagA.InnerHtml += "Proposta";

            //li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
            //sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
            //FIM MENU **PROPOSTA**

            //INICIO MENU **PEDIDOS**
            //li = new TagBuilder("li");
            //tagA = new TagBuilder("a");
            //tagA.Attributes.Add("href", "/Pedidos/Index/");

            //tagI = new TagBuilder("i");
            //tagI.AddCssClass("icon_pencil-edit_alt");
            //tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            //tagA.InnerHtml += "Pedidos";

            //li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
            //sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
            //FIM MENU **PEDIDOS**


            //INICIO MENU **PEDIDOS**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_pencil-edit_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Pedidos";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Pedidos/Index/");
            tagA_sub.InnerHtml += "Lista de Pedidos";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Acompanhamentopedidos/Index/");
            tagA_sub.InnerHtml += "Acompanhamento";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PEDIDOS**




            if (tipoUsuario == 7)
            {
                //INICIO CLASSE CLIENTES
                li = new TagBuilder("li");
                tagA = new TagBuilder("a");
                tagA.Attributes.Add("href", "/Classeclientes/Index/");

                tagI = new TagBuilder("i");
                tagI.AddCssClass("icon_contacts");
                tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

                tagA.InnerHtml += "Classe Clientes";

                li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
                sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
                //FIM CLASSE CLIENTES
            }
            //INICIO MENU **CADASTROS**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_table");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Cadastros";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            //AVISOS
            if (tipoUsuario < 13)
            {
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Avisos/Index/");
                tagA_sub.InnerHtml += "Avisos";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            }
            //PESSOAS
            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/Pessoa/Index/");
            tagA_sub.InnerHtml += "Pessoas";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            if (tipoUsuario == 7)
            {
                //Pessoa Relação
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/PessoaRelacao/Index/");
                tagA_sub.InnerHtml += "Pessoa Relação";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //USUÁRIOS
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Usuarios/Index/");
                tagA_sub.InnerHtml += "Usuários";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //EQUIPES
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Equipe/Index/");
                tagA_sub.InnerHtml += "Equipes";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //LICENÇAS FUNÇÕES
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Licencasfuncoes/Index/");
                tagA_sub.InnerHtml += "Funções Usuários";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //LICENÇAS USUÁRIOS
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Usuarioslicencas/Index/");
                tagA_sub.InnerHtml += "Usuários Licenças";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //fecha

            }

            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **CADASTROS**

            //INICIO MENU **PAINEL CONSULTOR**
            //if (tipoUsuario == 7)
            //{
            //sub_menu = new TagBuilder("li");
            //sub_menu.AddCssClass("sub-menu");

            //tagA = new TagBuilder("a");
            //tagA.Attributes.Add("href", "javascript:;");

            //tagI = new TagBuilder("i");
            //tagI.AddCssClass("icon_drawer_alt");
            //tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            //span = new TagBuilder("span");
            //span.InnerHtml += "Painel Consultor";
            //tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            //span = new TagBuilder("span");
            //span.AddCssClass("menu-arrow arrow_carrot-right");
            //tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            //sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            //sub = new TagBuilder("ul");
            //sub.AddCssClass("sub");

            
            //    //Produtos Linhas
            //    li_sub = new TagBuilder("li");
            //    tagA_sub = new TagBuilder("a");

            //    tagA_sub.Attributes.Add("href", "/Produtoslinhas/Index/");
            //    tagA_sub.InnerHtml += "Produtos Linhas";
            //    li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            //    sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            //    //fecha

            //}

            //sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            //sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **CONSULTOR**

            if (tipoUsuario == 7)
            {

                //INICIO MENU **PRODUTOS**
                sub_menu = new TagBuilder("li");
                sub_menu.AddCssClass("sub-menu");

                tagA = new TagBuilder("a");
                tagA.Attributes.Add("href", "javascript:;");

                tagI = new TagBuilder("i");
                tagI.AddCssClass("icon_gift_alt");
                tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

                span = new TagBuilder("span");
                span.InnerHtml += "Produtos";
                tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

                span = new TagBuilder("span");
                span.AddCssClass("menu-arrow arrow_carrot-right");
                tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

                sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

                sub = new TagBuilder("ul");
                sub.AddCssClass("sub");

                //PRODUTO OFERTA CLARO AGRUPAMENTO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoOfertaClaroAgrupamento/Index/");
                tagA_sub.InnerHtml += "Agrupamento";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


                //PRODUTO CLASSE
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoClasse/Index/");
                tagA_sub.InnerHtml += "Classe";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Concorrentes
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Concorrentes/Index/");
                tagA_sub.InnerHtml += "Concorrentes";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Faixascomissao
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Faixascomissao/Index/");
                tagA_sub.InnerHtml += "Faixa Comissão";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //PRODUTO OFERTA CLARO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoOfertaClaro/Index/");
                tagA_sub.InnerHtml += "Oferta Claro";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


                //PRODUTO OFERTA CLARO SUBSIDIO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoOfertaClaroSubsidio/Index/");
                tagA_sub.InnerHtml += "Oferta Claro Subsídio";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Oportunidades Origens
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/OportunidadesOrigens/Index/");
                tagA_sub.InnerHtml += "Oportunidades Origens";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Oportunidades Potencial
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/OportunidadesPotencial/Index/");
                tagA_sub.InnerHtml += "Oportunidades Potencial";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Oportunidades Status
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/OportunidadesStatus/Index/");
                tagA_sub.InnerHtml += "Oportunidades Status";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Oportunidades Status Estágios
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/OportunidadesStatusEstagios/Index/");
                tagA_sub.InnerHtml += "Oportunidades Status Estágios";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


                //Produtos Linhas
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Produtoslinhas/Index/");
                tagA_sub.InnerHtml += "Produtos Linhas";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //Produtos Subsidios
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Produtossubsidios/Index/");
                tagA_sub.InnerHtml += "Produtos Subsídios";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //PRODUTO CLASSES FRANQUIAS
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutosclassesFranquias/Index/");
                tagA_sub.InnerHtml += "Produtos Classes Franquias";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                
                //PRODUTO SUBTIPO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoSubTipo/Index/");
                tagA_sub.InnerHtml += "Subtipo";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //PRODUTO SERVICO AGREGADO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoServicoAgregado/Index/");
                tagA_sub.InnerHtml += "Serviços Agregados";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //PRODUTO TIPO
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/ProdutoTipo/Index/");
                tagA_sub.InnerHtml += "Tipo";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //TIPO VENDAS
                li_sub = new TagBuilder("li");
                tagA_sub = new TagBuilder("a");

                tagA_sub.Attributes.Add("href", "/Tiposvendas/Index/");
                tagA_sub.InnerHtml += "Tipo Venda";
                li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

                sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

                //fecha
                sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
                sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
                //FIM MENU **PRODUTOS**
            }

            //INICIO MENU **ESTOQUE**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_cart_alt");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Estoque";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/EstoqueAparelhos/Index/");
            tagA_sub.InnerHtml += "Importar";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");

            tagA_sub.Attributes.Add("href", "/EstoqueAparelhos/Listar/");
            tagA_sub.InnerHtml += "Listar";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);

            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **ESTOQUE**

            //INICIO MENU **PDF**
            sub_menu = new TagBuilder("li");
            sub_menu.AddCssClass("sub-menu");

            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "javascript:;");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_book");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.InnerHtml += "Termos PDF";
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            span = new TagBuilder("span");
            span.AddCssClass("menu-arrow arrow_carrot-right");
            tagA.InnerHtml += span.ToString(TagRenderMode.Normal);

            sub_menu.InnerHtml += tagA.ToString(TagRenderMode.Normal);

            sub = new TagBuilder("ul");
            sub.AddCssClass("sub");

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/AnexoLinhasModulosPacotes/Index/");
            tagA_sub.InnerHtml += "Anexo Linhas x Modulos";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/PDFContratoPermanencia/ContratoDePermanencia.pdf");
            tagA_sub.InnerHtml += "Contrato De Permanência";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/TermoDeAdesaoAoGestorDeLdn21/Index/");
            tagA_sub.InnerHtml += "Gestor LDN 21";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/TermoDeAdesaoAoModuloGestorOnline/Index/");
            tagA_sub.InnerHtml += "Gestor Online";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/PDFContratoPermanenciaPmeTelemetria/ContratoPermanenciaPmeTelemetria.pdf");
            tagA_sub.InnerHtml += "Permanência Pme Telemetria";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/TermoContratacaoPessoaJuridicaPme/Index/");
            tagA_sub.InnerHtml += "Termo Contra. Pes. Juridica";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/TermoContratacaoTelemetria/Index/");
            tagA_sub.InnerHtml += "Termo Contratação Telemetria";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);

            li_sub = new TagBuilder("li");
            tagA_sub = new TagBuilder("a");
            tagA_sub.Attributes.Add("href", "/TermoTransferenciaLinhasClientesAssinantes/Index/");
            tagA_sub.InnerHtml += "Transferência Linhas/Cliente";
            li_sub.InnerHtml += tagA_sub.ToString(TagRenderMode.Normal);
            sub.InnerHtml += li_sub.ToString(TagRenderMode.Normal);


            
            sub_menu.InnerHtml += sub.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += sub_menu.ToString(TagRenderMode.Normal);
            //FIM MENU **PDF**


            //INICIO MENU **GRAFICOS**
            li = new TagBuilder("li");
            tagA = new TagBuilder("a");
            tagA.Attributes.Add("href", "/Dashboard/Index/");

            tagI = new TagBuilder("i");
            tagI.AddCssClass("icon_easel");
            tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

            tagA.InnerHtml += "Graficos";

            li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
            sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
            //FIM MENU **GRAFICOS**

            if (tipoUsuario != 13)
            {
                //INICIO MENU **RELATORIOS**
                li = new TagBuilder("li");
                tagA = new TagBuilder("a");
                tagA.Attributes.Add("href", "/Relatorios/Index/");

                tagI = new TagBuilder("i");
                tagI.AddCssClass("icon_datareport");
                tagA.InnerHtml += tagI.ToString(TagRenderMode.Normal);

                tagA.InnerHtml += "Relatórios";

                li.InnerHtml += tagA.ToString(TagRenderMode.Normal);
                sidebar_menu.InnerHtml += li.ToString(TagRenderMode.Normal);
                //FIM MENU **RELATORIOS**
            }

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