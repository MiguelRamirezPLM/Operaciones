    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the QuestionDetailInfo information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]

    public class QuestionDetailInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionDetailInfo class. Not receive parameters.
        /// </summary> 
        public QuestionDetailInfo() { }

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
        ///         Property which gets or sets a value for Question Description.
        ///     </para>
        ///     <para>
        ///         Question Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
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
        ///         Property which gets or sets a value for Question type Description.
        ///     </para>
        ///     <para>
        ///         Question type Description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QuestionType
        {
            get { return this._questionType; }
            set { this._questionType = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionOrder.
        ///     </para>
        ///     <para>
        ///         Question Order  identifier.
        ///     </para>
        /// </summary>
            [DataMember]
        public int QuestionOrder
        {
            get { return this._questionOrder; }
            set { this._questionOrder = value; }
        }


       

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Answere.
        ///     </para>
        ///     <para>
            ///         It is of objects of type <see cref="PLMClientsBusinessEntities.AnswerOrderInfo"/>.
        ///     </para>
        /// </summary>
          [DataMember]
          public List<PLMClientsBusinessEntities.AnswerOrderInfo> Answer
           {
               get
               {
                   return this._answerDetail;
                   
               }
               set
               {
                   this._answerDetail = new List<AnswerOrderInfo>();
                   foreach (AnswerOrderInfo answerDet in value)
                       this._answerDetail.Add(answerDet);
               }


           }


        #endregion

        private int _questionId;
        private string _description;    
        private int _questionTypeId;
        private string _questionType;
        private int _questionOrder;
        private List<PLMClientsBusinessEntities.AnswerOrderInfo> _answerDetail = new List<AnswerOrderInfo>();
    }
}
