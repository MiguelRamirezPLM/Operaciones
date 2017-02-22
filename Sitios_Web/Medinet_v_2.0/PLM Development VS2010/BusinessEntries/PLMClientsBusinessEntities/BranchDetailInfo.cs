using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Company Branch information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.AddressInfo"/>
    [DataContract]
    public class BranchDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BranchDetailInfo class. Not receive parameters.
        /// </summary>
        public BranchDetailInfo() { }

        #endregion

       #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchId.
        ///     </para>
        ///     <para>
        ///         Branch identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BranchId
        {
            get
            {
                return this._branchId;
            }
            set
            {
                this._branchId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyClientId
        {
            get
            {
                return this._companyClientId;
            }
            set
            {
                this._companyClientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Company Client name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchKey.
        ///     </para>
        ///     <para>
        ///         Branch key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BranchKey
        {
            get
            {
                return this._branchKey;
            }
            set
            {
                this._branchKey = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchName.
        ///     </para>
        ///     <para>
        ///         Branch name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BranchName
        {
            get
            {
                return this._branchName;
            }
            set
            {
                this._branchName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WebPage.
        ///     </para>
        ///     <para>
        ///         Web site address.
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
        ///         Property which gets or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         Branch email.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Logo Repository.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Logo.
        ///     </para>
        ///     <para>
        ///         Logo name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LogoDetail.
        ///     </para>
        ///     <para>
        ///         Detail logo name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LogoDetail
        {
            get
            {
                return this._logoDetail;
            }
            set
            {
                this._logoDetail = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the Branch is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool BranchActive
        {
            get
            {
                return this._branchActive;
            }
            set
            {
                this._branchActive = value;
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
        ///         Property which gets or sets a value for AttentionSchedules.
        ///     </para>
        ///     <para>
        ///         Indicates the attention's schedules to client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttentionSchedules
        {
            get
            {
                return this._attentionSchedules;
            }
            set
            {
                this._attentionSchedules = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HomeService.
        ///     </para>
        ///     <para>
        ///         Indicates the service's schedules to domicile.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HomeService
        {
            get
            {
                return this._homeService;
            }
            set
            {
                this._homeService = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ServiceType.
        ///     </para>
        ///     <para>
        ///         Indicates the service type that the branch offers.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ServiceType
        {
            get
            {
                return this._serviceType;
            }
            set
            {
                this._serviceType = value;
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
        ///         Property which gets or sets a value for BranchDistance.
        ///     </para>
        ///     <para>
        ///         Distance between the Branch and the Client.
        ///     </para>
        ///     <para>
        ///        Distance shown in kilometers (km).
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? BranchDistance
        {
            get
            {
                return this._branchDistance;
            }
            set
            {
                this._branchDistance = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DisplayPharmacies.
        ///     </para>
        ///     <para>
        ///         Pharmacy number to display in the searcher.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? DisplayPharmacies
        {
            get
            {
                return this._displayPharmacies;
            }
            set
            {
                this._displayPharmacies = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyPromotions.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.PromotionsInfo"/>. Indicates if the Branch has promotions.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.PromotionsInfo> CompanyPromotions
        {
            get
            {
                return this._companyPromotions;
            }
            set
            {
                this._companyPromotions = new List<PLMClientsBusinessEntities.PromotionsInfo>();

                foreach (PLMClientsBusinessEntities.PromotionsInfo variable in value)
                    this._companyPromotions.Add(variable);
            }
        }

        #endregion

        private int _branchId;
        private int _companyClientId;
        private string _companyName;
        private string _branchKey;
        private string _branchName;
        private string _webPage;
        private string _email;
        private string _baseUrl;
        private string _logo;
        private string _logoDetail;
        private bool _branchActive;
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
        private string _attentionSchedules;
        private string _homeService;
        private string _serviceType;
        private decimal? _latitude;
        private decimal? _longitude;
        private decimal? _branchDistance;
        private int? _displayPharmacies;
        private List<PromotionsInfo> _companyPromotions;
        
    }
}
