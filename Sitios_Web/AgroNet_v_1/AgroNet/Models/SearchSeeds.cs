using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchSeeds
    {
         #region Constructor
        public SearchSeeds(string SeedName)
        {
            seed = SeedName;
        }

        #endregion

        #region Member

        private string seed;

        #endregion

        #region Properties

        public string SeedName
        {
            get
            {
                return seed;
            }
            set
            {
                seed = value;
            }
        }       
        #endregion
    }
}