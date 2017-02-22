using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class FolioPrefixesBLC
    {
        #region Constructors

        private FolioPrefixesBLC() { }

        #endregion

        #region Public Methods

        public FolioPrefixInfo getFolioPrefix(int prefixId)
        {
            if (prefixId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.FolioPrefixesDALC.Instance.getOne(prefixId);
        }

        public FolioPrefixInfo getFolioPrefix(string folioPrefix)
        {
            return PLMClientsDataAccessComponent.FolioPrefixesDALC.Instance.getByPrefixName(folioPrefix);
        }

        public void updateFolio(FolioPrefixInfo BEntity)
        {
            PLMClientsDataAccessComponent.FolioPrefixesDALC.Instance.update(BEntity);
        }

        #endregion

        public static readonly FolioPrefixesBLC Instance = new FolioPrefixesBLC();

    }
}
