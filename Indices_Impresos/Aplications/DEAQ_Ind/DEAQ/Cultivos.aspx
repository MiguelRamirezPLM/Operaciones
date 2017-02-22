<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cultivos.aspx.cs" Inherits="DEAQ_Cultivos" %>

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
            ForeColor="#0000C0" Text="INFORMACIÓN DE CULTIVOS"></asp:Label><br />
                
        <br />
        <br />
  
 <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
 <ItemTemplate>       
          
    <asp:Repeater runat="server" ID="rptCultivo" OnItemDataBound="rptCultivo_ItemDataBound" >
    <ItemTemplate>
        <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False" ForeColor="red" Text="Cultivo"></asp:Label>  
        <p><asp:Label runat="server" ID="lblCultivo" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "CropName")%></asp:Label><br />
        <asp:Label runat="server" ID="lblCCultivo" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ScientificName")%></asp:Label></p> 
            
                <asp:Repeater runat="server" ID="rptPlaga" OnItemDataBound="rptPlaga_ItemDataBound" >
                <ItemTemplate>  
                    <br />
                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="#0000C0" Text="PLAGAS"></asp:Label><br />  
                        <asp:Label runat="server" ID="Label3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "PestName")%></asp:Label><br />

                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="RED" Text="PLAGAS Tabla"></asp:Label><br />  
                    
                            <asp:Repeater runat="server" ID="rptInformacionPlagas" OnItemDataBound="rptInformacionPlagas_ItemDataBound" >
                            <ItemTemplate>  
                                <asp:Label runat="server" ID="Labele3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "FightName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Labelf3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ActiveSubstanceName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Label4" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label><br /><br />
                              <%--<asp:Label runat="server" ID="Label3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "FightName")%></asp:Label><br />--%>
                            </ItemTemplate>
                            </asp:Repeater>  

                 </ItemTemplate>
                 </asp:Repeater> 
        
                <asp:Repeater runat="server" ID="rptEnfermedad" OnItemDataBound="rptEnfermedad_ItemDataBound" >
                <ItemTemplate>  
                    <br />
                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="PINK" Text="ENFERMEDAD"></asp:Label><br />  
                      <asp:Label runat="server" ID="Label3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "DiseaseName")%></asp:Label><br />

                      <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="RED" Text="ENFERMEDAD Tabla"></asp:Label><br />  
                    
                            <asp:Repeater runat="server" ID="rptInformacionEnfermedades" OnItemDataBound="rptInformacionEnfermedades_ItemDataBound" >
                            <ItemTemplate>  
                                <asp:Label runat="server" ID="Labele3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "FightName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Labelf3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ActiveSubstanceName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Label4" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label><br /><br />
                            </ItemTemplate>
                            </asp:Repeater>   

                 </ItemTemplate>
                 </asp:Repeater> 
           
                <asp:Repeater runat="server" ID="rptMalezas" OnItemDataBound="rptMalezas_ItemDataBound" >
                <ItemTemplate>  
                    <br />
                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="GREEN" Text="MALEZAS"></asp:Label><br />  
                      <asp:Label runat="server" ID="Label3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "WeedName")%></asp:Label><br />

                      <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="15px" Font-Strikeout="False"  ForeColor="RED" Text="MALEZAS Tabla"></asp:Label><br />  
                    
                       <asp:Repeater runat="server" ID="rptInformacionMalezas" OnItemDataBound="rptInformacionMalezas_ItemDataBound" >
                            <ItemTemplate>  
                                <asp:Label runat="server" ID="Labele3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "FightName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Labelf3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ActiveSubstanceName")%></asp:Label><br />
                                <asp:Label runat="server" ID="Label4" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label><br /><br />
                            </ItemTemplate>
                            </asp:Repeater>  

                 </ItemTemplate>
                 </asp:Repeater> 

             
       </ItemTemplate>
       </asp:Repeater> 
               
  </ItemTemplate>
  </asp:Repeater>   
                   
 
          
 
    </div>
    </form>
</body>
</html>
