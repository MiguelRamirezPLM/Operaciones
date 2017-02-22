using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class ProjectActivitiesBLC
    {
        #region Constructors

        private ProjectActivitiesBLC() { }

        #endregion

        #region Public Methods

        public List<ProjectActivitiesInfo> getProjectActivities()
        {
            return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getProjectActivities();
        }

        public List<ProjectActivitiesNameInfo> getProjectActivitiesByUserByBinnacle(int userId, int binnacleId)
        {
            return PLMUsersDataAccessComponent.ProjectActivitiesNameDALC.Instance.getProjectActivitiesByUserByBinnacle(userId, binnacleId);
        }

        public List<ProjectActivitiesTimeInfo> getProjectActivitiesTimeByUserByBinnacle(int userId, int binnacleId)
        {
            return PLMUsersDataAccessComponent.ProjectActivitiesTimeDALC.Instance.getProjectActivitiesTimeByUserByBinnacle(userId, binnacleId);
        }

        public int insert(ProjectActivitiesInfo businessEntity)
        {
           return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.insert(businessEntity);
        }

        public void update(ProjectActivitiesInfo businessEntity)
        {
            PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.update(businessEntity);
        }

        public void delete(int activityId)
        {
            PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.delete(activityId); 
        }

        public ProjectActivitiesInfo getProjectActivity(int activityId)
        {
            if (activityId <= 0)
                throw new ArgumentException("The activity does not exist.");

            return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getOne(activityId);
        }

        #endregion

        public static readonly ProjectActivitiesBLC Instance = new ProjectActivitiesBLC();
    }
}
