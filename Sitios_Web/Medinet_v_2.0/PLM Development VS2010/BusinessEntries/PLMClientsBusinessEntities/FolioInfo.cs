using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class FolioInfo
    {
        #region Constructors

        public FolioInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int FolioId
        {
            get { return this._folioId; }
            set { this._folioId = value; }
        }

        [DataMember]
        public string FolioString
        {
            get { return this._folioString; }
            set { this._folioString = value; }
        }

        [DataMember]
        public int PrefixId
        {
            get { return this._prefixId; }
            set { this._prefixId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._activeId; }
            set { this._activeId = value; }
        }

        #endregion

        private int _folioId;
        private string _folioString;
        private int _prefixId;
        private bool _activeId;

    }
}
