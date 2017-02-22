using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class ActivityLog
    {
        public PLMUsers dbusers = new PLMUsers();
        private PLMUsers USERS = new PLMUsers();
        ActivityLogs ActivityLogs = new ActivityLogs();

        public void ProductPharmaForms(int ProdId, int PharmaFormId, int ApplicationId, int UsersId)
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

                    var Product = Convert.ToString(ProdId);
                    var PForm = Convert.ToString(PharmaFormId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + Product + "),(" + "PharmaFormId," + PForm + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void CreateNewProducts(int UsersId, string ProductName, int ProductId, string Descript, string Registry, int CountryId, int? LabId, int ApplicationId)
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
                     ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + ")");
                     ActivityLogs.FieldsAffected = ("(ProductName," + ProductName + "),(" + "Description," + Descript + "),(" + "Register," + Registry + "),(" + "CountryId," + CountryId + "),(" + "LaboratoryId," + LabId + "),(" + "Active," + 1 + ")");

                     USERS.ActivityLogs.Add(ActivityLogs);
                     USERS.SaveChanges();
                 }
             }
        }

        public void DivisionCategories(int Division, int CategoryId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(" + "CategoryId," + Pharma + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void ProductCategories(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductCategories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductCategories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void EditionDivisionProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionDivisionProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionDivisionProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteDivisionProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionDivisionProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionDivisionProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void NewProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "NewProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("NewProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void ParticipanProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
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

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void MentionatedProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "MentionatedProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("MentionatedProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteParticipanProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
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

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteMentionatedProducts(int ProdId, int PharmaFormId, int Division, int CategoryId, int ApplicationId, int UsersId, int Edition)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "MentionatedProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("MentionatedProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    var DivisionId = Convert.ToString(Division);
                    var Pharma = Convert.ToString(CategoryId);
                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void EditProducts(int ApplicationId, int UsersId, string ProductName, string Descript, string Registry, string ProductId, int? Laboratory, int country)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductName," + ProductName + "),(" + "Description," + Descript + "),(" + "Register," + Registry + "),(" + "CountryId," + country + "),(" + "LaboratoryId," + Laboratory + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void EditParticipantProducts(int ApplicationId, int Product, int Division, int Edition, int PharmaFormId, int CategoryId, int ContentType, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + Product + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");
                    ActivityLogs.FieldsAffected = ("(ContentType," + ContentType + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void EditParticipantProductsDeleteChanges(int ApplicationId, int Product, int Division, int Edition, int PharmaFormId, int CategoryId, int ContentType, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + Product + "),(" + "PharmaFormId," + PharmaFormId + "),(" + "DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "EditionId," + Edition + ")");
                    ActivityLogs.FieldsAffected = ("(ContentType,NULL)");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void ProductSubstances(int ApplicationId, int ActiveSubstance, int ProdId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "ActiveSubstanceId," + ActiveSubstance + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();

                }
            }
        }

        public void DeleteProductSubstances(int ApplicationId, int ActiveSubstance, int ProdId, int UsersId)
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
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "ActiveSubstanceId," + ActiveSubstance + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();

                }
            }
        }

        public void InsertProductCrops(int ApplicationId, int Crop, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductCrops"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductCrops"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "CropId," + Crop + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteProductCrops(int ApplicationId, int Crop, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductCrops"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductCrops"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProdId + "),(" + "CropId," + Crop + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void InsertProductAgrochemicalUses(int ApplicationId, int Use, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductAgrochemicalUses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductAgrochemicalUses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AgrochemicalUseId," + ProdId + "),(" + "ProductId," + Use + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteProductAgrochemicalUses(int ApplicationId, int Use, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductAgrochemicalUses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductAgrochemicalUses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(AgrochemicalUseId," + ProdId + "),(" + "ProductId," + Use + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void InsertProductSeeds(int ApplicationId, int Seed, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductSeeds"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductSeeds"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(SeedId," + Seed + "),(" + "ProductId," + ProdId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void DeleteProductSeeds(int ApplicationId, int Seed, int ProdId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductSeeds"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductSeeds"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(SeedId," + Seed + "),(" + "ProductId," + ProdId + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void UpdateRegistry(int ApplicationId, int Product, int UsersId, string Registry)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + Product + ")");
                    ActivityLogs.FieldsAffected = ("(" + "Register," + Registry + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void Updatepages(int EditionId, int PharmaFormId, int ProdId, int CategoryId, int ApplicationId, int UsersId, string Page, int Division)
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
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + ")");
                    ActivityLogs.FieldsAffected = ("(" + "Page," + Page + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void Updatepagesn(int EditionId, int PharmaFormId, int ProdId, int CategoryId, int ApplicationId, int UsersId, string Page, int Division)
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
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + Division + "),(" + "CategoryId," + CategoryId + "),(" + "ProductId," + ProdId + "),(" + "PharmaFormId," + PharmaFormId + ")");
                    ActivityLogs.FieldsAffected = ("(" + "Page,NULL)");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void createlab(string LabName, int ApplicationId, int UsersId, int LaboratoryId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Laboratories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Laboratories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(LaboratoryId," + LaboratoryId + ")");
                    ActivityLogs.FieldsAffected = ("(LaboratoryName," + LabName + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void updatelab(string LaboratoryN, int ApplicationId, int UsersId, int LaboratoryId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Laboratories"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Laboratories"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(LaboratoryId," + LaboratoryId + ")");
                    ActivityLogs.FieldsAffected = ("(LaboratoryName," + LaboratoryN + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void createdivision(string DivisionName, string ShortN, int CountryId, int LabId, int ApplicationId, int UsersId, int DivisionId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Divisions"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Divisions"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(LaboratoryId," + LabId + "),(CountryId," + CountryId + ")");
                    ActivityLogs.FieldsAffected = ("(DivisionName," + DivisionName + "),(ShortName," + ShortN + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void editdivision(string DivisionN, string ShortN, int Country, int LaboratoryId, int ApplicationId, int UsersId, int Division)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "Divisions"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("Divisions"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + Division + "),(LaboratoryId," + LaboratoryId + "),(CountryId," + Country + ")");
                    ActivityLogs.FieldsAffected = ("(DivisionName," + DivisionN + "),(ShortName," + ShortN + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void editivisioninformation(string Address, string Suburb, string Location, string ZipCode, string Telephone, string Lada, string Fax,
            string Web, string City, string State, string Email, int Division, int DivisionInformationId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionInformation"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionInformation"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + "),(DivisionId," + Division + ")");
                    ActivityLogs.FieldsAffected = ("(Address," + Address + "),(Suburb," + Suburb + "),(Location," + Location + "),(ZipCode," + ZipCode + "),(Telephone," + Telephone + "),(Lada," + Lada + "),(Fax," + Fax + "),(Web," + Web + "),(City," + City + "),(State," + State + "),(Email," + Email + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void createnewdivisioninformation(string Address, string City, string Fax, string Lada, string Location, string State, string Suburb, string Telephone,
            string Web, string ZipCode, int Division, int DivisionInformationId, int ApplicationId, int UsersId, string Email)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionInformation"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionInformation"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + "),(DivisionId," + Division + ")");
                    ActivityLogs.FieldsAffected = ("(Address," + Address + "),(Suburb," + Suburb + "),(Location," + Location + "),(ZipCode," + ZipCode + "),(Telephone," + Telephone + "),(Lada," + Lada + "),(Fax," + Fax + "),(Web," + Web + "),(City," + City + "),(State," + State + "),(Email," + Email + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void insertimages(int divisionimageid, int DivisionId, string img, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionImages"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionImages"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionImageId," + divisionimageid + "),(ImageTypeId," + 1 + "),(DivisionId," + DivisionId + ")");
                    ActivityLogs.FieldsAffected = ("(ImageName," + img + "),(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void insertDivisionImagesSizes(int divisionimageid, byte size, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionImageSizes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionImageSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionImageId," + divisionimageid + "),(ImageSizeId," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void editimages(int divisionimageid, int DivisionId, string img, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionImages"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionImages"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionImageId," + divisionimageid + "),(ImageTypeId," + 1 + "),(DivisionId," + DivisionId + ")");
                    ActivityLogs.FieldsAffected = ("(ImageName," + img + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertproductimages(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ProductImageSizeId, string FileName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductImages"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductImages"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductImageSizeId," + ProductImageSizeId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductShot," + FileName + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertProductImageSizes(int ProductImageSizeId, int SizeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductImageSizes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductImageSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductImageSizeId," + ProductImageSizeId + "),(ImageSizeId," + SizeId + ")");                    

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _updateproductimages(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ProductImageSizeId, string FileName, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductImages"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductImages"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductImageSizeId," + ProductImageSizeId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductShot," + FileName + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _insertreferencesidef(int ProdId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProdId + "),(PharmaFormId," + PharmaFormId + ")");
                    ActivityLogs.FieldsAffected = ("(Active," + 1 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deletereferencesidef(int ProdId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProdId + "),(PharmaFormId," + PharmaFormId + ")");
                    ActivityLogs.FieldsAffected = ("(Active," + 0 + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }

        public void _deleteproductcontents(int ProductId, int PharmaFormId, int EditionId, int DivisionId, int CategoryId, int AttributeId, String HTMLContent, String PlainContent, String Content, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(AttributeId," + AttributeId + ")");
                    ActivityLogs.FieldsAffected = ("(Content," + Content + "),(PlainContent," + PlainContent + "),(HTMLContent," + HTMLContent + ")");

                    USERS.ActivityLogs.Add(ActivityLogs);
                    USERS.SaveChanges();
                }
            }
        }
    }
}