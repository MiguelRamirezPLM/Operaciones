﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public abstract class CE_PharmaSearchEngineDataAccess<T>
    {
        public abstract int insert(T businessEntity);
        public abstract void delete(int pk);
        public abstract void delete(T businessEntity);
        public abstract void update(T businessEntity);
        public abstract T getOne(int pk);
        public abstract List<T> getAll();

        protected abstract T getFromDataReader(IDataReader current);
        protected abstract void getParameters(DbCommand dbCmd, T businessEntity);
    }
}