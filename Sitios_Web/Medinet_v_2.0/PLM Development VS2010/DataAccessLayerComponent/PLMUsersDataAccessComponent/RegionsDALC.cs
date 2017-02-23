using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class RegionsDALC : PLMUsersDataAccesAdapter<RegionInfo>
    {
        #region Constructors

        private RegionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(RegionInfo businessEntity)
        {
            DbCommand dbCom = RegionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRegions");

            //Add the parameters:
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Create);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            //Insert Record:
            RegionsDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

            businessEntity.RegionId = Convert.ToInt32(dbCom.Parameters["@Return"].Value);

            return businessEntity.RegionId;
 
        }

        public override void delete(int pk)
        {
            DbCommand dbCom = RegionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRegions");

            //Add the parameters:
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RegionsDALC.CRUD.Delete);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@regionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            RegionsDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override void update(RegionInfo businessEntity)
        {
            DbCommand dbCom = RegionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRegions");

            //Add the parameters:
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RegionsDALC.CRUD.Update);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@regionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RegionId);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            RegionsDALC.InstanceDatabase.ExecuteNonQuery(dbCom);
        }

        public override RegionInfo getOne(int pk)
        {
            DbCommand dbCom = RegionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRegions");

            //Add the parameters:
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RegionsDALC.CRUD.Read);
            RegionsDALC.InstanceDatabase.AddParameter(dbCom, "@regionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = RegionsDALC.InstanceDatabase.ExecuteReader(dbCom))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected

        protected override RegionInfo getFromDataReader(IDataReader current)
        {
            RegionInfo record = new RegionInfo();

            record.RegionId = Convert.ToInt32(current["RegionId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly RegionsDALC Instance = new RegionsDALC();
    }

}