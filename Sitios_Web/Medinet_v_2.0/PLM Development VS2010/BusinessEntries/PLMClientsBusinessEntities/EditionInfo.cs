using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Edition information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CountryInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.BookInfo"/>
    [Serializable]
    [DataContract]
    public class EditionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionInfo class. Not receive parameters.
        /// </summary>
        public EditionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition Identifier.
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
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Identifier of the source edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier associated with the Edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BookId.
        ///     </para>
        ///     <para>
        ///         Book identifier associated with the Edition.
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
        ///         Property which gets or sets a value for NumberEdition.
        ///     </para>
        ///     <para>
        ///         Edition number.
        ///     </para>
        /// </summary>
        [DataMember]
        public int NumberEdition
        {
            get { return this._numberEdition; }
            set { this._numberEdition = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ISBN.
        ///     </para>
        ///     <para>
        ///         International Standard Book Number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ISBN
        {
            get { return this._isbn; }
            set { this._isbn = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BarCode.
        ///     </para>
        ///     <para>
        ///         Bar code associated with an edition.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BarCode
        {
            get { return this._barCode; }
            set { this._barCode = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Edition is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _editionId;
        private int? _parentId;
        private byte _countryId;
        private int _bookId;
        private int _numberEdition;
        private string _isbn;
        private string _barCode;
        private bool _active;

    }
}
