using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public class ActiveSubstanceByProductInfo
    {

        #region Constructor

        public ActiveSubstanceByProductInfo() { }

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
        public int CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
        }

        [DataMember]
        public int PharmaFormId
        {
            get
            {
                return this._pharmaFormId;
            }
            set
            {
                this._pharmaFormId = value;
            }
        }

        [DataMember]
        public int DivisionId
        {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }

        #endregion

        private int _activeSubstanceId;
        private string _activeSubstanceName;
        private int _categoryId;
        private int _pharmaFormId;
        private int _divisionId;

    }
}
