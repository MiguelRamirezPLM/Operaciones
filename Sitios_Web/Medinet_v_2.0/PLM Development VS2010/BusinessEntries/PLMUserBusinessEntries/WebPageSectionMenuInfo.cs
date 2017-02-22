using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class WebPageSectionMenuInfo
    {
        #region Constructors

        public WebPageSectionMenuInfo() { }

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
            get { return this._webSectionId; }
            set { this._webSectionId = value; }
        }

        [DataMember]
        public int MenuId
        {
            get { return this._menuId; }
            set { this._menuId = value; }
        }

        [DataMember]
        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }

        [DataMember]
        public string ImageName
        {
            get { return this._imageName; }
            set { this._imageName = value; }
        }

        [DataMember]
        public byte MenuOrder
        {
            get { return this._menuOrder; }
            set { this._menuOrder = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _webPageId;
        private byte _webSectionId;
        private int _menuId;
        private string _url;
        private string _imageName;
        private byte _menuOrder;
        private bool _active;

    }
}
