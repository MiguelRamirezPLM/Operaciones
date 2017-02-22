using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class BlockingCodesBLC
    {
        #region Constructors

        private BlockingCodesBLC() { }

        #endregion
        
        #region Public Methods

        public BlockingCodeInfo getBlockCode(string blockString)
        {
            return PLMClientsDataAccessComponent.BlockingCodesDALC.Instance.getByBlockString(blockString);
        }

        #endregion

        public static readonly BlockingCodesBLC Instance = new BlockingCodesBLC();
    }
}
