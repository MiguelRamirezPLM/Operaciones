using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;

namespace FactoryControlComponent
{
    public class FactoryWebControl : FactoryControl
    {
        #region Constructors

        public FactoryWebControl(string type) : base(type) { }
        
        #endregion

        #region Protected Methods

        public WebControl getControl(string className, DataTable attributtes)
        {
            WebControl wc;

            switch (className)
            {
                case "Label":
                    Label l = new Label();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                l.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Text":
                                l.Text = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "CssClass":
                                l.CssClass = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                        }
                    }
                    wc = l;

                    break;
                case "TextBox":
                    TextBox t = new TextBox();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                t.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Text":
                                t.Text = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "CssClass":
                                t.CssClass = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Width":
                                t.Width = Convert.ToInt32(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "MaxLength":
                                t.MaxLength = Convert.ToInt32(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "Enabled":
                                t.Enabled = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "Visible":
                                t.Visible = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                        }
                    }
                    wc = t;
                    break;
                case "DropDownList":
                    DropDownList d = new DropDownList();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                d.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "CssClass":
                                d.CssClass = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Width":
                                d.Width = Convert.ToInt32(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "AutoPostBack":
                                d.AutoPostBack = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "Enabled":
                                d.Enabled = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                        }
                    }
                    wc = d;
                    break;
                case "RadioButtonList":
                    RadioButtonList rbl = new RadioButtonList();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                rbl.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "RepeatDirection":
                                rbl.RepeatDirection = getDirection(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "CssClass":
                                rbl.CssClass = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                        }
                    }
                    wc = rbl;
                    break;
                case "RequiredFieldValidator":
                    RequiredFieldValidator rfv = new RequiredFieldValidator();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                rfv.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "InitialValue":
                                rfv.InitialValue = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "CssClass":
                                rfv.CssClass = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "ErrorMessage":
                                rfv.ErrorMessage = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "ControlToValidate":
                                rfv.ControlToValidate = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Display":
                                rfv.Display = getDisplay(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "ValidationGroup":
                                rfv.ValidationGroup = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "SetFocusOnError":
                                rfv.SetFocusOnError = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                        }
                    }
                    wc = rfv;
                    break;
                case "RegularExpressionValidator":
                    RegularExpressionValidator rev = new RegularExpressionValidator();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                rev.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "ValidationExpression":
                                rev.ValidationExpression = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "ErrorMessage":
                                rev.ErrorMessage = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "ControlToValidate":
                                rev.ControlToValidate = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "Display":
                                rev.Display = getDisplay(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "ValidationGroup":
                                rev.ValidationGroup = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "SetFocusOnError":
                                rev.SetFocusOnError = Convert.ToBoolean(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                        }
                    }
                    wc = rev;
                    break;
                
                default:
                    wc = null;
                    break;
            }

            return wc;

        }

        public ExtenderControlBase getAjaxExtender(string className, DataTable attributtes)
        {
            ExtenderControlBase ec;

            switch (className)
            {
                case "FilteredTextBoxExtender":
                    AjaxControlToolkit.FilteredTextBoxExtender fte = new FilteredTextBoxExtender();
                    for (int x = 0; x < attributtes.Rows.Count; x++)
                    {
                        switch (attributtes.Rows[x]["AttributeName"].ToString())
                        {
                            case "ID":
                                fte.ID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                            case "FilterType":
                                fte.FilterType = getFilterType(attributtes.Rows[x]["AttributeValue"].ToString());
                                break;
                            case "TargetControlID":
                                fte.TargetControlID = attributtes.Rows[x]["AttributeValue"].ToString();
                                break;
                        }
                    }
                    ec = fte;
                    break;
                default:
                    ec = null;
                    break;
            }

            return ec;
        }

        #endregion

        #region Private Methods

        private ValidatorDisplay getDisplay(string displayType)
        {
            switch (displayType)
            {
                case "Dynamic":
                    return ValidatorDisplay.Dynamic;
                    break;
                case "Static":
                    return ValidatorDisplay.Static;
                    break;
                default:
                    return ValidatorDisplay.None;
                    break;
            }

        }

        private System.Web.UI.WebControls.RepeatDirection getDirection(string direction)
        {
            if(direction == "Horizontal")
                return System.Web.UI.WebControls.RepeatDirection.Horizontal;
            else                    
                return System.Web.UI.WebControls.RepeatDirection.Vertical;
    
        }

        private AjaxControlToolkit.FilterTypes getFilterType(string filterType)
        {
            switch (filterType)
            {
                case "Numbers":
                    return FilterTypes.Numbers;
                    break;
                case "UppercaseLetters":
                    return FilterTypes.UppercaseLetters;
                    break;
                case "LowercaseLetters":
                    return FilterTypes.LowercaseLetters;
                    break;
                default:
                    return FilterTypes.Custom;
                    break;
            }
        }

        #endregion


    }
}
