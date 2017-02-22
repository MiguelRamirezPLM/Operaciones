using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientDataTypesBLC
    {

        #region Constructors

        private ClientDataTypesBLC() { }

        #endregion

        #region Public Methods

        public void addClientDataTypes(string code, byte dataTypeId, string dataValue)
        {
            if (dataTypeId <= 0 || dataValue.Trim() == "" || dataValue == null)
                throw new ArgumentException("The Data Type or Data Value is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientDataTypesInfo clientDataInfo = new ClientDataTypesInfo();
                clientDataInfo.ClientId = targetClient.ClientId;
                clientDataInfo.CodeId = targetClient.CodeId;
                clientDataInfo.DeviceId = targetClient.DeviceId;
                clientDataInfo.TargetId = targetClient.TargetId;
                clientDataInfo.DataTypeId = dataTypeId;
                clientDataInfo.DataValue = dataValue;

                PLMClientsDataAccessComponent.ClientDataTypesDALC.Instance.insert(clientDataInfo);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        public void updateClientDataTypes(string code, byte dataTypeId, string dataValue)
        {
            if (dataTypeId <= 0 || dataValue.Trim() == "" || dataValue == null)
                throw new ArgumentException("The Data Type or Data Value is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientDataTypesInfo clientDataInfo = new ClientDataTypesInfo();
                clientDataInfo.ClientId = targetClient.ClientId;
                clientDataInfo.CodeId = targetClient.CodeId;
                clientDataInfo.DeviceId = targetClient.DeviceId;
                clientDataInfo.TargetId = targetClient.TargetId;
                clientDataInfo.DataTypeId = dataTypeId;
                clientDataInfo.DataValue = dataValue;

                PLMClientsDataAccessComponent.ClientDataTypesDALC.Instance.update(clientDataInfo);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        public void deleteClientDataTypes(string code, byte dataTypeId, string dataValue)
        {
            if (dataTypeId <= 0 || dataValue.Trim() == "" || dataValue == null)
                throw new ArgumentException("The Data Type or Data Value is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientDataTypesInfo clientDataInfo = new ClientDataTypesInfo();
                clientDataInfo.ClientId = targetClient.ClientId;
                clientDataInfo.CodeId = targetClient.CodeId;
                clientDataInfo.DeviceId = targetClient.DeviceId;
                clientDataInfo.TargetId = targetClient.TargetId;
                clientDataInfo.DataTypeId = dataTypeId;

                PLMClientsDataAccessComponent.ClientDataTypesDALC.Instance.delete(clientDataInfo);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        #endregion

        public static readonly ClientDataTypesBLC Instance = new ClientDataTypesBLC();

    }
}
