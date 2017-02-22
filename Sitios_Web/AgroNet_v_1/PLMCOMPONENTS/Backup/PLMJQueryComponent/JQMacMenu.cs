using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PLMJQueryComponent
{
    public class JQMacMenu : WebControl
    {
        #region Properties

        [Browsable(true)]
        [Description("Assign the items to the menu")]
        public List<MenuItem> MenuItems
        {
            get { return this._menuItems; }
            set { this._menuItems = value; }
        }

        [Browsable(true)]
        [Description("Sets the horizontal position")]
        public string HAlign
        {
            get { return this._halign; }
            set { this._halign = value; }
        }

        [Browsable(true)]
        [Description("Sets the maximum width of images")]
        public int MaxWidth
        {
            get { return this._maxWidth; }
            set { this._maxWidth = value; }
        }

        [Browsable(true)]
        [Description("Sets the width of each element")]
        public int ItemWidth
        {
            get { return this._itemWidth; }
            set { this._itemWidth = value; }
        }

        [Browsable(true)]
        [Description("Sets the effect of bringing the menu")]
        public int Proximity
        {
            get { return this._proximity; }
            set { this._proximity = value; }
        }

        [Browsable(true)]
        [Description("Sets the name of the control")]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        #endregion

        #region Render Contents
        
        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder stringToRender = new StringBuilder();

                //stringToRender.Append("<script type=\"text/javascript\">");
                //stringToRender.Append("\n $(document).ready(");
                //stringToRender.Append("\n function()");
                //stringToRender.Append("\n {");
                //stringToRender.Append("\n $('#dock').Fisheye(");
                //stringToRender.Append("\n {");
                //stringToRender.Append("\n maxWidth: " + this._maxWidth + ", ");
                //stringToRender.Append("\n items: 'a', ");
                //stringToRender.Append("\n itemsText: 'span',");
                //stringToRender.Append("\n container: '.dock-container',");
                //stringToRender.Append("\n itemWidth: " + this._itemWidth + ",");
                //stringToRender.Append("\n  proximity: " + this._proximity + ",");
                //stringToRender.Append("\n  alignment : 'left',");
                //stringToRender.Append("\n  valign: 'bottom',");
                //stringToRender.Append("\n  halign : '" + this._halign + "'");
                //stringToRender.Append("\n  }");
                //stringToRender.Append("\n  )");
                //stringToRender.Append("\n  }");
                //stringToRender.Append("\n  );");
                //stringToRender.Append("\n </script>");

                stringToRender.Append("\n  <div id='dock'>");
                stringToRender.Append("\n  <div class='dock-container'>");


                if (this._menuItems.Count > 0)
                {
                    for (int x = 0; x < this._menuItems.Count; x++)
                    {
                        if(!string.IsNullOrEmpty(this._menuItems[x].NavigateUrl))
                            stringToRender.Append("\n <a class='dock-item' href='" + this._menuItems[x].NavigateUrl + "' ><span></span><img src='img/" + this._menuItems[x].ImageUrl + "' /></a>");
                        else
                            stringToRender.Append("\n <a class='dock-item' ><span></span><img src='img/" + this._menuItems[x].ImageUrl + "' /></a>");
                    }

                }
                
                stringToRender.Append("\n  </div>");

                stringToRender.Append("\n </div>");

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(stringToRender.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Error Displaying JQMacMenu");
                writer.RenderEndTag();
            }

        }

        #endregion


        private List<MenuItem> _menuItems;
        private string _halign;
        private int _maxWidth;
        private int _itemWidth;
        private int _proximity;
        private string _name;

    }
}
