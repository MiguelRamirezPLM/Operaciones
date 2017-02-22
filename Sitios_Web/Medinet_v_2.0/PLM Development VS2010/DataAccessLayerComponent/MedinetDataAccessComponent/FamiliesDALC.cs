using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class FamiliesDALC : MedinetDataAccessAdapter<FamilyInfo>
    {

        #region Constructors

        private FamiliesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(FamilyInfo businessEntity)
        {
            DbCommand dbCmd = FamiliesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilies");

            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FamilyString);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            FamiliesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override FamilyInfo getOne(int pk)
        {
            DbCommand dbCmd = FamiliesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilies");

            // Add the parameters:
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamiliesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<FamilyInfo> getContentFamiliesByDivision(int editionId, int divisionId)
        {
            List<FamilyInfo> BECollection = new List<FamilyInfo>();

            DbCommand dbCmd = FamiliesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetContentFamiliesByDivision");

            // Add the parameters:
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamiliesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<FamilyInfo> getPSFamiliesByDivision(int editionId, int divisionId)
        {
            List<FamilyInfo> BECollection = new List<FamilyInfo>();

            DbCommand dbCmd = FamiliesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSFamiliesByDivision");

            // Add the parameters:
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FamiliesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamiliesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override FamilyInfo getFromDataReader(IDataReader current)
        {
            FamilyInfo record = new FamilyInfo();

            record.FamilyId = Convert.ToInt32(current["FamilyId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.FamilyString = current["FamilyString"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly FamiliesDALC Instance = new FamiliesDALC();

    }
}
