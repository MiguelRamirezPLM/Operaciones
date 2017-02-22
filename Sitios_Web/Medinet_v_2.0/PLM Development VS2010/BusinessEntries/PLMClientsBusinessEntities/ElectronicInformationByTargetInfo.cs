using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents information of electronic tools associated with a Output medium.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ElectronicInformationByTargetInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ElectronicInformationByTargetInfo class. Not receive parameters.
        /// </summary>
        public ElectronicInformationByTargetInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElectronicId.
        ///     </para>
        ///     <para>
        ///         Tool identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ElectronicId
        {
            get
            {
                return this._electronicId;
            }
            set
            {
                this._electronicId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InfTypeId.
        ///     </para>
        ///     <para>
        ///         Tool Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte InfoTypeId
        {
            get
            {
                return this._infoTypeId;
            }
            set
            {
                this._infoTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InfDescription.
        ///     </para>
        ///     <para>
        ///         Tool Type description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InfDescription
        {
            get
            {
                return this._infDescription;
            }
            set
            {
                this._infDescription = value;
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
        ///         Property which gets or sets a value for ElectronicTitle.
        ///     </para>
        ///     <para>
        ///         Tool Title.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElectronicTitle
        {
            get
            {
                return this._electronicTitle;
            }
            set
            {
                this._electronicTitle = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElectronicDescription.
        ///     </para>
        ///     <para>
        ///         Tool Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElectronicDescription
        {
            get
            {
                return this._electronicDescription;
            }
            set
            {
                this._electronicDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublishedDate.
        ///     </para>
        ///     <para>
        ///         Publication date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime PublishedDate
        {
            get
            {
                return this._publishedDate;
            }
            set
            {
                this._publishedDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         Tool file.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HTMLFileName.
        ///     </para>
        ///     <para>
        ///         Document HTML.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HTMLFileName
        {
            get
            {
                return this._htmlFileName;
            }
            set
            {
                this._htmlFileName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Link.
        ///     </para>
        ///     <para>
        ///         Web link.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Link
        {
            get
            {
                return this._link;
            }
            set
            {
                this._link = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         File path.
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
        ///         Property which gets or sets a value for ResolutionBaseUrl.
        ///     </para>
        ///     <para>
        ///         Resolution Path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ResolutionBaseUrl
        {
            get
            {
                return this._resolutionBaseUrl;
            }
            set
            {
                this._resolutionBaseUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Order.
        ///     </para>
        ///     <para>
        ///         Order in which the tool will be displayed.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte Order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        #endregion

        private int _electronicId;
        private byte _infoTypeId;
        private string _infDescription;
        private int _companyClientId;
        private string _electronicTitle;
        private string _electronicDescription;
        private DateTime _publishedDate;
        private string _fileName;
        private string _htmlFileName;
        private string _link;
        private string _baseUrl;
        private string _resolutionBaseUrl;
        private byte _order;

    }
}
