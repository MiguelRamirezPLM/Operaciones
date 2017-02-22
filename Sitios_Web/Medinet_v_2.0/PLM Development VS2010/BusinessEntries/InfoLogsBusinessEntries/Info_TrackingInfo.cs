using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InfoLogsBusinessEntries
{

    /// <summary>
    ///     Class which represents the query information of electronic tools.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="InfoLogsBusinessEntries.Info_EntitiesInfo"/>
    /// <seealso cref="InfoLogsBusinessEntries.Info_SearchTypesInfo"/>
    /// <seealso cref="InfoLogsBusinessEntries.Info_SourceInfo"/>
    [DataContract]
    public class Info_TrackingInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Info_TrackingInfo class. Not receives parameters.
        /// </summary>
        public Info_TrackingInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TrackId.
        ///     </para>
        ///     <para>
        ///         Query identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TrackId
        {
            get
            {
                return this._trackId;
            }
            set
            {
                this._trackId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the query's got a parent query.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeString.
        ///     </para>
        ///     <para>
        ///         Code associated with the client which made the query.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CodeString
        {
            get
            {
                return this._codeString;
            }
            set
            {
                this._codeString = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SearchDate.
        ///     </para>
        ///     <para>
        ///         Date which the query was made.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? SearchDate
        {
            get
            {
                return this._searchDate;
            }
            set
            {
                this._searchDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SourceId.
        ///     </para>
        ///     <para>
        ///         Source identifier associated with the query.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte SourceId
        {
            get
            {
                return this._sourceId;
            }
            set
            {
                this._sourceId = value;
            }
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
            get
            {
                return this._searchTypeId;
            }
            set
            {
                this._searchTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EntityId.
        ///     </para>
        ///     <para>
        ///         Query entity identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EntityId
        {
            get
            {
                return this._entityId;
            }
            set
            {
                this._entityId = value;
            }
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
            get
            {
                return this._searchParameters;
            }
            set
            {
                this._searchParameters = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for JSONFormat.
        ///     </para>
        ///     <para>
        ///         JavaScript Object Notation. Lightweight format for data exchange.
        ///     </para>
        /// </summary>
        [DataMember]
        public string JSONFormat
        {
            get
            {
                return this._jsonFormat;
            }
            set
            {
                this._jsonFormat = value;
            }
        }

        #endregion

        private int _trackId;
        private int? _parentId;
        private string _codeString;
        private DateTime? _searchDate;
        private byte _sourceId;
        private byte _searchTypeId;
        private int _entityId;
        private string _searchParameters;
        private string _jsonFormat;

    }
}
