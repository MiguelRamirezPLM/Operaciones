using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class AttributeGroupDALC : MedinetDataAccessAdapter<AttributeGroupInfo>
    {

        #region Constructors

        private AttributeGroupDALC() { }

        #endregion

        #region Public Methods

        public override AttributeGroupInfo getOne(int pk)
        {
            DbCommand dbCmd = AttributeGroupDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAttributeGroup");

            // Add the parameters:
            AttributeGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            AttributeGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AttributeGroupDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            } 
        }

        public override List<AttributeGroupInfo> getAll()
        {
            List<AttributeGroupInfo> BECollection = new List<AttributeGroupInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributeGroupDALC.MedInstanceDatabase.ExecuteReader(
                AttributeGroupDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAttributeGroups")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override AttributeGroupInfo getFromDataReader(IDataReader current)
        {
            AttributeGroupInfo record = new AttributeGroupInfo();

            record.AttributeGroupId = Convert.ToInt32(current["AttributeGroupId"]);
            record.AttributeGroupName = current["AttributeGroupName"].ToString();
            record.AttributeGroupOrder = Convert.ToInt32(current["AttributeGroupOrder"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly AttributeGroupDALC Instance = new AttributeGroupDALC();

    }
}
