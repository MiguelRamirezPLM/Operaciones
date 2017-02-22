using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Distribution Pharma information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class DistributionsPharmaInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionsInfo class. Not receive parameters.
        /// </summary>
        public DistributionsPharmaInfo() { }

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
        ///         Prefix identifier
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
        ///         Target identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte  TargetId
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
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         CompanyClient identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyClientId
        {
            get
            {
                return this._companyClientId;
            }
            set
            {
                this._companyClientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DisplayPharmacies.
        ///     </para>
        ///     <para>
        ///         DisplayPharmacies.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DisplayPharmacies
        {
            get
            {
                return this._displayPharmacies;
            }
            set
            {
                this._displayPharmacies = value;
            }
        }



        #endregion

        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private int _companyClientId;
        private int _displayPharmacies;
    }
}
