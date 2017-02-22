using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CodesBLC
    {
        #region Constructors

        private CodesBLC() { }

        #endregion

        #region Public Methods

        public int addCode(CodeInfo BEntity)
        {
            return PLMClientsDataAccessComponent.CodesDALC.Instance.insert(BEntity);
        }

        public void removeCode(int codeId)
        {
            if (codeId <= 0)
                throw new ArgumentException("The code does not exist.");

            PLMClientsDataAccessComponent.CodesDALC.Instance.delete(codeId);
        }

        public void updateCodes(CodeInfo BEntity)
        {
            PLMClientsDataAccessComponent.CodesDALC.Instance.update(BEntity);
        }

        public CodeInfo getCode(int codeId)
        {
            if (codeId <= 0)
                throw new ArgumentException("The code does not exist.");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getOne(codeId);
        }

        public CodeInfo getCode(string codeString)
        {
             CodeInfo code = PLMClientsDataAccessComponent.CodesDALC.Instance.getByCodeString(codeString);
             
             if(code == null)
                code = this.checkCode(codeString);

             return code;
        }

        public CodeInfo getCodeByPrefix(int prefixId)
        {
            if (prefixId <= 0)
                throw new ArgumentException("The code does not exist.");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByPrefix(prefixId);
        }

        public CodeInfo getByIMEI(string IMEI)
        { 
            if (string.IsNullOrEmpty(IMEI))
                throw new ArgumentNullException("IMEI");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByIMEI(IMEI);
        }

        public CodeInfo getByIMEIByEdition(string IMEI, string isbn)
        {

            if (string.IsNullOrEmpty(IMEI))
                throw new ArgumentNullException("IMEI");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByIMEIByISBN(IMEI, isbn);

        }

        public CodeInfo getByIMEIByPrefix(string IMEI, string prefix)
        {
            if (string.IsNullOrEmpty(IMEI))
                throw new ArgumentNullException("IMEI");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByIMEIByPrefix(IMEI, prefix);
        }

        public ValidCodeInfo validCode(string codeString)
        {
            CodeInfo code = this.getCode(codeString);
                     
            ValidCodeInfo validCode = new ValidCodeInfo();

            validCode.IsExist = code != null;

            if (validCode.IsExist)
                validCode.CodeStatusId = (Catalogs.CodeStatus)code.CodeStatusId;

            return validCode;
        }

        public CodeInfo getByClientByPrefix(int clientId, string codePrefix, string hwIdentifier)
        {
            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByClientByPrefix(clientId, codePrefix, hwIdentifier);
        }

        public PLMClientsBusinessEntities.CodeInfo generateCode(string codePrefix)
        {
            // Get CodePrefixInfo object by codePrefix varible:
            PLMClientsBusinessEntities.CodePrefixInfo codePrefixInfo = CodePrefixesBLC.Instance.getCodePrefix(codePrefix);

            // Create the new code:
            string codeString = new PLMCryptographyComponent.CryptographyComponent().addHash(codePrefixInfo.PrefixValue.Trim() + codePrefixInfo.CurrentValue.ToString());

            // Add new code to the database as inactive and not assign:
            PLMClientsBusinessEntities.CodeInfo codeInfo = new PLMClientsBusinessEntities.CodeInfo();
            codeInfo.PrefixId = codePrefixInfo.PrefixId;
            codeInfo.CodeString = codeString;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.INACTIVO;
            codeInfo.Assign = false;

            // Add Code to the database:
            CodesBLC.Instance.addCode(codeInfo);

            // Increment the counter:
            ++codePrefixInfo.CurrentValue;

            // Update the increment in the database:
            CodePrefixesBLC.Instance.updateCode(codePrefixInfo);

            // Add the create transaction:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.CREAR;

            // Add the create tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            //Return the code has been created:
            return codeInfo;
        }

        public PLMClientsBusinessEntities.CodeInfo generateCode(int clientId, string codePrefix)
        {
            // Get CodePrefixInfo object by codePrefix varible:
            PLMClientsBusinessEntities.CodePrefixInfo codePrefixInfo = CodePrefixesBLC.Instance.getCodePrefix(codePrefix);

            if (codePrefixInfo == null)
                throw new ArgumentException("The prefix does not exist.");

            // Create the new code:
            string codeString = new PLMCryptographyComponent.CryptographyComponent().addHash(codePrefixInfo.PrefixValue.Trim() + codePrefixInfo.CurrentValue.ToString());

            // Add new code to the database as inactive and not assign:
            PLMClientsBusinessEntities.CodeInfo codeInfo = new PLMClientsBusinessEntities.CodeInfo();
            codeInfo.PrefixId = codePrefixInfo.PrefixId;
            codeInfo.CodeString = codeString;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.INACTIVO;
            codeInfo.Assign = false;

            // Add Code to the database:
            CodesBLC.Instance.addCode(codeInfo);

            // Increment the counter:
            ++codePrefixInfo.CurrentValue;

            // Update the increment in the database:
            CodePrefixesBLC.Instance.updateCode(codePrefixInfo);

            // Add the create transaction:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.CREAR;

            // Add the create tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            // Associate code to the user, then activate it:
            PLMClientsBusinessEntities.ClientCodesInfo clientCodeInfo = new PLMClientsBusinessEntities.ClientCodesInfo();
            clientCodeInfo.ClientId = clientId;
            clientCodeInfo.CodeId = codeInfo.CodeId;

            // Add user and code association to the database:
            ClientCodesBLC.Instance.insert(clientCodeInfo);

            // Activate the new code which was created:
            codeInfo.Assign = true;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO;

            // Update the new code in the database:
            CodesBLC.Instance.updateCodes(codeInfo);

            // Add activate tran:
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.ACTIVAR;

            // Add activate tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            //Return the code has been created:
            return codeInfo;
        }

        public PLMClientsBusinessEntities.CodeInfo createCode(int clientId, string codePrefix)
        {
            // Get CodePrefixInfo object by codePrefix varible:
            PLMClientsBusinessEntities.CodePrefixInfo codePrefixInfo = CodePrefixesBLC.Instance.getCodePrefix(codePrefix);

            // Create the new code:
            string codeString = new PLMCryptographyComponent.CryptographyComponent().addHash(codePrefixInfo.PrefixValue.Trim() + codePrefixInfo.CurrentValue.ToString());

            // Add new code to the database as inactive and not assign:
            PLMClientsBusinessEntities.CodeInfo codeInfo = new PLMClientsBusinessEntities.CodeInfo();
            codeInfo.PrefixId = codePrefixInfo.PrefixId;
            codeInfo.CodeString = codeString;
            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.INACTIVO;
            codeInfo.Assign = false;

            // Add Code to the database:
            CodesBLC.Instance.addCode(codeInfo);

            // Increment the counter:
            ++codePrefixInfo.CurrentValue;

            // Update the increment in the database:
            CodePrefixesBLC.Instance.updateCode(codePrefixInfo);

            // Add the create transaction:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.CREAR;

            // Add the create tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);

            // Associate code to the user, then activate it:
            PLMClientsBusinessEntities.ClientCodesInfo clientCodeInfo = new PLMClientsBusinessEntities.ClientCodesInfo();
            clientCodeInfo.ClientId = clientId;
            clientCodeInfo.CodeId = codeInfo.CodeId;

            // Add user and code association to the database:
            ClientCodesBLC.Instance.insert(clientCodeInfo);

            // Activate the new code which was created:
            codeInfo.Assign = true;
            
            // Update the new code in the database:
            CodesBLC.Instance.updateCodes(codeInfo);

            //Return the code has been created:
            return codeInfo;
        }

        public void activateCode(string code)
        {
            PLMClientsBusinessEntities.CodeInfo codeInfo = new CodeInfo();
            codeInfo = PLMClientsBusinessLogicComponent.CodesBLC.Instance.getCode(code);

            codeInfo.CodeStatusId = (byte)PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO;

            PLMClientsBusinessLogicComponent.CodesBLC.Instance.updateCodes(codeInfo);

            // Add the create transaction:
            PLMClientsBusinessEntities.CodeTransactionInfo codeTranInfo = new PLMClientsBusinessEntities.CodeTransactionInfo();
            codeTranInfo.CodeId = codeInfo.CodeId;
            codeTranInfo.TransactionId = (byte)PLMClientsBusinessEntities.Catalogs.CodeTransactions.ACTIVAR;

            // Add the create tran to the database:
            CodeTransactionsBLC.Instance.addTransaction(codeTranInfo);
        }

        public bool checkClientCode(string code)
        {
            return PLMClientsDataAccessComponent.CodesDALC.Instance.checkClientCode(code);
        }

        public PLMClientsBusinessEntities.CodeInfo getByEmailByPrefix(string email, string prefix)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, System.Configuration.ConfigurationManager.AppSettings["RegExpression"]))
                throw new Exception("El correo electrónico no tiene el formato correcto.");

            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException("Prefix no válido");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByEmailByPrefix(email, prefix);
        }

        public PLMClientsBusinessEntities.CodeInfo getCodeByLicenseKey(string license)
        {
            if(string.IsNullOrEmpty(license))
                throw new ArgumentNullException("license no válida");

            return PLMClientsDataAccessComponent.CodesDALC.Instance.getByLicenseKey(license);
        }

        //GetTargetByCode
        public TargetOutputsInfo getTargetByCodeString(string codeString)
        {
            return PLMClientsDataAccessComponent.CodesDALC.Instance.getTargetByCodeString(codeString);
        }
        
        #endregion

        #region Private Methods

        private CodeInfo checkCode(string codeString)
        {
            if (codeString.Contains("0") || codeString.Contains("O") || codeString.Contains("o"))
            {
                codeString = codeString.Replace("0", "*");
                codeString = codeString.Replace("O", "*");
                codeString = codeString.Replace("o", "*");

                return PLMClientsDataAccessComponent.CodesDALC.Instance.getByCodeString(codeString);

            }

            return null;
        }

        #endregion

        public static readonly CodesBLC Instance = new CodesBLC();

    }
}
