using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ActivityImagesBLC
    {

        #region Constructors

        private ActivityImagesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ActivityImagesInfo> getImagesByPhysicalActivity(byte activityId)
        {
            if (activityId <= 0)
                throw new ArgumentException("The Physical Activity does not exist.");

            return PLMClientsDataAccessComponent.ActivityImagesDALC.Instance.getImagesByPhysicalActivity(activityId);
        }

        #endregion

        public static readonly ActivityImagesBLC Instance = new ActivityImagesBLC();

    }
}
