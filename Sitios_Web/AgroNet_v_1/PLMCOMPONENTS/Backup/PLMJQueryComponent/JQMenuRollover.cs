using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PLMJQueryComponent
{
    public class JQMenuRollover : WebControl
    {
        #region Properties

        [Browsable(true)]
        [Description("Obtain the items to the menu")]
        public MenuItemCollection MenuItems
        {
            get { return this._menuItems; }
            set { this._menuItems = value; }
        }

        [Browsable(true)]
        [Description("sets the duration of the effect ")]
        public int Duration
        {
            get { return this._duration; }
            set { this._duration= value; }
        }

        [Browsable(true)]
        [Description(" sets the  vertical position of the background")]
        public int VerticalPosition
        {
            get { return this._verticalPosition; }
            set { this._verticalPosition = value; }
        }

        [Browsable(true)]
        [Description("  sets the  horizontal position of the background")]
        public int HorizontalPosition
        {
            get { return this._horizontalPosition; }
            set { this._horizontalPosition = value; }
        }
       
        [Browsable(true)]
        [Description(" enables the effect of fade")]
        public bool Fade
        {
            get { return this._fade; }
            set { this._fade = value; }
        }

        [Browsable(true)]
        [Description("enables the topdown effect")]
        public bool TopEffect
        {
            get { return this._topEffect; }
            set { this._topEffect = value; }
        }

        #endregion

        #region render Contents

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder stringToRender = new StringBuilder();
                
                stringToRender.Append("<script type=\"text/javascript\">");
                
                stringToRender.Append("\n$(function(){");
                stringToRender.Append("\n \t $('#Menu a')");
                if (_fade)
                    {
                        stringToRender.Append("\n.css( {backgroundPosition: \"0 0\"} )");
                        stringToRender.Append("\n.mouseover(function(){");
                        stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + " " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration.ToString() + "})");
                        stringToRender.Append("\n	})");
                        stringToRender.Append("\n.mouseout(function(){");
                        stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"(0 0)\"}, {duration:" + this._duration.ToString() + "})");
                        stringToRender.Append("\n})");

                    }
                else
                    {
                        if (_topEffect)
                        {
                            stringToRender.Append("\n.css( {backgroundPosition: \"" + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px\"} )");
                            stringToRender.Append("\n.mouseover(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + "px " + (this._verticalPosition * 2.5) + "px)\"}, {duration:" + this._duration.ToString() + "})");
                            stringToRender.Append("\n	})");
                            stringToRender.Append("\n.mouseout(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + (this._horizontalPosition * -2) + "px " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration + ", complete:function(){");
                            stringToRender.Append("\n$(this).css({backgroundPosition: \"" + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px\"} )");
                            stringToRender.Append("\n}})");
                            stringToRender.Append("\n})");
                        }
                        else
                        {
                            stringToRender.Append("\n.css( {backgroundPosition: \"0 0\"} )");
                            stringToRender.Append("\n.mouseover(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration.ToString() + "})");
                            stringToRender.Append("\n	})");
                            stringToRender.Append("\n.mouseout(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + (this._horizontalPosition * 2) + "px " + (this._verticalPosition * 2) + "px)\"}, {duration:" + this._duration + ", complete:function(){");
                            stringToRender.Append("\n$(this).css({backgroundPosition: \" 0 0\"} )");
                            stringToRender.Append("\n}})");
                            stringToRender.Append("\n})");
                        }
                        
                    }

                    stringToRender.Append("\n \t $('#Menu span')");
                    if (_fade)
                    {
                        stringToRender.Append("\n.css( {backgroundPosition: \"0 0\"} )");
                        stringToRender.Append("\n.mouseover(function(){");
                        stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + " " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration.ToString() + "})");
                        stringToRender.Append("\n	})");
                        stringToRender.Append("\n.mouseout(function(){");
                        stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"(0 0)\"}, {duration:" + this._duration.ToString() + "})");
                        stringToRender.Append("\n})");

                    }
                    else
                    {
                        if (_topEffect)
                        {
                            stringToRender.Append("\n.css( {backgroundPosition: \"" + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px\"} )");
                            stringToRender.Append("\n.mouseover(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + "px " + (this._verticalPosition * 2.5) + "px)\"}, {duration:" + this._duration.ToString() + "})");
                            stringToRender.Append("\n	})");
                            stringToRender.Append("\n.mouseout(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + (this._horizontalPosition * -2) + "px " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration + ", complete:function(){");
                            stringToRender.Append("\n$(this).css({backgroundPosition: \"" + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px\"} )");
                            stringToRender.Append("\n}})");
                            stringToRender.Append("\n})");
                        }
                        else
                        {
                            stringToRender.Append("\n.css( {backgroundPosition: \"0 0\"} )");
                            stringToRender.Append("\n.mouseover(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + this._horizontalPosition.ToString() + "px " + this._verticalPosition.ToString() + "px)\"}, {duration:" + this._duration.ToString() + "})");
                            stringToRender.Append("\n	})");
                            stringToRender.Append("\n.mouseout(function(){");
                            stringToRender.Append("\n$(this).stop().animate({backgroundPosition:\"( " + (this._horizontalPosition * 2) + "px " + (this._verticalPosition * 2) + "px)\"}, {duration:" + this._duration + ", complete:function(){");
                            stringToRender.Append("\n$(this).css({backgroundPosition: \" 0 0\"} )");
                            stringToRender.Append("\n}})");
                            stringToRender.Append("\n})");
                        }

                    }

                    stringToRender.Append("\n \t }); ");
                    stringToRender.Append("\n  </script>");

                    stringToRender.Append("\n<table valign='Top'>");
                    stringToRender.Append("\n<tr> <td valign='Top'>");
                    stringToRender.Append("\n <ul id=\"Menu\" style=\"list-style:none;padding:0;margin:0;\">");

                for (int x = 0; x < this._menuItems.Count; x++)
                {
                    if(this._menuItems[x].ChildItems.Count == 0)
                        stringToRender.Append("\n <li><a href=\"" + this._menuItems[x].NavigateUrl + "\" >" + this._menuItems[x].Text + "</a></li>");
                    else
                    {
                        stringToRender.Append("\n <li><span>" + this._menuItems[x].Text + "</span>");
                        //stringToRender.Append("\n <ul style=\"list-style:none;padding:0;margin:0; display:none;\">");
                        stringToRender.Append("\n <ul>");
                        stringToRender.Append(this.generateMenu(this._menuItems[x]));
                        stringToRender.Append("\n </ul>");
                        stringToRender.Append("\n </li>");

                    }
                }

                stringToRender.Append("\n </ul>");
                stringToRender.Append("\n </td> </tr>");
                stringToRender.Append("\n </table>");
    
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(stringToRender.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Error Displaying JQMenuRollover Control");
                writer.RenderEndTag();
            }

        }

        #region Events

        public void Add(MenuItem item)
        {
            this._menuItems.Add(item);
        }
        #endregion

        #region Methods

        private string generateMenu(MenuItem item)
        {
            StringBuilder strRender = new StringBuilder();

            for (int z = 0; z < item.ChildItems.Count; z++)
            {
                if (item.ChildItems[z].ChildItems.Count != 0)
                {
                    //strRender.Append("\n <li style=\"float:left;position:relative;\"><span>" + item.ChildItems[z].Text + "</span>");
                    //strRender.Append("\n <ul style=\"list-style:none;padding:0;margin:0;display:none;float:left;position:absolute;margin-top:-15px;margin-left:100px;\">");
                    strRender.Append("\n <li><span>" + item.ChildItems[z].Text + "</span>");
                    strRender.Append("\n <ul>");
                    strRender.Append(this.generateMenu(item.ChildItems[z]));
                    strRender.Append("\n </ul>");
                    strRender.Append("\n </li>");
                }
                else
                    //strRender.Append("\n <li style=\"float:left;position:relative;\"><a href=\"" + item.ChildItems[z].NavigateUrl + "\" >" + item.ChildItems[z].Text + "</a></li>");
                    strRender.Append("\n <li><a href=\"" + item.ChildItems[z].NavigateUrl + "\" >" + item.ChildItems[z].Text + "</a></li>");

            }


            return strRender.ToString();
        }

        #endregion


        #endregion


        private MenuItemCollection _menuItems;
        private int _duration;
        private int _verticalPosition;
        private int _horizontalPosition;
        private bool _fade;
        private bool _topEffect;
        
    }
}
