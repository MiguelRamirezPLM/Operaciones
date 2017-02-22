using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
{
    [DataContract]
    public class SectionInfo
    {

        #region Constructor

        public SectionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SectionId
        {
            get
            {
                return this._sectionId;
            }
            set
            {
                this._sectionId = value;
            }
        }

        [DataMember]
        public int ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        [DataMember]
        public string SectionName
        {
            get
            {
                return this._sectionName;
            }
            set
            {
                this._sectionName = value;
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _sectionId;
        private int _parentId;
        private string _sectionName;
        private bool _active;

    }
}
