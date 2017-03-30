using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using ZTC.Models.ENUMS;

namespace ZTC.Model
{
        [Serializable]
        public class Licencas
        {
            #region Atributos
            private int _idLicenca;
            private string _descricao;
            private StatusLicenca _status;
            #endregion

            #region Propriedades
            [Display(Name = "ID Licença")]
            public int IdLicenca
            {
                get { return _idLicenca; }
                set {  _idLicenca = value; }
            }
            public string Descricao
            {
                get { return _descricao; }
                set {  _descricao = value; }
            }
            public StatusLicenca Status
            {
                get { return _status; }
                set {  _status = value; }
            }
            public bool Persisted { get; set; }

            #endregion

            #region Construtores

            public Licencas()
            {
                Persisted = false;
            }

            public Licencas(int IdLicenca)
            {
                this._idLicenca = IdLicenca ;
                Persisted = true;
            }
            #endregion
        }
}
