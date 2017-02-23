<%@ Page Language="C#" AutoEventWireup="true" CodeFile="downFile.aspx.cs" Inherits="downFile" %>

<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>downs</title>
</head>

<body>
                
    <form id="form1" runat="server">



    <p>
        &nbsp;</p>
<script type="text/javascript" language="javascript">
    
   function mostrar_procesar() {
        //document.getElementById('procesando_div').style.display = '';
        if (document.getElementById) {
            document.getElementById('progressMessage').style.display = '';
            document.getElementById('screenLock').style.display = '';
            var progressMessage = document.getElementById('progressMessage');
            var screenLock = document.getElementById('screenLock');


            progressMessage.style.backgroundColor = 'white';
            progressMessage.style.position = 'relative';
            screenLock.style.width = '100%';
            screenLock.style.backgroundColor = '#F2F2F2';
            screenLock.style.height = '200%';
            screenLock.style.position = 'absolute';
            progressMessage.style.width = '400px';
            progressMessage.style.height = '80px';
            progressMessage.style.top = document.documentElement.clientHeight / 3 - progressMessage.style.height.replace('px', '') / 2 + 'px';
            progressMessage.style.left = document.body.offsetWidth / 2 - progressMessage.style.width.replace('px', '') / 2 + 'px';

        }

    }
    
    </script>

     <div  id="screenLock" style='display: none;filter:alpha(opacity=50); opacity:0.5;'   >
                    </div>
                <div id="progressMessage" style='display: none;border: solid 1px gray;'>
                <table style="text-align:center;">
                <tr >
                <td style="text-align:center;"><img class="style1" src="images/loading.gif" style="height: 65px; width: 85px"/></td>
                <td style="text-align:center;">Generando archivo, por favor espere...</td>
                </tr>
                </table>
                    
                    
                </div>
                

    <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
    <div style="text-align: center">
    
    <table>
    <tr>
    <td colspan="2">
    
     <%--<asp:UpdatePanel ID="udpReport" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                <Triggers>
                <asp:PostBackTrigger ControlID="imgBtnExportPDF" />
                <asp:PostBackTrigger ControlID="imgBtnExportXls" />
               
               
                </Triggers>
                <ContentTemplate>
                
                 </ContentTemplate>
                        </asp:UpdatePanel>--%>
                       
                        </td> 
                        
    </tr>
    <tr>
    
    <td colspan="2">   
        <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="udpReport" DisplayAfter="0" DynamicLayout="true" >
                    <ProgressTemplate>
                     
                    
    <table style="text-align:center;">
                <tr >
                <td style="text-align:center;"><img class="style1" src="images/loading.gif" style="height: 65px; width: 85px"/></td>
                <td style="text-align:center;">Generando archivo, por favor espere...</td>
                </tr>
                </table>
                </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                </td>
    
    </tr>
    <tr><td align="right">
        &nbsp;</td>
    <td align="left">
     
                    &nbsp;</td>
    
    </tr>
    <tr>
    
    <td colspan="2">&nbsp;</td>
    
    </tr>
    </table>
                
    </div>
    </form>
</body>
</html>
