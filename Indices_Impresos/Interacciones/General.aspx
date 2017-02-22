<%@ Page Language="C#" AutoEventWireup="true" CodeFile="General.aspx.cs" Inherits="General" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     
        <div>    
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0"  Text="INTERACCIONES Y  CONTRAINDICACIONES"></asp:Label><br />

        <br />
        <br />

<Table>
    <tr>
      <td>  <asp:Label id="lblEditionI" runat="server">Edici&oacute;n de Interacciones:   </asp:Label></td>
     <td> <asp:TextBox ID="txtEditionInt" runat ="server"> </asp:TextBox> </td>
    <td> <asp:Button ID="btnInte" runat="server" Text="Generar" ValidationGroup="IndexesI" OnClick="btnInt_Click" /> 
        <asp:RequiredFieldValidator ID="rfvInt" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="IndexesI" ControlToValidate="txtEditionInt" SetFocusOnError="true" ></asp:RequiredFieldValidator>

    </td>
     </tr>
    <tr>
        <td> <asp:Label id="lblEditionC" runat="server">Edici&oacute;n de Contraindicaciones:  </asp:Label> </td>
        <td> <asp:TextBox ID="txtEditionCont" runat ="server"> </asp:TextBox> </td>
        <td> <asp:Button ID="btnCont" runat="server" Text="Generar" ValidationGroup="IndexesC"  OnClick="btnCont_Click"/> 
            
             <asp:RequiredFieldValidator ID="rfvCont" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="IndexesC" ControlToValidate="txtEditionCont" SetFocusOnError="true" ></asp:RequiredFieldValidator>

        </td>
    </tr>
         
     <tr>
        <td> <asp:Label id="lblEditionIMSubstanceFoods" runat="server">Edici&oacute;n de IMSubstanceFoods:  </asp:Label> </td>
        <td> <asp:TextBox ID="txtEditionIMSF" runat ="server"> </asp:TextBox> </td>
        <td> <asp:Button ID="btnIMSF" runat="server" Text="Generar" ValidationGroup="IndexesIMSF"  OnClick="btnIMSF_Click"/> 
            
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="IndexesIMSF" ControlToValidate="txtEditionIMSF" SetFocusOnError="true" ></asp:RequiredFieldValidator>

        </td>
    </tr>
         
    <tr>
        <td> <asp:Label id="Label1" runat="server">Indice de productos ICD y ATC:  </asp:Label> </td>
        <td> <asp:TextBox ID="txtEditionPICDATC" runat ="server"> </asp:TextBox> </td>
        <td> <asp:Button ID="btnPIACT" runat="server" Text="Generar" ValidationGroup="IndexesATCICD"  OnClick="btnPIACT_Click"/> 
            
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="IndexesATCICD" ControlToValidate="txtEditionPICDATC" SetFocusOnError="true" ></asp:RequiredFieldValidator>

        </td>
    </tr>

</Table>
       </div>

    </form>
</body>
</html>
