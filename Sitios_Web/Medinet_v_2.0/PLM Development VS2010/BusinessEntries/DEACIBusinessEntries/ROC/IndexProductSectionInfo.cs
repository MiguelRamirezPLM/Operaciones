using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public class IndexProductSectionInfo
    {

        #region Constructor

        public IndexProductSectionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        [DataMember]
        public string  ProductName
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
        public int CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

        [DataMember]
        public string CompanyShortName
        {
            get { return this._companyShortName; }
            set { this._companyShortName = value; }
        }

        [DataMember]
        public int ProdId
        {
            get { return this._prodId; }
            set { this._prodId = value; }
        }


        #endregion

        private int _productId;
        private string _productName;
        private string  _description;
        private int _companyId;
        private string _companyName;
        private string _companyShortName;
        private int _prodId;

    }
}