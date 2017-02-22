using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class InteractionsBLC
    {

        #region Constructors

        private InteractionsBLC() { }

        #endregion

        #region Public Methods

        #region Products

        public void getSubstancesInteractionsByProducts(List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> lista, string guid)
        {
            foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo product in lista)
            {
                MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstanceInteractionsByProduct(product.CategotyId, product.PharmaFormId, product.ProductId, product.DivisionId, guid);
                MedinetDataAccessComponent.InteractionsDALC.Instance.getGroupInteractionsByProduct(product.CategotyId, product.PharmaFormId, product.ProductId, product.DivisionId, guid);
                MedinetDataAccessComponent.InteractionsDALC.Instance.getOtherInteractionsByProduct(product.CategotyId, product.PharmaFormId, product.ProductId, product.DivisionId, guid);
            }
        }

        public List<MedinetBusinessEntries.ProductInteractionsDetailInfo> getInteractionsByProducts(List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> products, int countryId)
        {
            string guid = Guid.NewGuid().ToString();

            getSubstancesInteractionsByProducts(products, guid);

            List<MedinetBusinessEntries.ProductInteractionsDetailInfo> productsInteractions =
                MedinetDataAccessComponent.InteractionsDALC.Instance.getProductByInteractions(guid);


            List<MedinetBusinessEntries.ProductInteractionsDetailInfo> final = new List<MedinetBusinessEntries.ProductInteractionsDetailInfo>();



            List<InteractionSubstanceProductsInfo> substancesInteractions =
                MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstanceInteractionByProducts(guid, countryId);

            List<InteractionGroupProductsInfo> groupsInteractions =
                MedinetDataAccessComponent.InteractionsDALC.Instance.getGroupInteractionsByProducts(guid);

            List<ProductOtherInteractionsInfo> otherInteractions =
                MedinetDataAccessComponent.InteractionsDALC.Instance.getOtherInteractionsByProducts(guid);

            List<InteractionSubstanceProductsInfo> ret = new List<InteractionSubstanceProductsInfo>();

            foreach (MedinetBusinessEntries.ProductInteractionsDetailInfo product in productsInteractions)
            {
                var subs = (from c in substancesInteractions
                            join p in products on c.IProductId equals p.ProductId
                            where c.DivisionId == product.DivisionId
                            && c.CategoryId == product.CategoryId
                            && c.PharmaFormId == product.PharmaFormId
                            && c.ProductId == product.ProductId
                            && c.IDivisionId == p.DivisionId
                            && c.ICategoryId == p.CategotyId
                            && c.IPharmaFormId == p.PharmaFormId
                            select c);
                product.InteractionSubstances = subs.ToList();

                var groups = (from c in groupsInteractions
                           where c.DivisionId == product.DivisionId
                           && c.CategoryId == product.CategoryId
                           && c.PharmaFormId == product.PharmaFormId
                           && c.ProductId == product.ProductId
                           select c);
                product.PharmacologicalGroups = groups.ToList();

                var others = (from c in otherInteractions
                              where c.DivisionId == product.DivisionId
                              && c.CategoryId == product.CategoryId
                              && c.PharmaFormId == product.PharmaFormId
                              && c.ProductId == product.ProductId
                              select c);
                product.OtherInteractions = others.ToList();

                if (product.InteractionSubstances.Count != 0 || product.PharmacologicalGroups.Count != 0 || product.OtherInteractions.Count != 0)
                {
                    final.Add(product);
                }
            }

            


            MedinetDataAccessComponent.InteractionsDALC.Instance.deleteInteractionsByProduct(guid);

            //return productsInteractions;
            return final;
        }

        public List<ProductPresentationsInfo> getSubstitutesByProducts(int activeSubstanceId, int countryId)
        {
            return MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstitutesByProducts(activeSubstanceId, countryId);
        }

        #endregion

        #region Subsances

        public void getSubstancesInteractionsBySubstances(List<ActiveSubstanceInfo> lista)
        {
            foreach (ActiveSubstanceInfo substance in lista)
            {
                MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstancesInteractionsBySubstances(substance.ActiveSubstanceId);
            }
        }
 
        public List<MedinetBusinessEntries.InteractionSubstanceProductsInfo> getInteractionsBySubstances(List<MedinetBusinessEntries.ActiveSubstanceInfo> substances)
        {
            getSubstancesInteractionsBySubstances(substances);

            List<MedinetBusinessEntries.InteractionSubstanceProductsInfo> substancesInteractions =
                MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstanceInteractionBySubstances();

            //foreach (InteractionSubstanceProductsInfo itemSust in substancesInteractions)
            //{
            //    List<ProductPresentationsInfo> substitutes =
            //    MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstitutesBySubstances(itemSust.ActiveSubstanceId);
            //    itemSust.Substitutes = substitutes;
            //}

            MedinetDataAccessComponent.InteractionsDALC.Instance.deleteInteractionsBySubstance();

            return substancesInteractions;
        }

        public List<ProductPresentationsInfo> getSubstitutesBySubstances(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.InteractionsDALC.Instance.getSubstitutesBySubstances(activeSubstanceId);
        }

        #endregion Subsances

        #endregion Public Methods

        public static readonly InteractionsBLC Instance = new InteractionsBLC();

    }
}