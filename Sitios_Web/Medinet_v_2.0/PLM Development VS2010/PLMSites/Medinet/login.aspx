<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="Scripts/jquery.alerts.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            width: 433px;
           
        }
         div.centraTabla{
        
	
	
    }

    div.centraTabla table {
      
    }
    .txtbox{
        
        width:75%;
        position:relative; left:20px;
        height:20px;
    }
    .label{
       position:relative; left:20px;
    }


    .classname {
        position:relative; left:200px;
        top:-10px;

        -moz-box-shadow:inset 0px 1px 0px 0px #ffffff;
	-webkit-box-shadow:inset 0px 1px 0px 0px #ffffff;
	box-shadow:inset 0px 1px 0px 0px #ffffff;
	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #ededed), color-stop(1, #dfdfdf) );
	background:-moz-linear-gradient( center top, #ededed 5%, #dfdfdf 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#ededed', endColorstr='#dfdfdf');
	background-color:#ededed;
	-webkit-border-top-left-radius:6px;
	-moz-border-radius-topleft:6px;
	border-top-left-radius:6px;
	-webkit-border-top-right-radius:6px;
	-moz-border-radius-topright:6px;
	border-top-right-radius:6px;
	-webkit-border-bottom-right-radius:6px;
	-moz-border-radius-bottomright:6px;
	border-bottom-right-radius:6px;
	-webkit-border-bottom-left-radius:6px;
	-moz-border-radius-bottomleft:6px;
	border-bottom-left-radius:6px;
	text-indent:20px;
	border:1px solid #dcdcdc;
	display:inline-block;
	color:#777777;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	font-style:normal;
	height:27px;
	line-height:25px;
	width:80px;
	text-decoration:none;
	text-align:center;
	text-shadow:1px 1px 0px #ffffff;
        
	background-image:url(candado.png);
        background-size:15px;
        background-repeat:no-repeat;
        background-position-x:7px;
        background-position-y:4px;
}
.classname:hover {

	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #dfdfdf), color-stop(1, #ededed) );
	background:-moz-linear-gradient( center top, #dfdfdf 5%, #ededed 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#dfdfdf', endColorstr='#ededed');
	background-color:#dfdfdf;
    	background-image:url(~\Images\candado.png);
        background-size:15px;
        background-repeat:no-repeat;
        background-position-x:7px;
        background-position-y:4px;
 
}.classname:active {
	position:relative;
	top:1px;
}

 /*-----------------------------------------*/

h1 { margin: 0; }

a {
	color: #999;
	text-decoration: none;
}

a:hover { color: #1dabb8; }

fieldset {
	border: none;
	margin: 0;
}

input {
	/*border: none;
	font-family: inherit;
	font-size: inherit;
	margin: 0;
	-webkit-appearance: none;*/
}





/* ---------- LOGIN-FORM ---------- */

#login-form {
	
	width: 456px;
    height:400px;
    
    position:relative;
    left:700px;
    top:50px;

}

#login-form h1 {
background-color: rgb(157, 34, 60);
        background-image: -webkit-linear-gradient(#193E51, #184F6E, #0B6887);
        background-image: -moz-linear-gradient(#193E51, #184F6E, #0B6887);
        background-image: -o-linear-gradient(#193E51, #184F6E, #0B6887);
        background-image: linear-gradient(#193E51, #184F6E, #0B6887);
        border-bottom: 1px solid rgba(255,255,255,.1);
        box-shadow: inset 0 1px 1px rgba(0,0,0,.1), 0 1px 1px rgba(0,0,0,.1);
        color: rgb(255,255,255);
        display: block;
        font-size: 20px;
        font-weight: 500;
        height: 60px;
        border-radius: 4px;
        line-height: 50px;
        text-shadow: 0 1px 1px rgba(0,0,0,.1);
        
        transition: all .1s ease;
        text-decoration: none;
        z-index:4;
}

#login-form fieldset {
	background: #fff;
	border-radius: 0 0 5px 5px;
	padding: 20px;
	position: relative;
}

#login-form fieldset:before {
	background-color: #fff;
	content: "";
	height: 8px;
	left: 48%;
	margin: -4px 0 0 -4px;
	position: absolute;
	top: 0;
	-webkit-transform: rotate(45deg);
	-moz-transform: rotate(45deg);
	-ms-transform: rotate(45deg);
	-o-transform: rotate(45deg);
	transform: rotate(45deg);
	width: 8px;
}

#login-form input {
	/*font-size: 14px;*/
}

#login-form input[type="email"],
#login-form input[type="password"] {
	/*border: 1px solid #dcdcdc;
	padding: 12px 10px;
	width: 238px;*/
}

#login-form input[type="email"] {
	border-radius: 3px 3px 0 0;
}

#login-form input[type="password"] {
	/*border-top: none;
	border-radius: 0px 0px 3px 3px;*/
}

#login-form input[type="submit"] {
	/*background: #1dabb8;
	border-radius: 3px;
	color: #fff;
	float: right;
	font-weight: bold;
	margin-top: 20px;
	padding: 12px 20px;*/
}

#login-form input[type="submit"]:hover { /*background: #198d98;*/ }

#login-form footer {
	font-size: 12px;
	margin-top: 16px;
}

.info {
	background: #e5e5e5;
	border-radius: 50%;
	display: inline-block;
	height: 20px;
	line-height: 20px;
	margin: 0 10px 0 0;
	text-align: center;
	width: 20px;
}
        .vertical {
            
            position:relative;
            left:-20px;
            top:-22px;
            width: 454px;
    height:300px;
                border-left:ridge;
    border-left-width:1px;
     border-right:ridge;
     border-right-width:1px;
   border-bottom:ridge;
   border-bottom-width:1px;
   border-left-color:black;
    border-color:black;
        }

        input:required,
textarea:required {
  /*border-color: red !important;*/
}
input:required + label {
  color: red;
}


label {
  display: block;
}
input + label {
  display: inline-block;
  margin-right: 10px;
}

input[type=text],
input[type=email],
textarea {
  /*border: 1px solid #999;*/
  /*padding: 5px;*/
  /*width: 100%;*/
}


    </style>
</head>
<body> 
     <div id="login-form">

        <h1><asp:Label ID="Label1" runat="server" Text="MEDINET"   style="position:relative; left:23px"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Versión: "   style="position:relative; left:220px"></asp:Label>
            <asp:Label ID="lblVersion" runat="server"    style="position:relative; left:230px" ></asp:Label>

        </h1>
        <fieldset>
<div class="vertical">
    <form id="form1" runat="server">
        <div class="centraTabla">
        <table width="100%" >
        <tr>
        <td class="style1">

        </td>

        </tr>
        </table>
            
            <br />
            <asp:Label runat="server" Text="Credenciales: " Font-Size="X-Large" Font-Bold="true" style="position:relative; left:30px;"  />
            <hr style="width:85%; "/>
           <br />

            <asp:Label runat="server" Text="Usuario " Font-Size="Large" CssClass="label" style="position:relative; left:33px;" />
            <br />
            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtbox" style="position:relative; left:33px; width:380px;" required="required" />
            <%--<asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="Usuario Requerido" ControlToValidate="txtUserName" ValidationGroup="User" style="position:relative; left:30px;" />--%>
            <br />
            
            <asp:Label runat="server" Text="Contraseña: " Font-Size="Large" CssClass="label" style="position:relative; left:33px; "  />
            <br />
            <asp:TextBox ID="txtUserPsw" runat="server" TextMode="Password" CssClass="txtbox" style="position:relative; left:33px; width:380px;" required="required"   />
            <%--<asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Password Requerido" ControlToValidate="txtUserPsw" ValidationGroup="User" />--%>
            <br />
            <br />
            <asp:Button Text="Log In" runat="server" OnClick="btnAceptar_Click" ValidationGroup="User" CssClass="classname" />

        </div>
    </form>
    </div>
     </fieldset>

    </div> <!-- end login-form -->

</body>
</html>