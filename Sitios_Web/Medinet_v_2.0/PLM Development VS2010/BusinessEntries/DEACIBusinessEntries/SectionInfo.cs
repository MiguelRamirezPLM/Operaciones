using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries
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
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        [DataMember]
        public int? SectionIdParent
        {
            get
            {
                return this._sectionIdParent;
            }
            set
            {
                this._sectionIdParent = value;
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
        private string _description;
        private int? _sectionIdParent;
        private bool _active;

    }
}
