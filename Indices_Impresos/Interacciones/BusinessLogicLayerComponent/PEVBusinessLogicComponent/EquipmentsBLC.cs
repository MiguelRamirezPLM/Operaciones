using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class EquipmentsBLC
    {

        #region Constructor

        private EquipmentsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves one Equipment by EquipmentId
        public PEVBusinessEntries.EquipmentInfo rocGetEquipment(string code, int equipmentId)
        {
            if (equipmentId < 1)
            {
                throw new ArgumentException("The Equipment does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.EquipmentsDALC.Instance.rocGetEquipment(equipmentId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Equipments
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipments(string code, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.EquipmentsDALC.Instance.rocGetEquipments(page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Equipments by FullText
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByFullText(string code, int page, int numberByPage, string fullText)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.EquipmentsDALC.Instance.rocGetEquipmentsByFullText(page, numberByPage, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Equipments by Letter
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByLetter(string code, int page, int numberByPage, string letter)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.EquipmentsDALC.Instance.rocGetEquipmentsByLetter(page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Equipments by Text
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByText(string code, int page, int numberByPage, string text)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.EquipmentsDALC.Instance.rocGetEquipmentsByText(page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly EquipmentsBLC Instance = new EquipmentsBLC();

    }
}
