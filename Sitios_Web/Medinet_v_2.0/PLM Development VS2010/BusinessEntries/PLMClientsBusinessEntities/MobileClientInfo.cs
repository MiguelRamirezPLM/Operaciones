using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between clients and mobile devices.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class MobileClientInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MobileClientInfo class. Not receive parameters.
        /// </summary>
        public MobileClientInfo() 
        {
            this._birthday = null;
            this._entrySourceId = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FirstName.
        ///     </para>
        ///     <para>
        ///         Client name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastName.
        ///     </para>
        ///     <para>
        ///         Client last name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SecondLastName.
        ///     </para>
        ///     <para>
        ///         Client Second last name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SecondLastName
        {
            get { return this._secondLastName; }
            set { this._secondLastName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompleteName.
        ///     </para>
        ///     <para>
        ///         Client complete name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompleteName
        {
            get { return this._completeName; }
            set { this._completeName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Gender.
        ///     </para>
        ///     <para>
        ///         Client gender.
        ///     </para>
        /// </summary>
        [DataMember]
        public char Gender
        {
            get { return this._gender; }
            set { this._gender = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Birthday.
        ///     </para>
        ///     <para>
        ///         Client's date of birth.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? Birthday
        {
            get { return this._birthday; }
            set { this._birthday = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         Client email.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Password.
        ///     </para>
        ///     <para>
        ///         Password client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AddedDate.
        ///     </para>
        ///     <para>
        ///         Registration date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime AddedDate
        {
            get { return this._addedDate; }
            set { this._addedDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastUpdate.
        ///     </para>
        ///     <para>
        ///         Date of last update.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime LastUpdate
        {
            get { return this._lastUpdate; }
            set { this._lastUpdate = value; }
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
        public int? CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Client is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntrySourceId.
        ///     </para>
        ///     <para>
        ///         Entry source identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EntrySourceId
        {
            set { this._entrySourceId = value; }
            get { return this._entrySourceId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Profession.
        ///     </para>
        ///     <para>
        ///         It is an object of type <see cref="PLMClientsBusinessEntities.Catalogs.Professions"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public Catalogs.Professions Profession
        {
            set { this._profession = value; }
            get { return this._profession; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherProfession.
        ///     </para>
        ///     <para>
        ///         Alternative Profession associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherProfession
        {
            set { this._otherProfession = value; }
            get { return this._otherProfession; }
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
            set { this._professionalLicense = value; }
            get { return this._professionalLicense; }
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
            get{ return this._stateName; }
            set{ this._stateName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for StateShortName.
        ///     </para>
        ///     <para>
        ///         State short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string StateShortName
        {
            get { return this._stateShortName; }
            set { this._stateShortName = value; }
        }

        #endregion

        private int _clientId;
        private string _firstName;
        private string _lastName;
        private string _secondLastName;
        private string _completeName;
        private char _gender;
        private DateTime? _birthday;
        private string _email;
        private string _password;
        private DateTime _addedDate;
        private DateTime _lastUpdate;
        private int _entrySourceId;
        private int? _countryId;
        private bool _active;
        private Catalogs.Professions _profession;
        private string _otherProfession;
        private string _professionalLicense;
        private int? _stateId;
        private string _stateName;
        private string _stateShortName;
    }
}
