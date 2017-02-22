using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using AgroBusinessEntries;
namespace DEAQBusinessLogicComponent
{
    public class CropsBLC
    {
        #region Constructor

        private CropsBLC() { }

        #endregion

        #region Methods
        #region ROC Methods
        //Retrieves All Information From Crops
        public CropInfo rocGetCrop(string code, int cropId)
        {

            if (cropId <= 0)
                throw new ArgumentException("The crop  does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCrop(cropId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
      
        }

        //Retrieves Information From Crops By ProuctId
        public List<CropInfo> rocGetCropsByProductId(string code, int productId, int editionId)
        {
            if (productId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The crop  does not exist");
            }

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCropsByProductId(productId, editionId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }


        //Retrives Information From Crops By Edition
        public List<CropByEditionInfo> rocGetCrops(string code, int numberByPage, int page, int editionId)
        {

            if (numberByPage < 0 || page < 0 || editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCrops(numberByPage, page, editionId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
      
        }

         //Retrives Information From Crops By Fulltext
        public List<CropByEditionInfo> rocGetCropsByFullText(string code, int numberByPage, int page, int editionId, string text)
        {

            if (numberByPage < 0 || page < 0 || editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCropsByFullText(numberByPage, page, editionId, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

         //Retrieves Information From Crops By Letter
        public List<CropByEditionInfo> rocGetCropsByLetter(string code, int numberByPage, int page, int editionId, string letter)
        {

            if (numberByPage < 0 || page < 0 || editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCropsByLetter(numberByPage, page, editionId, letter);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves Information From Crops By Text
        public List<CropByEditionInfo> rocGetCropBtText(string code, int numberByPage, int page, int editionId, string text)
        {

            if (numberByPage < 0 || page < 0 || editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.CropsDALC.Instace.rocGetCropBtText(numberByPage, page, editionId, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }
#endregion
        #region PLM Methods
        public List<CropDetailInfo> getAll(string isbn, string code)
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
                    return DEAQDataAccessComponent.CropsDALC.Instace.getAll(isbn);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public CropDetailInfo getById(string isbn, int cropId, string code)
        {
            if (isbn == "" || cropId == 0)
            {
                throw new ArgumentException("The isbn or cropId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.CropsDALC.Instace.getById(isbn, cropId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<CropDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0 || pharmaFormId == 0 || categoryId == 0 || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId or categoryId  or pharmaFormId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.CropsDALC.Instace.getByProduct(isbn, productId, pharmaFormId, divisionId, categoryId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<CropDetailInfo> getByText(string isbn, string text, string code)
        {
            if (isbn == "" || text == "")
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<CropDetailInfo> cropList=
                     DEAQDataAccessComponent.CropsDALC.Instace.getByText(isbn, text);

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
                    if (cropList.Count > 0)
                    {
                        Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_Texto;
                        
                        _PLMTracking.TrackingListInfo Children;
                        List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                        foreach (CropDetailInfo crop in cropList)
                        {
                            Children = new _PLMTracking.TrackingListInfo();
                            Children.CodeString = code;
                            Children.ISBN = isbn;
                            Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            Children.SourceIdSpecified = true;
                            Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            Children.SearchTypeIdSpecified = true;
                            Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Cultivos;
                            Children.EntityIdSpecified = true;
                            Children.SearchDate = null;
                            Children.SearchParameters = "CropId=" + crop.CropId.ToString();
                            Children.JsonFormat = "{ \"Cultivo\" : \"" + crop.CropName.ToString() + "\" }";
                            list.Add(Children);

                        }
                        Track.ChildrenTrackingInfo = list.ToArray();

                    }
                    else
                    {
                        Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }
                    this.Tracking.addPLMTrackingParentActivity(Track);


                    return cropList;
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
        public static readonly CropsBLC Instance = new CropsBLC();
    }
}
