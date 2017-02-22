using System;
using System.Collections.Generic;
using System.Text;

namespace HarvardContentBusinessEntries
{
    /// <summary>
    ///     Class which represents enumerations that are used in the web service.
    /// </summary>
    public class NumberingInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the NumberingInfo class. Not receive parameters.
        /// </summary>
        public NumberingInfo() { }
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Ages.
        ///     </para>
        /// </summary>
        public enum Ages
        {
            /// <summary>
            ///     <para>
            ///         Value for Infant.
            ///     </para>
            /// </summary>
            Infant,

            /// <summary>
            ///     <para>
            ///         Value for Child.
            ///     </para>
            /// </summary>
            Child,

            /// <summary>
            ///     <para>
            ///         Value for Teen.
            ///     </para>
            /// </summary>
            Teen,

            /// <summary>
            ///     <para>
            ///         Value for Adult.
            ///     </para>
            /// </summary>
            Adult,

            /// <summary>
            ///     <para>
            ///         Value for GrownUP.
            ///     </para>
            /// </summary>
            GrownUP
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Genders.
        ///     </para>
        /// </summary>
        public enum Genders
        {
            /// <summary>
            ///     <para>
            ///         Value for Male.
            ///     </para>
            /// </summary>
            Male,

            /// <summary>
            ///     <para>
            ///         Value for Female.
            ///     </para>
            /// </summary>
            Female,

            /// <summary>
            ///     <para>
            ///         Value for All.
            ///     </para>
            /// </summary>
            All
        }

        #endregion

    }
}
