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
    public class ProductPhysiologicalContraindicationInfo
    {
         #region Constructor
        public ProductPhysiologicalContraindicationInfo() { }
        #endregion

        [DataMember]
        public int PhysContraindicationId
        {
            get { return this._physContraindicationId; }
            set { this._physContraindicationId= value; }
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
        public string PhysContraindicationName
        {
            get { return this._physContraindicationName; }
            set { this._physContraindicationName = value; }
        }

        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private int _physContraindicationId;
        private int _activeSubstanceId;
        private string _activeSubstance;
        private string _physContraindicationName;
    }
}
