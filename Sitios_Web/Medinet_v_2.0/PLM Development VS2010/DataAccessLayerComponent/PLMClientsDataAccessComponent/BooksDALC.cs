using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
   public sealed class BooksDALC : PLMClientsDataAccessAdapter<BookInfo>
   {
       #region Constructor

       private BooksDALC() { }

       #endregion 

       public BookInfo getBookByCode(string code)
       {
           DbCommand dbCmd = BooksDALC.InstanceDatabase.GetStoredProcCommand("plm_spGetBookByCode");

           // Add the parameters:
           BooksDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = BooksDALC.InstanceDatabase.ExecuteReader(dbCmd))
          
           {
               if (dataReader.Read())
                   return this.getFromDataReader(dataReader);
               else
                   return null;
           }
       }

       #region Protected Methods

       protected BookInfo getFromDataReader(IDataReader current)
       {
           BookInfo record = new BookInfo();
           
           record.BookId = Convert.ToInt32(current["BookId"]);
           record.BookName = current["BookName"].ToString();
           record.ShortName = current["ShortName"].ToString();
           record.Active = Convert.ToBoolean(current["Active"]);

           return record;

       }

       #endregion

       public static readonly BooksDALC Instance = new BooksDALC();

   }
}
