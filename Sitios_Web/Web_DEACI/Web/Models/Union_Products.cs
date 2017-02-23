using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    using System;
    using System.Collections.Generic;

    public class Union_Products
    {
        public Products prod { get; set; }
        public ProductTypes prodtypes { get; set; }
        public ProductEditions prodedition { get; set; }
        public Editions editions { get; set; }
        public Companies companies { get; set; }
        public Products Obtener(int id)
        {
            var eproducts = new Products();

            try
            {
                using (var context = new DEACI_20150917Entities())
                {
                    eproducts = context.Products
                                     .Include("Companies")
                                     .Where(x => x.ProductId == id)
                                     .Single();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return eproducts;
        }
    
    }
}