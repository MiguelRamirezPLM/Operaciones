using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Products and a Country Codes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.CountryCodeInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    [DataContract]
    public class CountryProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CountryProductInfo class. Not receive parameters.
        /// </summary>
        public CountryProductInfo(){}

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryCodeId.
        ///     </para>
        ///     <para>
        ///         Identifier for the CountryCode.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryCodeId
        {
            get { return this._countryCodeId; }
            set { this._countryCodeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
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
            get { return this._productId; }
            set { this._productId = value; }
        }

        #endregion

        private byte _countryCodeId;
        private int _pharmaFormId;
        private int _productId;

    }
}
