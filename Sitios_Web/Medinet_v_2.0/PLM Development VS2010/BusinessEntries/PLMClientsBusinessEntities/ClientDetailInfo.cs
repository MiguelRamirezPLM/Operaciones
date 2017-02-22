using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the detail of information about a Client, which inherits from the class ClientInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ProfessionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SpecialityInfo"/>
    [DataContract]
    public class ClientDetailInfo : ClientInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientDetailInfo class. Not receive parameters.
        /// </summary>
        public ClientDetailInfo() 
        {
            this._profession = null;
            this._otherProfession = null;
            this._speciality = null;
            this._otherSpeciality = null;
            this._professionalLicense = null;
        }

        /// <summary>
        ///     Initializes a new instance of the ClientDetailInfo class.
        /// </summary>
        /// <param name="clientInfo">
        ///     Basic data pertaining to a customer
        /// </param>
        public ClientDetailInfo(ClientInfo clientInfo)
        {
            this.ClientId = clientInfo.ClientId;
            this.FirstName = clientInfo.FirstName;
            this.LastName = clientInfo.LastName;
            this.SecondLastName = clientInfo.SecondLastName;
            this.CompleteName = clientInfo.CompleteName;
            this.Gender = clientInfo.Gender;
            this.Birthday = clientInfo.Birthday;
            this.Email = clientInfo.Email;
            this.AddedDate = clientInfo.AddedDate;
            this.LastUpdate = clientInfo.LastUpdate;
            this.Active = clientInfo.Active;
            this.CountryId = clientInfo.CountryId;
            this.StateId = clientInfo.StateId;
            this.Age = clientInfo.Age;
            this.LocationId = clientInfo.LocationId;
            this.SuburbId = clientInfo.SuburbId;
            this.ZipCodeId = clientInfo.ZipCodeId;
            
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Profession.
        ///     </para>
        ///     <para>
        ///         It is an object of type <see cref="PLMClientsBusinessEntities.ProfessionInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public ProfessionInfo Profession
        {
            get { return this._profession; }
            set { this._profession = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherProfession.
        ///     </para>
        ///     <para>
        ///         Other profession associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherProfession
        {
            get { return this._otherProfession; }
            set { this._otherProfession = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProfessionalLicense.
        ///     </para>
        ///     <para>
        ///         Professional license associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProfessionalLicense
        {
            get { return this._professionalLicense; }
            set { this._professionalLicense = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Speciality.
        ///     </para>
        ///     <para>
        ///         It is an object of type <see cref="PLMClientsBusinessEntities.SpecialityInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public SpecialityInfo Speciality
        {
            get { return this._speciality; }
            set { this._speciality = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherSpeciality.
        ///     </para>
        ///     <para>
        ///         Alternative speciality associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherSpeciality
        {
            get { return this._otherSpeciality; }
            set { this._otherSpeciality = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceId.
        ///     </para>
        ///     <para>
        ///         Residence level identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? ResidenceId
        {
            get
            {
                return this._residenceId;
            }
            set
            {
                this._residenceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceKey.
        ///     </para>
        ///     <para>
        ///         Residence level key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ResidenceKey
        {
            get
            {
                return this._residenceKey;
            }
            set
            {
                this._residenceKey = value;
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
        ///         Property which gets or sets a value for StateShortName.
        ///     </para>
        ///     <para>
        ///         State Short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string StateShortName
        {
            get
            {
                return this._stateShortName;
            }
            set
            {
                this._stateShortName = value;
            }
        }

        #endregion

        private ProfessionInfo _profession;
        private string _otherProfession;
        private SpecialityInfo _speciality;
        private string _otherSpeciality;
        private string _professionalLicense;
        private byte? _residenceId;
        private string _residenceKey;
        private string _countryName;
        private string _stateName;
        private string _stateShortName;
    }
}
