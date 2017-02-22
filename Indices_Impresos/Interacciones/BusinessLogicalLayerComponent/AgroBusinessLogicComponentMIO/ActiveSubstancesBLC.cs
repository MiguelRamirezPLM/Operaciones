using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSearchEngineBusinessLogicComponent;
using AgroBusinessEntries;
namespace AgroBusinessLogicComponent
{
    public class ActiveSubstancesBLC
    {

        #region Constructor

        private ActiveSubstancesBLC() { }

        #endregion

        #region Public Methods
        #region ROC Methods
        //Retrieves information about ActiveSubstances By ActiveSubstanceId
        public DEAQBusinessEntries.ActiveSubstanceInfo rocGetActiveSubstance(string code, int activeSubstanceId)
        {
            if (activeSubstanceId < 1)
            {
                throw new ArgumentException("The Active Substance does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstance(activeSubstanceId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all ActiveSubstances By Edition and product
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByProductInfo> rocGetActiveSubstancesByProduct(string code, int editionId, int productId)
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
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByProduct(editionId, productId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all ActiveSubstances By Edition
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstances(string code, int numberByPage, int page, int editionId)
        {
            if (numberByPage < 0 || page < 0 || editionId < 1)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstances(numberByPage, page, editionId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }
        
        //Retrieves all ActiveSubstances By Edition and fulltext
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByFullText(string code, int numberByPage, int page, int editionId, string fullText)
        {
            if (numberByPage < 0 || page < 0 || editionId < 1)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByFullText(numberByPage, page, editionId, fullText);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all ActiveSubstances By Edition and letter
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByLetter(string code, int numberByPage, int page, int editionId, string letter)
        {
            if (numberByPage < 0 || page < 0 || editionId < 1)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByLetter(numberByPage, page, editionId, letter);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all ActiveSubstances By Edition and text
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByText(string code, int numberByPage, int page, int editionId, string text)
        {
            if (numberByPage < 0 || page < 0 || editionId < 1)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByText(numberByPage, page, editionId, text);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        #endregion

        #region PLM Methods
        public void addActiveSubstancesForProduct(int productId, int agrochemicalUseId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.ActiveSubstancesDALC.Instance.insertForProduct(productId, agrochemicalUseId);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
            activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

            // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }
        public void deleteActiveSubstancesForProduct(int productId, int agrochemicalUseId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.ActiveSubstancesDALC.Instance.deleteForProduct(productId, agrochemicalUseId);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
            activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

            // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }

        public List<AgroBusinessEntries.ActiveSubstanceInfo> getActiveSubstancesNotAssociated(int editionId, int divisionId, int bookId, int productId, int pharmaformId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The Edition or Country does not exist.");

            return AgroDataAccessComponent.ActiveSubstancesDALC.Instance.getActiveSubstancesNotAssociated(editionId, divisionId, bookId, productId, pharmaformId);
        }

        public List<AgroBusinessEntries.ActiveSubstanceInfo> getActiveSubstancesForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The Edition or division does not exist.");

            return AgroDataAccessComponent.ActiveSubstancesDALC.Instance.getActiveSubstancesForProduct(editionId, divisionId, bookId, productId, pharmaFormId);
        }
        public List<ActiveSubstanceInfo> getAll(string isbn, string code)
        {
            if (isbn == "")
            {
                throw new ArgumentException("The isbn or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.getAll(isbn);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public ActiveSubstanceInfo getById(string isbn, int activeSubstanceId, string code)
        {
            if (isbn == "" || activeSubstanceId == 0)
            {
                throw new ArgumentException("The isbn or activeSubstanceId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.getById(isbn,activeSubstanceId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<ActiveSubstanceInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0 || pharmaFormId == 0 || categoryId == 0 || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId or categoryId  or pharmaFormId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ActiveSubstanceInfo> substanceList =
                     DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.getByProduct(isbn, productId, pharmaFormId, divisionId, categoryId);
                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified = true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "ProductId=" + productId.ToString();
                    Track.JsonFormat = "{ \"Producto\" : \"" + ProductsBLC.Instance.getProductsById(productId, code).ProductName.ToString() + "\" }";
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    Track.EntityIdSpecified = true;
                    if (substanceList.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancia_Producto;
                       
                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ActiveSubstanceInfo subst in substanceList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancia_Producto;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "ActiveSubstanceId=" + subst.ActiveSubstanceId.ToString();
                            Children.JsonFormat = "{ \"Sustancia\" : \"" + subst.ActiveSubstanceName.ToString() + "\" }";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();
                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);

                    return substanceList;
                     }
                    else
                    {
                        throw new ApplicationException("The code is not valid or is inactive.");
                    }
                }
          
        }

        public List<ActiveSubstanceInfo> getByText(string isbn, string text, string code)
        {
            if (isbn == "" || text == "")
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<ActiveSubstanceInfo> substanceList = DEAQDataAccessComponent.ActiveSubstancesDALC.Instance.getByText(isbn, text);
                   
                    _PLMTracking.TrackingParentInfo Track = new _PLMTracking.TrackingParentInfo();
                    Track.CodeString = code;
                    Track.ISBN = isbn;
                    Track.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Track.SourceIdSpecified=true;
                    Track.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    Track.SearchTypeIdSpecified = true;
                    Track.SearchDate = null;
                    Track.SearchParameters = "Text=" + text;
                    Track.JsonFormat = "{ \"Texto\" : \"" + text.ToString() + "\" }";
                    Track.ChildrenTrackingInfo = new List<_PLMTracking.TrackingListInfo>().ToArray();
                    Track.EntityIdSpecified = true;
                    if (substanceList.Count>0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_Texto;
                        
                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (ActiveSubstanceInfo subst in substanceList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sustancias;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "ActiveSubstanceId=" + subst.ActiveSubstanceId.ToString();
                            Children.JsonFormat = "{ \"Sustancia\" : \"" + subst.ActiveSubstanceName.ToString() + "\" }";
                            list.Add(Children);
                            
                        }
                        Track.ChildrenTrackingInfo = list.ToArray();
                        
                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                     
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);
                    
                   

                    return substanceList;
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }
        #endregion
        #endregion

        private _PLMTracking.PLMLogs Tracking = new _PLMTracking.PLMLogs();
        
        //private PLMTracking.PLMLogsClient _PlmTracking = new PLMTracking.PLMLogsClient();
        public static readonly ActiveSubstancesBLC Instance = new ActiveSubstancesBLC();

    }
}
