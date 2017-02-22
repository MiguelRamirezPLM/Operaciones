<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indiceSustancias.aspx.cs" Inherits="indiceSustancias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<link href='../estilos.css' rel='stylesheet' type='text/css' />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <script type="text/javascript">
        <!--
        function MM_preloadImages() { //v3.0
          var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
            var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
            if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
        }

        function MM_swapImgRestore() { //v3.0
          var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
        }

        function MM_findObj(n, d) { //v4.01
          var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
          if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
          for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
          if(!x && d.getElementById) x=d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
          var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
           if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
        }
        //-->
    </script>
<script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
</head>
<body topmargin='0' marginheight='0' onload="MM_preloadImages('../letras/a2.gif','../letras/b2.gif','../letras/c2.gif','../letras/d2.gif','../letras/e2.gif','../letras/f2.gif','../letras/g2.gif','../letras/h2.gif','../letras/i2.gif','../letras/j2.gif','../letras/k2.gif','../letras/l2.gif','../letras/m2.gif','../letras/n2.gif','../letras/&ntilde;2.gif','../letras/o2.gif','../letras/p2.gif','../letras/q2.gif','../letras/r2.gif','../letras/s2.gif','../letras/t2.gif','../letras/u2.gif','../letras/v2.gif','../letras/w2.gif','../letras/x2.gif','../letras/y2.gif','../letras/z2.gif')">
<form id="form2" runat="server">
<br />
<table width='100%' border='0' cellpadding='0' cellspacing='0' bordercolor='#111111' style='border-collapse: collapse'>
  <tr align="center" valign="middle">
    <th width="20" height="20" scope="col"><a href="a.htm" title="A"><img src="../letras/a1.gif" style="border:none" name="Image11" id="Image11"  onmouseover="MM_swapImage('Image11','','../letras/a2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="b.htm" title="B"><img src="../letras/b1.gif" style="border:none" name="Image12" id="Image12"  onmouseover="MM_swapImage('Image12','','../letras/b2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="c.htm" title="C"><img src="../letras/c1.gif" style="border:none" name="Image13" id="Image13"  onmouseover="MM_swapImage('Image13','','../letras/c2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="d.htm" title="D"><img src="../letras/d1.gif" style="border:none" name="Image14" id="Image14"  onmouseover="MM_swapImage('Image14','','../letras/d2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="e.htm" title="E"><img src="../letras/e1.gif" style="border:none" name="Image15" id="Image15"  onmouseover="MM_swapImage('Image15','','../letras/e2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="f.htm" title="F"><img src="../letras/f1.gif" style="border:none" name="Image16" id="Image16"  onmouseover="MM_swapImage('Image16','','../letras/f2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="g.htm" title="G"><img src="../letras/g1.gif" style="border:none" name="Image17" id="Image17"  onmouseover="MM_swapImage('Image17','','../letras/g2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="h.htm" title="H"><img src="../letras/h1.gif" style="border:none" name="Image18" id="Image18"  onmouseover="MM_swapImage('Image18','','../letras/h2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="i.htm" title="I"><img src="../letras/i1.gif" style="border:none" name="Image19" id="Image19"  onmouseover="MM_swapImage('Image19','','../letras/i2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="J"><img src="../letras/j1.gif" style="border:none" name="Image110" id="Image110" onmouseover="MM_swapImage('Image110','','../letras/j2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="k.htm" title="K"><img src="../letras/k1.gif" style="border:none" name="Image111" id="Image111" onmouseover="MM_swapImage('Image111','','../letras/k2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="l.htm" title="L"><img src="../letras/l1.gif" style="border:none" name="Image112" id="Image112" onmouseover="MM_swapImage('Image112','','../letras/l2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="m.htm" title="M"><img src="../letras/m1.gif" style="border:none" name="Image113" id="Image113" onmouseover="MM_swapImage('Image113','','../letras/m2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="n.htm" title="N"><img src="../letras/n1.gif" style="border:none" alt="n" id="Image2" onmouseover="MM_swapImage('Image2','','../letras/n2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="&Ntilde;"><img src="../letras/&ntilde;1.gif" style="border:none" alt="&ntilde;" id="Image3" onmouseover="MM_swapImage('Image3','','../letras/&ntilde;2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="o.htm" title="O"><img src="../letras/o1.gif" style="border:none" name="Image115" id="Image115" onmouseover="MM_swapImage('Image115','','../letras/o2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="p.htm" title="P"><img src="../letras/p1.gif" style="border:none" name="Image116" id="Image116" onmouseover="MM_swapImage('Image116','','../letras/p2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="q.htm" title="Q"><img src="../letras/q1.gif" style="border:none" name="Image117" id="Image117" onmouseover="MM_swapImage('Image117','','../letras/q2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="r.htm" title="R"><img src="../letras/r1.gif" style="border:none" name="Image118" id="Image118" onmouseover="MM_swapImage('Image118','','../letras/r2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="s.htm" title="S"><img src="../letras/s1.gif" style="border:none" name="Image119" id="Image119" onmouseover="MM_swapImage('Image119','','../letras/s2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="t.htm" title="T"><img src="../letras/t1.gif" style="border:none" name="Image120" id="Image120" onmouseover="MM_swapImage('Image120','','../letras/t2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="U"><img src="../letras/u1.gif" style="border:none" name="Image121" id="Image121" onmouseover="MM_swapImage('Image121','','../letras/u2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="v.htm" title="V"><img src="../letras/v1.gif" style="border:none" name="Image122" id="Image122" onmouseover="MM_swapImage('Image122','','../letras/v2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="W"><img src="../letras/w1.gif" style="border:none" name="Image123" id="Image123" onmouseover="MM_swapImage('Image123','','../letras/w2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="X"><img src="../letras/x1.gif" style="border:none" name="Image124" id="Image124" onmouseover="MM_swapImage('Image124','','../letras/x2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="no.htm" title="Y"><img src="../letras/y1.gif" style="border:none" name="Image125" id="Image125" onmouseover="MM_swapImage('Image125','','../letras/y2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>
    <th width="20" height="20" scope="col"><a href="z.htm" title="Z"><img src="../letras/z1.gif" style="border:none" name="Image1" id="Image1" onmouseover="MM_swapImage('Image1','','../letras/z2.gif',1)" onmouseout="MM_swapImgRestore()" /></a></th>

  </tr>
    <tr align="center" valign="middle">
  </tr>
</table>
<div align="center" >
  <p class="Titulo">&Iacute;ndice de Sustancias Activas </p>
  <p align='center' class="Titulo"><asp:Label ID="lblLetter" runat="server"></asp:Label></p>
</div>
 <div style='overflow:auto;'>
  <asp:Repeater ID="rptLevel_0" runat="server" OnItemDataBound="rptLevel_0_ItemDataBound"  >
     <ItemTemplate>         
        <p><a href="#" id="displayChildren_0" target='_self' runat="server" class= "sustancia"><%#Eval("sustancia")%></a></p>      
            <blockquote>                
                <div id="z_divLevelChildren_0" runat="server" style="text-align:left; display:none;">                    
                        <table>                
                            <tr>
                                <td>                                    
                                    <asp:Label id= "solos" runat="server" class="solos">SOLOS</asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptLevel_1" runat="server" >
                            <ItemTemplate>   
                            <tr>                                                         
                                <td><a href="<%#"../productos/"+Eval("ProductId")+"_"+Eval("PharmaFormID")+".htm"%>" id="displayChildren_1" target='_self' style='text-decoration:none' class="Producto">
                                    <b><%#Eval("Nombre")+"."%></b>&nbsp;<%#Eval("Laboratorio") %> </a>
                                </td>                               
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>                           
                            <tr>
                                <td>
                                    <asp:Label id= "combinados" runat="server" class="solos">COMBINADOS</asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptLevel_2" runat="server" >
                            <ItemTemplate>   
                            <tr>                                                        
                                <td><a href="<%#"../productos/"+Eval("ProductId")+"_"+Eval("PharmaFormId")+".htm"%>" id="displayChildren_2" target='_self' style='text-decoration:none' class="Producto">
                                    <b><%#Eval("Nombre")%>&nbsp;</b>. <i><%#Eval("Sustancias")+"." %>&nbsp;</i><%#Eval("Laboratorio") %> </a>
                                </td>                               
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>                                             
                        </table>                    
                </div>
            </blockquote>
     </ItemTemplate>
  </asp:Repeater>
 </div>
</form>
</body>
</html>
