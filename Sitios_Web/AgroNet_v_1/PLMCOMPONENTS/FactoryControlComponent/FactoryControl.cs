using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryControlComponent
{
    public class FactoryControl
    {
        #region Constructors

        public FactoryControl(string type)
        {
            this._controlType = type;
        }

        #endregion

        #region Protected Methods

        protected string getType()
        {
            return _controlType;
        }


        #endregion


        private string _controlType;
    }
}
