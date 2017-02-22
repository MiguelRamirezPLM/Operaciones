using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Pharmacovigilance Drug Questions information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]

    public class PharmacovigilanceDrugQuestionsInfo
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceDrugQuestionsInfo class. Not receive parameters.
        /// </summary>


        public PharmacovigilanceDrugQuestionsInfo() { }

        #endregion

        #region Properties 


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for pharmacovigilanceDrugsId.
        ///     </para>
        ///     <para>
        ///       pharmacovigilance Drugs identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmacovigilanceDrugId
        {
            get { return this._pharmacovigilanceDrugsId; }
            set { this._pharmacovigilanceDrugsId = value; }
        }

        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionId.
        ///     </para>
        ///     <para>
        ///         Question identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int QuestionId
        {
            get { return this._questionId; }
            set { this._questionId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionId.
        ///     </para>
        ///     <para>
        ///         Question identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int QuestionTypeId
        {
            get { return this._questionTypeId; }
            set { this._questionTypeId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AnswerId.
        ///     </para>
        ///     <para>
        ///         Answer identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int AnswerId
        {
            get { return this._answerId; }
            set { this._answerId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for  Description.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrugQuestions Description.
        ///     </para>
        /// </summary>
        [DataMember]

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }




        #endregion

        private int _pharmacovigilanceDrugsId;
        private int _questionId;
        private int _questionTypeId;
        private int _answerId;
        private string _description; 
    }
}
