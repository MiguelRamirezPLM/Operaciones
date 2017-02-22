using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CRUDContentTypes
    {
        Medinet db = new Medinet();
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
                _delete.ContentTypeId = null;
                db.SaveChanges();
            }
        }
        public void _updateChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            var _getElemets = (from _pp in db.ParticipantProducts
                               where _pp.ProductId == ProductId
                               && _pp.PharmaFormId == PharmaFormId
                               && _pp.CategoryId == CategoryId
                               && _pp.DivisionId == DivisionId
                               && _pp.EditionId == EditionId
                               select _pp).ToList();
            var _getType = db.ContentTypes.Where(model => model.ContentType == "CON CAMBIO DE FONDO").ToList();
            foreach (var _update in _getElemets)
            {
                _update.ContentTypeId = _getType[0].ContentTypeId;
                db.SaveChanges();
            }
        }
        public void _deleteChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
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
            foreach (var _delete in _getElements)
            {
                _delete.ContentTypeId = null;
                db.SaveChanges();
            }
        }
        public void _updatewithOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
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
            var _getType = db.ContentTypes.Where(model => model.ContentType == "CON CAMBIO ORTOGRÁFICO").ToList();
            foreach (var _update in _getElements)
            {
                _update.ContentTypeId = _getType[0].ContentTypeId;
                db.SaveChanges();
            }
        }
        public void _deletewithOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
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
                _delete.ContentTypeId = null;
                db.SaveChanges();
            }
        }
    }
}