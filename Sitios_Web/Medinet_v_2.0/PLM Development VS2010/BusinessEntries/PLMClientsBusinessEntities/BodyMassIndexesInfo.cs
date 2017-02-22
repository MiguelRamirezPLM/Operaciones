using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the body mass indexes.
    /// </summary>
    [DataContract]
    public class BodyMassIndexesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BodyMassIndexesInfo class. Not receive parameters.
        /// </summary>
        public BodyMassIndexesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IndexId.
        ///     </para>
        ///     <para>
        ///         Index identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte IndexId
        {
            get
            {
                return this._indexId;
            }
            set
            {
                this._indexId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IndexName.
        ///     </para>
        ///     <para>
        ///         Index name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IndexName
        {
            get
            {
                return this._indexName;
            }
            set
            {
                this._indexName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IndexDescription.
        ///     </para>
        ///     <para>
        ///         Index description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string IndexDescription
        {
            get
            {
                return this._indexDescription;
            }
            set
            {
                this._indexDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the index is enabled or disabled.
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

        private byte _indexId;
        private string _indexName;
        private string _indexDescription;
        private bool _active;

    }
}
