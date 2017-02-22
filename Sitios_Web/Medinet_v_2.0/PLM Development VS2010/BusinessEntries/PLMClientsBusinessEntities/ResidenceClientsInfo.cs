using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Client, Speciality and Residence level.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ResidenceClientsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ResidenceClientsInfo class. Not receive parameters.
        /// </summary>
        public ResidenceClientsInfo() { }

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
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
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
            get
            {
                return this._specialityId;
            }
            set
            {
                this._specialityId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceId.
        ///     </para>
        ///     <para>
        ///         Residence identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ResidenceId
        {
            get
            {
                return this._residenceId;
            }
            set
            {
                this._residenceId = value;
            }
        }

        #endregion

        private int _clientId;
        private short _specialityId;
        private byte _residenceId;

    }
}
