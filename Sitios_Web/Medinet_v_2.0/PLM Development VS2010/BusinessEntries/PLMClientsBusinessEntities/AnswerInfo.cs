using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Answers  information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>

      [DataContract]


   public class AnswerInfo
    {

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the AnsweresInfo class. Not receive parameters.
        /// </summary>

        public AnswerInfo() { }

        #endregion

        #region Properties


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
        public string Answer
        {
            get { return this._answer; }
            set { this._answer = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Answer is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }


        #endregion

        private int _answerId;
        private string _answer;
        private bool _active;



    }
}
