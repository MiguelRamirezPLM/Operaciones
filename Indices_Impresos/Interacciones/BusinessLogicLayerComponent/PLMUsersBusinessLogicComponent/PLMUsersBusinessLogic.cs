using System;
using System.Collections.Generic;
using System.Text;

namespace PLMUsersBusinessLogicComponent
{
    public class PLMUsersBusinessLogic
    {
        public enum Actions : byte
        {
            Agregar = 1,
            Actualizar = 2,
            Consultar = 3,
            Eliminar = 4
        }

        public enum Tables : byte
        {
            Products = 1,
            EditionProductContents = 2,
            ParticipantProducts = 3,
            ProductCategories = 4,
            DivisionProducts = 5,
            IPPNumbers = 6,
            ProductTherapeutic = 7,
            ProductSubstances = 8,
            ProductIndications = 9,
            ProductPharmaForms = 10,
            Concentrations = 11,
            NewProducts = 12,
            ProductClasifications = 13,
            ProductVersionPages = 14,
            ProductDescriptions = 15,
            CountryProducts = 16,
            MentionatedProducts = 17,
            EditionDivisionProducts = 18,
            ActiveSubstances = 19,
            Indications = 20,
            EditionProductShots = 21,
            Divisions = 22,
            DivisionInformation = 23,
            Families = 24,
            FamilyPrefixes = 25,
            FamilyProducts = 26,
            FamilyProductShots = 27,
            ModifiedAttributeGroups = 28,
            Presentations = 29,
            ProductContentSubstances = 30,
            PharmaGroupInteractions = 31,
            ProductInteractions = 32,
            OtherInteractions = 33,
            EditionPresentations = 34,
            Symptoms = 35,
            DivisionCategories = 36,
            IPPAProductInteractions=37,
            ProductSubstanceInteractions=38,
            ProductOtherInteractions=39,
            ProductPharmaGroupInteractions=40,
            ProductTherapeuticOMS=41,
            CompanyClientBranches=41,
            Addresses=42,
            CompanyClients=43,
            DistributionPharmacies=44,
            Events=45,
            ProductICD=46,
            IPPAProductContraindications=47,
            ProductICDContraindications=48,
            ProductPharmacologicalContraindications=49,
            ProductHypersensibilityContraindications=50,
            ProductPharmaGroupContraindications=51,
            ProductSubstanceContraindications=52,
            ProductOtherContraindications=53,
            ProductCommentContraindications=54,
            ProductImages=55,
            ProductImageSizes=56,
            DivisionImages=57,
            DivisionImageSizes=58,

            /// <summary>
            /// DEAQ Tables
            /// </summary>
            Weeds=107,
            Cities = 108,
            CropPests = 109,
            DivisionInformationDEAQ = 110,
            CropDiseases = 111,
            MentionatedProductsDEAQ = 112,
            CropWeeds = 113,
            CropFightPests = 114,
            Crops = 59,
            ProductAgrochemicalUses = 60,
            CropFightDiseases = 61,
            ProductCategoriesDEAQ = 62,
            CropFightWeeds = 63,
            ProductCrops = 64,
            ProductPharmaFormsDEAQ = 65,
            EditionsDEAQ = 66,
            ProductSeeds = 67,
            ProductSubstancesDEAQ = 68,
            ProductTypes = 69,
            Images = 70,
            FightTypes = 71,
            ProductImagesDEAQ = 72,
            Laboratories = 73,
            PharmaForms = 74,
            Books = 75,
            WeedTypes = 76,
            Seeds = 77,
            Types = 78,
            Countries = 79,
            ProductsDEAQ = 80,
            ActiveSubstancesDEAQ = 81,
            EditionDivisionProductsDEAQ = 82,
            Advertisements = 83,
            EditionDivisionAds = 84,
            ParticipantProductsDEAQ = 85,
            ImageTypes = 86,
            DivisionsDEAQ = 87,
           
            DivisionImagesDEAQ = 88,
            ProductContents = 89,
            Attributes = 90,
            
            EditionProductShotsDEAQ = 91,
            ProductShotTypes = 92,
            Carousel = 93,
            CarouselProductShots = 94,
            DivisionCategoriesDEAQ = 95,
            EditionAttributes = 96,
            States = 97,
            DivisionCities = 98,
            StateCrops = 99,
            AgrochemicalUses = 100,
            Pests = 101,
            AttributeGroup = 102,
            Categories = 103,
            Diseases = 104,
            EditionAttributeGroup = 105,
            NewProductsDEAQ=106
        
        }

        public enum Countries : byte
        { 
            Todos = 0,
            Belice = 2,
            Centroamérica= 3,
            Colombia = 4,
            CostaRica = 5,
            Ecuador = 6,
            ElSalvador = 7,
            Guatemala = 8,
            Haití = 9,
            Honduras = 10,
            México = 11,
            Nicaragua = 12,
            Panamá = 13,
            Perú = 14,
            RepúblicaDominicana = 15,
            Venezuela = 16
        }

        public enum PrefixTypes : byte
        {
            Contenido = 1,
            ProductShot = 2
        }
    }
}
