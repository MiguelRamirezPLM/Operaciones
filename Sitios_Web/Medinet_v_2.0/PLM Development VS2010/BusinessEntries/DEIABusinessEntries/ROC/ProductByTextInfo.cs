using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class ProductByTextInfo
    {

        #region Constructor

        public ProductByTextInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SectionId
        {
            get
            {
                return this._sectionId;
            }
            set
            {
                this._sectionId = value;
            }
        }

        [DataMember]
        public string SectionName
        {
            get
            {
                return this._sectionName;
            }
            set
            {
                this._sectionName = value;
            }
        }

        [DataMember]
        public int ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        [DataMember]
        public int? ParentProductId
        {
            get
            {
                return this._parentProductId;
            }
            set
            {
                this._parentProductId = value;
            }
        }

        [DataMember]
        public int ProductTypeId
        {
            get
            {
                return this._productTypeId;
            }
            set
            {
                this._productTypeId = value;
            }
        }

        [DataMember]
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        [DataMember]
        public string ProductDescription
        {
            get
            {
                return this._productDescription;
            }
            set
            {
                this._productDescription = value;
            }
        }

        [DataMember]
        public string HmtlFile
        {
            get
            {
                return this._htmlFile;
            }
            set
            {
                this._htmlFile = value;
            }
        }

        [DataMember]
        public int CompanyId
        {
            get
            {
                return this._companyId;
            }
            set
            {
                this._companyId = value;
            }
        }

        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        [DataMember]
        public int CompanyTypeId
        {
            get
            {
                return this._companyTypeId;
            }
            set
            {
                this._companyTypeId = value;
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

        private int _sectionId;
        private string _sectionName;
        private int _productId;
        private int? _parentProductId;
        private int _productTypeId;
        private string _productName;
        private string _productDescription;
        private string _htmlFile;
        private int _companyId;
        private string _companyName;
        private int _companyTypeId;
        private int _rowNumber;
        private int _total;

    }
}
