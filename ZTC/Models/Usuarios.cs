using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZTC.Models.ENUMS;

namespace ZTC.Model
{
    [Serializable]
    public class Usuarios
    {
        #region Atributos
        private int _idUsuario;
        private Usuariosperfis _idPerfil;
        private Usuarioslicencas _idUsuarioLicenca;
        private string _nomeUsuario;
        private string _senha;
        private string _email;
        private string _cpf;
        private bool _excluido;
        private StatusUsuarioLicenca _status;
        #endregion

        #region Propriedades

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public Usuariosperfis Perfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }
        public Usuarioslicencas IdUsuarioLicenca
        {
            get { return _idUsuarioLicenca; }
            set { _idUsuarioLicenca = value; }
        }

        public string NomeUsuario
        {
            get { return _nomeUsuario; }
            set { _nomeUsuario = value; }
        }
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }
        public StatusUsuarioLicenca Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }
        public bool Excluido
        {
            get { return _excluido; }
            set { _excluido = value; }
        }

        public string GetMD5Hash()
        {
            string password = _senha;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);

            byte[] hash = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();

        }


        public bool Persisted { get; set; }
        #endregion

        #region Construtores

        public Usuarios()
        {
            Persisted = false;
        }

        public Usuarios(int IdUsuario)
        {
            this._idUsuario = IdUsuario;
            Persisted = true;
        }
        #endregion
    }
}
