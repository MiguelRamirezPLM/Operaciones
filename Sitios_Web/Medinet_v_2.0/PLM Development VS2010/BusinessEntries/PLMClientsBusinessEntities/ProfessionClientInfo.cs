using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Clients and Professions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ProfessionClientInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProfessionClientInfo class. Not receive parameters.
        /// </summary>
        public ProfessionClientInfo() { }

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
        ///         Property which gets or sets a value for ProfessionId.
        ///     </para>
        ///     <para>
        ///         Professional practice identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public short ProfessionId
        {
            get { return this._professionId; }
            set { this._professionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherProfession.
        ///     </para>
        ///     <para>
        ///         Alternative profession associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherProfession
        {
            get { return this._otherProfession; }
            set { this._otherProfession = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProfessionalLicense.
        ///     </para>
        ///     <para>
        ///         Professional license associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProfessionalLicense
        {
            get { return this._professionalLicense; }
            set { this._professionalLicense = value; }
        }

        #endregion

        private int _clientId;
        private short _professionId;
        private string _otherProfession;
        private string _professionalLicense;

    }
}
