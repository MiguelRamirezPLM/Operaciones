using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the IppNumber Information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         IPP.- Prescribing information.
    ///     </para>
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CountriesInfo"/>
    [DataContract]
    public class IPPNumbersInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the IPPNumbersInfo class. Not receive parameters.
        /// </summary>
        public IPPNumbersInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IppId.
        ///     </para>
        ///     <para>
        ///         IPP Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IppId
        {
            get { return this._ippId; }
            set { this._ippId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country Identifier associated with the IPP.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Indentifier associated with the IPP.
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
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Description for the IPP.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the IPPNumber is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _ippId;
        private int _countryId;
        private int _productId;
        private string _description;
        private bool _active;

    }
}
