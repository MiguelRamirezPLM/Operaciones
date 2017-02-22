using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Upgrades information for each Distribution.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CodePrefixInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.DistributionsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TargetOutputsInfo"/>
    [DataContract]
    public class MetadataVersionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MetadataVersionInfo class. Not receive parameters.
        /// </summary>
        public MetadataVersionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for VersionId.
        ///     </para>
        ///     <para>
        ///         Version identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int VersionId
        {
            get
            {
                return this._versionId;
            }
            set
            {
                this._versionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier associated with the Version.
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
        ///         Property which gets or sets a value for UrlPackage.
        ///     </para>
        ///     <para>
        ///         Web address where is located the update package.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UrlPackage
        {
            get
            {
                return this._urlPackage;
            }
            set
            {
                this._urlPackage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AddedDate.
        ///     </para>
        ///     <para>
        ///         Publication date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime AddedDate
        {
            get
            {
                return this._addedDate;
            }
            set
            {
                this._addedDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Version is enabled or disabled.
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

        private int _versionId;
        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private string _urlPackage;
        private DateTime _addedDate;
        private bool _active;

    }
}
