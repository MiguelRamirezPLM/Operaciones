using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_PacksDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.PackInfo>
    {
        #region Constructors

        private CE_PacksDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PackInfo> getByProductByPF(int editionId, int productId, int pharmaFormId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct pp.PackPpfId,pp.ParentId,pp.PackId,p.PackName,pp.PackTypeId,");
            sb.Append("\n pt.TypeName,pp.QtyItems,v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,pp.Active ");
            sb.Append("\n From ProductsByEdition v");
            sb.Append("\n Inner Join PackPPF pp On(v.ProductId = pp.ProductId and v.PharmaFormId = pp.PharmaFormId) ");
            sb.Append("\n Inner Join Packs p On(pp.PackId = p.PackId) ");
            sb.Append("\n Inner Join PackTypes pt On(pp.PackTypeId = pt.PackTypeId) ");
            sb.Append("\n Where v.ProductActive = 1 and v.EditionId = " + editionId + " and ");
            sb.Append("\n pp.ProductId = " + productId + " and pp.PharmaFormId = " + pharmaFormId);
            sb.Append("\n Order By PackPpfId");

            List<MedinetBusinessEntries.PackInfo> BECollection = new List<MedinetBusinessEntries.PackInfo>();

            CE_PacksDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PacksDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_PacksDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public MedinetBusinessEntries.OuterPackInfo getPacks(int packPpfId)
        {
            MedinetBusinessEntries.OuterPackInfo record = new MedinetBusinessEntries.OuterPackInfo();

            record.InnerPack = this.getById(packPpfId);

            if (record.InnerPack.ParentId != null)
            {
                MedinetBusinessEntries.PackInfo outer = new MedinetBusinessEntries.PackInfo();

                outer = this.getById((int)record.InnerPack.ParentId);

                record.PackPpfId = outer.PackPpfId;

                if (outer.ParentId != null)
                    record.ParentId = outer.ParentId;

                record.PackId = outer.PackId;
                record.PackName = outer.PackName;
                record.PackTypeId = outer.PackTypeId;
                record.TypeName = outer.TypeName;
                record.QtyItems = outer.QtyItems;
                record.ProductId = outer.ProductId;
                record.Brand = outer.Brand;
                record.PharmaFormId = outer.PharmaFormId;
                record.PharmaForm = outer.PharmaForm;
                record.Active = outer.Active;

            }
            else
            {
                record.PackPpfId = record.InnerPack.PackPpfId;
                record.PackId = record.InnerPack.PackId;
                record.PackName = record.InnerPack.PackName;
                record.PackTypeId = record.InnerPack.PackTypeId;
                record.TypeName = record.InnerPack.TypeName;
                record.QtyItems = record.InnerPack.QtyItems;
                record.ProductId = record.InnerPack.ProductId;
                record.Brand = record.InnerPack.Brand;
                record.PharmaFormId = record.InnerPack.PharmaFormId;
                record.PharmaForm = record.InnerPack.PharmaForm;
                record.Active = record.InnerPack.Active;

                record.InnerPack = null;

            }

            return record;
        }

        public MedinetBusinessEntries.PackInfo getById(int packPpfId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select Distinct pp.PackPpfId,pp.ParentId,pp.PackId,p.PackName,pp.PackTypeId,");
            sb.Append("\n pt.TypeName,pp.QtyItems,v.ProductId,v.Brand,v.PharmaFormId,v.PharmaForm,pp.Active ");
            sb.Append("\n From ProductsByEdition v");
            sb.Append("\n Inner Join PackPPF pp On(v.ProductId = pp.ProductId and v.PharmaFormId = pp.PharmaFormId) ");
            sb.Append("\n Inner Join Packs p On(pp.PackId = p.PackId) ");
            sb.Append("\n Inner Join PackTypes pt On(pp.PackTypeId = pt.PackTypeId) ");
            sb.Append("\n Where v.ProductActive = 1 and pp.PackPpfId = " + packPpfId);

            MedinetBusinessEntries.PackInfo record = null;

            CE_PacksDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_PacksDALC.PharmaInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_PacksDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.PackInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.PackInfo record = new MedinetBusinessEntries.PackInfo();

            record.PackPpfId = Convert.ToInt32(current["PackPpfId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.PackId = Convert.ToInt32(current["PackId"]);
            record.PackName = current["PackName"].ToString();
            record.PackTypeId = Convert.ToInt32(current["PackTypeId"]);
            record.TypeName = current["TypeName"].ToString();
            record.QtyItems = Convert.ToInt32(current["QtyItems"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_PacksDALC Instance = new CE_PacksDALC();

    }
}
