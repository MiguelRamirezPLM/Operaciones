using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PSELogsBusinessEntities
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

        #region Enum

        /// <summary>
        ///     Enumeration which represents the values ​​for each search source.
        /// </summary>
        public enum Sources : byte
        {
            /// <summary>
            ///     Value one which represents web site.
            /// </summary>
            Portal = 1,

            /// <summary>
            ///     Value two which represents mobile service.
            /// </summary>
            Servicio_Móvil = 2,

            /// <summary>
            ///     Value three which represents web service.
            /// </summary>
            Servicio_Web = 3,

            /// <summary>
            ///     Value four which represents client-server application.
            /// </summary>
            Cliente_Servidor = 4,

             /// <summary>
            ///     Value four which represents server-server application.
            /// </summary>
            Servidor_Servidor = 5
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each search type.
        /// </summary>
        public enum SearchTypes : byte
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
        public enum Entities : int
        {
            /// <summary>
            ///     Value one which represents a medical indications search.
            /// </summary>
            Indicaciones = 1,

            /// <summary>
            ///     Value two which represents a laboratory search.
            /// </summary>
            Laboratorios = 2,

            /// <summary>
            ///     Value three which represents a drug search.
            /// </summary>
            Medicamentos = 3,

            /// <summary>
            ///     Value four which represents a active substance search.
            /// </summary>
            Sustancias = 4,

            /// <summary>
            ///     Value five which represents a therapeutic indication search.
            /// </summary>
            Terapéutico = 5,

            /// <summary>
            ///     Value six which represents a search of drugs with an only substance by therapeutic indication and active substance.
            /// </summary>
            Med_Monoing_ATC_Sust = 6,

            /// <summary>
            ///     Value seven which represents a search of drugs with an only substance by laboratory and active substance.
            /// </summary>
            Med_Monoing_Lab_Sust = 7,

            /// <summary>
            ///     Value eight which represents a search of drugs with an only substance by active substance.
            /// </summary>
            Med_Monoing_Sust = 8,

            /// <summary>
            ///     Value nine which represents a search of drugs with substance multiple by therapeutic indication and active substance.
            /// </summary>
            Med_Multiing_ATC_Sust = 9,

            /// <summary>
            ///     Value ten which represents a search of drugs with substance multiple by laboratory and active substance.
            /// </summary>
            Med_Multiing_Lab_Sust = 10,

            /// <summary>
            ///     Value eleven which represents a search of drugs with substance multiple by active substance.
            /// </summary>
            Med_Multiing_Sust = 11,

            /// <summary>
            ///     Value twelve which represents a search of drugs by therapeutic indication.
            /// </summary>
            Med_ATC = 12,

            /// <summary>
            ///     Value thirteen which represents a search of drugs by medical indication.
            /// </summary>
            Med_Ind = 13,

            /// <summary>
            ///     Value fourteen which represents a search of drugs by laboratory.
            /// </summary>
            Med_Lab = 14,

            /// <summary>
            ///     Value fifteen which represents a search of drugs by medical indication and laboratory.
            /// </summary>
            Med_Lab_Ind = 15,

            /// <summary>
            ///     Value sixteen which represents a search of drugs by laboratory and active substance.
            /// </summary>
            Med_Lab_Sust = 16,

            /// <summary>
            ///     Value seventeen which represents a search of drugs by active substance.
            /// </summary>
            Med_Sust = 17,

            /// <summary>
            ///     Value eighteen which represents a search of laboratories by medical indication.
            /// </summary>
            Lab_Ind = 18,

            /// <summary>
            ///     Value nineteen which represents a search of laboratories by active substance.
            /// </summary>
            Lab_Sust = 19,

            /// <summary>
            ///     Value twenty which represents a search of active substances by therapeutic indication.
            /// </summary>
            Sust_ATC = 20,

            /// <summary>
            ///     Value twenty-one which represents a search of active substances by laboratory.
            /// </summary>
            Sust_Lab = 21,

            /// <summary>
            ///     Value twenty-two which represents a search of active substances by drug.
            /// </summary>
            Sust_Med = 22,

            /// <summary>
            ///     Value twenty-three which represents a search of active substances by drug and active substance.
            /// </summary>
            Sust_Med_Sust = 23,

            /// <summary>
            ///     Value twenty-four which represents a search of medical indications by drug.
            /// </summary>
            Ind_Med = 24,

            /// <summary>
            ///     Value twenty-five which represents a search of therapeutic indications by drug.
            /// </summary>
            ATC_Med = 25,

            /// <summary>
            ///     Value twenty-six which represents a query of product content.
            /// </summary>
            Contenido = 26,

            /// <summary>
            ///     Value twenty-seven which represents a query of attribute content.
            /// </summary>
            Contenido_Atributo = 27,

            /// <summary>
            ///     Value twenty-eight which represents a query of symptom content.
            /// </summary>
            Sintomas = 28,

            /// <summary>
            ///     Value twenty-nine which represents a search of drugs by medical symptom.
            /// </summary>
            Medicamentos_por_Sintoma = 29,

            /// <summary>
            ///     Value thirty which represents a search of drugs by text.
            /// </summary>
            Busqueda_Texto = 30,

            /// <summary>
            ///     Value thirty-one which represents a search of drugs by medical prescription.
            /// </summary>
            Prescripcion = 31,

            /// <summary>
            ///     Value thirty-two which represents a search without results.
            /// </summary>
            Busqueda_no_encontrada = 32,

            /// <summary>
            ///     Value thirty-three which represents a search of medical indications by laboratory.
            /// </summary>
            Indicaciones_por_Laboratorio = 33,

            /// <summary>
            ///     Value thirty-four which represents a search of medical presentations by Producto.
            /// </summary>
            Presentaciones_por_Producto = 34,

            /// <summary>
            ///     Value thirty-seven  which represents a ICD search.
            /// </summary>
            ICD = 37,

            /// <summary>
            ///     Value thirty-eight which represents a search of drugs by ICD.
            /// </summary>
            Med_ICD = 38,

            /// <summary>
            ///     Value thirty-nine which represents a search of ICD by drugs.
            /// </summary>
            ICD_Med = 39,
            /// <summary>
            ///     Value thirty-nine which represents a search of Seed.
            /// </summary>
            Semilla = 49,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Category and Division.
            /// </summary>
            Categoria = 50,
            /// <summary>
            ///     Value thirty-nine which represents a search of ICD Products.
            /// </summary>
            Usos_Agroquimicos = 51,
            /// <summary>
            ///     Value thirty-nine which represents a search of Crops.
            /// </summary>
            Productos = 52,
            /// <summary>
            ///     Value thirty-nine which represents a search of  Crops.
            /// </summary>
            Cultivos = 53,
            /// <summary>
            ///     Value thirty-nine which represents a search of Substance by Product.
            /// </summary>
            Producto_Semilla = 54,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Category.
            /// </summary>
            Sustancia_Producto = 55,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Use.
            /// </summary>
            Producto_Categoria = 56,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Crop.
            /// </summary>
            Producto_Uso = 57,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Substance.
            /// </summary>
            Producto_Cultivo = 58,
            /// <summary>
            ///     Value thirty-nine which represents a search of AgrochemicalUse.
            /// </summary>
            Producto_Sustancia = 59,
            /// <summary>
            ///     Value thirty-nine which represents a search of Product by Seed.
            /// </summary>
            Producto_Categoria_Laboratorio = 60
         


        }

        #endregion

    }
}
