﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Pharmacy's users information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PharmacyUserInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacyUserInfo class. Not receive parameters.
        /// </summary>
        public PharmacyUserInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyUserId.
        ///     </para>
        ///     <para>
        ///         User identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyUserId
        {
            get
            {
                return this._companyUserId;
            }
            set
            {
                this._companyUserId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company identifier. Company to which the User belongs.
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
        ///         Property which gets or sets a value for FirstName.
        ///     </para>
        ///     <para>
        ///         User name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastName.
        ///     </para>
        ///     <para>
        ///         User surname.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UserName.
        ///     </para>
        ///     <para>
        ///         User Nickname.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UserPassword.
        ///     </para>
        ///     <para>
        ///         User password.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UserPassword
        {
            get
            {
                return this._userPassword;
            }
            set
            {
                this._userPassword = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier. Code assigned to user.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get
            {
                return this._codeId;
            }
            set
            {
                this._codeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeString.
        ///     </para>
        ///     <para>
        ///         Code string. Code assigned to user.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CodeString
        {
            get
            {
                return this._codeString;
            }
            set
            {
                this._codeString = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the User is enabled or disabled.
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

        private int _companyUserId;
        private int _companyClientId;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _userPassword;
        private int _codeId;
        private string _codeString;
        private bool _active;
    }
}