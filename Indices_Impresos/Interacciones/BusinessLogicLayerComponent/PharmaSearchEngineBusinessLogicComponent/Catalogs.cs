using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class Catalogs
    {
        
        #region Constructors

        private Catalogs() { }

        #endregion

        #region Enum

        public enum TypeInEdition : byte
        {
            Participante = 1,
            Mencionado = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​of targets.
        /// </summary>
        public enum Sources
        {
            /// <summary>
            ///     Value one which stands Client - Server.
            /// </summary>
            Cliente_Servidor = 1,

            /// <summary>
            ///     Value two which stands Website.
            /// </summary>
            Web = 2,

            /// <summary>
            ///     Value three which stands Android Devices.
            /// </summary>
            Android = 3,

            /// <summary>
            ///     Value four which stands BlackBerry Devices.
            /// </summary>
            BlackBerry = 4,

            /// <summary>
            ///     Value five which stands iPhone Devices.
            /// </summary>
            iOS_iPhone = 5,

            /// <summary>
            ///     Value six which stands iPad Devices.
            /// </summary>
            iOS_iPad = 6,

            /// <summary>
            ///     Value seven which stands server to server.
            /// </summary>
            Servidor_Servidor = 7,

            /// <summary>
            ///     Value eight which stands windows phone devices.
            /// </summary>
            Windows_Phone = 8,

            /// <summary>
            ///     Value eight which stands windows phone devices.
            /// </summary>
            Servicio_Web = 9

        }

        #endregion

        #region Publics Methods

       public byte getSourceIdByTargetName(byte targetId)
        {

            byte sourceId;

            switch ((PLMClientsBusinessEntities.Catalogs.TargetOutputs)targetId)
            {

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Android:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Móvil;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.BlackBerry:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Móvil;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.iOS_iPhone:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Móvil;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.iOS_iPad:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Móvil;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Windows_Phone:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Móvil;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Web:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Portal;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Cliente_Servidor:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Cliente_Servidor;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Servicio_Web:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servicio_Web;
                    break;

                case PLMClientsBusinessEntities.Catalogs.TargetOutputs.Servidor_Servidor:

                    sourceId = (byte)PSELogsBusinessEntities.Catalogs.Sources.Servidor_Servidor;
                    break;

                default:

                    throw new ApplicationException("El target no es válido o se encuentra inactivo.");
            }

            return sourceId;
        }

        #endregion

       public static readonly Catalogs Instance = new Catalogs();
    }
}
