using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the client professional information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.QuestionnairesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SummariesInfo"/>
    public class LicenseClientInfo : ClientInfo
    {
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
        public string Profession
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
        public string Speciality
        {
            get { return this._speciality ;}
            set { this._speciality = value;}
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
        ///         Property which gets or sets a value for prefix.
        ///     </para>
        ///     <para>
        ///         Alternative speciality associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Prefix
        {
            get { return this._prefix; }
            set { this._prefix = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for code.
        ///     </para>
        ///     <para>
        ///         Alternative speciality associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Code
        {
            get { return this._code; }
            set { this._code = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for StateName.
        ///     </para>
        ///     <para>
        ///         State Name associated with the Client.
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
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country Name associated with the Client.
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
        ///         Property which gets or sets a value for ShortNameState.
        ///     </para>
        ///     <para>
        ///         Short Name State associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortNameState
        {
            get { return this._shortNameState; }
            set { this._shortNameState = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortNameCountry.
        ///     </para>
        ///     <para>
        ///         Short Name Country associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortNameCountry
        {
            get { return this._shortNameCountry; }
            set { this._shortNameCountry = value; }
        }
        #endregion

        private string _profession;
        private string _otherProfession;
        private string _speciality;
        private string _otherSpeciality;
        private string _professionalLicense;
        private string _prefix;
        private string _code;
        private string _stateName;
        private string _countryName;
        private string _shortNameState;
        private string _shortNameCountry;

    }
}
