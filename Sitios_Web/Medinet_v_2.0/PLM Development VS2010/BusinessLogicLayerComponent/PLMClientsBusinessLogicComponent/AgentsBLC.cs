using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class AgentsBLC
    {

        #region Constructors

        private AgentsBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.AgentsInfo getAgent(int agentId)
        {
            return PLMClientsDataAccessComponent.AgentsDALC.Instance.getAgent(agentId);
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByBranch(int branchId)
        {
            return PLMClientsDataAccessComponent.AgentsDALC.Instance.getAgentsByBranch(branchId);
        }

        public PLMClientsBusinessEntities.AgentsInfo getAgentByZone(byte zoneId)
        {
            return PLMClientsDataAccessComponent.AgentsDALC.Instance.getAgentByZone(zoneId);
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByPrefix(string prefix, byte targetId)
        {
            return PLMClientsDataAccessComponent.AgentsDALC.Instance.getAgentsByPrefix(prefix, targetId);
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByZoneByLocation(string prefix, byte targetId, byte zoneId, int locationId)
        {
            return PLMClientsDataAccessComponent.AgentsDALC.Instance.getAgentsByZoneByLocation(prefix, targetId, zoneId, locationId);
        }

        public PLMClientsBusinessEntities.AgentDetailInfo getAgentDetail(byte zoneId)
        {
            PLMClientsBusinessEntities.AgentDetailInfo agentDetail = new AgentDetailInfo();

            //Add the Agent's Personal Information 
            this.addAgentInformation(agentDetail, zoneId);

            if (agentDetail != null)
            {
                //Add the Pharmacies information 
                this.addPharmaciesInformation(agentDetail, zoneId);

                //Add the Private Hospitals
                this.addPrivateHospitals(agentDetail, zoneId);

                //Add the Public Hospitals
                this.addPublicHospitals(agentDetail, zoneId);
            }

            return agentDetail;
        }

        #endregion

        #region Private Methods

        private PLMClientsBusinessEntities.AgentDetailInfo addAgentInformation(PLMClientsBusinessEntities.AgentDetailInfo agentDetail, byte zoneId)
        {
            PLMClientsBusinessEntities.AgentsInfo agentInfo = PLMClientsBusinessLogicComponent.AgentsBLC.Instance.getAgentByZone(zoneId);

            if (agentInfo != null)
            {
                agentDetail.AgentId = agentInfo.AgentId;
                agentDetail.TypeId = agentInfo.TypeId;
                agentDetail.FirstName = agentInfo.FirstName;
                agentDetail.LastName = agentInfo.LastName;
                agentDetail.SecondLastName = agentInfo.SecondLastName;
                agentDetail.Email = agentInfo.Email;
                agentDetail.PhoneOne = agentInfo.PhoneOne;
                agentDetail.PhoneTwo = agentInfo.PhoneTwo;
                agentDetail.Active = agentInfo.Active;
            }
            else
                agentDetail = null;

            return agentDetail;
        }

        private PLMClientsBusinessEntities.AgentDetailInfo addPharmaciesInformation(PLMClientsBusinessEntities.AgentDetailInfo agentDetail, byte zoneId)
        {
            agentDetail.PharmacyList = PLMClientsBusinessLogicComponent.CompanyClientBranchesBLC.Instance.getBranchesByAgentByType(zoneId, 
                agentDetail.AgentId, (byte)PLMClientsBusinessEntities.Catalogs.CompanyClientTypes.FARMACIA);

            return agentDetail;
        }

        private PLMClientsBusinessEntities.AgentDetailInfo addPrivateHospitals(PLMClientsBusinessEntities.AgentDetailInfo agentDetail, byte zoneId)
        {
            agentDetail.PrivateHospitals = PLMClientsBusinessLogicComponent.CompanyClientBranchesBLC.Instance.getBranchesByAgentByType(zoneId,
                agentDetail.AgentId, (byte)PLMClientsBusinessEntities.Catalogs.CompanyClientTypes.HOSPITAL_PRIVADO);

            return agentDetail;
        }

        private PLMClientsBusinessEntities.AgentDetailInfo addPublicHospitals(PLMClientsBusinessEntities.AgentDetailInfo agentDetail, byte zoneId)
        {
            agentDetail.PublicHospitals = PLMClientsBusinessLogicComponent.CompanyClientBranchesBLC.Instance.getBranchesByAgentByType(zoneId,
                agentDetail.AgentId, (byte)PLMClientsBusinessEntities.Catalogs.CompanyClientTypes.HOSPITAL_PUBLICO);

            return agentDetail;
        }

        #endregion

        public static readonly AgentsBLC Instance = new AgentsBLC();

    }
}
