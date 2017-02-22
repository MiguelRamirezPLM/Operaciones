using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CE_PLMClientsBusinessLogicComponent.PLMClientsEngine;
using System.Threading;

namespace CE_PLMClientsBusinessLogicComponent
{
    public class CE_PLMClientsBLC
    {
        #region Constructors
        
        private CE_PLMClientsBLC() { }

        #endregion
        
        public static readonly CE_PLMClientsBLC Instance = new CE_PLMClientsBLC();
                
    }
}

