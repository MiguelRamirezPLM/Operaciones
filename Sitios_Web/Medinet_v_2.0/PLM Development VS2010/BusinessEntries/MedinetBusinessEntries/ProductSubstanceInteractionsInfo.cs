using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the  products´s substances interaction.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CategoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class ProductSubstanceInteractionsInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductSubstanceInteractionsInfo class. Not receive parameters.
        /// </summary>
        public ProductSubstanceInteractionsInfo() { }

        #endregion
        #region Properties
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
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
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
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         PharmaForm Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get
            {
                return this._pharmaFormId;
            }
            set
            {
                this._pharmaFormId = value;
            }
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
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         ActiveSubastance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         ActiveSubastance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SubstanceInteractId
        {
            get
            {
                return this._substanceInteractId;
            }
            set
            {
                this._substanceInteractId = value;
            }
        }
         /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstance.
        ///     </para>
        ///     <para>
        ///         Active substance's Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstance
        {
            get
            {
                return this._activeSubstance;
            }
            set
            {
                this._activeSubstance= value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstance interaction.
        ///     </para>
        ///     <para>
        ///         Active substance's Name whith interaction.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SubstanceInteraction
        {
            get
            {
                return this._substanceInteraction;
            }
            set
            {
                this._substanceInteraction = value;
            }
        }
   
        #endregion

        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private int _activeSubstanceId;
        private int _substanceInteractId;
        private string _activeSubstance;
        private string _substanceInteraction;
    }
}
