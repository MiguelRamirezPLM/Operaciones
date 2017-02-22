using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PSELogsBusinessLogicComponent
{
    public class CE_PSE_TrackingBLC
    {
        #region Constructors

        private CE_PSE_TrackingBLC() { }

        #endregion

        #region Public Methods

        public void addLog(string codeString, int editionId, byte sourceId, byte searchTypeId, byte entityId, string searchParameters)
        {
            PSELogsBusinessEntities.PSE_TrackingInfo log = new PSELogsBusinessEntities.PSE_TrackingInfo();

            log.CodeString = codeString;
            log.EditionId = editionId;
            log.SourceId = sourceId;
            log.SearchTypeId = searchTypeId;
            log.EntityId = entityId;
            log.SearchParameters = searchParameters;

            CE_PSELogsDataAccessComponent.CE_PSE_TrackingDALC.Instance.insert(log);

        }

        public void addAttributeLog(string codeString, int editionId, byte sourceId, byte searchTypeId, byte entityId, string searchParameters, string attributes)
        {
            PSELogsBusinessEntities.PSE_TrackingInfo log = new PSELogsBusinessEntities.PSE_TrackingInfo();

            log.CodeString = codeString;
            log.EditionId = editionId;
            log.SourceId = sourceId;
            log.SearchTypeId = searchTypeId;
            log.EntityId = entityId;
            log.SearchParameters = searchParameters;

            log.TrackId = CE_PSELogsDataAccessComponent.CE_PSE_TrackingDALC.Instance.insert(log);

            string[] atr = attributes.Split(',');

            for (int x = 0; x < atr.Length; x++)
            {
                PSELogsBusinessEntities.PSE_TrackingAttributeInfo trackAttr = new PSELogsBusinessEntities.PSE_TrackingAttributeInfo();

                trackAttr.TrackId = log.TrackId;
                trackAttr.AttributeId = Convert.ToInt32(atr[x].Trim());

                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingAttributesBLC.Instance.addAttribute(trackAttr);

            }

        }

        public List<PSELogsBusinessEntities.PSE_TrackingListInfo> getLogs(int items, int lastParamLogs)
        {
            List<PSELogsBusinessEntities.PSE_TrackingListInfo> logs = CE_PSELogsDataAccessComponent.CE_PSE_TrackingDALC.Instance.getLogs(items, lastParamLogs);

            for (int x = 0; x < logs.Count; x++)
            {
                List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo> listAtr =
                    CE_PSE_TrackingAttributesBLC.Instance.getLogsAttributes(logs[x].TrackId);

                logs[x].Attributes = listAtr.Count > 0 ? this.getStringAttributes(listAtr) : string.Empty;
            }
            
            return logs;
        }

        public List<PSELogsBusinessEntities.PSE_TrackingInfo> getLogs(PSELogsBusinessEntities.Catalogs.SearchTypes searchType, int items)
        {
            return CE_PSELogsDataAccessComponent.CE_PSE_TrackingDALC.Instance.getLogs(searchType, items);
        }

        public void deleteLogs(List<PSELogsBusinessEntities.PSE_TrackingListInfo> logs)
        {
            StringBuilder sbTrackIds = new StringBuilder();

            foreach (PSELogsBusinessEntities.PSE_TrackingListInfo log in logs)
                if (log.Replicate)
                    sbTrackIds.Append(sbTrackIds.Length == 0 ? log.TrackId.ToString() : "," + log.TrackId.ToString());

            // Delete associated attributes:
            CE_PSE_TrackingAttributesBLC.Instance.deleteAttributeLogs(sbTrackIds.ToString());

            // Delete current tracks:
            CE_PSELogsDataAccessComponent.CE_PSE_TrackingDALC.Instance.delete(sbTrackIds.ToString());
        }
      
        #endregion

        #region private methods

        private string getStringAttributes(List<PSELogsBusinessEntities.PSE_TrackingAttributeInfo> listAtr)
        { 
            System.Text.StringBuilder sb = new StringBuilder(string.Empty);

            foreach (PSELogsBusinessEntities.PSE_TrackingAttributeInfo atrInfo in listAtr)
                if (string.IsNullOrEmpty(sb.ToString()))
                    sb.Append(atrInfo.AttributeId.ToString());
                else
                    sb.Append("," + atrInfo.AttributeId.ToString());

            return sb.ToString();
        }

        #endregion

        public static readonly CE_PSE_TrackingBLC Instance = new CE_PSE_TrackingBLC();
    }
}
