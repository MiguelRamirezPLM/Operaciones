using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Product information associated with a Company.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PharmacyProductsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacyProductsInfo class. Not receive parameters.
        /// </summary>
        public PharmacyProductsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product identifier.
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
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyClientId
        {
            get
            {
                return this._companyClientId;
            }
            set
            {
                this._companyClientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductName.
        ///     </para>
        ///     <para>
        ///         Product name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaceuticalForms.
        ///     </para>
        ///     <para>
        ///         Product's Pharmaceutical Form.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PharmaceuticalForms
        {
            get
            {
                return this._pharmaceuticalForms;
            }
            set
            {
                this._pharmaceuticalForms = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstances.
        ///     </para>
        ///     <para>
        ///         Active substances that contains the product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstances
        {
            get
            {
                return this._activeSubstances;
            }
            set
            {
                this._activeSubstances = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Indications.
        ///     </para>
        ///     <para>
        ///         Product indications.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Indications
        {
            get
            {
                return this._indications;
            }
            set
            {
                this._indications = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Presentation.
        ///     </para>
        ///     <para>
        ///         Product presentation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Presentation
        {
            get
            {
                return this._presentation;
            }
            set
            {
                this._presentation = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PresentationContent.
        ///     </para>
        ///     <para>
        ///         Presentation content.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PresentationContent
        {
            get
            {
                return this._presentationContent;
            }
            set
            {
                this._presentationContent = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Laboratory.
        ///     </para>
        ///     <para>
        ///         Laboratory which distributes the product.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Laboratory
        {
            get
            {
                return this._laboratory;
            }
            set
            {
                this._laboratory = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Logo.
        ///     </para>
        ///     <para>
        ///         Product logo.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Path where is located the product logo.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Available.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is avaible in a pharmacy.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool? Available
        {
            get
            {
                return this._available;
            }
            set
            {
                this._available = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Product is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _productId;
        private int _companyClientId;
        private string _productName;
        private string _pharmaceuticalForms;
        private string _activeSubstances;
        private string _indications;
        private string _presentation;
        private string _presentationContent;
        private string _laboratory;
        private string _logo;
        private string _baseUrl;
        private bool? _available;
        private bool _active;

    }
}
