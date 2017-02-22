using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Class;
using Web.Models.Sessions;

namespace Web.Models
{
    public class getData
    {
        Medinet db = new Medinet();

        public int getAlphabetId(string ProductName)
        {
            string letter = ProductName.Trim().Substring(0, 1);
            string[] num = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (int i = 0; i < num.Length; i++)
            {

            }

            var alphabet = db.Database.SqlQuery<Alphabet>("plm_spGetAlphabetByLetter @letter=" + letter + "")
                                                            .Select(x => x.AlphabetId).ToList();

            int AlphabetId = alphabet[0];


            return AlphabetId;
        }

        public int getLaboratoryId(int DivisionId)
        {
            var divisions = db.Database.SqlQuery<plm_spCRUDDivisions>("plm_spCRUDDivisions @divisionId = " + DivisionId + ", @CRUDType = 1")
                                                                .Select(x => x.LaboratoryId).ToList();

            int LaboratoryId = divisions[0];

            return LaboratoryId;
        }

        public List<PhysiologicalContraindications> GetPhysiologicalContraindicationsAsoc(int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int ActiveSubstanceId, int EditionId)
        {
            List<PhysiologicalContraindications> LS = new List<PhysiologicalContraindications>();

            LS = db.Database.SqlQuery<PhysiologicalContraindications>("plm_spGetPhysiologicalContraindicationsByProduct @editionId =" + EditionId + "" +
                                                                       ", @divisionId =" + DivisionId + "" +
                                                                       ", @categoryId =" + CategoryId + "" +
                                                                       ", @productId = " + ProductId + "" +
                                                                       ", @pharmaFormId = " + PharmaFormId + "" +
                                                                       ", @activeSubstanceId=" + ActiveSubstanceId + "").ToList();
            return LS;
        }

        public List<GetRowsPerPage> GetRowsPerPage(GetInteractions Model)
        {
            List<GetRowsPerPage> LS = new List<GetRowsPerPage>();
            GetRowsPerPage GetRowsPerPage = new Class.GetRowsPerPage();

            Pager SPager = (Pager)HttpContext.Current.Session["Pager"];

            int Count = 10;

            if (SPager != null)
            {
                int? Pager = SPager.ActiveSubstancesWithoutInteractionsPageId;
                int? PagerPG = SPager.PharmacologicalGroupsWithoutInteractionId;
                int? PagerOE = SPager.OtherElemensWithoutInteractionsId;

                if (Pager == 1)
                {
                    Count = Convert.ToInt32(Model.ActiveSubstancesWithoutInteractions.LongCount());

                    GetRowsPerPage.Pager = Count;

                }
                else if ((Pager != 0) && (Pager != 1) && (Pager != null))
                {
                    Count = Convert.ToInt32(Pager);

                    GetRowsPerPage.Pager = Count;
                }

                if (PagerPG == 1)
                {
                    Count = Convert.ToInt32(Model.PharmacologicalGroupsWithoutInteraction.LongCount());

                    GetRowsPerPage.PagerPG = Count;
                }
                else if ((PagerPG != 0) && (PagerPG != 1) && (PagerPG != null))
                {
                    Count = Convert.ToInt32(PagerPG);

                    GetRowsPerPage.PagerPG = Count;
                }

                if (PagerOE == 1)
                {
                    Count = Convert.ToInt32(Model.OtherElemensWithoutInteractions.LongCount());

                    GetRowsPerPage.PagerOE = Count;
                }
                else if ((PagerOE != 0) && (PagerOE != 1) && (PagerOE != null))
                {
                    Count = Convert.ToInt32(PagerOE);

                    GetRowsPerPage.PagerOE = Count;
                }
            }
            else
            {
                GetRowsPerPage.Pager = 10;
                GetRowsPerPage.PagerPG = 10;
                GetRowsPerPage.PagerOE = 10;
            }

            LS.Add(GetRowsPerPage);

            return LS;
        }

        public static String ReplaceHTMLCodes(string Interaction)
        {

            Interaction = Interaction.Replace("&aacute;","á");
            Interaction = Interaction.Replace("&eacute;", "é");
            Interaction = Interaction.Replace("&iacute;", "í");
            Interaction = Interaction.Replace("&oacute;", "ó");
            Interaction = Interaction.Replace("&uacute;", "ú");

            Interaction = Interaction.Replace("&Aacute;", "Á");
            Interaction = Interaction.Replace("&Eacute;", "É");
            Interaction = Interaction.Replace("&Iacute;", "Í");
            Interaction = Interaction.Replace("&Oacute;", "Ó");
            Interaction = Interaction.Replace("&Uacute;", "Ú");

            Interaction = Interaction.Replace("&#9;", "");
            Interaction = Interaction.Replace("\r", "");
            Interaction = Interaction.Replace("\t", "");

            Interaction = Interaction.Replace("\n", "\n \n");

            Interaction = Interaction.Replace("&#174;", " ®");

            return Interaction;
        }

        public List<GetRowsPerPage> GetRowsPerPageIndications(GetIndications Model)
        {
            List<GetRowsPerPage> LS = new List<GetRowsPerPage>();
            GetRowsPerPage GetRowsPerPage = new Class.GetRowsPerPage();

            PagerIndications SPager = (PagerIndications)HttpContext.Current.Session["PagerIndications"];

            int Count = 10;

            if (SPager != null)
            {
                int? Pager = SPager.ActiveSubstancesWithoutInteractionsPageId;

                if (Pager == 1)
                {
                    Count = Convert.ToInt32(Model.GetIndicationssWithoutProduct.LongCount());

                    GetRowsPerPage.Pager = Count;

                }
                else if ((Pager != 0) && (Pager != 1) && (Pager != null))
                {
                    Count = Convert.ToInt32(Pager);

                    GetRowsPerPage.Pager = Count;
                }
            }
            else
            {
                GetRowsPerPage.Pager = 10;
            }

            LS.Add(GetRowsPerPage);

            return LS;
        }

        public List<GetRowsPerPage> GetRowsPerPageContraindications(GetContraindications Model)
        {
            List<GetRowsPerPage> LS = new List<GetRowsPerPage>();
            GetRowsPerPage GetRowsPerPage = new Class.GetRowsPerPage();

            Pager SPager = (Pager)HttpContext.Current.Session["Pager"];

            int Count = 10;

            if (SPager != null)
            {
                int? Pager = SPager.ActiveSubstancesWithoutInteractionsPageId;
                int? PagerPG = SPager.PharmacologicalGroupsWithoutInteractionId;
                int? PagerOE = SPager.OtherElemensWithoutInteractionsId;

                if (Pager == 1)
                {
                    Count = Convert.ToInt32(Model.GetActiveSubstancesWithoutInteractions.LongCount());

                    GetRowsPerPage.Pager = Count;

                }
                else if ((Pager != 0) && (Pager != 1) && (Pager != null))
                {
                    Count = Convert.ToInt32(Pager);

                    GetRowsPerPage.Pager = Count;
                }

                if (PagerPG == 1)
                {
                    Count = Convert.ToInt32(Model.GetPharmacologicalGroupsWithoutInteraction.LongCount());

                    GetRowsPerPage.PagerPG = Count;
                }
                else if ((PagerPG != 0) && (PagerPG != 1) && (PagerPG != null))
                {
                    Count = Convert.ToInt32(PagerPG);

                    GetRowsPerPage.PagerPG = Count;
                }

                if (PagerOE == 1)
                {
                    Count = Convert.ToInt32(Model.GetOtherElemensWithoutInteractions.LongCount());

                    GetRowsPerPage.PagerOE = Count;
                }
                else if ((PagerOE != 0) && (PagerOE != 1) && (PagerOE != null))
                {
                    Count = Convert.ToInt32(PagerOE);

                    GetRowsPerPage.PagerOE = Count;
                }
            }
            else
            {
                GetRowsPerPage.Pager = 10;
                GetRowsPerPage.PagerPG = 10;
                GetRowsPerPage.PagerOE = 10;
            }

            LS.Add(GetRowsPerPage);

            return LS;
        }
    }
}