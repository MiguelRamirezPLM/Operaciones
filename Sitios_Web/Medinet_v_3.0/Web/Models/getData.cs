using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class getData
    {
        Medinet db = new Medinet();

        public int getAlphabetId(string ProductName)
        {
            string letter = ProductName.Trim().Substring(0, 1);
            string[] num = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[]  let = new string[] { "U", "D", "T", "C", "C", "S", "S", "O", "N" };
            bool areEqual;

            for (int i = 0; i < num.Length; i++ )
            {
               areEqual = String.Equals(letter, num[i], StringComparison.Ordinal);
               if (areEqual == true)
                {
                   letter = let[i];
                }
                
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
    }
}