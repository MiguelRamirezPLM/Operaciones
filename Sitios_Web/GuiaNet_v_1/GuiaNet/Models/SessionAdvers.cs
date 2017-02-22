using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class SessionAdvers
    {
        #region Constructor

        public SessionAdvers(int SpecialityId, int ClientId, int EditionId)
        {
            SPId = SpecialityId;
            CId = ClientId;
            EId = EditionId;
        }

        #endregion

        #region Members


        public int SPId;
        public int CId;
        public int EId;

        #endregion

        #region Properties

        public int SpecialityId
        {
            get
            {
                return SPId;
            }
            set
            {
                SPId = value;
            }
        }

        public int ClientId
        {
            get
            {
                return CId;
            }
            set
            {
                CId = value;
            }
        }

        public int EditionId
        {
            get
            {
                return EId;
            }
            set
            {
                EId = value;
            }
        }

        #endregion
    }
}