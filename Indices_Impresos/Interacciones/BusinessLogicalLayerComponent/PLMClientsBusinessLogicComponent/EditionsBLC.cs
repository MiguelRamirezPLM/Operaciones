using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class EditionsBLC
    {

        #region Constructors

        private EditionsBLC() { }

        #endregion

        #region Public Methods

        public EditionInfo getEditionByCode(string code)
        {
            return PLMClientsDataAccessComponent.EditionsDALC.Instance.getByCode(code);
        }

        public MobileEditionInfo getByTargetByCountry(byte targetId, string country)
        {
            return PLMClientsDataAccessComponent.EditionsDALC.Instance.getByTargetByCountry(targetId, country);
        }

        public MobileEditionInfo getByPrefixByTargetByCountry(string prefix, byte targetId, string country)
        {
            return PLMClientsDataAccessComponent.EditionsDALC.Instance.getByPrefixByTargetByCountry(prefix, targetId, country);
        }

        #endregion

        public static readonly EditionsBLC Instance = new EditionsBLC();

    }
}
