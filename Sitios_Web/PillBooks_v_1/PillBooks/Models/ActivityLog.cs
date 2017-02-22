using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class ActivityLog
    {
        PLMUsers db = new PLMUsers();
        ActivityLogs ActivityLogs = new ActivityLogs();

        public void createpillbook(int PillBookId, string PillBookName, string PIllBookCode, bool Active, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBook"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBook"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + ")");
                    ActivityLogs.FieldsAffected = ("(PillBookName," + PillBookName + "),(PIllBookCode," + PIllBookCode + "),(AddDate," + DateTime.Now + "),(ModifyDate," + DateTime.Now + "),(" + "Active," + Active + ")");
                     
                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void editpillbook(int PillBookId, string PillBookName, string PIllBookCode, bool Active, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBook"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBook"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + ")");
                    ActivityLogs.FieldsAffected = ("(PillBookName," + PillBookName + "),(PIllBookCode," + PIllBookCode + "),(" + "Active," + Active + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookproducts(int PillBookId, int ProductId, int PharmaFormId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookatc(int PillBookId, int TherapeuticId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookATC"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookATC"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(TherapeuticId," + TherapeuticId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookcie(int PillBookId, int ICDId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookICD"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookICD"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ICDId," + ICDId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookinnasubstances(int PillBookId, int InnaActiveSubstanceId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookINNSubstances"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookINNSubstances"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(INNActiveSubstanceId," + InnaActiveSubstanceId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void inserteditionpillbookattributes(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, String Content, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "EditionPillBookAttributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionPillBookAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");
                    ActivityLogs.FieldsAffected = ("(Content," + Content + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void editeditionpillbookattributes(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, String Content, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "EditionPillBookAttributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionPillBookAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");
                    ActivityLogs.FieldsAffected = ("(Content," + Content + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookasociated(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, int PillBookAsociateId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookAssociated"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookAssociated"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + "),(PillBookAsociateId," + PillBookAsociateId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookencyclopedias(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, int EncyclopediaId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookEncyclopedias"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookEncyclopedias"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + "),(EncyclopediaId," + EncyclopediaId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void editpillbookicon(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, byte PillBookIconId,String Content, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "EditionPillBookAttributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionPillBookAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");
                    ActivityLogs.FieldsAffected = ("(Content," + Content + "),(PillBookIconId," + PillBookIconId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookicon(int AttributeGroupId, int AttributeId, int EditionId, int PillBookId, byte PillBookIconId, String Content, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "EditionPillBookAttributes"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("EditionPillBookAttributes"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(EditionId," + EditionId + "),(AttributeId," + AttributeId + "),(AttributeGroupId," + AttributeGroupId + ")");
                    ActivityLogs.FieldsAffected = ("(Content," + Content + "),(PillBookIconId," + PillBookIconId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookinnSubstances(int PillBookId, int InnaActiveSubstanceId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookINNSubstances"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookINNSubstances"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(INNActiveSubstanceId," + InnaActiveSubstanceId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookatc(int PillBookId, int TherapeuticId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookATC"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookATC"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(TherapeuticId," + TherapeuticId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookproducts(int PillBookId, int ProductId, int PharmaFormId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookProducts"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookProducts"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ProductId," + ProductId + "),(PharmaFormId," + PharmaFormId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookcie(int PillBookId, int ICDId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookICD"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookICD"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ICDId," + ICDId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void clinicalreferences(int ClinicalReferenceId, String ClinicalReference, String URL, int OperationId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "ClinicalReferences"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("ClinicalReferences"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ClinicalReferenceId," + ClinicalReferenceId + ")");
                    ActivityLogs.FieldsAffected = ("(ClinicalReference," + ClinicalReference + "),(SourceReference," + URL + "),(Active," + 1 + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void insertpillbookreferences(int PillBookId, int ClinicalReferenceId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookReferences"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookReferences"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ClinicalReferenceId," + ClinicalReferenceId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookreferences(int PillBookId, int ClinicalReferenceId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookReferences"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;

            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookReferences"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(ClinicalReferenceId," + ClinicalReferenceId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void updatefinishedpillbook(int PillBookId, bool Finished, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBook"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBook"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 2;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + ")");
                    ActivityLogs.FieldsAffected = ("(" + "Finished," + Finished + "),(ModifyDate," + DateTime.Now + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }


        /*              ATC OMS         */

        public void insertpillbookatcOMS(int PillBookId, int TherapeuticOMSId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookATCOMS"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookATCOMS"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 1;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(TherapeuticId," + TherapeuticOMSId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }

        public void deletepillbookatcOMS(int PillBookId, int TherapeuticOMSId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PillBookATCOMS"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PillBookATCOMS"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = 4;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(PillBookId," + PillBookId + "),(TherapeuticId," + TherapeuticOMSId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }


        /*          SUBSTANCES          */

        public void insertplminnsubstances(int ActiveSubstanceId, int INNActiveSubstanceId,int OperationId, int ApplicationId, int UsersId)
        {
            var table = from Tables1 in db.Tables
                        where Tables1.Description == "PLMINNSubstances"
                        && Tables1.ApplicationId == ApplicationId
                        select Tables1;
            foreach (Tables tbl in table)
            {
                if (tbl.Description.Equals("PLMINNSubstances"))
                {
                    int TableId = tbl.TableId;

                    ActivityLogs = new Models.ActivityLogs();

                    ActivityLogs.UserId = UsersId;
                    ActivityLogs.TableId = TableId;
                    ActivityLogs.OperationId = OperationId;
                    ActivityLogs.Date = DateTime.Now;
                    ActivityLogs.PrimaryKeyAffected = ("(ActiveSubstanceId," + ActiveSubstanceId + "),(INNActiveSubstanceId," + INNActiveSubstanceId + ")");

                    db.ActivityLogs.Add(ActivityLogs);
                    db.SaveChanges();
                }
            }
        }
    }
}