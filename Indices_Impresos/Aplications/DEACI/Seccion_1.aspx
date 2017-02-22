<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seccion_1.aspx.cs" Inherits="Seccion_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href='../estilos.css' rel='stylesheet' type='text/css' />
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left;">
    
        <table width="100%">
            <tr>
                <td>
                    <p class="SeccionTitulo">
                        <asp:Label ID="lblSectionName" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                   &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Repeater ID="rptSubSections" runat="server" OnItemDataBound="rptSubSections_ItemDataBound">
                    
                        <ItemTemplate> 
                        
                            <span><%# getLink(DataBinder.Eval(Container.DataItem, "SubSectionId").ToString(), DataBinder.Eval(Container.DataItem, "Description").ToString(), DataBinder.Eval(Container.DataItem, "SectionId").ToString()) %></span><br />
                            <%--<a class="linksSecciones" href="<%# Eval("SectionId") %>.htm" style='text-decoration:none'><%# Eval("Description") %>.</a><br />--%>
                            <%--<span class="linksSecciones"><%# Eval("Description") %>.</span><br />--%>
                            
                             
                             <asp:Repeater ID="rptSubSubSection" runat="server">
                            
                                <ItemTemplate> 
                                    
                                    <blockquote>
                                        <%--<a class="linksSubSecciones" href="<%# Eval("SectionId") %>.htm" style='text-decoration:none'><%# Eval("Description") %>.</a><br />--%>
                                        <span><%# getLink(DataBinder.Eval(Container.DataItem, "SubSectionId").ToString(), DataBinder.Eval(Container.DataItem, "Description").ToString(), DataBinder.Eval(Container.DataItem, "SectionId").ToString()) %></span>
                                        
                                    </blockquote>
                                
                                </ItemTemplate> 
                                
                            </asp:Repeater>
                            <br />
                        </ItemTemplate> 
                    
                    </asp:Repeater> 
                
                </td>
            </tr>
            
        </table>
       
    </div>
    </form>
</body>
</html>
