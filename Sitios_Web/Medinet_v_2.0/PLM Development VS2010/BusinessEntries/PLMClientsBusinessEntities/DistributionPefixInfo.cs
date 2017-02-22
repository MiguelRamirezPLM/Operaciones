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
    public class DistributionPrefixInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionCodePrefixesInfo class. Not receive parameters.
        /// </summary>
        
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
        /// </summary> PrefixName
        
        [DataMember]
        public string PrefixName
        {
            get { return this._prefixName; }
            set { this._prefixName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixValue.
        ///     </para>
        ///     <para>
        ///         Base value for generating codes for clients.
        ///     </para>
        /// </summary>

        #endregion

        private int _distributionId;
        private int _prefixId;
        private string _prefixName;

    }
}
