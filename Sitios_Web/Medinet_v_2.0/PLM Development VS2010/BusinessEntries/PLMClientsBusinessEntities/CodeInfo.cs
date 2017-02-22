using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Code information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Unique code associated with an Client.
    ///     </para>
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CodePrefixInfo"/>
    [Serializable]
    [DataContract]
    public class CodeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodeInfo class. Not receive parameters.
        /// </summary>
        public CodeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get { return this._codeId; }
            set { this._codeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeStatusId.
        ///     </para>
        ///     <para>
        ///         Code status identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CodeStatusId
        {
            get { return this._codeStatusId; }
            set { this._codeStatusId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeString.
        ///     </para>
        ///     <para>
        ///         Code value.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CodeString
        {
            get { return this._codeString; }
            set { this._codeString = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Prefix code identifier. Unique code associated with an application.
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
        ///         Property which gets or sets a value for Assign.
        ///     </para>
        ///     <para>
        ///         Indicates if the Code is assigned to Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Assign
        {
            get { return this._assign; }
            set { this._assign = value; }
        }

        #endregion

        private int _codeId;
        private byte _codeStatusId;
        private string _codeString;
        private int _prefixId;
        private bool _assign;

    }
}
