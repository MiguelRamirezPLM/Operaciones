using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Web;

using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CommentsBLC
    {

        #region Constructors

        private CommentsBLC() { }

        #endregion

        #region Public Methods

        public int addComment(PLMClientsBusinessEntities.CommentsInfo BEntity)
        {
            PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail = new PLMClientsBusinessEntities.CommentTypeDetailInfo();
            
            commentDetail = 
                PLMClientsDataAccessComponent.CommentsDALC.Instance.getCommentType(BEntity.CommentTypeId, BEntity.BranchId, BEntity.BusinessUnitId, BEntity.DistributionId, BEntity.PrefixId, BEntity.TargetId);

            BEntity.CommentId = PLMClientsDataAccessComponent.CommentsDALC.Instance.insert(BEntity);

            sendCommentDelegate delegateMethod = new sendCommentDelegate(sendEmailComment);

            delegateMethod.BeginInvoke(BEntity, commentDetail, null, null);
            
            return BEntity.CommentId;
        }

        public void updateComment(PLMClientsBusinessEntities.CommentsInfo BEntity)
        {
            PLMClientsDataAccessComponent.CommentsDALC.Instance.update(BEntity);
        }

        public PLMClientsBusinessEntities.CommentsInfo getComment(int commentId)
        {
            return PLMClientsDataAccessComponent.CommentsDALC.Instance.getOne(commentId);
        }

        public List<PLMClientsBusinessEntities.CommentTypeDetailInfo> getCommentTypesByPrefix(byte targetId, string prefix)
        {
            return PLMClientsDataAccessComponent.CommentsDALC.Instance.getCommentTypesByPrefix(targetId, prefix);
        }
        
        public List<PLMClientsBusinessEntities.CommentTypeDetailInfo> getCommentTypesByPrefixByBranch(byte targetId, string prefix,int branchId)
        {
            return PLMClientsDataAccessComponent.CommentsDALC.Instance.getCommentTypesByPrefixByBranch(targetId, prefix,branchId);
        }

        public List<PLMClientsBusinessEntities.CommentTypeDetailInfo> getCommentTypesByPrefixByCountry(byte targetId, string prefix, string country)
        {
            return PLMClientsDataAccessComponent.CommentsDALC.Instance.getCommentTypesByPrefixByCountry(targetId, prefix, country);
        }
        
        public int addClientComment(PLMClientsBusinessEntities.CommentsInfo BEntity, string code)
        {
            PLMClientsBusinessEntities.ClientInfo clientInfo = new ClientInfo();

            PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail = 
                PLMClientsDataAccessComponent.CommentsDALC.Instance.getCommentType(BEntity.CommentTypeId, BEntity.BranchId, BEntity.BusinessUnitId, BEntity.DistributionId, BEntity.PrefixId, BEntity.TargetId);

            BEntity.CommentId = PLMClientsDataAccessComponent.CommentsDALC.Instance.insert(BEntity);

            if (code != null)
            {
                PLMClientsBusinessEntities.TargetClientCodesInfo targetClientCode =
                    PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

                if (targetClientCode != null)
                {
                    PLMClientsBusinessEntities.TargetClientCommentsInfo targetClientComment = new TargetClientCommentsInfo();
                    targetClientComment.ClientId = targetClientCode.ClientId;
                    targetClientComment.CodeId = targetClientCode.CodeId;
                    targetClientComment.DeviceId = targetClientCode.DeviceId;
                    targetClientComment.TargetId = targetClientCode.TargetId;
                    targetClientComment.CommentId = BEntity.CommentId;
                    PLMClientsDataAccessComponent.TargetClientCommentsDALC.Instance.insert(targetClientComment);

                    clientInfo = PLMClientsDataAccessComponent.ClientsDALC.Instance.getOne(targetClientCode.ClientId);
                }
                else
                    throw new ArgumentException("The Code is not valid.");
            }

            sendClientCommentDelegate delegateMethod = new sendClientCommentDelegate(sendEmailClientComment);
            delegateMethod.BeginInvoke(BEntity, commentDetail, clientInfo, null, null);

            return BEntity.CommentId;
        }

        #endregion

        #region Private Methods

        private string getFileContent(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);

            string fileContent = sr.ReadToEnd();
            sr.Close();

            return fileContent;
        }

        private void sendEmailComment(PLMClientsBusinessEntities.CommentsInfo commentInfo, PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail)
        {
            // Get the HtmlTemplate:
            string htmlContent =
                this.getFileContent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\HtmlTemplates\\SendComment.htm");

            StringBuilder sbHtmlContent = new StringBuilder(htmlContent);

            sbHtmlContent.Replace("@@@DistributionName@@@", commentDetail.DistributionName);
            sbHtmlContent.Replace("@@@TargetName@@@", commentDetail.TargetName);
            sbHtmlContent.Replace("@@@CommentTypeName@@@", commentDetail.TypeName);
            sbHtmlContent.Replace("@@@CommentTypeDescription@@@", commentDetail.TypeDescription);
            sbHtmlContent.Replace("@@@CommentContent@@@", commentInfo.Content);

            this._email.sendHTMLMail(commentDetail.Email, "Móviles PLM", "Comentario " + commentDetail.DistributionName, sbHtmlContent.ToString(), true);

            commentInfo.SentDate = DateTime.Now;
            PLMClientsDataAccessComponent.CommentsDALC.Instance.update(commentInfo);
        }

        private void sendEmailClientComment(PLMClientsBusinessEntities.CommentsInfo commentInfo, PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail, PLMClientsBusinessEntities.ClientInfo clientInfo)
        {
            // Get the HtmlTemplate:
            string htmlContent =
                this.getFileContent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\HtmlTemplates\\SendClientComment.htm");

            StringBuilder sbHtmlContent = new StringBuilder(htmlContent);

            sbHtmlContent.Replace("@@@DistributionName@@@", commentDetail.DistributionName);
            sbHtmlContent.Replace("@@@TargetName@@@", commentDetail.TargetName);
            sbHtmlContent.Replace("@@@CommentTypeName@@@", commentDetail.TypeName);
            sbHtmlContent.Replace("@@@CommentTypeDescription@@@", commentDetail.TypeDescription);
            
            if(clientInfo.CompleteName != null)
                sbHtmlContent.Replace("@@@ClientName@@@", clientInfo.CompleteName);
            else
                sbHtmlContent.Replace("@@@ClientName@@@", "Cliente no registrado");

            if (clientInfo.Email != null)
                sbHtmlContent.Replace("@@@ClientEmail@@@", clientInfo.Email);
            else
                sbHtmlContent.Replace("@@@ClientEmail@@@", "Cliente no registrado");

            sbHtmlContent.Replace("@@@CommentContent@@@", commentInfo.Content);

            this._email.sendHTMLMail(commentDetail.Email, "Móviles PLM", "Comentario " + commentDetail.DistributionName, sbHtmlContent.ToString(), true);

            commentInfo.SentDate = DateTime.Now;
            PLMClientsDataAccessComponent.CommentsDALC.Instance.update(commentInfo);
        }

        #endregion

        public static readonly CommentsBLC Instance = new CommentsBLC();
        private PLMEmailComponent.EmailComponent _email = new PLMEmailComponent.EmailComponent();
        private delegate void sendCommentDelegate(PLMClientsBusinessEntities.CommentsInfo commentInfo, PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail);
        private delegate void sendClientCommentDelegate(PLMClientsBusinessEntities.CommentsInfo commentInfo, PLMClientsBusinessEntities.CommentTypeDetailInfo commentDetail, PLMClientsBusinessEntities.ClientInfo clientInfo);

    }
}
