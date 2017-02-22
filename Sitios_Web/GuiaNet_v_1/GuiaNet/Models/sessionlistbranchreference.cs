using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionlistbranchreference
    {
           #region Constructor
        public sessionlistbranchreference(List<Clients> LS)
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