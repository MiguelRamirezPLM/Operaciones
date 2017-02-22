using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{
    [DataContract]
    public class AgrochemicalUseByTextInfo
    {

        #region Constructor

        public AgrochemicalUseByTextInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int AgrochemicalUseId
        {
            get
            {
                return this._agrochemicalUseId;
            }
            set
            {
                this._agrochemicalUseId = value;
            }
        }

        [DataMember]
        public string AgrochemicalUseName
        {
            get
            {
                return this._agrochemicalUseName;
            }
            set
            {
                this._agrochemicalUseName = value;
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

        private int _agrochemicalUseId;
        private string _agrochemicalUseName;
        private int _rowNumber;
        private int _total;

    }
}
