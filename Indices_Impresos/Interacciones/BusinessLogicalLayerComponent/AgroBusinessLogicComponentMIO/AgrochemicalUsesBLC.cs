using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using AgroBusinessEntries;
namespace AgroBusinessLogicComponent
{
    public class AgrochemicalUsesBLC
    {

        #region Constructor

        private AgrochemicalUsesBLC() { }

        #endregion

        #region Public Methods
        #region ROC MEthods
        //Retrieves information about AgrochemicalUse by UseId
        public DEAQBusinessEntries.AgrochemicalUseInfo rocGetUse(string code, int agrochemicalUseId)
        {
            if (agrochemicalUseId < 1)
            {
                throw new ArgumentException("The Agrochemical Use does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUse(agrochemicalUseId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all AgrochemicalUses by Edition and ParentId
        public List<DEAQBusinessEntries.AgrochemicalUseInfo> rocGetUseByParent(string code, int editionId, int parentId)
        {
            if (editionId < 1 || parentId < 1)
            {
                throw new ArgumentException("The Edition or Parent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUseByParent(editionId, parentId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all AgrochemicalUses by Edition
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUses(string code, int editionId, int numberByPage, int page)
        {
            if (editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUses(editionId, numberByPage, page);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all AgrochemicalUses by Edition and FullText
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByFullText(string code, int editionId, int numberByPage, int page, string fullText)
        {
            if (editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUsesByFullText(editionId, numberByPage, page, fullText);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all AgrochemicalUses by Edition and letter
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByLetter(string code, int editionId, int numberByPage, int page, string letter)
        {
            if (editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUsesByLetter(editionId, numberByPage, page, letter);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        //Retrieves all AgrochemicalUses by Edition and text
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByText(string code, int editionId, int numberByPage, int page, string text)
        {
            if (editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.rocGetUsesByText(editionId, numberByPage, page, text);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        #endregion
        #region PLM Methods

        public void addAgrochemicalUsesForProduct(int productId, int agrochemicalUseId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.AgrochemicalUsesDALC.Instance.insertForProduct(productId, agrochemicalUseId);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
            activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

            // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }
        public void deleteAgrochemicalUsesForProduct(int productId, int agrochemicalUseId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.AgrochemicalUsesDALC.Instance.deleteForProduct(productId, agrochemicalUseId);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
            activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

            // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }
        public List<AgrochemicalUseDetailInfo> getAll(string isbn, string code)
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
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.getAll(isbn);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public AgrochemicalUseDetailInfo getById(string isbn, int useId, string code)
        {
            if (isbn == "" || useId == 0)
            {
                throw new ArgumentException("The isbn or useId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.getById(isbn, useId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }
        public List<AgroBusinessEntries.AgrochemicalUseInfo> getAgrochemicalUsesNotAssociated(int editionId, int divisionId, int bookId,int productId, int pharmaformId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The Edition or Country does not exist.");

            return AgroDataAccessComponent.AgrochemicalUsesDALC.Instance.getAgrochemicalUsesNotAssociated(editionId, divisionId, bookId, productId, pharmaformId);
        }

        public List<AgroBusinessEntries.AgrochemicalUseInfo> getAgrochemicalUsesForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The Edition or division does not exist.");

            return AgroDataAccessComponent.AgrochemicalUsesDALC.Instance.getAgrochemicalUsesForProduct(editionId, divisionId, bookId, productId, pharmaFormId);
        }

        public List<AgrochemicalUseDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0 || pharmaFormId == 0 || categoryId == 0 || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId or categoryId  or pharmaFormId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.getByProduct(isbn, productId, pharmaFormId, divisionId, categoryId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<AgrochemicalUseDetailInfo> getByText(string isbn, string text, string code)
        {
            if (isbn == "" || text == "")
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<AgrochemicalUseDetailInfo> usesList=
                     DEAQDataAccessComponent.AgrochemicalUsesDALC.Instance.getByText(isbn, text);
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
                    if (usesList.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_Texto;
                       
                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (AgrochemicalUseDetailInfo use in usesList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Usos_Agroquimicos;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "AgrochemicalUseId=" + use.AgrochemicalUseId.ToString();
                            Children.JsonFormat = "{ \"Uso Agroquimico\" : \"" + use.AgrochemicalUseName.ToString() + "\" }";
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
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }
        #endregion

        #endregion


        private _PLMTracking.PLMLogs Tracking = new _PLMTracking.PLMLogs();
        public static readonly AgrochemicalUsesBLC Instance = new AgrochemicalUsesBLC();
    }
}
