using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_ProductsDALC : CE_PharmaSearchEngineDataAccessAdapter<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>
    {
        #region Constructors

        private CE_ProductsDALC() { }

        #endregion

        #region Public Methods

        public PharmaSearchEngineBusinessEntries.DrugInfo getById(byte countryId, int editionId, int productId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct ProductId, Brand, ProductDescription, ProductActive, ");
            sb.Append("\n DivisionId, DivisionName, DivisionShortName, DivisionActive, ");
            sb.Append("\n CategoryId, CategoryName, CategoryActive ");
            sb.Append("\n From ProductsByEdition ");
            sb.Append("\n Where ProductActive = 1 And DivisionActive = 1 And ");
            sb.Append("\n ProductId = " + productId);

            PharmaSearchEngineBusinessEntries.DrugInfo record = null;

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.DrugInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.DivisionActive = Convert.ToBoolean(dataReader["DivisionActive"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();
                    record.CategoryActive = Convert.ToBoolean(dataReader["CategoryActive"]);

                    record.PharmaForms = CE_PharmaFormsDALC.Instance.getByProduct(countryId, editionId, productId);
                }
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public PharmaSearchEngineBusinessEntries.DrugPharmaFormInfo getByIdByPharmaForm(int productId, int pharmaFormId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select ProductId, Brand, ProductDescription, ProductActive, ");
            sb.Append("\n PharmaFormId, PharmaForm, PharmaActive, ");
            sb.Append("\n DivisionId, DivisionName, DivisionShortName, DivisionActive, ");
            sb.Append("\n CategoryId, CategoryName, CategoryActive ");
            sb.Append("\n From ProductsByEdition ");
            sb.Append("\n Where ProductActive = 1 and ProductActive = 1 And PharmaActive = 1 And DivisionActive = 1 And ");
            sb.Append("\n ProductId = " + productId + " And PharmaFormId = " + pharmaFormId);

            PharmaSearchEngineBusinessEntries.DrugPharmaFormInfo record = null;

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.DrugPharmaFormInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.PharmaActive = Convert.ToBoolean(dataReader["PharmaActive"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.DivisionActive = Convert.ToBoolean(dataReader["DivisionActive"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();
                    record.CategoryActive = Convert.ToBoolean(dataReader["CategoryActive"]);
                }
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public PharmaSearchEngineBusinessEntries.ProductByEditionInfo getByParams(string paramSearch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Where v.ProductActive = 1 and " + paramSearch);

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            PharmaSearchEngineBusinessEntries.ProductByEditionInfo productByEditionInfo = null;

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    productByEditionInfo = this.getFromDataReader(dataReader);
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return productByEditionInfo;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAll(byte countryId, int editionId, string brand)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Where v.ProductActive = 1 and v.DivisionActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.Brand LIKE ('" + brand + "') ");
            sb.Append("\n Order By v.Brand");

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByDivision(byte countryId, int editionId, int divisionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId);
            sb.Append("\n Order By v.Brand");

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByActiveSubstance(byte countryId, int editionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n ps.ActiveSubstanceId = " + activeSubstanceId);
            sb.Append("\n Order By v.Brand");

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneByActiveSubstance(byte countryId, int editionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n ps.ActiveSubstanceId = " + activeSubstanceId + " and v.NumberOfActiveSubstances = 1 ");
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedByActiveSubstance(byte countryId, int editionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n ps.ActiveSubstanceId = " + activeSubstanceId + " and v.NumberOfActiveSubstances > 1 ");
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId + " and ps.ActiveSubstanceId = " + activeSubstanceId);
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId + " and ps.ActiveSubstanceId = " + activeSubstanceId + " and v.NumberOfActiveSubstances = 1 ");
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedByDivisionByActiveSubstance(byte countryId, int editionId, int divisionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId + " and ps.ActiveSubstanceId = " + activeSubstanceId + " and v.NumberOfActiveSubstances > 1 ");
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByIndication(byte countryId, int editionId, int indicationId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductIndications pin On(v.ProductId = pin.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pin.IndicationId = " + indicationId);
            sb.Append("\nOrder By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByDivisionByIndication(byte countryId, int editionId, int divisionId, int indicationId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductIndications pin On(v.ProductId = pin.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId + " and pin.IndicationId = " + indicationId);
            sb.Append("\n Order By v.Brand ");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByTherapeutic(byte countryId, int editionId, int therapeuticId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt On(v.ProductId = pt.ProductId And v.PharmaFormId = pt.PharmaFormId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pt.TherapeuticId = " + therapeuticId);
            sb.Append("\n Order By v.Brand");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt On(v.ProductId = pt.ProductId And v.PharmaFormId = pt.PharmaFormId) ");
            sb.Append("\n Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pt.TherapeuticId = " + therapeuticId + " and ps.ActiveSubstanceId = " + activeSubstanceId);
            sb.Append("\n Order By v.Brand");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt On(v.ProductId = pt.ProductId And v.PharmaFormId = pt.PharmaFormId) ");
            sb.Append("\n Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pt.TherapeuticId = " + therapeuticId + " and ps.ActiveSubstanceId = " + activeSubstanceId + " and ");
            sb.Append("\n v.NumberOfActiveSubstances = 1 ");
            sb.Append("\n Order By v.Brand");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedByTherapeuticByActiveSubstance(byte countryId, int editionId, int therapeuticId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt On(v.ProductId = pt.ProductId And v.PharmaFormId = pt.PharmaFormId) ");
            sb.Append("\n Inner Join ProductSubstances ps On(v.ProductId = ps.ProductId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pt.TherapeuticId = " + therapeuticId + " and ps.ActiveSubstanceId = " + activeSubstanceId + " and ");
            sb.Append("\n v.NumberOfActiveSubstances > 1 ");
            sb.Append("\n Order By v.Brand");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public MedinetBusinessEntries.ParticipantProductsInfo getContent(int editionId, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, Page, HTMLContent, XMLContent ");
            sb.Append("\n From ParticipantProducts ");
            sb.Append("\n Where EditionId = " + editionId + " and DivisionId = " + divisionId + " and CategoryId = " + categoryId + " and ");
            sb.Append("\n ProductId = " + productId + " and PharmaFormId = " + pharmaFormId);

            MedinetBusinessEntries.ParticipantProductsInfo record = null;

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ParticipantProductsInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);

                    if (dataReader["HTMLContent"] != DBNull.Value)
                        record.HTMLContent = dataReader["HTMLContent"].ToString();
                    else
                        record.HTMLContent = null;

                    if (dataReader["Page"] != DBNull.Value)
                        record.Page = dataReader["Page"].ToString();
                    else
                        record.Page = null;

                    if (dataReader["XMLContent"] != DBNull.Value)
                        record.XMLContent = dataReader["XMLContent"].ToString();
                    else
                        record.XMLContent = null;
                }
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> searchText(byte countryId, int editionId, string attributes, string text)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName,v.CountryCodes ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductContents pc  ");
            sb.Append("\n On(v.EditionId = pc.EditionId and v.DivisionId = pc.DivisionId and v.CategoryId = v.CategoryId and v.ProductId = pc.ProductId and v.PharmaFormId = pc.PharmaFormId) ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pc.AttributeId In(" + attributes + ") and pc.PlainContent LIKE ('%" + text + "%') ");
            sb.Append("\n Order By v.Brand");

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public bool checkContent(int editionId, int productId, int pharmaFormId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT HtmlContent ");
            sb.Append("\n From ParticipantProducts ");
            sb.Append("\n Where EditionId = " + editionId + " and ProductId = " + productId + " and PharmaFormId = " + pharmaFormId);

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            int prod = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0].Rows.Count;

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return prod > 0;
        }

        public int getAll(byte countryId, int editionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.DivisionName,v.DivisionShortName,v.CategoryId,v.CategoryName ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Where v.DivisionActive = 1 and v.ProductActive = 1 and ");
            sb.Append("\n v.CountryId = " + countryId + " and v.EditionId = " + editionId);

            List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo>();

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection.Count;
        }

        #endregion

        #region Protected Methods

        protected override PharmaSearchEngineBusinessEntries.ProductByEditionInfo getFromDataReader(IDataReader current)
        {
            PharmaSearchEngineBusinessEntries.ProductByEditionInfo record = new PharmaSearchEngineBusinessEntries.ProductByEditionInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();

            if (current["DivisionShortName"] != DBNull.Value)
                record.DivisionShortName = current["DivisionShortName"].ToString();
            else
                record.DivisionShortName = null;

            if (current["CountryCodes"] != DBNull.Value)
                record.CountryCodes = current["CountryCodes"].ToString();
            else
                record.CountryCodes = null;

            record.CategotyId = Convert.ToInt32(current["CategoryId"]);
            record.CategoryName = current["CategoryName"].ToString();

            return record;
        }

        #endregion

        public static readonly CE_ProductsDALC Instance = new CE_ProductsDALC();

    }
}
