using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ProductClientInfo
    {
        #region Constructors

        public ProductClientInfo() { }

        #endregion

        #region Properties

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
        public int? SubProductId
        {
            get
            {
                return this._subProductId;
            }
            set
            {
                this._subProductId = value;
            }
        }

        [DataMember]
        public string SubProductName
        {
            get
            {
                return this._subProductName;
            }
            set
            {
                this._subProductName = value;
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
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
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

        #endregion

        private int _total;
        private int _productId;
        private string _productName;
        private int? _subProductId;
        private string _subProductName;
        private int _rowNumber;
        private int _clientId;
        private string _companyName;
    }
}
