using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Questionnaire detail.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class QuestionnaireDetailInfo : QuestionnairesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the QuestionnaireDetailInfo class. Not receive parameters.
        /// </summary>
        public QuestionnaireDetailInfo() 
        {
            this._optionList = new List<OptionsInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OptionList.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.OptionsInfo"/>. Indicates if the Questionnaire has got Options.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.OptionsInfo> OptionList
        {
            get
            {
                return this._optionList;
            }
            set
            {
                this._optionList = new List<PLMClientsBusinessEntities.OptionsInfo>();

                foreach (PLMClientsBusinessEntities.OptionsInfo option in value)
                    this._optionList.Add(option);
            }
        }

        #endregion

        private List<PLMClientsBusinessEntities.OptionsInfo> _optionList;
    }
}
