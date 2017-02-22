using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the search's detail of medical information and its results.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PSELogsBusinessEntities.PSE_EntityInfo"/>
    /// <seealso cref="PSELogsBusinessEntities.PSE_SearchTypeInfo"/>
    /// <seealso cref="PSELogsBusinessEntities.PSE_SourceInfo"/>
    public class PSE_TrackingParentInfo : PSE_TrackingListInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_TrackingParentInfo class. Not receives parameters.
        /// </summary>
        public PSE_TrackingParentInfo()
        {
            this._childrenTrackingInfoList = new List<PSE_TrackingListInfo>();    
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ChildrenTrackingInfo.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PSELogsBusinessEntities.PSE_TrackingListInfo"/>.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PSE_TrackingListInfo> ChildrenTrackingInfo
        {
            get { return this._childrenTrackingInfoList; }
            set 
            {
                foreach (PSE_TrackingListInfo trackInfo in value)
                    this._childrenTrackingInfoList.Add(trackInfo);
            }
        }


        #endregion

        private List<PSE_TrackingListInfo> _childrenTrackingInfoList;
    }
}
