using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MedinetBusinessEntries
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ProductOtherInteractionsInfo
    {
        
         #region Constructor
        public ProductOtherInteractionsInfo() { }
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
        ///         Property which gets or sets a value for ElementId.
        ///     </para>
        ///     <para>
        ///         Element Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ElementId
        {
            get
            {
                return this._elementId;
            }
            set
            {
                this._elementId = value;
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
                this._activeSubstance = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Element name interaction.
        ///     </para>
        ///     <para>
        ///         Element's Name whith interaction.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElementName
        {
            get
            {
                return this._elementName;
            }
            set
            {
                this._elementName = value;
            }
        }

        #endregion
        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private int _productId;
        private int _activeSubstanceId;
        private int _elementId;
        private string _activeSubstance;
        private string _elementName;
    }
}
