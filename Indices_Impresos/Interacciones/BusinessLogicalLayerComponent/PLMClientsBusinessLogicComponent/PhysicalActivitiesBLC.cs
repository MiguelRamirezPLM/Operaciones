using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class PhysicalActivitiesBLC
    {

        #region Constructors

        private PhysicalActivitiesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> getActivitiesByCalculatorResult(int calculatorId, int resultId)
        {
            if (calculatorId <= 0 || resultId <= 0)
                throw new ArgumentException("The Calculator or Result does not exist.");

            List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> physicalActivities = 
                PLMClientsDataAccessComponent.PhysicalActivitiesDALC.Instance.getActivitiesByCalculatorResult(calculatorId, resultId);

            //Add Items for Physical Activity
            this.addItems(physicalActivities, calculatorId, resultId);

            //Add Images for Physical Activity
            this.addImages(physicalActivities, calculatorId, resultId);

            return physicalActivities;
        }

        #endregion

        #region Private Methods

        private void addItems(List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> physicalActivities, int calculatorId, int resultId)
        {
            foreach (PhysicalActivityDetailInfo activity in physicalActivities)
                activity.ActivityItems = PLMClientsBusinessLogicComponent.ItemsBLC.Instance.getActivityItemsByCalculatorResult(calculatorId, resultId, activity.ActivityId);
        }

        private void addImages(List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> physicalActivities, int calculatorId, int resultId)
        {
            foreach (PhysicalActivityDetailInfo activity in physicalActivities)
            {
                activity.ActivityImages = PLMClientsBusinessLogicComponent.ActivityImagesBLC.Instance.getImagesByPhysicalActivity(activity.ActivityId);

                foreach (ActivityImagesInfo image in activity.ActivityImages)
                    image.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["RutinesBaseUrl"] + image.BaseUrl;
            }
        }

        #endregion

        public static readonly PhysicalActivitiesBLC Instance = new PhysicalActivitiesBLC();

    }
}
