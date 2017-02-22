using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the Participant Products in an Edition.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class ParticipantProductsInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ParticipantProductsInfo class. Not receive parameters.
        /// </summary>
        public ParticipantProductsInfo() { }

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
        ///         Property which gets or sets a value for Page.
        ///     </para>
        ///     <para>
        ///         Page on which the drug is in the print edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Page
        {
            get { return this._page; }
            set { this._page = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HTMLContent.
        ///     </para>
        ///     <para>
        ///         Product IPP in HTML format.
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
        ///         Property which gets or sets a value for XMLContent.
        ///     </para>
        ///     <para>
        ///         Product IPP in XML format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string XMLContent
        {
            get { return this._xmlContent; }
            set { this._xmlContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ModifiedContent.
        ///     </para>
        ///     <para>
        ///         It indicates if the product content was modified.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool? ModifiedContent
        {
            get
            {
                return this._modifiedContent;
            }
            set
            {
                this._modifiedContent = value;
            }
        }

        #endregion

        private int _editionId;
        private int _divisionId;
        private int _categoryId;
        private int _productId;
        private int _pharmaFormId;
        private string _page;
        private string _htmlContent;
        private string _xmlContent;
        private bool? _modifiedContent;

    }
}
