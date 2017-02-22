using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessEntries
{
    public class CatalogsTypeSearchInfo
    {
        #region Constructors

        private CatalogsTypeSearchInfo() { }

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
