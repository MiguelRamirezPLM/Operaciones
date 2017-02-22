using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the branches of a company.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ContactInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ContactInfo class. Not receive parameters.
        /// </summary>
        public ContactInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchId.
        ///     </para>
        ///     <para>
        ///         Branch identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CountryName
        {
            get { return this._countryName; }
            set { this._countryName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Company name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Street.
        ///     </para>
        ///     <para>
        ///         Street name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Street
        {
            get { return this._street; }
            set { this._street = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Suburb.
        ///     </para>
        ///     <para>
        ///         Suburb name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Suburb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         ZipCode.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ZipCode
        {
            get { return this._zipCode; }
            set { this._zipCode = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Lada.
        ///     </para>
        ///     <para>
        ///         Area code.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Lada
        {
            get { return this._lada; }
            set { this._lada = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneOne.
        ///     </para>
        ///     <para>
        ///         Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneOne
        {
            get { return this._phoneOne; }
            set { this._phoneOne = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContactEmail.
        ///     </para>
        ///     <para>
        ///         Email.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContactEmail
        {
            get { return this._contactEmail; }
            set { this._contactEmail = value; }
        }
        #endregion

        private byte _branchId;
        private string _countryName;
        private string _companyName;
        private string _street;
        private string _suburb;
        private string _zipCode;
        private string _lada;
        private string _phoneOne;
        private string _contactEmail;        

    }
}
