using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace PLMClientsBusinessLogicComponent
{
   public sealed class AddressClientBranchesBLC
    {
        #region Constructors

        private AddressClientBranchesBLC() { }

        #endregion

        #region Public Methods

        //public List<ProjectActivitiesInfo> getProjectActivities()
        //{
        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getProjects();
        //}

        public int insert(PLMClientsBusinessEntities.AddressInfo BEntity)
        {
            return PLMClientsDataAccessComponent.AddressClientBranchesDALC.Instance.insert(BEntity);
        }

        //public void update(AddressInfo businessEntity)
        //{
        //    PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.update(businessEntity);
        //}

        //public AddressInfo getProjectActivity(int activityId)
        //{
        //    if (activityId <= 0)
        //        throw new ArgumentException("The activity does not exist.");

        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getOne(activityId);
        //}

        #endregion

        public static readonly AddressClientBranchesBLC Instance = new AddressClientBranchesBLC();
    }
}
