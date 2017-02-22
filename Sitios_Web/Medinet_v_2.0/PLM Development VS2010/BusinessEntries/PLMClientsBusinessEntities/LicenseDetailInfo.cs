using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the License information.
    /// </summary>
    [DataContract]
    public class LicenseDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the LicenseDetailInfo class. Not receive parameters.
        /// </summary>
        public LicenseDetailInfo() 
        {
            this._initialDate = Convert.ToDateTime("01/01/1900");
            this._finalDate = Convert.ToDateTime("01/01/1900");
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
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DistributionId
        {
            get
            {
                return this._distributionId;
            }
            set
            {
                this._distributionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get
            {
                return this._prefixId;
            }
            set
            {
                this._prefixId = value;
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
        ///         Device identifier.
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
        ///         Installation date.
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
        ///         Expiration date of the license.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeString.
        ///     </para>
        ///     <para>
        ///         Code assigned to the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CodeString
        {
            get
            {
                return this._codeString;
            }
            set
            {
                this._codeString = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixName.
        ///     </para>
        ///     <para>
        ///         Code prefix name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixName
        {
            get
            {
                return this._prefixName;
            }
            set
            {
                this._prefixName = value;
            }

        }

        #endregion

        private int _clientId;
        private int _codeId;
        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private byte _deviceId;
        private int _licenseId;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private string _codeString;
        private string _prefixName;

    }
}
