using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class ProductBySectionInfo
    {

        #region Construtor
        
        public ProductBySectionInfo() { }
        
        #endregion

        #region Properties

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
        public int? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        [DataMember]
        public byte ProductTypeId
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
        public int? CompanyId
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
        public string HtmlFile
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
        public string HtmlContent
        {
            get
            {
                return this._htmlContent;
            }
            set
            {
                this._htmlContent = value;
            }
        }

        #endregion

        private int _productId;
        private int? _parentId;
        private byte _productTypeId;
        private string _productName;
        private string _productDescription;
        private int? _companyId;
        private string _companyName;
        private string _htmlFile;
        private string _htmlContent;

    }
}
