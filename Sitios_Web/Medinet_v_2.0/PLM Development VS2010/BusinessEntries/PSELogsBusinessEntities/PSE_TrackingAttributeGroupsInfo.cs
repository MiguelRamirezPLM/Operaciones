using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between search records and attribute groups.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PSE_TrackingAttributeGroupsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_TrackingAttributeGroupsInfo class. Not receives parameters.
        /// </summary>
        public PSE_TrackingAttributeGroupsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TrackId.
        ///     </para>
        ///     <para>
        ///         Search identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TrackId
        {
            get { return this._trackId; }
            set { this._trackId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeGroupId.
        ///     </para>
        ///     <para>
        ///         Group identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeGroupId
        {
            get { return this._attributeGroupId; }
            set { this._attributeGroupId = value; }
        }

        #endregion

        private int _trackId;
        private int _attributeGroupId;

    }
}
