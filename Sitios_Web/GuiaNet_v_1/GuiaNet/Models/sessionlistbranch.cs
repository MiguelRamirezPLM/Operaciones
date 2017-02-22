using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionlistbranch
    {
          #region Constructor
        public sessionlistbranch(List<Clients> LS)
        {

            List = LS;
        }

        #endregion

        #region Member

        private List<Clients> List;

        #endregion

        #region Properties

        public List<Clients> LS
        {
            get
            {
                return List;
            }
            set
            {
                List = value;
            }
        }
        #endregion
    }
}