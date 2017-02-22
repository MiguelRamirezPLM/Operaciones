using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between search records and product attributes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PSE_TrackingAttributeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_TrackingAttributeInfo class. Not receives parameters.
        /// </summary>
        public PSE_TrackingAttributeInfo() { }

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
        ///         Property which gets or sets a value for AttributeId.
        ///     </para>
        ///     <para>
        ///         Attribute identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeId
        {
            get { return this._attributeId; }
            set { this._attributeId = value; }
        }

        #endregion

        private int _trackId;
        private int _attributeId;

    }
}
