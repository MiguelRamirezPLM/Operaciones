using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InfoLogsBusinessEntries
{
    /// <summary>
    ///     Class which contains nomenclature used in the Web Service.
    /// </summary>
    [DataContract]
    public class Catalogs
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Catalogs class. Not receives parameters.
        /// </summary>
        public Catalogs() { }

        #endregion

        #region Enums

        /// <summary>
        ///     Enumeration which represents the values ​​for each search source.
        /// </summary>
        public enum Info_Sources : byte
        {
            /// <summary>
            ///     Value one which represents web site.
            /// </summary>
            Web = 1,

            /// <summary>
            ///     Value two which represents android devices.
            /// </summary>
            Android = 2,

            /// <summary>
            ///     Value three which represents iPhone devices.
            /// </summary>
            iOS_iPhone = 3,

            /// <summary>
            ///     Value four which represents iPad devices.
            /// </summary>
            iOS_iPad = 4,

            /// <summary>
            ///     Value five which represents web services.
            /// </summary>
            Servicio_Web = 5,

            /// <summary>
            ///     Value six which represents client-server application.
            /// </summary>
            Cliente_Servidor = 6

        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each search type.
        /// </summary>
        public enum Info_SearchTypes : byte
        {
            /// <summary>
            ///     Value one which represents a search with parameters.
            /// </summary>
            Parametrizada = 1,

            /// <summary>
            ///     Value two which represents a text search.
            /// </summary>
            Texto = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each search entity.
        /// </summary>
        public enum Info_Entities : int
        {

            /// <summary>
            ///     Value one which represents abstracts.
            /// </summary>
            Abstracts = 1,

            /// <summary>
            ///     Value two which represents banners.
            /// </summary>
            Banners = 2,

            /// <summary>
            ///     Value three which represents medical calculators.
            /// </summary>
            Calculadoras = 3,

            /// <summary>
            ///     Value four which represents events.
            /// </summary>
            Eventos_Congresos = 4,

            /// <summary>
            ///     Value five which represents news.
            /// </summary>
            Noticias = 5,

            /// <summary>
            ///     Value six which represents links of interest.
            /// </summary>
            Vinculos_Interes = 6,

            /// <summary>
            ///     Value seven which represents schemes.
            /// </summary>
            Esquemas = 7,

            /// <summary>
            ///     Value eight which represents scientific articles.
            /// </summary>
            Articulos_Cientificos = 8,

            /// <summary>
            ///     Value nine which represents questionnaires.
            /// </summary>
            Cuestionarios = 9,

            /// <summary>
            ///     Value ten which represents pharmacies.
            /// </summary>
            Farmacias = 10,

            /// <summary>
            ///     Value eleven which represents physical activity routines.
            /// </summary>
            Rutinas_Ejercicios = 11,

            /// <summary>
            ///     Value twelve which represents diets.
            /// </summary>
            Dietas = 12,

            /// <summary>
            ///     Value thirteen which represents Harvard news.
            /// </summary>
            Noticias_Harvard = 13
        }

        #endregion

    }
}
