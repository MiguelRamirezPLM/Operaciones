using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GuiaNet.Models
{
    public class createproductwithcontent
    {
        Guia db = new Guia();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        ClientProducts ClientProducts = new ClientProducts();
        ActivityLog ActivityLog = new ActivityLog();
        EditionClients EditionClients = new EditionClients();
        ClientProductBarCodes ClientProductBarCodes = new ClientProductBarCodes();
        EditionClientProducts EditionClientProducts = new EditionClientProducts();
        ClientProductCategories ClientProductCategories = new ClientProductCategories();

        public bool createproduct(string ProductN, int edition, int client, int ApplicationId, int UsersId, string registers, int BarCodeId)
        {
            Products Products = new Products();
            int productid = 0;
            byte alphabetid = 0;

            var product = ProductN.Substring(0, 1);
            var alph = (from leters in db.Alphabet
                        where leters.Letter == product
                        select leters).ToList();
            foreach (Alphabet al in alph)
            {
                alphabetid = al.AlphabetId;
            }

            byte typeid = 0;

            var pt = (from ptypes in db.ProductTypes
                      where ptypes.Description == "Productos"
                      select ptypes).ToList();
            foreach (ProductTypes _pt in pt)
            {
                typeid = _pt.TypeId;
            }

            var products = (from prod in db.Products
                            where prod.ProductName == ProductN.Trim()
                            && prod.TypeId == typeid
                            select prod).ToList();
            if (products.LongCount() == 0)
            {
                Products.Active = true;
                Products.ParentId = null;
                Products.ProductId_Anterior = null;
                Products.ProductName = ProductN.Trim();
                Products.TypeId = typeid;
                Products.AlphabetId = alphabetid;

                db.Products.Add(Products);
                db.SaveChanges();



                var productss = (from prod in db.Products
                                 where prod.ProductName == ProductN.Trim()
                                  && prod.TypeId == typeid
                                 select prod).ToList();
                foreach (Products p in productss)
                {
                    productid = p.ProductId;
                }

                ActivityLog.createproductwithcontent(ProductN, productid, ApplicationId, UsersId, alphabetid, typeid);
            }
            else
            {
                var pproducts = (from prod in db.Products
                                 where prod.ProductName == ProductN.Trim()
                                  && prod.TypeId == typeid
                                 select prod).ToList();
                foreach (Products p in pproducts)
                {
                    productid = p.ProductId;
                }

            }

            byte clienttypeid = 0;
            var ct = from clientt in db.ClientTypes
                     where clientt.TypeName == "ANUNCIANTE"
                     select clientt;
            foreach (ClientTypes cct in ct)
            {
                clienttypeid = cct.ClientTypeId;
            }

            var ec = (from editionc in db.EditionClients
                      where editionc.ClientId == client
                      && editionc.EditionId == edition
                      select editionc).ToList();
            if (ec.LongCount() == 0)
            {
                EditionClients.ClientId = client;
                EditionClients.EditionId = edition;
                EditionClients.ClientTypeId = clienttypeid;

                db.EditionClients.Add(EditionClients);

                db.SaveChanges();

                ActivityLog._inserteditionclients(client, edition, ApplicationId, UsersId, clienttypeid);
            }

            var cp = (from cprods in db.ClientProducts
                      where cprods.ClientId == client
                      && cprods.ProductId == productid
                      select cprods).ToList();
            if (cp.LongCount() == 0)
            {
                ClientProducts.ClientId = client;
                ClientProducts.ProductId = productid;
                ClientProducts.RegisterSanitary = registers;

                db.ClientProducts.Add(ClientProducts);
                db.SaveChanges();

                ActivityLog._insertclientproducts(client, productid, ApplicationId, UsersId);
            }

            var cpbc = new List<ClientProductBarCodes>();
            if ((BarCodeId != 0))
            {
                cpbc = (from cpb in db.ClientProductBarCodes
                        where cpb.ClientId == client
                        && cpb.ProductId == productid
                        && cpb.BarCodeId == BarCodeId
                        select cpb).ToList();
                if (cpbc.LongCount() == 0)
                {
                    ClientProductBarCodes.BarCodeId = BarCodeId;
                    ClientProductBarCodes.ClientId = client;
                    ClientProductBarCodes.ProductId = productid;

                    db.ClientProductBarCodes.Add(ClientProductBarCodes);
                    db.SaveChanges();

                    ActivityLog._insertClientProductBarCodes(client, BarCodeId, productid, ApplicationId, UsersId);
                }
            }
            var pp = (from partprod in db.ParticipantProducts
                      where partprod.ProductId == productid
                      && partprod.ClientId == client
                      && partprod.EditionId == edition
                      select partprod).ToList();
            if (pp.LongCount() == 0)
            {
                ParticipantProducts.ClientId = client;
                ParticipantProducts.EditionId = edition;
                ParticipantProducts.HTMLContent = null;
                ParticipantProducts.XMLContent = null;
                ParticipantProducts.FileName = null;
                ParticipantProducts.ProductId = productid;

                db.ParticipantProducts.Add(ParticipantProducts);
                db.SaveChanges();

                ActivityLog._insertparticipantproducts(edition, client, productid, ApplicationId, UsersId);

                return true;
            }
            if ((pp.LongCount() > 0) && (cp.LongCount() > 0) && (ec.LongCount() > 0) && (products.LongCount() > 0))
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