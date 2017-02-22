using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class BlockingCodePrefixesBLC
    {
        #region Constructors

        private BlockingCodePrefixesBLC() { }

        #endregion

        #region Public Methods

        public bool checkBlockCode(string prefixName, byte targetId, string blockString)
        {
            return PLMClientsDataAccessComponent.BlockingCodePrefixesDALC.Instance.getByBlockCode(prefixName, targetId, blockString);
        }

        #endregion

        public static readonly BlockingCodePrefixesBLC Instance = new BlockingCodePrefixesBLC();
    }
}
