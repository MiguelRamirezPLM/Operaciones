using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    
    /// <summary>
    /// 
    /// </summary>
     [DataContract]
    public class FileInfo
    {
         #region Constructor
         public FileInfo(){ }
         #endregion
        #region Propierities

         [DataMember]
         public int VersionFileId
         {
             get { return this._versionFileId; }
             set { this._versionFileId = value; }
         }
         [DataMember]
         public int FormatFileId
         {
             get { return this._formatFileId; }
             set { this._formatFileId = value; }
         }
         [DataMember]
         public string FileName
         {
             get { return this._fileName; }
             set { this._fileName = value; }
         }
         [DataMember]
         public DateTime FileDate
         {
             get { return this._fileDate; }
             set { this._fileDate = value; }
         }
         [DataMember]
         public string BaseUrl
         {
             get { return this._baseUrl; }
             set { this._baseUrl = value; }
         }
         [DataMember]
         public string Version
         {
             get { return this._version; }
             set { this._version = value; }
         }
         [DataMember]
         public string NickName
         {
             get { return this._nickName; }
             set { this._nickName = value; }
         }
        #endregion

         private int _versionFileId;
         private int _formatFileId;
         private string _fileName;
         private string _nickName;
         private DateTime _fileDate;
         private string _baseUrl;
         private string _version;

    }
}
