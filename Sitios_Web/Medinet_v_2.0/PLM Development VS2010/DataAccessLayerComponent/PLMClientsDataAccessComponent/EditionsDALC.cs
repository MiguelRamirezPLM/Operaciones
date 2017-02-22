using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class EditionsDALC : PLMClientsDataAccessAdapter<EditionInfo>
    {
        #region Constructors

        private EditionsDALC() { }

        #endregion

        #region Public Methodsd

        public EditionInfo getByUserId(int userId)
        {
            DbCommand dbCmd = EditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditions");

            // Add the parameters:
            EditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }    
        }

        public EditionInfo getByCode(string code)
        {
            DbCommand dbCmd = EditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditions");

            // Add the parameters:
            EditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }    
        }

        public MobileEditionInfo getByTargetByCountry(byte targetId, string country)
        {
            DbCommand dbCmd = EditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditionsByTarget");

            // Add the parameters:
            EditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            EditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    MobileEditionInfo record = new MobileEditionInfo();

                    record.OsMobileId = Convert.ToByte(dataReader["TargetId"]);
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.ISBN = dataReader["ISBN"].ToString();
                    record.FileName = dataReader["FileName"].ToString();
                    record.BaseUrl = dataReader["BaseUrl"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override EditionInfo getFromDataReader(IDataReader current)
        {
            EditionInfo record = new EditionInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.CountryId = Convert.ToByte(current["CountryId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.BookId = Convert.ToInt32(current["BookId"]);
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);

            if (current["ISBN"] != DBNull.Value)
                record.ISBN = current["ISBN"].ToString();

            if (current["BarCode"] != DBNull.Value)
                record.BarCode = current["BarCode"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
            
        }

        #endregion

        public static readonly EditionsDALC Instance = new EditionsDALC();
    }
}
