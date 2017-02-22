using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Event detail.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EventsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ProfessionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SpecialityInfo"/>
    [DataContract]

    public class EventCongressInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventsDetailInfo class. Not receive parameters.
        /// </summary>
        public EventCongressInfo() 
        {
            _initialDate = Convert.ToDateTime("01/01/1900");
            _finalDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EventId.
        ///     </para>
        ///     <para>
        ///         Event identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EventId
        {
            get
            {
                return this._eventId;
            }
            set
            {
                this._eventId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EventName.
        ///     </para>
        ///     <para>
        ///         Event name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EventName
        {
            get
            {
                return this._eventName;
            }
            set
            {
                this._eventName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeId.
        ///     </para>
        ///     <para>
        ///         Event type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
            }
        }

       

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Site.
        ///     </para>
        ///     <para>
        ///         Host event.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InitialDate.
        ///     </para>
        ///     <para>
        ///         Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get
            {
                return this._initialDate;
            }
            set
            {
                this._initialDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         End date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
        {
            get
            {
                return this._finalDate;
            }
            set
            {
                this._finalDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Organizer.
        ///     </para>
        ///     <para>
        ///         Organizer name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Organizer
        {
            get
            {
                return this._organizer;
            }
            set
            {
                this._organizer = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WebPage.
        ///     </para>
        ///     <para>
        ///         Event website.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WebPage
        {
            get
            {
                return this._webPage;
            }
            set
            {
                this._webPage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Event is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AddressId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? AddressId
        {
            get
            {
                return this._addressId;
            }
            set
            {
                this._addressId = value;
            }
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
            get
            {
                return this._street;
            }
            set
            {
                this._street = value;
            }
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
            get
            {
                return this._internalNumber;
            }
            set
            {
                this._internalNumber = value;
            }
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
            get
            {
                return this._suburb;
            }
            set
            {
                this._suburb = value;
            }
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
            get
            {
                return this._zipCode;
            }
            set
            {
                this._zipCode = value;
            }
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
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
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
        public byte? CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                this._countryId = value;
            }
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
            get
            {
                return this._stateId;
            }
            set
            {
                this._stateId = value;
            }
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
            get
            {
                return this._stateName;
            }
            set
            {
                this._stateName = value;
            }
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
        ///         Property which gets or sets a value for CategoryName.
        ///     </para>
        ///     <para>
        ///         Category name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                this._categoryName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
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
            get
            {
                return this._countryName;
            }
            set
            {
                this._countryName = value;
            }
        }
        #endregion

        private int _eventId;
        private string _eventName;
        private byte _typeId;
        private string _site;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private string _organizer;
        private string _webPage;
        private bool _active;
        private int? _addressId;
        private string _street;
        private string _internalNumber;
        private string _suburb;
        private string _zipCode;
        private string _location;
        private byte? _countryId;
        private int? _stateId;
        private string _stateName;
        private string _lada;
        private string _phoneOne;
        private string _phoneTwo;
        private string _ext;
        private decimal? _latitude;
        private decimal? _longitude;
        private string _categoryName;
        private int _categoryId;
        private string _countryName;
    }
}

