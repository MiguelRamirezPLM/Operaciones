using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_UpdatesDALC : CE_PharmaSearchEngineDataAccessAdapter<object>, CE_SyncDatabasesDataAccessComponent.ISyncDatabases
    {
        #region Constructors

        public CE_UpdatesDALC() { }

        #endregion

        #region Public Methods

        public UpdateMessageInfo executeQuery(string sqlText)
        {
            CE_UpdatesDALC.PharmaInstanceDatabase.CreateConnection();

            UpdateMessageInfo message = new UpdateMessageInfo();

            try
            {
                CE_UpdatesDALC.PharmaInstanceDatabase.ExecuteNonQuery(CommandType.Text, sqlText);

                message.UpdateStatus = true;
                message.ExecuteDate = System.DateTime.Now;

                return message;
            
            }catch(Exception ex)
            {
                message.UpdateStatus = false;
                message.UpdateMessage = ex.Message;
                message.ExecuteDate = System.DateTime.Now;

                return message;
            }
            finally{
                CE_UpdatesDALC.PharmaInstanceDatabase.CloseSharedConnection();
            }
                     
            
        }

        #endregion

    }
}
