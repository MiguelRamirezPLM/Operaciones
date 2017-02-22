using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries.ROC
{  
    [DataContract]
    public class CropByEditionInfo
    {
        #region Constructor

        public CropByEditionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CropId
        {
            get { return this._cropId; }
            set { this._cropId = value; }
        }

        [DataMember]
        public string CropName
        {
            get { return this._cropName; }
            set { this._cropName = value; }
        }

        [DataMember]
        public int Total
        {
            get { return this._total; }
            set { this._total = value; }
        }

        [DataMember]
        public int RowNumber
        {
            get { return this._rowNumber; }
            set { this._rowNumber = value; }
        }
      

        #endregion

        private int _cropId;
        private string _cropName;
        private int _total;
        private int _rowNumber;
    }
}
