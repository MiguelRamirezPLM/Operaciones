using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Country and Company Client and Banners.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Image which is displayed in an application.
    ///     </para>
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.BannerInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CountryInfo"/>
    [DataContract]
    public class BannersByTargetInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BannersByTargetInfo class. Not receive parameters.
        /// </summary>
        public BannersByTargetInfo() { }

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
        ///         CompanyClient identifier.
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
        ///         Property which gets or sets a value for BannerId.
        ///     </para>
        ///     <para>
        ///         Banner identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BannerId
        {
            set { this._bannerId = value; }
            get { return this._bannerId; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Output medium identifier.
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
        ///         Property which gets or sets a value for BannerDescription.
        ///     </para>
        ///     <para>
        ///         Banner description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BannerDescription
        {
            set { this._bannerDescription = value; }
            get { return this._bannerDescription; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         Image name.
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
        ///         Banner path.
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
        ///         Property which gets or sets a value for BannerOrder.
        ///     </para>
        ///     <para>
        ///         Deployment order.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BannerOrder
        {
            set { this._bannerOrder = value; }
            get { return this._bannerOrder; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Url.
        ///     </para>
        ///     <para>
        ///         Web URL.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Url
        {
            set { this._url = value; }
            get { return this._url; }
        }

        #endregion

        private byte _countryId;
        private int _companyClientId;
        private int _bannerId;
        private byte _targetId;
        private string _bannerDescription;
        private string _fileName;
        private string _baseUrl;
        private int _bannerOrder;
        private string _url;

    }
}
