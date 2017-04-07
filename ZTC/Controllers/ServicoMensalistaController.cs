using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ServicoMensalistaController : BaseController
    {
        // GET: Entrada
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            var entrada = new List<ServicoMensalista>();
            var bll = new ServicoMensalistaBll();

            string sql = "";

            entrada = bll.GetList(sql, true);

            return View(entrada);
        }

        // GET: Entrada/Details/5
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Entrada/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entrada/Create
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Create(ServicoMensalista servicoMensalista, FormCollection collection)
        {
            try
            {
                var bll = new ServicoMensalistaBll();

                servicoMensalista.Placa = collection["placa"];
                servicoMensalista.Carro = collection["carro"];
                servicoMensalista.Nome = collection["nomeCliente"];
                servicoMensalista.DataHoraEntrada = collection["dataEntrada"] != "" ? DateTime.Parse(collection["dataEntrada"]) + collection["horaEntrada"] != "" ? DateTime.Parse(collection["horaEntrada"]) : (DateTime?)null : (DateTime?)null;
                servicoMensalista.DataHoraSaida = collection["dataSaida"] != "" ? DateTime.Parse(collection["dataSaida"]) + collection["horaSaida"] != "" ? DateTime.Parse(collection["horaSaida"]) : (DateTime?)null : (DateTime?)null;
                servicoMensalista.Servico1 = collection["servico1"];
                servicoMensalista.Servico2 = collection["servico2"];
                servicoMensalista.Servico3 = collection["servico3"];
                servicoMensalista.Observacao1 = collection["obs1"];
                servicoMensalista.Observacao2 = collection["obs2"];
                servicoMensalista.Observacao3 = collection["obs3"];
                servicoMensalista.ValorServico = Convert.ToDecimal(collection["valorServico"]);
                servicoMensalista.ValorHora = Convert.ToDecimal(collection["valorHora"]);
                servicoMensalista.TotalHoras = collection["totalHoras"];
                servicoMensalista.ValorTotal = Convert.ToDecimal(collection["valorTotal"]);
                servicoMensalista.FormaPagamento = collection["formaPagamento"];

                bll.Save(servicoMensalista);


                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Entrada/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {
            var bll = new ServicoMensalistaBll();
            var servicoMensalista = bll.GetObject(id);




            if (servicoMensalista == null)
            {
                return HttpNotFound();
            }

            return View(servicoMensalista);
        }

        // POST: Entrada/Edit/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Entrada/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entrada/Delete/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
