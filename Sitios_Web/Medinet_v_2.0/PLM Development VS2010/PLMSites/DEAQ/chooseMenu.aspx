<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chooseMenu.aspx.cs" Inherits="chooseMenu" MasterPageFile="~/deaq.master" %>


  <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
      <script type="text/javascript">
          function redi(bb) {
              alert("jjj");
              //$("#Btn").click();
          }
          $(document).ready(function () {

              activeItem = $("#accordion:first");
              $(activeItem).addClass('active');

              $("#accordion").hover(function () {
                  $(activeItem).animate({ width: "50px" }, { duration: 300, queue: false });
                  $(this).animate({ width: "450px" }, { duration: 300, queue: false });
                  activeItem = this;
              });

          });
</script>
      <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="imgBtnBackLabs_Click" Visible="false" />
    <div class="wrapper" id="">
  <nav class="vertical">
    <ul>
      <li>
        <a id="titulo" runat="server" href="#" > </a>
        <div>
          <ul id="hhh" runat="server">
      
            
          </ul>
        </div>
      </li>
     
    </ul>
  </nav>
 
</div>


      </asp:Content> 