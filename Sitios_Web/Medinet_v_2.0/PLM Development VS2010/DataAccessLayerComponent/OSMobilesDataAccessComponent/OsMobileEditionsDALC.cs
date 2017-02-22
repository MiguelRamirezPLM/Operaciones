using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace OSMobilesDataAccessComponent
{
    public class OsMobileEditionsDALC : OSMobilesDataAccessAdapter<EditionInfo>
    {
        #region Constructors

        private OsMobileEditionsDALC() { }

        #endregion

        #region Public Methods

        public MobileEditionInfo getByDeviceByCountry(byte osMobileId, string country)
        {
            DbCommand dbCmd = OsMobileEditionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditionsByDevice");

            // Add the parameters:
            OsMobileEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, osMobileId);
            OsMobileEditionsDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = OsMobileEditionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    MobileEditionInfo record = new MobileEditionInfo();

                    record.OsMobileId = Convert.ToByte(dataReader["OsMobileId"]);
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

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            record.ISBN = current["ISBN"].ToString();

            if (current["BarCode"] != System.DBNull.Value)
                record.BarCode = current["BarCode"].ToString();

            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.BookId = Convert.ToInt32(current["BookId"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return base.getFromDataReader(current);
        }

        #endregion

        public static readonly OsMobileEditionsDALC Instance = new OsMobileEditionsDALC();
    }
}
