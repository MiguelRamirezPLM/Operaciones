using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class IndicationsBLC
    {

        #region Constructors

        private IndicationsBLC() { }

        #endregion

        #region Public Methods

        public MedinetBusinessEntries.IndicationInfo getIndication(string code, int indicationId)
        {
            if (indicationId <= 0)
                throw new ArgumentException("The indication does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getOne(indicationId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Alphabetic
        public List<MedinetBusinessEntries.IndicationInfo> getIndications(string code, byte countryId, int editionId, string description)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.IndicationInfo> indicationList =
                        PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getAll(countryId, editionId, description, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + description;
                    BEntity.JsonFormat = "{ \"Texto\" : \" " + description + "\" }";

                    if (indicationList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Indicaciones;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.IndicationInfo indication in indicationList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Indicaciones;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "IndicationId=" + indication.IndicationId.ToString();
                            sonTracking.JsonFormat = "{ \"Indicacion \" : \"" + indication.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;

                    this._PLMLogs.addLogParentActivity(BEntity);
       
                    return indicationList;
                 }
                  else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionInfo> getDrugs(string code, byte countryId, int editionId, int indicationId)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country, book edition or indication does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByIndication(countryId, editionId, indicationId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;                 
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "IndicationId=" + indicationId.ToString();
                    BEntity.JsonFormat = 
                        "{ \"Indicacion \" : \"" + PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getOne(indicationId).Description + "\" }";

                    if (productList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Ind;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Ind;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategotyId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                           sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                          "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}"; 

                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return productList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionDetailInfo> getDrugsDetail(string code, byte countryId, int editionId, int indicationId)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country, book edition or indication does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByIndication(countryId, editionId, indicationId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "IndicationId=" + indicationId.ToString();
                    BEntity.JsonFormat ="{ \"Indicacion \" : \"" + PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getOne(indicationId).Description + "\" }";
                    if (productList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Ind;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Ind;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategotyId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                          "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}"; 

                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return ProductsBLC.Instance.getProductByEditionDetailInfoList(code, countryId, editionId, productList);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Drugs
        public List<MedinetBusinessEntries.IndicationInfo> getIndicationsByDrug(string code, byte countryId, int editionId, int productId)
        {
            if (countryId == 0 || editionId <= 0 || productId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.IndicationInfo> indicationList =
                        PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);

                    MedinetBusinessEntries.ProductsInfo product = MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));

                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ProductId=" + productId.ToString();
                    BEntity.JsonFormat = "{ \"Producto\" : \" " + product.Brand.ToString() + "\" }";
                    if (indicationList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Ind_Med;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.IndicationInfo indication in indicationList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Ind_Med;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "IndicationId=" + indication.IndicationId.ToString();
                            sonTracking.JsonFormat = "{ \"Indicacion \" : \"" + indication.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return indicationList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Laboratory
        public List<MedinetBusinessEntries.IndicationInfo> getIndicationsByDivision(string code, int countryId, int editionId, int divisionId)
        {
            if (countryId <= 0 || editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The country, book edition or division does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.IndicationInfo> indicationList =
                        PharmaSearchEngineDataAccessComponent.IndicationsDALC.Instance.getByDivision(countryId, editionId, divisionId, (byte)Catalogs.TypeInEdition.Participante);

                    MedinetBusinessEntries.DivisionsInfo divisionInfo = MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "DivisionId=" + divisionId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \" " + divisionInfo.ShortName.ToString() + "\" }";
                    if (indicationList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Indicaciones_por_Laboratorio;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.IndicationInfo indication in indicationList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Indicaciones_por_Laboratorio;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "IndicationId=" + indication.IndicationId.ToString();
                            sonTracking.JsonFormat = "{ \"Indicacion \" : \"" + indication.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return indicationList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly IndicationsBLC Instance = new IndicationsBLC();

    }
}
