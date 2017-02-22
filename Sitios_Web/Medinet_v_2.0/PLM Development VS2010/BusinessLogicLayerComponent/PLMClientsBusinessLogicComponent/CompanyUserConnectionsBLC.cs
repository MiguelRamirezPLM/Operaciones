using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CompanyUserConnectionsBLC
    {

        #region Constructors

        private CompanyUserConnectionsBLC() { }

        #endregion

        #region Public Methods

        public int addUserConnection(PLMClientsBusinessEntities.CompanyUserConnectionsInfo BEntity)
        {
            return PLMClientsDataAccessComponent.CompanyUserConnectionsDALC.Instance.insert(BEntity);
        }

        public PLMClientsBusinessEntities.CompanyUserConnectionsInfo getUserConnection(int userConnectionId)
        {
            return PLMClientsDataAccessComponent.CompanyUserConnectionsDALC.Instance.getOne(userConnectionId);
        }

        public bool checkSession(string code, int sessionTime)
        {
            return PLMClientsDataAccessComponent.CompanyUserConnectionsDALC.Instance.checkSession(code, sessionTime);
        }

        #endregion

        public static readonly CompanyUserConnectionsBLC Instance = new CompanyUserConnectionsBLC();

    }
}
