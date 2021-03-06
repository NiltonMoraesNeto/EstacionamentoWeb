﻿using System;
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
        [AccessDeniedAuthorize]
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
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mensalista/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mensalista/Create
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Create(Mensalista mensalista, FormCollection collection)
        {
            try
            {
                var bll = new MensalistaBll();

                bll.Save(mensalista);


                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format("Erro: " + ex.Message), true);
                return View();
            }
        }

        // GET: Mensalista/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {
            var bll = new MensalistaBll();
            var mensalista = bll.GetObject(id);




            if (mensalista == null)
            {
                return HttpNotFound();
            }
            
            return View(mensalista);
        }

        // POST: Mensalista/Edit/5
        [AccessDeniedAuthorize]
        [HttpPost]
        public ActionResult Edit(Mensalista mensalista, FormCollection collection)
        {
            mensalista.Nome = collection["Nome"];
            try
            {
                var bll = new MensalistaBll();
                bll.Save(mensalista);
                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("/");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format(ex.Message), true);
                return View(mensalista);
            }
        }

        // GET: Mensalista/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mensalista/Delete/5
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
