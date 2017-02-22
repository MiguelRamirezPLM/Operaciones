/// This code is FREE and NOT copyrighted. Use it anyway you want!
/// Distribution of this code in no way implies any warranty of guarantee. Use at your own risk.
/// Raj Kaimal http://weblogs.asp.net/rajbk/

using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Drawing.Design;

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("Flan.Controls.UpdatePanelPopupExtender.UpdatePanelPopupBehavior.js", "text/javascript")]
#endregion

namespace Flan.Controls {
    [TargetControlType(typeof(UpdatePanel))]
    [System.Diagnostics.DebuggerStepThrough]
    public class UpdatePanelPopupExtender : ExtenderBase, IPostBackEventHandler {

        [Description("This event is raised anytime the UpdatePanel is hidden as a result of clicking on the body of the page or when a 'close' button (RegisterCloseControl) is clicked")]
        public event EventHandler Close;


        private int _offsetX;
        private int _offsetY;
        private HorizontalAlign _horizontalAlign;
        private VerticalAlign _verticalAlign;
        private Color _calloutColor;
        private Color _calloutBorderColor;
        private CalloutType _calloutType;
        private bool _autoPostBack;
        private bool _sendDataItem;

        #region Properties

        [DefaultValue(0)]
        [Description("The horizontal offset between the UpdatePanel and the control where the UpdatePanel should be positioned")]
        public int OffsetX {
            get { return _offsetX; }
            set { _offsetX = value; }
        }

        [DefaultValue(0)]
        [Description("The vertical offset between the UpdatePanel and the control where the UpdatePanel should be positioned")]
        public int OffsetY {
            get { return _offsetY; }
            set { _offsetY = value; }
        }

        [DefaultValue(typeof(Color), ""), TypeConverter(typeof(WebColorConverter))]
        [Description("The color of the callout.")]
        public Color CalloutColor {
            get { return _calloutColor; }
            set { _calloutColor = value; }
        }

        [DefaultValue(typeof(Color), ""), TypeConverter(typeof(WebColorConverter))]
        [Description("The callout border color.")]
        public Color CalloutBorderColor {
            get { return _calloutBorderColor; }
            set { _calloutBorderColor = value; }
        }

        [Description("The type of callout.")]
        public CalloutType CalloutType {
            get { return _calloutType; }
            set { _calloutType = value; }
        }

        [Description("Raises the close event when the UpdatePanel is hidden.")]
        [DefaultValue(false)]
        public bool AutoPostBack {
            get { return _autoPostBack; }
            set { _autoPostBack = value; }
        }

        [DefaultValue(VerticalAlign.Middle)]
        [Description("The vertical alignment of the the UpdatePanel with the control where the UpdatePanel should be positioned")]
        public VerticalAlign VerticalAlign {
            get { return _verticalAlign; }
            set { _verticalAlign = value; }
        }

        [DefaultValue(HorizontalAlign.Right)]
        [Description("The horizontal alignment of the the UpdatePanel with the control where the UpdatePanel should be positioned")]
        public HorizontalAlign HorizontalAlign {
            get { return _horizontalAlign; }
            set { _horizontalAlign = value; }
        }

        [IDReferenceProperty(typeof(WebControl))]
        [Description("The ClientID of control for where the UpdatePanel should be positioned")]
        [Browsable(false)]
        public string PositionControlClientID {
            get { return ViewState["positionControlClientID"] as string; }
            set { ViewState["positionControlClientID"] = value; }
        }

        [Description("Sets the client side visibility of the UpdatePanel")]
        private bool UpdatePanelVisible {
            get { return (bool)(ViewState["UpdatePanelVisible"] ?? false); }
            set { ViewState["UpdatePanelVisible"] = value; }
        }

        private ScriptManager CurrentScriptManager {
            get {
                ScriptManager sm = ScriptManager.GetCurrent(Page);
                if (sm != null) {
                    return sm;
                }
                throw new HttpException("A ScriptManager control must exist on the current page.");
            }
        }
        #endregion

        #region Overrides

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            CurrentScriptManager.RegisterAsyncPostBackControl(this);
        }

        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl) {
            ScriptBehaviorDescriptor descriptor =
                new ScriptBehaviorDescriptor("Flan.UpdatePanelPopupBehavior", targetControl.ClientID);
            descriptor.AddProperty("clientID", this.ClientID);
            descriptor.AddProperty("uniqueID", this.UniqueID);
            descriptor.AddProperty("offsetX", this.OffsetX);
            descriptor.AddProperty("offsetY", this.OffsetY);
            descriptor.AddProperty("align", this.HorizontalAlign);
            descriptor.AddProperty("valign", this.VerticalAlign);
            descriptor.AddProperty("loadVisible", this.UpdatePanelVisible);
            descriptor.AddProperty("positionElementID", this.PositionControlClientID);
            descriptor.AddProperty("calloutColor", ColorAsHtml(this.CalloutColor));
            descriptor.AddProperty("calloutBorderColor", ColorAsHtml(this.CalloutBorderColor));
            descriptor.AddProperty("calloutType", this.CalloutType);
            descriptor.AddProperty("autoPostBack", this.AutoPostBack);
            return new ScriptDescriptor[] { descriptor };
        }

        protected override IEnumerable<ScriptReference> GetScriptReferences() {
            ScriptReference reference = new ScriptReference(
                "Flan.Controls.UpdatePanelPopupExtender.UpdatePanelPopupBehavior.js", "Flan.Controls");
            return new ScriptReference[] { reference };
        }

        protected override void OnPreRender(EventArgs e) {
            if (_sendDataItem) {
                SendAsyncData();
            }
            base.OnPreRender(e);
        }

        #endregion

        #region Private

        private void SendAsyncData() {
            if (!CurrentScriptManager.IsInAsyncPostBack) {
                return;
            }

            Dictionary<string, object> dataItems = new Dictionary<string, object>();
            dataItems.Add("positionElementID", PositionControlClientID);
            dataItems.Add("visible", UpdatePanelVisible);
            JavaScriptSerializer js = new JavaScriptSerializer();
            CurrentScriptManager.RegisterDataItem(this, js.Serialize(dataItems));
        }

        private string ColorAsHtml(Color c) {
            string s = ColorTranslator.ToHtml(c);
            if (string.IsNullOrEmpty(s)) {
                return null;
            }
            return s;
        }

        private void OnClose() {
            if (Close != null) {
                Close(this, EventArgs.Empty);
            }
        }

        private void RaisePostBackEvent(string eventArgument) {
            OnClose();
        }

        #endregion

        #region Public methods

        [Description("Register a control which will hide the UpdatePanel on click.")]
        public static void RegisterCloseControl(IAttributeAccessor control, UpdatePanel updatepanel) {
            control.SetAttribute("uppHide", "true");
            control.SetAttribute("uppTarget", updatepanel.ClientID);
        }

        [Description("Position and show the UpdatePanel")]
        public void Show() {
            if (string.IsNullOrEmpty(this.PositionControlClientID)) {
                throw new ArgumentNullException("PositionControlClientID", Target_Is_Null);
            }
            this.UpdatePanelVisible = true;
            _sendDataItem = true;
        }

        [Description("Position and show the UpdatePanel at the control specified")]
        public void ShowAt(Control positionControl) {
            this.PositionControlClientID = positionControl.ClientID;
            Show();
        }

        [Description("Hide the UpdatePanel on the client")]
        public void Hide() {
            this.UpdatePanelVisible = false;
            _sendDataItem = true;
        }

        #endregion


        #region IPostBackEventHandler Members

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument) {
            this.RaisePostBackEvent(eventArgument);
        }

        #endregion
    }

    public enum HorizontalAlign {
        Right = 0,
        Center = 1,
        Left = 2
    }

    public enum VerticalAlign {
        Middle = 0,
        Top = 1,
        Bottom = 2
    }

    public enum CalloutType {
        TransparentGradient = 0,
        Solid = 1
    }
}