using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Target Outputs and Device identifiers.
    /// </summary>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.DeviceIdentifierInfo"/>
    [DataContract]
    public class TargetIdentifiersInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TargetIdentifiersInfo class. Not receive parameters.
        /// </summary>
        public TargetIdentifiersInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Output medium identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TargetId
        {
            get
            {
                return this._targetId;
            }
            set
            {
                this._targetId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DeviceId.
        ///     </para>
        ///     <para>
        ///         Equipment type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte DeviceId
        {
            get
            {
                return this._deviceId;
            }
            set
            {
                this._deviceId = value;
            }
        }

        #endregion

        private byte _targetId;
        private byte _deviceId;

    }
}
