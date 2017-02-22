using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class MessagesDALC : PLMClientsDataAccessAdapter<MessageInfo>
    {
        #region Constructors

        private MessagesDALC() { }

        #endregion

        #region Public Methods

        public override MessageInfo getOne(int pk)
        {
            DbCommand dbCmd = MessagesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDMessages");

            // Add the parameters:
            MessagesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            MessagesDALC.InstanceDatabase.AddParameter(dbCmd, "@messageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MessagesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }


        }

        public MessageInfo getByCode(int messageCode)
        {
            DbCommand dbCmd = MessagesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMessages");

            // Add the parameters:
            MessagesDALC.InstanceDatabase.AddParameter(dbCmd, "@messageCode", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, messageCode);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MessagesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }


        }

        #endregion

        #region Protected Methods

        protected override MessageInfo getFromDataReader(IDataReader current)
        {
            MessageInfo record = new MessageInfo();

            record.MessageId = Convert.ToInt32(current["MessageId"]);
            record.MessageText = current["MessageText"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly MessagesDALC Instance = new MessagesDALC();

    }
}
