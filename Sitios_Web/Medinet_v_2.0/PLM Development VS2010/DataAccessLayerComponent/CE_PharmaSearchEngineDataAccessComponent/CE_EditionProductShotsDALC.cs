using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using PharmaSearchEngineBusinessEntries;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_EditionProductShotsDALC : CE_PharmaSearchEngineDataAccessAdapter<EditionProductShotInfo>
    {
        #region Constructors

        private CE_EditionProductShotsDALC() { }

        #endregion

        #region Public methods

        public List<EditionProductShotInfo> getEditionProductShots(int editionId, ProductByEditionInfo productByEdition)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT eps.EditionProductShotId, eps.EditionId, eps.DivisionId, ");
            sb.Append("\n eps.CategoryId, eps.PharmaFormId, eps.ProductId, eps.PSTypeId,");
            sb.Append("\n eps.ProductShot, eps.Active");
            sb.Append("\n FROM EditionProductShots eps");
            sb.Append("\n WHERE	eps.EditionId = " + editionId.ToString() + " AND");
            sb.Append("\n eps.DivisionId = " + productByEdition.DivisionId.ToString() + " AND");
            sb.Append("\n eps.CategoryId = " + productByEdition.CategotyId.ToString() + " AND");
            sb.Append("\n eps.PharmaFormId = " + productByEdition.PharmaFormId.ToString() + " AND");
            sb.Append("\n eps.ProductId = " + productByEdition.ProductId.ToString());

            List<EditionProductShotInfo> BECollection = new List<EditionProductShotInfo>();

            CE_EditionsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_EditionProductShotsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_EditionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override EditionProductShotInfo getFromDataReader(System.Data.IDataReader current)
        {
            EditionProductShotInfo record = new EditionProductShotInfo();

            record.EditionProductShotId = Convert.ToInt32(current["EditionProductShotId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PSTypeId = Convert.ToByte(current["PSTypeId"]);
            record.ProductShot = current["ProductShot"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static CE_EditionProductShotsDALC Instance = new CE_EditionProductShotsDALC();
    }
}
