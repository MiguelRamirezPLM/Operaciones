using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionclientid
    {
        #region Constructor
        public sessionclientid(int ClientId, int EditionId, List<Clients> LS)
        {
            clientid = ClientId;
            editionid = EditionId;
            List = LS;
        }

        #endregion

        #region Member

        private int clientid;
        private int editionid;
        private List<Clients> List;

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