using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Company Client Types information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.CompanyClientsInfo"/>
    [DataContract]
    public class CompanyClientTypesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CompanyClientTypesInfo class. Not receive parameters.
        /// </summary>
        public CompanyClientTypesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchId.
        ///     </para>
        ///     <para>
        ///         Branch identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CCTypeId
        {
            get
            {
                return this._cCTypeId;
            }
            set
            {
                this._cCTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CCTypeName.
        ///     </para>
        ///     <para>
        ///         Company Client Type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CCTypeName
        {
            get
            {
                return this._cCTypeName;
            }
            set
            {
                this._cCTypeName = value;
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Company Client Type is enabled or disabled.
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

        private byte _cCTypeId;
        private string _cCTypeName;
        private bool _active;

    }
}
