using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries.ROC
{
    [DataContract]
    public class ProductContentInfo
    {
        #region Constructors

        public ProductContentInfo() { }

        #endregion Constructors

        #region Properties

        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        [DataMember]
        public int PharmaFormsId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
        }

        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
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
        public string HTMLContent
        {
            get { return this._htmlContent; }
            set { this._htmlContent = value; }
        }

        [DataMember]
        public string Page
        {
            get { return this._page; }
            set { this._page = value; }
        }


        [DataMember]
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        [DataMember]
        public byte CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion Properties

        private int _productId;
        private int _pharmaFormId;
        private int _editionId;
        private int _divisionId;
        private int _categoryId;
        private string _htmlContent;
        private string _page;
        private string _productName;
        private byte _countryId;
        private int _laboratoryId;
        private string _description;
        private bool _active;
        

    }
}
