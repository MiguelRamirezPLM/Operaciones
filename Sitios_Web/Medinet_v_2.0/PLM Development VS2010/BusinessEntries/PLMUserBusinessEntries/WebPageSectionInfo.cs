using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class WebPageSectionInfo
    {
        #region Constructors

        public WebPageSectionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int WebPageId
        {
            get { return this._webPageId; }
            set { this._webPageId = value; }
        }

        [DataMember]
        public byte WebSectionId
        {
            get { return this._webSectionid; }
            set { this._webSectionid = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _webPageId;
        private byte _webSectionid;
        private bool _active;

    }
}
