using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class DivisionInfoDALC : AgroDataAccessAdapter<DivisionAddressInfo>
    {
        # region Constructors

        private DivisionInfoDALC() { }

        #endregion

        public override DivisionAddressInfo getOne(int divisionId)
        {
            DbCommand dbCmd = DivisionInfoDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionById");

            // Add the parameters:


            DivisionInfoDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionInfoDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())

                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }


        public override void update(DivisionAddressInfo businessEntity)
        {
            DbCommand dbCmd = DivisionInfoDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionsInformation");

            DivisionInfoDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);

            DivisionInfoDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);

            DivisionInfoDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionName);

            DivisionInfoDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@shortName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ShortName);

            DivisionInfoDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }


        #region Protected Methods

        protected override DivisionAddressInfo getFromDataReader(IDataReader current)
        {
            DivisionAddressInfo record = new DivisionAddressInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();
            record.ShortName = current["ShortName"].ToString();

           
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }


        #endregion

        public static readonly DivisionInfoDALC Instance = new DivisionInfoDALC();




    }
}
