using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class NickNamePrefixesBLC
    {
        #region Constructors

        private NickNamePrefixesBLC() { }

        #endregion

        #region Public Methods

        public NickNamePrefixInfo getNickNamePrefix(int nickPrefixId)
        {
            if (nickPrefixId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.NickNamePrefixesDALC.Instance.getOne(nickPrefixId);
        }
        
        public NickNamePrefixInfo getNickNamePrefix(string prefixName)
        {
            return PLMClientsDataAccessComponent.NickNamePrefixesDALC.Instance.getByPrefixName(prefixName);
        }

        public NickNamePrefixInfo getNickNameByPrefix(int prefixId)
        {
            if (prefixId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.NickNamePrefixesDALC.Instance.getByPrefixId(prefixId);
        }

        public void updateNickPrefix(NickNamePrefixInfo BEntity)
        {
            PLMClientsDataAccessComponent.NickNamePrefixesDALC.Instance.update(BEntity);
        }

        #endregion

        public static readonly NickNamePrefixesBLC Instance = new NickNamePrefixesBLC();

    }
}
