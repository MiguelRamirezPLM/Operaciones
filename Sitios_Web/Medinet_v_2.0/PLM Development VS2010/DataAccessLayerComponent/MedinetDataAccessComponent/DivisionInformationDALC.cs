using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class DivisionInformationDALC: MedinetDataAccessAdapter<DivisionInformationInfo>
    {

        #region Constructors

        private DivisionInformationDALC() { }

        #endregion

        #region Public methods

        public DivisionInformationInfo getInformationByDivId(int divisionId)
        {
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(
                DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionInformation", divisionId)))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }
        public List<DivisionInformationInfo> getListInformationByDivId(int divisionId)
        {
            List<DivisionInformationInfo> divInfo = new List<DivisionInformationInfo>();
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(
                DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionInformation", divisionId)))
            {
                while (dataReader.Read())
                    divInfo.Add(this.getFromDataReader(dataReader));
                
            }
            return divInfo;
        }

        public DivisionInformationInfo getAddressWithLogo(int divisionId)
        {
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(
                DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAddressWithLogo", divisionId)))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override void update(DivisionInformationInfo businessEntity)
        {
            DbCommand dbCmd = DivisionInformationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformation");

            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionInfId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInfId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@address", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Address);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@image", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Image);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@telephone", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Telephone);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@fax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Fax);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@web", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Web);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@email", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@city", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.City);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@state", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.State);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@contact", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Contact);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            DivisionInformationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override int insert(DivisionInformationInfo businessEntity)
        {
            DbCommand dbCmd = DivisionInformationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformation");

            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionInfId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInfId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@address", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Address);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@image", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Image);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@telephone", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Telephone);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@fax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Fax);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@web", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Web);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@email", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@city", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.City);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@state", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.State);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@contact", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Contact);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);

            DivisionInformationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.DivisionInfId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.DivisionInfId;  
        }

        public override void delete(DivisionInformationInfo businessEntity)
        {
            DbCommand dbCmd = DivisionInformationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionInformation");

            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionInfId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionInfId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@address", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Address);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@image", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Image);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@telephone", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Telephone);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@fax", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Fax);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@web", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Web);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@email", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@city", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.City);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@state", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.State);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@contact", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Contact);
            DivisionInformationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            DivisionInformationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected methods

        protected override DivisionInformationInfo getFromDataReader(System.Data.IDataReader current)
        {
            DivisionInformationInfo record = new DivisionInformationInfo();

            record.DivisionInfId = Convert.ToInt32(current["DivisionInfId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.Address = current["Address"].ToString();

            if (current["Suburb"] != System.DBNull.Value)
                record.Suburb = current["Suburb"].ToString();

            if (current["ZipCode"] != System.DBNull.Value)
                record.ZipCode = current["ZipCode"].ToString();

            if (current["Telephone"] != System.DBNull.Value)
                record.Telephone = current["Telephone"].ToString();

            if (current["Fax"] != System.DBNull.Value)
                record.Fax = current["Fax"].ToString();

            if (current["Web"] != System.DBNull.Value)
                record.Web = current["Web"].ToString();

            if (current["City"] != System.DBNull.Value)
                record.City = current["City"].ToString();

            if (current["State"] != System.DBNull.Value)
                record.State = current["State"].ToString();

            if (current["Email"] != System.DBNull.Value)
                record.Email = current["Email"].ToString();

            if (current["Contact"] != System.DBNull.Value)
                record.Contact = current["Contact"].ToString();

            if (current["Image"] != System.DBNull.Value)
                record.Image = current["Image"].ToString();

            //if (current["BaseUrl"] != System.DBNull.Value)
            //    record.BaseUrl = current["BaseUrl"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }  

        #endregion

        public static readonly DivisionInformationDALC Instance = new DivisionInformationDALC();
    }
}
