using System;
using System.Collections.Generic;
using System.Text;

namespace PLMUsersBusinessLogicComponent
{
    public class ActivitySessionsBLC
    {
        #region Constructors

        private ActivitySessionsBLC() { }

        #endregion

        #region Public Methods

        public void addActivity(PLMUserBusinessEntries.ActivitySessionInfo BEntity)
        {
            PLMUsersDataAccessComponent.ActivitySessionsDALC.Instance.insert(BEntity);
        }

        public int getCountry(int countryId)
        {
            switch (countryId)
            {
                case 1:
                    return Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Countries.México);
                    break;

                case 2:
                    return Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Countries.Centroamérica);
                    break;

                case 3:
                    return Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Countries.Ecuador);
                    break;
                case 4:
                    return Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Countries.Colombia);
                    break;
                default:
                    return Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Countries.Todos);
                    break;
            }
        }
        
        #endregion

        public static readonly ActivitySessionsBLC Instance = new ActivitySessionsBLC();

    }
}
