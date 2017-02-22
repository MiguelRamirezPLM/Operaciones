using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public class ActiveSubstanceByTextInfo
    {

        #region Constructor

        public ActiveSubstanceByTextInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ActiveSubstanceId
        {
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
        }

        [DataMember]
        public string ActiveSubstanceName
        {
            get
            {
                return this._activeSubstanceName;
            }
            set
            {
                this._activeSubstanceName = value;
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

        private int _activeSubstanceId;
        private string _activeSubstanceName;
        private int _rowNumber;
        private int _total;

    }
}
