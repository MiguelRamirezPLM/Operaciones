using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class MenuesBLC
    {

        #region Constructors

        private MenuesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.MobileMenuesInfo> getMenues(string isbn, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId, string applicationKey)
        {
            List<PLMClientsBusinessEntities.MobileMenuesInfo> menues = PLMClientsDataAccessComponent.MenuesDALC.Instance.getByTargetByEdition(isbn, (byte)targetId);

            if (menues != null)
                foreach (PLMClientsBusinessEntities.MobileMenuesInfo menu in menues)
                {
                    menu.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl + menu.BaseUrl;
                }

            return menues;
        }

        #endregion

        public static readonly MenuesBLC Instance = new MenuesBLC();

    }
}
