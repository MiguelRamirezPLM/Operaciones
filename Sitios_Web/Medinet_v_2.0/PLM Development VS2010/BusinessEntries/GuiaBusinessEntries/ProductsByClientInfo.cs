using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ProductsByClientInfo
    {
        #region Constructors

        public ProductsByClientInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        [DataMember]
        public int ProductId
        {
            get
            {
                return this._ProductId;
            }
            set
            {
                this._ProductId = value;
            }
        }

        [DataMember]
        public string ProductName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this._ProductName = value;
            }
        }

        [DataMember]
        public int? SubProductId
        {
            get
            {
                return this._SubProductId;
            }
            set
            {
                this._SubProductId = value;
            }
        }

        [DataMember]
        public string SubProductName
        {
            get
            {
                return this._SubProductName;
            }
            set
            {
                this._SubProductName = value;
            }
        }

        [DataMember]
        public string Page
        {
            get
            {
                return this._Page;
            }
            set
            {
                this._Page = value;
            }
        }

        [DataMember]
        public int ClientProductId
        {
            get
            {
                return this._ClientProductId;
            }
            set
            {
                this._ClientProductId = value;
            }
        }

        [DataMember]
        public bool ProductActive
        {
            get
            {
                return this._ProductActive;
            }
            set
            {
                this._ProductActive = value;
            }
        }

        [DataMember]
        public bool? SubProductActive
        {
            get
            {
                return this._SubProductActive;
            }
            set
            {
                this._SubProductActive = value;
            }
        }

        [DataMember]
        public bool ProductClientActive
        {
            get
            {
                return this._ProductClientActive;
            }
            set
            {
                this._ProductClientActive = value;
            }
        }

        #endregion

        private int _ClientId;

        private int _ProductId;

        private string _ProductName;

        private int? _SubProductId;

        private string _SubProductName;

        private string _Page;

        private int _ClientProductId;

        private bool _ProductActive;

        private bool? _SubProductActive;

        private bool _ProductClientActive;

    }
}
