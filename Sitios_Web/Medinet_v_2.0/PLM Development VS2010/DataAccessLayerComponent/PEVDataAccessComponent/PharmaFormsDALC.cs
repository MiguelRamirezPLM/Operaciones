using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class PharmaFormsDALC
    {

        #region Constructors

        private PharmaFormsDALC() { }

        #endregion


        //Retrieves all pharmaforms by product
        public List<PharmaFormsByProductInfo> rocGetPharmaFormsByProduct(int productId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var pharmaformRow = from pharmafRow in db.roc_spGetPharmaFomsByProduct(productId)
                                select new PharmaFormsByProductInfo
                                {

                                    PharmaFormId = pharmafRow.PharmaFormId,
                                    PharmaForm = pharmafRow.PharmaForm,
                                    activo = pharmafRow.Active
                                };
            List<PharmaFormsByProductInfo> pharmaform = pharmaformRow.ToList();

            return pharmaform.Count() > 0 ? pharmaform : null;

        }


        public static readonly PharmaFormsDALC Instance = new PharmaFormsDALC();
    }
}
