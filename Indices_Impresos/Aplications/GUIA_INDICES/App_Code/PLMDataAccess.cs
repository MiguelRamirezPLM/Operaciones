using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;



public abstract class PLMDataAccess<T>
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
