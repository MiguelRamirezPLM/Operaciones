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
    public class MobileMenuesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MobileMenuesInfo class. Not receive parameters.
        /// </summary>
        public MobileMenuesInfo() { }

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
        ///         Property which gets or sets a value for MenuName.
        ///     </para>
        ///     <para>
        ///         Menu name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string MenuName
        {
            get { return this._menuName; }
            set { this._menuName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Menu short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
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

        #endregion

        private byte _osMobileId;
        private int _editionId;
        private byte _menuId;
        private string _menuName;
        private string _shortName;
        private byte _order;
        private string _imageName;
        private string _baseUrl;

    }
}
