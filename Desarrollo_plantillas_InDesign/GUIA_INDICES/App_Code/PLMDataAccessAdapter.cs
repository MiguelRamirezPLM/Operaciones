//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


/// <summary>
/// Descripción breve de PLMDataAccessAdapter
/// </summary>
public class PLMDataAccessAdapter<T> : PLMDataAccess<T>
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

    protected static readonly Database GuiaInstanceDatabase =
        DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.GuiaSiteConnectionString");
    protected static readonly Database GuiaProdInstanceDatabase =
        DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.GuiaPSiteConnectionString");
    protected static readonly Database GuiaAliInstanceDatabase =
        DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.OldConnectionString");
}
