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

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("Flan.Controls.UpdateProgressOverlayExtender.UpdateProgressOverlayBehavior.js", "text/javascript")]
#endregion

namespace Flan.Controls {
    [TargetControlType(typeof(UpdateProgress))]
    public class UpdateProgressOverlayExtender : ExtenderBase {

        private string _controlToOverlayID;
        private string _cssClass;
        private OverlayType _overlayType;

        [Description("CSS class to apply to the UpdateProgress control")]
        public string CssClass {
            get { return _cssClass; }
            set { _cssClass = value; }
        }

        [Description("Control that the UpdateProgress control will Overlay. If left blank and OverlayType is not \"Browser\", the AssociatedUpdatePanelID of the TargetControl is used.")]
        public string ControlToOverlayID {
            get { return _controlToOverlayID; }
            set { _controlToOverlayID = value; }
        }

        [Description("Overlay over a specific control or the browser viewing area")]
        [DefaultValue(typeof(OverlayType), "Control")]
        public OverlayType OverlayType {
            get { return _overlayType; }
            set { _overlayType = value; }
        }

        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl) {
            ScriptBehaviorDescriptor descriptor =
                new ScriptBehaviorDescriptor("Flan.UpdateProgressOverlayBehavior", targetControl.ClientID);

            UpdateProgress up = targetControl as UpdateProgress;
            string asscUpdatePanelClientId = string.IsNullOrEmpty(up.AssociatedUpdatePanelID) ?
                null : GetClientId(up.AssociatedUpdatePanelID, "AssociatedUpdatePanelID");


            string controlToOverlayID = null;
            if (_overlayType != OverlayType.Browser) {
                controlToOverlayID = string.IsNullOrEmpty(ControlToOverlayID) ?
                    asscUpdatePanelClientId : GetClientId(ControlToOverlayID, "ControlToOverlayID");
            }

            descriptor.AddProperty("controlToOverlayID", controlToOverlayID);
            descriptor.AddProperty("associatedUpdatePanelID", asscUpdatePanelClientId);
            descriptor.AddProperty("displayAfter", up.DisplayAfter);
            descriptor.AddProperty("targetCssClass", this.CssClass);
            return new ScriptDescriptor[] { descriptor };
        }

        protected override IEnumerable<ScriptReference> GetScriptReferences() {
            ScriptReference reference = new ScriptReference(
                "Flan.Controls.UpdateProgressOverlayExtender.UpdateProgressOverlayBehavior.js", "Flan.Controls");
            return new ScriptReference[] { reference };
        }

        private string GetClientId(string controlID, string propertyName) {
            Control control = base.FindControlHelper(controlID);
            if (control == null) {
                throw new HttpException(
                   String.Format(Control_Not_Found, controlID,
                       propertyName, this.ID));
            }

            return control.ClientID;
        }
    }

    public enum OverlayType {
        Control = 0,
        Browser = 1
    }
}