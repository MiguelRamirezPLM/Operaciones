using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ProfessionalPracticesDALC : PLMClientsDataAccessAdapter<ProfessionalPracticeInfo>
    {
        #region Constructors

        private ProfessionalPracticesDALC() { }

        #endregion

        #region Public Methods

        public override List<ProfessionalPracticeInfo> getAll()
        {
            List<ProfessionalPracticeInfo> BECollection = new List<ProfessionalPracticeInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProfessionalPracticesDALC.InstanceDatabase.ExecuteReader(
                ProfessionalPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProfessionalPractices")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override ProfessionalPracticeInfo getFromDataReader(IDataReader current)
        {
            ProfessionalPracticeInfo record = new ProfessionalPracticeInfo();

            record.PracticeId = Convert.ToByte(current["PracticeId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ProfessionalPracticesDALC Instance = new ProfessionalPracticesDALC();

    }
}
