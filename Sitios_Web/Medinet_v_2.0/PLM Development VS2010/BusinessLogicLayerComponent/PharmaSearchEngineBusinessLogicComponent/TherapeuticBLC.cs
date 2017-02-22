using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class TherapeuticBLC
    {

        #region Constructors

        private TherapeuticBLC() { }

        #endregion

        #region public methods

        public List<ProductByEditionInfo> getDrugs(string code, byte countryId, int editionId, int therapeuticId)
        {
            if (countryId == 0 || editionId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition or therapeutic does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "TherapeuticId=" + therapeuticId.ToString();
                    BEntity.JsonFormat = "{ \"Terapeutico\" : \"" + MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId).Description.ToString() + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC;
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

        public List<ProductByEditionDetailInfo> getDrugsDetail(string code, byte countryId, int editionId, int therapeuticId)
        {
            if (countryId == 0 || editionId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition or therapeutic does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "TherapeuticId=" + therapeuticId.ToString();
                    BEntity.JsonFormat = "{ \"Terapeutico\" : \"" + MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId).Description.ToString() + "\" }";

                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC;
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

        public List<TherapeuticByProductInfo> getTherapeuticsByDrug(string code, byte countryId, int editionId, int productId)
        {
            if (countryId == 0 || editionId <= 0 || productId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<TherapeuticByProductInfo> therapeuticList =
                        PharmaSearchEngineDataAccessComponent.TherapeuticsByProductDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ProductId=" + productId.ToString();
                    BEntity.JsonFormat = "{ \"Producto\" : \"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId).Description.ToString() + "\" }";
                    if (therapeuticList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (TherapeuticByProductInfo therapeutic in therapeuticList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "TherapeuticId=" + therapeutic.TherapeuticId.ToString();
                            sonTracking.JsonFormat = "{ \"Terapeutico\" : \"" + therapeutic.Therapeutic.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return therapeuticList;

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med, "ProductId=" + productId.ToString());

                    //return PharmaSearchEngineDataAccessComponent.TherapeuticsByProductDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<TherapeuticByProductInfo> getTherapeuticsByDrugByTherapeutic(string code, byte countryId, int editionId, int productId, int therapeuticId)
        {
            if (countryId == 0 || editionId <= 0 || productId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition, ATC or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.TherapeuticsByProductDALC.Instance.getByProductByTherapeutic(countryId, editionId, productId, therapeuticId, (byte)Catalogs.TypeInEdition.Participante);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<MedinetBusinessEntries.TherapeuticInfo> getTherapeuticsByName(string code, byte countryId, int editionId, string description)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                List<MedinetBusinessEntries.TherapeuticInfo> therapeuticList =
                    PharmaSearchEngineDataAccessComponent.TherapeuticsByProductDALC.Instance.getByName(countryId, editionId, description, (byte)Catalogs.TypeInEdition.Participante);

                PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                
                BEntity.SearchDate = null;
                BEntity.SearchParameters = "Text=" + description;
                BEntity.JsonFormat = "{ \"Texto\" : \"" + description.ToString() + "\" }";
                if (therapeuticList.Count > 0)
                {
                    BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Terapéutico;
                    BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                    PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                    foreach (MedinetBusinessEntries.TherapeuticInfo therapeutic in therapeuticList)
                    {
                        sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                        sonTracking.CodeString = code;
                        sonTracking.EditionId = editionId;
                        sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                        sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                        sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Terapéutico;
                        sonTracking.SearchDate = null;
                        sonTracking.SearchParameters = "TherapeuticId=" + therapeutic.TherapeuticId.ToString();
                        sonTracking.JsonFormat = "{ \"Terapeutico\" : \"" + therapeutic.Description.ToString() + "\" }";
                        BEntity.ChildrenTrackingInfo.Add(sonTracking);
                    }
                }
                else
                {
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                }

                this._PLMLogs.addLogParentActivity(BEntity);
                return therapeuticList;

                //return PharmaSearchEngineDataAccessComponent.TherapeuticsByProductDALC.Instance.getByName(countryId, editionId, description, (byte)Catalogs.TypeInEdition.Participante);
            }

            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public List<MedinetBusinessEntries.TherapeuticInfo> getByDrugByPharmaForm(string code, byte countryId, int editionId, int drugId, int pharmaFormId)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                List<MedinetBusinessEntries.TherapeuticInfo> therapeuticList =
                    MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAllByProduct(drugId, pharmaFormId);

                PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                
                BEntity.SearchDate = null;
                BEntity.SearchParameters = "ProductId=" + drugId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString();
                BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(drugId).Brand.ToString() + 
                                        "\",\"Forma Farmaceutica\":\"" + MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(pharmaFormId).Description.ToString() + "\"}";

                if (therapeuticList.Count > 0)
                {
                    BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med;
                    BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                    PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                    foreach (MedinetBusinessEntries.TherapeuticInfo therapeutic in therapeuticList)
                    {
                        sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                        sonTracking.CodeString = code;
                        sonTracking.EditionId = editionId;
                        sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                        sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                        sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med;
                        sonTracking.SearchDate = null;
                        sonTracking.SearchParameters = "TherapeuticId=" + therapeutic.TherapeuticId.ToString();
                        sonTracking.JsonFormat = "{ \"Terapeutico\" : \"" + therapeutic.Description.ToString() + "\" }";
                        BEntity.ChildrenTrackingInfo.Add(sonTracking);
                    }
                }
                else
                {
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                }

                this._PLMLogs.addLogParentActivity(BEntity);
                return therapeuticList;

                //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC, 
                //    "ProductId=" + drugId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString());

                //return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAllByProduct(drugId, pharmaFormId);
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly TherapeuticBLC Instance = new TherapeuticBLC();

    }
}
