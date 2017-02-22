using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between Clients and Codes.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    [DataContract]
    public class ClientCodesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientCodesInfo class. Not receive parameters.
        /// </summary>
        public ClientCodesInfo() 
        {
            this._prescriptionFolio = null;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get
            {
                return this._codeId;
            }
            set
            {
                this._codeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrescriptionFolio.
        ///     </para>
        ///     <para>
        ///         Prescription Folio.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? PrescriptionFolio
        {
            get
            {
                return this._prescriptionFolio;
            }
            set
            {
                this._prescriptionFolio = value;
            }
        }
        
        #endregion

        private int _clientId;
        private int _codeId;
        private int? _prescriptionFolio;

    }
}
