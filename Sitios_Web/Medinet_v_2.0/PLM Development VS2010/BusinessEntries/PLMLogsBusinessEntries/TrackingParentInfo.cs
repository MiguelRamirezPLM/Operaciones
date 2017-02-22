using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMLogsBusinessEntries
{
    /// <summary>
    ///     Class which represents the search detail of contents information and its results.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class TrackingParentInfo : TrackingListInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TrackingParentInfo class. Not receives parameters.
        /// </summary>
        public TrackingParentInfo() 
        {
            this._childrenTrackingInfoList = new List<TrackingListInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ChildrenTrackingInfo.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMLogsBusinessEntities.TrackingListInfo"/>.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<TrackingListInfo> ChildrenTrackingInfo
        {
            get { return this._childrenTrackingInfoList; }
            set
            {
                this._childrenTrackingInfoList = new List<TrackingListInfo>();
                foreach (TrackingListInfo trackInfo in value)
                    this._childrenTrackingInfoList.Add(trackInfo);
            }
        }

        #endregion

        private List<TrackingListInfo> _childrenTrackingInfoList;
    }
}
