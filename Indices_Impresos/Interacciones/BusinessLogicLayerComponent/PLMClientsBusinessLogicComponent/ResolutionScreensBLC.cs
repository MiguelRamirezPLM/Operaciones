using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class ResolutionScreensBLC
    {

        #region Constructors

        private ResolutionScreensBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.ResolutionScreensInfo getByResolutionKey(string resolutionKey)
        {
            return PLMClientsDataAccessComponent.ResolutionScreensDALC.Instance.getByResolutionKey(resolutionKey);
        }

        #endregion

        public static readonly ResolutionScreensBLC Instance = new ResolutionScreensBLC();

    }
}
