using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_PharmaFormsDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.PharmaceuticalFormInfo>
    {
        #region Constructors

        private CE_PharmaFormsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getAll(byte countryId, int editionId, string description)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.PharmaFormId, v.PharmaForm, v.PharmaActive");
            sb.Append("\n From ProductsByEdition v");
            sb.Append("\n Where v.ProductActive = 1 and v.PharmaActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.PharmaForm LIKE ('" + description + "') ");
            sb.Append("\n Order By v.PharmaForm");

            List<MedinetBusinessEntries.PharmaceuticalFormInfo> BECollection = new List<MedinetBusinessEntries.PharmaceuticalFormInfo>();

            CE_PharmaFormsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PharmaFormsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_PharmaFormsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getByProduct(byte countryId, int editionId, int productId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct v.PharmaFormId, v.PharmaForm, v.PharmaActive");
            sb.Append("\n From ProductsByEdition v");
            sb.Append("\n Where v.ProductActive = 1 and v.PharmaActive = 1 and v.CountryId = " + countryId + " and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId);
            sb.Append("\n Order By v.PharmaForm ");

            List<MedinetBusinessEntries.PharmaceuticalFormInfo> BECollection = new List<MedinetBusinessEntries.PharmaceuticalFormInfo>();

            CE_PharmaFormsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PharmaFormsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_PharmaFormsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.PharmaceuticalFormInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.PharmaceuticalFormInfo record = new MedinetBusinessEntries.PharmaceuticalFormInfo();

            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.Description = current["PharmaForm"].ToString();
            record.Active = Convert.ToBoolean(current["PharmaActive"]);

            return record;
        }

        #endregion

        public static readonly CE_PharmaFormsDALC Instance = new CE_PharmaFormsDALC();

    }
}
