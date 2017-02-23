using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Union_Companies_CompanyTypes_CompanyPhones_Cities
    {
        public CompanyEditions CompanyEditions { get; set; }
        public Companies Companies { get; set; }
        public Products Products { get; set; }
        public ProductTypes ProductTypes { get; set; }
        public ProductIndexes ProductIndexes { get; set; }
        public Indexes Indexes { get; set; }
        public CompanySections CompanySections { get; set; }
        public Sections Sections { get; set; }
        public CompanyPhones CompanyPhones { get; set; }
        public Cities Cities { get; set; }
        public CompanyTypes CompanyTypes { get; set; }
        public Editions Editions { get; set; }
        public Countries Countries { get; set; }
        public CompanyBrands CompanyBrands { get; set; }
        public Brands Brands { get; set; }
        public ProductSections ProductSections { get; set; }
        public CompanyBrandIndexes CompanyBrandIndexes { get; set; }
        public CompanyBrandSections CompanyBrandSections { get; set; }
        public ProductEditions ProductEditions { get; set; }
        public CompanyBrandEditions CompanyBrandEditions { get; set; }
        public PhoneTypes PhoneTypes { get; set; }
        public Books Books { get; set; }
        public Advertisements Advertisements { get; set; }
        public AdvertisementEditions AdvertisementEditions { get; set; }
        public CompanyAddresses CompanyAddress { get; set; }
        public EditionCompanySectionAdvers EditionCompanySectionAdvers { get; set; }
        public Addresses Addresses { get; set; }
        public CompanyDistributions CompanyDistributions { get; set; }
        public EditionCompanyDistributions EditionCompanyDistributions { get; set; }
        public List<Companies> Listar()
        {
            var companies = new List<Companies>();

            try
            {
                using (var context = new DEACI_20150917Entities())
                {
                    companies = context.Companies.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return companies;

        }

        public Companies Obtener(int id)
        {
            var compani = new Companies();

            try
            {
                using (var context = new DEACI_20150917Entities())
                {
                    compani = context.Companies
                                     .Include("Products")
                                     .Where(x => x.CompanyId == id)
                                     .Single();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return compani;
        }

        public void guardar()
        {
            try
            {
                using (var context = new DEACI_20150917Entities())
                {
                    if (this.Companies.CompanyId == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
