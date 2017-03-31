using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZTC.Models.ENUMS;

namespace ZTC.Model
{
    [Serializable]
    public class Mensalista
    {
        #region Atributos
        private int _idMensalista;
        private string _nome;
        private string _cpf;
        private DateTime? _dataNascimento;
        private string _telefoneFixo;
        private string _telefoneCelular;
        private string _cep;
        private string _rua;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private string _complemento;
        #endregion

        #region Propriedades

        public int IdMensalista
        {
            get { return _idMensalista; }
            set { _idMensalista = value; }
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

        public DateTime? DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
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
        public string CEP
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public string Rua
        {
            get { return _rua; }
            set { _rua = value; }
        }
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Mensalista()
        {
            Persisted = false;
        }

        public Mensalista(int IdMensalista)
        {
            this._idMensalista = IdMensalista;
            Persisted = true;
        }
        #endregion
    }
}
