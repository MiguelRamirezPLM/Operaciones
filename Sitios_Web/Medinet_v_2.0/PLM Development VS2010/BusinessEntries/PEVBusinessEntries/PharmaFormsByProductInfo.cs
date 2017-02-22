using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class PharmaFormsByProductInfo
    {
        #region Constructors

        public PharmaFormsByProductInfo() {}

        #endregion Constructors

        #region Properties

        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }

        [DataMember]
        public string PharmaForm
        {
            get { return this._pharmaForm; }
            set { this._pharmaForm = value; }
        }

        [DataMember]
        public bool? activo
        {
            get { return this._activo; }
            set { this._activo = value; }
        }

        #endregion Properties

        private int _pharmaFormId;
        private string _pharmaForm;
        private bool? _activo;
    }
}
