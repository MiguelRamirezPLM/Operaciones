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

namespace Flan.Controls {
    public abstract class ExtenderBase : ExtenderControl {
        internal const string Control_Not_Found = "Unable to find control id '{0}' referenced by the '{1}' property of '{2}'";
        internal const string Target_Is_Null = "The target control is not defined.";

        /// <summary>
        /// Locate control by walking up the control tree
        /// ref: Ajax Control Toolkit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal Control FindControlHelper(string id) {
            Control c = base.FindControl(id);
            Control nc = NamingContainer;

            while ((null == c) && (null != nc)) {
                c = nc.FindControl(id);
                nc = nc.NamingContainer;
            }
            return c;
        }
    }
}