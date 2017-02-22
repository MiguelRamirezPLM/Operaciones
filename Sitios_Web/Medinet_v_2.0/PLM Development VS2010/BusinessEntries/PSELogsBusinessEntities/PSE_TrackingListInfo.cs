using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
{

    /// <summary>
    ///     Class which represents the search's detail of medical information and attribute information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PSELogsBusinessEntities.PSE_EntityInfo"/>
    /// <seealso cref="PSELogsBusinessEntities.PSE_SearchTypeInfo"/>
    /// <seealso cref="PSELogsBusinessEntities.PSE_SourceInfo"/>
    [DataContract]
    public class PSE_TrackingListInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PSE_TrackingListInfo class. Not receives parameters.
        /// </summary>
        public PSE_TrackingListInfo()
        {
            this._attributes = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TrackId.
        ///     </para>
        ///     <para>
        ///         Search identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TrackId
        {
            get { return this._trackId; }
            set { this._trackId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeString.
        ///     </para>
        ///     <para>
        ///         Code associated with the client which made the search.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CodeString
        {
            get { return this._codeString; }
            set { this._codeString = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SearchDate.
        ///     </para>
        ///     <para>
        ///         Date which the search was made.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? SearchDate
        {
            get { return this._searchDate; }
            set { this._searchDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition's identifier which is being queried.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SourceId.
        ///     </para>
        ///     <para>
        ///         Source identifier associated with the search.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte SourceId
        {
            get { return this._sourceId; }
            set { this._sourceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SearchTypeId.
        ///     </para>
        ///     <para>
        ///         Search type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte SearchTypeId
        {
            get { return this._searchTypeId; }
            set { this._searchTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntityId.
        ///     </para>
        ///     <para>
        ///         Search entity identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EntityId
        {
            get { return this._entityId; }
            set { this._entityId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SearchParameters.
        ///     </para>
        ///     <para>
        ///         Search parameters with which the search was made.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SearchParameters
        {
            get { return this._searchParameters; }
            set { this._searchParameters = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Attributes.
        ///     </para>
        ///     <para>
        ///         Indicates the drug attributes which is being queried.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Replicate.
        ///     </para>
        ///     <para>
        ///         Indicates if the search was loaded in the data base.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Replicate
        {
            get { return this._replicate; }
            set { this._replicate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for JsonFormat.
        ///     </para>
        ///     <para>
        ///         Indicates the jsonformat on request
        ///     </para>
        /// </summary>
        [DataMember]
        public string JsonFormat
        {
            get { return this._jsonFormat; }
            set { this._jsonFormat = value; }
        }
        #endregion

        private int _trackId;
        private string _codeString;
        private DateTime? _searchDate;
        private int _editionId;
        private byte _sourceId;
        private byte _searchTypeId;
        private int _entityId;
        private string _searchParameters;
        private string _jsonFormat;
        // Extra class attributes:
        private string _attributes;
        private bool _replicate;

    }
}
