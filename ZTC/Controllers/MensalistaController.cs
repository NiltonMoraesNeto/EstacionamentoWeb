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
    public class MensalistaController : BaseController
    {
        // GET: Mensalista
        public ActionResult Index()
        {
            var mensalista = new List<Mensalista>();
            var bll = new MensalistaBll();

            string sql = "";

            //sql += " where MONTH(DataHoraSolicitacao) = MONTH(NOW()) and YEAR(DataHoraSolicitacao) = YEAR(NOW())  and TipoManutencao_Id = 2 ";
            mensalista = bll.GetList(sql, true);

            return View(mensalista);
        }

        // GET: Mensalista/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mensalista/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mensalista/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mensalista/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mensalista/Edit/5
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

        // GET: Mensalista/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mensalista/Delete/5
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
