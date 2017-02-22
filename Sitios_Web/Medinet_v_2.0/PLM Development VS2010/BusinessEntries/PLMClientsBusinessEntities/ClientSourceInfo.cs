using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Clients and Entry Sources.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    [DataContract]
    public sealed class ClientSourceInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientSourceInfo class. Not receive parameters.
        /// </summary>
        public ClientSourceInfo() 
        {
            this._addedDate = DateTime.Now;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntrySourceId.
        ///     </para>
        ///     <para>
        ///         Entry source identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EntrySourceId
        {
            set { this._entrySourceId = value; }
            get { return this._entrySourceId; }
        }

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
            set { this._clientId = value; }
            get { return this._clientId; }
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
            set { this._addedDate = value; }
            get { return this._addedDate; }
        }

        #endregion

        private int _entrySourceId;
        private int _clientId;
        private DateTime _addedDate;
    }
}
