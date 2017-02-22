using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Book information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class BookInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BookInfo class. Not receive parameters.
        /// </summary>
        public BookInfo() { }

        #endregion 

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BookId.
        ///     </para>
        ///     <para>
        ///         Book identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int BookId
        {
            get { return this._bookId; }
            set { this._bookId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BookName.
        ///     </para>
        ///     <para>
        ///         Book name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BookName
        {
            get { return this._bookName; }
            set { this._bookName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Book short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Book is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _bookId;
        private string _bookName;
        private string _shortName;
        private bool _active;

    }
}
