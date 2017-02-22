using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_TherapeuticsDALC : CE_PharmaSearchEngineDataAccessAdapter<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo>
    {
        #region Constructors

        private CE_TherapeuticsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.TherapeuticInfo> getBySpanishDescription(string spanishDescription)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT t.TherapeuticId, t.ParentId, t.TherapeuticKey, t.[Description], t.SpanishDescription, t.Active");
            sb.Append("\nFROM Therapeutics t");
            sb.Append("\nWHERE t.[SpanishDescription] LIKE '" + spanishDescription + "'");
            sb.Append("\nORDER BY TherapeuticKey");

            List<MedinetBusinessEntries.TherapeuticInfo> BECollection = new List<MedinetBusinessEntries.TherapeuticInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                {
                    MedinetBusinessEntries.TherapeuticInfo record = new MedinetBusinessEntries.TherapeuticInfo();

                    record.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                        record.ParentId = Convert.ToInt32(dataReader["ParentId"]);
                    else
                        record.ParentId = null;

                    record.TherapeuticKey = dataReader["TherapeuticKey"].ToString();
                    record.Description = dataReader["Description"].ToString();
                    record.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    // Get the number of children:
                    sb.Clear();
                    sb.Append("\nSELECT COUNT(*) AS TherapeuticSons FROM Therapeutics WHERE ParentId = " + record.TherapeuticId);

                    using (IDataReader dataReaderSons = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
                    {
                        if (dataReaderSons.Read())
                            record.TherapeuticSons = Convert.ToInt32(dataReaderSons["TherapeuticSons"]);
                    }

                    BECollection.Add(record);
                }        
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<MedinetBusinessEntries.TherapeuticInfo> getByParent(int? therapeuticId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT t.TherapeuticId, t.ParentId, t.TherapeuticKey, t.[Description], t.SpanishDescription, t.Active");
            sb.Append("\nFROM Therapeutics t");

            if (therapeuticId != null)
                sb.Append("\nWHERE	t.ParentId = " + therapeuticId.ToString());
            else
                sb.Append("\nWHERE	t.ParentId IS NULL ");

            sb.Append("\nORDER BY TherapeuticKey");

            List<MedinetBusinessEntries.TherapeuticInfo> BECollection = new List<MedinetBusinessEntries.TherapeuticInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                {
                    MedinetBusinessEntries.TherapeuticInfo record = new MedinetBusinessEntries.TherapeuticInfo();

                    record.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                        record.ParentId = Convert.ToInt32(dataReader["ParentId"]);
                    else
                        record.ParentId = null;

                    record.TherapeuticKey = dataReader["TherapeuticKey"].ToString();
                    record.Description = dataReader["Description"].ToString();
                    record.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    // Get the number of children:
                    sb.Clear();
                    sb.Append("\nSELECT COUNT(*) AS TherapeuticSons FROM Therapeutics WHERE ParentId = " + record.TherapeuticId);

                    using (IDataReader dataReaderSons = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
                    {
                        if (dataReaderSons.Read())
                            record.TherapeuticSons = Convert.ToInt32(dataReaderSons["TherapeuticSons"]);
                    }

                    BECollection.Add(record);
                }
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;    
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getAll(byte countryId, int editionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct t.TherapeuticId, t.TherapeuticKey, t.SpanishDescription as Therapeutic,v.ProductId,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.CategoryId ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt ");
            sb.Append("\n On(v.ProductId = pt.ProductId and v.PharmaFormId = pt.PharmaFormId) Inner Join Therapeutics t ");
            sb.Append("\n On(pt.TherapeuticId = t.TherapeuticId)  ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId);
            sb.Append("\n Order By t.SpanishDescription ");

            List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getByProduct(byte countryId, int editionId, int productId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct t.TherapeuticId, t.TherapeuticKey, t.SpanishDescription as Therapeutic,v.ProductId,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.CategoryId ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt ");
            sb.Append("\n On(v.ProductId = pt.ProductId and v.PharmaFormId = pt.PharmaFormId) Inner Join Therapeutics t ");
            sb.Append("\n On(pt.TherapeuticId = t.TherapeuticId)  ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId);
            sb.Append("\n Order By t.SpanishDescription");

            List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getByProductByPharmaForm(byte countryId, int editionId, int productId, int pharmaFormId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct t.TherapeuticId, t.TherapeuticKey, t.SpanishDescription as Therapeutic,v.ProductId,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.CategoryId ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt ");
            sb.Append("\n On(v.ProductId = pt.ProductId and v.PharmaFormId = pt.PharmaFormId) Inner Join Therapeutics t ");
            sb.Append("\n On(pt.TherapeuticId = t.TherapeuticId)  ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId + " and v.PharmaFormId = " + pharmaFormId);
            sb.Append("\n Order By t.SpanishDescription ");

            List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getByProductByTherapeutic(byte countryId, int editionId, int productId, int therapeuticId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct t.TherapeuticId, t.TherapeuticKey, t.SpanishDescription as Therapeutic,v.ProductId,v.PharmaFormId,v.PharmaForm,v.DivisionId,v.CategoryId ");
            sb.Append("\n From ProductsByEdition v Inner Join ProductTherapeutics pt ");
            sb.Append("\n On(v.ProductId = pt.ProductId and v.PharmaFormId = pt.PharmaFormId) Inner Join Therapeutics t ");
            sb.Append("\n On(pt.TherapeuticId = t.TherapeuticId)  ");
            sb.Append("\n Where v.ProductActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId + " and pt.TherapeuticId = " + therapeuticId);
            sb.Append("\n Order By t.SpanishDescription ");

            List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo>();

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public MedinetBusinessEntries.TherapeuticInfo getByTherapeuticId(int? therapeuticId)
        {
            if (therapeuticId == null)
                return null;

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT t.TherapeuticId, t.ParentId, t.TherapeuticKey, t.[Description], t.SpanishDescription, t.Active");
            sb.Append("\nFROM Therapeutics t");
            sb.Append("\nWHERE	t.TherapeuticId = " + therapeuticId.ToString());

            MedinetBusinessEntries.TherapeuticInfo record = null;

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.TherapeuticInfo();

                    record.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                        record.ParentId = Convert.ToInt32(dataReader["ParentId"]);
                    else
                        record.ParentId = null;

                    record.TherapeuticKey = dataReader["TherapeuticKey"].ToString();
                    record.Description = dataReader["Description"].ToString();
                    record.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    // Get the number of children:
                    sb.Clear();
                    sb.Append("\nSELECT COUNT(*) AS TherapeuticSons FROM Therapeutics WHERE ParentId = " + record.TherapeuticId);

                    using (IDataReader dataReaderSons = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
                    {
                        if (dataReaderSons.Read())
                            record.TherapeuticSons = Convert.ToInt32(dataReaderSons["TherapeuticSons"]);
                    }
                }
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public MedinetBusinessEntries.TherapeuticInfo getByParams(string searchParams)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT TherapeuticId, ParentId, TherapeuticKey, [Description], SpanishDescription, Active");
            sb.Append("\nFROM Therapeutics ");
            sb.Append("\nWHERE " + searchParams);

            MedinetBusinessEntries.TherapeuticInfo record = null;

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.TherapeuticInfo();

                    record.TherapeuticId = Convert.ToInt32(dataReader["TherapeuticId"]);

                    if (dataReader["ParentId"] != DBNull.Value)
                        record.ParentId = Convert.ToInt32(dataReader["ParentId"]);
                    else
                        record.ParentId = null;

                    record.TherapeuticKey = dataReader["TherapeuticKey"].ToString();
                    record.Description = dataReader["Description"].ToString();
                    record.SpanishDescription = dataReader["SpanishDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    // Get the number of children:
                    sb.Clear();
                    sb.Append("\nSELECT COUNT(*) AS TherapeuticSons FROM Therapeutics WHERE ParentId = " + record.TherapeuticId);

                    using (IDataReader dataReaderSons = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
                    {
                        if (dataReaderSons.Read())
                            record.TherapeuticSons = Convert.ToInt32(dataReaderSons["TherapeuticSons"]);
                    }
                }
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;   
        }

        public int getMinTherapeuticValue()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT MIN(TherapeuticId) AS MinTherapeuticValue FROM Therapeutics");
   
            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            int minTherapeuticValue = -1;

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    if (dataReader["MinTherapeuticValue"] != DBNull.Value)
                        minTherapeuticValue = Convert.ToInt32(dataReader["MinTherapeuticValue"]);
                }
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return minTherapeuticValue;
        }

        public int getMaxTherapeuticValue()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT MAX(TherapeuticId) AS MaxTherapeuticValue FROM Therapeutics");

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CreateConnection();

            int maxTherapeuticValue = -1;

            using (IDataReader dataReader = CE_TherapeuticsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    if (dataReader["MaxTherapeuticValue"] != DBNull.Value)
                        maxTherapeuticValue = Convert.ToInt32(dataReader["MaxTherapeuticValue"]);
                }
            }

            CE_TherapeuticsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return maxTherapeuticValue;
        }

        #endregion

        #region Protected Methods

        protected override PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo getFromDataReader(IDataReader current)
        {
            PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo record = new PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo();

            if (current["TherapeuticId"] != DBNull.Value)
                record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);
            else
                record.TherapeuticId = null;

            if (current["TherapeuticKey"] != DBNull.Value)
                record.TherapeuticKey = current["TherapeuticKey"].ToString();
            else
                record.TherapeuticKey = null;

            if (current["Therapeutic"] != DBNull.Value)
                record.Therapeutic = current["Therapeutic"].ToString();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);

            return record;
        }

        #endregion

        public static readonly CE_TherapeuticsDALC Instance = new CE_TherapeuticsDALC();
    }
}
