using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class MessageInfo
    {
        #region Constructors

        public MessageInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int MessageId
        {
            get { return this._messageId; }
            set { this._messageId = value; }
        }

        [DataMember]
        public string MessageText
        {
            get { return this._messageText; }
            set { this._messageText = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _messageId;
        private string _messageText;
        private bool _active;

    }
}
