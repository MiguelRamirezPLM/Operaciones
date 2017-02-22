using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents relationship between Distributions and BlockingCodes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.BlockingCodeInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.DistributionCodePrefixesInfo"/>
    [DataContract]
    public class BlockingCodePrefixInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BlockingCodePrefixInfo class. Not receive parameters.
        /// </summary>
        public BlockingCodePrefixInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BlockingCodeId.
        ///     </para>
        ///     <para>
        ///         BlockingCode identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BlockingCodeId
        {
            get { return this._blockingCodeId; }
            set { this._blockingCodeId = value; }
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
            get { return this._distributionId; }
            set { this._distributionId = value; }
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
            get { return this._prefixId; }
            set { this._prefixId = value; }
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
            get { return this._targetId; }
            set { this._targetId = value; }
        }

        #endregion

        private int _blockingCodeId;
        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
    }
}
