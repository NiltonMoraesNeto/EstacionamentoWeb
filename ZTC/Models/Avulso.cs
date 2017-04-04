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
    public class Avulso
    {
        #region Atributos
        private int _idAvulso;
        private string _nome;
        private string _cpf;
        private string _telefoneFixo;
        private string _telefoneCelular;
        private string _marca;
        private string _carro;
        private string _placa;
        private string _cor;
        private string _obs;
        #endregion

        #region Propriedades

        public int IdAvulso
        {
            get { return _idAvulso; }
            set { _idAvulso = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        public string TelefoneFixo
        {
            get { return _telefoneFixo; }
            set { _telefoneFixo = value; }
        }
        public string TelefoneCelular
        {
            get { return _telefoneCelular; }
            set { _telefoneCelular = value; }
        }
        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }
        public string Carro
        {
            get { return _carro; }
            set { _carro = value; }
        }
        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }
        public string Cor
        {
            get { return _cor; }
            set { _cor = value; }
        }
        [DataType(DataType.MultilineText)]
        public string Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Avulso()
        {
            Persisted = false;
        }

        public Avulso(int IdAvulso)
        {
            this._idAvulso = IdAvulso;
            Persisted = true;
        }
        #endregion
    }
}
