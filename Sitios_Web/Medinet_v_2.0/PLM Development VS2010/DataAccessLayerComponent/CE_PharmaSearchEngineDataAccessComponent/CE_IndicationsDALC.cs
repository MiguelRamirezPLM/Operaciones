using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_IndicationsDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.IndicationInfo>
    {
        #region Constructors

        private CE_IndicationsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.IndicationInfo> getAll(byte countryId, int editionId, string description)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct i.IndicationId, i.ParentId, i.Description, i.Active");
            sb.Append("\n From ProductsByEdition v  ");
            sb.Append("\n Inner Join ProductIndications pin ");
            sb.Append("\n On(v.ProductId = pin.ProductId) Inner Join Indications i  ");
            sb.Append("\n On(pin.IndicationId = i.IndicationId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n i.Description LIKE ('" + description + "') ");
            sb.Append("\n Order By i.Description");

            List<MedinetBusinessEntries.IndicationInfo> BECollection = new List<MedinetBusinessEntries.IndicationInfo>();

            CE_IndicationsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_IndicationsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_IndicationsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<MedinetBusinessEntries.IndicationInfo> getByProduct(byte countryId, int editionId, int productId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct i.IndicationId, i.ParentId, i.Description, i.Active");
            sb.Append("\n From ProductsByEdition v ");
            sb.Append("\n Inner Join ProductIndications pin ");
            sb.Append("\n On(v.ProductId = pin.ProductId) Inner Join Indications i  ");
            sb.Append("\n On(pin.IndicationId = i.IndicationId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId);
            sb.Append("\n Order By i.Description");

            List<MedinetBusinessEntries.IndicationInfo> BECollection = new List<MedinetBusinessEntries.IndicationInfo>();

            CE_IndicationsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_IndicationsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_IndicationsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override MedinetBusinessEntries.IndicationInfo getOne(int indicationId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect IndicationId, ParentId, Description, Active ");
            sb.Append("\nFrom Indications ");
            sb.Append("\nWhere IndicationId = " + indicationId);

            MedinetBusinessEntries.IndicationInfo record = null;

            CE_IndicationsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_IndicationsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader); 
            }

            CE_IndicationsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public MedinetBusinessEntries.IndicationInfo getByParams(string paramSearch)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect IndicationId, ParentId, Description, Active ");
            sb.Append("\nFrom Indications ");
            sb.Append("\n Where " + paramSearch);

            MedinetBusinessEntries.IndicationInfo record = null;

            CE_IndicationsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_IndicationsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_IndicationsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.IndicationInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.IndicationInfo record = new MedinetBusinessEntries.IndicationInfo();

            record.IndicationId = Convert.ToInt32(current["IndicationId"]);
            record.Description = current["Description"].ToString();

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);
            else
                record.ParentId = null;

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly CE_IndicationsDALC Instance = new CE_IndicationsDALC();

    }
}
