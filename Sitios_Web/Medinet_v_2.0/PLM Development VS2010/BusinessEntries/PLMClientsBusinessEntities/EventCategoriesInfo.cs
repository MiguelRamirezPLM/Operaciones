using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the event category information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EventsInfo"/>
    [DataContract]
    public class EventCategoriesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventCategoriesInfo class. Not receive parameters.
        /// </summary>
        public EventCategoriesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Category identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CategoryId
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                this._categoryId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryName.
        ///     </para>
        ///     <para>
        ///         Category name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                this._categoryName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the Category is enabled or disabled.
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

        private byte _categoryId;
        private string _categoryName;
        private bool _active;

    }
}
