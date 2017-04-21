using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class sessionPS
    {
        #region Constructor

        public sessionPS(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            Product = ProductId;
            PharmaForm = PharmaFormId;
            Category = CategoryId;
            Division = DivisionId;
        }

        #endregion

        #region Member

        public int Product;
        public int PharmaForm;
        public int Category;
        public int Division;

        #endregion

        #region Properties

        public int ProductId
        {
            get
            {
                return Product;
            }
            set
            {
                Product = value;
            }
        }

        public int PharmaFormId
        {
            get
            {
                return PharmaForm;
            }
            set
            {
                PharmaForm = value;
            }
        }

        public int CategoryId
        {
            get
            {
                return Category;
            }
            set
            {
                Category = value;
            }
        }

        public int DivisionId
        {
            get
            {
                return Division;
            }
            set
            {
                Division = value;
            }
        }

        #endregion
    }
}