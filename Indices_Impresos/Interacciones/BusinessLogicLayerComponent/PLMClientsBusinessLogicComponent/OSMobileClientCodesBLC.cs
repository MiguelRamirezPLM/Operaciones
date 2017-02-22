using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class OSMobileClientCodesBLC
    {

        #region Constructors

        private OSMobileClientCodesBLC() { }

        #endregion

        #region Public Methods

        public int addMobileToCode(PLMClientsBusinessEntities.OSMobileClientCodesInfo BEntity)
        {
            return PLMClientsDataAccessComponent.OSMobileClientCodesDALC.Instance.insert(BEntity);
        }

        public void removeMobileToCode(PLMClientsBusinessEntities.OSMobileClientCodesInfo BEntity)
        {
            PLMClientsDataAccessComponent.OSMobileClientCodesDALC.Instance.delete(BEntity);
        }

        public PLMClientsBusinessEntities.OSMobileClientCodesInfo getMobile(PLMClientsBusinessEntities.OSMobileClientCodesInfo BEntity)
        {
            return PLMClientsDataAccessComponent.OSMobileClientCodesDALC.Instance.getOne(BEntity);
        }

        #endregion

        public static readonly OSMobileClientCodesBLC Instance = new OSMobileClientCodesBLC();

    }
}
