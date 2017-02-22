using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the physical activity detail
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ActivityImagesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.ItemDetailInfo"/>
    [DataContract]
    public class PhysicalActivityDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PhysicalActivityDetailInfo class. Not receive parameters.
        /// </summary>
        public PhysicalActivityDetailInfo() { }

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
        ///         Property which gets or sets a value for ActivityItems.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.ItemDetailInfo"/>. It indicates if the physical activity has got Items.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.ItemDetailInfo> ActivityItems
        {
            get
            {
                return this._activityItems;
            }
            set
            {
                this._activityItems = new List<PLMClientsBusinessEntities.ItemDetailInfo>();
                foreach (PLMClientsBusinessEntities.ItemDetailInfo item in value)
                    this._activityItems.Add(item);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActivityImages.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.ActivityImagesInfo"/>. It indicates if the physical activity has got Images.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.ActivityImagesInfo> ActivityImages
        {
            get
            {
                return this._activityImages;
            }
            set
            {
                this._activityImages = new List<PLMClientsBusinessEntities.ActivityImagesInfo>();
                foreach (PLMClientsBusinessEntities.ActivityImagesInfo image in value)
                    this._activityImages.Add(image);
            }
        }

        #endregion

        private byte _activityId;
        private string _activityName;
        private List<PLMClientsBusinessEntities.ItemDetailInfo> _activityItems;
        private List<PLMClientsBusinessEntities.ActivityImagesInfo> _activityImages;

    }
}
