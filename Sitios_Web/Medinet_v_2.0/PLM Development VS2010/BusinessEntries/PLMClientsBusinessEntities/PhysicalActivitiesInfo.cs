using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Physical activities information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ActivityImagesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ItemsInfo"/>
    [DataContract]
    public class PhysicalActivitiesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PhysicalActivitiesInfo class. Not receive parameters.
        /// </summary>
        public PhysicalActivitiesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActivityId.
        ///     </para>
        ///     <para>
        ///         Physical activity identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ActivityId
        {
            get
            {
                return this._activityId;
            }
            set
            {
                this._activityId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActivityName.
        ///     </para>
        ///     <para>
        ///         Physical activity name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActivityName
        {
            get
            {
                return this._activityName;
            }
            set
            {
                this._activityName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the physical activity is enabled or disabled.
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

        private byte _activityId;
        private string _activityName;
        private bool _active;

    }
}
