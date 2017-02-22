using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionAttributeGroup
    {
        #region Constructor
        public SessionAttributeGroup(List<HTMLAttributes> LA)
        {
            ListATTR = LA;
        }

        #endregion

        #region Member

        private List<HTMLAttributes> ListATTR;

        #endregion

        #region Properties

        public List<HTMLAttributes> LA
        {
            get
            {
                return ListATTR;
            }
            set
            {
                ListATTR = value;
            }
        }
        #endregion
    }
}