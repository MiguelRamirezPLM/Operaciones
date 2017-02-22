using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ProductsByEditionInfo
    {
        #region Constructors

        public ProductsByEditionInfo() { }

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

       


        #endregion

        private int _productId;
        private string _productName;
        private int? _subProductId;
        private string _subProductName;
      

    }
}
