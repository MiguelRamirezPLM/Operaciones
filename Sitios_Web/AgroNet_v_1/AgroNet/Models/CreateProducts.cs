using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace AgroNet.Models
{
    public class CreateProducts
    {
        private DEAQ db = new DEAQ();
        public DEAQ DEAQ = new DEAQ();
        public PLMUsers dbusers = new PLMUsers();
        private PLMUsers USERS = new PLMUsers();
        Products Products = new Products();
        ProductPharmaForms ProductPharmaForms = new ProductPharmaForms();
        ProductCategories ProductCategories = new ProductCategories();
        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();
        DivisionCategories DivisionCategories = new DivisionCategories();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        Newproducts Newproducts = new Newproducts();
        MentionatedProducts MentionatedProducts = new MentionatedProducts();
        Models.Users User = new Users();
        ActivityLogs ActivityLogs = new ActivityLogs();
        ActivityLog ActivityLog = new ActivityLog();

        //public bool CreateProduct(string Product, string description, string division, string editionid, string pharmaform,
        //    string category, string Mentionated, string Participant, string RegisterSanitary, int ApplicationId, int UsersId, string Country)
        //{

        //    string ProductName = Product.Trim();
        //    string Descript = description.Trim();
        //    string Registry = RegisterSanitary.Trim();

        //    int ProdId = 0;
        //    int DivisionId = int.Parse(division);
        //    int CategoryId = int.Parse(category);
        //    int EditionId = int.Parse(editionid);
        //    int PharmaFormId = int.Parse(pharmaform);
        //    bool ParticipantP = Convert.ToBoolean(Participant);
        //    bool MentionatedP = Convert.ToBoolean(Mentionated);
        //    int CountryId = int.Parse(Country);

        //    int? LaboratoryId;

        //    var divs = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();
        //    foreach (Divisions _divs in divs)
        //    {
        //        LaboratoryId = _divs.LaboratoryId;
        //    }

        //    if (ProductName.Contains(","))
        //    {
        //        String Value = String.Empty;
        //        String cierre = ",";
        //        int j, i;
        //        String _string = ProductName;
        //        i = _string.IndexOf(_string, 0);
        //        if (i >= 0)
        //        {
        //            j = _string.IndexOf(cierre, i);
        //            Value = _string.Substring(i, j - i);
        //            ProductName = Value;
        //        }
        //    }


        //    var ProdC = (from ProductCat in db.Products
        //                 where ProductCat.ProductName == ProductName
        //                select ProductCat).ToList();


        //    var Divs = (from Divisions in db.Divisions
        //                join Labs in db.Laboratories
        //                on Divisions.LaboratoryId equals Labs.LaboratoryId
        //                where Divisions.DivisionId == DivisionId
        //                && Divisions.CountryId == CountryId
        //                select Divisions).ToList();

        //    var PN = (from Productt in db.Products
        //             where Productt.ProductName == ProductName
        //             && Productt.Description == Descript
        //             && Productt.CountryId == CountryId
        //              select Productt).ToList();


        //    if (PN.LongCount() > 0)
        //    {
        //        foreach (Products Prods in PN)
        //        {
        //            if (Prods.ProductName.ToUpper().Equals(ProductName.ToUpper()))
        //            {
        //                ProdId = Prods.ProductId;

        //                if (PN.LongCount() > 0)
        //                {
        //                    goto ProdPF;
        //                }
        //                else
        //                {
        //                    goto ProductP;
        //                }
        //            ProductP: foreach (Divisions Div in Divs)
        //                {
        //                    if (Div.DivisionId.Equals(DivisionId))
        //                    {
        //                        int? Laboratory = Div.LaboratoryId;
        //                        Products.ProductName = ProductName;
        //                        Products.Description = Descript;
        //                        Products.Register = Registry;
        //                        Products.CountryId = int.Parse(Country);
        //                        Products.LaboratoryId = Laboratory;
        //                        Products.Active = true;
        //                        Products.AccessId = 0;
        //                    }
        //                }
        //                DEAQ.Products.Add(Products);
        //                DEAQ.SaveChanges();

        //            ProdPF: var ProductPF = from PPForms in db.ProductPharmaForms
        //                                    where PPForms.PharmaFormId == PharmaFormId
        //                                    && PPForms.ProductId == ProdId
        //                                    select PPForms;
        //                if (ProductPF.LongCount() > 0)
        //                {
        //                    goto DC;
        //                }
        //                else
        //                {
        //                    goto PPF;
        //                }
        //            PPF: ProductPharmaForms.ProductId = ProdId;
        //            ProductPharmaForms.PharmaFormId = PharmaFormId;
        //                ProductPharmaForms.Active = true;

        //                DEAQ.ProductPharmaForms.Add(ProductPharmaForms);
        //                DEAQ.SaveChanges();

        //                ActivityLog.ProductPharmaForms(ProdId, PharmaFormId, ApplicationId, UsersId);

        //            DC: var DivisionC = from DC in db.DivisionCategories
        //                                where DC.DivisionId == DivisionId
        //                                && DC.CategoryId == CategoryId
        //                                select DC;
        //                if (DivisionC.LongCount() > 0)
        //                {
        //                    goto PCP;
        //                }
        //                else
        //                {
        //                    goto DivC;
        //                }
        //            DivC: DivisionCategories.DivisionId = DivisionId;
        //            DivisionCategories.CategoryId = CategoryId;
        //                DivisionCategories.Active = true;

        //                DEAQ.DivisionCategories.Add(DivisionCategories);
        //                DEAQ.SaveChanges();

        //                ActivityLog.DivisionCategories(DivisionId, CategoryId, ApplicationId, UsersId);

        //            PCP: var PrC = from ProdCateg in db.ProductCategories
        //                           where ProdCateg.ProductId == ProdId
        //                           && ProdCateg.PharmaFormId == PharmaFormId
        //                           && ProdCateg.CategoryId == CategoryId
        //                           && ProdCateg.DivisionId == DivisionId
        //                           select ProdCateg;
        //                if (PrC.LongCount() > 0)
        //                {
        //                    goto NP;
        //                }
        //                else
        //                {
        //                    goto PC;
        //                }

        //            PC: ProductCategories.ProductId = ProdId;
        //            ProductCategories.PharmaFormId = PharmaFormId;
        //                ProductCategories.DivisionId = DivisionId;
        //                ProductCategories.CategoryId = CategoryId;

        //                DEAQ.ProductCategories.Add(ProductCategories);
        //                DEAQ.SaveChanges();

        //                ActivityLog.ProductCategories(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId);

        //            NP: var NEWP = from NProducts in db.Newproducts
        //                           where NProducts.CategoryId == CategoryId
        //                           && NProducts.DivisionId == DivisionId
        //                           && NProducts.EditionId == EditionId
        //                           && NProducts.PharmaFormId == PharmaFormId
        //                           && NProducts.ProductId == ProdId
        //                           select NProducts;
        //                if (NEWP.LongCount() > 0)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    NProducts.ProductId = ProdId;
        //                    NProducts.PharmaFormId = PharmaFormId;
        //                    NProducts.DivisionId = DivisionId;
        //                    NProducts.CategoryId = CategoryId;
        //                    NProducts.EditionId = EditionId;

        //                    DEAQ.Newproducts.Add(NProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.NewProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                }
        //                if (ParticipantP != false)
        //                {
        //                    ParticipantProducts.CategoryId = CategoryId;
        //                    ParticipantProducts.DivisionId = DivisionId;
        //                    ParticipantProducts.EditionId = EditionId;
        //                    ParticipantProducts.PharmaFormId = PharmaFormId;
        //                    ParticipantProducts.ProductId = ProdId;

        //                    DEAQ.ParticipantProducts.Add(ParticipantProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.ParticipanProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
        //                }
        //                else if (MentionatedP != false)
        //                {
        //                    MentionatedProducts.CategoryId = CategoryId;
        //                    MentionatedProducts.DivisionId = DivisionId;
        //                    MentionatedProducts.EditionId = EditionId;
        //                    MentionatedProducts.PharmaFormId = PharmaFormId;
        //                    MentionatedProducts.ProductId = ProdId;

        //                    DEAQ.MentionatedProducts.Add(MentionatedProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.MentionatedProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {



        //    Product: foreach (Divisions Div in Divs)
        //        {
        //            if (Div.DivisionId.Equals(DivisionId))
        //            {
        //                if (Registry == string.Empty)
        //                {


        //                    int? Laboratory = Div.LaboratoryId;
        //                    Products.ProductName = ProductName;
        //                    Products.Description = Descript;
        //                    Products.Register = Registry;
        //                    Products.CountryId = CountryId;
        //                    Products.LaboratoryId = Laboratory;
        //                    Products.Active = true;
        //                    Products.AccessId = 0;
        //                }
        //                else
        //                {
        //                    int? Laboratory = Div.LaboratoryId;
        //                    Products.ProductName = ProductName;
        //                    Products.Description = Descript;
        //                    Products.Register = Registry;
        //                    Products.CountryId = CountryId;
        //                    Products.LaboratoryId = Laboratory;
        //                    Products.Active = true;
        //                    Products.AccessId = 0;
        //                }
        //            }
        //        }


        //        DEAQ.Products.Add(Products);
        //        DEAQ.SaveChanges();

        //        var Prod = from Productt in db.Products
        //                   where Productt.ProductName == ProductName
        //              && Productt.Description == Descript
        //                   select Productt;

        //        var table = from Tables1 in dbusers.Tables
        //                    where Tables1.Description == "Products"
        //                    && Tables1.ApplicationId == ApplicationId
        //                    select Tables1;
        //        foreach (Products Pr in Prod)
        //        {
        //            foreach (Tables tbl in table)
        //            {
        //                if (tbl.Description.Equals("Products"))
        //                {
        //                    if (Pr.ProductName.Equals(ProductName))
        //                    {
        //                        foreach (Divisions Div in Divs)
        //                        {
        //                            if (Div.DivisionId.Equals(DivisionId))
        //                            {
        //                                int? Laboratory = Div.LaboratoryId;
        //                                ProdId = Pr.ProductId;
        //                                int TableId = tbl.TableId;


        //                                //var Product = Convert.ToString(Pr.ProductId);
        //                                var PForm = Convert.ToString(PharmaFormId);
        //                                var LabId = Convert.ToString(Laboratory);

        //                                ActivityLog.CreateNewProducts(UsersId, ProductName, Product, TableId, Descript, Registry, CountryId, LabId);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        var P = from Prods in db.Products
        //                where Prods.ProductName == ProductName
        //                && Prods.Description == Descript
        //                select Prods;

        //        foreach (Products Prods in P)
        //        {
        //            if (Prods.ProductName.Equals(ProductName))
        //            {
        //                ProdId = Prods.ProductId;

        //                if (P.LongCount() > 0)
        //                {
        //                    goto ProdPF;
        //                }
        //                else
        //                {
        //                    goto Product;
        //                }


        //            ProdPF: var ProductPF = from PPForms in db.ProductPharmaForms
        //                                    where PPForms.PharmaFormId == PharmaFormId
        //                                    && PPForms.ProductId == ProdId
        //                                    select PPForms;
        //                if (ProductPF.LongCount() > 0)
        //                {
        //                    goto DC;
        //                }
        //                else
        //                {
        //                    goto PPF;
        //                }

        //            PPF: ProductPharmaForms.ProductId = ProdId;
        //                ProductPharmaForms.PharmaFormId = PharmaFormId;
        //                ProductPharmaForms.Active = true;

        //                DEAQ.ProductPharmaForms.Add(ProductPharmaForms);
        //                DEAQ.SaveChanges();

        //                ActivityLog.ProductPharmaForms(ProdId, PharmaFormId, ApplicationId, UsersId);

        //            DC: var DivisionC = from DC in db.DivisionCategories
        //                                where DC.DivisionId == DivisionId
        //                                && DC.CategoryId == CategoryId
        //                                select DC;
        //                if (DivisionC.LongCount() > 0)
        //                {
        //                    goto PCP;
        //                }
        //                else
        //                {
        //                    goto DivC;
        //                }
        //            DivC: DivisionCategories.DivisionId = DivisionId;
        //            DivisionCategories.CategoryId = CategoryId;
        //                DivisionCategories.Active = true;

        //                DEAQ.DivisionCategories.Add(DivisionCategories);
        //                DEAQ.SaveChanges();

        //                ActivityLog.DivisionCategories(DivisionId, CategoryId, ApplicationId, UsersId);

        //            PCP: var PrC = from ProdCateg in db.ProductCategories
        //                           where ProdCateg.ProductId == ProdId
        //                           && ProdCateg.PharmaFormId == PharmaFormId
        //                           && ProdCateg.CategoryId == CategoryId
        //                           && ProdCateg.DivisionId == DivisionId
        //                           select ProdCateg;
        //                if (PrC.LongCount() > 0)
        //                {
        //                    goto NP;
        //                }
        //                else
        //                {
        //                    goto PC;
        //                }

        //            PC: ProductCategories.ProductId = ProdId;
        //            ProductCategories.PharmaFormId = PharmaFormId;
        //                ProductCategories.DivisionId = DivisionId;
        //                ProductCategories.CategoryId = CategoryId;

        //                DEAQ.ProductCategories.Add(ProductCategories);
        //                DEAQ.SaveChanges();

        //                ActivityLog.ProductCategories(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId);

        //            NP: var NEWP = from NProducts in db.Newproducts
        //                           where NProducts.CategoryId == CategoryId
        //                           && NProducts.DivisionId == DivisionId
        //                           && NProducts.EditionId == EditionId
        //                           && NProducts.PharmaFormId == PharmaFormId
        //                           && NProducts.ProductId == ProdId
        //                           select NProducts;
        //                if (NEWP.LongCount() > 0)
        //                {

        //                }
        //                else
        //                {
        //                    NProducts.ProductId = ProdId;
        //                    NProducts.PharmaFormId = PharmaFormId;
        //                    NProducts.DivisionId = DivisionId;
        //                    NProducts.CategoryId = CategoryId;
        //                    NProducts.EditionId = EditionId;

        //                    DEAQ.Newproducts.Add(NProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.NewProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);


        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                }

        //                if (ParticipantP != false)
        //                {
        //                    ParticipantProducts.CategoryId = CategoryId;
        //                    ParticipantProducts.DivisionId = DivisionId;
        //                    ParticipantProducts.EditionId = EditionId;
        //                    ParticipantProducts.PharmaFormId = PharmaFormId;
        //                    ParticipantProducts.ProductId = ProdId;

        //                    DEAQ.ParticipantProducts.Add(ParticipantProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.ParticipanProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
        //                }
        //                else if (MentionatedP != false)
        //                {
        //                    MentionatedProducts.CategoryId = CategoryId;
        //                    MentionatedProducts.DivisionId = DivisionId;
        //                    MentionatedProducts.EditionId = EditionId;
        //                    MentionatedProducts.PharmaFormId = PharmaFormId;
        //                    MentionatedProducts.ProductId = ProdId;

        //                    DEAQ.MentionatedProducts.Add(MentionatedProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.MentionatedProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);

        //                    EditionDivisionProducts.EditionId = EditionId;
        //                    EditionDivisionProducts.ProductId = ProdId;
        //                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
        //                    EditionDivisionProducts.DivisionId = DivisionId;
        //                    EditionDivisionProducts.CategoryId = CategoryId;

        //                    DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
        //                    DEAQ.SaveChanges();

        //                    ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}

        public bool CreateProduct(string Product, string description, string division, string editionid, string pharmaform,
            string category, string Mentionated, string Participant, string RegisterSanitary, int ApplicationId, int UsersId, string Country)
        {
            int DivisionId = int.Parse(division);
            int CategoryId = int.Parse(category);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            bool ParticipantP = Convert.ToBoolean(Participant);
            bool MentionatedP = Convert.ToBoolean(Mentionated);
            int CountryId = int.Parse(Country);

            int? LaboratoryId;
            int ProductId = 0;

            if (Product.Contains(","))
            {
                String Value = String.Empty;
                String cierre = ",";
                int j, i;
                String _string = Product;
                i = _string.IndexOf(_string, 0);
                if (i >= 0)
                {
                    j = _string.IndexOf(cierre, i);
                    Value = _string.Substring(i, j - i);
                    Product = Value;
                }
            }

            var divs = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();
            foreach (Divisions _divs in divs)
            {
                LaboratoryId = _divs.LaboratoryId;

                var prods = db.Products.Where(x => x.CountryId == CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == description.ToUpper().Trim()).ToList();

                if (prods.LongCount() == 0)
                {
                    Products.Active = true;
                    Products.CountryId = CountryId;
                    Products.Description = description.Trim();
                    Products.LaboratoryId = LaboratoryId;
                    Products.ProductName = Product.Trim();
                    Products.AccessId = 0;
                    Products.Register = RegisterSanitary.Trim();

                    db.Products.Add(Products);
                    db.SaveChanges();

                    var prod = db.Products.Where(x => x.CountryId == CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == description.ToUpper().Trim()).ToList();

                    foreach (Products _prod in prod)
                    {
                        ProductId = _prod.ProductId;
                    }

                    ActivityLog.CreateNewProducts(UsersId, Product, ProductId, description, RegisterSanitary, CountryId, LaboratoryId, ApplicationId);
                }


                var prodss = db.Products.Where(x => x.CountryId == CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == description.ToUpper().Trim()).ToList();

                foreach (Products _prod in prodss)
                {
                    ProductId = _prod.ProductId;
                }

                var ppf = db.ProductPharmaForms.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId).ToList();
                if (ppf.LongCount() == 0)
                {
                    ProductPharmaForms.ProductId = ProductId;
                    ProductPharmaForms.PharmaFormId = PharmaFormId;

                    db.ProductPharmaForms.Add(ProductPharmaForms);
                    db.SaveChanges();

                    ActivityLog.ProductPharmaForms(ProductId, PharmaFormId, ApplicationId, UsersId);
                }


                var dc = db.DivisionCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId).ToList();
                if (dc.LongCount() == 0)
                {
                    DivisionCategories.CategoryId = CategoryId;
                    DivisionCategories.DivisionId = DivisionId;

                    db.DivisionCategories.Add(DivisionCategories);
                    db.SaveChanges();

                    ActivityLog.DivisionCategories(DivisionId, CategoryId, ApplicationId, UsersId);
                }

                var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                if (pc.LongCount() == 0)
                {
                    ProductCategories.CategoryId = CategoryId;
                    ProductCategories.DivisionId = DivisionId;
                    ProductCategories.PharmaFormId = PharmaFormId;
                    ProductCategories.ProductId = ProductId;

                    db.ProductCategories.Add(ProductCategories);
                    db.SaveChanges();

                    ActivityLog.ProductCategories(ProductId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId);
                }

                var np = db.Newproducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId && x.EditionId == EditionId).ToList();
                if (np.LongCount() == 0)
                {
                    Newproducts.CategoryId = CategoryId;
                    Newproducts.DivisionId = DivisionId;
                    Newproducts.EditionId = EditionId;
                    Newproducts.PharmaFormId = PharmaFormId;
                    Newproducts.ProductId = ProductId;

                    db.Newproducts.Add(Newproducts);
                    db.SaveChanges();

                    ActivityLog.NewProducts(ProductId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
                }

                var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                if (edp.LongCount() == 0)
                {
                    EditionDivisionProducts.CategoryId = CategoryId;
                    EditionDivisionProducts.DivisionId = DivisionId;
                    EditionDivisionProducts.EditionId = EditionId;
                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
                    EditionDivisionProducts.ProductId = ProductId;

                    db.EditionDivisionProducts.Add(EditionDivisionProducts);
                    db.SaveChanges();

                    ActivityLog.EditionDivisionProducts(ProductId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
                }

                if (ParticipantP == true)
                {
                    var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    if (pp.LongCount() == 0)
                    {
                        ParticipantProducts.CategoryId = CategoryId;
                        ParticipantProducts.DivisionId = DivisionId;
                        ParticipantProducts.EditionId = EditionId;
                        ParticipantProducts.PharmaFormId = PharmaFormId;
                        ParticipantProducts.ProductId = ProductId;

                        DEAQ.ParticipantProducts.Add(ParticipantProducts);
                        DEAQ.SaveChanges();

                        ActivityLog.ParticipanProducts(ProductId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
                    }
                }

                if (MentionatedP == true)
                {
                    var mm = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                    if (mm.LongCount() == 0)
                    {
                        MentionatedProducts.CategoryId = CategoryId;
                        MentionatedProducts.DivisionId = DivisionId;
                        MentionatedProducts.EditionId = EditionId;
                        MentionatedProducts.PharmaFormId = PharmaFormId;
                        MentionatedProducts.ProductId = ProductId;

                        DEAQ.MentionatedProducts.Add(MentionatedProducts);
                        DEAQ.SaveChanges();

                        ActivityLog.MentionatedProducts(ProductId, PharmaFormId, DivisionId, CategoryId, ApplicationId, UsersId, EditionId);
                    }
                }


                if ((prods.LongCount() > 0) && (ppf.LongCount() > 0) && (pc.LongCount() > 0) && (edp.LongCount() > 0))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            return true;
        }
    }
}