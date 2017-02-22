using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class FamilyPrefixesDALC : MedinetDataAccessAdapter<FamilyPrefixInfo>
    {

        #region Constructors

        private FamilyPrefixesDALC() { }

        #endregion

        #region Public Methods

        public override FamilyPrefixInfo getOne(int pk)
        {
            DbCommand dbCmd = FamilyPrefixesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyPrefixes");

            // Add the parameters:
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamilyPrefixesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override void update(FamilyPrefixInfo businessEntity)
        {
            DbCommand dbCmd = FamilyPrefixesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyPrefixes");

            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixName);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@currentValue", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CurrentValue);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixDescription);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            FamilyPrefixesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public FamilyPrefixInfo getByEdition(int editionId, byte prefixTypeId)
        {
            DbCommand dbCmd = FamilyPrefixesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFamilyPrefixesByEdition");

            // Add the parameters:
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FamilyPrefixesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixTypeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamilyPrefixesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override FamilyPrefixInfo getFromDataReader(IDataReader current)
        {
            FamilyPrefixInfo record = new FamilyPrefixInfo();

            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PrefixName = current["PrefixName"].ToString();
            record.CurrentValue = Convert.ToInt32(current["CurrentValue"]);
            record.PrefixDescription = current["PrefixDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly FamilyPrefixesDALC Instance = new FamilyPrefixesDALC();

    }
}
