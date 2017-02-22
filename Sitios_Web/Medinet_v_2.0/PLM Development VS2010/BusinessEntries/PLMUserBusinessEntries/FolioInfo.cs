using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
     public class FolioInfo
     {
         #region constructor

         public FolioInfo() { }
        
         #endregion

        #region propierties

         [DataMember]
         public int FolioId
         {
             get { return _folioId; }
             set { this._folioId = value; }
         }

         [DataMember]
         public int ApplicationId
         {
             get { return _applicationId; }
             set { this._applicationId = value; }
         }

         [DataMember]
         public int InitialValue
         {
             get { return _initialValue; }
             set { this._initialValue = value; }
         }

         [DataMember]
         public string Prefix
         {
             get { return _prefix; }
             set { this._prefix = value; }
         }

         [DataMember]
         public int CurrentNumber
         {
             get { return _currentNumber; }
             set { this._currentNumber = value; }
         }

         [DataMember]
         public DateTime LastUpdate
         {
             get { return _lastUpdate; }
             set { this._lastUpdate = value; }
         }

        #endregion

         private int      _folioId;
        private int      _applicationId;
        private int      _initialValue;
        private string   _prefix;
        private int     _currentNumber;
        private DateTime _lastUpdate; 

    }
}
