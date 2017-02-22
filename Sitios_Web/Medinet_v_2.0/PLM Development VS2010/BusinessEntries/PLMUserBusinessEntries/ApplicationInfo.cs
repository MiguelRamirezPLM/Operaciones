using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class ApplicationInfo
    {
        private int _applicationId;
        private string _description;
        private string _hashKey;
        private string _url;
        private string _rootUrl;
        private string _version;
        private DateTime _lastUpdate;
        private string _documentFile;
        private bool _active;

        #region Constructors

        public ApplicationInfo() 
        {
           _lastUpdate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public string HashKey
        {
            get { return this._hashKey; }
            set { this._hashKey = value; }
        }

        [DataMember]
        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }

        [DataMember]
        public string RootUrl
        {
            get { return this._rootUrl; }
            set { this._rootUrl = value; }
        }

        [DataMember]
        public string Version
        { 
            get { return this._version; }
            set { this._version = value; }
        }

        [DataMember]
        public DateTime LastUpdate
        {
            get { return this._lastUpdate; }
            set { this._lastUpdate = value; }
        }

        [DataMember]
        public string DocumentFile
        {
            get { return this._documentFile; }
            set { this._documentFile = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion
    }

}
