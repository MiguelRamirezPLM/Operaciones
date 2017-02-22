using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Products and Therapeutic Indications.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.IndicationInfo"/>
    [DataContract]
    public class ProductIndicationInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductIndicationInfo class. Not receive parameters.
        /// </summary>
        public ProductIndicationInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IndicationId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Indication Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IndicationId
        {
            get { return this._indicationId; }
            set { this._indicationId = value; }
        }

        #endregion

        private int _productId;
        private int _indicationId;
    }
}
