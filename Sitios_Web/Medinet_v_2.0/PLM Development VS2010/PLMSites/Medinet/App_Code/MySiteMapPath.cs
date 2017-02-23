using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public class MySiteMapPath : SiteMapPath
    {
        private static readonly object _eventItemCreating = new object();

        public event SiteMapNodeItemEventHandler ItemCreating
        {
            add { Events.AddHandler(_eventItemCreating, value); }
            remove { Events.RemoveHandler(_eventItemCreating, value); }
        }

        protected virtual void OnItemCreating(SiteMapNodeItemEventArgs e)
        {
            SiteMapNodeItemEventHandler handler = (SiteMapNodeItemEventHandler)base.Events[_eventItemCreating];
            if (handler != null)
                handler(this, e);
        }

        protected override void InitializeItem(SiteMapNodeItem item)
        {
            OnItemCreating(new SiteMapNodeItemEventArgs(item));
            base.InitializeItem(item);
        }
    }
}