using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class TherapeuticUsesByTextInfo
    {

        #region Constructors

        public TherapeuticUsesByTextInfo() { }

        #endregion Constructors

        #region Properties

        [DataMember]
        public int TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
        }

        [DataMember]
        public string TherapeuticName
        {
            get { return this._therapeuticName; }
            set { this._therapeuticName = value; }
        }

        [DataMember]
        public int? Nivel
        {
            get
            {
                return this._nivel;
            }
            set
            {
                this._nivel = value;
            }
        }

        [DataMember]
        public int? ParentId
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
        public int? RowNumber
        {
            get { return this._rowNumber; }
            set { this._rowNumber = value; }
        }

        [DataMember]
        public int? Total
        {
            get { return this._total; }
            set { this._total = value; }
        }

        #endregion Constructors

        private int _therapeuticId;
        private string _therapeuticName;
        private int? _nivel;
        private int? _parentId;
        private int? _rowNumber;
        private int? _total;
    }
}
