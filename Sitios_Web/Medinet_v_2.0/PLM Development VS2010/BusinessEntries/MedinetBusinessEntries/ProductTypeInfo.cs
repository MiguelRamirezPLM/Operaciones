using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the ProductType information.
    /// </summary>
    
    [DataContract]
    public class ProductTypeInfo
    {
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductTypeId.
        ///     </para>
        ///     <para>
        ///         Indicates if the identifier from the Type of product.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductTypeId
        {
            get { return this._productTypeId; }
            set { this._productTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Indicates if the name from the Type of product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get { return this._typeName; }
            set { this._typeName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeProduct if is active.
        ///     </para>
        ///     <para>
        ///         Indicates if the status from the TypeProduct.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        private int _productTypeId;
        private string _typeName;
        private bool _active;
    }
}
