using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Sessions
{
    public class SessionLI
    {
        #region Constructor

        public SessionLI(int CountryId, int BookId, int EditionTypeId, int EditionId, int DivisionId, int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            country = CountryId;
            division = DivisionId;
            edition = EditionId;
            book = BookId;
            editiontype = EditionTypeId;
            Product = ProductId;
            PharmaForm = PharmaFormId;
            Category = CategoryId;
        }

        #endregion

        #region Member

        public int country;
        public int division;
        public int edition;
        public int book;
        public int editiontype;
        public int? Product;
        public int? PharmaForm;
        public int? Category;

        #endregion

        #region Properties

        public int CountryId
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public int DivisionId
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
            }
        }
        public int EditionId
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
            }
        }
        public int BookId
        {
            get
            {
                return book;
            }
            set
            {
                book = value;
            }
        }

        public int EditionTypeId
        {
            get
            {
                return editiontype;
            }
            set
            {
                editiontype = value;
            }
        }

        public int? ProductId
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

        public int? PharmaFormId
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

        public int? CategoryId
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

        #endregion
    }
}