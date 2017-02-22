using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class MenuByWebSection
    {
        #region Constructors

        public MenuByWebSection() { }

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
        public int RoleId
        {
            get { return this._roleId; }
            set { this._roleId = value; }
        }

        [DataMember]
        public string MenuName
        {
            get { return this._menuName; }
            set { this._menuName = value; }
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

        #endregion

        private int _webPageId;
        private byte _webSectionId;
        private int _menuId;
        private int _roleId;
        private string _menuName;
        private string _url;
        private string _imageName;
        
    }
}
