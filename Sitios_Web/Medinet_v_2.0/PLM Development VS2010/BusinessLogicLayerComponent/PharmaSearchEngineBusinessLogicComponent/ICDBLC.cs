using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class ICDBLC
    {
        #region Constructors

        private ICDBLC(){}


        #endregion


        #region Public Methods


        public MedinetBusinessEntries.ICDInfo getICD(string code, int icdId)
        {
          if(icdId <= 0)
              throw new ArgumentException("The ICD does not exist");

          else
            {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
              if(validCode!=null && validCode.CodeStatusId==PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                  return MedinetDataAccessComponent.ICDDALC.Instance.getOne(icdId);
               
              else
                  throw new ApplicationException("The code is not valid or is inactive");
            }

          
          }

        public List<MedinetBusinessEntries.ICDInfo> getICDByText(string code,int editionId,string search)
        {
            if(editionId<=0 || string.IsNullOrEmpty(search))
                throw new ArgumentException("The edition  or search does not exist");
                
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if(validCode !=null && validCode.CodeStatusId==PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                {
                    List<MedinetBusinessEntries.ICDInfo> ICDList = MedinetDataAccessComponent.ICDDALC.Instance.getICDByText(editionId,search);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString=code;
                    BEntity.EditionId=editionId;
                    BEntity.SourceId=Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte) PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    BEntity.SearchDate= null;
                    BEntity.SearchParameters= "Text="+ search;
                    BEntity.JsonFormat= "{\"Texto\":\" " +search +"\"}";

                    if(ICDList.Count != 0)
                    {
                        BEntity.EntityId= (int)PSELogsBusinessEntities.Catalogs.Entities.ICD;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ICDInfo ICD in ICDList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString=code;
                            sonTracking.EditionId=editionId;
                            sonTracking.SourceId=Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte) PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int) PSELogsBusinessEntities.Catalogs.Entities.ICD;
                            sonTracking.SearchDate =null;
                            sonTracking.SearchParameters ="ICDId"+ICD.ICDId.ToString();
                            sonTracking.JsonFormat="{\"ICD\": \""+ICD.SpanishDescription.ToString()+"\"}";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }

                    }
                    else
                        BEntity.EntityId=(byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;

                        this._PLMLogs.addLogParentActivity(BEntity);
                

                    return ICDList;

                }
                else
                    throw new ApplicationException("The code is not valid or is inactive");

               
            }

         }

        public List<MedinetBusinessEntries.ICDInfo>getICDByDrugs(string code,int editionId, int productId, int pharmaFormId)

        {
            if(editionId<=0 || productId<=0)
                throw new ArgumentException("The edition  or product does not exist");


            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if(validCode !=null && validCode.CodeStatusId==PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                {
                    List<MedinetBusinessEntries.ICDInfo> ICDList = MedinetDataAccessComponent.ICDDALC.Instance.getICDByProduct(editionId,productId, pharmaFormId);

                    MedinetBusinessEntries.ProductsInfo product = MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId);


                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ProductId=" + productId.ToString();
                    BEntity.JsonFormat = "{ \"Producto\" : \" " + product.Brand.ToString() + "\" }";

                    if (ICDList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ICD_Med;
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (MedinetBusinessEntries.ICDInfo ICD in ICDList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.ICD_Med;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "ICDId=" + ICD.ICDId.ToString();
                            sonTracking.JsonFormat = "{ \"ICD \" : \"" + ICD.SpanishDescription.ToString() + "\" }";
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);

                            

                        }

                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);
                    return ICDList;

                }
                else
                    throw new ApplicationException("The code is not valid or is inactive");
   
            }



        }

        public List<ProductByEditionInfo> getDrugsByICD(string code ,byte countryId,int editionId,int icdId)
           
          {
              if (countryId == 0 || editionId <= 0 || icdId<=0)
                throw new ArgumentException("The country, book edition or ICD does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByICD(countryId,editionId,icdId,(byte)Catalogs.TypeInEdition.Participante);

                    
                     PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;                 
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ICDId=" + icdId.ToString();
                    BEntity.JsonFormat = 
                        "{ \"ICD \" : \"" + MedinetDataAccessComponent.ICDDALC.Instance.getOne(icdId).SpanishDescription+ "\"}";

                    if(productList.Count != 0)
                    {

                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;                        
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach(PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {

                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;
                            sonTracking.SearchDate=null;
                            sonTracking.SearchParameters= "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategotyId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                             sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                          "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";

                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                      }

                    }

                    else
                    {
                        BEntity.EntityId=(byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }


                    this._PLMLogs.addLogParentActivity(BEntity);
                    return productList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }



           }

        public List<ProductByEditionDetailInfo> getDrugsDetail(string code, byte countryId, int editionId, int icdId)
        {


            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country, book edition or indication does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByICD(countryId, editionId, icdId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ICDId=" + icdId.ToString();
                    BEntity.JsonFormat = "{ \"ICD \" : \"" + MedinetDataAccessComponent.ICDDALC.Instance.getOne(icdId).SpanishDescription + "\" }";
                    if (productList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;
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

        public List<ProductByEditionShortDetailInfo> getDrugsShortDetail(string code, byte countryId, int editionId, int icdId)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country, book edition or indication does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductByEditionInfo> productList =
                        PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByICD(countryId, editionId, icdId, (byte)Catalogs.TypeInEdition.Participante);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "ICDId=" + icdId.ToString();
                    BEntity.JsonFormat = "{ \"ICD \" : \"" + MedinetDataAccessComponent.ICDDALC.Instance.getOne(icdId).SpanishDescription + "\" }";
                    if (productList.Count != 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in productList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_ICD;
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


        #region Private Instances

         private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

      public static readonly ICDBLC Instance = new ICDBLC();

    }
}
