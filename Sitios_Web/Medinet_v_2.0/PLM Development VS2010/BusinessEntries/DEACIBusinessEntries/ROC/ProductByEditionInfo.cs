using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public class ProductByEditionInfo
    {

        #region Constructor

        public ProductByEditionInfo() { }

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
        public int? ProductIdParent
        {
            get
            {
                return this._productIdParent;
            }
            set
            {
                this._productIdParent = value;
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
        public int ProdId
        {
            get
            {
                return this._prodId;
            }
            set
            {
                this._prodId = value;
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
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
        public int? SectionIdParent
        {
            get
            {
                return this._sectionIdParent;
            }
            set
            {
                this._sectionIdParent = value;
            }
        }

        #endregion

        private int _productId;
        private int? _productIdParent;
        private byte _productTypeId;
        private string _productName;
        private string _description;
        private int _companyId;
        private int _prodId;
        private bool _active;
        private string _htmlFile;
        private int? _sectionIdParent;

    }
}
