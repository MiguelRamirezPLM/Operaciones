using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory, Substances, PharmaceuticalForm and the content of the attributes associated with a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.AttributeDetailInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.EditionProductShotInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    [DataContract]
    public class MedicDetailInfo 
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductByCodeInfo class. Not receive parameters.
        /// </summary>

        public MedicDetailInfo() { }

        #endregion

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Vendor.
        ///     </para>
        ///     <para>
        ///         Vendor.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Vendor
        {
            get { return this._vendor; }
            set { this._vendor = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionShortName.
        ///     </para>
        ///     <para>
        ///         DivisionShortName.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionShortName
        {
            get { return this._divisionShortName; }
            set { this._divisionShortName = value; }
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
            get { return this._brand; }
            set { this._brand = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParmaForm.
        ///     </para>
        ///     <para>
        ///         ParmaForm.
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
        ///         Property which gets or sets a value for ATC.
        ///     </para>
        ///     <para>
        ///         ModifiedContent.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Atc
        {
            get { return this._atc; }
            set { this._atc = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substances.
        ///     </para>
        ///     <para>
        ///         ModifiedContent.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Substances
        {
            get { return this._susbtances; }
            set { this._susbtances = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Indications.
        ///     </para>
        ///     <para>
        ///         ModifiedContent.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Indications
        {
            get { return this._indications; }
            set { this._indications = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Indications.
        ///     </para>
        ///     <para>
        ///         ModifiedContent.
        ///     </para>
        /// </summary>
        [DataMember]
        public int NumberEdition
        {
            get { return this._numberEdition; }
            set { this._numberEdition = value; }
        }

        private string _vendor;
        private string _divisionShortName;
        private string _pharmaForm;
        private string _brand;
        private string _atc;
        private string _susbtances;
        private string _indications;
        private int _numberEdition;
    }
}
