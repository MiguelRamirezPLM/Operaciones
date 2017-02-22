using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Company Client information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class CompanyClientsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CompanyClientsInfo class. Not receive parameters.
        /// </summary>
        public CompanyClientsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company Client identifier.
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
        ///         Property which gets or sets a value for CCTypeId.
        ///     </para>
        ///     <para>
        ///         Company Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CCTypeId
        {
            get
            {
                return this._ccTypeId;
            }
            set
            {
                this._ccTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyName.
        ///     </para>
        ///     <para>
        ///         Company Client name.
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Company is enabled or disabled.
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

        private int _companyClientId;
        private byte _ccTypeId;
        private string _companyName;
        private bool _active;

    }
}
