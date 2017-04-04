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
    public class Entrada
    {
        #region Atributos
        private int _idEntrada;
        private string _nome;
        private string _placa;
        private string _carro;
        private string _servico;
        private DateTime? _dataHoraEntrada;
        private bool _fechado;
        #endregion

        #region Propriedades

        public int IdEntrada
        {
            get { return _idEntrada; }
            set { _idEntrada = value; }
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
        public DateTime? DataHoraEntrada
        {
            get { return _dataHoraEntrada; }
            set { _dataHoraEntrada = value; }
        }
        public bool Fechado
        {
            get { return _fechado; }
            set { _fechado = value; }
        }

        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Entrada()
        {
            Persisted = false;
        }

        public Entrada(int IdEntrada)
        {
            this._idEntrada = IdEntrada;
            Persisted = true;
        }
        #endregion
    }
}
