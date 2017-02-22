using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionspbybranch
    {
          #region Constructor
        public sessionspbybranch(int ClientId, int EditionId, List<SpecialitiesAdvers> LS)
        {
            clientid = ClientId;
            editionid = EditionId;
            List = LS;
        }

        #endregion

        #region Member

        private int clientid;
        private int editionid;
        private List<SpecialitiesAdvers> List;

        #endregion

        #region Properties

        public int ClientId
        {
            get
            {
                return clientid;
            }
            set
            {
                clientid = value;
            }
        }

        public int EditionId
        {
            get
            {
                return editionid;
            }
            set
            {
                editionid = value;
            }
        }

        public List<SpecialitiesAdvers> LS
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