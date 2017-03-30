using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZTC.Models.ENUMS;

namespace ZTC.Model
{
        [Serializable]
        public class Usuarioslicencas
        {
            #region Atributos
            private int _idUsuarioLicenca;
            private Usuarios _usuarios;
            private Licencas _licencas;
            private StatusUsuarioLicenca _status;
            #endregion

            #region Propriedades
            public int IdUsuarioLicenca
            {
                get { return _idUsuarioLicenca; }
                set {  _idUsuarioLicenca = value; }
            }
            public Usuarios Usuarios
            {
                get { return _usuarios; }
                set {  _usuarios = value; }
            }
            public Licencas Licencas
            {
                get { return _licencas; }
                set {  _licencas = value; }
            }
            public StatusUsuarioLicenca Status
            {
                get { return _status; }
                set {  _status = value; }
            }            
            public bool Persisted { get; set; }

            #endregion

            #region Construtores

            public Usuarioslicencas()
            {
                Persisted = false;
            }

            public Usuarioslicencas(int IdUsuarioLicenca)
            {
                this._idUsuarioLicenca = IdUsuarioLicenca ;
                Persisted = true;
            }
            #endregion
        }
}
