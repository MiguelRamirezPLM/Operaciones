using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
        /// <summary>
    ///     Class which represents the Events information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EventCongressDetailInfo
    {
       #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventsInfo class. Not receive parameters.
        /// </summary>
        public EventCongressDetailInfo() 
        {
            _initialDate = Convert.ToDateTime("1900/01/01");
            _finalDate = Convert.ToDateTime("1900/01/01");
        }
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EventId.
        ///     </para>
        ///     <para>
        ///         Event identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EventId
        {
            get
            {
                return this._eventId;
            }
            set
            {
                this._eventId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeId.
        ///     </para>
        ///     <para>
        ///         Event type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EventName.
        ///     </para>
        ///     <para>
        ///         Event name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EventName
        {
            get
            {
                return this._eventName;
            }
            set
            {
                this._eventName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Site.
        ///     </para>
        ///     <para>
        ///         Host event.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InitialDate.
        ///     </para>
        ///     <para>
        ///         Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get
            {
                return this._initialDate;
            }
            set
            {
                this._initialDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         End date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
        {
            get
            {
                return this._finalDate;
            }
            set
            {
                this._finalDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Organizer.
        ///     </para>
        ///     <para>
        ///         Organizer name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Organizer
        {
            get
            {
                return this._organizer;
            }
            set
            {
                this._organizer = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WebPage.
        ///     </para>
        ///     <para>
        ///         Event website.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WebPage
        {
            get
            {
                return this._webPage;
            }
            set
            {
                this._webPage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Event is enabled or disabled.
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CategoryId.
        ///     </para>
        ///     <para>
        ///         Event type identifier.
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
        #endregion

        private int _eventId;
        private byte _typeId;
        private string _eventName;
        private string _site;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private string _organizer;
        private string _webPage;
        private byte _categoryId;
        private bool _active;
    }
}
