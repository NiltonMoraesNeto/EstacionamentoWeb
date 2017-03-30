using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ZTC.Model
{
    [Serializable]
    public class Usuariosperfis
    {
        #region Atributos
        private int _idPerfil;
        private string _nome;
        #endregion

        #region Propriedades

        public int IdPerfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public bool Persisted { get; set; }

        #endregion

        #region Construtores

        public Usuariosperfis()
        {
            Persisted = false;
        }

        public Usuariosperfis(int IdPerfil)
        {
            this._idPerfil = IdPerfil;
            Persisted = true;
        }
        #endregion
    }
}
