using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the event category and its events.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EventsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.AddressInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ProfessionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SpecialityInfo"/>
    [DataContract]
    public class EventCategoryDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CategoryEventDetailInfo class. Not receive parameters.
        /// </summary>
        public EventCategoryDetailInfo() { }

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
        ///         Property which gets or sets a value for Events.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.EventsDetailInfo"/>. Indicates if the Category has got Events.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.EventsDetailInfo> Events
        {
            get
            {
                return this._events;
            }
            set
            {
                this._events = new List<PLMClientsBusinessEntities.EventsDetailInfo>();

                foreach (PLMClientsBusinessEntities.EventsDetailInfo eventItem in value)
                    this._events.Add(eventItem);
            }
        }

        #endregion

        private byte _categoryId;
        private string _categoryName;
        private List<PLMClientsBusinessEntities.EventsDetailInfo> _events;

    }
}
