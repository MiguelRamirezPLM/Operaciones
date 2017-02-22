using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class ProductsBLC
    {

        #region Constructors

        private ProductsBLC() { }

        #endregion

        #region Public Methods

        #region Alphabetic

        public List<ProductByEditionInfo> getDrugs(string code, byte countryId, int editionId, string brand)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAll(countryId, editionId, brand, (byte)Catalogs.TypeInEdition.Participante, null, null);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId; 
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    //BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + brand;
                    BEntity.JsonFormat = "{ \"Texto\" : \""+brand+"\" }";

                    if (productList.Count > 0 )
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
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

        public List<ProductByEditionDetailInfo> getDrugsDetail(string code, byte countryId, int editionId, string brand)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    // Get all matched brands:
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAll(countryId, editionId, brand, (byte)Catalogs.TypeInEdition.Participante, null, null);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + brand;
                    BEntity.JsonFormat = "{ \"Texto\" : \"" + brand + "\" }";
                    if (productList.Count > 0 )
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
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
                    return this.getProductByEditionDetailInfoList(code, countryId, editionId, productList);

                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionShortDetailInfo> getDrugsShortDetail(string code, byte countryId, int editionId, string brand, byte? numberPage, byte? page)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    // Get all matched brands:
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAll(countryId, editionId, brand, (byte)Catalogs.TypeInEdition.Participante, numberPage, page);

                    int totalRows = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAll(countryId, editionId, brand, (byte)Catalogs.TypeInEdition.Participante, null, null).Count;

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;

                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + brand;
                    BEntity.JsonFormat = "{ \"Texto\" : \"" + brand + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos;
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
                    return this.getProductByEditionShortDetailInfoList(code, countryId, editionId, productList, totalRows);

                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionDetailInfo> getProductByEditionDetailInfoList(string code, byte countryId, int editionId, List<ProductByEditionInfo> productList)
        {
            List<ProductByEditionDetailInfo> productDetailList = new List<ProductByEditionDetailInfo>();

            // Iterate over the returned list:
            foreach (ProductByEditionInfo product in productList)
            {
                ProductByEditionDetailInfo productDetail = new ProductByEditionDetailInfo();

                // Brand detail:
                productDetail.ProductId = product.ProductId;
                productDetail.Brand = product.Brand;
                productDetail.CategotyId = product.CategotyId;
                productDetail.CategoryName = product.CategoryName;
                productDetail.DivisionId = product.DivisionId;
                productDetail.DivisionName = product.DivisionName;
                productDetail.DivisionShortName = product.DivisionShortName;
                productDetail.PharmaFormId = product.PharmaFormId;
                productDetail.PharmaForm = product.PharmaForm;

                //Presentations
                productDetail.Presentations = PresentationsBLC.Instance.getPresentationByProduct(code, editionId, product.DivisionId, product.CategotyId, product.ProductId, product.PharmaFormId);
                
                // Substances:
                productDetail.Substances = SubstancesBLC.Instance.getSubstancesByDrug(code, countryId, editionId, product.ProductId);

                // Indications:
                productDetail.Indications = IndicationsBLC.Instance.getIndicationsByDrug(code, countryId, editionId, product.ProductId);

                // Therapeutics:
                productDetail.Therapeutics = TherapeuticBLC.Instance.getByDrugByPharmaForm(code, countryId, editionId, product.ProductId, product.PharmaFormId);

                //ICDs:
                productDetail.ICDs = ICDBLC.Instance.getICDByDrugs(code, editionId, product.ProductId, product.PharmaFormId);

                // Add current object to final list:
                productDetailList.Add(productDetail);
            }

            return productDetailList;
        }

        public List<ProductByEditionShortDetailInfo> getProductByEditionShortDetailInfoList(string code, byte countryId, int editionId, List<ProductByEditionInfo> productList, int? totalRows)
        {
            List<ProductByEditionShortDetailInfo> productDetailList = new List<ProductByEditionShortDetailInfo>();

            // Iterate over the returned list:
            foreach (ProductByEditionInfo product in productList)
            {
                ProductByEditionShortDetailInfo productDetail = new ProductByEditionShortDetailInfo();

                // Brand detail:
                productDetail.ProductId = product.ProductId;
                productDetail.Brand = product.Brand;
                productDetail.CategotyId = product.CategotyId;
                productDetail.CategoryName = product.CategoryName;
                productDetail.DivisionId = product.DivisionId;
                productDetail.DivisionName = product.DivisionName;
                productDetail.DivisionShortName = product.DivisionShortName;
                productDetail.PharmaFormId = product.PharmaFormId;
                productDetail.PharmaForm = product.PharmaForm;
                
                //Total Rows
                if(totalRows != null)
                    productDetail.TotalRows = totalRows;

                //Presentations
                productDetail.Presentations = PresentationsBLC.Instance.getPresentationByProduct(code, editionId, product.DivisionId, product.CategotyId, product.ProductId, product.PharmaFormId);

                // Substances:
                productDetail.Substances = SubstancesBLC.Instance.getSubstancesByDrug(code, countryId, editionId, product.ProductId);
                                
                // Add current object to final list:
                productDetailList.Add(productDetail);
            }

            return productDetailList;
        }

        #endregion

        #region Active Substance

        public List<ProductByEditionInfo> getDrugsBySubstance(string code, byte countryId, int editionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList = 
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByActiveSubstance(countryId, editionId, activeSubstanceId, (byte)Catalogs.TypeInEdition.Participante);
                    
                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Sustancia\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString()+"\" }";
                    if (productList.Count > 0 )
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
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

        public List<ProductByEditionDetailInfo> getDrugsDetailBySubstance(string code, byte countryId, int editionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByActiveSubstance(countryId, editionId, activeSubstanceId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                                        
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Sustancia\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
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

        public List<ProductByEditionShortDetailInfo> getDrugsShortDetailBySubstance(string code, byte countryId, int editionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByActiveSubstance(countryId, editionId, activeSubstanceId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Sustancia\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
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
                    return ProductsBLC.Instance.getProductByEditionShortDetailInfoList(code, countryId, editionId, productList, null);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region Content

        public MedinetBusinessEntries.ParticipantProductsInfo getContent(string code, int editionId, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            if (editionId <= 0 || productId <= 0 || pharmaFormId <= 0 || divisionId <= 0 || categoryId <= 0)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return this.addUrl(PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getContent(editionId, productId, pharmaFormId, divisionId, categoryId), 
                        editionId, System.Configuration.ConfigurationManager.AppSettings["hashkey"], code);
                
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public PharmaSearchEngineBusinessEntries.ProductByEditionInfo getInformation(string code, int editionId, int divisionId,int categoryId,int pharmaFormId,int productId)

        {
            if (editionId <= 0 || productId <= 0 || pharmaFormId <= 0 || divisionId <= 0 || categoryId <= 0)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getInformation(editionId, divisionId, categoryId, pharmaFormId, productId);

                else throw new ApplicationException("El código no es válido o se encuentra inactivo.");


            }

        }

        public PharmaSearchEngineBusinessEntries.ProductByEditionShortDetailInfo getInformationShortDetail(string code, int editionId, int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            if (editionId <= 0 || productId <= 0 || pharmaFormId <= 0 || divisionId <= 0 || categoryId <= 0)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    ProductByEditionInfo product = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getInformation(editionId, divisionId, categoryId, pharmaFormId, productId);

                    ProductByEditionShortDetailInfo productDetail = new ProductByEditionShortDetailInfo();

                    // Brand detail:
                    productDetail.ProductId = product.ProductId;
                    productDetail.Brand = product.Brand;
                    productDetail.CategotyId = product.CategotyId;
                    productDetail.CategoryName = product.CategoryName;
                    productDetail.DivisionId = product.DivisionId;
                    productDetail.DivisionName = product.DivisionName;
                    productDetail.DivisionShortName = product.DivisionShortName;
                    productDetail.PharmaFormId = product.PharmaFormId;
                    productDetail.PharmaForm = product.PharmaForm;

                    //Presentations
                    productDetail.Presentations = PresentationsBLC.Instance.getPresentationByProduct(code, editionId, product.DivisionId, product.CategotyId, product.ProductId, product.PharmaFormId);

                    // Substances:
                    productDetail.Substances = SubstancesBLC.Instance.getSubstancesByDrug(code, MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId, editionId, product.ProductId);

                    return productDetail;
                }
                else 
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                
            }

        }

        public bool checkContent(string code, int editionId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.checkContent(editionId, productId, pharmaFormId);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region Partipant products

        public int getNumberProducts(string code, int editionId, byte countryId)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAll(editionId, countryId);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region General search

        public SearchResultsInfo getResults(string code, byte countryId, int editionId, string searchText)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                SearchResultsInfo record = new SearchResultsInfo();

                //Product Results
                List<ProductByEditionInfo> productList = new List<ProductByEditionInfo>();
                productList = getDrugs(code, countryId, editionId, searchText);
                if (productList.Count > 0)
                    record.Products = productList;

                //Laboratory Results
                List<DivisionByEditionInfo> divisionList = new List<DivisionByEditionInfo>();
                divisionList = PharmaSearchEngineBusinessLogicComponent.LaboratoriesBLC.Instance.getLabs(code, countryId, editionId, searchText);
                if (divisionList.Count > 0)
                    record.Labs = divisionList;

                //Substance Results
                List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
                substanceList = SubstancesBLC.Instance.getAll(code, countryId, editionId, searchText);
                if (substanceList.Count > 0)
                    record.Substances = substanceList;

                //Indication Results
                List<MedinetBusinessEntries.IndicationInfo> indicationList = new List<MedinetBusinessEntries.IndicationInfo>();
                indicationList = PharmaSearchEngineBusinessLogicComponent.IndicationsBLC.Instance.getIndications(code, countryId, editionId, searchText);
                if (indicationList.Count > 0)
                    record.Indications = indicationList;

                //ICD Results

                List<MedinetBusinessEntries.ICDInfo> ICDList = new List<MedinetBusinessEntries.ICDInfo>();
                ICDList = PharmaSearchEngineBusinessLogicComponent.ICDBLC.Instance.getICDByText(code, editionId,searchText);
                if (ICDList.Count > 0)
                    record.ICD = ICDList;

               

                return record;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public SearchResultsDetailInfo getResultsDetail(string code, byte countryId, int editionId, string searchText)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                SearchResultsDetailInfo record = new SearchResultsDetailInfo();

                //Product Results
                List<ProductByEditionDetailInfo> productList = new List<ProductByEditionDetailInfo>();
                productList = getDrugsDetail(code, countryId, editionId, searchText);
                if (productList.Count > 0)
                    record.Products = productList;

                //Laboratory Results
                List<DivisionByEditionInfo> divisionList = new List<DivisionByEditionInfo>();
                divisionList = PharmaSearchEngineBusinessLogicComponent.LaboratoriesBLC.Instance.getLabs(code, countryId, editionId, searchText);
                if (divisionList.Count > 0)
                    record.Labs = divisionList;

                //Substance Results
                List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
                substanceList = SubstancesBLC.Instance.getAll(code, countryId, editionId, searchText);
                if (substanceList.Count > 0)
                    record.Substances = substanceList;

                //Indication Results
                List<MedinetBusinessEntries.IndicationInfo> indicationList = new List<MedinetBusinessEntries.IndicationInfo>();
                indicationList = PharmaSearchEngineBusinessLogicComponent.IndicationsBLC.Instance.getIndications(code, countryId, editionId, searchText);
                if (indicationList.Count > 0)
                    record.Indications = indicationList;

                //ICD Results

                List<MedinetBusinessEntries.ICDInfo> ICDList = new List<MedinetBusinessEntries.ICDInfo>();
                ICDList = PharmaSearchEngineBusinessLogicComponent.ICDBLC.Instance.getICDByText(code, editionId, searchText);
                if (ICDList.Count > 0)
                    record.ICD = ICDList;

                return record;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public SearchResultsShortDetailInfo getResultsShortDetail(string code, byte countryId, int editionId, string searchText)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                SearchResultsShortDetailInfo record = new SearchResultsShortDetailInfo();

                //Product Results
                List<ProductByEditionShortDetailInfo> productList = new List<ProductByEditionShortDetailInfo>();
                productList = getDrugsShortDetail(code, countryId, editionId, searchText, null, null);
                if (productList.Count > 0)
                    record.Products = productList;

                //Laboratory Results
                List<DivisionByEditionInfo> divisionList = new List<DivisionByEditionInfo>();
                divisionList = PharmaSearchEngineBusinessLogicComponent.LaboratoriesBLC.Instance.getLabs(code, countryId, editionId, searchText);
                if (divisionList.Count > 0)
                    record.Labs = divisionList;

                //Substance Results
                List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
                substanceList = SubstancesBLC.Instance.getAll(code, countryId, editionId, searchText);
                if (substanceList.Count > 0)
                    record.Substances = substanceList;

                //ICD Results

                List<MedinetBusinessEntries.ICDInfo> ICDList = new List<MedinetBusinessEntries.ICDInfo>();
                ICDList = PharmaSearchEngineBusinessLogicComponent.ICDBLC.Instance.getICDByText(code, editionId, searchText);
                if (ICDList.Count > 0)
                    record.ICD = ICDList;

                return record;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        #endregion

        #region Product Content

        public ProductDetailInfo getCompleteProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, string applicationKey, MedinetBusinessEntries.Catalogs.TargetOutputs targetId)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                ProductDetailInfo product = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getProductAttributes(editionId, divisionId, categoryId, productId, pharmaFormId);

                if (product != null)
                {
                    MedinetBusinessEntries.EditionsInfo beEdtion = MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId);

                    MedinetBusinessEntries.BooksInfo beBook = MedinetDataAccessComponent.BooksDALC.Instance.getOne(beEdtion.BookId);

                    product.Substances = SubstancesBLC.Instance.getSubstancesByDrug(code, MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId, editionId, productId);

                    product.Attributes = PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getAttributes(editionId, divisionId, categoryId, productId, pharmaFormId, targetId);
                    //Cambio HTMLContent
                        foreach ( PharmaSearchEngineBusinessEntries.AttributeDetailInfo html in product.Attributes)        
                        {
                            html.HTMLContent = html.HTMLContent.Replace("img/", PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                                MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName + "/" +
                                beBook.ShortName + beEdtion.NumberEdition.ToString() + "/img/");                         
                        }       
         
                    product.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName +
                        "/" + beBook.ShortName + beEdtion.NumberEdition.ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]
                        + System.Configuration.ConfigurationManager.AppSettings["DefaultResolutionUrl"];

                    if (string.IsNullOrEmpty(product.ProductShot))
                        product.ProductShot = "plm.jpg";
                }
            
                PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                if(product != null)
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido;
                else
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;

                string category = MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(categoryId).Description.ToString();

                BEntity.SearchParameters="DivisionId=" + divisionId.ToString() + ";CategoryId=" + categoryId.ToString() + ";ProductId=" + productId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString();
                BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName + "\",\"Categoria\":\"" + category +"\",\"Producto\":\"" +product.Brand + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm+ "\"}";
                this._PLMLogs.addLogParentActivity(BEntity);

                return product;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public PharmaSearchEngineBusinessEntries.ProductByCodeInfo getProducByCode(string code, string barCode, string applicationKey)
        {
             PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

             if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
             {
                MedinetBusinessEntries.ProductCategoriesInfo product = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getproductByCode(barCode);

                MedinetBusinessEntries.EditionsInfo editionId = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getMaxEdition(product.ProductId, product.PharmaFormId, product.CategoryId, product.DivisionId);
                
                if (editionId != null)
                {
                    MedinetBusinessEntries.EditionsInfo medition = MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(editionId.ISBN);

                    MedinetBusinessEntries.BooksInfo beBook = MedinetDataAccessComponent.BooksDALC.Instance.getOne(medition.BookId);

                    PLMClientsBusinessEntities.CodePrefixInfo bePrefix = PLMClientsBusinessLogicComponent.CodePrefixesBLC.Instance.getByCode(code);

                    if (product != null)
                    {
                        PharmaSearchEngineBusinessEntries.ProductByCodeInfo productCode = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getProductAttribute(editionId.EditionId, product.DivisionId, product.CategoryId, product.ProductId, product.PharmaFormId);

                        if (productCode != null)
                        {
                            productCode.Attributes = PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getCompleteAttributes(editionId.EditionId, product.DivisionId, product.CategoryId, product.ProductId, product.PharmaFormId, bePrefix.PrefixName);

                            productCode.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                                    MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId.EditionId).CountryId).CountryName +
                                    "/" + beBook.ShortName + editionId.NumberEdition.ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]
                                    + System.Configuration.ConfigurationManager.AppSettings["DefaultResolutionUrl"];

                            productCode.Substances = MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByProduct(product.ProductId);

                            if (string.IsNullOrEmpty(productCode.ProductShot))
                                productCode.ProductShot = "plm.jpg";
                        }

                        return productCode;
                    }

                    PharmaSearchEngineBusinessEntries.ProductByEditionInfo productByEditionInfo = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getOne(product.ProductId);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId.EditionId;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    if (product != null)
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido;
                    else
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;

                    BEntity.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString() + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + productByEditionInfo.DivisionName.ToString() + "\",\"Categoria\":\"" + productByEditionInfo.CategoryName.ToString() +
                                "\",\"Producto\":\"" + productByEditionInfo.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + productByEditionInfo.PharmaForm.ToString() + "\"}";
                    this._PLMLogs.addLogParentActivity(BEntity);

                    
                }
                else
                    throw new ApplicationException("No se encontraron productos.");
                 return null;
             }
             else
                 throw new ApplicationException("El código no es válido o se encuentra inactivo.");

        }

        public ProductDetailInfo getAllAttributesByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, string resolutionKey, string applicationKey)
        {
            if (resolutionKey == null)
                throw new ArgumentException("The resolution does not exist.");

            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                ProductDetailInfo product = PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getProductAttributes(editionId, divisionId, categoryId, productId, pharmaFormId);

                if (product != null)
                {
                    MedinetBusinessEntries.EditionsInfo beEdtion = MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId);
                    MedinetBusinessEntries.BooksInfo beBook = MedinetDataAccessComponent.BooksDALC.Instance.getOne(beEdtion.BookId);
                    PLMClientsBusinessEntities.CodePrefixInfo bePrefix = PLMClientsBusinessLogicComponent.CodePrefixesBLC.Instance.getByCode(code);
                    //Presentations
                    product.Presentations = PharmaSearchEngineBusinessLogicComponent.PresentationsBLC.Instance.getPresentationByProduct(code, editionId, divisionId, categoryId, productId, pharmaFormId);
                    product.Substances = SubstancesBLC.Instance.getSubstancesByDrug(code, MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId, editionId, productId);
                    product.Therapeutics = TherapeuticBLC.Instance.getByDrugByPharmaForm(code, MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId, editionId, productId, pharmaFormId);
                    product.Attributes = PharmaSearchEngineDataAccessComponent.AttributesDALC.Instance.getCompleteAttributes(editionId, divisionId, categoryId, productId, pharmaFormId, bePrefix.PrefixName);
//Cambio HTMLContent
                        foreach (PharmaSearchEngineBusinessEntries.AttributeDetailInfo html in product.Attributes)
                        {
                            html.HTMLContent = html.HTMLContent.Replace("img/", PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                                MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName + "/" +
                                beBook.ShortName + beEdtion.NumberEdition.ToString() + "/img/");
                        } 
              
                    product.ProductInteractions = new ProductInteractionsInfo();

                    //Interaction Substances
                    product.ProductInteractions.InteractionSubstances =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getInteractionSubstances(editionId, divisionId, categoryId, productId, pharmaFormId);
                    //Pharmacological Groups
                    product.ProductInteractions.PharmacologicalGroups =
                        PharmaSearchEngineDataAccessComponent.PharmacologicalGroupsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
                    //Other interactions
                    product.ProductInteractions.OtherInteractions =
                        PharmaSearchEngineDataAccessComponent.OtherElementsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);

                    //Product Reference Url
                    if (product.ReferenceUrl != null)
                        product.ReferenceUrl = this.addReferenceUrl(beBook, product.ReferenceUrl);

                    //Add BaseUrl
                    product.BaseUrl = this.addBaseUrl(applicationKey, editionId, beBook, beEdtion, product.ProductShot, product.DivisionImage, resolutionKey);

                    //Add ProductShot
                    if (string.IsNullOrEmpty(product.ProductShot))
                    {
                        if (string.IsNullOrEmpty(product.DivisionImage))
                            product.ProductShot = System.Configuration.ConfigurationManager.AppSettings["DefaultImage"];
                        else
                            product.ProductShot = product.DivisionImage;
                    }
                }

                string category = MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(categoryId).Description.ToString();

                PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido;
                BEntity.SearchParameters = "DivisionId=" + divisionId.ToString() + ";CategoryId=" + categoryId.ToString() + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + category +
                            "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                this._PLMLogs.addLogParentActivity(BEntity);

                return product;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        #endregion

        #region Symptoms

        public List<ProductByEditionInfo> getDrugsBySymptom(string code, int editionId, int symptomId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The book edition does not exist");

            else if (symptomId <= 0)
                throw new ArgumentException("The symptom does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getBySymptoms(editionId, symptomId);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "SymptomId=" + symptomId.ToString();
                    BEntity.JsonFormat = "{\"Sintoma\":\""+MedinetDataAccessComponent.SymptomsDALC.Instance.getOne(symptomId).SymptomName.ToString()+"\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos_por_Sintoma;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos_por_Sintoma;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategotyId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                          "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";    
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }                    else
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

        #endregion

        #endregion

        #region Private Methods

        private MedinetBusinessEntries.ParticipantProductsInfo addUrl(MedinetBusinessEntries.ParticipantProductsInfo content, int editionId, string applicationKey, string code)
        {
            if (content != null)
            {
                if (content.EditionId.ToString() == System.Configuration.ConfigurationManager.AppSettings["RemoveImages"])
                    content.HTMLContent = this.removeImages(content.HTMLContent);

                else
                {
                    MedinetBusinessEntries.EditionsInfo beEdtion = MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId);
                    MedinetBusinessEntries.BooksInfo beBook = MedinetDataAccessComponent.BooksDALC.Instance.getOne(beEdtion.BookId);
                    
                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido;
                    BEntity.SearchParameters = "DivisionId=" + content.DivisionId.ToString() + ";CategoryId=" + content.CategoryId.ToString() + ";ProductId=" + content.ProductId.ToString() + ";PharmaFormId=" + content.PharmaFormId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(content.DivisionId).ShortName.ToString() + "\",\"Categoria\":\"" + MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(content.CategoryId).Description.ToString() +
                                "\",\"Producto\":\"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(content.ProductId).Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(content.PharmaFormId).Description.ToString() + "\"}";

                    this._PLMLogs.addLogParentActivity(BEntity);

                    content.HTMLContent = content.HTMLContent.Replace("img/", PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                            MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName + "/" +
                            beBook.ShortName + beEdtion.NumberEdition.ToString() + "/img/");
                }
            }
            return content;
        }

        private string removeImages(string content)
        {
            int ini, fin;
            string cadena;

            ini = fin = 0;

            if (content.IndexOf("<img") != -1)
                while (content.IndexOf("<img") != -1)
                {
                    ini = content.IndexOf("<img");
                    fin = content.IndexOf(">", ini);

                    cadena = content.Substring(ini, ((fin + 1) - ini));

                    content = content.Replace(cadena, "");
                }
            return content;
        }

        private string addReferenceUrl(MedinetBusinessEntries.BooksInfo beBook, string referenceUrl)
        {
            if (beBook.BookId == (int)MedinetBusinessEntries.Catalogs.Books.OTC)
                referenceUrl = System.Configuration.ConfigurationManager.AppSettings["WebPageOTC"] + referenceUrl;

            else if (beBook.BookId == (int)MedinetBusinessEntries.Catalogs.Books.VMG)
                referenceUrl = System.Configuration.ConfigurationManager.AppSettings["WebPageVMG"] + referenceUrl;

            else
                referenceUrl = System.Configuration.ConfigurationManager.AppSettings["WebPageDEF"] + referenceUrl;

            return referenceUrl;
        }

        private string addBaseUrl(string applicationKey, int editionId, MedinetBusinessEntries.BooksInfo beBook, MedinetBusinessEntries.EditionsInfo beEdtion,
            string productShot, string divisionImage, string resolutionKey)
        {
            string baseUrl;

            if (beEdtion.ParentId == null)
            {
                baseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                    MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName +
                    "/" + beBook.ShortName + beEdtion.NumberEdition.ToString() + "/";
            }
            else
            {
                MedinetBusinessEntries.EditionsInfo beParentEdition = MedinetDataAccessComponent.EditionsDALC.Instance.getOne((int)beEdtion.ParentId);

                if (beParentEdition.ParentId != null)
                {
                    do
                    {
                        beParentEdition = MedinetDataAccessComponent.EditionsDALC.Instance.getOne((int)beParentEdition.ParentId);
                    }
                    while (beParentEdition.ParentId != null);
                }
                
                MedinetBusinessEntries.BooksInfo beParentBook = MedinetDataAccessComponent.BooksDALC.Instance.getOne(beParentEdition.BookId);

                baseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                    MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(beParentEdition.EditionId).CountryId).CountryName +
                    "/" + beParentBook.ShortName + beParentEdition.NumberEdition.ToString() + "/";
            }

            if (string.IsNullOrEmpty(productShot))
            {
                if (string.IsNullOrEmpty(divisionImage))
                    baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];

                else
                    baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["DivisionBaseUrl"];
            }
            else
            {
                baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
            }

            if (resolutionKey == "")
                baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["DefaultResolutionUrl"];
            else
                baseUrl = baseUrl + PLMClientsBusinessLogicComponent.ResolutionScreensBLC.Instance.getByResolutionKey(resolutionKey).BaseUrl + "/";

            return baseUrl;
        }
        
        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly ProductsBLC Instance = new ProductsBLC();

    }
}
