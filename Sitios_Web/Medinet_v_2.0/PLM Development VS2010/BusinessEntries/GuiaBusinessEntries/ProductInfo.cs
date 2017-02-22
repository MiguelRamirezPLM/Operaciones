using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ProductInfo
    {
        #region Constructors

        public ProductInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        [DataMember]
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

       
        #endregion

        private int _productId;
        private int? _parentId;
        private string _productName;
        private bool _active;
        

    }
}
