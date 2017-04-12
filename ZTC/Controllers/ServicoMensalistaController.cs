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
using System.IO;

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
                //servicoMensalista.DataHoraEntrada = collection["dataLimiteRetorno"] != "" ? DateTime.Parse(collection["dataLimiteRetorno"]) : (DateTime?)null;
                servicoMensalista.DataHoraEntrada = collection["dataEntrada"] != "" ? DateTime.Parse(collection["dataEntrada"] + " " + collection["horaEntrada"]) : (DateTime?)null;
                //servicoMensalista.DataHoraEntrada = collection["dataEntrada"] != "" ? DateTime.Parse(collection["dataEntrada"]) + collection["horaEntrada"] != "" ? DateTime.Parse(collection["horaEntrada"]) : (DateTime?)null : (DateTime?)null;
                //servicoMensalista.DataHoraSaida = collection["dataSaida"] != "" ? DateTime.Parse(collection["dataSaida"]) + collection["horaSaida"] != "" ? DateTime.Parse(collection["horaSaida"]) : (DateTime?)null : (DateTime?)null;
                servicoMensalista.DataHoraSaida = collection["dataSaida"] != "" ? DateTime.Parse(collection["dataSaida"] + " " + collection["horaSaida"]) : (DateTime?)null;
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

                GetFileEntrada(servicoMensalista.IdServicoMensalista, servicoMensalista);

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
        public ActionResult Edit(ServicoMensalista servicoMensalista, FormCollection collection)
        {

            try
            {
                var bll = new ServicoMensalistaBll();

                servicoMensalista.Placa = collection["placa"];
                servicoMensalista.Carro = collection["carro"];
                servicoMensalista.Nome = collection["nome"];
                servicoMensalista.DataHoraEntrada = collection["dataHoraEntrada"] != "" ? DateTime.Parse(collection["dataHoraEntrada"] + " " + collection["horaEntrada"]) : (DateTime?)null;
                servicoMensalista.DataHoraSaida = collection["dataSaida"] != "" ? DateTime.Parse(collection["dataSaida"] + " " + collection["horaSaida"]) : (DateTime?)null;
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

                GetFileSaida(servicoMensalista.IdServicoMensalista, servicoMensalista);

                Success("Sucesso", "Salvo com sucesso!", true);
                return RedirectToAction("/");
            }
            catch (Exception ex)
            {
                Danger("Erro", string.Format(ex.Message), true);
                return View(servicoMensalista);
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


        public FileStreamResult GetFileEntrada(int id, ServicoMensalista servicoMensalista)
        {


            string name = @"D:\Teste\" + id + "_ENTRADA" + ".txt";

            FileInfo info = new FileInfo(name);
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine("");
                    writer.WriteLine("ZTC PARKING");
                    writer.WriteLine("RUA DR.BEZERRA DE MENEZES, 113 - AHU");
                    writer.WriteLine("");
                    writer.WriteLine("ENTRADA - MENSALISTA");
                    writer.WriteLine("TICKET: " + id);
                    writer.WriteLine("DATA HORA ENTRADA: " + servicoMensalista.DataHoraEntrada);
                    writer.WriteLine("PLACA: " + servicoMensalista.Placa);
                    writer.WriteLine("CARRO: " + servicoMensalista.Carro);
                    writer.WriteLine("NOME: " + servicoMensalista.Nome);

                }
            }

            else
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine("");
                    writer.WriteLine("ZTC PARKING");
                    writer.WriteLine("RUA DR.BEZERRA DE MENEZES, 113 - AHU");
                    writer.WriteLine("");
                    writer.WriteLine("ENTRADA - MENSALISTA");
                    writer.WriteLine("TICKET: " + id);
                    writer.WriteLine("DATA HORA ENTRADA: " + servicoMensalista.DataHoraEntrada);
                    writer.WriteLine("PLACA: " + servicoMensalista.Placa);
                    writer.WriteLine("CARRO: " + servicoMensalista.Carro);
                    writer.WriteLine("NOME: " + servicoMensalista.Nome);

                }
            }

            return File(info.OpenRead(), "text/plain");

        }

        public FileStreamResult GetFileSaida(int id, ServicoMensalista servicoMensalista)
        {


            string name = @"D:\Teste\" + id + "_SAIDA" + ".txt";

            FileInfo info = new FileInfo(name);
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine("");
                    writer.WriteLine("ZTC PARKING");
                    writer.WriteLine("RUA DR.BEZERRA DE MENEZES, 113 - AHU");
                    writer.WriteLine("");
                    writer.WriteLine("SAÍDA - MENSALISTA");
                    writer.WriteLine("TICKET: " + id);
                    writer.WriteLine("DATA HORA ENTRADA: " + servicoMensalista.DataHoraEntrada);
                    writer.WriteLine("DATA HORA SAÍDA: " + servicoMensalista.DataHoraSaida);
                    writer.WriteLine("PLACA: " + servicoMensalista.Placa);
                    writer.WriteLine("CARRO: " + servicoMensalista.Carro);
                    writer.WriteLine("NOME: " + servicoMensalista.Nome);

                }
            }

            else
            {
                using(StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine("");
                    writer.WriteLine("ZTC PARKING");
                    writer.WriteLine("RUA DR.BEZERRA DE MENEZES, 113 - AHU");
                    writer.WriteLine("");
                    writer.WriteLine("SAÍDA - MENSALISTA");
                    writer.WriteLine("TICKET: " + id);
                    writer.WriteLine("DATA HORA ENTRADA: " + servicoMensalista.DataHoraEntrada);
                    writer.WriteLine("DATA HORA SAÍDA: " + servicoMensalista.DataHoraSaida);
                    writer.WriteLine("PLACA: " + servicoMensalista.Placa);
                    writer.WriteLine("CARRO: " + servicoMensalista.Carro);
                    writer.WriteLine("NOME: " + servicoMensalista.Nome);

                }
            }

            return File(info.OpenRead(), "text/plain");

        }
    }
}
