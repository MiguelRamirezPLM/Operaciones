using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Questions information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]

  public  class QuestionsInfo
    {
        #region Constructor
            /// <summary>
            ///     Initializes a new instance of the QuestionsInfo class. Not receive parameters.
            /// </summary>

            public QuestionsInfo() { }

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
            ///         Property which gets or sets a value for Active.
            ///     </para>
            ///     <para>
            ///         Indicates if the Question is enabled or disabled.
            ///     </para>
            /// </summary>
            [DataMember]
            public bool Active
            {
                get { return this._active; }
                set { this._active = value; }
            }



        #endregion


          private int _questionId;
          private string _description;
          private bool _active;




    }
}
