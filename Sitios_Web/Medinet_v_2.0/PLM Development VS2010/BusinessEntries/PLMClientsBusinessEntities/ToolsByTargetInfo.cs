using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Electronic tools and Countries.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ToolsByTargetInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ToolsByTargetInfo class. Not receive parameters.
        /// </summary>
        public ToolsByTargetInfo() 
        {
            this._publishedDate = null;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryId
        {
            set { this._countryId = value; }
            get { return this._countryId; }
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
            set { this._companyClientId = value; }
            get { return this._companyClientId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolId.
        ///     </para>
        ///     <para>
        ///         Electronic tool identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ToolId
        {
            set { this._toolId = value; }
            get { return this._toolId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Deployment media identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TargetId
        {
            set { this._targetId = value; }
            get { return this._targetId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolTitle.
        ///     </para>
        ///     <para>
        ///         Electronic tool title.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ToolTitle
        {
            set { this._toolTitle = value; }
            get { return this._toolTitle; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolDescription.
        ///     </para>
        ///     <para>
        ///         Electronic tool description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ToolDescription
        {
            set { this._toolDescription = value; }
            get { return this._toolDescription; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         Source file name of the electronic tool.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FileName
        {
            set { this._fileName = value; }
            get { return this._fileName; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Source file name of the electronic tool.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            set { this._baseUrl = value; }
            get { return this._baseUrl; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublishedDate.
        ///     </para>
        ///     <para>
        ///         Publication date of the electronic tool.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? PublishedDate
        {
            set { this._publishedDate = value; }
            get { return this._publishedDate; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Url.
        ///     </para>
        ///     <para>
        ///         Route which will be consumed electronic tool.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Url
        {
            set { this._url = value; }
            get { return this._url; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BannerId.
        ///     </para>
        ///     <para>
        ///         Banner identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? BannerId
        {
            set { this._bannerId = value; }
            get { return this._bannerId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BannerName.
        ///     </para>
        ///     <para>
        ///         Banner name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BannerName
        {
            set { this._bannerName = value; }
            get { return this._bannerName; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BannerUrl.
        ///     </para>
        ///     <para>
        ///         Route which will be consumed Banner.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BannerUrl
        {
            set { this._bannerUrl = value; }
            get { return this._bannerUrl; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolOrder.
        ///     </para>
        ///     <para>
        ///         Indicates the order in which the electronic tool will be displayed.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ToolOrder
        {
            set { this._toolOrder = value; }
            get { return this._toolOrder; }
        }

        #endregion

        private byte _countryId;
        private int _companyClientId;
        private int _toolId;
        private byte _targetId;
        private string _toolTitle;
        private string _toolDescription;
        private string _fileName;
        private string _baseUrl;
        private DateTime? _publishedDate;
        private string _url;
        private int? _bannerId;
        private string _bannerName;
        private string _bannerUrl;
        private int _toolOrder;

    }
}
