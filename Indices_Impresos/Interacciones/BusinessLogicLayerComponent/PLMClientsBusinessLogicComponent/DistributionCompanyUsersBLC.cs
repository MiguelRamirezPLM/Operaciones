using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class DistributionCompanyUsersBLC
    {

        #region Constructors

        private DistributionCompanyUsersBLC() { }

        #endregion

        #region Public Methods

        public void addDistributionCompanyUser(DistributionCompanyUsersInfo BEntity)
        {
            PLMClientsDataAccessComponent.DistributionCompanyUsersDALC.Instance.insert(BEntity);
        }

        #endregion

        public static readonly DistributionCompanyUsersBLC Instance = new DistributionCompanyUsersBLC();

    }
}
