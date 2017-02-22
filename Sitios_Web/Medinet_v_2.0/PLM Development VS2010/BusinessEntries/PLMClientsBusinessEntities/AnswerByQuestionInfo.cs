using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the AnswerByQuestionInfo information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]

    public class AnswerByQuestionInfo
    {

         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AnswerByQuestionInfo class. Not receive parameters.
        /// </summary> 
        public AnswerByQuestionInfo() { }

        #endregion


        #region Properties

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
        ///         Property which gets or sets a value for Answer Description.
        ///     </para>
        ///     <para>
        ///         Answer Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AnswerDescription
        {
            get { return this._answerDescription; }
            set { this._answerDescription = value; }
        }
     

        #endregion


        private int _questionId;
        private int _questionTypeId;
        private int _answerId;
        private string _answerDescription;

    }

   
       
       


}
