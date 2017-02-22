using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessLogicComponent;
using PLMClientsBusinessEntities;
using PharmaSearchEngineBusinessLogicComponent;
using DEAQBusinessEntries;
using AgroBusinessEntries;

namespace AgroBusinessLogicComponent
{
    public class ProductsBLC
    {

        #region Constructor

        private ProductsBLC() { }

        #endregion

        #region Public Methods

        #region ROC Methods
        //Retrieves information about MentionatedProduct by Edition and ProductId
        public DEAQBusinessEntries.MentionatedProductInfo rocGetMentionatedProduct(string code, int editionId, int productId)
        {
            if (editionId < 1 || productId < 1)
            {
                throw new ArgumentException("The Edition or Product does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetMentionatedProduct(editionId, productId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves information about ParticipantProduct by Edition and ProductId
        public DEAQBusinessEntries.ParticipantProductInfo rocGetParticipantProduct(string code, int editionId, int productId)
        {
            if (editionId < 1 || productId < 1)
            {
                throw new ArgumentException("The Edition or Product does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DEAQBusinessEntries.ParticipantProductInfo product = DEAQDataAccessComponent.ProductsDALC.Instance.rocGetParticipantProduct(editionId, productId);
                   
                    if (product != null)
                    {
                        PLMClientsBusinessEntities.BookInfo bookinfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                        DEAQBusinessEntries.ROC.CountryByEditionInfo countryInfo = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryByEdition(product.EditionId);


                        if (!string.IsNullOrEmpty(product.HtmlContent))
                        {
                            product.HtmlContent = product.HtmlContent.Replace("src=\"", "src=\"" +

                                System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countryInfo.CountryName + "/" +  bookinfo.ShortName + "/img/");
                        }
                        
                           

                    }
                    return product;
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves information about Product by Edition and ProductId
        public DEAQBusinessEntries.ROC.ProductByEditionInfo rocGetProductById(string code, int editionId, int productId)
        {
            if (editionId < 1 || productId < 1)
            {
                throw new ArgumentException("The Edition or Product does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductById(editionId, productId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition and Country
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProducts(string code, int countryId, int editionId, int numberByPage, int page)
        {
            if (countryId < 1 || editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Country or Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProducts(countryId, editionId, numberByPage, page);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition and ActiveSubstance
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByActiveSubstance(string code, int activeSubstanceId, int editionId)
        {
            if (activeSubstanceId < 1 || editionId < 1)
            {
                throw new ArgumentException("The ActiveSubstance or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByActiveSubstance(activeSubstanceId, editionId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition and ActiveSubstance
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByCombinedActiveSubstance(string code, int activeSubstanceId, int editionId)
        {
            if (activeSubstanceId < 1 || editionId < 1)
            {
                throw new ArgumentException("The ActiveSubstance or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByCombinedActiveSubstance(activeSubstanceId, editionId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition and Crop
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByCrop(string code, int editionId, int cropId)
        {
            if (editionId < 1 || cropId < 1)
            {
                throw new ArgumentException("The Edition or Crop does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByCrop(editionId, cropId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition, Category and Division
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByDivisionAndCategory(string code, int editionId, int categoryId, int divisionId)
        {
            if (editionId < 1 || categoryId < 1 || divisionId < 1)
            {
                throw new ArgumentException("The Edition or Category or Division does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByDivisionAndCategory(editionId, categoryId, divisionId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Country, Edition, and FullText
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByFullText(string code, int countryId, int editionId, int numberByPage, int page, string fullText)
        {
            if (countryId < 1 || editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Country or Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByFullText(countryId, editionId, numberByPage, page, fullText);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Country, Edition, and letter
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByLetter(string code, int countryId, int editionId, int numberByPage, int page, string letter)
        {
            if (countryId < 1 || editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Country or Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByLetter(countryId, editionId, numberByPage, page, letter);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Country, Edition and Text
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByText(string code, int countryId, int editionId, int numberByPage, int page, string text)
        {
            if (countryId < 1 || editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Country or Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByText(countryId, editionId, numberByPage, page, text);
                }
                else 
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Country and Seed
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsBySeed(string code, int countryId, int seedId)
        {
            if (countryId < 1 || seedId < 1)
            {
                throw new ArgumentException("The Country or Seed does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsBySeed(countryId, seedId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Edition, Seed and Laboratory
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsBySeedAndLaboratory(string code, int editionId, int seedId, int laboratoryId)
        {
            if (editionId < 1 || seedId < 1 || laboratoryId < 1)
            {
                throw new ArgumentException("The Edition or Seed or Laboratory does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsBySeedAndLaboratory(editionId, seedId, laboratoryId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all Products by Use
        public List<DEAQBusinessEntries.ROC.ProductByUseInfo> rocGetProductsByUseId(string code, int useId, int countryId, int editionId)
        {
            if (useId <= 0 || countryId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The Edition or Seed or Laboratory does not exist.");
            }

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.rocGetProductsByUseId(useId, countryId, editionId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        #endregion

        #region PLM Methods
 
        #region Product

        public int addProduct(ProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.ProductId = AgroDataAccessComponent.ProductsDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ")";
            activityLog.FieldsAffected = "(LaboratoryId," + BEntity.LaboratoryId + ");(AlphabetId," + "aa" + ");(Brand,"
               + BEntity.ProductName + ");(SanitaryRegistry, " + BEntity.SanitaryRegistry + ");(Description," + BEntity.Description + ");(Active," + BEntity.Active + ")";

           // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            return BEntity.ProductId;
        }
        
        public List<ProductDetailByEditionInfo> getProductByEdition(string isbn, string code)
        {
            if (isbn == "")
                throw new ArgumentException("The isbn does not exist");
            else
            {
                List<ProductDetailByEditionInfo> productByEdition = new List<ProductDetailByEditionInfo>();

                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    productByEdition = DEAQDataAccessComponent.ProductsDALC.Instance.getAll(isbn);
                    
                    return productByEdition;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }
        public List<ProductsToEditInfo> getProductsByCountryAndDivision(int divisionId, int countryId)
        {
            if (divisionId <= 0 || countryId <= 0)
                throw new ArgumentException("The Division or Country does not exist.");

            return AgroDataAccessComponent.ProductsDALC.Instance.getProductsByCountryAndDivision(divisionId, countryId);
        }
        public List<ProductDetailByEditionInfo> getProductByActiveSubstance(string isbn, int activeSubstanceId, string code)
        {
            if (isbn == "" && activeSubstanceId != 0)
                throw new ArgumentException("The isbn or activeSubstance does not exist");                
            else
            {                
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductDetailByEditionInfo> productByActiveSubstance = DEAQDataAccessComponent.ProductsDALC.Instance.getByActiveSubstance(isbn, activeSubstanceId);

                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                    Track.JsonFormat = "{ \"Sustancia\" : \"" + ActiveSubstancesBLC.Instance.getById(isbn, activeSubstanceId, code).ActiveSubstanceName + "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productByActiveSubstance.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancias;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productByActiveSubstance)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Sustancia;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    
                    this.Tracking.addPLMTrackingParentActivity(Track);


                    return productByActiveSubstance;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
                /*PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId = 1;// Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                BEntity.SearchDate = null;
                BEntity.SearchParameters = "ActiveSubstanceId=" + activeSubstanceId.ToString();
                BEntity.JsonFormat = "{ \"Sustancia\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\" }";
                if (productByActiveSubstance.Count > 0)
                {
                    BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
                    BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                    PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                    foreach (ProductDetailByEditionInfo product in productByActiveSubstance)
                    {
                        sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                        sonTracking.CodeString = code;
                        sonTracking.EditionId = editionId;
                        sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                        sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                        sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
                        sonTracking.SearchDate = null;
                        sonTracking.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                            + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                        sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                        BEntity.ChildrenTrackingInfo.Add(sonTracking);
                    }
                }
                else
                {
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                }

                this._PLMLogs.add(BEntity);*/
            }
        }

        public List<ProductDetailByEditionInfo> getProductByCrop(string isbn, int cropId, string code)
        {
            if (isbn == "" && cropId != 0)
                throw new ArgumentException("The isbn or crop does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductDetailByEditionInfo> productByCrop = DEAQDataAccessComponent.ProductsDALC.Instance.getByCrop(isbn, cropId);

                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "CropId=" + cropId.ToString();
                    Track.JsonFormat = "{ \"Cultivo\" : \"" + CropsBLC.Instance.getById(isbn, cropId, code).CropName + "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productByCrop.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Cultivos;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productByCrop)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Cultivo;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    
                    
                    this.Tracking.addPLMTrackingParentActivity(Track);



                    return productByCrop;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
                

                /*PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                BEntity.CodeString = code;
                BEntity.EditionId = editionId;
                BEntity.SourceId = 1;// Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                BEntity.SearchDate = null;
                BEntity.SearchParameters = "CropId=" + cropId.ToString();
                BEntity.JsonFormat = "{ \"Cultivo\" : \"" + MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId).Description.ToString() + "\" }";
                if (productByCrop.Count > 0)
                {
                    BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
                    BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                    PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                    foreach (ProductDetailByEditionInfo product in productByCrop)
                    {
                        sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                        sonTracking.CodeString = code;
                        sonTracking.EditionId = editionId;
                        sonTracking.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                        sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                        sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust;
                        sonTracking.SearchDate = null;
                        sonTracking.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                            + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                        sonTracking.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                        BEntity.ChildrenTrackingInfo.Add(sonTracking);
                    }
                }
                else
                {
                    BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                }

                this._PLMLogs.add(BEntity);*/

            }
        }

        public List<ProductDetailByEditionInfo> getProductByDivision(string isbn, int divisionId, string code)
        {
            if (isbn == "" && divisionId != 0)
                throw new ArgumentException("The isbn or division does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.getByDivision(isbn, divisionId);
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
                
            }
        }

        public List<ProductDetailByEditionInfo> getProductByDivisionCategory(string isbn, int divisionId,int categoryId, string code)
        {
            if (isbn == "" && divisionId != 0&&categoryId != 0)
                throw new ArgumentException("The isbn or division or categoryId does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                   List<ProductDetailByEditionInfo> productByCategory= DEAQDataAccessComponent.ProductsDALC.Instance.getByDivisionCategory(isbn, divisionId, categoryId);
                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "CategoryId=" + categoryId.ToString()+";DivisionId="+divisionId.ToString();
                    Track.JsonFormat = "{ \"Categoria\" : \"" + CatalogsBLC.Instance.getCategoryById(code, categoryId).Description + "\",\"Laboratorio\" : \""+CatalogsBLC.Instance.getDivisionById(code,divisionId).DivisionName.ToString() +"\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productByCategory.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Categoria;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productByCategory)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Categoria;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);


                    return productByCategory;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }

        public ProductsInfo getProductsById(int productId,string code)
        {
            if (productId == 0)
                throw new ArgumentException("The product not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ProductsDALC.Instance.getById(productId);
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }

        public List<ProductDetailByEditionInfo> getProductsByCategory(string isbn, int categoryId, string code)
        {
            if (isbn == "" && categoryId != 0)
                throw new ArgumentException("The isbn or division or categoryId does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductDetailByEditionInfo> productByCategory =
                     DEAQDataAccessComponent.ProductsDALC.Instance.getByCategory(isbn, categoryId);
                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "CategoryId=" + categoryId.ToString();
                    Track.JsonFormat = "{ \"Categoria\" : \"" + CatalogsBLC.Instance.getCategoryById(code,categoryId).Description+ "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productByCategory.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Categoria;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productByCategory)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Categoria;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);



                    return DEAQDataAccessComponent.ProductsDALC.Instance.getByCategory(isbn,categoryId);




                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }
        public List<ProductDetailByEditionInfo> getProductBySeed(string isbn, int seedId, string code)
        {
            if (isbn == "" && seedId != 0)
                throw new ArgumentException("The isbn or seed does not exist");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                   List<ProductDetailByEditionInfo> productBySeed= DEAQDataAccessComponent.ProductsDALC.Instance.getBySeed(isbn, seedId);

                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "SeedId=" + seedId.ToString();
                    Track.JsonFormat = "{ \"Semilla\" : \"" + SeedsBLC.Instance.getById(isbn, seedId, code).SeedName + "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productBySeed.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Semilla;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productBySeed)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Semilla;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);






                    return productBySeed;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
                
        }

        public List<ProductDetailByEditionInfo> getProductByUse(string isbn, int useId, string code)
        {
            if (isbn == "" && useId != 0)
                throw new ArgumentException("The isbn or use does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductDetailByEditionInfo> usesList=
                     DEAQDataAccessComponent.ProductsDALC.Instance.getByUse(isbn, useId);

                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "AgrochemicalUseId=" +  useId.ToString();
                    Track.JsonFormat = "{ \"Uso Agroquimico\" : \"" + AgrochemicalUsesBLC.Instance.getById(isbn,useId,code).AgrochemicalUseName + "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (usesList.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Usos_Agroquimicos;

                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in usesList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Producto_Uso;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);




                    return usesList;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
                
        }

        public List<ProductDetailByEditionInfo> getProductByText(string isbn, string text, string code)
        {
            if (isbn == "")
                throw new ArgumentException("The isbn or code does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ProductDetailByEditionInfo> productList =
                     DEAQDataAccessComponent.ProductsDALC.Instance.getByText(isbn, text);

                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "Text=" + text;
                    Track.JsonFormat = "{ \"Texto\" : \"" + text.ToString() + "\" }";
                    Track.EntityIdSpecified = true;
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    if (productList.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_Texto;
                       
                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ProductDetailByEditionInfo product in productList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Productos;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "DivisionId=" + product.DivisionId.ToString() + ";CategoryId=" + product.CategoryId.ToString()
                                + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                            Children.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + product.CategoryName.ToString() +
                                                      "\",\"Producto\":\"" + product.ProductName.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);
                    

                    return productList;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
                
        }
        #endregion

        public void updateProduct(ProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ")";
            activityLog.FieldsAffected = "(LaboratoryId," + BEntity.LaboratoryId + ");"+"(Brand,"
                + BEntity.ProductName + ");(SanitaryRegistry, " + BEntity.SanitaryRegistry + ");(Description," + BEntity.Description + ");(Active," + BEntity.Active + ")";

           AgroDataAccessComponent.ProductsDALC.Instance.update(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
        #endregion

        #region General search
        public SearchResultInfo getResults(string code,string isbn,string searchText) {
            if (isbn == "" )
                throw new ArgumentException("The isbn does not exist");
            else {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    SearchResultInfo sResult = new SearchResultInfo();
                    //get the substances
                    List<AgroBusinessEntries.ActiveSubstanceInfo> substances=
                        DEAQBusinessLogicComponent.ActiveSubstancesBLC.Instance.getByText(isbn, searchText, code);
                    if (substances.Count > 0)
                        sResult.Substances = substances;

                    //get the products
                    List<ProductDetailByEditionInfo> products =
                        getProductByText(isbn, searchText, code);
                    if (products.Count > 0)
                        sResult.Products = products;
                    
                    //get the Uses

                    List<AgrochemicalUseDetailInfo> uses =
                        DEAQBusinessLogicComponent.AgrochemicalUsesBLC.Instance.getByText(isbn, searchText, code);
                    if (uses.Count > 0)
                        sResult.Uses = uses;

                    //get the crops
                    List<CropDetailInfo> crops =
                        DEAQBusinessLogicComponent.CropsBLC.Instance.getByText(isbn, searchText, code);
                    if (crops.Count > 0)
                        sResult.Crops = crops;

                    //get the crops
                    List<DivisionInfo> labs =
                        DEAQBusinessLogicComponent.DivisionsBLC.Instance.getByText(isbn, searchText, code);
                    if (labs.Count > 0)
                        sResult.Labs = labs;
                    

                    return sResult;
                }
                else
                {
                    throw new ArgumentException("El código no es válido o se encuentra inactivo.");
                }    
            }
        
        
        }
        public ProductsInfo getProductById(int productId)
        {
            return AgroDataAccessComponent.ProductsDALC.Instance.getOne(productId);
        }
        public List<ProductsToEditInfo> getEditableProductsByDivision(int divisionId, int countryId, int editionId, int bookId, string brand)
        {
            if (divisionId <= 0 || countryId <= 0 || editionId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");
            List<ProductsToEditInfo> products = AgroDataAccessComponent.ProductsDALC.Instance.getEditableProductsByDivision(divisionId, countryId, editionId, bookId, brand);
            
            return products;
        }
        public List<ProductsInfo> getProductsByDivision(int divisionId, int countryId, int bookId)
        {
            if (divisionId <= 0 || countryId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Book does not exist.");

            return AgroDataAccessComponent.ProductsDALC.Instance.getByDivision(divisionId, countryId, bookId);
        }

        public ProductDetailInfo getAllAttributesByProduct(string code, string isbn, int divisionId, int categoryId, int productId, int pharmaFormId, string resolutionKey, string applicationKey)
        {
            if (resolutionKey == null)
                throw new ArgumentException("The resolution does not exist.");

            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                ProductDetailInfo product = DEAQDataAccessComponent.ProductsDALC.Instance.getProductAttributes(isbn, divisionId, categoryId, productId, pharmaFormId);
                if (product!=null)
                {
                    AgroBusinessEntries.EditionsInfo beEdtion = DEAQDataAccessComponent.EditionsDALC.Instance.getOne(DEAQDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).EditionId);
                    AgroBusinessEntries.BooksInfo beBook = DEAQDataAccessComponent.BooksDALC.Instance.getOne(beEdtion.BookId);
                    product.Substances = ActiveSubstancesBLC.Instance.getByProduct(isbn, productId, pharmaFormId, divisionId, categoryId, code);
                    product.Attributes = DEAQDataAccessComponent.AttributesDALC.Instance.getCompleteAttributes(isbn, divisionId, categoryId, productId, pharmaFormId);
                    //foreach (PharmaSearchEngineBusinessEntries.AttributeDetailInfo html in product.Attributes)
                    //{
                    //    html.HTMLContent = html.HTMLContent.Replace(html.HTMLContent, PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                    //        DEAQDataAccessComponent.CountriesDALC.Instance.getOne((DEAQDataAccessComponent.EditionsDALC.Instance.getOne(DEAQDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).EditionId).CountryId)).CountryName + "/" +
                    //        beBook.ShortName + beEdtion.NumberEdition.ToString() + "/img/");
                    //}
                    //Add BaseUrl
                    product.BaseUrl = this.addBaseUrl(applicationKey, isbn, beBook, beEdtion, product.ProductShot, product.DivisionImage, resolutionKey);

                    if (string.IsNullOrEmpty(product.ProductShot))
                    {
                        if (string.IsNullOrEmpty(product.DivisionImage))
                            product.ProductShot = System.Configuration.ConfigurationManager.AppSettings["DefaultImage"];
                        else
                            product.ProductShot = product.DivisionImage;
                    }
                }
                string category = CatalogsBLC.Instance.getCategoryById(code,categoryId).Description;
                _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                Track.CodeString = code;
                Track.ISBN = isbn;
                Track.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                Track.SourceIdSpecified = true;
                Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                Track.SearchTypeIdSpecified = true;
                Track.SearchDate = null;
                Track.SearchParameters = "DivisionId=" + divisionId.ToString() + ";CategoryId=" + categoryId.ToString() + ";ProductId=" + product.ProductId.ToString() + ";PharmaFormId=" + product.PharmaFormId.ToString();
                Track.JsonFormat = "{ \"Laboratorio\" : \"" + product.DivisionName.ToString() + "\",\"Categoria\":\"" + category +
                            "\",\"Producto\":\"" + product.Brand.ToString() + "\",\"Forma Farmaceutica\":\"" + product.PharmaForm.ToString() + "\"}";
                Track.EntityIdSpecified = true;
                Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Contenido;
                Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                this.Tracking.addPLMTrackingParentActivity(Track);

                return product;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
            
        }
        #endregion



        #endregion

        private string addBaseUrl(string applicationKey, string isbn, AgroBusinessEntries.BooksInfo beBook, AgroBusinessEntries.EditionsInfo beEdtion,
           string productShot, string divisionImage, string resolutionKey)
        {
            string baseUrl="";

            if (beEdtion.ParentId == null)
            {
                baseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                    DEAQDataAccessComponent.CountriesDALC.Instance.getOne((DEAQDataAccessComponent.EditionsDALC.Instance.getOne(DEAQDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).EditionId).CountryId)).CountryName +
                    "/" + beBook.ShortName + beEdtion.NumberEdition.ToString() + "/";
            }
            else
            {
                AgroBusinessEntries.EditionsInfo beParentEdition = DEAQDataAccessComponent.EditionsDALC.Instance.getOne((int)beEdtion.ParentId);

                if (beParentEdition.ParentId != null)
                {
                    do
                    {
                        beParentEdition = DEAQDataAccessComponent.EditionsDALC.Instance.getOne((int)beParentEdition.ParentId);
                    }
                    while (beParentEdition.ParentId != null);
                }

                AgroBusinessEntries.BooksInfo beParentBook = DEAQDataAccessComponent.BooksDALC.Instance.getOne(beParentEdition.BookId);

                baseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                    DEAQDataAccessComponent.CountriesDALC.Instance.getOne((DEAQDataAccessComponent.EditionsDALC.Instance.getOne(DEAQDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).EditionId).CountryId)).CountryName +
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
                if (resolutionKey == "")
                    baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["DefaultResolutionUrl"];
                else
                    baseUrl = baseUrl + PLMClientsBusinessLogicComponent.ResolutionScreensBLC.Instance.getByResolutionKey(resolutionKey).BaseUrl + "/";
            }

            //if (resolutionKey == "")
            //    baseUrl = baseUrl + System.Configuration.ConfigurationManager.AppSettings["DefaultResolutionUrl"];
            //else
            //    baseUrl = baseUrl + PLMClientsBusinessLogicComponent.ResolutionScreensBLC.Instance.getByResolutionKey(resolutionKey).BaseUrl + "/";

            return baseUrl;
        }



        private _PLMTracking.PLMLogs Tracking = new _PLMTracking.PLMLogs();
        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();
        public static readonly ProductsBLC Instance = new ProductsBLC();

    }
}
