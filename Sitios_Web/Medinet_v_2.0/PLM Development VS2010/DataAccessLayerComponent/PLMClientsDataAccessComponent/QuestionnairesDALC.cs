using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class QuestionnairesDALC : PLMClientsDataAccessAdapter<QuestionnairesInfo>
    {

        #region Constructors

        private QuestionnairesDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesByPrefix(string prefix)
        {
            List<PLMClientsBusinessEntities.QuestionnairesInfo> BECollection = new List<PLMClientsBusinessEntities.QuestionnairesInfo>();

            DbCommand dbCmd = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetQuestionnaires");

            // Add the parameters:
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = QuestionnairesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesBySpeciality(int specialityId)
        {
            List<PLMClientsBusinessEntities.QuestionnairesInfo> BECollection = new List<PLMClientsBusinessEntities.QuestionnairesInfo>();

            DbCommand dbCmd = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetQuestionnaires");

            // Add the parameters:
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = QuestionnairesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.QuestionnairesInfo> getQuestionnairesByPrefixBySpeciality(string prefix, int specialityId)
        {
            List<PLMClientsBusinessEntities.QuestionnairesInfo> BECollection = new List<PLMClientsBusinessEntities.QuestionnairesInfo>();

            DbCommand dbCmd = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetQuestionnaires");

            // Add the parameters:
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = QuestionnairesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public PLMClientsBusinessEntities.QuestionnaireDetailInfo getQuestionnaireDetail(int questionnaireId)
        {
            List<PLMClientsBusinessEntities.QuestionnaireDetailInfo> BECollection = new List<QuestionnaireDetailInfo>();

            DbCommand dbCmd = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetQuestionnaires");

            // Add the parameters:
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@questionnaireId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, questionnaireId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = QuestionnairesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    QuestionnaireDetailInfo record = new QuestionnaireDetailInfo();

                    record.QuestionnaireId = Convert.ToInt32(dataReader["QuestionnaireId"]);
                    record.QuestionnaireName = dataReader["QuestionnaireName"].ToString();

                    if (dataReader["QuestionnaireDescription"] != System.DBNull.Value)
                        record.QuestionnaireDescription = dataReader["QuestionnaireDescription"].ToString();

                    if (dataReader["QuestionnaireInstructions"] != System.DBNull.Value)
                        record.QuestionnaireInstructions = dataReader["QuestionnaireInstructions"].ToString();

                    if (dataReader["QuestionnaireReferences"] != System.DBNull.Value)
                        record.QuestionnaireReferences = dataReader["QuestionnaireReferences"].ToString();

                    if (dataReader["WebPage"] != System.DBNull.Value)
                        record.WebPage = dataReader["WebPage"].ToString();

                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    DbCommand optionResult = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOptionsByQuestionnaire");

                    // Add the parameters:
                    QuestionnairesDALC.InstanceDatabase.AddParameter(optionResult, "@questionnaireId", DbType.Int32,
                        ParameterDirection.Input, string.Empty, DataRowVersion.Current, record.QuestionnaireId);

                    using (IDataReader dataOption = QuestionnairesDALC.InstanceDatabase.ExecuteReader(optionResult))
                    {
                        while (dataOption.Read())
                        {
                            PLMClientsBusinessEntities.OptionsInfo optionItem = new OptionsInfo();

                            optionItem.OptionId = Convert.ToInt32(dataOption["OptionId"]);
                            optionItem.OptionDescription = dataOption["OptionDescription"].ToString();
                            optionItem.Active = Convert.ToBoolean(dataOption["Active"]);
                            optionItem.Order = Convert.ToByte(dataOption["Order"]);

                            record.OptionList.Add(optionItem);
                        }
                    }
                    BECollection.Add(record);
                }
            }
            return BECollection.Count > 0 ? BECollection[0] : null;
        }

        public List<PLMClientsBusinessEntities.SummariesInfo> getSummariesByOption(int questionnaireId, int optionId, byte order)
        {
            List<PLMClientsBusinessEntities.SummariesInfo> BECollection = new List<SummariesInfo>();

            DbCommand dbCmd = QuestionnairesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSummariesByOption");

            // Add the parameters:
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@questionnaireId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, questionnaireId);
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@optionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, optionId);
            QuestionnairesDALC.InstanceDatabase.AddParameter(dbCmd, "@order", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, order);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = QuestionnairesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    SummariesInfo record = new SummariesInfo();

                    record.SummaryId = Convert.ToInt32(dataReader["SummaryId"]);
                    record.SummaryDescription = dataReader["SummaryDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection.Count > 0 ? BECollection : null;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.QuestionnairesInfo getFromDataReader(IDataReader current)
        {
            QuestionnairesInfo record = new QuestionnairesInfo();

            record.QuestionnaireId = Convert.ToInt32(current["QuestionnaireId"]);
            record.QuestionnaireName = current["QuestionnaireName"].ToString();

            if (current["QuestionnaireDescription"] != System.DBNull.Value)
                record.QuestionnaireDescription = current["QuestionnaireDescription"].ToString();

            if (current["QuestionnaireInstructions"] != System.DBNull.Value)
                record.QuestionnaireInstructions = current["QuestionnaireInstructions"].ToString();

            if (current["QuestionnaireReferences"] != System.DBNull.Value)
                record.QuestionnaireReferences = current["QuestionnaireReferences"].ToString();

            if (current["WebPage"] != System.DBNull.Value)
                record.WebPage = current["WebPage"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly QuestionnairesDALC Instance = new QuestionnairesDALC();

    }
}
