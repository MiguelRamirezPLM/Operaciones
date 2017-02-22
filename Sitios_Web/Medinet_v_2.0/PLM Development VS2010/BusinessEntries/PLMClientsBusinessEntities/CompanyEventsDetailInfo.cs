using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class whcih represents the relation between an events and its sponsor.
    /// </summary>
    [DataContract]
    public class CompanyEventsDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CompanyEventsDetailInfo class. Not receive parameters.
        /// </summary>
        public CompanyEventsDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyClientId
        {
            get
            {
                return this._companyClientId;
            }
            set
            {
                this._companyClientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Company name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyImage.
        ///     </para>
        ///     <para>
        ///         Company logo.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyImage
        {
            get
            {
                return this._companyImage;
            }
            set
            {
                this._companyImage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Logo Repository.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        #endregion

        private int _companyClientId;
        private string _companyName;
        private string _companyImage;
        private string _baseUrl;

    }
}
