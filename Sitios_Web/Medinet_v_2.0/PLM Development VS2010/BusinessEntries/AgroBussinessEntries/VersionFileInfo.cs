using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace AgroBusinessEntries
{
    [DataContract]
    public class VersionFileInfo
    {
        #region Constructor
        public VersionFileInfo() { }
        #endregion

        #region Propierities
        [DataMember]
        public int VersionFileId
        {
            get { return this._versionFileId; }
            set { this._versionFileId = value; }
        }

        [DataMember]
        public string ApplicationName
        {
            get { return this._applicationName; }
            set { this._applicationName = value; }
        }

        [DataMember]
        public string PrefixApplication
        {
            get { return this._prefixApplication; }
            set { this._prefixApplication= value; }
        }

        [DataMember]
        public int Edition
        {
            get { return this._edition; }
            set { this._edition = value; }
        }

        [DataMember]
        public int CurrentValue
        {
            get { return this._currentValue; }
            set { this._currentValue = value; }
        }
        #endregion

        private int _versionFileId;
        private string _applicationName;
        private string _prefixApplication;
        private int _edition;
        private int _currentValue;

    }
}
