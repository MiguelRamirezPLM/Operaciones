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
    /// <seealso cref="PLMClientsBusinessEntities.CompanyClientsInfo"/>
    [DataContract]
    public class CompanyClientBranchesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CompanyClientBranchesInfo class. Not receive parameters.
        /// </summary>
        public CompanyClientBranchesInfo() { }

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
        ///         Property which gets or sets a value for AddressId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AddressId
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Branch is enabled or disabled.
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

        #endregion

        private int _branchId;
        private string _branchKey;
        private string _branchName;
        private string _webPage;
        private string _email;
        private string _baseUrl;
        private string _logo;
        private int _companyClientId;
        private int _addressId;
        private string _attentionSchedules;
        private string _homeService;
        private bool _active;

    }
}
