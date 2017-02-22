using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;
using MedinetBusinessEntries;


namespace PLMClientsBusinessLogicComponent
{
   public  class PharmacovigilanceBLC
   {
       #region Constructors

       private PharmacovigilanceBLC() {}
       #endregion 

       #region Public Methods


       public void savePharmacovigilance(PharmacovigilanceDetailInfo pharmacovigilanceDetail)
       {

           /*  declaration of variables  */

           int countAdeverse = pharmacovigilanceDetail.AdverseReaction.Count;
           int [] identifierAdverse= new int[countAdeverse];

           string questionsByDrug="";
           string questionsByPacient = "";
           string questionsBySource="";
           string questionsByAdverse = "";

           string descriptionChildrenDrug = "";
           string descriptionAdverseReaction = "";



           /* declaration of objects*/

           PharmacovigilanceInfo pharmacov = new PharmacovigilanceInfo();
           PatientInfo patient = new PatientInfo();
           PharmacovigilanceParentDrugInfo drug = new PharmacovigilanceParentDrugInfo();
           AdverseReactionByIdentifierInfo adverseReaction = new AdverseReactionByIdentifierInfo();
           AnswerByQuestionInfo AnswerByQuest = new AnswerByQuestionInfo();
           PharmacovigilanceSourceDetailInfo source = new PharmacovigilanceSourceDetailInfo();
           PharmacovigilanceParentDrugInfo childrenDrugs = new PharmacovigilanceParentDrugInfo();

           /* PharmaTypeId = 1 :Medicos */

          
               #region Pharmacovigilance

               pharmacov.PharmaTypeId = pharmacovigilanceDetail.PharmaTypeId;
             
               if (pharmacovigilanceDetail.CompanyClient!= null)
               pharmacov.CompanyClient = pharmacovigilanceDetail.CompanyClient;

               if (pharmacovigilanceDetail.CompanyClientId != 0)
                   pharmacov.CompanyClientId = pharmacovigilanceDetail.CompanyClientId;


               #endregion

               #region Patient

               if (pharmacovigilanceDetail.Patient.FirstName != null)
                   patient.FirstName = pharmacovigilanceDetail.Patient.FirstName.Trim().ToUpper();
               else
                   patient.FirstName = null;

               if (pharmacovigilanceDetail.Patient.LastName != null)
                   patient.LastName = pharmacovigilanceDetail.Patient.LastName.Trim().ToUpper();
               else
                   patient.LastName = null;

               if (pharmacovigilanceDetail.Patient.SecondLastName != null)
                   patient.SecondLastName = pharmacovigilanceDetail.Patient.SecondLastName.Trim().ToUpper();
               else
                   patient.SecondLastName = null;

               patient.Initials = pharmacovigilanceDetail.Patient.Initials;

               if (pharmacovigilanceDetail.Patient.Birthday != null)
                   patient.Birthday = pharmacovigilanceDetail.Patient.Birthday;


               if (pharmacovigilanceDetail.Patient.Gender != null)
                   patient.Gender = pharmacovigilanceDetail.Patient.Gender;


               if (pharmacovigilanceDetail.Patient.Height != null)
                   patient.Weight = pharmacovigilanceDetail.Patient.Height;

               if (pharmacovigilanceDetail.Patient.Weight != null)
                   patient.Weight = pharmacovigilanceDetail.Patient.Weight;

               if (pharmacovigilanceDetail.Patient.Street != null)
                   patient.Street = pharmacovigilanceDetail.Patient.Street;
               else
                   patient.Street = "";

               if (pharmacovigilanceDetail.Patient.Suburb != null)
                   patient.Suburb = pharmacovigilanceDetail.Patient.Suburb;
               else
                   patient.Suburb = "";

               if (pharmacovigilanceDetail.Patient.StateId != null)
                   if (pharmacovigilanceDetail.Patient.StateId != null)
                       patient.StateId = pharmacovigilanceDetail.Patient.StateId;
                   else
                       patient.StateId = null;

               if (pharmacovigilanceDetail.Patient.PhoneOne != null)
                   patient.PhoneOne = pharmacovigilanceDetail.Patient.PhoneOne;

               if (pharmacovigilanceDetail.Patient.Email != null)
                   patient.Email = pharmacovigilanceDetail.Patient.Email;

               if (pharmacovigilanceDetail.Patient.MedicalDiagnostic != null)
                   patient.MedicalDiagnostic = pharmacovigilanceDetail.Patient.MedicalDiagnostic;


               #endregion         

               #region PharmacovigilanceParentDrugs
                   
               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.ActiveSubstanceKey != null)
                   drug.ActiveSubstanceKey = pharmacovigilanceDetail.PharmacovigilanceParentDrug.ActiveSubstanceKey;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.ActiveSubstance != null)
                   drug.ActiveSubstance = pharmacovigilanceDetail.PharmacovigilanceParentDrug.ActiveSubstance;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.BrandKey != null)
                   drug.BrandKey = pharmacovigilanceDetail.PharmacovigilanceParentDrug.BrandKey;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.Brand != null)
                   drug.Brand = pharmacovigilanceDetail.PharmacovigilanceParentDrug.Brand;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.DivisionKey != null)
                   drug.DivisionKey = pharmacovigilanceDetail.PharmacovigilanceParentDrug.DivisionKey;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.DivisionName != null)
                   drug.DivisionName = pharmacovigilanceDetail.PharmacovigilanceParentDrug.DivisionName;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.RegisterNumber != null)
                   drug.RegisterNumber = pharmacovigilanceDetail.PharmacovigilanceParentDrug.RegisterNumber;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.Expiration != null)
                   drug.Expiration = pharmacovigilanceDetail.PharmacovigilanceParentDrug.Expiration;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.Dose != null)
                   drug.Dose = pharmacovigilanceDetail.PharmacovigilanceParentDrug.Dose;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.UnitDose != null)
                   drug.UnitDose = pharmacovigilanceDetail.PharmacovigilanceParentDrug.UnitDose;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.AdministrationRouteKey != null)
                   drug.AdministrationRouteKey = pharmacovigilanceDetail.PharmacovigilanceParentDrug.AdministrationRouteKey;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.AdministrationRoute != null)
                   drug.AdministrationRoute = pharmacovigilanceDetail.PharmacovigilanceParentDrug.AdministrationRoute;

               drug.StartDate = pharmacovigilanceDetail.PharmacovigilanceParentDrug.StartDate;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.EndDate != null)
                   drug.EndDate = pharmacovigilanceDetail.PharmacovigilanceParentDrug.EndDate;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.ICDKey != null)
                   drug.ICDKey = pharmacovigilanceDetail.PharmacovigilanceParentDrug.ICDKey;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.ICD != null)
                   drug.ICD = pharmacovigilanceDetail.PharmacovigilanceParentDrug.ICD;

               if(pharmacovigilanceDetail.PharmacovigilanceParentDrug.PharmacovigilanceDrugTypeId != 0)
                 drug.PharmacovigilanceDrugTypeId = pharmacovigilanceDetail.PharmacovigilanceParentDrug.PharmacovigilanceDrugTypeId;

               //drug.FrequencyId = pharmacovigilanceDetail.PharmacovigilanceParentDrug.FrequencyId;

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.FrequencyId != null)
               {
                   drug.FrequencyId = pharmacovigilanceDetail.PharmacovigilanceParentDrug.FrequencyId;
               }
               else
                   drug.FrequencyId = null;

               #endregion

               #region PharmacovigilanceSource

               source.PharmaSourceInfoTypeId = pharmacovigilanceDetail.PharmacovigilanceSource.PharmaSourceInfoTypeId; //PAC O FAM

               if (pharmacovigilanceDetail.PharmacovigilanceSource.Name != null)
                   source.Name = pharmacovigilanceDetail.PharmacovigilanceSource.Name;
               else
                   source.Name = "";

               if (pharmacovigilanceDetail.PharmacovigilanceSource.PharmaInfoTypeId != null)
                   source.PharmaInfoTypeId = (byte)pharmacovigilanceDetail.PharmacovigilanceSource.PharmaInfoTypeId;

               if (pharmacovigilanceDetail.PharmacovigilanceSource.SourceId != null)
                   source.SourceId = (byte)pharmacovigilanceDetail.PharmacovigilanceSource.SourceId;

               if (pharmacovigilanceDetail.PharmacovigilanceSource.ReceptionDate != null)
                   source.ReceptionDate = pharmacovigilanceDetail.PharmacovigilanceSource.ReceptionDate;

               if (pharmacovigilanceDetail.PharmacovigilanceSource.PhoneOne != null)
                   source.PhoneOne = pharmacovigilanceDetail.PharmacovigilanceSource.PhoneOne;

               if (pharmacovigilanceDetail.PharmacovigilanceSource.Street != null)
                   source.Street = pharmacovigilanceDetail.PharmacovigilanceSource.Street;
               else
                   source.Street = "";

               if (pharmacovigilanceDetail.PharmacovigilanceSource.Suburb != null)
                   source.Suburb = pharmacovigilanceDetail.PharmacovigilanceSource.Suburb;
               else
                   source.Suburb = "";

               if(pharmacovigilanceDetail.PharmacovigilanceSource.StateId != null)
                   if (pharmacovigilanceDetail.PharmacovigilanceSource.StateId < 0) 
                       source.StateId = pharmacovigilanceDetail.PharmacovigilanceSource.StateId;
                   else
                       source.StateId = null;

               if (pharmacovigilanceDetail.PharmacovigilanceSource.ZipCode != null)
                   source.ZipCode = pharmacovigilanceDetail.PharmacovigilanceSource.ZipCode;

               #endregion

               #region AdverseReaction

              int aux1 = 0;

               foreach (AdverseReactionByIdentifierInfo itemsAdverse in pharmacovigilanceDetail.AdverseReaction)
               {

                       identifierAdverse[aux1] = aux1+1;
                       adverseReaction.StartReaction = itemsAdverse.StartReaction;
                       adverseReaction.ConsequenceId = itemsAdverse.ConsequenceId;
                       adverseReaction.DescriptionReaction = itemsAdverse.DescriptionReaction;
                       adverseReaction.QuestionId = itemsAdverse.QuestionId;
                       adverseReaction.QuestionTypeId = itemsAdverse.QuestionTypeId;
                       adverseReaction.AnswerId = itemsAdverse.AnswerId;

                       if (itemsAdverse.AnswerDescription != null)
                           adverseReaction.AnswerDescription = itemsAdverse.AnswerDescription;
                       else
                           adverseReaction.AnswerDescription = "";



                       descriptionAdverseReaction += identifierAdverse[aux1].ToString() + "~'" + adverseReaction.DescriptionReaction.ToString() + "'," + adverseReaction.ConsequenceId.ToString() + ",'" + adverseReaction.StartReaction.ToString("yyyy/MM/dd") + "'|";
                       
                       if( itemsAdverse.QuestionId!=0)
                           questionsByAdverse += identifierAdverse[aux1].ToString() + "~" + adverseReaction.QuestionId.ToString() + "," + adverseReaction.QuestionTypeId.ToString() + "," + adverseReaction.AnswerId.ToString() + ",'" + adverseReaction.AnswerDescription.ToString() + "'|";
                       

                       aux1++;
               }

               if (descriptionAdverseReaction.Length != 0)
                   descriptionAdverseReaction = descriptionAdverseReaction.Substring(0, descriptionAdverseReaction.Length - 1);

               if (questionsByAdverse.Length != 0)
                   questionsByAdverse = questionsByAdverse.Substring(0, questionsByAdverse.Length - 1);

               #endregion

               #region ChidrenDrugsDetail

               if (pharmacovigilanceDetail.PharmacovigilanceParentDrug.ChildrenPharmacovigilanceDrugs.Count != 0 )
               {

                   foreach (var itemsChildren in pharmacovigilanceDetail.PharmacovigilanceParentDrug.ChildrenPharmacovigilanceDrugs)
                   {
                       if (itemsChildren.Brand != null) 
                           childrenDrugs.Brand = itemsChildren.Brand;
                       else
                           childrenDrugs.Brand = "";


                       if (itemsChildren.Dose != null)
                           childrenDrugs.Dose = itemsChildren.Dose;
                       else
                           childrenDrugs.Dose = "";


                       if (itemsChildren.AdministrationRoute != null) 
                           childrenDrugs.AdministrationRoute = itemsChildren.AdministrationRoute;
                       else
                           childrenDrugs.AdministrationRoute = "";

                        if (itemsChildren.ICD != null)
                           childrenDrugs.ICD = itemsChildren.ICD;
                       else
                            childrenDrugs.ICD = "";

                       if (itemsChildren.BrandKey != null)
                           childrenDrugs.BrandKey = itemsChildren.BrandKey;
                       else
                           childrenDrugs.BrandKey = "";

                       if (itemsChildren.AdministrationRouteKey != null)
                           childrenDrugs.AdministrationRouteKey = itemsChildren.AdministrationRouteKey;
                       else
                           childrenDrugs.AdministrationRouteKey = "";

                       if (itemsChildren.ICDKey != null)
                           childrenDrugs.ICDKey = itemsChildren.ICDKey;
                       else
                           childrenDrugs.ICDKey = "";

                       if (itemsChildren.ActiveSubstanceKey != null)
                           childrenDrugs.ActiveSubstanceKey = itemsChildren.ActiveSubstanceKey;
                       else
                           childrenDrugs.ActiveSubstanceKey = "";

                       if (itemsChildren.ActiveSubstance != null)
                           childrenDrugs.ActiveSubstance = itemsChildren.ActiveSubstance;
                       else
                           childrenDrugs.ActiveSubstance = "";

                       if (itemsChildren.DivisionKey != null)
                           childrenDrugs.DivisionKey = itemsChildren.DivisionKey;
                       else
                           childrenDrugs.DivisionKey = "";

                       if (itemsChildren.DivisionName != null)
                           childrenDrugs.DivisionName = itemsChildren.DivisionName;
                       else
                           childrenDrugs.DivisionName = "";

                       if (itemsChildren.RegisterNumber != null)
                           childrenDrugs.RegisterNumber = itemsChildren.RegisterNumber;
                       else
                           childrenDrugs.RegisterNumber = "";

                       if (itemsChildren.UnitDose != null)
                           childrenDrugs.UnitDose = itemsChildren.UnitDose;
                       else
                           childrenDrugs.UnitDose = "";

                       if (itemsChildren.UnitDoseKey != null)
                           childrenDrugs.UnitDoseKey = itemsChildren.UnitDoseKey;
                       else
                           childrenDrugs.UnitDoseKey = "";

                       string freq = "";
                       if (itemsChildren.FrequencyId != null)
                       {
                           freq = itemsChildren.FrequencyId.ToString();
                       }
                       else
                           freq = "null";

                       string starDate = "";
                       if (itemsChildren.StartDate != null)
                       {
                           starDate = itemsChildren.StartDate.ToString("yyyy/MM/dd");
                       }
                       else
                           starDate = "null";

                       DateTime EndDateString;
                       DateTime ExpirationString;

                       string endDate = "";
                       if (itemsChildren.EndDate != null)
                       {
                           EndDateString = Convert.ToDateTime(itemsChildren.EndDate);
                           endDate = EndDateString.ToString("yyyy/MM/dd");
                           endDate = "'" + endDate + "'";
                       }
                       else
                           endDate = "null";

                       string expiration = "";
                       if (itemsChildren.Expiration != null)
                       {
                           ExpirationString = Convert.ToDateTime(itemsChildren.Expiration);
                           expiration = ExpirationString.ToString("yyyy/MM/dd");
                           expiration = "'" + expiration + "'";
                       }
                       else
                           expiration = "null";


                       descriptionChildrenDrug += "'" + childrenDrugs.Brand.ToString() + "','" + childrenDrugs.Dose.ToString() + "','" + childrenDrugs.AdministrationRoute.ToString() + "','"
                               + starDate + "'," + endDate + ",'"
                               + childrenDrugs.BrandKey.ToString() + "','" + childrenDrugs.AdministrationRouteKey.ToString() + "','"
                               + childrenDrugs.ICDKey.ToString() + "','" + childrenDrugs.ActiveSubstanceKey.ToString() + "','"
                               + childrenDrugs.ActiveSubstance.ToString() + "','" + childrenDrugs.DivisionKey.ToString() + "','"
                               + childrenDrugs.DivisionName.ToString() + "','" + childrenDrugs.RegisterNumber.ToString() + "',"
                               + expiration + ",'" + childrenDrugs.UnitDose.ToString() + "','"
                               + childrenDrugs.UnitDoseKey.ToString() + "'," + freq + ",'" +
                               childrenDrugs.ICD + "'|";
                   }
                   descriptionChildrenDrug = descriptionChildrenDrug.Substring(0, descriptionChildrenDrug.Length - 1);
               }

               #endregion

               #region QuestionDetail

               
               foreach (AnswerByQuestionInfo items in pharmacovigilanceDetail.AnswerByQuestion)
               {
                   AnswerByQuest.QuestionId = items.QuestionId;
                   AnswerByQuest.QuestionTypeId = items.QuestionTypeId;
                   AnswerByQuest.AnswerId = items.AnswerId;
                   AnswerByQuest.AnswerDescription = items.AnswerDescription;

                   if (AnswerByQuest.AnswerDescription == null)
                       AnswerByQuest.AnswerDescription = "";


                   if (AnswerByQuest.QuestionTypeId == 1 || AnswerByQuest.QuestionTypeId == 5)
                       questionsByDrug += AnswerByQuest.QuestionId.ToString() + "," + AnswerByQuest.QuestionTypeId.ToString() + "," + AnswerByQuest.AnswerId.ToString() + ",'" + AnswerByQuest.AnswerDescription.ToString() + "'|";
                    
                   if (AnswerByQuest.QuestionTypeId == 2)
                       questionsByPacient += AnswerByQuest.QuestionId.ToString() + "," + AnswerByQuest.QuestionTypeId.ToString() + "," + AnswerByQuest.AnswerId.ToString() + ",'" + AnswerByQuest.AnswerDescription.ToString() + "'|";

                   if (AnswerByQuest.QuestionTypeId == 3)
                       questionsBySource += AnswerByQuest.QuestionId.ToString() + "," + AnswerByQuest.QuestionTypeId.ToString() + "," + AnswerByQuest.AnswerId.ToString() + ",'" + AnswerByQuest.AnswerDescription.ToString() + "'|";


               }

               if (questionsByDrug.Length != 0)
                   questionsByDrug = questionsByDrug.Substring(0, questionsByDrug.Length - 1);

               if (questionsByPacient.Length != 0)
                   questionsByPacient = questionsByPacient.Substring(0, questionsByPacient.Length - 1);

               if (questionsBySource.Length != 0)
                   questionsBySource = questionsBySource.Substring(0, questionsBySource.Length - 1);

              



               #endregion


           if(pharmacov.PharmaTypeId ==1)
               PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.savePharmacovigilanceMedico(pharmacov,patient,drug,source,descriptionAdverseReaction,descriptionChildrenDrug,questionsByAdverse,questionsByDrug,questionsBySource,questionsByPacient);
           
           if(pharmacov.PharmaTypeId==2)
               PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.savePharmacovigilancePatient(pharmacov, patient, drug, source, descriptionAdverseReaction, descriptionChildrenDrug, questionsByAdverse, questionsByDrug, questionsByPacient);
       

      


       }



       public List<QuestionDetailInfo> getQuestionsByType(int questionTypeId)
       {
           List<QuestionDetailInfo> questions = PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getQuestions(questionTypeId);

           foreach (QuestionDetailInfo item in questions)
           {
               item.Answer = PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getAnswerByQuestion(item.QuestionId);
           }

           return questions;
       }

       public List<ConsequenceInfo> getConsequences()
       {
           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getConsequences();
       }

       public List<PharmacovigilanceDrugTypeInfo> getPharmacovigilanceDrugTypes()
       {

           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getPharmacovigilanceDrugTypes();
       }

       public List<PharmacovigilanceTypeInfo> getPharmacovigilanceTypes()
       {

           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getPharmacovigilanceTypes();

       }

       public List<FrequenciesInfo> getFrequences()
       {
           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getFrequences();
       }

       public List<PharmacovigilanceSourceInfoTypeInfo> getSourceInfoTypes()
       {
           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getSourceInfoTypes();
       }

       public List<SourceInfo> getSources()
       {

           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getSources();
       }

       public List<PharmacovigilanceInfoTypeInfo> getPharmacovigilanceInfoTypes()
       {
           return PLMClientsDataAccessComponent.PharmacovigilanceDALC.Instance.getPharmacovigilanceInfoTypes();
       }


       #endregion


       public static readonly PharmacovigilanceBLC Instance = new PharmacovigilanceBLC();

       

   }
}
