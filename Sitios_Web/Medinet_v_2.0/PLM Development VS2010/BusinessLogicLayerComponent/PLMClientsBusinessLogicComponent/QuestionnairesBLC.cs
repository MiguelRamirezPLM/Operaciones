using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class QuestionnairesBLC
    {

        #region Constructors

        private QuestionnairesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesByPrefix(string prefix)
        {
            if (prefix == null || prefix.Trim() == "")
                throw new ArgumentException("The prefix does not exist.");
            else
                return PLMClientsDataAccessComponent.QuestionnairesDALC.Instance.getQuestionnairesByPrefix(prefix);
        }

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesBySpeciality(PLMClientsBusinessEntities.Catalogs.Specialities speciality)
        {
            return PLMClientsDataAccessComponent.QuestionnairesDALC.Instance.getQuestionnairesBySpeciality(Convert.ToInt32(speciality));
        }

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesByPrefixBySpeciality(string prefix, PLMClientsBusinessEntities.Catalogs.Specialities speciality)
        {
            if (prefix == null || prefix.Trim() == "")
                throw new ArgumentException("The Prefix does not exist.");
            else
                return PLMClientsDataAccessComponent.QuestionnairesDALC.Instance.getQuestionnairesByPrefixBySpeciality(prefix, Convert.ToInt32(speciality));
        }

        public PLMClientsBusinessEntities.QuestionnaireDetailInfo getQuestionnaireDetail(int questionnaireId)
        {
            if (questionnaireId <= 0)
                throw new ArgumentException("The Questionnaire does not exist.");
            else
                return PLMClientsDataAccessComponent.QuestionnairesDALC.Instance.getQuestionnaireDetail(questionnaireId);
        }

        public List<PLMClientsBusinessEntities.SummariesInfo> getSummariesByOption(int questionnaireId, int optionId, byte order)
        {
            return PLMClientsDataAccessComponent.QuestionnairesDALC.Instance.getSummariesByOption(questionnaireId, optionId, order);
        }

        #endregion

        public static readonly QuestionnairesBLC Instance = new QuestionnairesBLC();

    }
}
