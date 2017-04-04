using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZTC.Bll;
using ZTC.Model;
using ZTC.Models;
using ZTC.Models.ENUMS;
using System.Net.Mail;

namespace ZTC.Controllers
{
    public class ClientesApiController : ApiController
    {

        public List<Mensalista> Get()
        {
            var bll = new MensalistaBll();
            var list = bll.GetList();
            return list;
        }

        public List<Mensalista> GetByNomeMensalista(string placa)
        {
            var bll = new MensalistaBll();
            var placa1 = bll.GetList("Where (Placa LIKE '%" + placa + "%')");

            if (placa1 == null)
                placa1 = new List<Mensalista>();

            //var bllpessoa = new PessoasBll();
            //foreach (var empresanome in nomeempresa1)
            //{
            //    empresanome.PessoaRepresentante = bllpessoa.GetObject(empresanome.PessoaRepresentante.IdPessoa);
            //}

            return placa1;
        }

        public Mensalista GetByNomeCliente(string nomeCliente)
        {
            var bll = new MensalistaBll();
            var nome = bll.GetList("Where Nome = '" + nomeCliente + "'").FirstOrDefault();
            if (nome == null)
                nome = new Mensalista();

            //var bllpessoa = new PessoasBll();
            //nome.PessoaRepresentante = bllpessoa.GetObject(nome.PessoaRepresentante.IdPessoa);

            return nome;
        }




    }
}
