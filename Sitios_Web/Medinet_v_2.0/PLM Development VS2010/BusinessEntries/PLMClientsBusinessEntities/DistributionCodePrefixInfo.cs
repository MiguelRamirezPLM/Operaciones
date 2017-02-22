using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents relationship between Distributions and CodePrefixes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.DistributionsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodePrefixInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    [DataContract]
    public class DistributionCodePrefixInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionCodePrefixesInfo class. Not receive parameters.
        /// </summary>
        public DistributionCodePrefixInfo() 
        {
            this._initialDate = Convert.ToDateTime("01/01/1900");
            this._finalDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Code Prefix identifier.
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
        ///         Property which gets or sets a value for InicialDate.
        ///     </para>
        ///     <para>
        ///         Distribution's Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? InicialDate
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
        ///         Distribution's Ending date.
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AllowedInstallations.
        ///     </para>
        ///     <para>
        ///         Number of installations permitted for this Distribution.
        ///     </para>
        /// </summary>
        [DataMember]
        public short? AllowedInstallations
        {
            get
            {
                return this._allowedInstallations;
            }
            set
            {
                this._allowedInstallations = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixDescription.
        ///     </para>
        ///     <para>
        ///         Description the Prefix.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixDescription
        {
            get
            {
                return this._prefixDescription;
            }
            set
            {
                this._prefixDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetName.
        ///     </para>
        ///     <para>
        ///         Name the target.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TargetName
        {
            get
            {
                return this._targetName;
            }
            set
            {
                this._targetName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionName.
        ///     </para>
        ///     <para>
        ///         Name the distribution.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DistributionName
        {
            get
            {
                return this._distributionName;
            }
            set
            {
                this._distributionName = value;
            }
        }
        #endregion

        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private DateTime? _initialDate;
        private DateTime? _finalDate;
        private short? _allowedInstallations;
        private string _prefixDescription;
        private string _targetName;
        private string _distributionName;
    }
}
