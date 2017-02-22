using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class EquipmentByTextInfo
    {

        #region Constructor

        public EquipmentByTextInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public byte EquipmentId
        {
            get
            {
                return this._equipmentId;
            }
            set
            {
                this._equipmentId = value;
            }
        }

        [DataMember]
        public string EquipmentName
        {
            get
            {
                return this._equipmentName;
            }
            set
            {
                this._equipmentName = value;
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

        private byte _equipmentId;
        private string _equipmentName;
        private bool _active;
        private int _rowNumber;
        private int _total;

    }
}
