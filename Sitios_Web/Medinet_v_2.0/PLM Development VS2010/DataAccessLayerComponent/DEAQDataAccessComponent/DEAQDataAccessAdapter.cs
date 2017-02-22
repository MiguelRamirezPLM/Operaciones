using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DEAQDataAccessComponent
{
    public class DEAQDataAccessAdapter<T> : DEAQDataAccess<T>
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

        protected enum CRUD : byte
        {
            Create = 0,
            Read = 1,
            Update = 2,
            Delete = 3
        }

        public enum TypeInEdition : byte
        {
            Participante = 1,
            Mencionado = 2
        }

        protected static readonly Database DEAQInstanceDatabase =
            DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.DEAQConnectionString");


    }
}
