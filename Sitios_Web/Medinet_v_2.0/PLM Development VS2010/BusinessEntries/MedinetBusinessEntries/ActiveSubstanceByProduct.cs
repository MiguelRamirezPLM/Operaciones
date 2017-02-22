using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the information of the Active Substances contained in a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.UnitInfo"/>
    [DataContract]
    public class ActiveSubstanceByProduct
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ActiveSubstanceByProduct class. Not receive parameters.
        /// </summary>
        public ActiveSubstanceByProduct() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Pro_ID. 
        ///     </para>
        ///     <para>
        ///         Product identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int Pro_ID
        {
            get { return this._pro_ID; }
            set { this._pro_ID = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Sus_ID.
        ///     </para>
        ///     <para>
        ///         Active Substance identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int Sus_ID
        {
            get { return this._sus_ID; }
            set { this._sus_ID = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Sus_Nombre.
        ///     </para>
        ///     <para>
        ///         Active Substance Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Sus_Nombre
        {
            get { return this._sus_Nombre; }
            set { this._sus_Nombre = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Prs_Cantidad.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Prs_Cantidad
        {
            get { return this._prs_Cantidad; }
            set { this._prs_Cantidad = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Uni_ID.
        ///     </para>
        ///     <para>
        ///         Unit measure Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int Uni_ID
        {
            get { return this._uni_ID; }
            set { this._uni_ID = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Uni_Nombre.
        ///     </para>
        ///     <para>
        ///         Name of the Unit.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Uni_Nombre
        {
            get { return this._uni_Nombre; }
            set { this._uni_Nombre = value; }
        }

        #endregion

        private int _pro_ID;
        private int _sus_ID;
        private string _sus_Nombre;
        private string _prs_Cantidad;
        private int _uni_ID;
        private string _uni_Nombre;




    }
}
