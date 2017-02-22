using System;
using System.Collections.Generic;
using System.Text;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class MenuesBLC
    {
        #region Constructors

        private MenuesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMUserBusinessEntries.MenuInfo> getMenues()
        {
            return PLMUsersDataAccessComponent.MenuesDALC.Instance.getAll();
        }

        public List<PLMUserBusinessEntries.MenuByWebSection> getMenues(int webPageId, byte webSectionId, int roleId)
        {
            if (webPageId <= 0 || webSectionId == 0 || roleId <= 0)
                throw new ArgumentException("The page does not exist.");

            return PLMUsersDataAccessComponent.MenuesDALC.Instance.getBySectionByRole(webPageId, webSectionId, roleId);
        }

        #endregion

        public static readonly MenuesBLC Instance = new MenuesBLC();
    }
}
