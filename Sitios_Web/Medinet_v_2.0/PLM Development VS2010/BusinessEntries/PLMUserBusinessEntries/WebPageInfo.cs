using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class WebPageInfo
    {
        #region Constructors

        public WebPageInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int WebPageId
        {
            get { return this._webPageId; }
            set { this._webPageId = value; }
        }

        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public string PageDescription
        {
            get { return this._pageDescription; }
            set { this._pageDescription = value; }
        }

        [DataMember]
        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _webPageId;
        private int? _parentId;
        private int _applicationId;
        private string _pageDescription;
        private string _url;
        private bool _active;

    }
}
