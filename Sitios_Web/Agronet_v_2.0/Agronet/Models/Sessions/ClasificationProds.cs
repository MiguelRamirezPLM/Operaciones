using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class ClasificationProds
    {
        #region Constructor

        public ClasificationProds(int? PId, int? PFId, int? CatId)
        {
            pharmaform = PFId;
            product = PId;
            category = CatId;
        }

        #endregion

        #region Member

        public int? pharmaform;
        public int? product;
        public int? category;
        #endregion

        #region Properties

        public int? PId
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }

        public int? PFId
        {
            get
            {
                return pharmaform;
            }
            set
            {
                pharmaform = value;
            }
        }

        public int? CatId
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        #endregion
    }
}