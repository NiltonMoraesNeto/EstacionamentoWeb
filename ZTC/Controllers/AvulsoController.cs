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
    public class AvulsoController : BaseController
    {
        // GET: Avulso
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            var avulso = new List<Avulso>();
            var bll = new AvulsoBll();

            string sql = "";

            avulso = bll.GetList(sql, true);

            return View(avulso);

        }

        // GET: Avulso/Details/5
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Avulso/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avulso/Create
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Create(Avulso avulso,FormCollection collection)
        {
            try
            {
                var bll = new AvulsoBll();

                bll.Save(avulso);


                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Avulso/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {
            var bll = new AvulsoBll();
            var avulso = bll.GetObject(id);




            if (avulso == null)
            {
                return HttpNotFound();
            }

            return View(avulso);
        }

        // POST: Avulso/Edit/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Edit(Avulso avulso, FormCollection collection)
        {
            avulso.Nome = collection["Nome"];
            try
            {
                var bll = new AvulsoBll();
                bll.Save(avulso);
                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("/");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format(ex.Message), true);
                return View(avulso);
            }
        }

        // GET: Avulso/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Avulso/Delete/5
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
