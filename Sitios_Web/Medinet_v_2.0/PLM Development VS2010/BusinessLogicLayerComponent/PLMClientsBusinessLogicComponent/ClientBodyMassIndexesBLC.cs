using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientBodyMassIndexesBLC
    {

        #region Constructors

        private ClientBodyMassIndexesBLC() { }

        #endregion

        #region Public Methods

        public void addClientBodyMassIndexes(string code, byte indexId)
        {
            if (indexId <= 0)
                throw new ArgumentException("The Body Mass Index is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientBodyMassIndexesInfo clientBodyMassIndex = new ClientBodyMassIndexesInfo();
                clientBodyMassIndex.ClientId = targetClient.ClientId;
                clientBodyMassIndex.CodeId = targetClient.CodeId;
                clientBodyMassIndex.DeviceId = targetClient.DeviceId;
                clientBodyMassIndex.TargetId = targetClient.TargetId;
                clientBodyMassIndex.IndexId = indexId;

                //Insert the record
                PLMClientsDataAccessComponent.ClientBodyMassIndexesDALC.Instance.insert(clientBodyMassIndex);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        public void deleteClientBodyMassIndexes(string code, byte indexId)
        {
            if (indexId <= 0)
                throw new ArgumentException("The Body Mass Index is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientBodyMassIndexesInfo clientBodyMassIndex = new ClientBodyMassIndexesInfo();
                clientBodyMassIndex.ClientId = targetClient.ClientId;
                clientBodyMassIndex.CodeId = targetClient.CodeId;
                clientBodyMassIndex.DeviceId = targetClient.DeviceId;
                clientBodyMassIndex.TargetId = targetClient.TargetId;
                clientBodyMassIndex.IndexId = indexId;

                PLMClientsDataAccessComponent.ClientBodyMassIndexesDALC.Instance.delete(clientBodyMassIndex);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        #endregion

        public static readonly ClientBodyMassIndexesBLC Instance = new ClientBodyMassIndexesBLC();

    }
}
