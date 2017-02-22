using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class TargetIdentifiersBLC
    {

        #region Constructors

        private TargetIdentifiersBLC() { }

        #endregion

        #region Public Methods

        public int addTargetIdentifier(PLMClientsBusinessEntities.TargetIdentifiersInfo BEntity)
        {
            return PLMClientsDataAccessComponent.TargetIdentifiersDALC.Instance.insert(BEntity);
        }

        public void removeTargetIdentifier(PLMClientsBusinessEntities.TargetIdentifiersInfo BEntity)
        {
            PLMClientsDataAccessComponent.TargetIdentifiersDALC.Instance.delete(BEntity);
        }

        public PLMClientsBusinessEntities.TargetIdentifiersInfo getOne(PLMClientsBusinessEntities.TargetIdentifiersInfo BEntity)
        {
            return PLMClientsDataAccessComponent.TargetIdentifiersDALC.Instance.getOne(BEntity);
        }

        public PLMClientsBusinessEntities.TargetIdentifiersInfo getDeviceByTarget(byte targetId)
        {
            return PLMClientsDataAccessComponent.TargetIdentifiersDALC.Instance.getDeviceByTarget(targetId);
        }

        #endregion

        public static readonly TargetIdentifiersBLC Instance = new TargetIdentifiersBLC();

    }
}
