using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class SpeciesInfo
    {
        #region Constructors

        public SpeciesInfo() { }
        
        #endregion

        #region Properties

        [DataMember]
        public int SpecieId
        {
            get
            {
                return this._specieId;
            }
            set
            {
                this._specieId = value;
            }
        }

        [DataMember]
        public string SpecieName
        {
            get
            {
                return this._specieName;
            }
            set
            {
                this._specieName = value;
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

        private int _specieId;
        private string _specieName;
        private int _rowNumber;
        private int _total;
    }
}
