using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Editions and Attributes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.AttributesInfo"/>
    [DataContract]
    public class EditionAttributesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionAttributesInfo class. Not receive parameters.
        /// </summary>
        public EditionAttributesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeId.
        ///     </para>
        ///     <para>
        ///         Attribute Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeId
        {
            get { return this._attributeId; }
            set { this._attributeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributteOrder.
        ///     </para>
        ///     <para>
        ///         Order in which the attribute is present.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte AttributteOrder
        {
            get { return this._attributeOrder; }
            set { this._attributeOrder = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeSearch.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool AttributeSearch
        {
            get { return this._attributeSearch; }
            set { this._attributeSearch = value; }
        }

        #endregion

        private int _editionId;
        private int _attributeId;
        private byte _attributeOrder;
        private bool _attributeSearch;

    }
}
