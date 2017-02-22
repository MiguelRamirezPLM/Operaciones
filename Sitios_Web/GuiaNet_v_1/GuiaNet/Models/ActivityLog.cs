using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class ActivityLog
    {
        public PLMUsers dbusers = new PLMUsers();
        private PLMUsers USERS = new PLMUsers();
        ActivityLogs ActivityLogs = new ActivityLogs();

        public void createcategory(string categoryname, int ApplicationId, int UsersId, int categoryid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Categories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Categories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + categoryid + ")");
                    ActivityLogs.FieldsAffected = ("(CategoryName," + categoryname + "),(" + "Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void editproduct(string PName, int prodid, int ApplicationId, int UsersId, byte alphabetid, byte typeid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + prodid + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + PName + "),(" + "Active," + 1 + "),(" + "ParentId,NULL" + "),(" + "TypeId," + typeid + "),(" + "AlphabetId," + alphabetid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void createproductwithcontent(string ProductN, int productid, int ApplicationId, int UsersId, byte alphabetid, byte typeid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + productid + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + ProductN + "),(" + "Active," + 1 + "),(" + "ParentId,NULL" + "),(" + "TypeId," + typeid + "),(" + "AlphabetId," + alphabetid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclientproducts(int client, int productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClienttId," + client + "),(ProductId," + productid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertparticipantproducts(int edition, int client, int productid, int ApplicationId, int UsersId)
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
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + edition + "),(ClienttId," + client + "),(ProductId," + productid + ")");
                    ActivityLogs.FieldsAffected = ("(HTMLContent," + "NULL" + "),(XMLContent," + "NULL" + "),(FileName," + "NULL" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteparticipantproducts(int edition, int client, int productid, int ApplicationId, int UsersId)
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
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + edition + "),(ClienttId," + client + "),(ProductId," + productid + ")");
                    ActivityLogs.FieldsAffected = ("(HTMLContent," + "NULL" + "),(XMLContent," + "NULL" + "),(FileName," + "NULL" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclientcategories(int Id, int _clientid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteclientcategories(int categoryid, int _clientid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(CategoryId," + categoryid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void createproduct(string ProductN, int productid, int ApplicationId, int UsersId, byte alphabetid, byte typeid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Products"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Products"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + productid + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + ProductN + "),(" + "Active," + 1 + "),(" + "ParentId,NULL" + "),(" + "TypeId," + typeid + "),(" + "AlphabetId," + alphabetid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertproductpharmaforms(int productid, int _pharmaformid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductPharmaForms"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductPharmaForms"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + productid + "),(PharmaFormId," + _pharmaformid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertproductsubstances(int productid, int _pharmaformid, int _activesubstanceid, int _presentationid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductSubstances"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductSubstances"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + productid + "),(PharmaFormId," + _pharmaformid + "),(ActiveSusbtanceId," + _activesubstanceid + "),(PresentationId," + _presentationid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclients(int _clientid, int _edition, int ApplicationId, int UsersId, byte clienttypeid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _edition + "),(ClientId," + _clientid + ")");
                    ActivityLogs.FieldsAffected = ("(ClientTypeId," + clienttypeid + "),(Page," + "NULL" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclientmedicalproducts(int _edition, int _clientid, int productid, int _pharmaformid, int _activesubstanceid, int _presentationid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientMedicalProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientMedicalProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _edition + "),(ClientId," + _clientid + "),(ProductId," + productid + "),(PharmaFormId," + _pharmaformid + "),(ActiveSubstanceId," + _activesubstanceid + "),(PresentationId," + _presentationid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertparticipantmedicalproducts(int client, int edition, int product, int pharmaform, int activesubstance, int presentation, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientMedicalProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientMedicalProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + edition + "),(ClienttId," + client + "),(ProductId," + product + "),(PharmaFormId," + pharmaform + "),(ActiveSubstanceId," + activesubstance + "),(PresentationId," + presentation + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteparticipantmedicalproducts(int client, int edition, int product, int pharmaform, int activesubstance, int presentation, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientMedicalProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientMedicalProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + edition + "),(ClienttId," + client + "),(ProductId," + product + "),(PharmaFormId," + pharmaform + "),(ActiveSubstanceId," + activesubstance + "),(PresentationId," + presentation + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclientcategories(int _clientid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + editionid + "),(ClienttId," + _clientid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteditionclientcategories(int _clientid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditiontId," + editionid + "),(ClienttId," + _clientid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclientproductsubstances(int _activesubstanceid, int _clientid, int _pharmaformid, int _presentationid, int productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductSubstances"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductSubstances"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + productid + "),(PharmaFormId," + _pharmaformid + "),(ActiveSubstanceId," + _activesubstanceid + "),(PresentationId," + _presentationid + "),(ClientId," + _clientid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclientproductcategories(int _clientid, int _productid, int Id, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(ProductId," + _productid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclientproducts(int _clientid, int _productid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(ProductId," + _productid + "),(CategoryId," + Id + "),(EditionId," + editionid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteclientproductcategories(int _clientid, int _productid, int Id, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(ProductId," + _productid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteeditionclientproducts(int _clientid, int _productid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(ProductId," + _productid + "),(CategoryId," + Id + "),(EditionId," + editionid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertMatchCategories(int categoryid, int Id, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "MatchCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("MatchCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + Id + "),(CategoryId," + categoryid + "),(AddedDate," + DateTime.Now + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateHeterogeneousCategories(int HeterogeneousCategoryId, int? ParentId, string Description, int Level, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "HeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("HeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + HeterogeneousCategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ParentId," + ParentId + "),(Description," + Description + "),(Level," + Level + "),(Active," + 0 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateHeterogeneousCategoriesactive(int HeterogeneousCategoryId, int? ParentId, string Description, int Level, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "HeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("HeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + HeterogeneousCategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ParentId," + ParentId + "),(Description," + Description + "),(Level," + Level + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertClientProductCategories(int categoryid, int ClientId, int ProductId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(ProductId," + ProductId + "),(CategoryId," + categoryid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }


        /*---------------------------   Recategorización--------------------------------------*/

        public void _insertclientheterogeneouscategories(int Id, int _clientid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientHeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientHeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteclientheterogeneouscategories(int Id, int _clientid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientHeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientHeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + _clientid + "),(CategoryId," + Id + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclientheterogeneouscategories(int _clientid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientHeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientHeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + Id + "),(ClienttId," + _clientid + "),(EditiontId," + editionid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteditionclientheterogeneouscategories(int _clientid, int Id, int editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientHeterogeneousCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientHeterogeneousCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + Id + "),(ClienttId," + _clientid + "),(EditiontId," + editionid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteMatchCategories(int categoryid, int Id, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "MatchCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("MatchCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(HeterogeneousCategoryId," + Id + "),(CategoryId," + categoryid + "),(AddedDate," + DateTime.Now + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertcategorylevel1(int Categoryid, string valuelevel, int levell, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Categories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Categories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + Categoryid + ")");
                    ActivityLogs.FieldsAffected = ("(ParentId," + "NULL" + "),(Description," + valuelevel + "),(Active," + 1 + "),(Level," + levell + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertcategorylevel(int Categoryid, int? parentid, string valuelevel, int levell, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Categories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Categories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + Categoryid + ")");
                    ActivityLogs.FieldsAffected = ("(ParentId," + parentid + "),(Description," + valuelevel + "),(Active," + 1 + "),(Level," + levell + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }


        /*---------------------  Nuevas Tablas en Base      ----------------------   */

        public void _editbarcode(int BarCodeIdd, string BarCode, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "BarCodes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("BarCodes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BarCodeId," + BarCodeIdd + ")");
                    ActivityLogs.FieldsAffected = ("(BarCode," + BarCode + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertbarcode(int BarCodeIdd, string barcode, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "BarCodes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("BarCodes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BarCodeId," + BarCodeIdd + ")");
                    ActivityLogs.FieldsAffected = ("(BarCode," + barcode + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertClientProductBarCodes(int ClientId, int BarCdId, int prodid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductBarCodes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductBarCodes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(BarCodeId," + BarCdId + "),(ProductId," + prodid + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }


        /*--------------------  Production      ------------------------------------*/

        public void _insertproductcontents(int _editionid, int _clienid, int _productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Se Inserto Contenido para el Producto" + "),(Content," + "Se Inserto Contenido para el Producto" + "),(HTMLContent," + "Se Inserto Contenido para el Producto" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteproductcontents(int _editionid, int _clienid, int _productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Actualización de Contenido" + "),(Content," + "Actualización de Contenido" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateproductcontents(int AttributeId, String content, String plainContent, int _editionid, int _clienid, int _productid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductContents"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductContents"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + "),(EditionId," + _editionid + "),(ClientId," + _clienid + "),(ProductId," + _productid + ")");
                    ActivityLogs.FieldsAffected = ("(PlainContent," + "Actualización de Contenido" + "),(Content," + "Actualización de Contenido" + "),(HTMLContent," + "Actualización de Contenido" + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionattributes(int AttributeId, int _editionid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionAttributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + "),(EditionId," + _editionid + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertattributes(int AttributeId, String AttributeName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Attributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Attributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AttributeId," + AttributeId + ")");
                    ActivityLogs.FieldsAffected = ("(Description," + AttributeName + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteeditionattributesgroup(int EditionId, int AttributeId, int AttributeGroupId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionAttributeGroup"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionAttributeGroup"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionattributesgroup(int EditionId, int AttributeId, int AttributeGroupId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionAttributeGroup"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionAttributeGroup"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
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

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updatepagesbyclient(int ClientId, int EditionId, int ClientTypeId, string Page, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(ClientTypeId," + ClientTypeId + ")");
                    ActivityLogs.FieldsAffected = ("(Page," + Page + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        /*------------------    Especialidades, Marcas y Sucursales     ----------------------------*/

        public void _insertEditionClientSpecialities(int Client, int Edition, int SpecialityId, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientSpecialities"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientSpecialities"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + Edition + "),(ClientId," + Client + "),(SpecialityId," + SpecialityId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteEditionClientSpecialities(int Client, int Edition, int SpecialityId, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientSpecialities"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientSpecialities"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + Edition + "),(ClientId," + Client + "),(SpecialityId," + SpecialityId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertClientBrands(int BrandId, int Client, int? ClientBrandTypeId, int Edition, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientBrandTypeId," + ClientBrandTypeId + "),(Active," + 1 + "),(Page," + "NULL" + "),(ExpireDescription," + "NULL" + "),(EditionId," + Edition + "),(ClientId," + Client + "),(BrandId," + BrandId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteClientBrands(int BrandId, int Client, int ClientBrandTypeId, int Edition, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientBrandTypeId," + ClientBrandTypeId + "),(Active," + 1 + "),(Page," + "NULL" + "),(ExpireDescription," + "NULL" + "),(EditionId," + Edition + "),(ClientId," + Client + "),(BrandId," + BrandId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateClientBrands(int BrandId, int Client, byte? ClientBrandTypeId, int Edition, byte? ClientTypeId, string ExpireDescription, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientBrandTypeId," + ClientBrandTypeId + "),(Active," + 1 + "),(Page," + "NULL" + "),(ExpireDescription," + "NULL" + "),(EditionId," + Edition + "),(ClientId," + Client + "),(BrandId," + BrandId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _inserteditionclient(int ClientId, int EditionId, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(Page," + "NULL" + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteeditionclient(int ClientId, int EditionId, int ClientTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(Page," + "NULL" + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclient(int ClientIdSuc, int ClientId, string ClientCode, string BranchName, string ShortName, string Description, byte CountryId, byte alphabetid, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AlphabetId," + alphabetid + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(ClientIdParent," + ClientIdSuc + "),(ClientCode," + ClientCode + "),(CompanyName," + BranchName + "),(ShortName," + ShortName + "),");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _editclient(int ClientId, int CountryId, string CompanyName, string ShortName, int ApplicationId, int UsersId, int alphabetid)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AlphabetId," + alphabetid + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(CompanyName," + CompanyName + "),(ShortName," + ShortName + "),");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _editinformation(int AddressIdd, string Address, string Suburb, string City, string Email, string Web, string ZipC, int CountryId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Addresses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Addresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressIdd + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(Address," + Address + "),(ZipCode," + ZipC + "),(City," + City + "),(Email," + Email + "),(Web," + Web + "),(Suburb," + Suburb + "),");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertAddresses(int AddressId, string Address, string Suburb, string City, int CountryId, string Email, string Web, string ZipCode, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Addresses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Addresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressId + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(Address," + Address + "),(ZipCode," + ZipCode + "),(City," + City + "),(Email," + Email + "),(Web," + Web + "),(Suburb," + Suburb + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertClientAddresses(int AddressId, int ClientId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientAddresses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientAddresses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(AddressId," + AddressId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _editphones(int ClientPhoneId, int ClientId, int AddressId, int PhoneTypeId, string Number, string Lada, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientPhones"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientPhones"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AddressId," + AddressId + "),(ClientPhoneId," + ClientPhoneId + "),(ClientId," + ClientId + "),(AddressId," + AddressId + ")");
                    ActivityLogs.FieldsAffected = ("(Number," + Number + "),(Lada," + Lada + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertphone(int ClientPhoneId, int AddressId, int ClientId, int PhoneTypeId, string Number, string Lada, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientPhones"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientPhones"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientPhoneId," + ClientPhoneId + "),(ClientId," + ClientId + "),(AddressId," + AddressId + ")");
                    ActivityLogs.FieldsAffected = ("(Number," + Number + "),(Lada," + Lada + "),(PhoneTypeId," + PhoneTypeId + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateactiveclient(int ClientId, bool Active, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(Active," + Active + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateeditionclientspecialities(int ClientId, int EditionId, int SpecialityId, int ApplicationId, int UsersId, string Advers, string Quantity)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionClientSpecialities"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionClientSpecialities"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + "),(EditionId," + EditionId + "),(SpecialityId," + SpecialityId + ")");
                    ActivityLogs.FieldsAffected = ("(AdversDescription," + Advers + "),(Quantity," + Quantity + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertclientbrandtypeRep(int BrandId, int ClientId, int EditionId, byte ClientBrandTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(BrandId," + BrandId + ")");
                    ActivityLogs.FieldsAffected = ("(ClientBrandTypeId," + ClientBrandTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void deleteclientbrandtype(int BrandId, int ClientId, int EditionId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientBrands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientBrands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ClientId," + ClientId + "),(BrandId," + BrandId + ")");
                    ActivityLogs.FieldsAffected = ("(ClientBrandTypeId," + null + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertBrandImageSizes(int BrandId, int SizeId, string FileName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "BrandImageSizes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("BrandImageSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BrandId," + BrandId + "),(SizeId," + SizeId + "),(FileName," + FileName + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updatebrandslogo(int BrandId, string FileName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Brands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Brands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BrandId," + BrandId + ")");
                    ActivityLogs.FieldsAffected = ("(Logo," + FileName + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteBrandImageSizes(int BrandId, int SizeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "BrandImageSizes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("BrandImageSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BrandId," + BrandId + "),(SizeId," + SizeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _editBrandName(int BrandId, string BrandNames, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Brands"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Brands"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(BrandId," + BrandId + ")");
                    ActivityLogs.FieldsAffected = ("(BrandName," + BrandNames + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _editclients(int ClientId, string CompanyName, string ShortName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Clients"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Clients"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClientId," + ClientId + ")");
                    ActivityLogs.FieldsAffected = ("(CompanyName," + CompanyName + "),(ShortName," + ShortName + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteEditionSpecialityClientAdvers(int Client, int Edition, int SpecialityId, int ClientTypeId, int AdvertId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionSpecialityClientAdvers"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionSpecialityClientAdvers"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + Edition + "),(ClientId," + Client + "),(SpecialityId," + SpecialityId + "),(AdvertId," + AdvertId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }



        /*                                                              ANUNCIOS                                                */

        public void _insertadvertisements(int AdvertIds, string FileName, byte AdvertTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Advertisements"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Advertisements"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AdvertId," + AdvertIds + ")");
                    ActivityLogs.FieldsAffected = ("(AdvertName," + FileName + "),(AdvertTypeId," + AdvertTypeId + "),(Active," + 1 + "),(Order," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertEditionSpecialityClientAdvers(int Client, int Edition, int SpecialityId, int ClientTypeId, int AdvertId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionSpecialityClientAdvers"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionSpecialityClientAdvers"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + Edition + "),(ClientId," + Client + "),(SpecialityId," + SpecialityId + "),(AdvertId," + AdvertId + "),(ClientTypeId," + ClientTypeId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteadvertisements(int AdvertIds, string FileName, byte AdvertTypeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Advertisements"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Advertisements"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AdvertId," + AdvertIds + ")");
                    ActivityLogs.FieldsAffected = ("(AdvertName," + FileName + "),(AdvertTypeId," + AdvertTypeId + "),(Active," + 1 + "),(Order," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

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


                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _ClientProductLeafCategories(int client, int productid, int HomogeneousCategoryId, int LeafCategoryId, int OperationId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ClientProductLeafCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClientProductLeafCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClienttId," + client + "),(ProductId," + productid + "),(HomogeneousCategoryId," + HomogeneousCategoryId + "),(LeafCategoryId," + LeafCategoryId + ")");


                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }
    }
}