using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class TargetOutputsBLC
    {
        #region Constructors

        private TargetOutputsBLC() { }

        #endregion

                
        #region Public Methods

        public List<TargetOutputsInfo> getTargets()
        {
            return PLMClientsDataAccessComponent.TargetOutputDALC.Instance.getTargets();
        }

        public void insert(TargetOutputsInfo businessEntity)
        {
            PLMClientsDataAccessComponent.TargetOutputDALC.Instance.insert(businessEntity);
        }

        public TargetOutputsInfo getTarget(Byte targetId)
        {
            if (targetId <= 0)
                throw new ArgumentException("The distribution does not exist.");

            PLMClientsBusinessEntities.TargetOutputsInfo m = new TargetOutputsInfo();

            m.TargetId = targetId;

            return PLMClientsDataAccessComponent.TargetOutputDALC.Instance.getOne(targetId);
        }


        public void updateCode(TargetOutputsInfo BEntity)
        {
            PLMClientsDataAccessComponent.TargetOutputDALC.Instance.update(BEntity);
        }

        #endregion

        public static readonly TargetOutputsBLC Instance = new TargetOutputsBLC();
    }
}
