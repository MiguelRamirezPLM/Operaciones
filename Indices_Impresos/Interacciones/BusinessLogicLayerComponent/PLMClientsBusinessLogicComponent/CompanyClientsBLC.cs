using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CompanyClientsBLC
    {

        #region Constructors

        private CompanyClientsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.CompanyClientsInfo> getCompanyClientsByPrefix(string prefix)
        {
            if (prefix == null || prefix.Trim() == "")
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.CompanyClientsDALC.Instance.getCompanyClientsByPrefix(prefix);
        }

        public List<CompanyClientsInfo> getAllCompanyClients()
        {
            return PLMClientsDataAccessComponent.CompanyClientsDALC.Instance.getAllCompanyClients();
        }

        public void insert(CompanyClientsInfo businessEntity)
        {
            PLMClientsDataAccessComponent.CompanyClientsDALC.Instance.insert(businessEntity);
        }


        #endregion

        public static readonly CompanyClientsBLC Instance = new CompanyClientsBLC();

    }
}
