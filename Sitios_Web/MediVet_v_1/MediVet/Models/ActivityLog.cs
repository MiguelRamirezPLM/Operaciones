using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediVet.Models;

namespace MediVet.Models
{
    public class ActivityLog
    {
        PLMUsers dbusers = new PLMUsers();
        ActivityLogs ActivityLogs = new ActivityLogs();


        /*           VENTAS          */

        public void _editproductSM(int ProductId, string Description, int ApplicationId, int UsersId,string ProductName)
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
                    ActivityLogs.FieldsAffected = ("(ProductName," + ProductName + "),(Description," + Description + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deletementionatedproducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertEditionDivisionProducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deleteEditionDivisionProducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertParticipantProducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deleteParticipantProducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertMentionatedProducts(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertProduct(int ProductId, string description, int CountryId, string Product, int LaboratoryId, int ApplicationId, int UsersId)
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
                    ActivityLogs.FieldsAffected = ("(Product," + Product + "),(Description," + description + "),(CountryId," + CountryId + "),(LaboratoryId," + LaboratoryId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertProductPharmaForms(int PharmaFormId, int ProductId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertdivisioncategories(int CategoryId, int DivisionId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertProductCategories(int CategoryId, int DivisionId, int PharmaFormId, int ProductId, int ApplicationId, int UsersId, string RegisterSanitary)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(RegisterSanitary," + RegisterSanitary + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertnewproducts(int CategoryId, int DivisionId, int EditionId, int PharmaFormId, int ProductId, int ApplicationId, int UsersId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _referencesidef(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId,bool Active, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(Active, " + Active + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _productchanges(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ContentTypeId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ContentTypeId, " + ContentTypeId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deleteproductchanges(int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId, int ContentTypeId, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ContentTypeId, " + ContentTypeId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _editdivisioninformation(int DivisionInformationId, int DivisionId, string Address, string Suburb, string ZipCode, string City, string State, string Telephone, string Fax, string Web, string Email, string Lada,int ApplicationId,int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + "),(DivisionId," + DivisionId + ")");
                    ActivityLogs.FieldsAffected = ("(Address, " + Address + "),(Suburb, " + Suburb + "),(ZipCode, " + ZipCode + "),(City, " + City + "),(State, " + State + "),(Telephone, " + Telephone + "),(Fax, " + Fax + "),(Web, " + Web + "),(Email, " + Email + "),(Lada, " + Lada + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _insertdivisioninformation(int DivisionInformationId, int DivisionId, string Address, string Suburb, string ZipCode, string City, string State, string Telephone, string Fax, string Web, string Email, string Lada, int ApplicationId, int UsersId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + "),(DivisionId," + DivisionId + ")");
                    ActivityLogs.FieldsAffected = ("(Address, " + Address + "),(Suburb, " + Suburb + "),(ZipCode, " + ZipCode + "),(City, " + City + "),(State, " + State + "),(Telephone, " + Telephone + "),(Fax, " + Fax + "),(Web, " + Web + "),(Email, " + Email + "),(Lada, " + Lada + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _deletedivisioninformation(int DivisionInformationId, int ApplicationId, int UsersId)
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
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _addregisterSanitaryProductCategories(int CategoryId, int DivisionId, int PharmaFormId, int ProductId, int ApplicationId, int UsersId, string RegisterSanitary, int OperationId)
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

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + ")");
                    ActivityLogs.FieldsAffected = ("(RegisterSanitary," + RegisterSanitary + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _addDivisionProducts(int DivisionId, int ProductId, int ApplicationId, int UsersId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "DivisionProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("DivisionProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(ProductId," + ProductId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /*           VETERINARIO          */

        public void _productequipment(int EquipmentId, int ProductId, int ApplicationId, int UsersId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductEquipment"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductEquipment"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(EquipmentId," + EquipmentId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _productspecies(int SpecieId, int ProductId, int ApplicationId, int UsersId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductSpecies"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductSpecies"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(SpecieId," + SpecieId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _productsubstances(int ActiveSubstanceId, int ProductId, int ApplicationId, int UsersId, int OperationId)
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
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _producttherapeuticuses(int TherapeuticId, int ProductId, int ApplicationId, int UsersId, int OperationId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "ProductTherapeuticUses"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ProductTherapeuticUses"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(TherapeuticId," + TherapeuticId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }


        /*           PRODUCCION         */

        public void _updatepages(int CategoryId, int DivisionId, int PharmaFormId, int ProductId, int ApplicationId, int UsersId, int OperationId, string page, int EditionId)
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
                    ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(Page," + page +")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _editionproductshots(int CategoryId, int DivisionId, int PharmaFormId, int ProductId, int ApplicationId, int UsersId, int OperationId, string ProductShot, int EditionId, int EditionProductShotId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionProductShots"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionProductShots"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionProductShotId," + EditionProductShotId + "),(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductShot," + ProductShot + "),(PSTypeId," + 1 + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _editionproductshotsimagesizes(int EditionProductShotId, int ImageSizeId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionProductShotsImageSizes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionProductShotsImageSizes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionProductShotId," + EditionProductShotId + "),(ImageSizeId," + ImageSizeId + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }

        public void _editimagename(int EditionProductShotId, string ImageName, int ApplicationId, int UsersId, int ProductId, int DivisionId, int EditionId, int PharmaFormId, int CategoryId)
        {
            var table = from Tables1 in dbusers.Tables
                        where Tables1.Description == "EditionProductShots"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionProductShots"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(EditionProductShotId," + EditionProductShotId + "),(ProductId," + ProductId + "),(DivisionId," + DivisionId + "),(EditionId," + EditionId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + ")");
                    ActivityLogs.FieldsAffected = ("(ProductShot," + ImageName + ")");

                    dbusers.ActivityLogs.Add(ActivityLogs);
                    dbusers.SaveChanges();
                }
            }
        }
    }
}