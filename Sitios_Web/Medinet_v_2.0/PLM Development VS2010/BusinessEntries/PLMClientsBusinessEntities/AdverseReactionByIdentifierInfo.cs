using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the  Adverse Reaction By Identifier  information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    
     public class AdverseReactionByIdentifierInfo
    {

          #region Constructor

         /// <summary>
        ///     Initializes a new instance of the AdverseReactionByIdentifier class. Not receive parameters.
        /// </summary>
         public AdverseReactionByIdentifierInfo() { }

        #endregion 


         #region Properties

         /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for AdverseReactionId.
         ///     </para>
         ///     <para>
         ///         Adverse Reaction identifier.
         ///     </para>
         /// </summary>
         [DataMember]
         public int AdverseReactionId
         {
             get { return this._adverseReactionId; }
             set { this._adverseReactionId = value; }
         }

         /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for StartReaction.
         ///     </para>
         ///     <para>
         ///         Start Reaction date.
         ///     </para>
         /// </summary>
         [DataMember]
         public DateTime StartReaction
         {
             get { return this._startReaction; }
             set { this._startReaction = value; }
         }

         /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for BookName.
         ///     </para>
         ///     <para>
         ///         Book name.
         ///     </para>
         /// </summary>
         [DataMember]
         public string DescriptionReaction
         {
             get { return this._descriptionReaction; }
             set { this._descriptionReaction = value; }
         }

         /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for ConsequenceId.
         ///     </para>
         ///     <para>
         ///         Consequence identifier.
         ///     </para>
         /// </summary>
         [DataMember]
         public byte ConsequenceId
         {
             get { return this._consequenceId; }
             set { this._consequenceId = value; }
         }

         /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for Active.
         ///     </para>
         ///     <para>
         ///         Indicates if the Adverse Reaction is enabled or disabled.
         ///     </para>
         /// </summary>
         [DataMember]
         public bool Active
         {
             get { return this._active; }
             set { this._active = value; }
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
     
          /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for Answer.
         ///     </para>
         ///     <para>
         ///         Answer name.
         ///     </para>
         /// </summary>
         [DataMember]
         public string Answer
         {
             get { return this._answer; }
             set { this._answer = value; }
         }
		 
		 
		 /// <summary>
         ///     <para>
         ///         Property which gets or sets a value for Consequence.
         ///     </para>
         ///     <para>
         ///         Consequence name.
         ///     </para>
         /// </summary>
         [DataMember]
         public string Consequence
         {
             get { return this._consequence; }
             set { this._consequence = value; }
         }

         #endregion

         private int _adverseReactionId;
         private DateTime _startReaction;
         private string _descriptionReaction;
         private byte _consequenceId;
         private bool _active;

         private int _questionId;
         private int _questionTypeId;
         private int _answerId;
         private string _answerDescription;

         private string _answer;
         private string _consequence;
    }
}
