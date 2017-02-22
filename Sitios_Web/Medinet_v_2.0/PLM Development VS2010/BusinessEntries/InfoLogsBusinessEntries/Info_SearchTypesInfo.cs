using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InfoLogsBusinessEntries
{

    /// <summary>
    ///     Class which represents the query types of electronic tools.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class Info_SearchTypesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Info_SearchTypesInfo class. Not receives parameters.
        /// </summary>
        public Info_SearchTypesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SearchTypeId.
        ///     </para>
        ///     <para>
        ///         Search type's identifier.
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
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Type Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the search type is enabled or disabled.
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

        private byte _searchTypeId;
        private string _typeName;
        private bool _active;

    }
}
