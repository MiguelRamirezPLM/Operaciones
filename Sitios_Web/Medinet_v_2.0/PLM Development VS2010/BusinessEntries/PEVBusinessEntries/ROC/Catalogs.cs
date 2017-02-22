using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessEntries.ROC
{
    public class Catalogs
    {
        #region Constructors

        private Catalogs() { }

        #endregion

        #region Enums

        public enum TypeSearch 
        {
            BYLETTER = 1,
            BYTEXT = 2,
            BYFULLTEXT = 3
        }

        #endregion


    }
}
