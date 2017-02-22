using System;
using System.Collections.Generic;
using System.Text;

namespace PLMUsersBusinessLogicComponent
{
    public class ActivityLogsBLC
    {
        #region Constructors

        private ActivityLogsBLC() { }

        #endregion

        #region Public Methods

        public void addActivity(PLMUserBusinessEntries.ActivityLogInfo BEntity)
        {
            PLMUsersDataAccessComponent.ActivityLogsDALC.Instance.insert(BEntity);
        }

        #endregion


        public static readonly ActivityLogsBLC Instance = new ActivityLogsBLC();

    }
}
