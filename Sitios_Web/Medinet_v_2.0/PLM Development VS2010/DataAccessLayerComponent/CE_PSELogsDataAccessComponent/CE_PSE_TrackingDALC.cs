using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CE_PSELogsDataAccessComponent
{
    public class CE_PSE_TrackingDALC : CE_PSELogsDataAccessComponent<PSELogsBusinessEntities.PSE_TrackingInfo>
    {
        #region Constructors

        private CE_PSE_TrackingDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PSELogsBusinessEntities.PSE_TrackingInfo businessEntity)
        {
            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CreateConnection();
            StringBuilder sb = new StringBuilder();
            int result = 0;

            sb.Append("\nINSERT INTO [PSE_Tracking] ([CodeString],[SearchDate],[EditionId],[SourceId],[SearchTypeId],[EntityId],[SearchParameters])");
            sb.Append("\nVALUES('" + businessEntity.CodeString + "',GETDATE()," + businessEntity.EditionId + "," + businessEntity.SourceId);
            sb.Append("," + businessEntity.SearchTypeId + "," + businessEntity.EntityId + ",'" + businessEntity.SearchParameters + "')");

            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString(), out result);

            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CloseSharedConnection();

            return result;
        }

        public List<PSELogsBusinessEntities.PSE_TrackingListInfo> getLogs(int items, int lastParamLogs)
        {
            List<PSELogsBusinessEntities.PSE_TrackingInfo> notInLogs = this.getLogs(PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, lastParamLogs);

            StringBuilder sbNotInLogs = new StringBuilder();

            foreach (PSELogsBusinessEntities.PSE_TrackingInfo trackInfo in notInLogs)
                if (sbNotInLogs.Length == 0)
                    sbNotInLogs.Append(trackInfo.TrackId.ToString());

                else
                    sbNotInLogs.Append("," + trackInfo.TrackId.ToString());

            // Get logs to replicate:
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT TOP(" + items.ToString() + ") [TrackId],[CodeString],[SearchDate],[EditionId],[SourceId],[SearchTypeId],[EntityId],[SearchParameters] ");
            sb.Append("\n FROM PSE_Tracking ");

            if (sbNotInLogs.Length > 0)
                sb.Append("\n WHERE TrackId NOT IN (" + sbNotInLogs.ToString() + ")");

            sb.Append("\n ORDER BY TrackId");

            List<PSELogsBusinessEntities.PSE_TrackingListInfo> BECollection = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();

            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PSE_TrackingDALC.PSELogsInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                PSELogsBusinessEntities.PSE_TrackingListInfo record;

                while (dataReader.Read())
                {
                    record = new PSELogsBusinessEntities.PSE_TrackingListInfo();

                    record.TrackId = Convert.ToInt32(dataReader["TrackId"]);
                    record.CodeString = dataReader["CodeString"].ToString();
                    record.SearchDate = Convert.ToDateTime(dataReader["SearchDate"]);
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.SourceId = Convert.ToByte(dataReader["SourceId"]);
                    record.SearchTypeId = Convert.ToByte(dataReader["SearchTypeId"]);
                    record.EntityId = Convert.ToInt32(dataReader["EntityId"]);
                    record.SearchParameters = dataReader["SearchParameters"].ToString();

                    BECollection.Add(record);

                }
                    
            }

            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override void delete(int pk)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [PSE_Tracking] WHERE TrackId = " + pk);

            try
            {
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CreateConnection();
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString());
            }
            catch
            {
                
            }
            finally
            {
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CloseSharedConnection();
            }
        }

        public void delete(string trackIds)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [PSE_Tracking] WHERE TrackId IN (" + trackIds + ")");

            try
            {
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CreateConnection();
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.ExecuteNonQuerySql(sb.ToString());
            }
            catch
            {

            }
            finally
            {
                CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CloseSharedConnection();
            }

        }

        public List<PSELogsBusinessEntities.PSE_TrackingInfo> getLogs(PSELogsBusinessEntities.Catalogs.SearchTypes searchType, int items)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT TOP(" + items.ToString() + ") t.[TrackId],t.[CodeString],t.[SearchDate],t.[EditionId],t.[SourceId],t.[SearchTypeId],t.[EntityId],t.[SearchParameters] ");
            sb.Append("\nFROM PSE_Tracking t");
            sb.Append("\nINNER JOIN PSE_Entities e ON (t.EntityId = e.EntityId)");
            sb.Append("\nINNER JOIN PSE_SearchTypes st ON (t.SearchTypeId = st.SearchTypeId)");
            sb.Append("\nWHERE st.SearchTypeId = " + ((byte)searchType).ToString());

            if (searchType == PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada)
                sb.Append("\nAND e.EntityId IN (1,2,3,4,5)");

            sb.Append("\nORDER BY SearchDate DESC");

            List<PSELogsBusinessEntities.PSE_TrackingInfo> BECollection = new List<PSELogsBusinessEntities.PSE_TrackingInfo>();

            CE_PSE_TrackingDALC.PSELogsInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PSE_TrackingDALC.PSELogsInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        
        #endregion

        #region Protected Methods

        protected override PSELogsBusinessEntities.PSE_TrackingInfo getFromDataReader(IDataReader current)
        {
            PSELogsBusinessEntities.PSE_TrackingInfo record = new PSELogsBusinessEntities.PSE_TrackingInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);
            record.CodeString = current["CodeString"].ToString();
            record.SearchDate = Convert.ToDateTime(current["SearchDate"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.SourceId = Convert.ToByte(current["SourceId"]);
            record.SearchTypeId = Convert.ToByte(current["SearchTypeId"]);
            record.EntityId = Convert.ToInt32(current["EntityId"]);
            record.SearchParameters = current["SearchParameters"].ToString();

            return record;
        }
        
        #endregion

        public static readonly CE_PSE_TrackingDALC Instance = new CE_PSE_TrackingDALC();

    }
}
