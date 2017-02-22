using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship of a Product with Active Substances.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    [DataContract]
    public class SubstanceByProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SubstanceByProductInfo class. Not receive parameters.
        /// </summary>
        public SubstanceByProductInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         Active Substance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get { return this._activeSubstanceId; }
            set { this._activeSubstanceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substance.
        ///     </para>
        ///     <para>
        ///         Name or description of the Active Substance.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Substance
        {
            get { return this._substance; }
            set { this._substance = value; }
        }

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
            get { return this.productId; }
            set { this.productId = value; }
        }

        #endregion

        private int _activeSubstanceId;
        private string _substance;
        private int productId;
    }
}
