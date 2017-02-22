using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PLMJQueryComponent
{
     public class JQcarousel : WebControl
     {
         #region propierties
         [Browsable(true)]
         [Description("Assign the name to the carousel")]
         public string CarouselName
         {
             get { return this._carouselName; }
             set { this._carouselName = value; }
         }
         [Browsable(true)]
         [Description("Assign the items to the carousel")]
         public List<MenuItem> BannersItems
         {
             get { return this._bannersItems; }
             set { this._bannersItems = value; }
         }


         [Browsable(true)]
         [Description("Set the number of images displaced")]
         public int DispItems
         {
             get { return this._dispItems; }
             set { this._dispItems = value; }
         }
         [Browsable(true)]
         [Description("Active the AutoSlide")]
         public bool AutoSlide
         {
             get { return this._autoSlide; }
             set { this._autoSlide = value; }
         }
         [Browsable(true)]
         [Description("Active the loop")]
         public bool Loop
         {
             get { return this._loop; }
             set { this._loop = value; }
         }
         [Browsable(true)]
         [Description("Assign the time between intervals")]
         public int AutoSlideInterval
         {
             get { return this._autoSlideInterval; }
             set { this._autoSlideInterval = value; }
         } 



         #endregion

         #region render Contentes

         protected override void Render(HtmlTextWriter writer)
         {
             try
             {
                 StringBuilder stringToRender = new StringBuilder();




                 stringToRender.Append("<script type=\"text/javascript\">");
                 stringToRender.Append("\n $(function(){");
                 stringToRender.Append("\n $.unobtrusivelib();");
                 stringToRender.Append("\n prettyPrint();");
                 stringToRender.Append("\n $(\"div.carrusel\").carousel({ dispItems:"+ this.DispItems.ToString() + ",autoSlide: "+ this.AutoSlide.ToString().ToLower() +",loop:"+ this.Loop.ToString().ToLower() +",autoSlideInterval:"+ this.AutoSlideInterval.ToString() +" });");
                 stringToRender.Append("\n });");
                 stringToRender.Append("\n  </script>");


                 stringToRender.Append("\n  <div id=\"container\">");
                 stringToRender.Append("\n <div id=\"examples\" class=\"box box-inner\">");
                 stringToRender.Append("\n <div class=\"carousel carrusel\">");
                 stringToRender.Append("\n <ul>");

                 if (this._bannersItems.Count > 0)
                 {
                     for (int x = 0; x < this._bannersItems.Count; x++)
                     {
                         stringToRender.Append("\n <li><a href=" + this._bannersItems[x].NavigateUrl + " target='_blank'><img src= \" img/" + this._bannersItems[x].ImageUrl + "\" border=\"0\"  title='" + this._bannersItems[x].ToolTip  + "' /></a></li>");
                     }
                 }

                 stringToRender.Append("\n </ul>");
                 stringToRender.Append("\n </div>");
                 stringToRender.Append("\n </div>");
                 stringToRender.Append("\n </div>");

                 writer.RenderBeginTag(HtmlTextWriterTag.Div);
                 writer.Write(stringToRender.ToString());
                 writer.RenderEndTag();
             }
             catch
             {
                 writer.RenderBeginTag(HtmlTextWriterTag.Div);
                 writer.Write("Error Displaying JQcarousel");
                 writer.RenderEndTag();
             }

         }


         #endregion

         private string _carouselName;
         private List<MenuItem> _bannersItems;
         private int _dispItems;
         private bool _autoSlide;
         private bool _loop;
         private int _autoSlideInterval;
     }
}
