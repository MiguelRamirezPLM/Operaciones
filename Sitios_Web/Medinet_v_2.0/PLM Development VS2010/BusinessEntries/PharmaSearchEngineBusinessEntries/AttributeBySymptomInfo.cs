using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the attribute information associated with a Symptom.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.AttributesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.SymptomInfo"/>
    [DataContract]
    public class AttributeBySymptomInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributeBySymptomInfo class. Not receive parameters.
        /// </summary>
        public AttributeBySymptomInfo() { }

        #endregion

        #region Properties

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
            get
            {
                return this._attributeId;
            }
            set
            {
                this._attributeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or description of the Attribute.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Content.
        ///     </para>
        ///     <para>
        ///         Content of the attribute in HTML Format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PlainContent.
        ///     </para>
        ///     <para>
        ///         Content of the attribute as plain text.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PlainContent
        {
            get
            {
                return this._plainContent;
            }
            set
            {
                this._plainContent = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeOrder.
        ///     </para>
        ///     <para>
        ///         Order in which the attribute is present.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? AttributeOrder
        {
            get
            {
                return this._attributeOrder;
            }
            set
            {
                this._attributeOrder = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HeaderImage.
        ///     </para>
        ///     <para>
        ///         Image associated with the symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HeaderImage
        {
            get
            {
                return this._headerImage;
            }
            set
            {
                this._headerImage = value;
            }
        }

        #endregion

        private int _attributeId;
        private string _description;
        private string _content;
        private string _plainContent;
        private int? _attributeOrder;
        private string _headerImage;

    }
}
