using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class DivisionAddresDALC : AgroDataAccessAdapter<DivisionAddressInfo>
    {
        # region Constructors

        private DivisionAddresDALC() { }

        #endregion

        //public List< DivisionAddressInfo> getDivisionLab(int laboratoryId, int countryId)
        //{
        //    List<DivisionAddressInfo> BeCollection = new List<DivisionAddressInfo>();
        //                DbCommand dbCmd = LaboratoryDivisionDALC.AgroInstanceDatabase.GetStoredProcCommand("plm_spGetDivisionByLaboratory");

        //    // Add the parameters:

        //    LaboratoryDivisionDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
        //        ParameterDirection.Input, string.Empty, DataRowVersion.Current, laboratoryId);

        //    LaboratoryDivisionDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
        //        ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

        //    // Get the result set from the stored procedure:
        //    using (IDataReader dataReader = LaboratoryDivisionDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
        //    {
        //        // Iterates through records:
        //        while (dataReader.Read())
        //        {
        //            BeCollection.Add(this.getFromDataReader(dataReader));
        //        }
        //    }

        //    return BeCollection;
        //}

        public DivisionAddressInfo getInformationByDivId(int divisionId)
        {
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionAddresDALC.AgroInstanceDatabase.ExecuteReader(
                DivisionsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionByLaboratory", divisionId)))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<DivisionAddressInfo> getListInformationByDivId(int divisionId)
        {
            List<DivisionAddressInfo> divInfo = new List<DivisionAddressInfo>();
            // Get the resultset from the stored procedure:

            using (IDataReader dataReader = DivisionAddresDALC.AgroInstanceDatabase.ExecuteReader(
                DivisionAddresDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionByLaboratory", divisionId)))
            {
                while (dataReader.Read())
                    divInfo.Add(this.getFromDataReader(dataReader));

            }
            return divInfo;
        }

       


        public override void update(DivisionAddressInfo businessEntity)
        {
            DbCommand dbCmd = DivisionAddresDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformationAddress");

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionInformationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInformationId);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@address", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Address);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@telephone", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Telephone);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@fax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Fax);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@web", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Web);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@email", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@city", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.City);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@state", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.State);

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@lada", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Lada);

          

            DivisionAddresDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override int insert(DivisionAddressInfo businessEntity)
        {
            DbCommand dbCmd = DivisionAddresDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformationAddress");

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionInformationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInformationId);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@address", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Address);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@telephone", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Telephone);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@fax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Fax);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@web", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Web);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@email", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@city", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.City);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@state", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.State);
            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@lada", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Lada);

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);

            DivisionAddresDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.DivisionInformationId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.DivisionInformationId;
        }


        public override void delete(DivisionAddressInfo businessEntity)
        {
            DbCommand dbCmd = DivisionAddresDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformationAddress");

            // Add the parameters:

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);

            DivisionAddresDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionInformationId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInformationId);

            //Delete record:
            DivisionAddresDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }


        #region Protected Methods

        protected override DivisionAddressInfo getFromDataReader(IDataReader current)
        {
            DivisionAddressInfo record = new DivisionAddressInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();
            record.ShortName = current["ShortName"].ToString();

            record.DivisionInformationId = Convert.ToInt32(current["DivisionInformationId"]);
            record.Address = current["Address"].ToString();
            record.City = current["City"].ToString();
            record.Location = current["Location"].ToString();
            record.Suburb = current["Suburb"].ToString();
            record.State = current["State"].ToString();
            record.ZipCode = current["ZipCode"].ToString();
            record.Lada = current["Lada"].ToString();
            record.Telephone = current["Telephone"].ToString();
            record.Fax = current["Fax"].ToString();
            record.Email = current["Email"].ToString();
            record.Web = current["Web"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }


        #endregion

        public static readonly DivisionAddresDALC Instance = new DivisionAddresDALC();
    }
}
