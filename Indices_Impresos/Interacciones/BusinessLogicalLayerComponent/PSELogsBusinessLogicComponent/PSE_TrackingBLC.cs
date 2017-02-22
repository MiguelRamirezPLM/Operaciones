using System;
using System.Collections.Generic;
using System.Text;
using PSELogsBusinessEntities;

namespace PSELogsBusinessLogicComponent
{
    public class PSE_TrackingBLC
    {
        #region Constructors

        private PSE_TrackingBLC() { }

        #endregion

        #region Public Methods

        public List<PSE_TrackingListInfo> replicateLogs(List<PSE_TrackingListInfo> logs)
        {
            foreach (PSE_TrackingListInfo log in logs)
                this.replicateLogs(log);

            return this.cleanList(logs);
        }

        public PSE_TrackingListInfo replicateLogs(PSE_TrackingListInfo log)
        {
            PSE_TrackingInfo record = this.convertLog(log);

            PSELogsDataAccessComponent.PSE_TrackingDALC.Instance.insert(record, log.Attributes);

            // If the method execution is here then it means the insert operation was OK:
            log.Replicate = true;
            log.TrackId = record.TrackId;

            return log;
        }

        public PSELogsBusinessEntities.PSE_TrackingParentInfo replicateParentLogs(PSELogsBusinessEntities.PSE_TrackingParentInfo parentLog)
        {
            // Insert parent:
            PSE_TrackingListInfo trackParentInfo = this.replicateLogs(parentLog);

            // Iterate over children:
            foreach(PSE_TrackingListInfo log in parentLog.ChildrenTrackingInfo)
            {
                PSE_TrackingInfo record = this.convertLog(log);

                PSELogsDataAccessComponent.PSE_TrackingDALC.Instance.insert(record, trackParentInfo.TrackId);

                // If the method execution is here then it means the insert operation was OK:
                log.Replicate = true;
                log.TrackId = record.TrackId;
            }

            // Return original object with Replicate attribute updated:
            return parentLog;
        }

        public List<PSELogsBusinessEntities.PSE_TrackingParentInfo> replicateParentLogs(List<PSELogsBusinessEntities.PSE_TrackingParentInfo> parentTrackList)
        {
            foreach (PSELogsBusinessEntities.PSE_TrackingParentInfo parentTrack in parentTrackList)
                this.replicateParentLogs(parentTrack);

            return parentTrackList;
        }   

        public void addLog(string codeString, int editionId, byte sourceId, byte searchTypeId, byte entityId, string searchParameters)
        {
            PSELogsBusinessEntities.PSE_TrackingInfo log = new PSELogsBusinessEntities.PSE_TrackingInfo();

            log.CodeString = codeString;
            log.EditionId = editionId;
            log.SourceId = sourceId;
            log.SearchTypeId = searchTypeId;
            log.EntityId = entityId;
            log.SearchParameters = searchParameters;
            
            log.SearchDate = null;

            PSELogsDataAccessComponent.PSE_TrackingDALC.Instance.insert(log);

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
            log.SearchDate = null;

            log.TrackId = PSELogsDataAccessComponent.PSE_TrackingDALC.Instance.insert(log, attributes);
        }

        #endregion

        #region Private Methods

        private PSE_TrackingInfo convertLog(PSE_TrackingListInfo current)
        {
            PSE_TrackingInfo record = new PSE_TrackingInfo();

            record.CodeString = current.CodeString;

            if(current.SearchDate != null)
                record.SearchDate = current.SearchDate;

            record.EditionId = current.EditionId;
            record.SourceId = current.SourceId;
            record.SearchTypeId = current.SearchTypeId;
            record.EntityId = current.EntityId;
            record.SearchParameters = current.SearchParameters;
            record.ClientTrackId = current.TrackId;
            record.JsonFormat = current.JsonFormat;

            return record;

        }

        private string getAttributes(List<PSE_TrackingAttributeInfo> attributes)
        {
            string attr = "";

            if(attributes != null)
                for (int x = 0; x < attributes.Count; x++)
                {
                    if (attr == "")
                        attr = attributes[x].AttributeId.ToString();
                    else
                        attr = attr + "," + attributes[x].AttributeId.ToString();
                }

                        
            return attr;
        }

        private List<PSE_TrackingListInfo> cleanList(List<PSE_TrackingListInfo> logs)
        {
            foreach (PSE_TrackingListInfo log in logs)
            {
                if (log.Replicate == false)
                    logs.Remove(log);
            }

            return logs;
        }

        #endregion

        public static readonly PSE_TrackingBLC Instance = new PSE_TrackingBLC();

    }
}
