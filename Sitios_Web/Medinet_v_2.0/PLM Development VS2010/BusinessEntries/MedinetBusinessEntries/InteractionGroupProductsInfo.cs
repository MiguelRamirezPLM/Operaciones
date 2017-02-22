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
    public class InteractionGroupProductsInfo
    {
         #region Constructor
            /// <summary>
            ///     Initializes a new instance of the InteractionGroupProductsInfo class. Not receive parameters.
            /// </summary>
            public InteractionGroupProductsInfo() { }
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
        ///         Property which gets or sets a PharmaForm description.
        ///     </para>
        ///     <para>
        ///         PharmaForm description
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaForm
        {
            get
            {
                return this._pharmaForm;
            }
            set
            {
                this._pharmaForm = value;
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
        ///         Property that returns the active substance belonging to the group interaction.
        ///     </para>
        ///     <para>
        ///         ActiveSubastance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceIdGI
        {
            get
            {
                return this._activeSubstanceIdGI;
            }
            set
            {
                this._activeSubstanceIdGI = value;
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
        public int IPharmaGroupId
        {
            get
            {
                return this._iPharmaGroupId;
            }
            set
            {
                this._iPharmaGroupId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId Product Interaction.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IDivisionId
        {
            get
            {
                return this._iDivisionId;
            }
            set
            {
                this._iDivisionId = value;
            }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId Product Interaction.
        ///     </para>
        ///     <para>
        ///         Category Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ICategoryId
        {
            get
            {
                return this._iCategoryId;
            }
            set
            {
                this._iCategoryId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId Product Interaction.
        ///     </para>
        ///     <para>
        ///         PharmaForm Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IPharmaFormId
        {
            get
            {
                return this._iPharmaFormId;
            }
            set
            {
                this._iPharmaFormId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a PharmaForm description Product Interaction.
        ///     </para>
        ///     <para>
        ///         PharmaForm description Product Interaction.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IPharmaForm
        {
            get
            {
                return this._iPharmaForm;
            }
            set
            {
                this._iPharmaForm = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId Product Interaction.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IProductId
        {
            get
            {
                return this._iProductId;
            }
            set
            {
                this._iProductId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property that returns value the active substance belonging to the product of interaction.
        ///     </para>
        ///     <para>
        ///         ActiveSubastance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IActiveSubstanceId
        {
            get
            {
                return this._iActiveSubstanceId;
            }
            set
            {
                this._iActiveSubstanceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property that returns the active substance belonging to the product of interaction.
        ///     </para>
        ///     <para>
        ///         Active substance's Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IActiveSubstance
        {
            get
            {
                return this._iActiveSubstance;
            }
            set
            {
                this._iActiveSubstance = value;
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
        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                this._groupName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Product Name Product Interaction.
        ///     </para>
        ///     <para>
        ///         Product´s Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IBrand
        {
            get
            {
                return this._iBrand;
            }
            set
            {
                this._iBrand = value;
            }
        }

        #endregion
        private int _productId;
        private int _divisionId;
        private int _categoryId;
        private int _pharmaFormId;
        private string _pharmaForm;
        private int _activeSubstanceId;
        private string _activeSubstance;
        private int _iPharmaGroupId;
        private string _groupName; 
        private int _activeSubstanceIdGI;
        private string _iBrand;
        private int _iProductId;
        private int _iDivisionId;
        private int _iCategoryId;
        private int _iPharmaFormId;
        private string _iPharmaForm;
        private int _iActiveSubstanceId;
        private string _iActiveSubstance; 
    }
}