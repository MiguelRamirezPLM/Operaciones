using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ResidenceClientsBLC
    {

        #region Constructors

        private ResidenceClientsBLC() { }

        #endregion

        #region Public Methods

        public int addResidenceToClient(PLMClientsBusinessEntities.ResidenceClientsInfo BEntity)
        {
            return PLMClientsDataAccessComponent.ResidenceClientsDALC.Instance.insert(BEntity);
        }

        public void removeResidenceToClient(PLMClientsBusinessEntities.ResidenceClientsInfo BEntity)
        {
            PLMClientsDataAccessComponent.ResidenceClientsDALC.Instance.delete(BEntity);
        }

        public PLMClientsBusinessEntities.ResidenceClientsInfo getResidenceClient(PLMClientsBusinessEntities.ResidenceClientsInfo BEntity)
        {
            return PLMClientsDataAccessComponent.ResidenceClientsDALC.Instance.getOne(BEntity);
        }

        #endregion

        public static readonly ResidenceClientsBLC Instance = new ResidenceClientsBLC();

    }
}
