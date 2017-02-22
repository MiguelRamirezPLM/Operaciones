using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public  class ServiceByCompanyInfo
    {
        #region Constructors

        public ServiceByCompanyInfo() { }

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
        public int ProductTypeId
        {
            get { return this._productTypeId; }
            set { this._productTypeId = value; }
        }

        [DataMember]
        public int CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

      
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public int EdProdId
        {
            get { return this._edProdId; }
            set { this._edProdId = value; }
        }


        [DataMember]
        public string HtmlFile
        {
            get { return this._htmlFile; }
            set { this._htmlFile = value; }
        }

        [DataMember]
        public string HtmlContent
        {
            get { return this._htmlContent; }
            set { this._htmlContent = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }


        [DataMember]
        public int CompanyTypeId
        {
            get { return this._companyTypeId; }
            set { this._companyTypeId = value; }
        }

      

        #endregion 

        private int _productId;
        private string _productName;
        private int _productTypeId;
        private int _companyId;
        private string _description;
        private int _edProdId;
        private string _htmlFile;
        private string _htmlContent;
        private string  _companyName;
        private int _companyTypeId;
  
    }

   
}
