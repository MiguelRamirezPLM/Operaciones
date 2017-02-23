<%@ Page Language="C#" MasterPageFile="~/deaq.master" AutoEventWireup="true" CodeFile="classifiedProducts.aspx.cs" Inherits="classifiedProducts" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/deaq.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

 
  
 <script type="text/javascript" src="Scripts/jquery-ui.js"></script>


    <script language="javascript" type="text/javascript">

        var rowKey = -1;
        var rowValue = "";
        var otherGrid;
        var sourceGrid
      
        
        function rbGoToServer(btn) {
            
           
        }

    </script>
    
    <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="imgBtnBackLabs_Click" />
    <br />
    <br />
    <asp:Label ID="lblProd" runat="server" Text="Producto: " Font-Bold="true"></asp:Label>
    <asp:Label ID="lblProdName" runat="server" Font-Bold="true"></asp:Label>
    <br />
    <asp:Label ID="lblPharma" runat="server" Text="Forma Farmacéutica: " Font-Bold="true"></asp:Label>
    <asp:Label ID="lblPharmaName" runat="server" Font-Bold="true"></asp:Label>
    <div >
			
			<section class="ac-container"  >
			
                <div>
					<input id="ac1" name="accordion-1" runat="server" class="hide" value="Radio1" type="radio" onclick="rbGoToServer(this);" clientidmode="Static"/>
					<label for="ac1" >Usos Agroquímicos</label>
					<article class="ac-large"  >
                        
					
                                <div style=" float:left; position:relative; top:130px; left:160px; ">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnConsult">
                                     <asp:Label ID="Label7" runat="server" Text="Buscar Uso Agroquímico" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" style="position:relative; top:-30px; left:-30px;" />
                                    <asp:TextBox ID="txtAgroName" runat="server" Text="" Enabled="true" style="position:relative; top:-30px; left:-30px;"  />
                    <asp:Button ID="btnConsult" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" style="position:relative; top:-30px; left:-30px;"/>
                                        </asp:Panel>
                               <asp:Label ID="Label1" runat="server" Text="No. Registros" style=" float:left; position:relative; left:-35px; top:-20px; "></asp:Label>
                             
                                <asp:Label ID="Label3" runat="server" Text="Catálogo de Usos Agroquímicos" style=" float:left; position:relative; top:-95px; left:-130px; " Font-Bold="true"></asp:Label>

                                         <asp:DropDownList ID="ddlAgroPageSizeFrom" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" 
        OnSelectedIndexChanged="ddlPageSizeFrom_SelectedIndexChanged" style=" float:left; position:relative; top:-20px; left:-220px;  ">
        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
        <asp:ListItem Value="50" >50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>             
               
                                <asp:GridView ID="agrochemicalGridFrom" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
      DataKeyNames="AgrochemicalUseId" style="float:left; position:relative; left:-40px; top:0px;" OnPageIndexChanging="grdProducts_PageIndexChanging">
                                    <Columns>
                                     <asp:BoundField DataField="AgrochemicalUseId" Visible="False" />
                                         <asp:TemplateField HeaderText="Uso Agroquímico">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="200px" CssClass="draggableRow right" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroFrom" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "AgrochemicalUseName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agregar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
            
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/agregar.png" Width="20px" Text="Editar Logos" Visible="true" OnClick="btnAdd_Click"  />
             
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    

                                </asp:GridView>

                      
                                </div>
                                &nbsp;&nbsp;&nbsp;
                               <div style="position:relative; top:130px; left:300px;">
                                <asp:Label ID="Label4" runat="server" Text="Usos Agroquímicos Asociados" style=" position:relative; top: -40px; left:-0px;  " Font-Bold="true"></asp:Label>
                                 
                                <asp:GridView ID="agrochemicalGridTo" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller"  DataKeyNames="ProductId,AgrochemicalUseId" style="float:left; position:relative;  " >
                                    <Columns>
                                        <asp:BoundField DataField="ProductId" Visible="False" />
                                     <asp:BoundField DataField="AgrochemicalUseId" Visible="False" />
                                        <asp:TemplateField HeaderText="Producto">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroProdName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Uso Agroquímico">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroTo" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "AgrochemicalUseName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" Text="Editar Logos" Visible="true" OnClick="btnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    
                                </asp:GridView>
                                    
                                </div>
                         
                          
                    </article>
				</div>


                <div>
					<input id="ac2" name="accordion-1" runat="server" class="hide" value="Radio2" type="radio" onclick="rbGoToServer(this);" clientidmode="Static"/>
					<label for="ac2" >Cultivos</label>
					<article class="ac-large"  >
                        
					                      <div style=" float:left; position:relative; top:130px; left:160px; " >
                                              <asp:Panel ID="pnlSearch" runat="server" DefaultButton="Button1">
                                     <asp:Label ID="Label2" runat="server" Text="Buscar Cultivo" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" style="position:relative; top:-30px; left:-30px;"  />
                                    <asp:TextBox ID="txtCropName" runat="server" Text="" Enabled="true" style="position:relative; top:-30px; left:-30px; " />
                                                  
                    <asp:Button ID="Button1" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" style="position:relative; top:-30px; left:-30px;" />
                                                  </asp:Panel>
                               <asp:Label ID="Label5" runat="server" Text="No. Registros" style=" float:left; position:relative; left:-35px; top:-20px; "></asp:Label>
                             
                                <asp:Label ID="Label6" runat="server" Text="Catálogo de Cultivos" style=" float:left; position:relative; top:-95px; left:-130px; " Font-Bold="true"></asp:Label>

                                         <asp:DropDownList ID="ddlCropPageSizeFrom" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" 
        OnSelectedIndexChanged="ddlPageSizeFrom_SelectedIndexChanged" style=" float:left; position:relative; top:-20px; left:-170px;  ">
        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
        <asp:ListItem Value="50" >50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>             
               
                                <asp:GridView ID="cropGridFrom" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
      DataKeyNames="CropId" style="float:left; position:relative; left:-40px; top:0px;" OnPageIndexChanging="grdProducts_PageIndexChanging">
                                    <Columns>
                                     <asp:BoundField DataField="CropId" Visible="False" />
                                         <asp:TemplateField HeaderText="Cultivo">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="200px" CssClass="draggableRow right" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroFrom" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "CropName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agregar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
            
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/agregar.png" Width="20px" Text="Editar Logos" Visible="true" OnClick="btnAdd_Click"  />
             
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    

                                </asp:GridView>

                      
                                </div>
                                &nbsp;&nbsp;&nbsp;
                               <div style="position:relative; top:130px; left:300px;">
                                <asp:Label ID="Label8" runat="server" Text="Cultivos Asociados" style=" position:relative; top: -40px; left:-0px;  " Font-Bold="true"></asp:Label>
                                 
                                <asp:GridView ID="cropGridTo" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller"  DataKeyNames="ProductId,CropId" style="float:left; position:relative;  " >
                                    <Columns>
                                        <asp:BoundField DataField="ProductId" Visible="False" />
                                     <asp:BoundField DataField="CropId" Visible="False" />
                                        <asp:TemplateField HeaderText="Producto">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblCropProdName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cultivo">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroTo" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "CropName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" Text="Editar Logos" Visible="true" OnClick="btnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    
                                </asp:GridView>
                                    
                                </div>
                    </article>
				</div>




                <div>
					<input id="ac3" name="accordion-1" runat="server" value="Radio3" class="hide" type="radio" onclick="rbGoToServer(this);" clientidmode="Static" />
					<label for="ac3" >Semillas</label>
					<article class="ac-large"  >
                        
						                      <div style=" float:left; position:relative; top:130px; left:160px; ">
                                                   <asp:Panel ID="Panel2" runat="server" DefaultButton="Button2">
                                     <asp:Label ID="Label9" runat="server" Text="Buscar Semilla" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" style="position:relative; top:-30px; left:-30px;" />
                                    <asp:TextBox ID="txtSeedName" runat="server" Text="" Enabled="true" style="position:relative; top:-30px; left:-30px;" />
                    <asp:Button ID="Button2" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" style="position:relative; top:-30px; left:-30px;"/>
                                                       </asp:Panel>
                               <asp:Label ID="Label10" runat="server" Text="No. Registros" style=" float:left; position:relative; left:-35px; top:-20px; "></asp:Label>
                             
                                <asp:Label ID="Label11" runat="server" Text="Catálogo de Semillas" style=" float:left; position:relative; top:-95px; left:-130px; " Font-Bold="true"></asp:Label>

                                         <asp:DropDownList ID="ddlSeedPageSizeFrom" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" 
        OnSelectedIndexChanged="ddlPageSizeFrom_SelectedIndexChanged" style=" float:left; position:relative; top:-20px; left:-170px;  ">
        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
        <asp:ListItem Value="50" >50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>             
               
                                <asp:GridView ID="seedGridFrom" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
      DataKeyNames="SeedId" style="float:left; position:relative; left:-40px; top:0px;" OnPageIndexChanging="grdProducts_PageIndexChanging">
                                    <Columns>
                                     <asp:BoundField DataField="SeedId" Visible="False" />
                                         <asp:TemplateField HeaderText="Semilla">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="200px" CssClass="draggableRow right" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroFrom" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "SeedName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agregar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
            
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/agregar.png" Width="20px" Text="Editar Logos" Visible="true" OnClick="btnAdd_Click"  />
             
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    

                                </asp:GridView>

                      
                                </div>
                                &nbsp;&nbsp;&nbsp;
                               <div style="position:relative; top:130px; left:300px;">
                                <asp:Label ID="Label12" runat="server" Text="Semillas Asociadas" style=" position:relative; top: -40px; left:-0px;  " Font-Bold="true"></asp:Label>
                                 
                                <asp:GridView ID="seedGridTo" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller"  DataKeyNames="ProductId,SeedId" style="float:left; position:relative;  " >
                                    <Columns>
                                        <asp:BoundField DataField="ProductId" Visible="False" />
                                     <asp:BoundField DataField="SeedId" Visible="False" />
                                        <asp:TemplateField HeaderText="Producto">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroProdName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Semillas">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroTo" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "SeedName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" Text="Editar Logos" Visible="true" OnClick="btnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    
                                </asp:GridView>
                                    
                                </div>
                    </article>
				</div>





                <div>
					<input id="ac4" name="accordion-1" runat="server" value="Radio4" class="hide" type="radio" onclick="rbGoToServer(this);" clientidmode="Static"/>
					<label for="ac4" >Ingredientes Activos</label>
					<article class="ac-large"  >
                        
				                                          <div style=" float:left; position:relative; top:130px; left:160px; ">
                                                               <asp:Panel ID="Panel3" runat="server" DefaultButton="Button3">
                                     <asp:Label ID="Label13" runat="server" Text="Buscar Sustancia Activa" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" style="position:relative; top:-30px; left:-30px;" />
                                    <asp:TextBox ID="txtSubsName" runat="server" Text="" Enabled="true" style="position:relative; top:-30px; left:-30px;" />
                    <asp:Button ID="Button3" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" style="position:relative; top:-30px; left:-30px;"/>
                                                                   </asp:Panel>
                               <asp:Label ID="Label14" runat="server" Text="No. Registros" style=" float:left; position:relative; left:-35px; top:-20px; "></asp:Label>
                             
                                <asp:Label ID="Label15" runat="server" Text="Catálogo de Sustancias Activas" style=" float:left; position:relative; top:-95px; left:-130px; " Font-Bold="true"></asp:Label>

                                         <asp:DropDownList ID="ddlSubsPageSizeFrom" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" 
        OnSelectedIndexChanged="ddlPageSizeFrom_SelectedIndexChanged" style=" float:left; position:relative; top:-20px; left:-220px;  ">
        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
        <asp:ListItem Value="50" >50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>             
               
                                <asp:GridView ID="substanceGridFrom" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
      DataKeyNames="ActiveSubstanceId" style="float:left; position:relative; left:-40px; top:0px;" OnPageIndexChanging="grdProducts_PageIndexChanging">
                                    <Columns>
                                     <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                                         <asp:TemplateField HeaderText="Sustancia Activa">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="200px" CssClass="draggableRow right" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroFrom" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ActiveSubstanceName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agregar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"  />
                <ItemStyle  Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
            
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/agregar.png" Width="20px" Text="Editar Logos" Visible="true" OnClick="btnAdd_Click"  />
             
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    

                                </asp:GridView>

                      
                                </div>
                                &nbsp;&nbsp;&nbsp;
                               <div style="position:relative; top:130px; left:300px;">
                                <asp:Label ID="Label16" runat="server" Text="Sustancias Activas Asociadas" style=" position:relative; top: -40px; left:-0px;  " Font-Bold="true"></asp:Label>
                                 
                                <asp:GridView ID="substanceGridTo" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller"  DataKeyNames="ProductId,ActiveSubstanceId" style="float:left; position:relative;  " >
                                    <Columns>
                                        <asp:BoundField DataField="ProductId" Visible="False" />
                                     <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                                        <asp:TemplateField HeaderText="Producto">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroProdName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Sustancia Activa">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblAgroTo" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ActiveSubstanceName") %>
                    </asp:Label></ItemTemplate>
            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" Text="Editar Logos" Visible="true" OnClick="btnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
                                        </Columns>
                                    
                                </asp:GridView>
                                    
                                </div>     
                    </article>
				</div>



                </section>
        </div>

     
    </asp:Content>