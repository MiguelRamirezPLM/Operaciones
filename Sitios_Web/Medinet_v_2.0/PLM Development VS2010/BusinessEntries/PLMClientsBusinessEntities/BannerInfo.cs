using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the banners information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Image which is displayed in an application.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class BannerInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BannerInfo class. Not receive parameters.
        /// </summary>
        public BannerInfo() { }

        #endregion

        #region Properties

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
            get { return this._bannerId; }
            set { this._bannerId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BannerTypeId.
        ///     </para>
        ///     <para>
        ///         Banner Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte BannerTypeId
        {
            get { return this._bannerTypeId; }
            set { this._bannerTypeId = value; }
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
            get { return this._bannerDescription; }
            set { this._bannerDescription = value; }
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
            get { return this._fileName; }
            set { this._fileName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Banner is enabled or disabled.
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
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Banner path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        #endregion

        private int _bannerId;
        private byte _bannerTypeId;
        private string _bannerDescription;
        private string _fileName;
        private bool _active;
        private string _baseUrl;

    }
}
