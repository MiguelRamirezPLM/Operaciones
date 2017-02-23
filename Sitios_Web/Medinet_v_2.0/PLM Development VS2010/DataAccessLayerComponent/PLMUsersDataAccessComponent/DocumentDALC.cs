using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
   public class DocumentDALC : PLMUsersDataAccesAdapter<DocumentInfo>
   {
       #region Constructor

       private DocumentDALC() { }

       #endregion 

       //Get all documentfile by user

       public List<DocumentInfo> getDocumentFile(int userId)
       {

           DbCommand dbCmd = DocumentDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spSearchDocument");

           //Add parameters
           DocumentDALC.InstanceDatabase.AddParameter(dbCmd, "userId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);


           List<DocumentInfo> recordCollection = new List<DocumentInfo>();


           using (IDataReader dataReader = DocumentDALC.InstanceDatabase.ExecuteReader(dbCmd))
           {
               DocumentInfo record;

               while (dataReader.Read())
               {
                   record = new DocumentInfo();

                   record.ApplicationId = Convert.ToInt32(dataReader["ApplicationId"]);
                   record.Description = dataReader["Description"].ToString();
                   record.DocumentFile = dataReader["DocumentFile"].ToString();

                   recordCollection.Add(record);


               }
                  
           }

           return recordCollection;

       }

           #region Protected

       protected override DocumentInfo getFromDataReader(IDataReader current)
       {

           DocumentInfo record =   new DocumentInfo();

           record.ApplicationId = Convert.ToInt32(current["ApplicationId"]);
           record.Description = current["Description"].ToString();
           record.DocumentFile = current["DocumentFile"].ToString();

           return record;

       }

        
           #endregion 

       public static readonly DocumentDALC Instance = new DocumentDALC();

      
      
   }
}
