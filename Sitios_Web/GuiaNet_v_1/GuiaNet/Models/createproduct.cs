using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class createproduct
    {
        Guia db = new Guia();
        private Guia Guia = new Guia();
        Products Products = new Products();
        ProductPharmaForms ProductPharmaForms = new ProductPharmaForms();
        ProductSubstances ProductSubstances = new ProductSubstances();
        EditionClientMedicalProducts EditionClientMedicalProducts = new EditionClientMedicalProducts();
        EditionClients EditionClients = new EditionClients();
        ClientProducts ClientProducts = new ClientProducts();
        ActivityLog ActivityLog = new ActivityLog();
        ClientProductSubstances ClientProductSubstances = new ClientProductSubstances();
        ClientProductBarCodes ClientProductBarCodes = new ClientProductBarCodes();
        ClientMedicalProductsBarCode ClientMedicalProductsBarCode = new ClientMedicalProductsBarCode();

        public bool insertnewproduct(string ProductN, string pharmaformid, string presentationid, string activesubstanceid, string editionid, string clientid, int ApplicationId, int UsersId, int BarCodeId, string registers)
        {
            int productid = 0;
            int _pharmaformid = int.Parse(pharmaformid);
            int _presentationid = int.Parse(presentationid);
            int _activesubstanceid = int.Parse(activesubstanceid);
            int _edition = int.Parse(editionid);
            int _clientid = int.Parse(clientid);

            byte clienttypeid = 0;
            byte typeid = 0;
            byte alphabetid = 0;

            var pt = (from ptypes in db.ProductTypes
                      where ptypes.Description == "Medicamentos Hospitalarios"
                      select ptypes).ToList();
            foreach (ProductTypes _pt in pt)
            {
                typeid = _pt.TypeId;
            }
            var prods = (from product in db.Products
                         where product.ProductName == ProductN.Trim()
                         && product.TypeId == typeid
                         select product).ToList();
            if (prods.LongCount() == 0)
            {
                var product = ProductN.Substring(0, 1);

                var alph = (from leters in db.Alphabet
                            where leters.Letter == product
                            select leters).ToList();
                foreach (Alphabet al in alph)
                {
                    Products.Active = true;
                    Products.ParentId = null;
                    Products.ProductName = ProductN.Trim();
                    Products.TypeId = typeid;
                    Products.AlphabetId = al.AlphabetId;
                    alphabetid = al.AlphabetId;
                }
                db.Products.Add(Products);
                db.SaveChanges();

                var products = (from prod in db.Products
                                where prod.ProductName == ProductN.Trim()
                                && prod.TypeId == typeid
                                select prod).ToList();
                foreach (Products p in products)
                {
                    productid = p.ProductId;
                }

                ActivityLog.createproduct(ProductN, productid, ApplicationId, UsersId, alphabetid, typeid);
            }

            var productss = (from prod in db.Products
                             where prod.ProductName == ProductN.Trim()
                             && prod.TypeId == typeid
                             select prod).ToList();
            foreach (Products p in productss)
            {
                productid = p.ProductId;
            }

            var ppf = (from productpf in db.ProductPharmaForms
                       where productpf.ProductId == productid
                       && productpf.PharmaFormId == _pharmaformid
                       select productpf).ToList();
            if (ppf.LongCount() == 0)
            {
                ProductPharmaForms.ProductId = productid;
                ProductPharmaForms.PharmaFormId = _pharmaformid;

                db.ProductPharmaForms.Add(ProductPharmaForms);
                db.SaveChanges();

                ActivityLog._insertproductpharmaforms(productid, _pharmaformid, ApplicationId, UsersId);
            }

            var ps = (from productsubs in db.ProductSubstances
                      where productsubs.ProductId == productid
                      && productsubs.PharmaFormId == _pharmaformid
                      && productsubs.ActiveSubstanceId == _activesubstanceid
                      && productsubs.PresentationId == _presentationid
                      select productsubs).ToList();
            if (ps.LongCount() == 0)
            {
                ProductSubstances.ProductId = productid;
                ProductSubstances.PharmaFormId = _pharmaformid;
                ProductSubstances.ActiveSubstanceId = _activesubstanceid;
                ProductSubstances.PresentationId = _presentationid;

                db.ProductSubstances.Add(ProductSubstances);
                db.SaveChanges();

                ActivityLog._insertproductsubstances(productid, _pharmaformid, _activesubstanceid, _presentationid, ApplicationId, UsersId);
            }

            var ec = (from editionc in db.EditionClients
                      where editionc.ClientId == _clientid
                      && editionc.EditionId == _edition
                      select editionc).ToList();
            if (ec.LongCount() == 0)
            {
                EditionClients.ClientId = _clientid;
                EditionClients.EditionId = _edition;

                db.EditionClients.Add(EditionClients);
                db.SaveChanges();


                var ct = from client in db.ClientTypes
                         where client.TypeName == "ANUNCIANTE"
                         select client;
                foreach (ClientTypes cct in ct)
                {
                    clienttypeid = cct.ClientTypeId;
                }
                ActivityLog._inserteditionclients(_clientid, _edition, ApplicationId, UsersId, clienttypeid);
            }

            var cpp = (from _cp in db.ClientProducts
                       where _cp.ClientId == _clientid
                       && _cp.ProductId == productid
                       select _cp).ToList();
            if (cpp.LongCount() == 0)
            {
                ClientProducts.ClientId = _clientid;
                ClientProducts.ProductId = productid;

                db.ClientProducts.Add(ClientProducts);
                db.SaveChanges();
            }

            var cps = (from clientps in db.ClientProductSubstances
                       where clientps.ActiveSubstanceId == _activesubstanceid
                       && clientps.ClientId == _clientid
                       && clientps.PharmaFormId == _pharmaformid
                       && clientps.PresentationId == _presentationid
                       && clientps.ProductId == productid
                       select clientps).ToList();
            if (cps.LongCount() == 0)
            {
                try
                {
                    ClientProductSubstances = new Models.ClientProductSubstances();

                    ClientProductSubstances.ActiveSubstanceId = _activesubstanceid;
                    ClientProductSubstances.ClientId = _clientid;
                    ClientProductSubstances.PharmaFormId = _pharmaformid;
                    ClientProductSubstances.PresentationId = _presentationid;
                    ClientProductSubstances.ProductId = productid;
                    if(registers != string.Empty)
                    {
                        ClientProductSubstances.RegisterSanitary = registers;
                    }                   
                    db.ClientProductSubstances.Add(ClientProductSubstances);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    String msg = e.Message;
                }


                ActivityLog._insertclientproductsubstances(_activesubstanceid, _clientid, _pharmaformid, _presentationid, productid, ApplicationId, UsersId);
            }



            var cpbc = new List<ClientMedicalProductsBarCode>();
            if ((BarCodeId != 0))
            {
                cpbc = (from cpb in db.ClientMedicalProductsBarCode
                        where cpb.ClientId == _clientid
                        && cpb.ProductId == productid
                        && cpb.BarCodeId == BarCodeId
                        && cpb.ActiveSubstanceId == _activesubstanceid
                        && cpb.PharmaFormId == _pharmaformid
                        && cpb.PresentationId == _presentationid
                        select cpb).ToList();
                if (cpbc.LongCount() == 0)
                {
                    ClientMedicalProductsBarCode.BarCodeId = BarCodeId;
                    ClientMedicalProductsBarCode.ClientId = _clientid;
                    ClientMedicalProductsBarCode.ProductId = productid;
                    ClientMedicalProductsBarCode.ActiveSubstanceId = _activesubstanceid;
                    ClientMedicalProductsBarCode.PresentationId = _presentationid;
                    ClientMedicalProductsBarCode.PharmaFormId = _pharmaformid;

                    db.ClientMedicalProductsBarCode.Add(ClientMedicalProductsBarCode);
                    db.SaveChanges();
                }
            }


            var ecmp = (from editioncmp in db.EditionClientMedicalProducts
                        where editioncmp.EditionId == _edition
                        && editioncmp.ClientId == _clientid
                        && editioncmp.ProductId == productid
                        && editioncmp.PharmaFormId == _pharmaformid
                        && editioncmp.ActiveSubstanceId == _activesubstanceid
                        && editioncmp.PresentationId == _presentationid
                        select editioncmp).ToList();
            if (ecmp.LongCount() == 0)
            {
                var ct = from client in db.ClientTypes
                         where client.TypeName == "ANUNCIANTE"
                         select client;
                foreach (ClientTypes cct in ct)
                {
                    clienttypeid = cct.ClientTypeId;
                }
                EditionClientMedicalProducts.EditionId = _edition;
                EditionClientMedicalProducts.ClientId = _clientid;
                EditionClientMedicalProducts.ProductId = productid;
                EditionClientMedicalProducts.PharmaFormId = _pharmaformid;
                EditionClientMedicalProducts.ActiveSubstanceId = _activesubstanceid;
                EditionClientMedicalProducts.PresentationId = _presentationid;

                db.EditionClientMedicalProducts.Add(EditionClientMedicalProducts);
                db.SaveChanges();

                ActivityLog._inserteditionclientmedicalproducts(_edition, _clientid, productid, _pharmaformid, _activesubstanceid, _presentationid, ApplicationId, UsersId);
            }
            if ((prods.LongCount() > 0) && (ppf.LongCount() > 0) && (ps.LongCount() > 0) && (ecmp.LongCount() > 0) && (cps.LongCount() > 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}