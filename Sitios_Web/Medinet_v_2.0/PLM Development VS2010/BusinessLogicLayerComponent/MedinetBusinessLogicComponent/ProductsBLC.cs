using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductsBLC 
    {

        #region Contructors

        private ProductsBLC() { }
        
        #endregion

        #region Public Methods

        public List<ProductsToEditInfo> getProductsByFilter(int laboratoryId, string filter, int countryId, int editionId, int bookId)
        {
            return MedinetDataAccessComponent.ProductsDALC.Instance.getEditableProducts(laboratoryId, filter, countryId, editionId, bookId);
        }

        public List<ProductsToEditInfo> getEditableProductsByDivision(int divisionId, int countryId, int editionId, int bookId, string brand)
        {
            if(divisionId <= 0 || countryId <= 0 || editionId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");
            List<ProductsToEditInfo> products=MedinetDataAccessComponent.ProductsDALC.Instance.getEditableProductsByDivision(divisionId, countryId, editionId, bookId, brand);
            foreach (ProductsToEditInfo item in products)
            {
                List<CountryCodeInfo> countries = MedinetDataAccessComponent.CountriesDALC.Instance.getCountryCodesByProduct(Convert.ToInt32(item.ProductId), Convert.ToInt32(item.PharmaFormId), countryId);
                string countriesCodes = "";
                foreach (CountryCodeInfo coun in countries)
                {
                    if (countriesCodes.Length>0)
                    {   countriesCodes=countriesCodes+","+coun.CountryCodeId;}
                    else
                    {   countriesCodes = coun.CountryCodeId.ToString();}
                }
                item.Countries = countriesCodes;
            }
            return products;
        }

        public List<ProductsToEditInfo> getParticipantProductsByDivision(int divisionId, int countryId, int editionId, int bookId)
        {
            if (divisionId <= 0 || countryId <= 0 || editionId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getParticipantProductsByDivision(divisionId, countryId, editionId, bookId);
        }

        public List<ProductsToEditInfo> getProductsByCountryAndDivision(int divisionId, int countryId)
        {
            if (divisionId <= 0 || countryId <= 0 )
                throw new ArgumentException("The Division or Country does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getProductsByCountryAndDivision(divisionId, countryId);
        }
        public List<ProductsToEditInfo> getParticipantProductImages(int divisionId, int countryId,int bookId,int editionId)
        {
            if (divisionId <= 0 || countryId <= 0 )
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getParticipantProductImages(divisionId, countryId,bookId,editionId);
        }
        public List<ProductsToEditInfo> getParticipantProductsByDivisionSymptoms(int divisionId, int countryId, int editionId, int bookId)
        {
            if (divisionId <= 0 || countryId <= 0 || editionId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getParticipantProductsByDivisionSymptoms(divisionId, countryId, editionId, bookId);
        }

        public List<ProductsToEditInfo> getParticipantProductsByEdition(int countryId, int editionId, int bookId, string brand)
        {
            if (countryId <= 0 || editionId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Edition or Book does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getParticipantProductsByEdition(countryId, editionId, bookId, brand);
        }

        public ProductsInfo getProductById(int productId)
        {
            return MedinetDataAccessComponent.ProductsDALC.Instance.getOne(productId);
        }

        public int addProduct(ProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            
            BEntity.ProductId = MedinetDataAccessComponent.ProductsDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ")";
            activityLog.FieldsAffected = "(LaboratoryId," + BEntity.LaboratoryId + ");(AlphabetId," + BEntity.AlphabetId + ");(Brand,"
                + BEntity.Brand + ");(SanitaryRegistry, " + BEntity.SanitaryRegistry + ");(Description," + BEntity.Description + ");(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            return BEntity.ProductId;
        }

        public void removeProduct(int productId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";

            MedinetDataAccessComponent.ProductsDALC.Instance.delete(productId);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void updateProduct(ProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ")";
            activityLog.FieldsAffected = "(LaboratoryId," + BEntity.LaboratoryId + ");(AlphabetId," + BEntity.AlphabetId + ");(Brand,"
                + BEntity.Brand + ");(SanitaryRegistry, " + BEntity.SanitaryRegistry + ");(Description," + BEntity.Description + ");(Active," + BEntity.Active + ")";

            MedinetDataAccessComponent.ProductsDALC.Instance.update(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<ProductsInfo> getProductsByDivision(int divisionId, int countryId, int bookId)
        {
            if (divisionId <= 0 || countryId <= 0 || bookId <= 0)
                throw new ArgumentException("The Division or Country or Book does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getByDivision(divisionId, countryId, bookId);
        }

        public List<DivisionProductsToEditInfo> getEditableDivisionProducts(int divisionId, int countryId, int? divisionSearch, string brand)
        {
            if (divisionId <= 0 || countryId <= 0)
                throw new ArgumentException("The Division or Country does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getEditableDivisionProducts(divisionId, countryId, divisionSearch, brand);
        }

        #endregion

        public static readonly ProductsBLC Instance = new ProductsBLC();

    }
}
