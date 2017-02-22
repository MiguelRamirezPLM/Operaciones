using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ItemsBLC
    {

        #region Constructors

        private ItemsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ItemDetailInfo> getActivityItemsByCalculatorResult(int calculatorId, int resultId, byte activityId)
        {
            if (calculatorId <= 0 || resultId <= 0 || activityId <= 0)
                throw new ArgumentException("The Calculator or Result or Physical Activity does not exist.");

            return PLMClientsDataAccessComponent.ItemsDALC.Instance.getActivityItemsByCalculatorResult(calculatorId, resultId, activityId);
        }

        #endregion

        public static readonly ItemsBLC Instance = new ItemsBLC();

    }
}
