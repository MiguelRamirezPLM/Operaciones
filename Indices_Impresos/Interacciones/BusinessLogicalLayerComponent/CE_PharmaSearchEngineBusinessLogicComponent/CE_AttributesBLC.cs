using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_AttributesBLC
    {

        #region Constructor
        
        private CE_AttributesBLC() { }

        #endregion

        #region Public Method

        public List<MedinetBusinessEntries.AttributesInfo> getAttributes(string code, int editionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_AttributesDALC.Instance.getAll(editionId);
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> getAttributesByDrug(string code, int editionId, int labId, int categoryId, int drugId, int pharmaFormId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_AttributesDALC.Instance.getByProduct(editionId, labId, categoryId, drugId, pharmaFormId);
            else
                return null;
        }

        public MedinetBusinessEntries.ProductContentsInfo getContentByAttribute(string code, int editionId, int labId, int categoryId, int drugId, int pharmaFormId, int attributeId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido_Atributo, "DivisionId=" + labId.ToString() + ";CategoryId=" + categoryId.ToString() + ";ProductId=" + 
                    drugId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString() + ";AttributeId=" + attributeId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_AttributesDALC.Instance.getAttribute(editionId, labId, categoryId, drugId, pharmaFormId, attributeId);
            }
            else
                return null;
        }
        #endregion

        public static readonly CE_AttributesBLC Instance = new CE_AttributesBLC();

        
    }
}
