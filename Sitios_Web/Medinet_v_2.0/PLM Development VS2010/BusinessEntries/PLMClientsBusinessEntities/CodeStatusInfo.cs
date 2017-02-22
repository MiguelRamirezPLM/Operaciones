using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the Code phases.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class CodeStatusInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodeStatusInfo class. Not receive parameters.
        /// </summary>
        public CodeStatusInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeStatusId.
        ///     </para>
        ///     <para>
        ///         Status identifier.
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
        ///         Property which gets or sets a value for StatusName.
        ///     </para>
        ///     <para>
        ///         Status name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string StatusName
        {
            get { return this._statusName; }
            set { this._statusName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Code status is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _codeStatusId;
        private string _statusName;
        private bool _active;

    }
}
