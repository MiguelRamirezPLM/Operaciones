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
    public class PharmacologicalContraindicationInfo
    {
         #region Constructor
        public PharmacologicalContraindicationInfo() { }
         #endregion
        [DataMember]
        public int PharmaContraindicationId
        {
            get { return this._pharmaContraindicationId; }
            set { this._pharmaContraindicationId = value; }
        }
        [DataMember]
        public string PharmaContraindicationName
        {
            get { return this._pharmaContraindicationName; }
            set { this._pharmaContraindicationName = value; }
        }
        private int _pharmaContraindicationId;
        private string _pharmaContraindicationName;
    }
}
