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
    public class UsuariosController : BaseController
    {
        //[AccessDeniedAuthorizeAttribute]
        //[AccessDeniedAuthorizeAttribute(Roles = "Administrador,Funcionário")]
        // GET: Usuarios
        [AccessDeniedAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        [AccessDeniedAuthorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        [AccessDeniedAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [AccessDeniedAuthorize]
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

        // GET: Usuarios/Edit/5
        [AccessDeniedAuthorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
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

        // GET: Usuarios/Delete/5
        [AccessDeniedAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                UsuariosBll bll = new UsuariosBll();
                Usuarios usuarioLogin = new Usuarios();
                usuarioLogin.Persisted = true;
                //usuarioLogin.Email = collection["login"];
                usuarioLogin.CPF = collection["cpf"];
                usuarioLogin.Senha = collection["password"].Trim();

                var usuario = bll.Login(usuarioLogin);

                var licencaBll = new UsuarioslicencasBll();
                Usuarioslicencas licenca = null;

                if (usuario.Perfil.Nome.ToLower() == "Administrador")
                {
                    //licenca = new Usuarioslicencas()
                    //{
                    //    Funcao = new Licencasfuncoes() { Nome = "Desenvolvedor" },
                    //    Status = StatusUsuarioLicenca.Ativo,
                    //    Licencas = new Licencas() { Descricao = "Desenvolvedor" },
                    //    Usuarios = usuario
                    //};
                    licenca = licencaBll.GetObject(usuario);
                }
                else
                {
                    licenca = licencaBll.GetObject(usuario);


                    if (licenca == null)
                    {
                        throw new Exception("Atenção! Seu usuário não possui uma licença valida.");
                    }
                    else
                    {
                        if (licenca.Status != StatusUsuarioLicenca.Ativo)
                        {
                            throw new Exception("Atenção! Sua licença não está ativa.");
                        }

                       
                    }

                }



                SessionContext.UsuarioLogado = licenca;
                FormsAuthentication.SetAuthCookie(usuario.CPF, false);

                
                if (SessionContext.UsuarioLogado.Usuarios.Perfil.IdPerfil == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Danger("Erro", ex.Message, true);
                
                return View();
            }
        }


        public ActionResult LogOff()
        {
            SessionContext.UsuarioLogado = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Usuarios");
        }

    }
}
