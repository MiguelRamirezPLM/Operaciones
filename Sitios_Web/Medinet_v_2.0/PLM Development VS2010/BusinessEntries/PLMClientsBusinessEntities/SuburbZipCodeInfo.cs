using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the SuburbZipCodeInfo information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class SuburbZipCodeInfo
    {
           #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SuburbZipCodeInfo class. Not receive parameters.
        /// </summary>
        public SuburbZipCodeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SuburbId.
        ///     </para>
        ///     <para>
        ///         Suburb Identifier
        ///     </para>
        /// </summary>
        [DataMember]
        public int SuburbId
        {
            get
            {
                return this._suburbId;
            }
            set
            {
                this._suburbId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCodeId.
        ///     </para>
        ///     <para>
        ///         ZipCode Identifier
        ///     </para>
        /// </summary>
        [DataMember]
        public int ZipCodeId
        {
            get
            {
                return this._zipCodeId;
            }
            set
            {
                this._zipCodeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SuburbName.
        ///     </para>
        ///     <para>
        ///         Suburb name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SuburbName
        {
            get
            {
                return this._SuburbName;
            }
            set
            {
                this._SuburbName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         ZipCode
        ///     </para>
        /// </summary>
        [DataMember]
        public string Zipcode
        {
            get
            {
                return this._zipCode;
            }
            set
            {
                this._zipCode = value;
            }
        }

        #endregion
        private int _suburbId;
        private int _zipCodeId;
        private string _SuburbName;
        private string _zipCode;
    }
}
