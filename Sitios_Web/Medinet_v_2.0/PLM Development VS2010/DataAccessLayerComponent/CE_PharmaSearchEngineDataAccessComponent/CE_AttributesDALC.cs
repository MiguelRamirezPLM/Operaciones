using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_AttributesDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.AttributesInfo>
    {
        #region Constructors

        private CE_AttributesDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.AttributesInfo> getAll(int editionId)
        {
            CE_AttributesDALC.PharmaInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect Distinct a.AttributeId, a.Description, a.TechnicalSheet, a.Active, ea.AttributeOrder ");
            sb.Append("\nFrom EditionAttributes ea ");
            sb.Append("\nInner Join Attributes a ON (ea.AttributeId = a.AttributeId)  ");
            sb.Append("\nWhere ea.AttributeSearch = 1 and a.Active = 1 and ea.EditionId = " + editionId);
            sb.Append("\nOrder By ea.AttributeOrder");

            List<MedinetBusinessEntries.AttributesInfo> BECollection = new List<MedinetBusinessEntries.AttributesInfo>();

            using (IDataReader dataReader = CE_AttributesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_AttributesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            CE_AttributesDALC.PharmaInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect pc.EditionId, pc.DivisionId, pc.CategoryId, pc.ProductId, pc.PharmaFormId, a.AttributeId, a.Description as AttributeName");
            sb.Append("\nFrom EditionAttributes ea ");
            sb.Append("\nInner Join ProductContents pc On(ea.AttributeId = pc.AttributeId And ea.EditionId = pc.EditionId)  ");
            sb.Append("\nInner Join Attributes a On(ea.AttributeId = a.AttributeId)    ");
            sb.Append("\nWhere a.Active	= 1 and ea.EditionId = " + editionId + " and pc.DivisionId = " + divisionId + " and ");
            sb.Append("\npc.CategoryId = " + categoryId + " and pc.ProductId = " + productId + " and pc.PharmaFormId = " + pharmaFormId);
            sb.Append("\nOrder By ea.AttributeOrder, a.Description");

            List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo>();

            // Get the resultset:
            using (IDataReader dataReader = CE_AttributesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                PharmaSearchEngineBusinessEntries.AttributeByProductInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeByProductInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);
                    record.AttributeName = dataReader["AttributeName"].ToString();

                    BECollection.Add(record);

                }
            }

            CE_AttributesDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public MedinetBusinessEntries.ProductContentsInfo getAttribute(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, int attributeId)
        {
            CE_AttributesDALC.PharmaInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect ProductContentId,ProductId,PharmaFormId,AttributeId,EditionId,DivisionId,CategoryId,Content,PlainContent ");
            sb.Append("\nFrom ProductContents ");
            sb.Append("\nWhere EditionId = " + editionId + " and DivisionId = " + divisionId + " and CategoryId = " + categoryId + " and ");
            sb.Append("\nProductId = " + productId + " and PharmaFormId = " + pharmaFormId + " and AttributeId = " + attributeId);

            using (IDataReader dataReader = CE_AttributesDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    MedinetBusinessEntries.ProductContentsInfo record = new MedinetBusinessEntries.ProductContentsInfo();

                    record.ProductContentId = Convert.ToInt32(dataReader["ProductContentId"]);
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);

                    if (dataReader["Content"] != System.DBNull.Value)
                        record.Content = dataReader["Content"].ToString();

                    if (dataReader["PlainContent"] != System.DBNull.Value)
                        record.PlainContent = dataReader["PlainContent"].ToString();

                    CE_AttributesDALC.PharmaInstanceDatabase.CloseSharedConnection();

                    return record;
                }
                else
                {
                    CE_AttributesDALC.PharmaInstanceDatabase.CloseSharedConnection();
                    return null;
                }
            }

        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.AttributesInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.AttributesInfo record = new MedinetBusinessEntries.AttributesInfo();

            record.AttributeId = Convert.ToInt32(current["AttributeId"]);
            record.Description = current["Description"].ToString();
            record.TechnicalSheet = Convert.ToBoolean(current["TechnicalSheet"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly CE_AttributesDALC Instance = new CE_AttributesDALC();

    }
}
