using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
namespace MedinetDataAccessComponent
{
    public class ProductTypeDALC : MedinetDataAccessAdapter<ProductTypeInfo>
    {
        public List<ProductTypeInfo> getProductTypes()
        {
            List<ProductTypeInfo> list = new List<ProductTypeInfo>();
            DbCommand dbCmd = ProductTypeDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductTypes");

            ProductTypeInfo record;

            using (IDataReader dataReader = ProductTypeDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    record = new ProductTypeInfo();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ProductTypeId = Convert.ToInt32(dataReader["ProductTypeId"]);
                    record.TypeName = dataReader["Typename"].ToString();

                    list.Add(record);
                }
                
            }
            return list;
        }
        public static readonly ProductTypeDALC Instance = new ProductTypeDALC();
    }
}
