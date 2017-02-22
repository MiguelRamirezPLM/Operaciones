using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_PresentationsDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.PresentationsInfo>
    {
        #region Constructors

        private CE_PresentationsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PresentationsInfo> getByProduct(int editionId, int productId, int pharmaFormId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct p.PresentationId,pp.PackPpfId,p.ContentUnitId,p.QtyContentUnit,cu.UnitName as ContentName,p.WeightUnitId,p.QtyWeightUnit,");
            sb.Append("\n wu.UnitName,wu.ShortName,p.TargetUseId,tu.UseName,v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,p.Active  ");
            sb.Append("\n From ProductsByEdition v");
            sb.Append("\n Inner Join PackPPF pp On(v.ProductId = pp.ProductId and v.PharmaFormId = pp.PharmaFormId) ");
            sb.Append("\n Inner Join Presentations p On(pp.PackPpfId = p.PackPpfId) ");
            sb.Append("\n Inner Join ContentUnits cu On(p.ContentUnitId = cu.ContentUnitId) ");
            sb.Append("\n Left Join WeightUnits wu On(p.WeightUnitId = wu.WeightUnitId) ");
            sb.Append("\n Left Join TargetUses tu On(p.TargetUseId = tu.TargetUseId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.EditionId = " + editionId + " and ");
            sb.Append("\n v.ProductId = " + productId + " and v.PharmaFormId = " + pharmaFormId);
            sb.Append("\n Order By v.PharmaForm");

            List<MedinetBusinessEntries.PresentationsInfo> BECollection = new List<MedinetBusinessEntries.PresentationsInfo>();

            CE_PresentationsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_IndicationsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                MedinetBusinessEntries.PresentationsInfo record;

                while (dataReader.Read())
                {
                    record = this.getFromDataReader(dataReader);

                    record.Packs = CE_PacksDALC.Instance.getPacks(record.PackPpfId);

                    BECollection.Add(record);
                }
            }

            CE_PresentationsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;

        }

        #endregion
        
        #region Protected Methods

        protected override MedinetBusinessEntries.PresentationsInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.PresentationsInfo record = new MedinetBusinessEntries.PresentationsInfo();

            record.PresentationId = Convert.ToInt32(current["PresentationId"]);
            record.PackPpfId = Convert.ToInt32(current["PackPpfId"]);
            record.QtyContentUnit = current["QtyContentUnit"].ToString();
            record.ContentUnitId = Convert.ToInt32(current["ContentUnitId"]);
            record.ContentName = current["ContentName"].ToString();

            if (current["QtyWeightUnit"] != System.DBNull.Value)
                record.QtyWeightUnit = current["QtyWeightUnit"].ToString();

            if (current["WeightUnitId"] != System.DBNull.Value)
                record.WeightUnitId = Convert.ToInt32(current["WeightUnitId"]);

            if (current["UnitName"] != System.DBNull.Value)
                record.UnitName = current["UnitName"].ToString();

            if (current["ShortName"] != System.DBNull.Value)
                record.ShortName = current["ShortName"].ToString();

            if (current["TargetUseId"] != System.DBNull.Value)
                record.TargetUseId = Convert.ToInt32(current["TargetUseId"]);

            if (current["UseName"] != System.DBNull.Value)
                record.UseName = current["UseName"].ToString();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["pharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
                        
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_PresentationsDALC Instance = new CE_PresentationsDALC();
    }
}
