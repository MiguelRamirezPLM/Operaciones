using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between OSMobiles and Editions and Menues.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EditionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.MenuInfo"/>
    [DataContract]
    public class OSMobileMenuInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the OSMobileMenuInfo class. Not receive parameters.
        /// </summary>
        public OSMobileMenuInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OSMobileId.
        ///     </para>
        ///     <para>
        ///         OSMobile identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte OSMobileId
        {
            get { return this._osMobileId; }
            set { this._osMobileId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for MenuId.
        ///     </para>
        ///     <para>
        ///         Menu identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte MenuId
        {
            get { return this._menuId; }
            set { this._menuId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Order.
        ///     </para>
        ///     <para>
        ///         Deployment order.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte Order
        {
            get { return this._order; }
            set { this._order = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ImageName.
        ///     </para>
        ///     <para>
        ///         Image name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ImageName
        {
            get { return this._imageName; }
            set { this._imageName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Image path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the OSMobile Menu is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _osMobileId;
        private int _editionId;
        private byte _menuId;
        private byte _order;
        private string _imageName;
        private string _baseUrl;
        private bool _active;

    }
}
