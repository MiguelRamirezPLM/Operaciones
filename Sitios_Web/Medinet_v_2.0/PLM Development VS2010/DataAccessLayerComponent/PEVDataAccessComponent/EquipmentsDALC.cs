using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class EquipmentsDALC
    {

        #region Constructor

        private EquipmentsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves one Equipment by EquipmentId
        public PEVBusinessEntries.EquipmentInfo rocGetEquipment(int equipmentId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var EquipmentInformation = from EquipmentInf in db.roc_spGetEquipment(equipmentId)
                                       select new PEVBusinessEntries.EquipmentInfo
                                       {
                                           EquipmentId = EquipmentInf.EquipmentId,
                                           EquipmentName = EquipmentInf.EquipmentName,
                                           Active = EquipmentInf.Active
                                       };

            List<PEVBusinessEntries.EquipmentInfo> Equipments = EquipmentInformation.ToList();

            return Equipments.Count() > 0 ? Equipments[0] : null;

        }

        //Retrieves all Equipments
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipments(int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var EquipmentInformation = from EquipmentInf in db.roc_spGetEquipments(page, numberByPage)
                                       select new PEVBusinessEntries.ROC.EquipmentByTextInfo
                                       {
                                           EquipmentId = EquipmentInf.EquipmentId,
                                           EquipmentName = EquipmentInf.EquipmentName,
                                           Active = EquipmentInf.Active,
                                           RowNumber = (int)EquipmentInf.RowNumber,
                                           Total = (int)EquipmentInf.TOTAL
                                       };

            List<PEVBusinessEntries.ROC.EquipmentByTextInfo> Equipments = EquipmentInformation.ToList();

            return Equipments.Count() > 0 ? Equipments : null;

        }

        //Retrieves all Equipments by FullText
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByFullText(int page, int numberByPage, string fullText)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var EquipmentInformation = from EquipmentInf in db.roc_spGetEquipmentsByFullText(page, numberByPage, fullText)
                                       select new PEVBusinessEntries.ROC.EquipmentByTextInfo
                                       {
                                           EquipmentId = EquipmentInf.EquipmentId,
                                           EquipmentName = EquipmentInf.EquipmentName,
                                           Active = EquipmentInf.Active,
                                           RowNumber = (int)EquipmentInf.RowNumber,
                                           Total = (int)EquipmentInf.TOTAL
                                       };

            List<PEVBusinessEntries.ROC.EquipmentByTextInfo> Equipments = EquipmentInformation.ToList();

            return Equipments.Count() > 0 ? Equipments : null;

        }

        //Retrieves all Equipments by Letter
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByLetter(int page, int numberByPage, string letter)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var EquipmentInformation = from EquipmentInf in db.roc_spGetEquipmentsByLetter(page, numberByPage, letter)
                                       select new PEVBusinessEntries.ROC.EquipmentByTextInfo
                                       {
                                           EquipmentId = EquipmentInf.EquipmentId,
                                           EquipmentName = EquipmentInf.EquipmentName,
                                           Active = EquipmentInf.Active,
                                           RowNumber = (int)EquipmentInf.RowNumber,
                                           Total = (int)EquipmentInf.TOTAL
                                       };

            List<PEVBusinessEntries.ROC.EquipmentByTextInfo> Equipments = EquipmentInformation.ToList();

            return Equipments.Count() > 0 ? Equipments : null;

        }

        //Retrieves all Equipments by Text
        public List<PEVBusinessEntries.ROC.EquipmentByTextInfo> rocGetEquipmentsByText(int page, int numberByPage, string text)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var EquipmentInformation = from EquipmentInf in db.roc_spGetEquipmentsByText(page, numberByPage, text)
                                       select new PEVBusinessEntries.ROC.EquipmentByTextInfo
                                       {
                                           EquipmentId = EquipmentInf.EquipmentId,
                                           EquipmentName = EquipmentInf.EquipmentName,
                                           Active = EquipmentInf.Active,
                                           RowNumber = (int)EquipmentInf.RowNumber,
                                           Total = (int)EquipmentInf.TOTAL
                                       };

            List<PEVBusinessEntries.ROC.EquipmentByTextInfo> Equipments = EquipmentInformation.ToList();

            return Equipments.Count() > 0 ? Equipments : null;

        }

        #endregion

        public static readonly EquipmentsDALC Instance = new EquipmentsDALC();

    }
}
