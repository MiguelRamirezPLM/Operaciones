using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public class FoliosBLC
    {
        #region Constructor

        private FoliosBLC() { }

        #endregion
        #region Public methods

        public FolioInfo  getFolio(int pk)
        {
            return PLMUsersDataAccessComponent.FolioDALC.Instance.getOne(pk);
        }

        public int addFolio(FolioInfo BEntity)
        {
            return PLMUsersDataAccessComponent.FolioDALC.Instance.insert(BEntity);
        }
        
        public void removeFolio(FolioInfo BEntity)
        {
            PLMUsersDataAccessComponent.FolioDALC.Instance.delete(BEntity);
        }

        public void updateFolio(FolioInfo BEntity)
        {
             PLMUsersDataAccessComponent.FolioDALC.Instance.update(BEntity);
        }

        public FolioInfo getByapplication(int application)
        {
            return PLMUsersDataAccessComponent.FolioDALC.Instance.getByApplication(application);
        }

        #endregion

        public static readonly FoliosBLC Instance = new FoliosBLC();
    }
}
