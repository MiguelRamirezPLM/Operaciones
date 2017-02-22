using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Code Prefix Type information.
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

    public class CodePrefixTypesInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodePrefixTypesInfo class. Not receive parameters.
        /// </summary>
        public CodePrefixTypesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixTypeId.
        ///     </para>
        ///     <para>
        ///         Prefix Type identifier.
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
        ///         Property which gets or sets a value for PrefixTypeName.
        ///     </para>
        ///     <para>
        ///         Prefix type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixTypeName
        {
            get { return this._prefixTypeName; }
            set { this._prefixTypeName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Prefix Type is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _prefixTypeId;
        private string _prefixTypeName;
        private bool _active;
    }
}
