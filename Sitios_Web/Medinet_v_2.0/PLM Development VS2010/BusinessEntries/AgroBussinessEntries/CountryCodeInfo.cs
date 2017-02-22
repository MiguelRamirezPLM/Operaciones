using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a Country Code.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.CountriesInfo"/>
    [DataContract]
    public class CountryCodeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CountryCodeInfo class. Not receive parameters.
        /// </summary>
        public CountryCodeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryCodeId.
        ///     </para>
        ///     <para>
        ///         Identifier of the Country Code.
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
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country Identifier.
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
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CountryName
        {
            get { return this._countryName; }
            set { this._countryName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryCode.
        ///     </para>
        ///     <para>
        ///         Code number.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryCode
        {
            get { return this._countryCode; }
            set { this._countryCode = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the CountryCode is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _countryCodeId;
        private int _countryId;
        private string _countryName;
        private byte _countryCode;
        private bool _active;

    }
}
