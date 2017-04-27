using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agronet.Models;

namespace Agronet.Models
{
    public class ActivityLog
    {
        PLMUsers dbusers = new PLMUsers();
        CountriesUsers c = (CountriesUsers)System.Web.HttpContext.Current.Session["CountriesUsers"];
        ActivityLogs ActivityLogs = new ActivityLogs();

        /*          Products            */

        public void Products(int ProductId, string ProductName, string Description, string Register, int CountryId, int LaboratoryId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "Products" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(CountryId," + CountryId + "),(LaboratoryId," + LaboratoryId + ")");
                ActivityLogs.FieldsAffected = ("(ProductName," + ProductName + "),(Description," + Description + "),(" + "Register," + Register + "),(" + "Active," + 1 + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }

        }

        public void ProductPharmaForms(int ProductId, int PharmaFormId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ProductPharmaForms" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(Active,1)");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void DivisionCategories(int DivisionId, int CategoryId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "DivisionCategories" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(Active,1)");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void ProductCategories(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ProductCategories" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void NewProducts(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "NewProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(EditionId," + EditionId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void EditionDivisionProducts(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "EditionDivisionProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void ParticipantProducts(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ParticipantProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");
                ActivityLogs.FieldsAffected = ("(HTMLContent,NULL),(Page,NULL),(XMLContent,NULL),(ContentTypeId,NULL),(Active,NULL)");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void MentionatedProducts(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "MentionatedProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void SIDEF(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation, int Active)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ParticipantProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");
                if (Active == 1)
                {
                    ActivityLogs.FieldsAffected = ("(Active," + Active + ")");
                }
                else
                {
                    ActivityLogs.FieldsAffected = ("(Active,NULL)");
                }

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void ProductChanges(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation, int ContentTypeId)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ParticipantProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");
                if (ContentTypeId != 0)
                {
                    ActivityLogs.FieldsAffected = ("(ContentTypeId," + ContentTypeId + ")");
                }
                else
                {
                    ActivityLogs.FieldsAffected = ("(ContentTypeId,NULL)");
                }

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void ProductPage(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int EditionId, int Operation, string Page)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ParticipantProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(EditionId," + EditionId + "),(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");
                if (!string.IsNullOrEmpty(Page))
                {
                    ActivityLogs.FieldsAffected = ("(Page," + Page + ")");
                }
                else
                {
                    ActivityLogs.FieldsAffected = ("(Page, NULL)");
                }

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        /*          Divisions            */

        public void Divisions(int DivisionId, string DivisionName, string ShortName, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "ParticipantProducts" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + ")");
                ActivityLogs.FieldsAffected = ("(DivisionName, " + DivisionName + "),(ShortName, " + ShortName + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void DivisionInformation(int DivisionInformationId, int DivisionId, string Address, string City, string Email, string Fax, string Lada, string Location, string State, string Suburb, string Telephone, string Web, string ZipCode, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "DivisionInformation" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionInformationId," + DivisionInformationId + "),(DivisionId," + DivisionId + ")");
                ActivityLogs.FieldsAffected = ("(Address, " + Address + "),(City, " + City + "),(Email, " + Email + "),(Fax, " + Fax + "),(Lada, " + Lada + "),(Location, " + Location + "),(State, " + State + "),(Suburb, " + Suburb + "),(Telephone, " + Telephone + "),(Web, " + Web + "),(ZipCode, " + ZipCode + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void DivisionImages(int DivisionImageId, int DivisionId, string ImageName, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "DivisionImages" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionImageId," + DivisionImageId + "),(DivisionId," + DivisionId + ")");
                ActivityLogs.FieldsAffected = ("(ImageName, " + ImageName + "),(Active, " + ImageName + "),(BaseURL,),(ImageSizeId,1)");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void DivisionImageSizes(int DivisionImageId, int ImageSizeId, int Operation)
        {
            List<Tables> LT = dbusers.Tables.Where(x => x.Description == "DivisionImageSizes" && x.ApplicationId == c.ApplicationId).ToList();

            foreach (Tables tbl in LT)
            {
                int TableId = tbl.TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = Operation;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionImageId," + DivisionImageId + "),(ImageSizeId," + ImageSizeId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }
    }
}