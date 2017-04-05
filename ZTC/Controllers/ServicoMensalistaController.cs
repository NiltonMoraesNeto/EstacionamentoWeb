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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Entrada/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entrada/Create
        [HttpPost]
        public ActionResult Create(ServicoMensalista servicoMensalista, FormCollection collection)
        {
            try
            {
                var bll = new ServicoMensalistaBll();


                servicoMensalista.Nome = collection["nomeCliente"];
                servicoMensalista.DataHoraEntrada = collection["dataEntrada"] != "" ? DateTime.Parse(collection["dataEntrada"]) + collection["dataHora"] != "" ? DateTime.Parse(collection["dataHora"]) : (DateTime?)null : (DateTime?)null;

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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Entrada/Edit/5
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entrada/Delete/5
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
