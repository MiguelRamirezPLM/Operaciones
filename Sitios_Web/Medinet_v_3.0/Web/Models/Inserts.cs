using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Inserts
    {
        Medinet db = new Medinet();
        PLMUsers plm = new PLMUsers();
        CRUD CRUD = new CRUD();
        PLMUserActions Action = new PLMUserActions();
        PLMUserTables Tables = new PLMUserTables();
        CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();

        public int inserproduct(int CountryId, int LaboratoryId, int AlphabetId, int ProductTypeId, string ProductName, string Description, int UserIdP,string HashKeyP)
        {
            try
            {
                
                List<CreateProduct_result> LI = new List<CreateProduct_result>();
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

                if (string.IsNullOrEmpty(Description))
                {
                    Description = null;
                }
                int Active = 1;
                int productId = 0;
                string primaryKeyAffected;
                string FieldsAffected;

                LI = db.Database.SqlQuery<CreateProduct_result>("plm_spCRUDProducts @productId= 0,@CRUDType=" + CRUD.Create + ", @countryId=" + CountryId + ", @laboratoryId=" + LaboratoryId + ", @alphabetId=" + AlphabetId + ", @ProductTypeId=" + ProductTypeId + ", @brand='" + ProductName.ToUpper().Trim() + "',@description='" + Description + "', @active=" + Active + "").ToList();

                var qryprod = db.Products.Where(x => x.CountryId == CountryId && x.AlphabetId == AlphabetId && x.LaboratoryId == LaboratoryId && x.ProductTypeId == ProductTypeId && x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim()).ToList();

                productId = qryprod[0].ProductId;
                primaryKeyAffected = "(ProductId," + productId + ")";

                FieldsAffected = "(LaboratoryId," + LaboratoryId + ");(AlphabetId," + AlphabetId + ");(Brand,"+ ProductName + ");(Description," + Description + ");(Active," + Active + ")";

                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.Products + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'"+ "").ToList();

                return productId;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return 1;
            }

        }

        public bool insertproductpharmaforms(int ProductId, int PharmaFormId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductPharmaForms @CRUDType=" + CRUD.Create + ", @pharmaFormId= " + PharmaFormId + " ,@productId = " + ProductId + ",@active= 1");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(ProductId," + ProductId + "); (PharmaFormId," + PharmaFormId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.ProductPharmaForms + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
               
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool insertproductcategories(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductCategories @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId= " + ProductId + ",@technicalSheet= 0, @CRUDType=" + CRUD.Create + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(ProductId," + ProductId + "); (PharmaFormId," + PharmaFormId + ")" + "; ( CategoryId," + CategoryId + "; (DivisionId," + DivisionId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.ProductCategories + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
               
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool insertparticipantproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
               
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool insertnewproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.NewProducts + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                _CRUDContentTypes._deleteNew(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }

        }

        public bool inserteditiondivisionproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Create + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.EditionDivisionProducts + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
               
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool insertDivisionCategories(int DivisionId, int CategoryId, int UserIdP, string HashKeyP)
        {
            string primaryKeyAffected;
            try
            {
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDDivisionCategories @CRUDType=" + CRUD.Create + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(DivisionId," + DivisionId + "); (CategoryId" + CategoryId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.DivisionCategories + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool deletenewproducts(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int UserIdP, string HashKeyP)
        {
            try
            {
                string primaryKeyAffected;
                var _response = db.Database.ExecuteSqlCommand("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "");
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserIdP + ",@tableId=" + Tables.NewProducts + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKeyP + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                _CRUDContentTypes._deleteNew(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
                return true;
            }
            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }


    }
}