using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Session information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class SessionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SessionInfo class. Not receive parameters.
        /// </summary>
        public SessionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SessionId.
        ///     </para>
        ///     <para>
        ///         Session identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SessionId
        {
            get { return this._sessionId; }
            set { this._sessionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SessionName.
        ///     </para>
        ///     <para>
        ///         Session name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SessionName
        {
            get { return this._sessionName; }
            set { this._sessionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Session is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _sessionId;
        private string _sessionName;
        private bool _active;

    }
}
