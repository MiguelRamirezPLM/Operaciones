using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
     /// <summary>
    ///     Class which represents the Questions Section information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]

   public class QuestionSectionTypesInfo
    {
        #region Constructor

        
        /// <summary>
        ///     Initializes a new instance of the QuestionSectionTypesInfo class. Not receive parameters.
        /// </summary>
        public QuestionSectionTypesInfo (){}


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
            ///         Question Type identifier.
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
            ///         Property which gets or sets a value for QuestionOrder.
            ///     </para>
            ///     <para>
            ///         Indicates the order of question.
            ///     </para>
            /// </summary>
            
            public int QuestionOrder
            {
                get {return this._questionOrder;}
                set {this._questionOrder= value;}
            }




        #endregion  

        private int _questionId;
        private int _questionTypeId;
        private int _questionOrder;
    }
}
