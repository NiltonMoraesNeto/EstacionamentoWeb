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
    public class EntradaController : BaseController
    {
        // GET: Entrada
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            var entrada = new List<Entrada>();
            var bll = new EntradaBll();

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
