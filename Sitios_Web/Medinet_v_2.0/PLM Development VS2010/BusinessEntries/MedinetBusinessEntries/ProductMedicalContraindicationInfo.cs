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
    public class ProductMedicalContraindicationInfo
    {
        #region Constructor
        public ProductMedicalContraindicationInfo() { }
        #endregion

        [DataMember]
        public int MedicalContraindicationId
        {
            get { return this._medicalContraindicationId; }
            set { this._medicalContraindicationId= value; }
        }
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }
        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }
        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }
        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        [DataMember]
        public int ActiveSubstanceId
        {
            get { return this._activeSubstanceId; }
            set { this._activeSubstanceId = value; }
        }

        [DataMember]
        public string ActiveSubstance
        {
            get { return this._activeSubstance; }
            set { this._activeSubstance = value; }
        }

        [DataMember]
        public string MedicalContraindicationName
        {
            get { return this._medicalContraindicationName; }
            set { this._medicalContraindicationName = value; }
        }

        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private int _medicalContraindicationId;
        private int _activeSubstanceId;
        private string _activeSubstance;
        private string _medicalContraindicationName;
    }
}
