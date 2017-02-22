using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Clients and Professional Practices.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ProfessionalPracticeInfo"/>
    [Serializable]
    [DataContract]
    public class ClientPracticeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientPracticeInfo class. Not receive parameters.
        /// </summary>
        public ClientPracticeInfo() { }

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
        ///         Property which gets or sets a value for PracticeId.
        ///     </para>
        ///     <para>
        ///         Professional Practice identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PracticeId
        {
            get { return this._practiceId; }
            set { this._practiceId = value; }
        }

        #endregion

        private int _clientId;
        private byte _practiceId;

    }
}
