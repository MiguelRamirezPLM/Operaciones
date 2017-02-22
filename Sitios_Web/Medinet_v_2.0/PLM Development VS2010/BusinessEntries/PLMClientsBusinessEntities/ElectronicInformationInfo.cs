using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents information of electronic tools.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ElectronicInformationInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ElectronicInformationInfo class. Not receive parameters.
        /// </summary>
        public ElectronicInformationInfo() 
        {
            this._publishedDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElectronicId.
        ///     </para>
        ///     <para>
        ///         Tool identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ElectronicId
        {
            get
            {
                return this._electronicId;
            }
            set
            {
                this._electronicId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InfTypeId.
        ///     </para>
        ///     <para>
        ///         Tool Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte InfoTypeId
        {
            get
            {
                return this._infoTypeId;
            }
            set
            {
                this._infoTypeId = value;
            }
        }

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
        ///         Property which gets or sets a value for ElectronicTitle.
        ///     </para>
        ///     <para>
        ///         Tool Title.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElectronicTitle
        {
            get
            {
                return this._electronicTitle;
            }
            set
            {
                this._electronicTitle = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElectronicDescription.
        ///     </para>
        ///     <para>
        ///         Tool Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElectronicDescription
        {
            get
            {
                return this._electronicDescription;
            }
            set
            {
                this._electronicDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublishedDate.
        ///     </para>
        ///     <para>
        ///         Publication date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime PublishedDate
        {
            get
            {
                return this._publishedDate;
            }
            set
            {
                this._publishedDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         Tool file.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Link.
        ///     </para>
        ///     <para>
        ///         Web link.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Link
        {
            get
            {
                return this._link;
            }
            set
            {
                this._link = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Tool is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _electronicId;
        private byte _infoTypeId;
        private int _companyClientId;
        private string _electronicTitle;
        private string _electronicDescription;
        private DateTime _publishedDate;
        private string _fileName;
        private string _link;
        private bool _active;

    }
}
