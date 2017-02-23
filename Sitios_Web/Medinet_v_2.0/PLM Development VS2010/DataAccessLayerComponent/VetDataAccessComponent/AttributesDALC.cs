
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetDataAccessComponent
{
    public sealed class AttributesDALC : VetDataAccessAdapter<AttributeDetailInfo>
    {
        #region Public Methods

        public List<AttributeDetailInfo> getCompleteAttributes(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
           
            List<AttributeDetailInfo> BeCollection = new List<AttributeDetailInfo>();
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETCompleteAttributes");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);



            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {


                // Iterates through records:
                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));

                }

                return BeCollection;
            }

        }





        #endregion

        #region Protected Methods

        protected override AttributeDetailInfo getFromDataReader(IDataReader current)
        {
            AttributeDetailInfo record = new AttributeDetailInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.AttributeId = Convert.ToInt32(current["AttributeGroupId"]);
            record.AttributeName = current["AttributeGroupName"].ToString();
            record.AttributeContent = current["AttributePlainContent"].ToString();
            record.HTMLContent = current["AttributeHTMLContent"].ToString();


            return record;

        }


        #endregion

        public static readonly AttributesDALC Instance = new AttributesDALC();

    }
}
