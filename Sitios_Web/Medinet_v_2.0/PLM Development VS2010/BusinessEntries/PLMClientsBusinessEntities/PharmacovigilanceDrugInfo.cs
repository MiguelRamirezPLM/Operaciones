using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceDrug information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]   
    public class PharmacovigilanceDrugInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceDrugInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceDrugInfo() 
            {
                _expiration = Convert.ToDateTime("01/01/1900");
            }

        #endregion


        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceDrugId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrug identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmacovigilanceDrugId
        {
            get { return this._pharmacovigilanceDrugId; }
            set { this._pharmacovigilanceDrugId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Identifier of the source PharmacovigilanceDrug.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceKey.
        ///     </para>
        ///     <para>
        ///         Active Substance identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstanceKey
        {
            get { return this._activeSubstanceKey; }
            set { this._activeSubstanceKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Active Substance name.
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
        ///         Property which gets or sets a value for BrandKey.
        ///     </para>
        ///     <para>
        ///         Brand identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BrandKey
        {
            get { return this._brandKey; }
            set { this._brandKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Brand name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get { return this._brand; }
            set { this._brand = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionKey.
        ///     </para>
        ///     <para>
        ///         Division identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionKey
        {
            get { return this._DivisionKey; }
            set { this._DivisionKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionName.
        ///     </para>
        ///     <para>
        ///         Division name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionName
        {
            get { return this._divisionName; }
            set { this._divisionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for RegisterNumber.
        ///     </para>
        ///     <para>
        ///         Register identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string RegisterNumber
        {
            get { return this._registerNumber; }
            set { this._registerNumber = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Expiration.
        ///     </para>
        ///     <para>
        ///         Expiration date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? Expiration
        {
            get { return this._expiration; }
            set { this._expiration = value; }
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Dose.
        ///     </para>
        ///     <para>
        ///         Dose.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Dose
        {
            get { return this._dose; }
            set { this._dose = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitDoseKey.
        ///     </para>
        ///     <para>
        ///         Unit Dose identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UnitDoseKey
        {
            get { return this._unitDoseKey; }
            set { this._unitDoseKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitDose.
        ///     </para>
        ///     <para>
        ///         Unit Dose name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UnitDose
        {
            get { return this._unitDose; }
            set { this._unitDose = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AdministrationRouteKey.
        ///     </para>
        ///     <para>
        ///         Administration Route identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AdministrationRouteKey
        {
            get { return this._administrationRouteKey; }
            set { this._administrationRouteKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AdministrationRoute.
        ///     </para>
        ///     <para>
        ///         Administration Route name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AdministrationRoute
        {
            get { return this._administrationRoute; }
            set { this._administrationRoute = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for StartDate.
        ///     </para>
        ///     <para>
        ///         Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime StartDate
        {
            get { return this._startDate; }
            set { this._startDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICDKey.
        ///     </para>
        ///     <para>
        ///         ICD identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ICDKey
        {
            get { return this._icdKey; }
            set { this._icdKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICD.
        ///     </para>
        ///     <para>
        ///         ICD name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ICD
        {
            get { return this._icd; }
            set { this._icd = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceDrugTypeId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrugType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmacovigilanceDrugTypeId
        {
            get { return this._pharmacovigilanceDrugTypeId; }
            set { this._pharmacovigilanceDrugTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FrequencyId.
        ///     </para>
        ///     <para>
        ///         Frequency identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int FrequencyId
        {
            get { return this._frequencyId; }
            set { this._frequencyId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the PharmacovigilanceDrugId is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EndDate.
        ///     </para>
        ///     <para>
        ///         End date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? EndDate
        {
            get { return this._endDate; }
            set { this._endDate = value; }
        }

      

        #endregion

        private int _pharmacovigilanceDrugId;
        private int? _parentId;
        private string _activeSubstanceKey;
        private string _activeSubstance;
        private string _brandKey;
        private string _brand;
        private string _DivisionKey;
        private string _divisionName;
        private string _registerNumber;
        private DateTime? _expiration;
        private string _dose;
        private string _unitDoseKey;
        private string _unitDose;
        private string _administrationRouteKey;
        private string _administrationRoute;
        private DateTime _startDate;
        private DateTime? _endDate;
        private string _icdKey;
        private string _icd;
        private byte _pharmacovigilanceDrugTypeId;
        private int _frequencyId;
        private bool _active;
       
        
    }
}
