using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceDetailInfo information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]

    public  class PharmacovigilanceDetailInfo
    {
          #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceDetailInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceDetailInfo() { }

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
        public PatientInfo Patient
        {
            get
            {
                return
                 this._patient;
            }

            set
            {
                this._patient = value;
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
        public PharmacovigilanceParentDrugInfo PharmacovigilanceParentDrug
        {
            get { return this._pharmacovigilanceParentDrug; }
            set { this._pharmacovigilanceParentDrug = value; }
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
        public PharmacovigilanceSourceDetailInfo PharmacovigilanceSource
        {
            get { return this._sourceInfo; }
            set { this._sourceInfo = value; }
        }



        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AnswerByQuestion.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.AnswerByQuestionInfo"/>..
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.AnswerByQuestionInfo> AnswerByQuestion
        {
            get
            {
                return this._answerByQuestion;
            }

            set
            {
                this._answerByQuestion = new List<PLMClientsBusinessEntities.AnswerByQuestionInfo>();
                foreach (PLMClientsBusinessEntities.AnswerByQuestionInfo detailItem in value)
                    this._answerByQuestion.Add(detailItem);

            }

        }

        ///// <summary>
        /////     <para>
        /////         Property which gets or sets a value for ChildrenDrugsInfo
        /////     </para>
        /////     <para>
        /////         It is a list of objects of type <see cref="PLMClientsBusinessEntities.PharmacovigilanceParentDrugInfo"/>..
        /////     </para>
        ///// </summary>
        //[DataMember]
        //public List<PLMClientsBusinessEntities.PharmacovigilanceParentDrugInfo> ChildrenPharmacovigilanceDrugs
        //{
        //    get
        //    {
        //        return this._children;
        //    }

        //    set
        //    {
        //        this._children = new List<PLMClientsBusinessEntities.PharmacovigilanceParentDrugInfo>();
        //        foreach (PLMClientsBusinessEntities.PharmacovigilanceParentDrugInfo vigialnceDrugsInfo in value)
        //            this._children.Add(vigialnceDrugsInfo);

        //    }

        //}



        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AdverseReaction.
        ///     </para>
        ///     <para>
        ///         It is of objects of type <see cref="PLMClientsBusinessEntities.AdverseReactionInfo"/>..
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.AdverseReactionByIdentifierInfo> AdverseReaction
        {
            get
            {
                return this._adverseReaction;
            }

            set
            {
                this._adverseReaction = new List<PLMClientsBusinessEntities.AdverseReactionByIdentifierInfo>();
                foreach (PLMClientsBusinessEntities.AdverseReactionByIdentifierInfo adverReaction in value)
                    this._adverseReaction.Add(adverReaction);
            }

        }


        #endregion

        private int _pharmacovigilanceId;
        private byte _pharmaTypeId;
        private int? _companyClientId;
        private string _companyClient;

        private PatientInfo _patient = new PatientInfo();
        //private PharmacovigilanceDrugInfo _pharmacovigilanceDrug = new PharmacovigilanceDrugInfo();
        private PharmacovigilanceParentDrugInfo _pharmacovigilanceParentDrug = new PharmacovigilanceParentDrugInfo();
        private PharmacovigilanceSourceDetailInfo _sourceInfo = new PharmacovigilanceSourceDetailInfo();
        private List<AnswerByQuestionInfo> _answerByQuestion = new List<AnswerByQuestionInfo>();

        //private List<PharmacovigilanceParentDrugInfo> _children = new List<PharmacovigilanceParentDrugInfo>();

        private List<AdverseReactionByIdentifierInfo> _adverseReaction = new List<AdverseReactionByIdentifierInfo>();
       

       

    }
}
