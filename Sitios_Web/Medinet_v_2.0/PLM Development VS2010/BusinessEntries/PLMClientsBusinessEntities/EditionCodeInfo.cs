using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Codes and Editions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.EditionInfo"/>
    [DataContract]
    public class EditionCodeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionCodeInfo class. Not receive parameters.
        /// </summary>
        public EditionCodeInfo() 
        {
            this._initialDate = Convert.ToDateTime("01/01/1900");
            this._finalDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get { return this._codeId; }
            set { this._codeId = value; }
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InitialDate.
        ///     </para>
        ///     <para>
        ///         Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get { return this._initialDate; }
            set { this._initialDate = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         Ending date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
        {
            get { return this._finalDate; }
            set { this._finalDate = value; }
        }
    
        #endregion

        private int _codeId;
        private int _editionId;
        private DateTime _initialDate;
        private DateTime _finalDate;

    }
}
