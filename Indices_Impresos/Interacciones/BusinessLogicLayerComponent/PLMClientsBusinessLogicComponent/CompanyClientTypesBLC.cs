using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CompanyClientTypesBLC
    {
        #region Constructors

        private CompanyClientTypesBLC() { }

        #endregion

        #region Public Methods

        public List<CompanyClientTypesInfo> getCompanyClientTypes()
        {
            return PLMClientsDataAccessComponent.CompanyClientTypesDALC.Instance.getCompanyClientTypes();
        }

        #endregion

        public static readonly CompanyClientTypesBLC Instance = new CompanyClientTypesBLC();
    }
}
