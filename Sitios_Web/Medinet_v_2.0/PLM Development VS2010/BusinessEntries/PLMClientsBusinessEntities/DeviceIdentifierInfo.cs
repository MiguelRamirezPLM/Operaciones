using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Device information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class DeviceIdentifierInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DeviceIdentifierInfo class. Not receive parameters.
        /// </summary>
        public DeviceIdentifierInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DeviceId.
        ///     </para>
        ///     <para>
        ///         Device identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte DeviceId
        {
            get { return this._deviceId; }
            set { this._deviceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DeviceName.
        ///     </para>
        ///     <para>
        ///         Device name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DeviceName
        {
            get { return this._deviceName; }
            set { this._deviceName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Device is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _deviceId;
        private string _deviceName;
        private bool _active;

    }
}
