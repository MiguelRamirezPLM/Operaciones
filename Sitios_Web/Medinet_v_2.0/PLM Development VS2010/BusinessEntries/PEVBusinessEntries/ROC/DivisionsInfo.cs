using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class DivisionsInfo
    {
        #region Constructors

        public DivisionsInfo() { }
        
        #endregion

        #region Properties

        [DataMember]
        public int DivisionId {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }

        [DataMember]
        public string DivisionName {
            get
            {
                return this._divisionName;
            }
            set
            {
                this._divisionName = value;
            }
        }

        [DataMember]
        public string ShortName {
            get
            {
                return this._shortName;
            }
            set
            {
                this._shortName = value;
            }
        }

        [DataMember]
        public int RowNumber {
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
        public int Total {
            get
            {
                return this._total;
            }
            set {
                this._total = value;
            }
        }

        #endregion

        private int _divisionId;
        private string _divisionName;
        private string _shortName;
        private int _rowNumber;
        private int _total;

    }
}
