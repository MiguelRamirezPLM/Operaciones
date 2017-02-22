using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class TherapeuticDescriptionsDALC : MedinetDataAccessAdapter<TherapeuticDescriptionInfo>
    {
        #region Constructors

        private TherapeuticDescriptionsDALC() { }

        #endregion

        #region Public Methods

        public override TherapeuticDescriptionInfo getOne(int pk)
        {
            DbCommand dbCmd = TherapeuticDescriptionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTherapeuticDescriptions");

            TherapeuticDescriptionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, TherapeuticsDALC.CRUD.Read);
            TherapeuticDescriptionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = TherapeuticDescriptionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion


        #region Protected Methods

        protected override TherapeuticDescriptionInfo getFromDataReader(IDataReader current)
        {
            TherapeuticDescriptionInfo record = new TherapeuticDescriptionInfo();

            record.TherapeuticDescriptionId = Convert.ToInt32(current["TherapeuticDescriptionId"]);
            record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);

            if (current["Explanation"] != DBNull.Value)
                record.Explanation = current["Explanation"].ToString();
            
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion 


        public static readonly TherapeuticDescriptionsDALC Instance = new TherapeuticDescriptionsDALC();

    }
}
