using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Client and Code and installation Device.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.DeviceIdentifierInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    [DataContract]
    public class TargetClientCodesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TargetClientCodesInfo class. Not receive parameters.
        /// </summary>
        public TargetClientCodesInfo() 
        {
            this._installationDate = Convert.ToDateTime("01/01/1900");
        }

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
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HWIdentifier.
        ///     </para>
        ///     <para>
        ///         Device's logical address.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HWIdentifier
        {
            get
            {
                return this._hwIdentifier;
            }
            set
            {
                this._hwIdentifier = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InstallationDate.
        ///     </para>
        ///     <para>
        ///         Installation date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InstallationDate
        {
            get
            {
                return this._installationDate;
            }
            set
            {
                this._installationDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the installation is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _clientId;
        private int _codeId;
        private byte _targetId;
        private byte _deviceId;
        private string _hwIdentifier;
        private DateTime _installationDate;
        private bool _active;

    }
}
