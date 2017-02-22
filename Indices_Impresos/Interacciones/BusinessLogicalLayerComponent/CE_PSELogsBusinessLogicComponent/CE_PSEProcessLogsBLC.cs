using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PSELogsBusinessLogicComponent
{
    public class CE_PSEProcessLogsBLC
    {
        #region Constructors

        private CE_PSEProcessLogsBLC() 
        {
            this._logService = new PLMLogsEngine.PLMLogsClient();
            this._logService.logsAddActivitiesCompleted += new EventHandler<PLMLogsEngine.logsAddActivitiesCompletedEventArgs>(logService_logsAddActivitiesCompleted);
        }

        #endregion

        #region Public methods

        public void getLogs(int items, int lastParamLogs)
        {
            List<PSELogsBusinessEntities.PSE_TrackingListInfo> logs = CE_PSE_TrackingBLC.Instance.getLogs(items, lastParamLogs);
            this._logService.logsAddActivitiesAsync(logs);
        }

        #endregion

        #region Callback methods

        void logService_logsAddActivitiesCompleted(object sender, PLMLogsEngine.logsAddActivitiesCompletedEventArgs e)
        {
            var result = e.Result;
            CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.deleteLogs(result);
        }

        #endregion

        private PLMLogsEngine.PLMLogsClient _logService;
        public static readonly CE_PSEProcessLogsBLC Instance = new CE_PSEProcessLogsBLC();
    }
}
