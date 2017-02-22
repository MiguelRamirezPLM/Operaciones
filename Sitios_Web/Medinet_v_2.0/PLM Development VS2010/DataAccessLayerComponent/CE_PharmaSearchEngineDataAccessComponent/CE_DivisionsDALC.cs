using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_DivisionsDALC : CE_PharmaSearchEngineDataAccessAdapter<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo>
    {
        #region Constructors

        private CE_DivisionsDALC() { }

        #endregion

        #region Public Methods

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getAll(byte countryId, int editionId, string description)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.DivisionId, v.DivisionName, v.DivisionActive, v.DivisionShortName, d.LaboratoryId, v.CountryId ");
            sb.Append("\n From ProductsByEdition v Inner Join Divisions d On(v.DivisionId = d.DivisionId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.DivisionActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId);
            sb.Append("\n and v.DivisionName Like('" + description + "') ");
            sb.Append("\n Order By v.DivisionName");

            List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo>();

            // Create the connection object:
            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getByActiveSubstance(byte countryId, int editionId, int activeSubstanceId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.DivisionId, v.DivisionName, v.DivisionActive, v.DivisionShortName, d.LaboratoryId, v.CountryId ");
            sb.Append("\n From ProductsByEdition v Inner Join Divisions d On(v.DivisionId = d.DivisionId) ");
            sb.Append("\n Inner Join ProductSubstances ps");
            sb.Append("\n On(v.ProductId = ps.ProductId)");
            sb.Append("\n Where v.ProductActive = 1 and v.DivisionActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId);
            sb.Append("\n And ps.ActiveSubstanceId = " + activeSubstanceId);
            sb.Append("\n Order By v.DivisionName");

            List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo>();

            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getByIndication(byte countryId, int editionId, int indicationId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.DivisionId, v.DivisionName, v.DivisionActive, v.DivisionShortName, d.LaboratoryId, v.CountryId ");
            sb.Append("\n From ProductsByEdition v Inner Join Divisions d On(v.DivisionId = d.DivisionId) ");
            sb.Append("\n Inner Join ProductIndications pin");
            sb.Append("\n On(v.ProductId = pin.ProductId)");
            sb.Append("\n Where v.ProductActive = 1 and v.DivisionActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId);
            sb.Append("\n And IndicationId = " + indicationId);
            sb.Append("\n Order By v.DivisionName");

            List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo>();

            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public int getAll(int editionId, byte countryId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.DivisionId, v.DivisionName, v.DivisionActive, v.DivisionShortName, d.LaboratoryId, v.CountryId ");
            sb.Append("\n From ProductsByEdition v Inner Join Divisions d On(v.DivisionId = d.DivisionId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.DivisionActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId);
            sb.Append("\n Order By DivisionName");

            List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo>();

            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection.Count;
        }

        public MedinetBusinessEntries.DivisionsInfo getById(int divisionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select DivisionId, ParentId, LaboratoryId, Description, ShortName, CountryId, Active ");
            sb.Append("\n From Divisions ");
            sb.Append("\n Where Active = 1 and  DivisionId = " + divisionId);

            MedinetBusinessEntries.DivisionsInfo record = null;

            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.DivisionsInfo();

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.LaboratoryId = Convert.ToInt32(dataReader["LaboratoryId"]);
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public MedinetBusinessEntries.DivisionInformationInfo getDivisionInformation(int divisionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect DivisionInfId, DivisionId, Image, Address, Suburb, ZipCode, Telephone, Fax, Web, Email, City, State, Contact, Active ");
            sb.Append("\nFrom DivisionInformation ");
            sb.Append("\nWhere Active = 1 and DivisionId = " + divisionId);

            MedinetBusinessEntries.DivisionInformationInfo record = null;

            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.DivisionInformationInfo();

                    record.DivisionInfId = Convert.ToInt32(dataReader["DivisionInfId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.Address = dataReader["Address"].ToString();

                    if (dataReader["Suburb"] != System.DBNull.Value)
                        record.Suburb = dataReader["Suburb"].ToString();

                    if (dataReader["ZipCode"] != System.DBNull.Value)
                        record.ZipCode = dataReader["ZipCode"].ToString();

                    if (dataReader["Telephone"] != System.DBNull.Value)
                        record.Telephone = dataReader["Telephone"].ToString();

                    if (dataReader["Fax"] != System.DBNull.Value)
                        record.Fax = dataReader["Fax"].ToString();

                    if (dataReader["Web"] != System.DBNull.Value)
                        record.Web = dataReader["Web"].ToString();

                    if (dataReader["City"] != System.DBNull.Value)
                        record.City = dataReader["City"].ToString();

                    if (dataReader["State"] != System.DBNull.Value)
                        record.State = dataReader["State"].ToString();

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    if (dataReader["Contact"] != System.DBNull.Value)
                        record.Contact = dataReader["Contact"].ToString();

                    if (dataReader["Image"] != System.DBNull.Value)
                        record.Image = dataReader["Image"].ToString();

                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public PharmaSearchEngineBusinessEntries.DivisionByEditionInfo getByParams(string searchParam)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.DivisionId, v.DivisionName, v.DivisionActive, v.DivisionShortName, d.LaboratoryId, v.CountryId ");
            sb.Append("\n From ProductsByEdition v Inner Join Divisions d On(v.DivisionId = d.DivisionId) ");
            sb.Append("\n Where v." + searchParam);

            PharmaSearchEngineBusinessEntries.DivisionByEditionInfo record = null;

            // Create the connection object:
            CE_DivisionsDALC.PharmaInstanceDatabase.CreateConnection();

            // Get the result set:
            using (IDataReader dataReader = CE_DivisionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_DivisionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;    
        }

        #endregion

        #region Protected Methods

        protected override PharmaSearchEngineBusinessEntries.DivisionByEditionInfo getFromDataReader(IDataReader current)
        {
            PharmaSearchEngineBusinessEntries.DivisionByEditionInfo record = new PharmaSearchEngineBusinessEntries.DivisionByEditionInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.Description = current["DivisionName"].ToString();
            record.Active = Convert.ToBoolean(current["DivisionActive"]);

            if (current["DivisionShortName"] != System.DBNull.Value)
                record.ShortName = current["DivisionShortName"].ToString();
            else
                record.ShortName = null;


            record.CountryId = Convert.ToInt32(current["CountryId"]);
            

            return record;
        }

        #endregion

        public static readonly CE_DivisionsDALC Instance = new CE_DivisionsDALC();
    }
}
