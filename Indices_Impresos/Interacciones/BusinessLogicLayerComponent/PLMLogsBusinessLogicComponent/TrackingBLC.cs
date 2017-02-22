using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMLogsBusinessEntries;

namespace PLMLogsBusinessLogicComponent
{
    public class TrackingBLC
    {

        #region Constructors

        private TrackingBLC() { }

        #endregion

        #region Public Methods

        public TrackingListInfo replicateLogs(TrackingListInfo log)
        {
            TrackingInfo record = this.convertLog(log);

            PLMLogsDataAccessComponent.TrackingDALC.Instance.insert(record, log.AttributeGroupKey);

            // If the method execution is here then it means the insert operation was OK:
            log.Replicate = true;
            log.TrackId = record.TrackId;

            return log;
        }

        public List<TrackingListInfo> replicateLogs(List<TrackingListInfo> logs)
        {
            foreach (TrackingListInfo log in logs)
                this.replicateLogs(log);

            return this.cleanList(logs);
        }

        public TrackingParentInfo replicateParentLogs(TrackingParentInfo parentLog)
        {
            // Insert parent:
            TrackingListInfo trackParentInfo = this.replicateLogs(parentLog);

            // Iterate over children:
            foreach (TrackingListInfo log in parentLog.ChildrenTrackingInfo)
            {
                TrackingInfo record = this.convertLog(log);

                PLMLogsDataAccessComponent.TrackingDALC.Instance.insert(record, trackParentInfo.TrackId);

                // If the method execution is here then it means the insert operation was OK:
                log.Replicate = true;
                log.TrackId = record.TrackId;
            }

            // Return original object with Replicate attribute updated:
            return parentLog;
        }

        public List<TrackingParentInfo> replicateParentLogs(List<TrackingParentInfo> parentTrackList)
        {
            foreach (TrackingParentInfo parentTrack in parentTrackList)
                this.replicateParentLogs(parentTrack);

            return parentTrackList;
        }

        #endregion

        #region Private Methods

        private TrackingInfo convertLog(TrackingListInfo current)
        {
            TrackingInfo record = new TrackingInfo();

            record.CodeString = current.CodeString;

            if (current.SearchDate != null)
                record.SearchDate = current.SearchDate;

            PLMLogsBusinessEntries.EditionInfo editionInfo =
                PLMLogsBusinessLogicComponent.EditionBLC.Instance.getByISBN(current.ISBN);

            if(editionInfo != null)
                record.EditionId = editionInfo.EditionId;
            else
                throw new ArgumentException("The ISBN is not valid.");
            
            record.SourceId = current.SourceId;
            record.SearchTypeId = current.SearchTypeId;
            record.EntityId = current.EntityId;
            record.SearchParameters = current.SearchParameters;
            record.ClientTrackId = current.TrackId;
            record.JsonFormat = current.JsonFormat;

            return record;
        }

        private List<TrackingListInfo> cleanList(List<TrackingListInfo> logs)
        {
            foreach (TrackingListInfo log in logs)
            {
                if (log.Replicate == false)
                    logs.Remove(log);
            }

            return logs;
        }

        #endregion

        public static readonly TrackingBLC Instance = new TrackingBLC();

    }
}
