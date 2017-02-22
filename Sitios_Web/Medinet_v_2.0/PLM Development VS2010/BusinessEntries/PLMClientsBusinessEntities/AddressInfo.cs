using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Client address.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class AddressInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AddressInfo class. Not receive parameters.
        /// </summary>
        public AddressInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AddressId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AddressId
        {
            get { return this._addressId; }
            set { this._addressId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Street.
        ///     </para>
        ///     <para>
        ///         Street name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Street
        {
            get { return this._street; }
            set { this._street = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalNumber.
        ///     </para>
        ///     <para>
        ///         Home internal number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InternalNumber
        {
            get { return this._internalNumber; }
            set { this._internalNumber = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Suburb.
        ///     </para>
        ///     <para>
        ///         Suburb name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Suburb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         Home zipCode.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ZipCode
        {
            get { return this._zipCode; }
            set { this._zipCode = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Location.
        ///     </para>
        ///     <para>
        ///         City hall.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

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
        ///         Property which gets or sets a value for StateId.
        ///     </para>
        ///     <para>
        ///         State identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? StateId
        {
            get { return this._stateId; }
            set { this._stateId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for StateName.
        ///     </para>
        ///     <para>
        ///         State name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string StateName
        {
            get { return this._stateName; }
            set { this._stateName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Lada.
        ///     </para>
        ///     <para>
        ///         Long distance code.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Lada
        {
            get
            {
                return this._lada;
            }
            set
            {
                this._lada = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneOne.
        ///     </para>
        ///     <para>
        ///         Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneOne
        {
            get
            {
                return this._phoneOne;
            }
            set
            {
                this._phoneOne = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneTwo.
        ///     </para>
        ///     <para>
        ///         Second Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneTwo
        {
            get
            {
                return this._phoneTwo;
            }
            set
            {
                this._phoneTwo = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Ext.
        ///     </para>
        ///     <para>
        ///         Extension number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Ext
        {
            get
            {
                return this._ext;
            }
            set
            {
                this._ext = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Latitude.
        ///     </para>
        ///     <para>
        ///         Latitude position.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? Latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                this._latitude = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Longitude.
        ///     </para>
        ///     <para>
        ///         Longitude position.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? Longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                this._longitude = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Address is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _addressId;
        private string _street;
        private string _internalNumber;
        private string _suburb;
        private string _zipCode;
        private string _location;
        private byte _countryId;
        private int? _stateId;
        private string _stateName;
        private string _lada;
        private string _phoneOne;
        private string _phoneTwo;
        private string _ext;
        private decimal? _latitude;
        private decimal? _longitude;
        private bool _active;

    }
}
