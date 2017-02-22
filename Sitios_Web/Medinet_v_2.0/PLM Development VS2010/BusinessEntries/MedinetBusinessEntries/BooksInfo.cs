using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Book information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class BooksInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the BooksInfo class. Not receive parameters.
        /// </summary>
        public BooksInfo() { }

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
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Book Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Book Name.
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
        ///         Indicates if the Book is enabled.
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
        private string _description;
        private string _shortName;
        private bool _active;

    }
}
