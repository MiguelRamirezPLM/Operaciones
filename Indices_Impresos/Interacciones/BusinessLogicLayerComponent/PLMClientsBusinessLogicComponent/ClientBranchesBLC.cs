using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public sealed class ClientBranchesBLC
    {
        #region Constructors

        private ClientBranchesBLC() { }

        #endregion

        #region Public Methods

        //public List<ProjectActivitiesInfo> getProjectActivities()
        //{
        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getProjects();
        //}

        public int insert(PLMClientsBusinessEntities.CompanyClientBranchesInfo BEntity)
        {
            return PLMClientsDataAccessComponent.ClientBranchesDALC.Instance.insert(BEntity);
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

        public static readonly ClientBranchesBLC Instance = new ClientBranchesBLC();
    }
}
