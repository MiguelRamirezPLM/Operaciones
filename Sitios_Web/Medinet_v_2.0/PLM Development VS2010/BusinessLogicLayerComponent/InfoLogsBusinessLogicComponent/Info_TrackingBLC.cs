using System;
using System.Collections.Generic;
using System.Text;
using InfoLogsBusinessEntries;

namespace InfoLogsBusinessLogicComponent
{
    public class Info_TrackingBLC
    {

        #region Constructors

        private Info_TrackingBLC() { }

        #endregion

        #region Public Methods

        public InfoLogsBusinessEntries.Info_TrackingListInfo addTracking(InfoLogsBusinessEntries.Info_TrackingListInfo log)
        {
            InfoLogsBusinessEntries.Info_TrackingInfo record = this.convertLog(log);
            InfoLogsDataAccessComponent.Info_TrackingDALC.Instance.insert(record);
            
            log.Replicate = true;
            return log;
        }

        public List<InfoLogsBusinessEntries.Info_TrackingListInfo> addTracking(List<InfoLogsBusinessEntries.Info_TrackingListInfo> logs)
        {
            foreach (Info_TrackingListInfo log in logs)
            {
                InfoLogsBusinessEntries.Info_TrackingInfo record = this.convertLog(log);
                InfoLogsDataAccessComponent.Info_TrackingDALC.Instance.insert(record);
                
                log.Replicate = true;
            }
            return this.cleanList(logs);
        }

        #endregion

        #region Private Methods

        private InfoLogsBusinessEntries.Info_TrackingInfo convertLog(InfoLogsBusinessEntries.Info_TrackingListInfo current)
        {
            Info_TrackingInfo record = new Info_TrackingInfo();

            if (current.ParentId != null)
                record.ParentId = current.ParentId;
            
            record.CodeString = current.CodeString;
            record.SearchDate = current.SearchDate;
            record.SourceId = current.SourceId;
            record.SearchTypeId = current.SearchTypeId;
            record.EntityId = current.EntityId;
            record.SearchParameters = current.SearchParameters;
            record.JSONFormat = current.JSONFormat;
            
            return record;

        }

        private List<InfoLogsBusinessEntries.Info_TrackingListInfo> cleanList(List<InfoLogsBusinessEntries.Info_TrackingListInfo> logs)
        {
            foreach (Info_TrackingListInfo log in logs)
            {
                if (log.Replicate == false)
                    logs.Remove(log);
            }
            return logs;
        }

        #endregion

        public static readonly Info_TrackingBLC Instance = new Info_TrackingBLC();

    }
}
