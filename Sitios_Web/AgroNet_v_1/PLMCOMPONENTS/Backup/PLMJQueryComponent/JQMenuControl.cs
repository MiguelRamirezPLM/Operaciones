using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PLMJQueryComponent
{

    public class JQMenuControl : WebControl
    {
        #region Properties

        [Browsable(true)]
        [Description("Obtain the items to the menu")]
        public MenuItemCollection MenuItems
        {
            get { return this._menuItems; }      
        }

        [Browsable(true)]
        [Description("Set the margin size")]
        public int Margin
        {
            get { return this._margin; }
            set { this._margin = value; }
        }
        
        [Browsable(true)]
        [Description("Set the effect speed")]
        public int Velocity
        {
            get { return this._velocity; }
            set { this._velocity = value; }
        }

        [Browsable(true)]
        [Description("Establish the left container position.")]
        public int ContainerLeft
        {
            get { return this._leftContainerPosition; }
            set { this._leftContainerPosition =value; }
        }
        [Browsable(true)]
        [Description("Establish the top container position.")]
        public double TopContainerPosition
        {
            get { return this._topContainerPosition; }
            set { this._topContainerPosition = value; }
        }

        #endregion

        #region Render content

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder stringToRender = new StringBuilder();
                
               
                
                stringToRender.Append("<script type=\"text/javascript\">");
                
         

                stringToRender.Append("\n$(document).ready(function() {");
                stringToRender.Append("\n$(\"#JQMenu li\").prepend(\"<span></span>\");");
                stringToRender.Append("\n$(\"#JQMenu li\").each(function() {");
                stringToRender.Append("\nvar linkText = $(this).find(\"a\").html();");
                stringToRender.Append("\n$(this).find(\"span\").show().html(linkText);");
    	        stringToRender.Append("   }); ");
                stringToRender.Append("\n$(\"#JQMenu li\").hover(function() {");
                stringToRender.Append("\n $(this).find(\"span\").stop().animate({ ");
                stringToRender.Append(" \n marginTop: \"-" + this._margin + "\"");
                stringToRender.Append("\n}, " + this._velocity + ");");
                stringToRender.Append("\n } , function() {");
                stringToRender.Append("\n$(this).find(\"span\").stop().animate({");
                stringToRender.Append("\n marginTop: \"0\"");
                stringToRender.Append("\n }, " + this._velocity + ");");
                stringToRender.Append("\n });");
                stringToRender.Append("\n });");
                stringToRender.Append("\n  </script>");

                stringToRender.Append("\n <div class='container' style='position:absolute; overflow:hidden; top:" 
                    + this._topContainerPosition.ToString()+ "%; left:"+ this._leftContainerPosition.ToString() + "%;' > ");
                stringToRender.Append("\n <table><tr>");
                stringToRender.Append("\n <ul id=\"JQMenu\" class=\"v2\" >");
                
                for (int x = 0; x < this._menuItems.Count; x++)
                {
                    if (this._menuItems[x].ChildItems.Count == 0)
                        stringToRender.Append("\n <td><li><a href=\"" + this._menuItems[x].NavigateUrl + "\" >" + this._menuItems[x].Text + "</a></li></td>");
                    else
                    {
                        stringToRender.Append("\n <td>");
                        //stringToRender.Append("\n <td><li><span>" + this._menuItems[x].Text + "</span>");
                        //stringToRender.Append("\n <ul>");

                        stringToRender.Append(this.generateMenu(this._menuItems[x]));
                        
                        //stringToRender.Append("\n </ul>");
                        stringToRender.Append("\n </td>");
                    }
                }

                stringToRender.Append("\n </ul>");
                stringToRender.Append("\n </tr></table>");
                stringToRender.Append("\n </div>");
                
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(stringToRender.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Error Displaying JQMenu Control");
                writer.RenderEndTag();
            }
            
        }

        #endregion

        #region Events

        public virtual void Add(MenuItem item)
        {
            this._menuItems.Add(item);
        }
        #endregion

        #region Methods

        private string generateMenu(MenuItem item)
        {
            StringBuilder strRender = new StringBuilder();

            strRender.Append("\n <li><span>" + item.Text + "</span>");
            strRender.Append("\n <ul>");

            for (int z = 0; z < item.ChildItems.Count; z++)
            {
                if (item.ChildItems[z].ChildItems.Count != 0)
                    strRender.Append(this.generateMenu(item.ChildItems[z]));
                else
                    strRender.Append("\n <li><a href=\"" + item.ChildItems[z].NavigateUrl + "\" >" + item.ChildItems[z].Text + "</a></li>");
                               
            }
            strRender.Append("\n </ul>");
            strRender.Append("\n </li>");

            return strRender.ToString();
        }

        #endregion

        private MenuItemCollection _menuItems;
        private int _margin = 40;
        private int _velocity = 800;
        private int _leftContainerPosition;
        private double _topContainerPosition;   
    }
}
