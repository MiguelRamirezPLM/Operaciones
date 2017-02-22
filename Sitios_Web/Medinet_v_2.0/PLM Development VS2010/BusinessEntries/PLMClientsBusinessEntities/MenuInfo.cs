using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Menu information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class MenuInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MenuInfo class. Not receive parameters.
        /// </summary>
        public MenuInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Menu description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Menu is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _menuId;
        private string _menuName;
        private string _shortName;
        private string _description;
        private bool _active;

    }
}
