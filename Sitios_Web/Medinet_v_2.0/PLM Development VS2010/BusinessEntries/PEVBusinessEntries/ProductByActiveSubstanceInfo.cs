using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class ProductByActiveSubstanceInfo
    {
        #region Constructors
        
        public ProductByActiveSubstanceInfo() { }

        #endregion 

        #region Properties

        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        [DataMember]
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public string LaboratoryName
        {
            get { return this._laboratoryName; }
            set { this._laboratoryName = value; }
        }


        #endregion

        private int _productId;
        private string _productName;
        private string _description;
        private string _laboratoryName;
    }

}
