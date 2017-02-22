using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class insertclientcategories
    {
        Guia db = new Guia();
        ClientCategories ClientCategories = new ClientCategories();
        EditionClientCategories EditionClientCategories = new EditionClientCategories();
        ActivityLog ActivityLog = new ActivityLog();

        public bool asociatecategorybyclient(int Id, int _clientid, int editionid, int ApplicationId, int UsersId)
        {

            var cats = (from cat in db.Categories
                        where cat.ParentId == Id
                        select cat).ToList();
            if (cats.LongCount() > 0)
            {
                return false;
            }
            else
            {
                var cc = (from clientc in db.ClientCategories
                          where clientc.CategoryId == Id
                          && clientc.ClientId == _clientid
                          select clientc).ToList();
                if (cc.LongCount() == 0)
                {
                    ClientCategories.ClientId = _clientid;
                    ClientCategories.CategoryId = Id;

                    db.ClientCategories.Add(ClientCategories);
                    db.SaveChanges();
                }

                var ccat = (from ccc in db.EditionClientCategories
                            where ccc.EditionId == editionid
                            && ccc.ClientId == _clientid
                            && ccc.CategoryId == Id
                            select ccc).ToList();
                if (ccat.LongCount() == 0)
                {
                    EditionClientCategories.CategoryId = Id;
                    EditionClientCategories.ClientId = _clientid;
                    EditionClientCategories.EditionId = editionid;

                    db.EditionClientCategories.Add(EditionClientCategories);
                    db.SaveChanges();

                    ActivityLog._inserteditionclientcategories(_clientid, Id, editionid, ApplicationId, UsersId);
                }
                if ((cc.LongCount() > 0) && (ccat.LongCount() > 0))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}