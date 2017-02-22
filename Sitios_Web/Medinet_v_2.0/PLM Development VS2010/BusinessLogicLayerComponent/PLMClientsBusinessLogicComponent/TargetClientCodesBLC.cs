using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class TargetClientCodesBLC
    {

        #region Constructors

        private TargetClientCodesBLC() { }

        #endregion

        #region Public Methods

        public int addCodeToTargetOutput(PLMClientsBusinessEntities.TargetClientCodesInfo BEntity)
        {
            if (BEntity.DeviceId == Convert.ToByte(PLMClientsBusinessEntities.Catalogs.DeviceIdentifiers.NIC))
            {
                if(BEntity.HWIdentifier != null)
                    BEntity.HWIdentifier = this.cleanHwIdentifier(BEntity.HWIdentifier);
            }
            return PLMClientsDataAccessComponent.TargetClientCodesDALC.Instance.insert(BEntity);
        }

        public void updateCodeToTargetOutput(PLMClientsBusinessEntities.TargetClientCodesInfo BEntity)
        {
            if (BEntity.DeviceId == Convert.ToByte(PLMClientsBusinessEntities.Catalogs.DeviceIdentifiers.NIC))
                BEntity.HWIdentifier = this.cleanHwIdentifier(BEntity.HWIdentifier);

            PLMClientsDataAccessComponent.TargetClientCodesDALC.Instance.update(BEntity);
        }

        public void removeCodeToTargetOutput(PLMClientsBusinessEntities.TargetClientCodesInfo BEntity)
        {
            PLMClientsDataAccessComponent.TargetClientCodesDALC.Instance.delete(BEntity);
        }

        public PLMClientsBusinessEntities.TargetClientCodesInfo getByPrefixByHwIdentifier(string codePrefix, string hwIdentifier)
        {
            return PLMClientsDataAccessComponent.TargetClientCodesDALC.Instance.getByPrefixByHwIdentifier(codePrefix, hwIdentifier);
        }

        public PLMClientsBusinessEntities.TargetClientCodesInfo getTargetClientByCode(string code)
        {
            if (code.Trim() == "" || code == null)
                throw new ArgumentException("The code is not valid.");

            return PLMClientsDataAccessComponent.TargetClientCodesDALC.Instance.getTargetClientByCode(code);
        }

        #endregion

        #region Private Methods

        private string cleanHwIdentifier(string hwIdentifier)
        {
            string[] macAddsArray = hwIdentifier.Trim().Split(';');

            StringBuilder macAdds = new StringBuilder();

            for (int x = 0; x < macAddsArray.Length; x++)
            {
                if (!macAddsArray[x].Equals("0000000000000000") && !string.IsNullOrEmpty(macAddsArray[x].Trim()))
                {
                    macAdds.Append(macAddsArray[x] + ";");
                }
            }
            return macAdds.ToString();
        }

        #endregion

        public static readonly TargetClientCodesBLC Instance = new TargetClientCodesBLC();

    }
}
