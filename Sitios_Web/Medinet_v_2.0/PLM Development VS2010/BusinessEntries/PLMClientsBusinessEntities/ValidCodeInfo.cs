using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information of a valid code.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ValidCodeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ValidCodeInfo class. Not receive parameters.
        /// </summary>
        public ValidCodeInfo() 
        {
            this.IsExist = false;
            this.CodeStatusId = Catalogs.CodeStatus.NO_EXISTE;
        }
        
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeStatusId.
        ///     </para>
        ///     <para>
        ///         Code status identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public PLMClientsBusinessEntities.Catalogs.CodeStatus CodeStatusId
        {
            get
            {
                return this._codeStatusId; 
            }
            set 
            { 
                this._codeStatusId = value; 
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IsExist.
        ///     </para>
        ///     <para>
        ///         Indicates if the Code exists or not exists.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool IsExist
        {
            get 
            { 
                return this._exist; 
            }
            set 
            { 
                this._exist = value; 
            }
        }

        #endregion

        private PLMClientsBusinessEntities.Catalogs.CodeStatus _codeStatusId;
        private bool _exist;

    }
}
