using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Code transactions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class TransactionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TransactionInfo class. Not receive parameters.
        /// </summary>
        public TransactionInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for TranName.
        ///     </para>
        ///     <para>
        ///         Transaction name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TranName
        {
            get { return this._tranName; }
            set { this._tranName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Transaction is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _transactionId;
        private string _tranName;
        private bool _active;

    }
}
