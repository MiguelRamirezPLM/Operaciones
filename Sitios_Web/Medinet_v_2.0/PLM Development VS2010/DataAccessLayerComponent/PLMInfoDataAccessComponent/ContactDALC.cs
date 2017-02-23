using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMInfoDataAccessComponent
{
    public sealed class ContactDALC : PLMInfoDataAccessAdapter<ContactInfo>
    {
        #region Constructors

        private ContactDALC() { }

        #endregion

        #region Public Methods

        public ContactInfo getContactInfoByCountry(string id)
        {
            DbCommand dbCmd = ContactDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetContactInfo");

            //// Add the parameters:
            ContactDALC.InstanceDatabase.AddParameter(dbCmd, "@id", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, id);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ContactDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ContactInfo getFromDataReader(IDataReader current)
        {
            ContactInfo record = new ContactInfo();

            record.BranchId = Convert.ToByte(current["BranchId"]);
            record.CountryName = current["CountryName"].ToString();
            record.CompanyName = current["CompanyName"].ToString();
            record.Street = current["Street"].ToString();
            record.Suburb = current["Suburb"].ToString();
            record.ZipCode = current["ZipCode"].ToString();
            record.Lada = current["Lada"].ToString();
            record.PhoneOne = current["PhoneOne"].ToString();
            record.ContactEmail = current["ContactEmail"].ToString();
                       
            return record;
        }

        #endregion

        public static readonly ContactDALC Instance = new ContactDALC();
    }
}
