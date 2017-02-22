using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the user information web application's.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class WebApplicationUsersInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the WebApplicationUsersInfo class. Not receive parameters.
        /// </summary>
        public WebApplicationUsersInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Company name. Company to which the User belongs.
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
        ///         Property which gets or sets a value for CCTypeId.
        ///     </para>
        ///     <para>
        ///         Company Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CCTypeId
        {
            get
            {
                return this._ccTypeId;
            }
            set
            {
                this._ccTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CCTypeName.
        ///     </para>
        ///     <para>
        ///         Company Type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CCTypeName
        {
            get
            {
                return this._ccTypeName;
            }
            set
            {
                this._ccTypeName = value;
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
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition identifier. Edition which the user have access.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
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

        private int _companyClientId;
        private string _companyName;
        private byte _ccTypeId;
        private string _ccTypeName;
        private int _codeId;
        private string _codeString;
        private int _editionId;
        private bool _active;

    }
}
