using System;
using System.Collections.Generic;
using System.Text;

namespace PLMUsersBusinessLogicComponent
{
    public class ErrorsBLC
    {
        #region Constructors

        private ErrorsBLC() { }

        #endregion

        #region Public Methods

        public string registerError(Exception ex, int applicationId)
        {

            PLMUserBusinessEntries.ErrorInfo error = new PLMUserBusinessEntries.ErrorInfo();

            error.ApplicationId = applicationId;

            PLMUserBusinessEntries.FolioInfo folioNumber = PLMUsersBusinessLogicComponent.FoliosBLC.Instance.getByapplication(error.ApplicationId);

            folioNumber.CurrentNumber = folioNumber.CurrentNumber + 1;

            error.Message = ex.Message;
            error.StackTrace = ex.StackTrace;
            error.Status = true;
            error.Folio = folioNumber.Prefix + folioNumber.CurrentNumber.ToString();
            
            error.ErrorId = PLMUsersDataAccessComponent.ErrorsDALC.Instance.insert(error);

            PLMUsersBusinessLogicComponent.FoliosBLC.Instance.updateFolio(folioNumber);

            return PLMUsersDataAccessComponent.ErrorsDALC.Instance.getOne(error.ErrorId).Folio;
        }

        #endregion

        public static readonly ErrorsBLC Instance = new ErrorsBLC();
    }
}
