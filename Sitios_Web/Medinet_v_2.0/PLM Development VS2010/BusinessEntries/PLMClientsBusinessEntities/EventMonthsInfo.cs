using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the events grouped by month.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EventMonthsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventMonthsInfo class. Not receive parameters.
        /// </summary>
        public EventMonthsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Month.
        ///     </para>
        ///     <para>
        ///         Month which is associated with events.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Month
        {
            get
            {
                return this._month;
            }
            set
            {
                this._month = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Year.
        ///     </para>
        ///     <para>
        ///         Year which is associated with events.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Year
        {
            get
            {
                return this._year;
            }
            set
            {
                this._year = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Events.
        ///     </para>
        ///     <para>
        ///         Events which are going to happen on year and month specified.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<EventsDetailInfo> Events
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

        private string _month;
        private string _year;
        private List<EventsDetailInfo> _events;

    }
}
