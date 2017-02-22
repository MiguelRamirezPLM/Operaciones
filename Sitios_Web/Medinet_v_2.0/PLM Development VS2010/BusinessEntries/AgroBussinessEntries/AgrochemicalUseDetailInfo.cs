using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Agrochemical Use information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is Agrochemical Use contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class AgrochemicalUseDetailInfo
    {

        #region Constructor

        public AgrochemicalUseDetailInfo() { }
        
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Agrochemical Use.
        ///     </para>
        ///     <para>
        ///         Agrochemical Use identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AgrochemicalUseId
        {
            get
            {
                return this._agrochemicalUseId;
            }
            set
            {
                this._agrochemicalUseId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Agrochemical Use.
        ///     </para>
        ///     <para>
        ///         Agrochemical Use.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AgrochemicalUseName
        {
            get
            {
                return this._agrochemicalUseName;
            }
            set
            {
                this._agrochemicalUseName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Parent.
        ///     </para>
        ///     <para>
        ///         ParentId.
        ///     </para>
        /// </summary>
        [DataMember]
        private int? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Use Key.
        ///     </para>
        ///     <para>
        ///         Use Key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UseKey
        {
            get
            {
                return this._useKey;
            }
            set
            {
                this._useKey = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Agrochemical Use is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _agrochemicalUseId;
        private string _agrochemicalUseName;
        private int? _parentId;
        private string _useKey;
        private bool _active;

    }
}
