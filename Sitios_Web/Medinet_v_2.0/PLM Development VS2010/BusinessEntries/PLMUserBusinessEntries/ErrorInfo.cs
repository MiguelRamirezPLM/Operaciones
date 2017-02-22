using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class ErrorInfo
    {
        #region Constructors


        public ErrorInfo() { }


        #endregion


        #region Properties

        [DataMember]
        public int ErrorId
        {
            get { return _errorId; }
            set { this._errorId = value; }
        }

        [DataMember]
        public int ApplicationId
        {
            get { return _applicationId; }
            set { this._applicationId = value; }
        }

        [DataMember]
        public string Folio
        {
            get { return _folio; }
            set { this._folio = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set { this._date = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _message; }
            set { this._message = value; }
        }

        [DataMember]
        public string StackTrace
        {
            get { return _stackTrace; }
            set { this._stackTrace = value; }
        }

        [DataMember]
        public bool Status
        {
            get { return _status; }
            set { this._status = value; }
        }

        #endregion


        private int _errorId;
        private int _applicationId;
        private string _folio;
        private DateTime _date;
        private string _message;
        private string _stackTrace;
        private bool _status;


    }
}
