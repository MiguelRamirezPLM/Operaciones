using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Clients and Specialities.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class SpecialityClientInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SpecialityClientInfo class. Not receive parameters.
        /// </summary>
        public SpecialityClientInfo() { }

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
        ///         Property which gets or sets a value for SpecialityId.
        ///     </para>
        ///     <para>
        ///         Speciality identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public short SpecialityId
        {
            get { return this._specialityId; }
            set { this._specialityId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherSpeciality.
        ///     </para>
        ///     <para>
        ///         Alternative speciality associated with the Client.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherSpeciality
        {
            get { return this._otherSpeciality; }
            set { this._otherSpeciality = value; }
        }

        #endregion

        private int _clientId;
        private short _specialityId;
        private string _otherSpeciality;

    }
}
