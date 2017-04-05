using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ZTC.Models.ENUMS;

namespace ZTC.Model
{
    [Serializable]
    public class ServicoMensalista
    {
        #region Atributos
        private int _idServicoMensalista;
        private string _nome;
        private string _placa;
        private string _carro;
        private string _servico;
        private string _observacao;
        private DateTime? _dataHoraEntrada;
        private DateTime? _dataHoraSaida;
        private decimal _valorServico;
        private decimal _valorHora;
        private decimal _totalHoras;
        private decimal _valorTotal;
        private string _formaPagamento;
        private bool _fechado;
        #endregion

        #region Propriedades

        public int IdServicoMensalista
        {
            get { return _idServicoMensalista; }
            set { _idServicoMensalista = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }


        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }
        public string Carro
        {
            get { return _carro; }
            set { _carro = value; }
        }

        public string Servico
        {
            get { return _servico; }
            set { _servico = value; }
        }
        public string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }
        public DateTime? DataHoraEntrada
        {
            get { return _dataHoraEntrada; }
            set { _dataHoraEntrada = value; }
        }
        public DateTime? DataHoraSaida
        {
            get { return _dataHoraEntrada; }
            set { _dataHoraEntrada = value; }
        }
        public decimal ValorServico
        {
            get { return _valorServico; }
            set { _valorServico = value; }
        }
        public decimal ValorHora
        {
            get { return _valorHora; }
            set { _valorHora = value; }
        }
        public decimal TotalHoras
        {
            get { return _totalHoras; }
            set { _totalHoras = value; }
        }
        public decimal ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; }
        }
        public string FormaPagamento
        {
            get { return _formaPagamento; }
            set { _formaPagamento = value; }
        }

        public bool Fechado
        {
            get { return _fechado; }
            set { _fechado = value; }
        }

        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public ServicoMensalista()
        {
            Persisted = false;
        }

        public ServicoMensalista(int IdServicoMensalista)
        {
            this._idServicoMensalista = IdServicoMensalista;
            Persisted = true;
        }
        #endregion
    }
}
