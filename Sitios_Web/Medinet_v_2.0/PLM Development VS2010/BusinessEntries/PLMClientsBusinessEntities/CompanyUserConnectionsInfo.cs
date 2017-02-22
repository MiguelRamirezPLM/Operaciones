using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the User's Connection information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class CompanyUserConnectionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CompanyUserConnectionsInfo class. Not receive parameters.
        /// </summary>
        public CompanyUserConnectionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UserConnectionId.
        ///     </para>
        ///     <para>
        ///         Connection identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int UserConnectionId
        {
            get
            {
                return this._userConnectionId;
            }
            set
            {
                this._userConnectionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier. Application used by the user.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DistributionId
        {
            get
            {
                return this._distributionId;
            }
            set
            {
                this._distributionId = value;
            }
        }

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
        ///         Property which gets or sets a value for SessionId.
        ///     </para>
        ///     <para>
        ///         Session identifier. Indicates if the connection is open or closed.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte SessionId
        {
            get
            {
                return this._sessionId;
            }
            set
            {
                this._sessionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DateConnection.
        ///     </para>
        ///     <para>
        ///         Connection date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? DateConnection
        {
            get
            {
                return this._dateConnection;
            }
            set
            {
                this._dateConnection = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IP.
        ///     </para>
        ///     <para>
        ///         Logical address of the computer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IP
        {
            get
            {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        #endregion

        private int _userConnectionId;
        private int _distributionId;
        private int _companyUserId;
        private int _codeId;
        private byte _sessionId;
        private DateTime? _dateConnection;
        private string _ip;
    }
}
