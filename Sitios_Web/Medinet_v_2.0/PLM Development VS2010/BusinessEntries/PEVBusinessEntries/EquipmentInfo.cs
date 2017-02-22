using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class EquipmentInfo
    {

        #region Constructor

        public EquipmentInfo() { }

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

        #endregion

        private byte _equipmentId;
        private string _equipmentName;
        private bool _active;

    }
}
