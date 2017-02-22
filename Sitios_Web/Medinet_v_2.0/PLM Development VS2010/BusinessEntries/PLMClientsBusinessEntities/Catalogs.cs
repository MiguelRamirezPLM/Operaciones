using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which contains nomenclature used in the Web Service.
    /// </summary>
    [DataContract]
    public sealed class Catalogs
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Catalogs class. Not receive parameters.
        /// </summary>
        private Catalogs() { }

        #endregion

        #region Enums

        /// <summary>
        ///     Enumeration which represents the values ​​for each data source.
        /// </summary>
        public enum EntrySources : int
        {
            /// <summary>
            ///     Value one which represents Website.
            /// </summary>
            Portal_PLM_2010 = 1,

            /// <summary>
            ///     Value twenty-six which represents Conexifarma Company.
            /// </summary>
            CONEXIFARMA = 26,

            /// <summary>
            ///     Value twenty-seven which represents Mobile Devices.
            /// </summary>
            DISPOSITIVO_MOVIL = 27,

            /// <summary>
            ///     Value twenty-eight which represents Client-Server application.
            /// </summary>
            CLIENTE_SERVIDOR = 29,

            /// <summary>
            ///     Value thirty which represents site medicamentosplm.com.co.
            /// </summary>
            MEDICAMENTOSPLM_COM_CO = 30
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each professional practice.
        /// </summary>
        public enum ProfessionalPractices : byte
        {
            /// <summary>
            ///     Value one which stands for public practice.
            /// </summary>
            PUBLICA = 1,

            /// <summary>
            ///     Value two which stands for private practice.
            /// </summary>
            PRIVADA = 2
        }

        /// <summary>
        ///     Enumeration that represents the values ​​for each Client profession.
        /// </summary>
        public enum Professions : short
        {
            /// <summary>
            ///     Value one which stands for Agronomist.
            /// </summary>
            AGRONOMO = 1,

            /// <summary>
            ///     Value two which stands for Biologist.
            /// </summary>
            BIOLOGO = 2,

            /// <summary>
            ///     Value three which stands for Distributor.
            /// </summary>
            DISTRIBUIDOR = 3,

            /// <summary>
            ///     Value four which stands for Nurse.
            /// </summary>
            ENFERMERO = 4,

            /// <summary>
            ///     Value five which stands for food Engineer.
            /// </summary>
            INGENIERO_EN_ALIMENTOS = 5,

            /// <summary>
            ///     Value six which stands for Chemical Engineer.
            /// </summary>
            INGENIERO_QUIMICO = 6,

            /// <summary>
            ///     Value seven which stands for Doctor.
            /// </summary>
            MEDICO = 7,

            /// <summary>
            ///     Value eight which stands for Veterinarian.
            /// </summary>
            MEDICO_VETERINARIO = 8,

            /// <summary>
            ///     Value nine which stands for Odontologist.
            /// </summary>
            ODONTOLOGO = 9,

            /// <summary>
            ///     Value ten which stands for other Profession.
            /// </summary>
            OTRA = 10,

            /// <summary>
            ///     Value eleven which stands for paramedic.
            /// </summary>
            PARAMEDICO = 11,

            /// <summary>
            ///     Value twelve which stands for Psychologist.
            /// </summary>
            PSICOLOGO = 12,

            /// <summary>
            ///     Value thirteen which stands for chemical Biologist.
            /// </summary>
            QUIMICO_BIOLOGO_PARASITOLOGO = 13,

            /// <summary>
            ///     Value fourteen which stands for Chemist.
            /// </summary>
            QUIMICO_FARMACOBIOLOGO = 14,

            /// <summary>
            ///     Value fifteen which stands for Laboratorian.
            /// </summary>
            TECNICO_LABORATORISTA = 15,

            /// <summary>
            ///     Value sixteen which stands for Radiographer.
            /// </summary>
            TECNICO_RADIOLOGO = 16,

            /// <summary>
            ///     Value seventeen which stands for Association.
            /// </summary>
            ASOCIACION = 17,

            /// <summary>
            ///     Value eighteen which stands for veterinary pharmacy.
            /// </summary>
            FARMACIA_VETERINARIA = 18,

            /// <summary>
            ///     Value nineteen which stands for Researcher.
            /// </summary>
            INVESTIGADOR = 19,

            /// <summary>
            ///     Value twenty which stands for Producer.
            /// </summary>
            PRODUCTOR = 20,

            /// <summary>
            ///     Value twenty-one which stands for University.
            /// </summary>
            UNIVERSIDAD = 21,

            /// <summary>
            ///     Value twenty-two which stands for health Professional.
            /// </summary>
            PROFESIONAL_DE_LA_SALUD = 22,

            /// <summary>
            ///     Value twenty-three which stands for Patient.
            /// </summary>
            PACIENTE = 23,

            /// <summary>
            ///     Value twenty-four which stands for Student.
            /// </summary>
            ESTUDIANTE = 24,

            /// <summary>
            ///     Value twenty-five which stands for pet owner.
            /// </summary>
            DUENO_DE_MASCOTA = 25,

            /// <summary>
            ///     Value twenty-six which stands for pharmacy owner.
            /// </summary>
            DUENO_FARMACIA_VETERINARIA = 26,

            /// <summary>
            ///     Value twenty-seven which stands for clinic owner.
            /// </summary>
            DUENO_CLINICA_VETERINARIA = 27,

            /// <summary>
            ///     Value twenty-eight which stands for pharmacist.
            /// </summary>
            FARMACEUTA = 28,

            /// <summary>
            ///     Value twenty-nine which stands for pharmaceutical industry.
            /// </summary>
            PERSONAL_INDUSTRIA_FARMACEUTICA = 29,

            /// <summary>
            ///     Value thirty which stands for therapist.
            /// </summary>
            TERAPEUTA = 30,

            /// <summary>
            ///     Value thirty-one which stands for medical agent.
            /// </summary>
            REPRESENTANTE_MEDICO = 31

        }

        /// <summary>
        ///     Enumeration that represents the values ​​for each Client speciality.
        /// </summary>
        public enum Specialities : short
        {

            /// <summary>
            ///     Value zero which represents null.
            /// </summary>
            VALOR_NULO = 0,

            /// <summary>
            ///     Value thirty-two which stands for Allergology.
            /// </summary>
            ALERGOLOGIA = 32,

            /// <summary>
            ///     Value thirty-three which stands for Andrology.
            /// </summary>
            ANDROLOGIA = 33,

            /// <summary>
            ///     Value thirty-four which stands for Anesthesiology.
            /// </summary>
            ANESTESIOLOGIA = 34,

            /// <summary>
            ///     Value thirty-five which stands for Angiology.
            /// </summary>
            ANGIOLOGIA = 35,

            /// <summary>
            ///     Value thirty-six which stands for Bariatrics.
            /// </summary>
            BARIATRIA = 36,

            /// <summary>
            ///     Value thirty-seven which stands for Cardiology.
            /// </summary>
            CARDIOLOGIA = 37,

            /// <summary>
            ///     Value thirty-eight which stands for Surgery.
            /// </summary>
            CIRUGIA = 38,

            /// <summary>
            ///     Value thirty-nine which stands for Coloproctology.
            /// </summary>
            COLOPROCTOLOGIA = 39,

            /// <summary>
            ///     Value forty which stands for Coloproctology.
            /// </summary>
            DERMATOLOGIA = 40,

            /// <summary>
            ///     Value forty-one which stands for Dermatology.
            /// </summary>
            DIABETOLOGIA = 41,

            /// <summary>
            ///     Value forty-two which stands for Endocrinology.
            /// </summary>
            ENDOCRINOLOGIA = 42,

            /// <summary>
            ///     Value forty-three which stands for Endoscopy.
            /// </summary>
            ENDOSCOPIA = 43,

            /// <summary>
            ///     Value forty-four which stands for Epidemiology.
            /// </summary>
            EPIDEMIOLOGIA = 44,

            /// <summary>
            ///     Value forty-five which stands for Stomatology.
            /// </summary>
            ESTOMATOLOGIA = 45,

            /// <summary>
            ///     Value forty-six which stands for Pharmacology.
            /// </summary>
            FARMACOLOGIA = 46,

            /// <summary>
            ///     Value forty-seven which stands for Gastroenterology.
            /// </summary>
            GASTROENTEROLOGIA = 47,

            /// <summary>
            ///     Value forty-eight which stands for Gerontology.
            /// </summary>
            GERONTOLOGIA = 48,

            /// <summary>
            ///     Value forty-nine which stands for Geriatrics.
            /// </summary>
            GERIATRIA = 49,

            /// <summary>
            ///     Value fifty which stands for Gynecology.
            /// </summary>
            GINECOLOGIA = 50,

            /// <summary>
            ///     Value fifty-one which stands for Hematology.
            /// </summary>
            HEMATOLOGIA = 51,

            /// <summary>
            ///     Value fifty-two which stands for Infectious.
            /// </summary>
            INFECTOLOGIA = 52,

            /// <summary>
            ///     Value fifty-three which stands for Immunology.
            /// </summary>
            INMUNOLOGIA = 53,

            /// <summary>
            ///     Value fifty-four which stands for critical care Medicine.
            /// </summary>
            MEDICINA_CRITICA = 54,

            /// <summary>
            ///     Value fifty-five which stands for rehabilitation Medicine.
            /// </summary>
            MEDICINA_DE_REHABILITACION = 55,

            /// <summary>
            ///     Value fifty-six which stands for sports Medicine.
            /// </summary>
            MEDICINA_DEL_DEPORTE = 56,

            /// <summary>
            ///     Value fifty-seven which stands for sports family Medicine.
            /// </summary>
            MEDICINA_FAMILIAR = 57,

            /// <summary>
            ///     Value fifty-eight which stands for general Medicine.
            /// </summary>
            MEDICINA_GENERAL = 58,

            /// <summary>
            ///     Value fifty-nine which stands for internal Medicine.
            /// </summary>
            MEDICINA_INTERNA = 59,

            /// <summary>
            ///     Value sixty which stands for nuclear Medicine.
            /// </summary>
            MEDICINA_NUCLEAR = 60,

            /// <summary>
            ///     Value sixty-one which stands for Nephrology.
            /// </summary>
            NEFROLOGIA = 61,

            /// <summary>
            ///     Value sixty-two which stands for Neonatology.
            /// </summary>
            NEONATOLOGIA = 62,

            /// <summary>
            ///     Value sixty-three which stands for Pulmonology.
            /// </summary>
            NEUMOLOGIA = 63,

            /// <summary>
            ///     Value sixty-four which stands for Neurology.
            /// </summary>
            NEUROLOGIA = 64,

            /// <summary>
            ///     Value sixty-five which stands for Nutriology.
            /// </summary>
            NUTRIOLOGIA = 65,

            /// <summary>
            ///     Value sixty-six which stands for Ophthalmology.
            /// </summary>
            OFTALMOLOGIA = 66,

            /// <summary>
            ///     Value sixty-seven which stands for Oncology.
            /// </summary>
            ONCOLOGIA = 67,

            /// <summary>
            ///     Value sixty-eight which stands for Orthopedics.
            /// </summary>
            ORTOPEDIA = 68,

            /// <summary>
            ///     Value sixty-nine which stands for Otolaryngology.
            /// </summary>
            OTORRINOLARINGOLOGIA = 69,

            /// <summary>
            ///     Value seventy which stands for Patology.
            /// </summary>
            PATOLOGIA = 70,

            /// <summary>
            ///     Value seventy-one which stands for Pediatrics.
            /// </summary>
            PEDIATRIA = 71,

            /// <summary>
            ///     Value seventy-two which stands for Psychiatry.
            /// </summary>
            PSIQUIATRIA = 72,

            /// <summary>
            ///     Value seventy-three which stands for Radiology.
            /// </summary>
            RADIOLOGIA = 73,

            /// <summary>
            ///     Value seventy-four which stands for Rheumatology.
            /// </summary>
            REUMATOLOGIA = 74,

            /// <summary>
            ///     Value seventy-five which stands for Traumatologist.
            /// </summary>
            TRAUMATOLOGIA = 75,

            /// <summary>
            ///     Value seventy-six which stands for Urology.
            /// </summary>
            UROLOGIA = 76,

            /// <summary>
            ///     Value seventy-seven which stands for intesive Medicine.
            /// </summary>
            MEDICINA_INTENSIVA = 77,

            /// <summary>
            ///     Value seventy-eight which stands for emergency Medicine.
            /// </summary>
            MEDICINA_DE_URGENCIAS = 78,

            /// <summary>
            ///     Value seventy-nine which stands for other Speciality.
            /// </summary>
            OTRA = 79,

            /// <summary>
            ///     Value eighty which stands for clinical analysis.
            /// </summary>
            ANALISIS_CLINICOS = 80,

            /// <summary>
            ///     Value eighty-one which stands for Pathology.
            /// </summary>
            ANATOMIA_PATOLOGICA = 81,

            /// <summary>
            ///     Value eighty-two which stands for Bacteriology.
            /// </summary>
            BACTERIOLOGIA = 82,

            /// <summary>
            ///     Value eighty-three which stands for ultrasound scan.
            /// </summary>
            ECOGRAFIA = 83,

            /// <summary>
            ///     Value eighty-four which stands for Ethology.
            /// </summary>
            ETOLOGIA = 84,

            /// <summary>
            ///     Value eighty-five which stands for Genetics.
            /// </summary>
            GENETICA = 85,

            /// <summary>
            ///     Value eighty-six which stands for Obstetrics.
            /// </summary>
            OBSTETRICIA = 86,

            /// <summary>
            ///     Value eighty-seven which stands for Odontology.
            /// </summary>
            ODONTOLOGIA = 87,

            /// <summary>
            ///     Value eighty-eight which stands for Parasitology.
            /// </summary>
            PARASITOLOGIA = 88,

            /// <summary>
            ///     Value eighty-nine which stands for animal reproduction.
            /// </summary>
            REPRODUCCION_ANIMAL = 89,

            /// <summary>
            ///     Value ninety which stands for public Health.
            /// </summary>
            SALUD_PUBLICA_Y_CONTROL_ALIMENTARIO = 90,

            /// <summary>
            ///     Value ninety-one which stands for Virology.
            /// </summary>
            VIROLOGIA = 91,

            /// <summary>
            ///     Value ninety-two which stands for Algology.
            /// </summary>
            ALGOLOGIA = 92,

            /// <summary>
            ///     Value ninety-three which stands for Oncologic surgery.
            /// </summary>
            CIRUGIA_ONCOLOGICA = 93,

            /// <summary>
            ///     Value ninety-four which stands for plastic surgery.
            /// </summary>
            CIRUGIA_PLASTICA = 94,

            /// <summary>
            ///     Value ninety-five which stands for endodontics.
            /// </summary>
            ENDODONCIA = 95,

            /// <summary>
            ///     Value ninety-six which stands for homeopathy.
            /// </summary>
            HOMEOPATIA = 96,

            /// <summary>
            ///     Value ninety-seven which stands for aesthetic medicine.
            /// </summary>
            MEDICINA_ESTETICA = 97,

            /// <summary>
            ///     Value ninety-eight which stands for neurosurgery.
            /// </summary>
            NEUROCIRUGIA = 98,

            /// <summary>
            ///     Value ninety-nine which stands for proctology.
            /// </summary>
            PROCTOLOGIA = 99,

            /// <summary>
            ///     Value one hundred which stands for rehabilitation.
            /// </summary>
            REHABILITACION = 100,

            /// <summary>
            ///     Value one hundred and one which stands for intensive therapy.
            /// </summary>
            TERAPIA_INTENSIVA = 101,

            /// <summary>
            ///     Value one hundred and two which stands for birds.
            /// </summary>
            AVES = 102,

            /// <summary>
            ///     Value one hundred and three which stands for beef cattle.
            /// </summary>
            BOVINOS_CARNE = 103,

            /// <summary>
            ///     Value one hundred and four which stands for milk cattle.
            /// </summary>
            BOVINOS_LECHE = 104,

            /// <summary>
            ///     Value one hundred and five which stands for pigs.
            /// </summary>
            CERDOS = 105,

            /// <summary>
            ///     Value one hundred and six which stands for small species.
            /// </summary>
            PEQUENAS_ESPECIES = 106,

            /// <summary>
            ///     Value one hundred and seven which stands for horses.
            /// </summary>
            EQUINOS = 107,

            /// <summary>
            ///     Value one hundred and eight which stands for exotic animals.
            /// </summary>
            ANIMALES_EXOTICOS = 108,

            /// <summary>
            ///     Value one hundred and nine which stands for audiology, otoneurology and phoniatrics.
            /// </summary>
            AUDIOLOGIA_OTONEUROLOGIA_Y_FONIATRIA = 109,

            /// <summary>
            ///     Value one hundred and ten which stands for quality of clinical care.
            /// </summary>
            CALIDAD_DE_LA_ATENCION_CLINICA = 110,

            /// <summary>
            ///     Value one hundred and eleven which stands for exotic animals.
            /// </summary>
            IMAGENOLOGIA_DIAGNOSTICA_Y_TERAPEUTICA = 111,

            /// <summary>
            ///     Value one hundred and twelve which stands for occupational and environmental medicine.
            /// </summary>
            MEDICINA_DEL_TRABAJO_Y_AMBIENTAL = 112,

            /// <summary>
            ///     Value one hundred and thirteen which stands for integrated medicine.
            /// </summary>
            MEDICINA_INTEGRADA = 113,

            /// <summary>
            ///     Value one hundred and fourteen which stands for legal medicine.
            /// </summary>
            MEDICINA_LEGAL = 114,

            /// <summary>
            ///     Value one hundred and fifteen which stands for radio oncology.
            /// </summary>
            RADIO_ONCOLOGIA = 115
            
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Code Phase.
        /// </summary>
        public enum CodeStatus
        {
            /// <summary>
            ///     Value zero which means that the code does not exist.
            /// </summary>
            NO_EXISTE = 0,

            /// <summary>
            ///     Value one which means that the code is inactive.
            /// </summary>
            INACTIVO = 1,

            /// <summary>
            ///     Value two which means that the code is active.
            /// </summary>
            ACTIVO = 2,

            /// <summary>
            ///     Value three which means that the code has expired.
            /// </summary>
            CADUCO = 3,

            /// <summary>
            ///     Value four which means that the code is canceled.
            /// </summary>
            CANCELADO = 4
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Code transaction.
        /// </summary>
        public enum CodeTransactions
        {

            /// <summary>
            ///     Value one which represents the transaction to create.
            /// </summary>
            CREAR = 1,

            /// <summary>
            ///     Value two which represents the transaction to activate.
            /// </summary>
            ACTIVAR = 2,

            /// <summary>
            ///     Value three which represents the transaction to inactivate.
            /// </summary>
            VENCER = 3,

            /// <summary>
            ///     Value four which represents the transaction to cancel.
            /// </summary>
            CANCELAR = 4

        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Banner type.
        /// </summary>
        public enum BannerTypes
        {
            /// <summary>
            ///     Value one which represents the fixed banners.
            /// </summary>
            FIJO = 1,

            /// <summary>
            ///     Value two which represents the dinamyc banners.
            /// </summary>
            HIGHLIGHT = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each mobile device.
        /// </summary>
        public enum OSMobileDevices
        {
            /// <summary>
            ///     Value one which stands iOS devices.
            /// </summary>
            iOS = 1,

            /// <summary>
            ///     Value two which stands BlackBerry devices.
            /// </summary>
            BLACKBERRY = 2,

            /// <summary>
            ///     Value three which stands Android devices.
            /// </summary>
            ANDROID = 3
        }

        /// <summary>
        ///     Enumeration that represents the values ​​for each device identifier.
        /// </summary>
        public enum DeviceIdentifiers
        {
            /// <summary>
            ///     Value one which stands Network Interface Card.
            /// </summary>
            NIC = 1,

            /// <summary>
            ///     Value two which stands International Mobile Equipment Identity.
            /// </summary>
            IMEI = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Output medium.
        /// </summary>
        public enum TargetOutputs
        {
            /// <summary>
            ///     Value one which stands for Client - Server.
            /// </summary>
            Cliente_Servidor = 1,

            /// <summary>
            ///     Value two which stands for Website.
            /// </summary>
            Web = 2,

            /// <summary>
            ///     Value three which stands for Android Devices.
            /// </summary>
            Android = 3,

            /// <summary>
            ///     Value four which stands for BlackBerry Devices.
            /// </summary>
            BlackBerry = 4,

            /// <summary>
            ///     Value five which stands for iPhone Devices.
            /// </summary>
            iOS_iPhone = 5,

            /// <summary>
            ///     Value six which stands for iPad Devices.
            /// </summary>
            iOS_iPad = 6,
            
            /// <summary>
            ///     Value seven which stands for server to server.
            /// </summary>
            Servidor_Servidor = 7,

            /// <summary>
            ///     Value eight which stands for windows phone devices.
            /// </summary>
            Windows_Phone = 8,

            /// <summary>
            ///     Value nine which stands for web services.
            /// </summary>
            Servicio_Web = 9

        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Electronic Tool.
        /// </summary>
        public enum ElectronicToolTypes
        {
            /// <summary>
            ///     Value one which stands Medical Notes.
            /// </summary>
            Notas_Médicas = 1,

            /// <summary>
            ///     Value two which stands PLM internal websites.
            /// </summary>
            Sitios_PLM = 2,

            /// <summary>
            ///     Value three which stands external websites.
            /// </summary>
            Sitios_Externos = 3
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each CodePrefix Types.
        /// </summary>
        public enum CodePrefixTypes
        {
            /// <summary>
            ///     Value one which stands standard type.
            /// </summary>
            Estandar = 1,

            /// <summary>
            ///     Value two which stands courtesy type.
            /// </summary>
            Cortesia = 2,

            /// <summary>
            ///     Value three which stands service type.
            /// </summary>
            Servicio = 3,

            /// <summary>
            ///     Value four which stands Medical Notes.
            /// </summary>
            Pruebas = 4,

            /// <summary>
            ///     Value five which stands distribution type.
            /// </summary>
            Distribucion = 5,

            /// <summary>
            ///     Value six which stands client/server type.
            /// </summary>
            Cliente_Servidor = 6,

            /// <summary>
            ///     Value seven which stands sale type.
            /// </summary>
            Venta_directa = 7,

            /// <summary>
            ///     Value eight which stands Mobile devices.
            /// </summary>
            Moviles = 8
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Event Type.
        /// </summary>
        public enum EventTypes
        {
            /// <summary>
            ///     Value one which stands Congress.
            /// </summary>
            Congresos = 1
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each CompanyClient Type.
        /// </summary>
        public enum CompanyClientTypes
        {
            /// <summary>
            ///     Value one which stands Generic.
            /// </summary>
            GENERICO = 1,

            /// <summary>
            ///     Value two which stands Pharmacy.
            /// </summary>
            FARMACIA = 2,

            /// <summary>
            ///     Value three which stands Laboratory.
            /// </summary>
            LABORATORIO = 3,

            /// <summary>
            ///     Value four which stands Private Hospital.
            /// </summary>
            HOSPITAL_PRIVADO = 4,

            /// <summary>
            ///     Value five which stands Public Hospital.
            /// </summary>
            HOSPITAL_PUBLICO = 5,

            /// <summary>
            ///     Value six which stands Distributor.
            /// </summary>
            DISTRIBUIDOR = 6,

            /// <summary>
            ///     Value seven which stands Hospital.
            /// </summary>
            HOSPITAL = 7,

            /// <summary>
            ///     Value eight which stands sales point.
            /// </summary>
            PUNTO_DE_VENTA = 8

        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each month year.
        /// </summary>
        public enum Months
        {
            /// <summary>
            ///     Value one which stands January.
            /// </summary>
            Enero = 1,

            /// <summary>
            ///     Value two which stands February.
            /// </summary>
            Febrero = 2,

            /// <summary>
            ///     Value three which stands March.
            /// </summary>
            Marzo = 3,

            /// <summary>
            ///     Value four which stands April.
            /// </summary>
            Abril = 4,

            /// <summary>
            ///     Value five which stands May.
            /// </summary>
            Mayo = 5,

            /// <summary>
            ///     Value six which stands June.
            /// </summary>
            Junio = 6,

            /// <summary>
            ///     Value seven which stands July.
            /// </summary>
            Julio = 7,

            /// <summary>
            ///     Value eight which stands August.
            /// </summary>
            Agosto = 8,

            /// <summary>
            ///     Value nive which stands September.
            /// </summary>
            Septiembre = 9,

            /// <summary>
            ///     Value ten which stands October.
            /// </summary>
            Octubre = 10,

            /// <summary>
            ///     Value eleven which stands November.
            /// </summary>
            Noviembre = 11,

            /// <summary>
            ///     Value twelve which stands December.
            /// </summary>
            Diciembre = 12
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Electronic Information Type.
        /// </summary>
        public enum ElectronicInformationTypes
        {
            /// <summary>
            ///     Value one which stands for Medical notes.
            /// </summary>
            Notas_Medicas = 1,

            /// <summary>
            ///     Value two which stands for Web Sites.
            /// </summary>
            Sitios_Web = 2,

            /// <summary>
            ///     Value three which stands for Abstracts.
            /// </summary>
            Abstracts = 3,

            /// <summary>
            ///     Value four which stands for Banners.
            /// </summary>
            Banners = 4,
            
            /// <summary>
            ///     Value five which stands for Medical Atlas.
            /// </summary>
            Atlas = 5,

            /// <summary>
            ///     Value six which stands for Scientific Articles.
            /// </summary>
            Articulos_Cientificos = 6,

            /// <summary>
            ///     Value seven which stands for Medical References.
            /// </summary>
            Refencias_Medicas = 7,

            /// <summary>
            ///     Value eight which stands for Promotions.
            /// </summary>
            Promociones = 8,

            /// <summary>
            ///     Value nine which stands for Medical Pratice Guides.
            /// </summary>
            Guias_de_Practica_Clinica = 9,

            /// <summary>
            ///     Value ten which stands for videos of interest.
            /// </summary>
            Videos_Interes = 10,

            /// <summary>
            ///     Value eleven which stands for men diets.
            /// </summary>
            Dietas_Hombres = 11,

            /// <summary>
            ///     Value twelve which stands for women diets.
            /// </summary>
            Dietas_Mujeres = 12,

            /// <summary>
            ///     Value thriteen which stands for prices list.
            /// </summary>
            Precios = 13,

            /// <summary>
            ///     Value fourteen which stands for handbooks.
            /// </summary>
            Manuales = 14,

            /// <summary>
            ///     Value fifteen which stands for magazines.
            /// </summary>
            Revistas = 15,

            /// <summary>
            ///     Value sixteen which stands for patients information.
            /// </summary>
            Informacion_Pacientes = 16,

            /// <summary>
            ///     Value seventeen which stands for guides.
            /// </summary>
            Guias = 17,

            /// <summary>
            ///     Value eighteen which stands for questionnaires.
            /// </summary>
            Cuestionarios = 18,

            /// <summary>
            ///     Value nineteen which stands for programs.
            /// </summary>
            Programas = 19,

            /// <summary>
            ///     Value twenty which stands for products monography.
            /// </summary>
            Monografias_Productos = 20,

            /// <summary>
            ///     Value twenty-one which stands for clinical surveys.
            /// </summary>
            Estudios_Clinicos = 21,

            /// <summary>
            ///     Value twenty-two which stands for veterinarios.
            /// </summary>
            Veterinarios = 22,

            /// <summary>
            ///     Value twenty-three which stands for instructional videos.
            /// </summary>
            Videos_Instructivos = 23,

            /// <summary>
            ///     Value twenty-four which stands for meal plan.
            /// </summary>
            Plan_Alimentacion = 24,

            /// <summary>
            ///     Value twenty-five which stands for exercise routines.
            /// </summary>
            Rutinas_Ejercicios = 25
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Application section.
        /// </summary>
        public enum Sections
        {
            /// <summary>
            ///     Value one which stands for indexes.
            /// </summary>
            INDICES = 1,

            /// <summary>
            ///     Value two which stands for Menus.
            /// </summary>
            MENUS = 2,

            /// <summary>
            ///     Value three which stands for New Information.
            /// </summary>
            NOVEDADES = 3,

            /// <summary>
            ///     Value four which stands for Options.
            /// </summary>
            OPCIONES = 4,

            /// <summary>
            ///     Value five which stands for Results.
            /// </summary>
            RESULTADOS = 5,

            /// <summary>
            ///     Value six which stands for Interest Links.
            /// </summary>
            VINCULOS_DE_INTERES = 6,

            /// <summary>
            ///     Value seven which stands for Banners.
            /// </summary>
            BANNERS = 7,

            /// <summary>
            ///     Value eight which stands for News.
            /// </summary>
            NOTICIAS = 8,

            /// <summary>
            ///     Value nine which stands for PLM OnLine.
            /// </summary>
            PLMONLINE = 9,

            /// <summary>
            ///     Value ten which stands for Editorial.
            /// </summary>
            EDITORIAL = 10,

            /// <summary>
            ///     Value eleven which stands for Services.
            /// </summary>
            SERVICIOS = 11,

            /// <summary>
            ///     Value twelve which stands for Medical Atlas.
            /// </summary>
            ATLAS = 12,

            /// <summary>
            ///     Value thirteen which stands for Scientific Articles.
            /// </summary>
            ARTICULOS_CIENTIFICOS = 13,

            /// <summary>
            ///     Value fourteen which stands for Medical Schemes.
            /// </summary>
            ESQUEMAS = 14,

            /// <summary>
            ///     Value fifteen which stands for Pharmacies.
            /// </summary>
            FARMACIAS = 15,

            /// <summary>
            ///     Value sixteen which stands for Agents.
            /// </summary>
            REPRESENTANTES = 16,

            /// <summary>
            ///     Value seventeen which stands for Medical References.
            /// </summary>
            REFERENCIAS_MEDICAS = 17,

            /// <summary>
            ///     Value eighteen which stands for Searcher.
            /// </summary>
            BUSCADOR = 18,

            /// <summary>
            ///     Value nineteen which stands for Medical Calculators.
            /// </summary>
            CALCULADORAS = 19,

            /// <summary>
            ///     Value twenty which stands for programs.
            /// </summary>
            PROGRAMAS = 20,

            /// <summary>
            ///     Value twenty-one which stands for settings.
            /// </summary>
            CONFIGURACION = 21,

            /// <summary>
            ///     Value twenty-two which stands for events.
            /// </summary>
            EVENTOS = 22,

            /// <summary>
            ///     Value twenty-three which stands for regional search.
            /// </summary>
            BUSCADOR_REGIONAL = 23,

            /// <summary>
            ///     Value twenty-four which stands for medical guides.
            /// </summary>
            GUIAS_CLINICAS = 24,

            /// <summary>
            ///     Value twenty-five which stands for diets.
            /// </summary>
            DIETAS = 25,

            /// <summary>
            ///     Value twenty-six which stands for distributors.
            /// </summary>
            DISTRIBUIDORES = 26,

            /// <summary>
            ///     Value twenty-seven which stands for sponsor products.
            /// </summary>
            PRODUCTOS_PATROCINADORES = 27,

            /// <summary>
            ///     Value twenty-eight which stands for handbooks.
            /// </summary>
            MANUALES = 28,

            /// <summary>
            ///     Value twenty-nine which stands for magazines.
            /// </summary>
            REVISTAS = 29,

            /// <summary>
            ///     Value thirty which stands for patients information.
            /// </summary>
            INFORMACION_PACIENTES = 30,

            /// <summary>
            ///     Value thirty-one which stands for guides.
            /// </summary>
            GUIAS = 31,

            /// <summary>
            ///     Value thirty-two which stands for favorites.
            /// </summary>
            FAVORITOS = 32,

            /// <summary>
            ///     Value thirty-three which stands for about that.
            /// </summary>
            SABIAS_QUE = 33,

            /// <summary>
            ///     Value thirty-four which stands for tools.
            /// </summary>
            HERRAMIENTAS = 34,

            /// <summary>
            ///     Value thirty-five which stands for medics.
            /// </summary>
            MEDICOS = 35,

            /// <summary>
            ///     Value thirty-six which stands for questionnaires.
            /// </summary>
            CUESTIONARIOS = 36,

            /// <summary>
            ///     Value thirty-seven which stands for monographies.
            /// </summary>
            MONOGRAFIAS = 37,

            /// <summary>
            ///     Value thirty-eigth which stands for drug interactions.
            /// </summary>
            INTERACCIONES = 38,

            /// <summary>
            ///     Value thirty-nine which stands for hospitals.
            /// </summary>
            HOSPITALES = 39,

            /// <summary>
            ///     Value forty which stands for clinical surveys.
            /// </summary>
            ESTUDIOS_CLINICOS = 40,

            /// <summary>
            ///     Value forty-one which stands for editorial content.
            /// </summary>
            CONTENIDO_EDITORIAL = 41,

            /// <summary>
            ///     Value forty-two which stands for videos.
            /// </summary>
            VIDEOS = 42,

            /// <summary>
            ///     Value forty-three which stands for meal plan.
            /// </summary>
            PLANES_ALIMENTACION = 43,

            /// <summary>
            ///     Value forty-four which stands for exercise routines.
            /// </summary>
            RUTINAS_EJERCICIOS = 44,

            /// <summary>
            ///     Value forty-five which stands for product manual.
            /// </summary>
            INSTRUCTIVOS_PRODUCTOS = 45,

            /// <summary>
            ///     Value forty-six which stands for outlets.
            /// </summary>
            PUNTOS_VENTA = 46,

            BUSCADOR_BANNER_1 = 47,

            BUSCADOR_BANNER_2 = 48,

            BUSCADOR_BANNER_3 = 49,

            BUSCADOR_BANNER_4 = 50,

            BUSCADOR_BANNER_5 = 51,

            BUSCADOR_BANNER_6 = 52,

            BUSCADOR_BANNER_7 = 53,

            BUSCADOR_BANNER_8 = 54,

            BUSCADOR_BANNER_9 = 55,

            BUSCADOR_BANNER_10 = 56,

            FAVORITOS_BANNER_1 = 57,

            FAVORITOS_BANNER_2 = 58,

            FAVORITOS_BANNER_3 = 59,

            FAVORITOS_BANNER_4 = 60,

            FAVORITOS_BANNER_5 = 61,

            FAVORITOS_BANNER_6 = 62,

            FAVORITOS_BANNER_7 = 63,

            FAVORITOS_BANNER_8 = 64,

            FAVORITOS_BANNER_9 = 65,

            FAVORITOS_BANNER_10 = 66,

            EVENTOS_BANNER_1 = 67,

            EVENTOS_BANNER_2 = 68,

            EVENTOS_BANNER_3 = 69,

            EVENTOS_BANNER_4 = 70,

            EVENTOS_BANNER_5 = 71,

            EVENTOS_BANNER_6 = 72,

            EVENTOS_BANNER_7 = 73,

            EVENTOS_BANNER_8 = 74,

            EVENTOS_BANNER_9 = 75,

            EVENTOS_BANNER_10 = 76,

            EDITORIAL_BANNER_1 = 77,

            EDITORIAL_BANNER_2 = 78,

            EDITORIAL_BANNER_3 = 79,

            EDITORIAL_BANNER_4 = 80,

            EDITORIAL_BANNER_5 = 81,

            EDITORIAL_BANNER_6 = 82,

            EDITORIAL_BANNER_7 = 83,

            EDITORIAL_BANNER_8 = 84,

            EDITORIAL_BANNER_9 = 85,

            EDITORIAL_BANNER_10 = 86,

            ATLAS_BANNER_1 = 87,

            ATLAS_BANNER_2 = 88,

            ATLAS_BANNER_3 = 89,

            ATLAS_BANNER_4 = 90,

            ATLAS_BANNER_5 = 91,

            ATLAS_BANNER_6 = 92,

            ATLAS_BANNER_7 = 93,

            ATLAS_BANNER_8 = 94,

            ATLAS_BANNER_9 = 95,

            ATLAS_BANNER_10 = 96,

            CALCULADORAS_BANNER_1 = 97,

            CALCULADORAS_BANNER_2 = 98,

            CALCULADORAS_BANNER_3 = 99,

            CALCULADORAS_BANNER_4 = 100,

            CALCULADORAS_BANNER_5 = 101,

            CALCULADORAS_BANNER_6 = 102,

            CALCULADORAS_BANNER_7 = 103,

            CALCULADORAS_BANNER_8 = 104,

            CALCULADORAS_BANNER_9 = 105,

            CALCULADORAS_BANNER_10 = 106,

            FARMACIAS_BANNER_1 = 107,

            FARMACIAS_BANNER_2 = 108,

            FARMACIAS_BANNER_3 = 109,

            FARMACIAS_BANNER_4 = 110,

            FARMACIAS_BANNER_5 = 111,

            FARMACIAS_BANNER_6 = 112,

            FARMACIAS_BANNER_7 = 113,

            FARMACIAS_BANNER_8 = 114,

            FARMACIAS_BANNER_9 = 115,

            FARMACIAS_BANNER_10 = 116

        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Questionnaire type.
        /// </summary>
        public enum QuestionnaireTypes
        {
            /// <summary>
            ///     Value one which stands Unique option.
            /// </summary>
            MONO_OPCION = 1,

            /// <summary>
            ///     Value two which stands Option multiple.
            /// </summary>
            MULTI_OPCION = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Session operation.
        /// </summary>
        public enum Sessions
        {
            /// <summary>
            ///     Value one which stands session openned.
            /// </summary>
            ABIERTA = 1,

            /// <summary>
            ///     Value one which stands session closed.
            /// </summary>
            CERRADA = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Client Activity type.
        /// </summary>
        public enum ActivityTypes
        {
            /// <summary>
            ///     Value one which stands Breakfast.
            /// </summary>
            DESAYUNO = 1,

            /// <summary>
            ///     Value two which stands Lunch.
            /// </summary>
            COMIDA = 2,

            /// <summary>
            ///     Value three which stands Dinner.
            /// </summary>
            CENA = 3,

            /// <summary>
            ///     Value four which stands First collation.
            /// </summary>
            PRIMERA_COLACION = 4,

            /// <summary>
            ///     Value five which stands Second collation.
            /// </summary>
            SEGUNDA_COLACION = 5,

            /// <summary>
            ///     Value six which stands do exercise.
            /// </summary>
            EJERCICIO = 6
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Client Data type.
        /// </summary>
        public enum DataTypes
        {
            /// <summary>
            ///     Value one which stands client weight.
            /// </summary>
            PESO = 1,

            /// <summary>
            ///     Value two which stands client height.
            /// </summary>
            TALLA = 2
        }

        /// <summary>
        ///     Enumeration which represents the values ​​for each Body Mass Index.
        /// </summary>
        public enum BodyMassIndexes
        {
            /// <summary>
            ///     Value one which stands severe thinness.
            /// </summary>
            DELGADEZ_SEVERA = 1,

            /// <summary>
            ///     Value two which stands moderate thinness.
            /// </summary>
            DELGADEZ_MODERADA = 2,

            /// <summary>
            ///     Value three which stands acceptable thinness.
            /// </summary>
            DELGADEZ_ACEPTABLE = 3,

            /// <summary>
            ///     Value four which stands normal weight.
            /// </summary>
            PESO_NORMAL = 4,

            /// <summary>
            ///     Value five which stands overweight.
            /// </summary>
            SOBREPESO = 5,

            /// <summary>
            ///     Value six which stands obesity first grade.
            /// </summary>
            OBESIDAD_I = 6,

            /// <summary>
            ///     Value seven which stands obesity second grade.
            /// </summary>
            OBESIDAD_II = 7,

            /// <summary>
            ///     Value eight which stands obesity third grade.
            /// </summary>
            OBESIDAD_III = 8

        }

        #endregion

        #region Farmacovigilancia

        ///<summary>
        /// Enumeration which represents the values for Questions Types
        ///</summary>

        public enum QuestionsTypes
        {
            /// <summary>
            ///   Value one which represents suspect  dugs information.
            /// </summary>
            INFORMACION_MEDICAMENTO_SOSPECHOSO =1,

            ///<summary>
            ///Value two which represents medical history
            ///</summary>
            
            HISTORIA_CLINICA = 2,

            ///<summary>
            ///Value three which represents Source of Information
            ///</summary>
            PROCEDENCIA_DE_INFORMACION=3,

            ///<summary>
            ///Value four which represents Details of Suspected Adverse Reaction
            ///</summary>
            DATOS_DE_SOSPECHA_DE_REACCION_ADVERSA=4


        }


        ///<summary>
        /// Enumeration which represents the values for Consequences
        ///</summary>
        public enum Consequences
        {

            /// <summary>
            ///  Value one which represents
            /// </summary>
            MUERTE_DEBIDO_A_LA_REACCION_ADVERSA=1,

            /// <summary>
            ///  Value two which represents
            /// </summary>
            MUERTE_EL_FARMACO_PUDO_HABER_CONTRIBUIDO=2,

            /// <summary>
            ///  Value three which represents
            /// </summary>
            MUERTE_NO_RELACIONADA_AL_MEDICAMNETO=3,
            /// <summary>
            ///   Value four which represents
            /// </summary>
            RECUPERADO_CON_SECUELA=4,
            /// <summary>
            ///  Value five which represents
            /// </summary>
            RECUPERADO_SIN_SECUELA=5

        }

        #endregion

    }
}
