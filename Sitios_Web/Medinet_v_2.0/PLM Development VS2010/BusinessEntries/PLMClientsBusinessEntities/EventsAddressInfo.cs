using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Event detail.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EventsInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ProfessionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SpecialityInfo"/>
    [DataContract]
    public class EventsAddressInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventsDetailInfo class. Not receive parameters.
        /// </summary>
        public EventsAddressInfo() 
        {
            
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
        ///         Property which gets or sets a value for AddressId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AddressId
        {
            get
            {
                return this._addressId;
            }
            set
            {
                this._addressId = value;
            }
        }

        #endregion

        private int _eventId;
        private int _addressId;

    }
}
