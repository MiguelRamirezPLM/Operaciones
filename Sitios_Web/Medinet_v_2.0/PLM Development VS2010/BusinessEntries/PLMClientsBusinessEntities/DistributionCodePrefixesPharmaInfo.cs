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

   public class DistributionCodePrefixesPharmaInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionsInfo class. Not receive parameters.
        /// </summary>
        public DistributionCodePrefixesPharmaInfo() { }

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
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryId
        {
            get
            {
                return this._countryId;
            }
            set
            {
                this._countryId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixDescription.
        ///     </para>
        ///     <para>
        ///         Prefix description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixDescription
        {
            get { return this._prefixDescription; }
            set { this._prefixDescription = value; }
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Name the company.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Name the country.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CountryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                this._countryName = value;
            }
        }
        #endregion

        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private int _companyClientId;
        private byte _countryId;
        private string _prefixDescription;
        private string _targetName;
        private string _distributionName;
        private string _companyName;
        private string _countryName;

    }
}
