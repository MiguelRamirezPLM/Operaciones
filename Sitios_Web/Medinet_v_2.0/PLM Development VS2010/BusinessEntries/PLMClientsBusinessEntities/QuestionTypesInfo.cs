using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the QuestionsTypes information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]

   public class QuestionTypesInfo
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the QuestionTypesInfo class. Not receive parameters.
        /// </summary>

        public QuestionTypesInfo() { }


        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionTypeId.
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
        ///         Property which gets or sets a value for Description
        ///     </para>
        ///     <para>
        ///         Description of question type
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
        ///         Indicates if the Question Type is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        #endregion


        private int _questionTypeId;
        private string _description;
        private bool _active;



    }
}
