using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which contains nomenclature used in the Web Service.
    /// </summary>
    public sealed class Catalogs
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Catalogs class. Not receive parameters.
        /// </summary>
        public Catalogs() { }

        #endregion

        #region Enum

        /// <summary>
        ///     Enumeration which represents the values ​​for each mobile device.
        /// </summary>
        public enum TargetOutputs
        {
            /// <summary>
            ///     Value one which stands iOS devices.
            /// </summary>
            iOS = 1,
            
            /// <summary>
            ///     Value two which stands BlackBerry devices.
            /// </summary>
            BLACKBERRY = 2,
            
            /// <summary>
            ///     Value three which stands Android devices.
            /// </summary>
            ANDROID = 3,
            
            /// <summary>
            ///     Value four which stands Functional Cellulars.
            /// </summary>
            CELULARES_FUNCIONALES = 4,
            
            /// <summary>
            ///     Value five which stands Web Services.
            /// </summary>
            SERVICIO_WEB = 5
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each mobile device.
        /// </summary>
        public enum Books
        {

            /// <summary>
            ///     Value one which stands DEF Book.
            /// </summary>
            DEF = 3,

            /// <summary>
            ///     Value twenty-one which stands OTC Book.
            /// </summary>
            OTC = 21,

            /// <summary>
            ///     Value two which stands VMG Book.
            /// </summary>
            VMG = 25

        }

        #endregion

    }
}
