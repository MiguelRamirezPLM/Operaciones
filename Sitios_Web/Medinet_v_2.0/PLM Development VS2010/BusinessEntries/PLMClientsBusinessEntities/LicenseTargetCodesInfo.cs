using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Client and Code and License.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.LicensesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    [DataContract]
    public class LicenseTargetCodesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the LicenseTargetCodesInfo class. Not receive parameters.
        /// </summary>
        public LicenseTargetCodesInfo() 
        {
            this._initialDate = Convert.ToDateTime("01/01/1900");
            this._finalDate = null;
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
        ///         Property which gets or sets a value for LicenseId.
        ///     </para>
        ///     <para>
        ///         License identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int LicenseId
        {
            get
            {
                return this._licenseId;
            }
            set
            {
                this._licenseId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InitialDate.
        ///     </para>
        ///     <para>
        ///         License's initial date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get
            {
                return this._initialDate;
            }
            set
            {
                this._initialDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         License's final date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? FinalDate
        {
            get
            {
                return this._finalDate;
            }
            set
            {
                this._finalDate = value;
            }
        }

        #endregion

        private int _clientId;
        private int _codeId;
        private byte _targetId;
        private byte _deviceId;
        private int _licenseId;
        private DateTime _initialDate;
        private DateTime? _finalDate;

    }
}
