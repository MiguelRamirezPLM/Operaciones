using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;
using System.Linq;

namespace MedinetBusinessLogicComponent
{
    public sealed class CatalogsBLC
    {

        #region Constructors

        private CatalogsBLC() { }

        #endregion

        #region Public Methods

        #region Countries

        public List<CountriesInfo> getCountries()
        {
            return MedinetDataAccessComponent.CountriesDALC.Instance.getAll();
        }

        public CountriesInfo getCountry(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");

            return MedinetDataAccessComponent.CountriesDALC.Instance.getOne(countryId);
        }

        public List<CountriesInfo> getCountries(string countries)
        {
            return MedinetDataAccessComponent.CountriesDALC.Instance.getCountries(countries);
        }

        public List<CountryCodeInfo> getOtherCountries(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");
                
            return MedinetDataAccessComponent.CountriesDALC.Instance.getSpecialCountries(countryId);
        }

        public List<CountryCodeInfo> getCountryCodes(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");

            return MedinetDataAccessComponent.CountriesDALC.Instance.getCountryCodes(countryId);
        }

        #endregion

        public BooksInfo getBook(int bookId)
        {
            return MedinetDataAccessComponent.BooksDALC.Instance.getOne(bookId);
        }

        public List<BooksInfo> getBooksByCountry(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");

            return MedinetDataAccessComponent.BooksDALC.Instance.getAllByCountry(countryId);  
        }

        public List<EditionsInfo> getEditionsByBook(int bookId, int countryId)
        {
            if (bookId <= 0 || countryId <= 0)
                throw new ArgumentException("The book or country does not exist.");
            
            return MedinetDataAccessComponent.EditionsDALC.Instance.getAllByBook(bookId, countryId);
        }
        public List<BooksInfo> getEncyclopediaBooksByCountry(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");

            return MedinetDataAccessComponent.BooksDALC.Instance.getEncyclopediaBooksByCountry(countryId);
        }

        public List<EditionsInfo> getEncyclopediaEditionsByBook(int bookId, int countryId)
        {
            if (bookId <= 0 || countryId <= 0)
                throw new ArgumentException("The book or country does not exist.");

            return MedinetDataAccessComponent.EditionsDALC.Instance.getEncyclopediaEditionsByBook(bookId, countryId);
        }


        public EditionsInfo getEdition(int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist.");

            return MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId);
        }

        public EditionsInfo getEditionByISBN(string isbn)
        {
            return MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn);
        }

        public List<LaboratoriesInfo> getLaboratories()
        {
            return MedinetDataAccessComponent.LaboratoriesDALC.Instance.getAll();
        }

        public List<DivisionsInfo> getDivisionByCountryLab(int countryId, int laboratoryId)
        {
            if (countryId <= 0 || laboratoryId <= 0)
                throw new ArgumentException("The country or laboratory does not exist.");

            List<DivisionsInfo> collection = MedinetDataAccessComponent.DivisionsDALC.Instance.getAllByCountryLab(countryId, laboratoryId);
            
            DivisionsInfo record = new DivisionsInfo();

            record.DivisionId = -1;
            record.ShortName = "SELECCIONAR";

            collection.Add(record);

            return collection;
        }

        public List<CategoriesInfo> getCategoriesByDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The laboratory does not exist.");

            List<CategoriesInfo> collection = MedinetDataAccessComponent.CategoriesDALC.Instance.getAll(divisionId);

            CategoriesInfo record = new CategoriesInfo();

            record.CategoryId = -1;
            record.Description = "SELECCIONAR";

            collection.Add(record);

            return collection;
        }

        public CategoriesInfo getCategory(int categoryId)
        {
            return MedinetDataAccessComponent.CategoriesDALC.Instance.getOne(categoryId);
        }

        public List<AlphabetInfo> getAlphabet()
        {
            return MedinetDataAccessComponent.AlphabetDALC.Instance.getAll();
        }

        public AlphabetInfo getLetter(char letter)
        {
            return MedinetDataAccessComponent.AlphabetDALC.Instance.getOne(letter);
        }

        public List<UnitInfo> getUnits()
        {
            return MedinetDataAccessComponent.UnitsDALC.Instance.getAll();
        }

        public List<AssignedProduct> getNoAssigned(int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist.");

            return MedinetDataAccessComponent.ProductsDALC.Instance.getNoAssignedProducts(editionId,(byte)CatalogsBLC.TypeInEdition.Participante); 
        }

        public List<DivisionsInfo> getDivisionsByCountry(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The country does not exist.");

            return MedinetDataAccessComponent.DivisionsDALC.Instance.getAllByCountry(countryId);
        }

        public DivisionsInfo getDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");

            return MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId);
        }

        #region Therapeutics

        public List<TherapeuticInfo> getTherapeutics(int? parentId)
        {
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAll(parentId);
        }

        public List<TherapeuticInfo> getTherapeuticsByName(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAllByName(description);
        }

        public TherapeuticInfo getTherapeutic(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(description);
        }

        public List<TherapeuticInfo> getTherapeuticsByNameSp(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAllByNameSp(description);
        }

        public TherapeuticInfo getTherapeuticSp(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOneSp(description);
        }

        public TherapeuticInfo getTherapeuticById(int therapeuticId)
        {
            if (therapeuticId <= 0)
                throw new ArgumentException("The therapeutic does not exist.");

            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOne(therapeuticId);
        }

        public List<TherapeuticInfo> getTherapeuticsByProduct(int productId, int pharmaFormId)
        {
            if (productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The product does not exist.");
            
            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getAllByProduct(productId, pharmaFormId);
        }

        public TherapeuticInfo getTheraByProduct(int productId, int pharmaFormId)
        {
            if (productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.TherapeuticsDALC.Instance.getOneByProduct(productId, pharmaFormId);
        }
        
        public TherapeuticDescriptionInfo getDescription(int therapeuticId)
        {
            if (therapeuticId <= 0)
                throw new ArgumentException("The therapeutic does not exist.");

            return MedinetDataAccessComponent.TherapeuticDescriptionsDALC.Instance.getOne(therapeuticId);
        }

        public bool checkTherapeuticGroup(int theraGroupId, int editionId, int productId, int pharmaFormId)
        {
            if (theraGroupId <= 0 || editionId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The therapeutic Group, edition, pharmaceutical form or product does not exist.");

            return MedinetDataAccessComponent.TherapeuticGroupProductsDALC.Instance.checkGroup(theraGroupId, editionId, pharmaFormId, productId);
        }

        #endregion



                
        #region Active Substances

        public ActiveSubstanceInfo getSubstanceByName(string description)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOneByName(description);
        }

        public List<ActiveSubstanceInfo> getAllSubstancesByFilter(string description)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByFilter(description);
        }

        public List<ActiveSubstanceInfo> getSubstancesByProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByProduct(productId);
        }
         
        public List<ActiveSubstanceInfo> getSubstancesWithoutProduct(int productId, string substance)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllWithoutProduct(productId, substance);
        }
        public List<ActiveSubstanceInfo> getSubstancesWithoutProductInteractions(int productId, string substance,int categoryId,int pharmaFormId,int divisionId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllWithoutProductInteractions(productId, substance,pharmaFormId,categoryId,divisionId);
        }

        public List<PharmacologicalGroupsInfo> getPharmaGroupWithoutProductInteractions(int productId, string pharmaGroup, int categoryId, int pharmaFormId, int divisionId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.PharmacologicalGroupDALC.Instance.getAllGroupswithOutProductInteractions(categoryId,pharmaFormId,productId,divisionId,pharmaGroup);
        }

        public List<OtherElementsInfo> getOtherElementsWithoutProductInteractions(int productId, string ElementName, int categoryId, int pharmaFormId, int divisionId)
        {
            if (productId <= 0) 
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.OthersElementsDALC.Instance.getAllOthersElementswithOutProductInteractions(categoryId, pharmaFormId, productId, divisionId, ElementName);
        }
        #endregion

        #region Indications

        public List<IndicationInfo> getAllIndicationsByFilter(string description)
        {
            return MedinetDataAccessComponent.IndicationsDALC.Instance.getAllByFilter(description);
        }

        public IndicationInfo getIndicationByName(string description)
        {
            return MedinetDataAccessComponent.IndicationsDALC.Instance.getOneByName(description);
        }

        public List<IndicationInfo> getIndicationsByProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.IndicationsDALC.Instance.getAllByProduct(productId);
        }

        public List<IndicationInfo> getIndicationsByParent(int parentId)
        {
            return MedinetDataAccessComponent.IndicationsDALC.Instance.getAllByParent(parentId);
        }

        public List<IndicationInfo> getIndicationsWithoutProduct(int productId, string indication)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.IndicationsDALC.Instance.getAllWithoutProduct(productId, indication);
        }

        #endregion

        #region Pharmaceutical Forms

        public PharmaceuticalFormInfo getPharmaceuticalForm(string name)
        {
            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOneByName(name);
        }

        public List<PharmaceuticalFormInfo> getPharmaForms(string description)
        {
            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getAllByFilter(description);
        }

        public List<PharmaceuticalFormInfo> getPharmaFormsByProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getAllByProduct(productId);
        }

        public List<ProductPharmaFormInfo> getPharmaFormsSubstanceByProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.ProductPharmaFormsDALC.Instance.getAllByProduct(productId);
        }

        public List<PharmaTherapeuticInfo> getPharmaTherapeuticByProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getPharmaTherapeutic(productId);
        }

        public PharmaceuticalFormInfo getPharmaForm(int pharmaFormId)
        {
            if (pharmaFormId <= 0)
                throw new ArgumentException("The pharmaceutical form does not exist.");

            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(pharmaFormId);
        }

        public List<PharmaceuticalFormInfo> getPharmaFormsWithoutProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.PharmaceuticalFormsDALC.Instance.getAllWithoutProduct(productId);
        }

        #endregion

        #region ProductTypes

        public List<ProductTypeInfo> getProductTypes()
        {
            return MedinetDataAccessComponent.ProductTypeDALC.Instance.getProductTypes();
        }

        #endregion

        #region Symptoms
        public List<MedinetBusinessEntries.SymptomInfo> getSymptoms(string searchName) {
            return MedinetDataAccessComponent.SymptomsDALC.Instance.getSymptomsCatalog(searchName);
        }

        public List<MedinetBusinessEntries.SymptomInfo> getSymptomsByProduct(int productId,int pharmaFormId)
        {
            return MedinetDataAccessComponent.SymptomsDALC.Instance.getSymptomsByProduct(productId, pharmaFormId);
        }
        #endregion

        #region Hypersensibilities
        public List<MedinetBusinessEntries.HypersensibilitiesInfo> getHypersensibilities(string searchName)
        {
            return MedinetDataAccessComponent.HypersensibilitiesDALC.Instance.getHypersensibilities(searchName);
        }

        public List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo> getProductHypersensibilities(int productId,int pharmaFormId,int divisionId,int categoryId)
        {
            return MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.getProductHypersensibilities(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.HypersensibilitiesInfo> getHypersensibilitiesWithOutAttach(string searchName,int productId,int pharmaFormId,int divisionId,int categoryId)
        {
            
            List<HypersensibilitiesInfo> fullCatalog = this.getHypersensibilities(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductHypersensibilitiesContraindicationInfo> productHyp = this.getProductHypersensibilities(productId, pharmaFormId, divisionId, categoryId);
            List<ProductHypersensibilitiesContraindicationInfo> select;
            List<int> distinct;
            var hypDistinct = (from c in productHyp
                       select c.HypersensibilityId).Distinct();
            distinct = hypDistinct.ToList();

            foreach (int hyperId in distinct)
            {
                    var hyp = (from c in productHyp
                               where c.HypersensibilityId==hyperId
                               select c);
                    select = hyp.ToList();
                    if (productSubstances.Count == select.Count)
                    {
                        foreach (ProductHypersensibilitiesContraindicationInfo item in select)
                        {
                            fullCatalog.RemoveAll(x=> x.HypersensibilityId==item.HypersensibilityId);
                        }
                    }
            }
            return fullCatalog;
        }

        #endregion

        #region MedicalContraindication
        public List<MedinetBusinessEntries.MedicalContraindicationInfo> getMedicalContraindications(string searchName)
        {
            return MedinetDataAccessComponent.MedicalContraindicationDALC.Instance.getMedicalContraindications(searchName);
        }

        public List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> getProductMedicalContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.getProductMedicalContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.MedicalContraindicationInfo> getMedicalContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<MedicalContraindicationInfo> fullCatalog = this.getMedicalContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductMedicalContraindicationInfo> productHyp = this.getProductMedicalContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductMedicalContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productHyp
                               select c.ICDId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productHyp
                           where c.ICDId == value
                           select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductMedicalContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.ICDId == item.ICDId);
                    }
                }
            }
            return fullCatalog;
        }

        #endregion

        #region PhysiologicalContraindication
        public List<MedinetBusinessEntries.PhysiologicalContraindicationInfo> getPhysContraindications(string searchName)
        {
            return MedinetDataAccessComponent.PhysiologicalContraindicationsDALC.Instance.getPhysContraindications(searchName);
        }

        public List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> getProductPhysContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductPhysiologicalContraindicationsDALC.Instance.getProductPhysContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.PhysiologicalContraindicationInfo> getPhysContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<PhysiologicalContraindicationInfo> fullCatalog = this.getPhysContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductPhysiologicalContraindicationInfo> productCon = this.getProductPhysContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductPhysiologicalContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productCon
                             select c.PhysContraindicationId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productCon
                         where c.PhysContraindicationId == value
                         select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductPhysiologicalContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.PhysContraindicationId == item.PhysContraindicationId);
                    }
                }
            }
            return fullCatalog;
        }

        #endregion

        #region PharmacologicalContraindication
        public List<MedinetBusinessEntries.PharmacologicalContraindicationInfo> getPharmacologicalContraindications(string searchName)
        {
            return MedinetDataAccessComponent.PharmacologicalContraindicationDALC.Instance.getPharmaContraindications(searchName);
        }

        public List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo> getProductPharmacologicalContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.getProductPharmacologicalContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.PharmacologicalContraindicationInfo> getPharmacologicalContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<PharmacologicalContraindicationInfo> fullCatalog = this.getPharmacologicalContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductPharmacologicalContraindicationInfo> productCon = this.getProductPharmacologicalContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductPharmacologicalContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productCon
                             select c.PharmaContraindicationId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productCon
                         where c.PharmaContraindicationId == value
                         select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductPharmacologicalContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.PharmaContraindicationId == item.PharmaContraindicationId);
                    }
                }
            }
            return fullCatalog;
        }

        #endregion

        #region Pharmacological Groups Contraindication
        public List<MedinetBusinessEntries.PharmacologicalGroupsInfo> getPharmaGroupsContraindications(string searchName)
        {
            return MedinetDataAccessComponent.PharmacologicalGroupDALC.Instance.getAllGroupswithOutProductInteractions(0,0,0,0,searchName);
        }

        public List<MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo> getProductPharmaGroupContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.getProductPharmaGroupContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.PharmacologicalGroupsInfo> getPharmaGroupsContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<PharmacologicalGroupsInfo> fullCatalog = this.getPharmaGroupsContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductPharmaGroupContraindicationInfo> productCon = this.getProductPharmaGroupContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductPharmaGroupContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productCon
                             select c.PharmaGroupId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productCon
                         where c.PharmaGroupId == value
                         select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductPharmaGroupContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.PharmaGroupId == item.PharmaGroupId);
                    }
                }
            }
            return fullCatalog;
        }


        #endregion

        #region Substances Contraindications
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesContraindications(string searchName)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByFilter(searchName);
        }

        public List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> getProductSubstancesContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.getContraindicationSubstances(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<ActiveSubstanceInfo> fullCatalog = this.getSubstancesContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductSubstanceContraindicationInfo> productCon = this.getProductSubstancesContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductSubstanceContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productCon
                             select c.SubstanceContraindicationId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productCon
                         where c.SubstanceContraindicationId == value
                         select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductSubstanceContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.ActiveSubstanceId == item.SubstanceContraindicationId);
                    }
                }
            }
            return fullCatalog;
        }


        #endregion

        #region Other Contraindications
        public List<MedinetBusinessEntries.OtherElementsInfo> getOtherContraindications(string searchName)
        {
            return MedinetDataAccessComponent.OthersElementsDALC.Instance.getAllOthersElementswithOutProductInteractions(0,0,0,0,searchName);
        }

        public List<MedinetBusinessEntries.ProductOtherContraindicationInfo> getProductOtherContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.getProductOtherContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        public List<MedinetBusinessEntries.OtherElementsInfo> getOtherContraindicationsWithOutAttach(string searchName, int productId, int pharmaFormId, int divisionId, int categoryId)
        {

            List<OtherElementsInfo> fullCatalog = this.getOtherContraindications(searchName);
            List<ActiveSubstanceInfo> productSubstances = this.getSubstancesByProduct(productId);
            List<ProductOtherContraindicationInfo> productCon = this.getProductOtherContraindications(productId, pharmaFormId, divisionId, categoryId);
            List<ProductOtherContraindicationInfo> select;
            List<int> distinct;
            var cDistinct = (from c in productCon
                             select c.ElementId).Distinct();
            distinct = cDistinct.ToList();

            foreach (int value in distinct)
            {
                var s = (from c in productCon
                         where c.ElementId == value
                         select c);
                select = s.ToList();
                if (productSubstances.Count == select.Count)
                {
                    foreach (ProductOtherContraindicationInfo item in select)
                    {
                        fullCatalog.RemoveAll(x => x.ElementId == item.ElementId);
                    }
                }
            }
            return fullCatalog;
        }

        #endregion

        #region Commments Contraindications
        
        public List<MedinetBusinessEntries.ProductCommentContraindicationsInfo> getCommentsContraindications(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.getCommentsContraindications(productId, pharmaFormId, categoryId, divisionId);
        }

        #endregion

        #region Encyclopedias

        public string getSizes()
        {
            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.getSizes("Encyclopedias");
        }

        public int addImageToEncyclopedia(MedinetBusinessEntries.EncyclopediaImageInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            //BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insertImage(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductImages);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(EncyclopediaImageId," + BEntity.ImageId + ")";
            activityLog.FieldsAffected = "(EncyclopediaImage," + BEntity.encyclopediaImage + "); (Active," + BEntity.Active + "); (EncyclopediaId,"
                + BEntity.encyclopediaId + ");";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.insertImage(BEntity);


        }

        public void deleteImageToEncyclopedia(MedinetBusinessEntries.EncyclopediaImageInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            //BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insertImage(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductImages);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(EncyclopediaImageId," + BEntity.ImageId + ")";
            activityLog.FieldsAffected = "(EncyclopediaImage," + BEntity.encyclopediaImage + "); (Active," + BEntity.Active + "); (EncyclopediaId,"
                + BEntity.encyclopediaId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            MedinetDataAccessComponent.EncyclopediasDALC.Instance.deleteImage(BEntity);

        }
        public List<EncyclopediasInfo> getAllEncyclopediasWithOutProduct(int productId,int pharmaFormId,string encyclopedia) {

            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.getAllEncyclopediasWithOutProduct(productId,pharmaFormId,encyclopedia);

        }

        public List<EncyclopediasInfo> getEncyclopediasByProductByPharmaForm(int productId,int pharmaFormId)
        {
            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.getEncyclopediasByProductByPharmaForm(productId,pharmaFormId);
        }
        public List<EncyclopediasInfo> getEncyclopediasByCountry(int countryId,int bookId, int editionId)
        {
            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.getEncyclopediasByCountry(countryId,bookId,editionId);
        }
        public List<EncyclopediasInfo> getEncyclopediaImagesByCountry(int encyclopediaId)
        {
            return MedinetDataAccessComponent.EncyclopediasDALC.Instance.getImagesByEncyclopedias(encyclopediaId);
        }


        #endregion


        #region TherapeuticsOMS

        public List<TherapeuticOMSInfo> getOMSTherapeutics(int? parentId)
        {
            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getAll(parentId);
        }

        public List<TherapeuticOMSInfo> getOMSTherapeuticsByName(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getAllByName(description);
        }

        public TherapeuticOMSInfo getOMSTherapeutic(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getOne(description);
        }

        public List<TherapeuticOMSInfo> getOMSTherapeuticsByNameSp(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getAllByNameSp(description);
        }

        public TherapeuticOMSInfo getOMSTherapeuticSp(string description)
        {
            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getOneSp(description);
        }

        public TherapeuticOMSInfo getOMSTherapeuticById(int therapeuticOMSId)
        {
            if (therapeuticOMSId <= 0)
                throw new ArgumentException("The therapeutic does not exist.");

            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getOne(therapeuticOMSId);
        }

        public List<TherapeuticOMSInfo> getOMSTherapeuticsByProduct(int productId, int pharmaFormId)
        {
            if (productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getAllByProduct(productId, pharmaFormId);
        }

        public TherapeuticOMSInfo getOMSTheraByProduct(int productId, int pharmaFormId)
        {
            if (productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The product does not exist.");

            return MedinetDataAccessComponent.TherapeuticsOMSDALC.Instance.getOneByProduct(productId, pharmaFormId);
        }

        //public TherapeuticDescriptionInfo getDescription(int therapeuticId)
        //{
        //    if (therapeuticId <= 0)
        //        throw new ArgumentException("The therapeutic does not exist.");

        //    return MedinetDataAccessComponent.TherapeuticDescriptionsDALC.Instance.getOne(therapeuticId);
        //}

        //public bool checkTherapeuticGroup(int theraGroupId, int editionId, int productId, int pharmaFormId)
        //{
        //    if (theraGroupId <= 0 || editionId <= 0 || productId <= 0 || pharmaFormId <= 0)
        //        throw new ArgumentException("The therapeutic Group, edition, pharmaceutical form or product does not exist.");

        //    return MedinetDataAccessComponent.TherapeuticGroupProductsDALC.Instance.checkGroup(theraGroupId, editionId, pharmaFormId, productId);
        //}

        #endregion

        #endregion

        #region Enum

        public enum TypeInEdition : byte
        {
            Participante = 1,
            Mencionado = 2
        }

        #endregion

        public static readonly CatalogsBLC Instance = new CatalogsBLC();
    }
}
