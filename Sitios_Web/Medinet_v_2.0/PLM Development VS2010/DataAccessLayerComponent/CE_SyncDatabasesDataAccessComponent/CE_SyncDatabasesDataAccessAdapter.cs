using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.SqlCe;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_SyncDatabasesDataAccessAdapter<T> : CE_SyncDatabasesDataAccess<T>
    {
        public override int insert(T businessEntity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void delete(int pk)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void delete(T businessEntity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void update(T businessEntity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override T getOne(int pk)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<T> getAll()
        {
            throw new Exception("The method or operation is not implemented.");
        }


        protected override T getFromDataReader(IDataReader current)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void getParameters(DbCommand dbCmd, T businessEntity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected static readonly SqlCeDatabase SyncInstanceDatabase =
            new SqlCeDatabase("Data Source=|DataDirectory|\\CE_SyncDatabases_def_siegfried.sdf;Password=sdcef08*;Max Database Size = 256;Persist Security Info=False;");

        //protected static readonly SqlCeDatabase SyncInstanceDatabase =
        //    new SqlCeDatabase("Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\CE_SyncDatabases.sdf;Password=sdcef08*;Max Database Size = 256;Persist Security Info=False;");

    }
}
