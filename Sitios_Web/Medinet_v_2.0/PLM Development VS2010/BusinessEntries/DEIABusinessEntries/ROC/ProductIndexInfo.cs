using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class ProductIndexInfo
    {

        #region Constructor

        public ProductIndexInfo() { }

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
        public int? ProductTypeId
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
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
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

        [DataMember]
        public int EdProdId
        {

            get 
            {
                return this._edProdId;
            }
            set
            {

                this._edProdId = value;
            }
        }

        #endregion

        private int _edProdId;
        private int _productId;
        private int? _productTypeId;
        private string _productName;
        private string _description;
        private int _companyId;
        private string _htmlFile;
        private string _htmlContent;
        private int _rowNumber;
        private int _total;
    
    }
}
