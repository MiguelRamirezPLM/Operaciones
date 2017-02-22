using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a Country.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Country which is published Edition of a Book.
    ///     </para>
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.BooksInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    [DataContract]
    public class CountriesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CountriesInfo class. Not receive parameters.
        /// </summary>
        public CountriesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CountryName
        {
            get { return this._countryName; }
            set { this._countryName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property that gets or sets a value for ID.
        ///     </para>
        ///     <para>
        ///         Identification key of a country.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property that gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Country is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _countryId;
        private string _countryName;
        private string _id;
        private bool _active;
    }
}
