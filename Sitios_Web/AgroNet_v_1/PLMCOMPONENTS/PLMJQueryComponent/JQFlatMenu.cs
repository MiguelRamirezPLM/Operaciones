using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace PLMJQueryComponent
{
    
     public class JQFlatMenu: WebControl
    {
        #region Propierties

         [Browsable(true)]
         [Description("Assign the name to the menu")]
         public string MenuName
         {
             get { return this._menuName; }
             set { this._menuName = value; }
         } 
         
        [Browsable(true)]
        [Description("Assign the items to the menu")]
        public List<MenuItem> MenuItems
        {
            get { return this._menuItems; }
            set { this._menuItems = value; }
        }

        public int Duration
        {
            get { return this._duration; }
            set { this._duration = value; }
        }
        public bool Flyout
        {
            get { return this._flyout; }
            set { this._flyout = value; }
        }

        #endregion

        #region render Contents


        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder stringToRender = new StringBuilder();



                stringToRender.Append("<script type=\"text/javascript\">");
                stringToRender.Append("\n $(function(){");
                stringToRender.Append("\n $('.fg-button').hover(");
                stringToRender.Append("\n function(){ $(this).removeClass('ui-state-default').addClass('ui-state-focus'); },");
                stringToRender.Append("\n function(){ $(this).removeClass('ui-state-focus').addClass('ui-state-default'); }");
                stringToRender.Append("\n );");
                stringToRender.Append("\n $('#Flatmenu').menu({ ");
                stringToRender.Append("\n content: $('#Flatmenu').next().html(),");
                stringToRender.Append("\n flyOut: " + this._flyout.ToString().ToLower() +"");
                stringToRender.Append("\n });");
                stringToRender.Append("\n });"); 
                stringToRender.Append("\n  </script>");
                stringToRender.Append("\n  ");

                stringToRender.Append("\n  <a tabindex=\"0\" href='#TImenu' class='fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all' id='Flatmenu'><span class='ui-icon ui-icon-triangle-1-s'></span>" + this._menuName + "</a>");
                stringToRender.Append("\n  <div id='TImenu' class='hidden' align='center'>");
                stringToRender.Append("\n  <ul class='ulFlat'>");

                if (this._menuItems.Count > 0)
                {
                    for (int x = 0; x < this._menuItems.Count; x++)
                    {
                        stringToRender.Append("\n <li class='liFlat'><a href='" + this._menuItems[x].NavigateUrl + "'><img src='img/" + this._menuItems[x].ImageUrl + "' border='none' align='left' />" + this._menuItems[x].Text + "</a></li>");
                    }
                }

                stringToRender.Append("\n  </ul>");

                stringToRender.Append("\n </div>");

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(stringToRender.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Error Displaying JQFLatMenu");
                writer.RenderEndTag();
            }

        }

        #endregion

         private string _menuName;
        private List<MenuItem> _menuItems;
        private int _duration;
        private bool _flyout;
      
    }
}
