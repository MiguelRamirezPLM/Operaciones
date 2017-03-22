using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CRUDContentTypes
    {
        Medinet db = new Medinet();
        PLMUsers plm = new PLMUsers();
        CRUD CRUD = new CRUD();
        PLMUserActions Action = new PLMUserActions();
        PLMUserTables Tables = new PLMUserTables();

        public void _updateNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            List<plm_spGetAttributeGroups_Result> _getAttribute = db.Database.SqlQuery<plm_spGetAttributeGroups_Result>
                ("plm_spGetAttributeGroups"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId + "").ToList();
            foreach (var _rowvl in _getAttribute)
            {
                List<plm_spCRUDModifiedAttributeGroups> _plm_spCRUDModifiedAttributeGroups = db.Database.SqlQuery<plm_spCRUDModifiedAttributeGroups>
                    ("EXECUTE dbo.plm_spCRUDModifiedAttributeGroups"
                    + " @CRUDType=" + 3
                    + ",@attributeGroupId=" + _rowvl.AttributeGroupId
                    + ",@editionId=" + EditionId
                    + ",@productId=" + ProductId
                    + ",@divisionId=" + DivisionId
                    + ",@categoryId=" + CategoryId
                    + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            }
            var _getElements = (from _pp in db.ParticipantProducts
                                where _pp.ProductId == ProductId
                                && _pp.PharmaFormId == PharmaFormId
                                && _pp.CategoryId == CategoryId
                                && _pp.DivisionId == DivisionId
                                && _pp.EditionId == EditionId
                                select _pp).ToList();
            var _getType = db.ContentTypes.Where(model => model.ContentType == "Nuevo").ToList();
            foreach (var _update in _getElements)
            {
                _update.ContentTypeId = _getType[0].ContentTypeId;
                db.SaveChanges();
            }
        }
        public void _deleteNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            var _getElements = (from _pp in db.ParticipantProducts
                                where _pp.ProductId == ProductId
                                && _pp.PharmaFormId == PharmaFormId
                                && _pp.CategoryId == CategoryId
                                && _pp.DivisionId == DivisionId
                                && _pp.EditionId == EditionId
                                select _pp).ToList();
            foreach (var _delete in _getElements)
            {
                _delete.ContentTypeId = 4;
                db.SaveChanges();
            }
        }
        public bool _updateChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId, int ContentTypeId, int UserId, string HashKey)
        {
            string FieldsAffected = "(ContentTypeId,2)";
            string primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            try
            {
                db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + ", @modifiedContent=" + ContentTypeId + "").ToList();

                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                return true;
            }
            
             catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }
        public bool _deleteChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId, int ContentTypeId, int UserId, string HashKey)
        {
            string FieldsAffected = "(ContentTypeId,4)";
            string primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            List<plm_spGetAttributeGroups_Result> _getAttribute = db.Database.SqlQuery<plm_spGetAttributeGroups_Result>
                ("plm_spGetAttributeGroups"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId + "").ToList();
            foreach (var _rowvl in _getAttribute)
            {
                List<plm_spCRUDModifiedAttributeGroups> _plm_spCRUDModifiedAttributeGroups = db.Database.SqlQuery<plm_spCRUDModifiedAttributeGroups>
                    ("EXECUTE dbo.plm_spCRUDModifiedAttributeGroups"
                    + " @CRUDType=" + 3
                    + ",@attributeGroupId=" + _rowvl.AttributeGroupId
                    + ",@editionId=" + EditionId
                    + ",@productId=" + ProductId
                    + ",@divisionId=" + DivisionId
                    + ",@categoryId=" + CategoryId
                    + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            }
            try
            {
                db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + ", @modifiedContent=" + ContentTypeId + "").ToList();

                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                return true;
            }

            catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }
        public bool _updatewithOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId, int ContentTypeId, int UserId, string HashKey)
        {
            
             string FieldsAffected = "(ContentTypeId,3)";
            string primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            try
            {
                db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + ", @modifiedContent=" + ContentTypeId + "").ToList();

                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                return true;
            }
            
             catch (Exception e)
            {
                string message = e.Message;

                return false;
            }
        }

        public bool _deletewithOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId, int ContentTypeId, int UserId, string HashKey)
        {
            string FieldsAffected = "(ContentTypeId,4)";
            string primaryKeyAffected = "(EditionId," + EditionId + ");(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            List<plm_spGetAttributeGroups_Result> _getAttribute = db.Database.SqlQuery<plm_spGetAttributeGroups_Result>
                ("plm_spGetAttributeGroups"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId + "").ToList();
            foreach (var _rowvl in _getAttribute)
            {
                List<plm_spCRUDModifiedAttributeGroups> _plm_spCRUDModifiedAttributeGroups = db.Database.SqlQuery<plm_spCRUDModifiedAttributeGroups>
                    ("EXECUTE dbo.plm_spCRUDModifiedAttributeGroups"
                    + " @CRUDType=" + 3
                    + ",@attributeGroupId=" + _rowvl.AttributeGroupId
                    + ",@editionId=" + EditionId
                    + ",@productId=" + ProductId
                    + ",@divisionId=" + DivisionId
                    + ",@categoryId=" + CategoryId
                    + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            }

            try
            {
                db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + ", @modifiedContent=" + ContentTypeId + "").ToList();

                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
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