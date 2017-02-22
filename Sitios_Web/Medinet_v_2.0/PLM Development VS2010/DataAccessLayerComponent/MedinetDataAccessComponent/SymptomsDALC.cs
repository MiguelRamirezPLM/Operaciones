using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class SymptomsDALC : MedinetDataAccessAdapter<SymptomInfo>
    {
        #region Constructors

        private SymptomsDALC() { }

        #endregion

        #region Public methods

        public override SymptomInfo getOne(int pk)
        {
            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptoms");
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symtomid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            SymptomInfo record = null;

            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {                
                while (dataReader.Read())
                {
                    record = new SymptomInfo();
                    record.Active =Convert.ToBoolean(dataReader["Active"]);
                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);
                    record.SymptomName = dataReader["SymptomName"].ToString();                    
                }
            }

            return record;
        }
    

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getByEdition(int editionId, string quest)
        {
            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptomsByEditionByText");

            // Add the parameters:
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            if (!string.IsNullOrEmpty(quest))
                SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@quest", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, quest);

            List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.SymptomByEditionInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.SymptomByEditionInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                    {record.ParentId = (int)dataReader["ParentId"];}
                    else
                    {record.ParentId = null;}

                    record.SymptomName = dataReader["SymptomName"].ToString();

                    if (dataReader["SymptomColorIn"] != DBNull.Value)
                    {record.SymptomColorIn = dataReader["SymptomColorIn"].ToString();}
                    else
                    {record.SymptomColorIn = null;}

                    if (dataReader["SymptomColorOut"] != DBNull.Value)
                    {record.SymptomColorOut = dataReader["SymptomColorOut"].ToString();}
                    else
                    {record.SymptomColorOut = null;}

                    if (dataReader["HeaderImage"] != DBNull.Value)
                    { record.HeaderImage = dataReader["HeaderImage"].ToString(); }
                    else
                    { record.HeaderImage = null; }

                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getSymptomsDetailByEdition(int editionId)
        {
            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptomsByEdition");

            //Add the parameters:
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.SymptomByEditionInfo record;

                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.SymptomByEditionInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                    { record.ParentId = Convert.ToInt32(dataReader["ParentId"]); }
                    else
                    { record.ParentId = null; }

                    record.SymptomName = dataReader["SymptomName"].ToString();

                    if (dataReader["SymptomColorIn"] != DBNull.Value)
                    { record.SymptomColorIn = dataReader["SymptomColorIn"].ToString(); }
                    else
                    { record.SymptomColorIn = null; }

                    if (dataReader["SymptomColorOut"] != DBNull.Value)
                    { record.SymptomColorOut = dataReader["SymptomColorOut"].ToString(); }
                    else
                    { record.SymptomColorOut = null; }

                    if (dataReader["HeaderImage"] != DBNull.Value)
                    { record.HeaderImage = dataReader["HeaderImage"].ToString(); }
                    else
                    { record.HeaderImage = null; }

                    if (record.ParentId == null)
                    {
                        BECollection.Add(record);
                    }
                    else
                    {
                        this.getParentFromList(BECollection, record);
                    }
                }
            }
            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getSymptomsByEditionByParent(int editionId, int parentId)
        {
            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptomsByEditionByParent");

            //Add the parameters:
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.SymptomByEditionInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.SymptomByEditionInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                    { record.ParentId = Convert.ToInt32(dataReader["ParentId"]); }
                    else
                    { record.ParentId = null; }

                    record.SymptomName = dataReader["SymptomName"].ToString();

                    if (dataReader["SymptomColorIn"] != DBNull.Value)
                    { record.SymptomColorIn = dataReader["SymptomColorIn"].ToString(); }
                    else
                    { record.SymptomColorIn = null; }

                    if (dataReader["SymptomColorOut"] != DBNull.Value)
                    { record.SymptomColorOut = dataReader["SymptomColorOut"].ToString(); }
                    else
                    { record.SymptomColorOut = null; }

                    if (dataReader["HeaderImage"] != DBNull.Value)
                    { record.HeaderImage = dataReader["HeaderImage"].ToString(); }
                    else
                    { record.HeaderImage = null; }

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<MedinetBusinessEntries.SymptomInfo> getSymptomsCatalog(string searchName) {
            
            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptomsCatalog");

            //Add the parameters:
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchName);

            List<MedinetBusinessEntries.SymptomInfo> BECollection = new List<MedinetBusinessEntries.SymptomInfo>();
            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.SymptomInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.SymptomInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);
                    record.SymptomName = dataReader["SymptomName"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<MedinetBusinessEntries.SymptomInfo> getSymptomsByProduct(int productId,int pharmaFormId)
        {

            DbCommand dbCmd = SymptomsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductSymptoms");

            //Add the parameters:
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SymptomsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<MedinetBusinessEntries.SymptomInfo> BECollection = new List<MedinetBusinessEntries.SymptomInfo>();
            using (IDataReader dataReader = SymptomsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.SymptomInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.SymptomInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);
                    record.SymptomName = dataReader["SymptomName"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

       
        #endregion

        #region Private methods

        private void getParentFromList(List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> current, PharmaSearchEngineBusinessEntries.SymptomByEditionInfo record)
        {
            foreach (PharmaSearchEngineBusinessEntries.SymptomByEditionInfo symptom in current)
            {
                if (symptom.SymptomId == record.ParentId)
                {
                    symptom.SymptomChildrens.Add(record);
                }
                else
                {
                    if (symptom.SymptomChildrens.Count() > 0)
                    {
                        this.getParentFromList(symptom.SymptomChildrens, record);
                    }
                }
            }
        }

        #endregion

        public static readonly SymptomsDALC Instance = new SymptomsDALC();
    }
}
