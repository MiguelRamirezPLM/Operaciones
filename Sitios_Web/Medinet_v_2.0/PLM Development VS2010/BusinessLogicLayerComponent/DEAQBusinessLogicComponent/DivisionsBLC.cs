using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using PLMClientsBusinessLogicComponent;
using PLMClientsBusinessEntities;
using AgroBusinessEntries;
namespace DEAQBusinessLogicComponent
{
    public class DivisionsBLC
    {
        #region Constructor

        private DivisionsBLC() { }

        #endregion

        #region Methods
        #region ROC Methods
        //Retrieves All Division By DivisionId
        public DivisionByCountryInfo rocGetDivision(string code, int countryId, int divisionId)
        {

            if (countryId <= 0 || divisionId <= 0)
                throw new ArgumentException("The country or division does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DivisionByCountryInfo div = DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivision(countryId, divisionId);

                    if (div != null)
                    {
                        if (!string.IsNullOrEmpty(div.Image))
                        {

                           
                           DEAQBusinessEntries.CountryInfo countinf = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryById(countryId);

                           PLMClientsBusinessEntities.BookInfo bookInfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                           div.Image = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countinf.CountryName + "/" + bookInfo.ShortName + "/Logos/" + div.Image;
                        }
                        
                            
                    } 
                     return div;
                    
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
      
        }

        //Retrieves All Division By LaboratoryId
        public DivisionByCountryInfo rocGetDivisionByLaboratoryId(string code, int laboratoryId)
        {
            if (laboratoryId <= 0)
                throw new ArgumentException("The laboratory does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DivisionByCountryInfo div = DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionByLaboratoryId(laboratoryId);

                    if (div != null)
                    {
                        if (!string.IsNullOrEmpty(div.Image))
                        {

                            DEAQBusinessEntries.CountryInfo countinf = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryById((int)div.CountryId);

                            PLMClientsBusinessEntities.BookInfo bookinfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                            div.Image = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countinf.CountryName + "/" + bookinfo.ShortName +  "/Logos/" + div.Image;

                        }
                             
                    }

                    return div;
                }
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves ALL Category by Divisions
        public CategoryInfo rocGetDivisionCategories(string code, int divisionId, int editionId)
        {
            if (divisionId <= 0 || editionId <= 0)
                throw new ArgumentException("The division or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionCategories(divisionId, editionId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves All Division By EditionId
        public List<DivisionByEditionInfo> rocGetDivisions(string code, int numberByPage, int page, int editionId)
        {
            if (numberByPage < 0 || page < 0 ||  editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisions(numberByPage, page, editionId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

         //Retrieves All Division By Fulltext
        public List<DivisionByEditionInfo> rocGetDivisionsByFullText(string code, int numberByPage, int page, int editionId, string text)
        {
            if (numberByPage < 0 || page < 0 || editionId <= 0)
                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByFullText(numberByPage, page, editionId, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        
        }

         //Retrieves All Division By ParentId
        public List<DivisionByCountryInfo> rocGetDivisionsByLaboratoryIdAndParentId(string code, int laboratoryId, int parentId)
        {

            if (laboratoryId <= 0 || parentId <= 0)
                throw new ArgumentException("The laboratory or parent does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                  List<DivisionByCountryInfo> div = DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByLaboratoryIdAndParentId(laboratoryId, parentId);
                   
                    if (div != null)
                    {
                        foreach (DivisionByCountryInfo divisions in div)
                        {
                            if (!string.IsNullOrEmpty(divisions.Image))
                            {
                                
                                 DEAQBusinessEntries.CountryInfo countinf = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryById((int)divisions.CountryId);

                                 PLMClientsBusinessEntities.BookInfo bookinfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                                divisions.Image = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countinf.CountryName  + "/" + bookinfo.ShortName + "/Logos/" + divisions.Image;
                            }
                            
                        }
                        
                    }
                    return div;
                }

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves All Division By Letter
        public List<DivisionByEditionInfo> rocGetDivisionsByLetter(string code, int numberByPage, int page, int editionId, string letter)
        {
            if (numberByPage < 0 || page < 0 || editionId <= 0)

                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByLetter(numberByPage, page, editionId, letter);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves Information From Division By ParentId
        public List<DivisionByCountryInfo> rocGetDivisionsByParentId(string code, int parentId)
        {
            if (parentId <= 0)
                throw new ArgumentException("The parent does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DivisionByCountryInfo> div = DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByParentId(parentId);

                    if (div != null)
                    {
                        foreach (DivisionByCountryInfo divisions in div)
                        {
                            if (!string.IsNullOrEmpty(divisions.Image))
                            {
                              DEAQBusinessEntries.CountryInfo countinf = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryById((int)divisions.CountryId);

                              PLMClientsBusinessEntities.BookInfo bookinfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                                divisions.Image = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countinf.CountryName +  "/" + bookinfo.ShortName + "/Logos" + divisions.Image;
                            }
                        }

                    }
                    return div;
                }    

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

         //Retrieves ALL Division By Text
        public List<DivisionByEditionInfo> rocGetDivisionsByText(string code, int numberByPage, int page, int editionId, string text)
        {

            if (numberByPage < 0 || page < 0 || editionId <= 0)

                throw new ArgumentException("The page or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByText(numberByPage, page, editionId, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }



        #endregion
        
        #region PLM Methods
        public List<DivisionInfo> getAll(string isbn, string code)
        {
            if (isbn == "")
                throw new ArgumentException("The isbn does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return DEAQDataAccessComponent.DivisionsDALC.Instance.getAll(isbn);
                else
                    throw new ApplicationException("The code is not valid or is inactive.");
               
            }
        }

        public DivisionInfo getById(string isbn, int divisionId, string code)
        {
            if (isbn == "" || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return DEAQDataAccessComponent.DivisionsDALC.Instance.getById(isbn,divisionId);
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }

        public List<DivisionInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0 || pharmaFormId == 0 || categoryId == 0 || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId or categoryId  or pharmaFormId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return DEAQDataAccessComponent.DivisionsDALC.Instance.getByProduct(isbn,productId,pharmaFormId,divisionId,categoryId);
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }

        public List<DivisionInfo> getByText(string isbn, string text, string code)
        {
            if (isbn == "" || text == "")
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
            List<DivisionInfo> labsList = DEAQDataAccessComponent.DivisionsDALC.Instance.getByText(isbn, text);
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
            if (labsList.Count > 0)
            {
                Track.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_Texto;

                _PLMTracking.TrackingListInfo Children;
                List<_PLMTracking.TrackingListInfo> list = new List<_PLMTracking.TrackingListInfo>();
                foreach (DivisionInfo lab in labsList)
                {
                    Children = new _PLMTracking.TrackingListInfo();
                    Children.CodeString = code;
                    Children.ISBN = isbn;
                    Children.SourceId = Convert.ToByte(PharmaSearchEngineBusinessLogicComponent.Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                    Children.SourceIdSpecified = true;
                    Children.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    Children.SearchTypeIdSpecified = true;
                    Children.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Laboratorios;
                    Children.EntityIdSpecified = true;
                    Children.SearchDate = null;
                    Children.SearchParameters = "DivisionId=" + lab.DivisionId.ToString();
                    Children.JsonFormat = "{ \"DivisionName\" : \"" + lab.DivisionName.ToString() + "\" }";
                    list.Add(Children);

                }
                Track.ChildrenTrackingInfo = list.ToArray();

            }
            else
            {
                Track.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
            }
            this.Tracking.addPLMTrackingParentActivity(Track);


            return labsList;
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<DivisionInfo> getByCategory(string isbn, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0)
                throw new ArgumentException("The isbn or CategoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return DEAQDataAccessComponent.DivisionsDALC.Instance.getByCategory(isbn,categoryId);
                else
                    throw new ApplicationException("The code is not valid or is inactive.");

            }
        }
        #endregion

        #endregion
        private _PLMTracking.PLMLogs Tracking = new _PLMTracking.PLMLogs();
        public static readonly DivisionsBLC Instance = new DivisionsBLC();
    }
}
