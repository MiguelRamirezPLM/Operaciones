using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which represents information about a AttributeContent.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AttributeContentInfo
    {
        #region Constructors

        ///<summary> 
        /// Initializes a new instance of the AttributeContentInfo class.
        ///</summary>
        public AttributeContentInfo() { }

        #endregion

        #region Properties
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaId.
        ///     </para>
        ///     <para>
        ///         Encyclopedia Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaId
        {
            get { return this._encyclopediaId; }
            set { this._encyclopediaId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeTypeId.
        ///     </para>
        ///     <para>
        ///         Attribute Type Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeTypeId
        {
            get { return this._attributeTypeId; }
            set { this._attributeTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Content.
        ///     </para>
        ///     <para>
        ///         Content in Text.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ImageFile.
        ///     </para>
        ///     <para>
        ///         Image Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ImageFile
        {
            get { return this._imageFile; }
            set { this._imageFile = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Url Addres.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeName.
        ///     </para>
        ///     <para>
        ///         Attribute Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttributeName
        {
            get { return this._attributeName; }
            set { this._attributeName = value; }
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
        ///         Property which gets or sets a value for AttributeGroupName.
        ///     </para>
        ///     <para>
        ///         AttributeGroup Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttributeGroupName
        {
            get { return this._attributeGroupName; }
            set { this._attributeGroupName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeGroupId.
        ///     </para>
        ///     <para>
        ///         AttributeGroup Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeGroupId
        {
            get { return this._attributeGroupId; }
            set { this._attributeGroupId = value; }
        }

        #endregion

        private int _encyclopediaId;
        private int _attributeTypeId;
        private string _content;
        private string _imageFile;
        private string _baseUrl;
        private string _attributeName;
        private int _attributeId;
        private int _attributeGroupId;
        private string _attributeGroupName;
        
    }
}
