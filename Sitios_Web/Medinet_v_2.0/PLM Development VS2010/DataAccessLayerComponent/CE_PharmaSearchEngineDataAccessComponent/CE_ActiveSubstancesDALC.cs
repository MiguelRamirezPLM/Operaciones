using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_ActiveSubstancesDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.ActiveSubstanceInfo>
    {
        #region Constructors

        private CE_ActiveSubstancesDALC() { } 

        #endregion

        #region Public Methods
        
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getAll(byte countryId, int editionId, string description)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct a.ActiveSubstanceId, a.Description, a.EnglishDescription, a.Enunciative, a.Active ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Inner Join ProductSubstances ps ");
            sb.Append("\n On(v.ProductId = ps.ProductId) Inner Join ActiveSubstances a  ");
            sb.Append("\n On(ps.ActiveSubstanceId = a.ActiveSubstanceId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n Description LIKE ('" + description + "') ");
            sb.Append("\n Order By a.Description");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByDivision(byte countryId, int editionId, int divisionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct a.ActiveSubstanceId, a.Description, a.EnglishDescription, a.Enunciative, a.Active ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Inner Join ProductSubstances ps ");
            sb.Append("\n On(v.ProductId = ps.ProductId) Inner Join ActiveSubstances a  ");
            sb.Append("\n On(ps.ActiveSubstanceId = a.ActiveSubstanceId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.DivisionId = " + divisionId);
            sb.Append("\n Order By a.Description");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByProduct(byte countryId, int editionId, int productId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct a.ActiveSubstanceId, a.Description, a.EnglishDescription, a.Enunciative, a.Active ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Inner Join ProductSubstances ps ");
            sb.Append("\n On(v.ProductId = ps.ProductId) Inner Join ActiveSubstances a  ");
            sb.Append("\n On(ps.ActiveSubstanceId = a.ActiveSubstanceId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId);
            sb.Append("\n Order By a.Description");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByProductWithoutActiveSubstance(byte countryId, int editionId, int productId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct a.ActiveSubstanceId, a.Description, a.EnglishDescription, a.Enunciative, a.Active ");
            sb.Append("\n From ProductsByEdition v  ");
            sb.Append("\n Inner Join ProductSubstances ps ");
            sb.Append("\n On(v.ProductId = ps.ProductId) Inner Join ActiveSubstances a  ");
            sb.Append("\n On(ps.ActiveSubstanceId = a.ActiveSubstanceId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId + " and a.ActiveSubstanceId <> " + activeSubstanceId);
            sb.Append("\n Order By a.Description");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByTherapeutic(byte countryId, int editionId, int therapeuticId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct a.ActiveSubstanceId, a.Description, a.EnglishDescription, a.Enunciative, a.Active ");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Inner Join ProductSubstances ps ");
            sb.Append("\n On(v.ProductId = ps.ProductId) Inner Join ActiveSubstances a  ");
            sb.Append("\n On(ps.ActiveSubstanceId = a.ActiveSubstanceId) Inner Join ProductTherapeutics pt  ");
            sb.Append("\n On(v.ProductId = pt.ProductId and v.PharmaFormId = pt.PharmaFormId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n pt.TherapeuticId = " + therapeuticId);
            sb.Append("\n Order By a.Description");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override MedinetBusinessEntries.ActiveSubstanceInfo getOne(int pk)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect ActiveSubstanceId, Description, EnglishDescription, Enunciative, Active ");
            sb.Append("\nFrom ActiveSubstances ");
            sb.Append("\nWhere ActiveSubstanceId = " + pk);

            MedinetBusinessEntries.ActiveSubstanceInfo record = null;

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_ActiveSubstancesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_ActiveSubstancesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public MedinetBusinessEntries.ActiveSubstanceInfo getByParams(string paramSearch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select ActiveSubstanceId, Description, EnglishDescription, Enunciative, Active");
            sb.Append("\n From ActiveSubstances");
            sb.Append("\n Where " + paramSearch);

            CE_ProductsDALC.PharmaInstanceDatabase.CreateConnection();

            MedinetBusinessEntries.ActiveSubstanceInfo activeSubstanceInfo = null;

            using (IDataReader dataReader = CE_ProductsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    activeSubstanceInfo = this.getFromDataReader(dataReader);
            }

            CE_ProductsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return activeSubstanceInfo;
        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.ActiveSubstanceInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo record = new MedinetBusinessEntries.ActiveSubstanceInfo();

            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.Description = current["Description"].ToString();

            if (current["EnglishDescription"] != DBNull.Value)
                record.EnglishDescription = current["EnglishDescription"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);
            record.Enunciative = Convert.ToBoolean(current["Enunciative"]);
            
            return record;
        }

        #endregion

        public static readonly CE_ActiveSubstancesDALC Instance = new CE_ActiveSubstancesDALC();
    }
}
