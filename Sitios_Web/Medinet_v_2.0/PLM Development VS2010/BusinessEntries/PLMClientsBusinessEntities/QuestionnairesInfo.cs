using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Questionnaire Information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class QuestionnairesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionnairesInfo class. Not receive parameters.
        /// </summary>
        public QuestionnairesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionnaireId.
        ///     </para>
        ///     <para>
        ///         Questionnaire identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int QuestionnaireId
        {
            get
            {
                return this._questionnaireId;
            }
            set 
            {
                this._questionnaireId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionnaireName.
        ///     </para>
        ///     <para>
        ///         Questionnaire name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QuestionnaireName
        {
            get
            {
                return this._questionnaireName;
            }
            set
            {
                this._questionnaireName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionnaireDescription.
        ///     </para>
        ///     <para>
        ///         Questionnaire description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QuestionnaireDescription
        {
            get
            {
                return this._questionnaireDescription;
            }
            set
            {
                this._questionnaireDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionnaireInstructions.
        ///     </para>
        ///     <para>
        ///         Questionnaire instructions.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QuestionnaireInstructions
        {
            get
            {
                return this._questionnaireInstructions;
            }
            set
            {
                this._questionnaireInstructions = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for QuestionnaireReferences.
        ///     </para>
        ///     <para>
        ///         Questionnaire references.
        ///     </para>
        /// </summary>
        [DataMember]
        public string QuestionnaireReferences
        {
            get
            {
                return this._questionnaireReferences;
            }
            set
            {
                this._questionnaireReferences = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WebPage.
        ///     </para>
        ///     <para>
        ///         Web Page where is the Questionnaire.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WebPage
        {
            get
            {
                return this._webPage;
            }
            set
            {
                this._webPage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Questionnaire is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _questionnaireId;
        private string _questionnaireName;
        private string _questionnaireDescription;
        private string _questionnaireInstructions;
        private string _questionnaireReferences;
        private string _webPage;
        private bool _active;
    }
}
