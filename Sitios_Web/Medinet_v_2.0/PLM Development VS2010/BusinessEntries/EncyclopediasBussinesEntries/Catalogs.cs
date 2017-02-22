using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which contains nomenclature used in the Web Service.
    /// </summary>
    [DataContract]
    public sealed class Catalogs
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Catalogs class. Not receive parameters.
        /// </summary>
        private Catalogs() { }

        #endregion

        /// <summary>
        ///     Enumeration which represents the values ​​for each data source.
        /// </summary>
        public enum EncyclopediaType : int
        {
            /// <summary>
            ///     value for the type of sickness encyclopedia
            /// </summary>
            ENFERMEDADES = 1,

            /// <summary>
            ///     value for the type of symptoms encyclopedia
            /// </summary>
            SINTOMAS = 2,

            /// <summary>
            ///    value for the type of surgical procedures encyclopedia
            /// </summary>
            PROCEDIMIENTOS_QUIRURGICOS = 3,

            /// <summary>
            ///     value for the type of diagnostic procedures encyclopedia
            /// </summary>
            PROCEDIMIENTOS_DIAGNOSTICOS = 4,
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each professional practice.
        /// </summary>
        public enum AttributeType : byte
        {
            /// <summary>
            ///     Value for  type attribute content.
            /// </summary>
            CONTENIDO = 1,

            /// <summary>
            ///     Value for  type attribute image.
            /// </summary>
            IMAGEN = 2
        }

    }
}
