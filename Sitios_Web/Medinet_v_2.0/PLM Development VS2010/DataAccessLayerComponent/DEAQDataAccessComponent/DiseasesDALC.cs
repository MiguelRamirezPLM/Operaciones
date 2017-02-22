using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEAQDataAccessComponent
{
    public class DiseasesDALC
    {
        #region Constructor

         private DiseasesDALC() { }

        #endregion

        #region Public Methods
         
        public void CreateDiseases(int diseaseId ,string diseaseName , string scientificName, bool active)
        {
            AgroEntityFramework.Diseases.CreateDiseases(diseaseId, diseaseName, scientificName, active);            
        }

         public AgroEntityFramework.Diseases getDiseasesById(int diseaseId)
         {
             //DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();
            //  AgroEntityFramework.di

            //var diseases = from p in db
            //               where db.DiseaseId = diseaseId
            //               select p;
         }
      //var lowNums = 
      //      from n in numbers 
      //      where n < 5 
      //      select n; 

        #endregion

        public static readonly DiseasesDALC Instance = new DiseasesDALC();
    }
}
