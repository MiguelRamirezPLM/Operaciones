using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the content of a IPP attribute.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.AttributesInfo"/>
    [DataContract]
    public class ProductContentsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductContentsInfo class. Not receive parameters.
        /// </summary>
        public ProductContentsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductContentId.
        ///     </para>
        ///     <para>
        ///         Identifier of  the content of a IPP attribute.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ProductContentId
        {
            get { return this._productContentId; }
            set { this._productContentId = value; }
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
        ///         Property which gets or sets a value for AttributeId.
        ///     </para>
        ///     <para>
        ///         Identifier of the IPP attribute.
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
        ///         Property which gets or sets a value for Content.
        ///     </para>
        ///     <para>
        ///         Content of the IPP attribute.
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
        ///         Property which gets or sets a value for PlainContent.
        ///     </para>
        ///     <para>
        ///         The content of the attribute, as plain text.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PlainContent
        {
            get { return this._plainContent; }
            set { this._plainContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HTMLContent.
        ///     </para>
        ///     <para>
        ///         The attribute content, as HTML format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HTMLContent
        {
            get
            {
                return this._htmlContent;
            }
            set
            {
                this._htmlContent = value;
            }
        }

        #endregion

        private int _productContentId;
        private int _productId;
        private int _pharmaFormId;
        private int _editionId;
        private int _divisionId;
        private int _categoryId;
        private int _attributeId;
        private string _content;
        private string _plainContent;
        private string _htmlContent;

    }
}
