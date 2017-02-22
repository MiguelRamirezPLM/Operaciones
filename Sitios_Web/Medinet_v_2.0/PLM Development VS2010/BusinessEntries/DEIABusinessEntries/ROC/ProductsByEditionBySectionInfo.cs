using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class ProductsByEditionBySectionInfo
    {

        #region Constructors

        public ProductsByEditionBySectionInfo() { }

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
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
            }
        }

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
        public string EPSDescription
        {
            get
            {
                return this._epsDescription;
            }
            set
            {
                this._epsDescription = value;
            }
        }

        #endregion

        private int _productId;
        private int? _parentId;
        private byte _productTypeId;
        private int _companyId;
        private string _productName;
        private string _productDescription;
        private int _editionId;
        private int _sectionId;
        private string _htmlFile;
        private string _htmlContent;
        private string _epsDescription;

    }
}
