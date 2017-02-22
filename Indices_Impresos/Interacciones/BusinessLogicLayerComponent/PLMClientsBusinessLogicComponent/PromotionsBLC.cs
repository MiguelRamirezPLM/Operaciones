using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class PromotionsBLC
    {

        #region Constructors

        private PromotionsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PromotionsInfo> getByCompanyClient(int companyClientId)
        {
            return PLMClientsDataAccessComponent.PromotionsDALC.Instance.getByCompanyClient(companyClientId);
        }

        #endregion

        public static readonly PromotionsBLC Instance = new PromotionsBLC();

    }
}
