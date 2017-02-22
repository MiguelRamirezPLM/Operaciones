using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientCodesBLC
    {

        #region Constructors

        private ClientCodesBLC() { }

        #endregion

        #region Public Methods

        public int insert(ClientCodesInfo businessEntity)
        {
            return PLMClientsDataAccessComponent.ClientCodesDALC.Instance.insert(businessEntity);
        }

        public PLMClientsBusinessEntities.CodeInfo associateCode(int clientId, int codeId)
        {
            // Associate code to the user, then activate it:
            PLMClientsBusinessEntities.ClientCodesInfo clientCodeInfo = new PLMClientsBusinessEntities.ClientCodesInfo();
            clientCodeInfo.CodeId = codeId;
            clientCodeInfo.ClientId = clientId;

            PLMClientsDataAccessComponent.ClientCodesDALC.Instance.insert(clientCodeInfo);

            PLMClientsBusinessEntities.CodeInfo codeInfo = PLMClientsDataAccessComponent.CodesDALC.Instance.getOne(codeId);

            // Activate the code:
            codeInfo.Assign = true;
            
            // Update the new code in the database:
            CodesBLC.Instance.updateCodes(codeInfo);

            //Return the code has been created:
            return codeInfo;
        }

        #endregion

        public static readonly ClientCodesBLC Instance = new ClientCodesBLC();

    }
}
