using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class MenuesDALC : PLMClientsDataAccessAdapter<MenuInfo>
    {

        #region Constructors

        private MenuesDALC() { }

        #endregion

        #region Public Methods

        public List<MobileMenuesInfo> getByTargetByEdition(string isbn, byte targetId)
        {
            DbCommand dbCmd = MenuesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetMenues");

            List<MobileMenuesInfo> BECollection = new List<MobileMenuesInfo>();

            // Add the parameters:
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)targetId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = MenuesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    MobileMenuesInfo record = new MobileMenuesInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.OSMobileId = Convert.ToByte(dataReader["TargetId"]);
                    record.MenuId = Convert.ToByte(dataReader["MenuId"]);
                    record.MenuName = dataReader["MenuName"].ToString();
                    record.ShortName = dataReader["ShortName"].ToString();
                    record.ImageName = dataReader["ImageName"].ToString();
                    record.BaseUrl = dataReader["BaseUrl"].ToString();
                    record.Order = Convert.ToByte(dataReader["Order"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        public static readonly MenuesDALC Instance = new MenuesDALC();

    }
}
