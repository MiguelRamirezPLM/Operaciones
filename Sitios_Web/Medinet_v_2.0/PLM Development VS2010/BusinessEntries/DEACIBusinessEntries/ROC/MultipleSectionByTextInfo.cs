using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public class MultipleSectionByTextInfo
    {

        #region Constructor

        public MultipleSectionByTextInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SubSectionId
        {
            get
            {
                return this._subSectionId;
            }
            set
            {
                this._subSectionId = value;
            }
        }

        [DataMember]
        public string SubSectionDescription
        {
            get
            {
                return this._subSectionDescription;
            }
            set
            {
                this._subSectionDescription = value;
            }
        }

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
        public string SectionDescription
        {
            get
            {
                return this._sectionDescription;
            }
            set
            {
                this._sectionDescription = value;
            }
        }

        [DataMember]
        public int RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                this._rowNumber = value;
            }
        }

        [DataMember]
        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }

        #endregion

        private int _subSectionId;
        private string _subSectionDescription;
        private int _sectionId;
        private int? _sectionIdParent;
        private string _sectionDescription;
        private int _rowNumber;
        private int _total;

    }
}
