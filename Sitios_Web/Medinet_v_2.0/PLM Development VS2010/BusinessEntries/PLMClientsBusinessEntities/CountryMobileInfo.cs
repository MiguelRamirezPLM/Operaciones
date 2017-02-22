using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Countries and Mobile Menues.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.MobileMenuesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CountryInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.EditionInfo"/>
    [DataContract]
    public class CountryMobileInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CountryMobileInfo class. Not receive parameters.
        /// </summary>
        public CountryMobileInfo() 
        {
            this._menues = new List<MobileMenuesInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier.
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
        ///         Property which gets or sets a value for CountryName.
        ///     </para>
        ///     <para>
        ///         Country name.
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
        ///         Property which gets or sets a value for ID.
        ///     </para>
        ///     <para>
        ///         Country ID.
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
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         Image name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FileName
        {
            get { return this._fileName; }
            set { this._fileName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Image path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         CSS path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CSSUrl
        {
            get
            {
                return this._cssUrl;
            }
            set
            {
                this._cssUrl = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentImagesUrl.
        ///     </para>
        ///     <para>
        ///         Images path.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ContentImagesUrl
        {
            get 
            {
                return this._contentImagesUrl;
            }
            set
            {
                this._contentImagesUrl = value;
            }
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
        ///         Property which gets or sets a value for Menues.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.MobileMenuesInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MobileMenuesInfo> Menues
        {
            get { return this._menues; }
            set {
                this._menues = new List<MobileMenuesInfo>();
                foreach (MobileMenuesInfo menu in value)
                {   
                    this._menues.Add(menu);
                }
            }
        }

        #endregion

        private byte _countryId;
        private string _countryName;
        private string _id;
        private string _fileName;
        private string _baseUrl;
        private string _cssUrl;
        private string _contentImagesUrl;
        private int _editionId;
        private string _isbn;
        private List<MobileMenuesInfo> _menues;

    }
}
