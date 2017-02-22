using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory and Pharmaceutical Form of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    [DataContract]
    public class DivisionProductsToEditInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DivisionProductsToEditInfo class. Not receive parameters.
        /// </summary>
        public DivisionProductsToEditInfo() { }

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
        ///         Property which gets or sets a value for DivisionName.
        ///     </para>
        ///     <para>
        ///         Name of the Division.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionName
        {
            get
            {
                return this._divisionName;
            }
            set
            {
                this._divisionName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         Short Name of the Division.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionShortName
        {
            get
            {
                return this._divisionShortName;
            }
            set
            {
                this._divisionShortName = value;
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
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Product Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get
            {
                return this._brand;
            }
            set
            {
                this._brand = value;
            }
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
        ///         Property which gets or sets a value for PharmaForm.
        ///     </para>
        ///     <para>
        ///         Name of the PharmaceuticalForm associated with the Product.
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

        #endregion

        private int _divisionId;
        private string _divisionName;
        private string _divisionShortName;
        private int _productId;
        private string _brand;
        private int _pharmaFormId;
        private string _pharmaForm;

    }
}
