using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between CodePrefixes and Editions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EditionInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodePrefixInfo"/>
    [DataContract]
    public class CodePrefixEditionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodePrefixEditionInfo class. Not receive parameters.
        /// </summary>
        public CodePrefixEditionInfo() {}

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Code prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get { return this._prefixId; }
            set { this._prefixId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }
            
        #endregion

        private int _prefixId;
        private int _editionId;
    }
}
