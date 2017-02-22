using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class PresentationsBLC
    {
        #region Constructors

        private PresentationsBLC() { }

        #endregion

        #region public methods

        public List<PharmaSearchEngineBusinessEntries.PresentationByProductInfo> getPresentationByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PresentationByProductInfo> presentationList =
                        PharmaSearchEngineDataAccessComponent.PresentationsDALC.Instance.getPresentationByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Presentaciones_por_Producto;
                    BEntity.SearchDate = null;
                    string shortName =  MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId).ShortName.ToString();
                    string category = MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(categoryId).Description.ToString();
                    string brand = MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId).Brand.ToString();
                    string pharmaForm = MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(pharmaFormId).Description.ToString();
                    BEntity.SearchParameters = "DivisionId=" + divisionId.ToString() + ";CategoryId=" + categoryId.ToString()
                                    + ";ProductId=" + productId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + shortName + "\",\"Categoria\":\"" + category +
                    "\",\"Producto\":\"" +brand  + "\",\"Forma Farmaceutica\":\"" + pharmaForm  + "\"}";
                    if (presentationList != null)
                    {
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;


                        foreach (PharmaSearchEngineBusinessEntries.PresentationByProductInfo presentation in presentationList)
                        {
                            string presentations = presentation.QtyExternalPack.ToString() + " " + presentation.ExternalPackName + " " + presentation.QtyInternalPack + " " +
                                                   presentation.InternalPackName + " " + presentation.QtyContentUnit + " " + presentation.ContentUnitName + " " + presentation.QtyWeightUnit.ToString() + " " + presentation.WeightShortName.ToString();
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Presentaciones_por_Producto;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ProductId =" + productId + ";PharmaFormId = " + pharmaFormId + ";PresentationId=" + presentation.PresentationId.ToString();
                            sonTracking.JsonFormat = "{\"Producto\" : \""+brand+"\" , \"Forma Farmaceutica\" : \""+pharmaForm+"\" , \"Presentacion\" : \""+presentations+" \" }";

                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return presentationList;
                }

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }
        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly PresentationsBLC Instance = new PresentationsBLC();
    }
}
