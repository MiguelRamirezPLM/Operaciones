using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about the content of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.UnitInfo"/>
    [DataContract]
    public class ConcentrationInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ConcentrationInfo class. Not receive parameters.
        /// </summary>
        public ConcentrationInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ConcentrationId.
        ///     </para>
        ///     <para>
        ///         Identifier Concentration.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ConcentrationId
        {
            get { return this._concentrationId; }
            set { this._concentrationId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///          Pharmaceutical Form Identifier.
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
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         Active Substance Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get { return this._activeSubstanceId; }
            set { this._activeSubstanceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitId.
        ///     </para>
        ///     <para>
        ///         Identifier of the unit of measure.
        ///     </para>
        /// </summary>
        [DataMember]
        public int UnitId
        {
            get { return this._unitId; }
            set { this._unitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Quantity.
        ///     </para>
        ///     <para>
        ///         Amount of active substance.
        ///     </para>
        /// </summary>
        [DataMember]
        public int Quantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SecondUnitId.
        ///     </para>
        ///     <para>
        ///         Identifier of the second unit of mesure.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? SecondUnitId
        {
            get { return this._secondUnitId; }
            set { this._secondUnitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SecondQuantity.
        ///     </para>
        ///     <para>
        ///         Amount of active substance.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? SecondQuantity
        {
            get { return this._secondQuantity; }
            set { this._secondQuantity = value; }
        }

        #endregion

        private int _concentrationId;
        private int _pharmaFormId;
        private int _productId;
        private int _activeSubstanceId;
        private int _unitId;
        private int _quantity;
        private int? _secondUnitId;
        private int? _secondQuantity;
        
    }
}
