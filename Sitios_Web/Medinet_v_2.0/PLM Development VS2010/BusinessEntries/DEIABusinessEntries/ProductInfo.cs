using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
{
    [DataContract]
    public class ProductInfo
    {

        #region Constructor

        public ProductInfo() { }

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
        public int? Product_Id
        {
            get
            {
                return this._product_Id;
            }
            set
            {
                this._product_Id = value;
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

        #endregion

        private int _productId;
        private int? _parentId;
        private int? _productTypeId;
        private string _productName;
        private int _companyId;
        private int? _product_Id;
        private bool _active;

    }
}
