using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the images associated with a physical activity.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.PhysicalActivitiesInfo"/>
    [DataContract]
    public class ActivityImagesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ActivityImagesInfo class. Not receive parameters.
        /// </summary>
        public ActivityImagesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ImageId.
        ///     </para>
        ///     <para>
        ///         Image identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ImageId
        {
            get
            {
                return this._imageId;
            }
            set
            {
                this._imageId = value;
            }
        }

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
        ///         Property which gets or sets a value for ImageName.
        ///     </para>
        ///     <para>
        ///         Image name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ImageName
        {
            get
            {
                return this._imageName;
            }
            set
            {
                this._imageName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Image repository.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the Image is enabled or disabled.
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

        private int _imageId;
        private byte _activityId;
        private string _imageName;
        private string _baseUrl;
        private bool _active;

    }
}
