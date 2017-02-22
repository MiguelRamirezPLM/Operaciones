using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Code Prefix information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Unique code associated with an application.
    ///     </para>
    /// </remarks>
    [Serializable]
    [DataContract]
    public class CodePrefixInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodePrefixInfo class. Not receive parameters.
        /// </summary>
        public CodePrefixInfo() 
        {
            this._addedDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Code prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get { return this._prefixId; }
            set { this._prefixId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Identifier of source code prefix.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixTypeId.
        ///     </para>
        ///     <para>
        ///         Prefix type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PrefixTypeId
        {
            get { return this._prefixTypeId; }
            set { this._prefixTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClienId.
        ///     </para>
        ///     <para>
        ///         Company client identifier associated with the Code prefix.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyClienId
        {
            get { return this._companyClientId; }
            set { this._companyClientId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixName.
        ///     </para>
        ///     <para>
        ///         Prefix name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixName
        {
            get { return this._prefixName; }
            set { this._prefixName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixValue.
        ///     </para>
        ///     <para>
        ///         Base value for generating codes for clients.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixValue
        {
            get { return this._prefixValue; }
            set { this._prefixValue = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CurrentValue.
        ///     </para>
        ///     <para>
        ///         Value which indicates the current amount of codes generated.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CurrentValue
        {
            get { return this._currentValue; }
            set { this._currentValue = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalValue.
        ///     </para>
        ///     <para>
        ///         Number of available rows for this code.
        ///     </para>
        /// </summary>
        [DataMember]
        public int FinalValue
        {
            get { return this._finalValue; }
            set { this._finalValue = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixDescription.
        ///     </para>
        ///     <para>
        ///         Prefix description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixDescription
        {
            get { return this._prefixDescription; }
            set { this._prefixDescription = value; }
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Code prefix is enabled or disabled.
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
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         CountryId identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        #endregion

        private int _prefixId;
        private int? _parentId;
        private byte _prefixTypeId;
        private int _companyClientId;
        private string _prefixName;
        private string _prefixValue;
        private string _prefixDescription;
        private int _currentValue;
        private int _finalValue;
        private DateTime _addedDate;
        private bool _active;
        private byte _countryId;
    }
}
