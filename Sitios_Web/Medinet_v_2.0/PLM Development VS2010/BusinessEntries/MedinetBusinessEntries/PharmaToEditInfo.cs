using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Pharmaceutical Forms and Active Substances.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.UnitInfo"/>
    [DataContract]
    public class PharmaToEditInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmaToEditInfo class. Not receive parameters.
        /// </summary>
        public PharmaToEditInfo() { }

        #endregion 

        #region Properties

        /// <summary>
        ///     <para >
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
        ///         Property which gets or sets a value for ActiveSubstance.
        ///     </para>
        ///     <para>
        ///         Name or description of the ActiveSubstance.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstance
        {
            get { return this._activeSubstance; }
            set { this._activeSubstance = value; }
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
        ///         Property which gets or sets a value for Name.
        ///     </para>
        ///     <para>
        ///         Name or description of the unit of measure.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Quantity.
        ///     </para>
        ///     <para>
        ///         Quantity of Active Substance in a PharmaceuticalForm.
        ///     </para>
        /// </summary>
        [DataMember]
        public int Quantity
        {
            get{ return this._quantity; }
            set{ this._quantity = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherUnitId.
        ///     </para>
        ///     <para>
        ///         Another unit of measure identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int OtherUnitId
        {
            get { return this._otherUnitId; }
            set { this._otherUnitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherName.
        ///     </para>
        ///     <para>
        ///         Another unit of measure name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OtherName
        {
            get { return this._otherName; }
            set { this._otherName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherConcentrationId.
        ///     </para>
        ///     <para>
        ///         Another concentration identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int OtherConcentrationId
        {
            get { return this._otherConcentrationId; }
            set { this._otherConcentrationId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherQuantity.
        ///     </para>
        ///     <para>
        ///         Another quantity of Active Substance in a PharmaceuticalForm.
        ///     </para>
        /// </summary>
        [DataMember]
        public int OtherQuantity
        {
            get{ return this._otherQuantity; }
            set{ this._otherQuantity = value; }
        }

        #endregion

        private int _pharmaFormId;
        private string _pharmaForm;
        private int _activeSubstanceId;
        private string _activeSubstance;
        private int _unitId;
        private string _name;
        private int _quantity;
        private int _otherConcentrationId;
        private int _otherUnitId;
        private string _otherName;
        private int _otherQuantity;

    }
}
