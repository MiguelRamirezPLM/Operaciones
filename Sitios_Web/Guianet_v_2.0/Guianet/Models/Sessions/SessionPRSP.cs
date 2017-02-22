using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionPRSP
    {
        #region Constructor

        public SessionPRSP(int CId, int ClId, int EId, int BId, byte? CTId, int? Type)
        {
            country = CId;
            client = ClId;
            edition = EId;
            book = BId;
            ClientType = CTId;
            Types = Type;
        }

        #endregion

        #region Member

        private int country;
        private int client;
        public int edition;
        public int book;
        public byte? ClientType;
        public int? Types;

        #endregion

        #region Properties

        public int CId
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public int ClId
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        public int EId
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
            }
        }

        public int BId
        {
            get
            {
                return book;
            }
            set
            {
                book = value;
            }
        }

        public byte? CTId
        {
            get
            {
                return ClientType;
            }
            set
            {
                ClientType = value;
            }
        }

        public int? Type
        {
            get
            {
                return Types;
            }
            set
            {
                Types = value;
            }
        }

        #endregion
    }
}