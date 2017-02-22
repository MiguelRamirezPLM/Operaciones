using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class UserFolioCodesBLC
    {
        #region Constructors

        private UserFolioCodesBLC() { }

        #endregion

        #region Public Methods

        public void addFolioToUser(UserFolioCodeInfo BEntity)
        {
            PLMClientsDataAccessComponent.UserFolioCodesDALC.Instance.insert(BEntity);
        }

        public void removeFolioToUser(UserFolioCodeInfo BEntity)
        {
            PLMClientsDataAccessComponent.UserFolioCodesDALC.Instance.delete(BEntity);
        }

        public bool checkUserFolio(int folioId)
        {
            if (folioId <= 0)
                throw new ArgumentException("The folio does not exist.");

            return PLMClientsDataAccessComponent.UserFolioCodesDALC.Instance.assignFolio(folioId);
        }

        #endregion

        public static readonly UserFolioCodesBLC Instance = new UserFolioCodesBLC();

    }
}
