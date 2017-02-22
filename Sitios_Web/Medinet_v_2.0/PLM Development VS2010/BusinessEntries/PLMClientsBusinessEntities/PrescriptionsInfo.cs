using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Prescription information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.CodeInfo"/>
    [DataContract]
    public class PrescriptionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PrescriptionsInfo class. Not receive parameters.
        /// </summary>
        public PrescriptionsInfo() 
        {
            this._datePrescription = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrescriptionId.
        ///     </para>
        ///     <para>
        ///         Prescription identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrescriptionId
        {
            get
            {
                return this._prescriptionId;
            }
            set
            {
                this._prescriptionId = value;
            }
        }

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
        ///         Property which gets or sets a value for DatePrescription.
        ///     </para>
        ///     <para>
        ///         Date Prescription.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? DatePrescription
        {
            get
            {
                return this._datePrescription;
            }
            set
            {
                this._datePrescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Patient.
        ///     </para>
        ///     <para>
        ///         Patient name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Patient
        {
            get
            {
                return this._patient;
            }
            set
            {
                this._patient = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Folio.
        ///     </para>
        ///     <para>
        ///         Folio value.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Folio
        {
            get
            {
                return this._folio;
            }
            set
            {
                this._folio = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Comments.
        ///     </para>
        ///     <para>
        ///         Medical comments.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this._comments = value;
            }
        }

        #endregion

        private int _prescriptionId;
        private int _clientId;
        private int _codeId;
        private DateTime? _datePrescription;
        private string _patient;
        private string _folio;
        private string _comments;

    }
}
