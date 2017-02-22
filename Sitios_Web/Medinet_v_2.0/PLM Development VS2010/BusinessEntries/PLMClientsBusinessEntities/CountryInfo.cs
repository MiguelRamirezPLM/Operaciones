using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Country information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class CountryInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CountryInfo class. Not receive parameters.
        /// </summary>
        public CountryInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country name.
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
        ///         Country code.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? CountryCode
        {
            get { return this._countryCode; }
            set { this._countryCode = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ID.
        ///     </para>
        ///     <para>
        ///         Country key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Country is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _countryId;
        private string _countryName;
        private byte? _countryCode;
        private string _id;
        private bool _active;

    }
}
