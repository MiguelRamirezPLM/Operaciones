using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Pharmaceutical Form and Therapeutic of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    [DataContract]  
    public class PharmaTherapeuticInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmaTherapeuticInfo class. Not receive parameters.
        /// </summary>
        public PharmaTherapeuticInfo() { }

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
        ///         Property which gets or sets a value for TherapeuticId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
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
        ///         Property which gets or sets a value for EnglishDescription.
        ///     </para>
        ///     <para>
        ///         Name or description in English of the PharmaceuticalForm associated with the Product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EnglishDescription
        {
            get { return this._englishDescription; }
            set { this._englishDescription = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Therapeutic.
        ///     </para>
        ///     <para>
        ///         Name or description of the Therapeutic associated with the Product.
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the product is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _productId;
        private int _pharmaFormId;
        private int _therapeuticId;
        private string _pharmaForm;
        private string _englishDescription;
        private string _therapeutic;
        private bool _active;
    }
}
