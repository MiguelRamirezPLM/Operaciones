using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class ProductByCategoryAndDivisionInfo
    {
        #region Constructors

        public ProductByCategoryAndDivisionInfo() { }

        #endregion Constructors
    

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
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

    #endregion Properties

        private int _productId;
        private string _productName;
        private string _description;
        private int _laboratoryId;

}
}