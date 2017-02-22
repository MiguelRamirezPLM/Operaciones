using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class PharmaFormInfo
    {
        #region Constructor

        public PharmaFormInfo() { }

        #endregion

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
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _pharmaFormId;
        private string _pharmaForm;
        private bool _active;

    }
}
