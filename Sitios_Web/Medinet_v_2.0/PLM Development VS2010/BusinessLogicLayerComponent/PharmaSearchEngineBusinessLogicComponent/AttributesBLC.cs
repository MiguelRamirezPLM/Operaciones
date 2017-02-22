using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class AttributesBLC
    {
        #region Constructors

        private AttributesBLC() { }

        #endregion

        #region Public Methods

        public List<AttributesInfo> getAttributes(string code, int editionId)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                return PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getAll(editionId);

            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> getAttributtesByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {            
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                return PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);

            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public ProductContentsInfo getAttributeContent(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, int attributeId)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                /*
                PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido_Atributo, "DivisionId= " + divisionId.ToString() + 
                    ";CategoryId=" + categoryId.ToString() + ";ProductId=" + productId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString() + ";AttributeId=" + attributeId.ToString());
                */
                PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();

                BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                sonTracking.CodeString = code;
                sonTracking.EditionId = editionId;
                sonTracking.SourceId = Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]);
                sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                sonTracking.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido_Atributo;
                sonTracking.SearchDate = null;
                sonTracking.SearchParameters = "DivisionId= " + divisionId.ToString() +
                    ";CategoryId=" + categoryId.ToString() + ";ProductId=" + productId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString() + ";AttributeId=" + attributeId.ToString();
                sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId).ShortName.ToString() + "\",\"Categoria\":\"" + MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(categoryId).Description.ToString() +
                "\",\"Producto\":\"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId).Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(pharmaFormId).Description.ToString() + "\"}";

                BEntity.ChildrenTrackingInfo.Add(sonTracking);   

                return PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getAttribute(editionId, divisionId, categoryId, productId, pharmaFormId, attributeId);
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo> getAttributesBySymptom(string code, int editionId, int symptomId)
        {
            if (editionId < 1 || symptomId < 1)
                throw new ArgumentException("The Edition or Symptom does not exist.");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo> symptomAttributes =
                        PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getAttributesBySymptom(editionId, symptomId);

                    foreach (PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo attribute in symptomAttributes)
                    {
                        if(attribute.HeaderImage != null)
                            attribute.HeaderImage = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl +
                                MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName +
                                "/" + System.Configuration.ConfigurationManager.AppSettings["OtcUrl"] + "/" + attribute.HeaderImage;
                    }
                    return symptomAttributes;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        public static readonly AttributesBLC Instance = new AttributesBLC();

    }
}
