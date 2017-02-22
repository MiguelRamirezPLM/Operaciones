using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Client information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ClientInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientInfo class. Not receive parameters.
        /// </summary>
        public ClientInfo() 
        {
            this._birthday = null;
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
        ///         Surname.
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
        ///         Second surname.
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
        ///         Date of birth of Client.
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
        ///         Client Email.
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
        ///         Property which gets or sets a value for Phone.
        ///     </para>
        ///     <para>
        ///         Client Phone.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Password.
        ///     </para>
        ///     <para>
        ///         Client password.
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
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier associated with the Client.
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
        ///         Property which gets or sets a value for StateId.
        ///     </para>
        ///     <para>
        ///         State identifier associated with the Client.
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
        ///         Property which gets or sets a value for LocationId.
        ///     </para>
        ///     <para>
        ///         Location identifier associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? LocationId
        {
            get { return this._locationId; }
            set { this._locationId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SuburbId.
        ///     </para>
        ///     <para>
        ///         Suburb identifier associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? SuburbId
        {
            get { return this._suburbId; }
            set { this._suburbId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCodeId.
        ///     </para>
        ///     <para>
        ///         ZipCode identifier associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ZipCodeId
        {
            get { return this._zipCodeId; }
            set { this._zipCodeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Age.
        ///     </para>
        ///     <para>
        ///         Client age.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Age
        {
            get
            {
                return this._age;
            }
            set
            {
                this._age = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         Home zip code.
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
            get { return this._entrySourceId; }
            set { this._entrySourceId = value; }
        }

        #endregion

        private int _clientId;
        private string _firstName;
        private string _lastName;
        private string _secondLastName;
        private char _gender;
        private DateTime? _birthday;
        private string _email;
        private string _password;
        private DateTime _addedDate;
        private DateTime _lastUpdate;
        private string _completeName;
        private int? _countryId;
        private int? _stateId;
        private int? _locationId;
        private int? _suburbId;
        private int? _zipCodeId;
        private string _age;
        private string _zipCode;
        private bool _active;
        private int _entrySourceId;
        private string _phoneNumber;
    }
}
