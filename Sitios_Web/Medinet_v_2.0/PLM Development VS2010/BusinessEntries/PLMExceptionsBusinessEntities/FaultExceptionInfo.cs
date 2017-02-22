using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMExceptionsBusinessEntities
{
    [DataContract]
    public class FaultExceptionInfo
    {
        #region Constructors

        public FaultExceptionInfo()
        {
            this._message = "Ha ocurrido un error en el servicio.";
        }

        #endregion

        #region Properties

        [DataMember]
        public string Message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        [DataMember]
        public string Folio
        {
            get { return this._folio; }
            set { this._folio = value; }
        }

        #endregion

        private string _message;
        private string _folio;
    }
}
