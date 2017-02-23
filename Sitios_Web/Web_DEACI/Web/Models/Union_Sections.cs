using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
namespace Web.Models
{
    public class Union_Sections
    {
        public Countries countries { get; set; }
        public Editions edtions { get; set; }
        public CompanyEditions compeditions { get; set; }
        public Companies comp { get; set; }
        public CompanySections compsections { get; set; }
        public Sections sections { get; set; }
     

        // Metodo para que pueda jalar el identificador
        public Companies Obtener(int id)
        {
            var clientes = new Companies();
            try
            {
                using (var bas = new DEACI_20150917Entities())
                {
                    clientes = bas.Companies
                         .Include("Union_Sections")
                        .Where(x => x.CompanyId == id)
                        .Single();
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return clientes; 
            }

        }
        
    }