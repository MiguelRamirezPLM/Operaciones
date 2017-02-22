using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMUserBusinessEntries
{
    [DataContract]
   public sealed class DocumentInfo
    {

       

        private int _applicationId;
        private string _description;
        private string _documentFile;

        #region Constructors

        public DocumentInfo()
        {

        }

        #endregion

        #region Properties

        [DataMember]
        public int ApplicationId
        {
            get { return this._applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember]
        public string DocumentFile
        {
            get { return this._documentFile; }
            set { this._documentFile = value; }
        }

        #endregion
    }
}
