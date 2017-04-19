using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class ActivityLog
    {
        PLMUsers dbusers = new PLMUsers();
        ActivityLogs ActivityLogs = new ActivityLogs();

        CountriesUsers c = (CountriesUsers)System.Web.HttpContext.Current.Session["CountriesUsers"];

        public void editproduct(string PName, int prodid, int alphabetid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + prodid + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + PName + "),(" + "Active," + 1 + "),(" + "ParentId,NULL" + "),(" + "AlphabetId," + alphabetid + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createBarCodes(int BarCodeId, string BarCode, int ClientId, int ProductId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "BarCodes"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("BarCodes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BarCodeId," + BarCodeId + ")");
                    ActivityLogs.FieldsAffected = ("(BarCode," + BarCode + "),(" + "Active," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createClientProductBarCodes(int BarCodeId, int ClientId, int ProductId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductBarCodes"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductBarCodes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BarCodeId," + BarCodeId + "),(ClientId," + ClientId + "),(ProductId," + ProductId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createProducts(int ProductId, string Product, int AlphabetId, int ManufactureId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + Product + "),(" + "Active," + 1 + "),(" + "ParentId,NULL" + "),(" + "AlphabetId," + AlphabetId + "),(" + "ManufactureId," + ManufactureId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createClientProducts(int ProductId, int ClientId, string Register, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(RegisterSanitary," + Register + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createDistributorProducts(int ProductId, int DistributorId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DistributorProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DistributorProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DistributorId," + DistributorId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionClientProducts(int ProductId, int EditionId, int ClientId, int? StatusId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(StatusId," + StatusId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void editEditionClientProducts(int? ProductId, int? EditionId, int? ClientId, int OperationId, bool Validation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(Validation," + Validation + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createParticipantProducts(int ProductId, int EditionId, int ClientId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ParticipantProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ParticipantProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionClientProductShots(int ProductId, int EditionId, int ClientId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProductShots"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProductShots"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionClientProductSIDEF(int ProductId, int EditionId, int ClientId, int OperationId, string ImageName)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProductSIDEF"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProductSIDEF"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");
                    if (!string.IsNullOrEmpty(ImageName))
                    {
                        ActivityLogs.FieldsAffected = ("(ImageName," + ImageName + ")");
                    }

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionBookClientProducts(int ProductId, int EditionId, int ClientId, int OperationId, string Page)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionBookClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionBookClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");
                    if (!string.IsNullOrEmpty(Page))
                    {
                        ActivityLogs.FieldsAffected = ("(Page," + Page + ")");
                    }
                    else
                    {
                        ActivityLogs.FieldsAffected = ("(Page,NULL)");
                    }


                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionBookClientProducts(int ProductId, int EditionId, int ClientId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionBookClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionBookClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _ClientProductLeafCategories(int client, int productid, int LeafCategoryId, int CategoryThreeId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductLeafCategories"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductLeafCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClienttId," + client + "),(ProductId," + productid + "),(CategoryThreeId," + CategoryThreeId + "),(LeafCategoryId," + LeafCategoryId + ")");


                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANDS          ************************ */

        public void _insertClientBrands(int BrandId, int Client, int? ClientBrandTypeId, int Edition, int? ClientTypeId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + Edition + "),(ClientId," + Client + "),(BrandId," + BrandId + "),(ClientTypeId," + ClientTypeId + ")");
                    ActivityLogs.FieldsAffected = ("(ClientBrandTypeId," + ClientBrandTypeId + "),(Active," + 1 + "),(Page," + "NULL" + "),(ExpireDescription," + "NULL" + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void EditClientBrands(int EditionId, int ClientId, int BrandId, string ExpireDescription, int OperationId, byte? ClientBrandTypeId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(BrandId," + BrandId + "),(ClientBrandTypeId," + ClientBrandTypeId + ")");
                    ActivityLogs.FieldsAffected = ("(ExpireDescription," + ExpireDescription + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createEditionClients(int ClientId, int EditionId, int ClientTypeId, string Page, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClients"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(ClientTypeId," + ClientTypeId + ")");
                    ActivityLogs.FieldsAffected = ("(Page,NULL)");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void createClientAdverts(int ClientId, int? AdvertId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientAdverts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientAdverts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AdvertId," + AdvertId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void Brands(int BrandId, string BrandName, string Logo, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Brands"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Brands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BrandId," + BrandId + ")");
                    if (!string.IsNullOrEmpty(Logo))
                    {
                        ActivityLogs.FieldsAffected = ("(BrandName," + BrandName + "),(Logo," + Logo + ")");
                    }
                    else
                    {
                        ActivityLogs.FieldsAffected = ("(BrandName," + BrandName + ")");
                    }
                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANCH          ************************ */

        public void EditBranch(int ClientId, string CompanyName, string ShortName, string Page, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(CompanyName," + CompanyName + "),(ShortName," + ShortName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void CreateBranch(int ClientId, string CompanyName, string ShortName, string Page, int CountryId, int AlphabetId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(CountryId," + CountryId + "),(AlphabetId," + AlphabetId + ")");
                    ActivityLogs.FieldsAffected = ("(CompanyName," + CompanyName + "),(ShortName," + ShortName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void AddClientImage(int ClientId, string FileName, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(Image," + FileName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADVERTS          ************************ */

        public void _Adverts(int AdvertId, string AdvertName, string Description, string AdvertFile, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Adverts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Adverts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AdvertId," + AdvertId + ")");
                    if (!string.IsNullOrEmpty(AdvertFile))
                    {
                        ActivityLogs.FieldsAffected = ("(AdvertName," + AdvertName + "),(AdvertDescription," + Description + "),(AdvertFile," + AdvertFile + "),(BaseURL,NULL),(Active,1)");
                    }
                    else
                    {
                        ActivityLogs.FieldsAffected = ("(AdvertName," + AdvertName + "),(AdvertDescription," + Description + "),(BaseURL,NULL),(Active,1)");
                    }


                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _ClientAdvertCategories(int ClientId, int AdvertId, int EditionId, int CategoryThreeId, int AdvertTypeId, string Page, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientAdvertCategories"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientAdvertCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AdvertId," + AdvertId + "),(EditionId," + EditionId + "),(CategoryThreeId," + CategoryThreeId + "),(AdvertTypeId," + AdvertTypeId + ")");
                    ActivityLogs.FieldsAffected = ("(AddedDate," + DateTime.Now + ")");


                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADDRESSES          ************************ */

        public void _insertAddresses(int AddressId, string Address, string Suburb, string City, int CountryId, string Email, string Web, string ZipCode, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Addresses"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Addresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressId + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(Address," + Address + "),(ZipCode," + ZipCode + "),(City," + City + "),(Email," + Email + "),(Web," + Web + "),(Suburb," + Suburb + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertClientAddresses(int AddressId, int ClientId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientAddresses"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientAddresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AddressId," + AddressId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _editphones(int ClientPhoneId, int ClientId, int AddressId, int PhoneTypeId, string Number, string Lada)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientPhones"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientPhones"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressId + "),(ClientPhoneId," + ClientPhoneId + "),(ClientId," + ClientId + "),(AddressId," + AddressId + ")");
                    ActivityLogs.FieldsAffected = ("(Number," + Number + "),(Lada," + Lada + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertphone(int ClientPhoneId, int AddressId, int ClientId, int PhoneTypeId, string Number, string Lada, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientPhones"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientPhones"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientPhoneId," + ClientPhoneId + "),(ClientId," + ClientId + "),(AddressId," + AddressId + ")");
                    ActivityLogs.FieldsAffected = ("(Number," + Number + "),(Lada," + Lada + "),(PhoneTypeId," + PhoneTypeId + "),(Active," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void Geolocalization(int AddressId, string Latitud, string Longitud, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Addresses"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Addresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressId + ")");
                    ActivityLogs.FieldsAffected = ("(Latitud," + Latitud + "),(Longitud," + Longitud + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               CLASIFICACIÓN          ************************ */

        public void _ProductStatus(int client, int productid, int StatusId, int Operation, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductStatus"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductStatus"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClienttId," + client + "),(ProductId," + productid + "),(StatusId," + StatusId + ")");


                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void ReassignCategories(int ClientId, int ProductId, int LeafCategoryId, int CategoryThreeId, int Operation, string NickName)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ReassignCategories"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ReassignCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClienttId," + ClientId + "),(ProductId," + ProductId + "),(LeafCategoryId," + LeafCategoryId + "),(CategoryThreeId," + CategoryThreeId + ")");
                    ActivityLogs.FieldsAffected = ("(Date," + DateTime.Now + "),(NickName," + NickName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void EditionClientProductShots(int EditionClientProductShotId, int EditionId, int ClientId, int ProductId, string FileName, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProductShots"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProductShots"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionClientProductShotId," + EditionClientProductShotId + "),(EditionId," + EditionId + "),(ClientId," + ClientId + "),(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(Date," + DateTime.Now + "),(FileName," + FileName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void EditionProductShotSizes(int EditionClientProductShotId, byte ImageSizeId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionProductShotSizes"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionProductShotSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionClientProductShotId," + EditionClientProductShotId + "),(ImageSizeId," + ImageSizeId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               HTML          ************************ */

        public void EditionAttributes(int AttributeId, int EditionId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionAttributes"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + "),(EditionId," + EditionId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void Attributes(int AttributeId, string Description, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Attributes"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Attributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + ")");
                    ActivityLogs.FieldsAffected = ("(Description," + Description + "),(Active,1)");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void EditionAttributeGroup(int? AttributeGroupId, int AttributeId, int EditionId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionAttributeGroup"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionAttributeGroup"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeGroupId," + AttributeGroupId + "),(AttributeId," + AttributeId + "),(EditionId," + EditionId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deleteproductcontents(int _editionid, int _clienid, int _productid, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Actualización de Contenido" + "),(Content," + "Actualización de Contenido" + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertproductcontents(int _editionid, int _clienid, int _productid, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Se Inserto Contenido para el Producto" + "),(Content," + "Se Inserto Contenido para el Producto" + "),(HTMLContent," + "Se Inserto Contenido para el Producto" + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _updateproductcontents(int AttributeId, int _editionid, int _clienid, int _productid, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + "),(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Actualización de Contenido" + "),(Content," + "Actualización de Contenido" + "),(HTMLContent," + "Actualización de Contenido" + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _updatecontentparticipantproducts(int edition, int client, int productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ParticipantProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ParticipantProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + edition + "),(ClienttId," + client + "),(ProductId," + productid + ")");
                    ActivityLogs.FieldsAffected = ("(HTMLContent," + "Actualización de Contenido" + "),(XMLContent," + "Actualización de Contenido" + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               INTERNATIONAL PRODUCTS          ************************ */

        public void InternationalProducts(int InternationalProductId, string InternationalProductName, int AlphabetId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "InternationalProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("InternationalProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(InternationalProductId," + InternationalProductId + ")");
                    ActivityLogs.FieldsAffected = ("(InternationalProductName," + InternationalProductName + "),(AlphabetId," + AlphabetId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void InternationalClientProducts(int InternationalProductId, int ClientId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "InternationalClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("InternationalClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(InternationalProductId," + InternationalProductId + "),(ClientId," + ClientId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void InternationalEditionClientProducts(int InternationalProductId, int ClientId, int EditionId, int Operation)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "InternationalEditionClientProducts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("InternationalEditionClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = Operation;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(InternationalProductId," + InternationalProductId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void UpdateManufacture(int ProductId, int? ManufactureId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(ManufactureId," + ManufactureId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADD CATEGORIES          ************************ */

        public void LeafCategories(int LeafCategoryId, string LeafCategory, int Level, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "LeafCategories"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("LeafCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(LeafCategoryId," + LeafCategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(LeafCategory," + LeafCategory + "),(Level," + Level + "),(Active," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void LeafHomogeneousCategories(int LeafCategoryId, int CategoryThreeId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "LeafHomogeneousCategories"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("LeafHomogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(LeafCategoryId," + LeafCategoryId + "),(CategoryThreeId," + CategoryThreeId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void CategoryThree(int CategoryThreeId, string CategoryThree, int Level, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "CategoryThree"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("CategoryThree"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryThreeId," + CategoryThreeId + ")");
                    ActivityLogs.FieldsAffected = ("(CategoryThree," + CategoryThree + "),(Level," + Level + "),(Active," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               SPECIAL PRODUCTS          ************************ */

        public void ProductAdverts(int ProductAdvertId, string ProductAdvertName, string Description, int Order, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductAdverts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductAdverts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductAdvertId," + ProductAdvertId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductAdvertName," + ProductAdvertName + "),(Description," + Description + "),(Order," + Order + "),(Active," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void ClientProductAdverts(int ClientId, int ProductAdvertId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductAdverts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductAdverts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(ProductAdvertId," + ProductAdvertId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void EditionClientProductAdverts(int ClientId, int ProductAdvertId, int EditionId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProductAdverts"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProductAdverts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(ProductAdvertId," + ProductAdvertId + "),(EditionId," + EditionId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               EDITIONCATEGORYTHREE          ************************ */

        public void EditionCategoryThree(int EditionId, int CategoryThreeId, string ProductPage, string CategoryPage, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionCategoryThree"
                        && Tables1.ApplicationId == c.ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionCategoryThree"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = c.userId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(CategoryThreeId," + CategoryThreeId + ")");
                    ActivityLogs.FieldsAffected = ("(CategoryProductPage," + ProductPage + "),(CategoryPSPage," + CategoryPage + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }
    }
}