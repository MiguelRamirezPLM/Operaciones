using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceType information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class PharmacovigilanceInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceId identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmacovigilanceId
        {
            get { return this._pharmacovigilanceId; }
            set { this._pharmacovigilanceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaTypeId.
        ///     </para>
        ///     <para>
        ///         PharmaType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmaTypeId
        {
            get { return this._pharmaTypeId; }
            set { this._pharmaTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClientId.
        ///     </para>
        ///     <para>
        ///         Company Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? CompanyClientId
        {
            get { return this._companyClientId; }
            set { this._companyClientId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CompanyClient.
        ///     </para>
        ///     <para>
        ///         Company Client name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CompanyClient
        {
            get { return this._companyClient; }
            set { this._companyClient = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Patient.
        ///     </para>
        ///     <para>
        ///         It is of objects of type <see cref="PLMClientsBusinessEntities.PatientInfo"/>..
        ///     </para>
        /// </summary>
        [DataMember]
        public int PatientId
        {
            get
            {
                return
                 this._patientId;
            }

            set
            {
                this._patientId = value;
            }

        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceDrugId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrug identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmacovigilanceDrugId
        {
            get
            {
                return this._pharmacovigilanceDrugId;
            }

            set {
                this._pharmacovigilanceDrugId = value;
            }

        }
       

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceSourceId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceSource identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int PharmacovigilanceSourceId
        {
            get
            {
                return this._pharmacovigilanceSourceId;
            }

            set
            {
                this._pharmacovigilanceSourceId = value;
            }

        }



        #endregion

        private int _pharmacovigilanceId;
        private byte _pharmaTypeId;
        private int? _companyClientId;
        private string _companyClient;
        private int _patientId;
        private int _pharmacovigilanceDrugId;
        private int _pharmacovigilanceSourceId;


   

       
      
    }
}
