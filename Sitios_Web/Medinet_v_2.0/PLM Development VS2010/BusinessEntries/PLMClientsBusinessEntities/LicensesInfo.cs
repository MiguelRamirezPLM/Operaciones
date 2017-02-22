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
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.DistributionsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodePrefixInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    [DataContract]
    public class LicensesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the LicensesInfo class. Not receive parameters.
        /// </summary>
        public LicensesInfo() { }

        #endregion

        #region Properties

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

        ///// <summary>
        /////     <para>
        /////         Property which gets or sets a value for DistributionId.
        /////     </para>
        /////     <para>
        /////         Distribution identifier.
        /////     </para>
        ///// </summary>
        //[DataMember]
        //public int DistributionId
        //{
        //    get
        //    {
        //        return this._distributionId;
        //    }
        //    set 
        //    {
        //        this._distributionId = value;
        //    }
        //}

        ///// <summary>
        /////     <para>
        /////         Property which gets or sets a value for PrefixId.
        /////     </para>
        /////     <para>
        /////         Code Prefix identifier.
        /////     </para>
        ///// </summary>
        //[DataMember]
        //public int PrefixId
        //{
        //    get
        //    {
        //        return this._prefixId;
        //    }
        //    set
        //    {
        //        this._prefixId = value;
        //    }
        //}

        ///// <summary>
        /////     <para>
        /////         Property which gets or sets a value for TargetId.
        /////     </para>
        /////     <para>
        /////         Output medium identifier.
        /////     </para>
        ///// </summary>
        //[DataMember]
        //public byte TargetId
        //{
        //    get
        //    {
        //        return this._targetId;
        //    }
        //    set
        //    {
        //        this._targetId = value;
        //    }
        //}

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LicenseKey.
        ///     </para>
        ///     <para>
        ///         License key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LicenseKey
        {
            get
            {
                return this._licenseKey;
            }
            set
            {
                this._licenseKey = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CurrentInstallation.
        ///     </para>
        ///     <para>
        ///         Installation number.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CurrentInstallation
        {
            get
            {
                return this._currentInstallation;
            }
            set
            {
                this._currentInstallation = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Duration.
        ///     </para>
        ///     <para>
        ///         License duration.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the License is enabled or disabled.
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

        private int _licenseId;
        //private int _distributionId;
        //private int _prefixId;
        //private byte _targetId;
        private string _licenseKey;
        private byte _currentInstallation;
        private decimal _duration;
        private bool _active;

    }
}
