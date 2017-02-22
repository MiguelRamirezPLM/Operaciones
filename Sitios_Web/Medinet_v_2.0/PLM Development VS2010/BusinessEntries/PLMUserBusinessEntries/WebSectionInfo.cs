using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class WebSectionInfo
    {
        #region Constructors

        public WebSectionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public byte WebSectionId
        {
            get { return this._webSectionId; }
            set { this._webSectionId = value; }
        }

        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public string SectionName
        {
            get { return this._sectionName; }
            set { this._sectionName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _webSectionId;
        private int? _parentId;
        private string _sectionName;
        private bool _active;

    }
}
