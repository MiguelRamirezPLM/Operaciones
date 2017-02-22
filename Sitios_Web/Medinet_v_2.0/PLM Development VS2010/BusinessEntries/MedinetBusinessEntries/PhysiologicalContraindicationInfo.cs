using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MedinetBusinessEntries
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PhysiologicalContraindicationInfo
    {
         #region Constructor
        public PhysiologicalContraindicationInfo() { }
         #endregion
        [DataMember]
        public int PhysContraindicationId
        {
            get { return this._physContraindicationId; }
            set { this._physContraindicationId = value; }
        }
        [DataMember]
        public string PhysContraindicationName
        {
            get { return this._physContraindicationName; }
            set { this._physContraindicationName = value; }
        }
        private int _physContraindicationId;
        private string _physContraindicationName;
    }
}
