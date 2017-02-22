using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class ApplicationMenuInfo
    {
        #region Constructors

        public ApplicationMenuInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public int MenuId
        {
            get { return this._menuId; }
            set { this._menuId = value; }
        }

        [DataMember]
        public string URL
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
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _applicationId;
        private int _menuId;
        private string _url;
        private string _imageName;
        private bool _active;

    }
}
