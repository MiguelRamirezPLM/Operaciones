using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Distribution Pharma information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]

   public class DistributionCodePrefixesEventInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionsInfo class. Not receive parameters.
        /// </summary>
        public DistributionCodePrefixesEventInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DistributionId
        {
            get
            {
                return this._distributionId;
            }
            set
            {
                this._distributionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Prefix identifier
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get
            {
                return this._prefixId;
            }
            set
            {
                this._prefixId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Target identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte  TargetId
        {
            get
            {
                return this._targetId;
            }
            set
            {
                this._targetId = value;
            }
        }

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
        ///         Property which gets or sets a value for Site.
        ///     </para>
        ///     <para>
        ///         Site description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Site
        {
            get { return this._site; }
            set { this._site = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EventName.
        ///     </para>
        ///     <para>
        ///         Name the event.
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
        ///         Property which gets or sets a value for DistributionName.
        ///     </para>
        ///     <para>
        ///         Name the distribution.
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


 
        #endregion

        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private int _eventId;
        private string _site;
        private string _eventName;

        private string _categoryName;
   

    }
}
