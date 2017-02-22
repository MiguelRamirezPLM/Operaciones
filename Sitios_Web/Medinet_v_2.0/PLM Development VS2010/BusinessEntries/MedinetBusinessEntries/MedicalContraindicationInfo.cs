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
    public class MedicalContraindicationInfo
    {
         #region Constructor
        public MedicalContraindicationInfo() { }
         #endregion
        [DataMember]
        public int MedicalContraindicationId
        {
            get { return this._medicalContraindicationId; }
            set { this._medicalContraindicationId = value; }
        }
        [DataMember]
        public string MedicalContraindicationName
        {
            get { return this._medicalContraindicationName; }
            set { this._medicalContraindicationName = value; }
        }

        private int _medicalContraindicationId;
        private string _medicalContraindicationName;
        
    }
}
