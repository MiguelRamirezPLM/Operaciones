using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the content of the attributes associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.AttributesInfo"/>
    [DataContract]
    public class AttributeDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributeDetailInfo class. Not receive parameters.
        /// </summary>
        public AttributeDetailInfo() { }

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
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CategoryId
        {
            get { return this._categoryId; }
            set { this._categoryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get { return this._pharmaFormId; }
            set { this._pharmaFormId = value; }
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
        ///         Property which gets or sets a value for AttributeName.
        ///     </para>
        ///     <para>
        ///         Name or description of the Attribute.
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
        ///         Property which gets or sets a value for AttributeContent.
        ///     </para>
        ///     <para>
        ///         Attribute content in plain text.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttributeContent
        {
            get { return this._attibuteContent; }
            set { this._attibuteContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HTMLContent.
        ///     </para>
        ///     <para>
        ///         Attribute content in HTML format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HTMLContent
        {
            get { return this._htmlContent; }
            set { this._htmlContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeGroupKey.
        ///     </para>
        ///     <para>
        ///         Key of the Attribute.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttributeGroupKey
        {
            get { return this._attributeGroupKey; }
            set { this._attributeGroupKey = value; }
        }
        #endregion

        private int _editionId;
        private int _divisionId;
        private int _categoryId;
        private int _productId;
        private int _pharmaFormId;
        private int _attributeId;
        private string _attributeName;
        private string _attibuteContent;
        private string _htmlContent;
        private string _attributeGroupKey;
        
    }
}
