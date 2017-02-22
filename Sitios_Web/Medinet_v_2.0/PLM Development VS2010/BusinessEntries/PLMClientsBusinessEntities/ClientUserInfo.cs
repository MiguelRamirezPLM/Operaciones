using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Customers and Users.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.UserInfo"/>
    [Serializable]
    [DataContract]
    public class ClientUserInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientUserInfo class. Not receive parameters.
        /// </summary>
        public ClientUserInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Customer identifier.
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
        ///         Property which gets or sets a value for UserId.
        ///     </para>
        ///     <para>
        ///         User identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        #endregion

        private int _clientId;
        private int _userId;

    }
}
