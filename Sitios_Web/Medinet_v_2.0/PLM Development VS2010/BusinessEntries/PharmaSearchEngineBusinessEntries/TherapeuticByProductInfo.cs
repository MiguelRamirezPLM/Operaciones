using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Category, Pharmaceutical Form and Therapeutic of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    [DataContract]
    public class TherapeuticByProductInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticByProductInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticByProductInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticKey.
        ///     </para>
        ///     <para>
        ///         Key of the Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TherapeuticKey
        {
            get { return this._therapeuticKey; }
            set { this._therapeuticKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Therapeutic.
        ///     </para>
        ///     <para>
        ///         Name or description of the Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Therapeutic
        {
            get { return this._therapeutic; }
            set { this._therapeutic = value; }
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
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Name or description of the PharmaceuticalForm.
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
        
        #endregion

        private int? _therapeuticId;
        private string _therapeuticKey;
        private string _therapeutic;
        private int _productId;
        private int _pharmaFormId;
        private string _pharmaForm;
        private int _divisionId;
        private int _categoryId;

    }
}
