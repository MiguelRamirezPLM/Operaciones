using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class LaboratoryInfo
    {
        #region Constructor

        public LaboratoryInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        [DataMember]
        public string LaboratoryName
        {
            get { return this._laboratoryName; }
            set { this._laboratoryName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _laboratoryId;
        private string _laboratoryName;
        private bool _active;

    }
}
