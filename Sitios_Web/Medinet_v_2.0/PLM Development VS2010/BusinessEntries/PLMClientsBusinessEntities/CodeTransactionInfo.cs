using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the Code transactions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.TransactionInfo"/>
    [Serializable]
    [DataContract]
    public class CodeTransactionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CodeTransactionInfo class. Not receive parameters.
        /// </summary>
        public CodeTransactionInfo() 
        {
            this._tranDate = Convert.ToDateTime("01/01/1900");
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
        ///         Property which gets or sets a value for TransactionId.
        ///     </para>
        ///     <para>
        ///         Transaction identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TransactionId
        {
            get { return this._transactionId; }
            set { this._transactionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TranDate.
        ///     </para>
        ///     <para>
        ///         Transaction date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime TranDate
        {
            get { return this._tranDate; }
            set { this._tranDate = value; }
        }

        #endregion

        private int _codeId;
        private byte _transactionId;
        private DateTime _tranDate;

    }
}
