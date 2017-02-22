using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Sessions
{
    public class Pager
    {
        #region Constructor

        public Pager(int ActiveSubstancesWithoutInteractionsPageId, int PharmacologicalGroupsWithoutInteractionId, int OtherElemensWithoutInteractionsId)
        {
            PageActiveSubstancesWithoutInteractions = ActiveSubstancesWithoutInteractionsPageId;
            PagePharmacologicalGroupsWithoutInteraction = PharmacologicalGroupsWithoutInteractionId;
            PageOtherElemensWithoutInteractions = OtherElemensWithoutInteractionsId;
        }

        #endregion

        #region Member

        public int PageActiveSubstancesWithoutInteractions;
        public int PagePharmacologicalGroupsWithoutInteraction;
        public int PageOtherElemensWithoutInteractions;

        #endregion

        #region Properties

        public int ActiveSubstancesWithoutInteractionsPageId
        {
            get
            {
                return PageActiveSubstancesWithoutInteractions;
            }
            set
            {
                PageActiveSubstancesWithoutInteractions = value;
            }
        }

        public int PharmacologicalGroupsWithoutInteractionId
        {
            get
            {
                return PagePharmacologicalGroupsWithoutInteraction;
            }
            set
            {
                PagePharmacologicalGroupsWithoutInteraction = value;
            }
        }

        public int OtherElemensWithoutInteractionsId
        {
            get
            {
                return PageOtherElemensWithoutInteractions;
            }
            set
            {
                PageOtherElemensWithoutInteractions = value;
            }
        }


        #endregion
    }

    public class PagerIndications
    {
        #region Constructor

        public PagerIndications(int IndicationssWithoutProductPageId)
        {
            IndicationssWithoutProductPage = IndicationssWithoutProductPageId;
        }

        #endregion

        #region Member

        public int IndicationssWithoutProductPage;

        #endregion

        #region Properties

        public int ActiveSubstancesWithoutInteractionsPageId
        {
            get
            {
                return IndicationssWithoutProductPage;
            }
            set
            {
                IndicationssWithoutProductPage = value;
            }
        }

        #endregion
    }
}