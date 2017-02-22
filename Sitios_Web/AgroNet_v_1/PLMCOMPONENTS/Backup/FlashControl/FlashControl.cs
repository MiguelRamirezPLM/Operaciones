using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PLMComponents
{
    [DefaultProperty("Movie")]
    public class FlashControl : WebControl
    {
        #region Properties

        [Browsable(true)]
        [Description("Set path to source file")]
        public string Movie
        {
            get { return this.m_movie; }
            set { this.m_movie = value; }
        }

        [Browsable(true)]
        [Description("Set quality to display the movie")]
        public string Quality
        {
            get { return this.m_quality; }
            set { this.m_quality = value; }
        }

        [Browsable(true)]
        [Description("Set mode to display the movie")]
        public string Wmode
        {
            get { return this.m_wmode; }
            set { this.m_wmode = value; }
        }

        [Browsable(true)]
        [Description("Used to send root variables to the movie")]
        public string FlashVars
        {
            get { return this.m_flashVars; }
            set { this.m_flashVars = value; }
        }

        [Browsable(true)]
        [Description()]
        public string Scale
        {
            get { return this.m_scale; }
            set { this.m_scale = value; }
        }

        [Browsable(true)]
        [Description()]
        public string Seamlesstabbing
        {
            get { return this.m_seamlesstabbing; }
            set { this.m_seamlesstabbing = value; }
        }

        [Browsable(true)]
        [Description()]
        public string Allowfullscreen
        {
            get { return this.m_allowfullscreen; }
            set { this.m_allowfullscreen = value; }
        }

        [Browsable(true)]
        [Description()]
        public string Menu
        {
            get { return this.m_menu; }
            set { this.m_menu = value; }
        }

        #endregion

        #region Render content

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder stringToRender = new StringBuilder();

                stringToRender.Append("<object ");
                stringToRender.Append("classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' ");
                stringToRender.Append("codebase='http://active.macromedia.com/flash6/cabs/swflash.cab#version=6.0.0.0' ");
                stringToRender.Append("height='" + this.Height.ToString() + "' ");
                stringToRender.Append("width='" + this.Width.ToString() + "' ");
                stringToRender.Append("id='" + this.ClientID + "'>");

                stringToRender.Append("<param name='Movie' value='" + this.m_movie + "'> ");
                stringToRender.Append("<param name='quality' value='" + this.m_quality + "'> ");
                stringToRender.Append("<param name='wmode' value='" + this.m_wmode + "'> ");
                stringToRender.Append("<param name='bgcolor' value='" + "#FFFFFF" + "'> ");
                stringToRender.Append("<param name='flashvars' value='" + this.m_flashVars + "'> ");
                stringToRender.Append("<param name='scale' value='" + this.m_scale + "'> ");
                stringToRender.Append("<param name='menu' value='" + this.m_menu + "'> ");
                stringToRender.Append("<param name='seamlesstabbing' value='" + this.m_seamlesstabbing + "'> ");
                stringToRender.Append("<param name='allowfullscreen' value='" + this.m_allowfullscreen + "'> ");

                stringToRender.Append("<embed ");
                stringToRender.Append("src='" + this.m_movie + "' ");
                stringToRender.Append("quality='" + this.m_quality + "' ");
                stringToRender.Append("bgcolor='" + "#FFFFFF" + "' ");
                stringToRender.Append("flashvars='" + this.m_flashVars + "'");
                stringToRender.Append("width='" + this.Width.ToString() + "' ");
                stringToRender.Append("height='" + this.Height.ToString() + "' ");
                stringToRender.Append("name='" + this.ClientID + "' ");
                stringToRender.Append("type='application/x-shockwave-flash' ");
                stringToRender.Append("pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' ");
                stringToRender.Append("type='application/x-shockwave-flash' wmode='" + this.m_wmode + "'> ");
                
                stringToRender.Append("</embed>");
                stringToRender.Append("</object>");

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(stringToRender.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Error Displaying Flash Control");
                writer.RenderEndTag();
            }      
        }

        #endregion

        #region Public Methods

        public void AddFlashVars(string var, string value)
        {
            if (!string.IsNullOrEmpty(this.m_flashVars))
                this.m_flashVars = this.m_flashVars + "&" + var + "=" + value;
            else
                this.m_flashVars = var + "=" + value;

        }

        #endregion


        private string m_movie;
        private string m_quality;
        private string m_wmode;
        private string m_scale;
        private string m_menu;
        private string m_seamlesstabbing;
        private string m_allowfullscreen;
        private string m_flashVars;
    }
}