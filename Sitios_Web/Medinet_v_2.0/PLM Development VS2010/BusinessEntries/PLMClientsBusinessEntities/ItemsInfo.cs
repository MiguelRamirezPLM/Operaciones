using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the content of a item associated with a physical activity.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ActivityImagesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.PhysicalActivitiesInfo"/>
    [DataContract]
    public class ItemsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ItemsInfo class. Not receive parameters.
        /// </summary>
        public ItemsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ItemId.
        ///     </para>
        ///     <para>
        ///         Item identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ItemId
        {
            get
            {
                return this._itemId;
            }
            set
            {
                this._itemId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ItemName.
        ///     </para>
        ///     <para>
        ///         Item name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                this._itemName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the item is enabled or disabled.
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

        private byte _itemId;
        private string _itemName;
        private bool _active;

    }
}
