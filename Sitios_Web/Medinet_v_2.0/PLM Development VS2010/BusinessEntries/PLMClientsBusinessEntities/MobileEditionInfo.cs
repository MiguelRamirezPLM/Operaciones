using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between OSMobiles and Editions.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EditionInfo"/>
    [DataContract]
    public class MobileEditionInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MobileEditionInfo class. Not receive parameters.
        /// </summary>
        public MobileEditionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OsMobileId.
        ///     </para>
        ///     <para>
        ///         OSMobile identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte OsMobileId
        {
            get { return this.osMobileId; }
            set { this.osMobileId = value; }
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

        #endregion

        private byte osMobileId;
        private int _editionId;
        private string _isbn;
        private string _fileName;
        private string _baseUrl;

    }
}
