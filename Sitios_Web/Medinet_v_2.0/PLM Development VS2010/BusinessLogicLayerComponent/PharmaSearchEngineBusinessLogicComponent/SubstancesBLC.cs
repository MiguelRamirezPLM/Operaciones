using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class SubstancesBLC
    {

        #region Constructors

        private SubstancesBLC() { }

        #endregion

        #region Public Methods

        //Alphabetic
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getAll(string code, byte countryId, int editionId, string description)
        {
            if (countryId <= 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getAll(countryId, editionId, description, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                                        
                   
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + description;
                    BEntity.JsonFormat = "{ \"Texto\" : \"" + description.ToString() + "\" }";
                    if (substanceList.Count > 0)
                    {
                         BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancias;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in substanceList)
                        {

                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancias;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ActiveSubstanceId=" + substance.ActiveSubstanceId.ToString();
                            sonTracking.JsonFormat = "{ \"Sustancia\" : \"" + substance.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return substanceList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //therapeutic
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByTherapeutic(string code, byte countryId, int editionId, int therapeuticId)
        {
            if (countryId <= 0 || editionId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition or therapeutic does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "TherapeuticId=" + therapeuticId.ToString();
                    BEntity.JsonFormat = "{ \"Terapeutico\" : \"" + MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId).Description.ToString() + "\" }";
                    if (substanceList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_ATC;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in substanceList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_ATC;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ActiveSubstanceId=" + substance.ActiveSubstanceId.ToString();
                            sonTracking.JsonFormat = "{ \"Sustancia\" : \"" + substance.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return substanceList;

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_ATC, "TherapeuticId=" + therapeuticId.ToString());

                    //return PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId, (byte)Catalogs.TypeInEdition.Participante);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionInfo> getAloneDrugsBySubstance(string code, byte countryId, int editionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAloneByActiveSubstance(countryId, editionId, activeSubstanceId, true, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                   
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Sustancia\" : \"" +MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString()  + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Sust;  
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Sust;
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

        public List<ProductByEditionInfo> getCombinedDrugsBySubstance(string code, byte countryId, int editionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getCombinedByActiveSubstance(countryId, editionId, activeSubstanceId, false, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Sustancia\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\" }";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Sust;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Sust;
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

        public List<ProductByEditionInfo> getAloneDrugsByTheraBySubstance(string code, byte countryId, int editionId, int therapeuticId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition, substance or therapeutic does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAloneByTherapeuticByActiveSubstance(countryId, editionId, therapeuticId, activeSubstanceId, true, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "TherapeuticId=" + therapeuticId.ToString() + ";ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Terapeutico\" : \"" + MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId).Description.ToString() +
                        "\",\"Sustancia\":\"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\"}";

                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_ATC_Sust;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_ATC_Sust;
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

        public List<ProductByEditionInfo> getCombinedDrugsByTheraBySubstance(string code, byte countryId, int editionId, int therapeuticId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || activeSubstanceId <= 0 || therapeuticId <= 0)
                throw new ArgumentException("The country, book edition, substance or therapeutic does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getCombinedByTherapeuticByActiveSubstance(countryId, editionId, therapeuticId, activeSubstanceId, false, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "TherapeuticId=" + therapeuticId.ToString() + ";ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Terapeutico\" : \"" + MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId).Description.ToString() +
                        "\",\"Sustancia\":\"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\"}";

                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_ATC_Sust;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_ATC_Sust;
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

        //labs
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByDivision(string code, byte countryId, int editionId, int divisionId)
        {
            if (countryId <= 0 || editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The country, book edition or division does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByDivision(countryId, editionId, divisionId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "DivisionId=" + divisionId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio \" : \"" + MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId).Description.ToString() + "\" }";

                    if (substanceList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Lab;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in substanceList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Lab;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ActiveSubstanceId=" + substance.ActiveSubstanceId.ToString();
                            sonTracking.JsonFormat = "{ \"Sustancia \" : \"" + substance.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return substanceList;

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Lab, "DivisionId=" + divisionId.ToString());

                    //return PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByDivision(countryId, editionId, divisionId, (byte)Catalogs.TypeInEdition.Participante);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesByDrug(string code, byte countryId, int editionId, int productId)
        {
            if (countryId <= 0 || editionId <= 0 || productId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ProductId=" + productId.ToString();
                    BEntity.JsonFormat = "{ \"Producto \" : \"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId).Brand.ToString() + "\" }";
                    if (substanceList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in substanceList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ActiveSubstanceId=" + substance.ActiveSubstanceId.ToString();
                            sonTracking.JsonFormat = "{ \"Sustancia \" : \"" + substance.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return substanceList;

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med, "ProductId=" + productId.ToString());

                    //return PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesByDrug(string code, byte countryId, int editionId, int productId, int activeSubstanceId)
        {
            if (countryId <= 0 || editionId <= 0 || productId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList =
                        PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByProductWithoutActiveSubstance(countryId, editionId, productId, activeSubstanceId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                   
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ProductId=" + productId.ToString() + ";ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Producto\" : \"" + MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId).Description.ToString() +
                                 "\",\"Sustancia\":\"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\"}";
                    if (substanceList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med_Sust;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in substanceList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med_Sust;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ActiveSubstanceId=" + substance.ActiveSubstanceId.ToString();
                            sonTracking.JsonFormat = "{ \"Sustancia \" : \"" + substance.Description.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return substanceList;

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), 
                    //    (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med, "ProductId=" + productId.ToString());

                    //return PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getByProductWithoutActiveSubstance(countryId, editionId, productId, activeSubstanceId, (byte)Catalogs.TypeInEdition.Participante);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<ProductByEditionInfo> getAloneDrugsByDivisionBySubstance(string code, byte countryId, int editionId, int divisionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || divisionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition, laboratorie or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getAloneByDivisionByActiveSubstance(countryId, editionId, divisionId, activeSubstanceId, true, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "DivisionId=" + divisionId.ToString() + ";ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId).ShortName.ToString() +
                                 "\",\"Sustancia\":\"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\"}";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Lab_Sust;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Lab_Sust;
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

        public List<ProductByEditionInfo> getCombinedDrugsByDivisionBySubstance(string code, byte countryId, int editionId, int divisionId, int activeSubstanceId)
        {
            if (countryId == 0 || editionId <= 0 || divisionId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The country, book edition, laboratorie or substance does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getCombinedByDivisionByActiveSubstance(countryId, editionId, divisionId, activeSubstanceId, false, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "DivisionId=" + divisionId.ToString() + ";ActiveSubstanceId=" + activeSubstanceId.ToString();
                    BEntity.JsonFormat = "{ \"Laboratorio\" : \"" + MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId).ShortName.ToString() +
                                 "\",\"Sustancia\":\"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\"}";
                    if (productList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Lab_Sust;
                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Lab_Sust;
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

        public MedinetBusinessEntries.ActiveSubstanceInfo getSubstance(string code, int activeSubstanceId)
        {
            if (activeSubstanceId <= 0)
                throw new ArgumentException("The substance do not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly SubstancesBLC Instance = new SubstancesBLC();
    }
}
