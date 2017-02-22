using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Product and PharmaceuticalForm.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class PharmaFormByProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmaFormByProductInfo class. Not receive parameters.
        /// </summary>
        public PharmaFormByProductInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Name or description of the PharmaceuticalForm associated with the Product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaForm
        {
            get { return this._pharmaForm; }
            set { this._pharmaForm = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Content.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        #endregion

        private int _productId;
        private int _pharmaFormId;
        private string _pharmaForm;
        private string _content;

    }
}
