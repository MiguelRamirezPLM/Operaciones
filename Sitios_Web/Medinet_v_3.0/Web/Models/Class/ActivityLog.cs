using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Class;
using Web.Models;

namespace Web.Models.Class
{
    public class ActivityLog
    {
        ActivityLogs ActivityLogs = new ActivityLogs();
        PLMUsers dbusers = new PLMUsers();
        CountriesUsers c = (CountriesUsers)System.Web.HttpContext.Current.Session["CountriesUsers"];

        public void UpdateProductType(int ProductId, byte ProductTypeId, int OperationId)
        {
            string Table = "Products";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + ")");
                ActivityLogs.FieldsAffected = ("(ProductTypeId," + ProductTypeId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductSubstances(int ProductId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductSubstances";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductTherapeutics(int ProductId, int PharmaFormId, int TherapeuticId, int OperationId)
        {
            string Table = "ProductTherapeutic";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(TherapeuticId," + TherapeuticId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductContraindicationsByICD(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ICDId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductTherapeutic";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(ICDId," + ICDId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDIPPAProductContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int StatusId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "IPPAProductContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(StatusId," + StatusId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        #region Contraindications

        public void CRUDProductSubstanceContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductSubstanceContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductICDContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductICDContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductPharmaGroupContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductPharmaGroupContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductPhysiologicalContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductPhysiologicalContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductPharmacologicalContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductPharmacologicalContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductHypersensibilityContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductHypersensibilityContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductOtherContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "ProductOtherContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductCommentContraindications(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, string Comment, int OperationId)
        {
            string Table = "ProductCommentContraindications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + ")");
                ActivityLogs.FieldsAffected = ("Comments," + Comment + "");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDIPPAProductInteractions(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int StatusId, int ActiveSubstanceId, int OperationId)
        {
            string Table = "IPPAProductInteractions";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + DivisionId + "),(CategoryId," + CategoryId + "),(PharmaFormId," + PharmaFormId + "),(ProductId," + ProductId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(StatusId," + StatusId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        #endregion

        #region CIE-10

        public void CRUDProductICD(int ProductId, int PharmaFormId, int ICDId, int OperationId)
        {
            string Table = "IPPAProductInteractions";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(ICDId," + ICDId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductTherapeuticOMS(int ProductId, int PharmaFormId, int TherapeuticOMSId, int OperationId)
        {
            string Table = "ProductTherapeuticOMS";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(TherapeuticOMSId," + TherapeuticOMSId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        #endregion

        #region Interactions

        public void CRUDProductSubstanceInteractions(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int SubstanceInteractId, int OperationId)
        {
            string Table = "ProductSubstanceInteractions";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(SubstanceInteractId," + SubstanceInteractId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductPharmaGroupInteractions(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int PharmaGroupId, int OperationId)
        {
            string Table = "ProductPharmaGroupInteractions";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(PharmaGroupId," + PharmaGroupId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductOtherInteractions(int ProductId, int PharmaFormId, int DivisionId, int CategoryId, int ActiveSubstanceId, int ElementId, int OperationId)
        {
            string Table = "ProductOtherInteractions";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(CategoryId," + CategoryId + "),(DivisionId," + DivisionId + "),(ActiveSubstanceId," + ActiveSubstanceId + "),(ElementId," + ElementId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductIndications(int ProductId, int IndicationId, int OperationId)
        {
            string Table = "ProductIndications";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(IndicationId," + IndicationId + ")");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        public void CRUDProductPharmaFormRoutes(int ProductId, int PharmaFormId, int RouteId, string JSONFormat, int OperationId)
        {
            string Table = "ProductPharmaFormRoutes";

            List<Tables> LT = dbusers.Tables.Where(x => x.ApplicationId == c.ApplicationId && x.Description == Table).ToList();

            if (LT.LongCount() > 0)
            {
                int TableId = LT[0].TableId;

                ActivityLogs = new Models.ActivityLogs();

                ActivityLogs.UserId = c.userId;
                ActivityLogs.TableId = TableId;
                ActivityLogs.OperationId = OperationId;
                ActivityLogs.Date = DateTime.Now;
                ActivityLogs.PrimaryKeyAffected = ("(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + "),(RouteId," + RouteId + ")");
                ActivityLogs.FieldsAffected = ("JSONFormat," + JSONFormat + "");

                dbusers.ActivityLogs.Add(ActivityLogs);
                dbusers.SaveChanges();
            }
        }

        #endregion

    }
}