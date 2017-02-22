using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMUserBusinessEntries
{
    public sealed class Catalogs
    {
        #region Constructors

        public Catalogs() { }


        #endregion

        #region Enum

        public enum Aplications : byte
        {
            Medinet = 1,
            PharmaSearchEngine = 2,
            Website = 3,
            CrossPharmaSearchEngine = 5,
            AndroidEngine = 6,
            BBEngine = 7,
            iOSEngine = 8
        }

        #endregion


    }
}
