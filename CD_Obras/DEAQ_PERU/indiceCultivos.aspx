<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indiceCultivos.aspx.cs" Inherits="indiceUsos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Índice de Cultivos</title>
    <link href="../estilos.css" rel="stylesheet" type="text/css" />
    <script language='JavaScript' type='text/JavaScript'>
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf('#')!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf('?'))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
</script>
<script language='javascript' type='text/javascript' src='../../Scripts/SustActIFrame.js'></script>
</head>
<body  onLoad="MM_preloadImages('../letras/a2.gif','../letras/z2.gif','../letras/b2.gif','../letras/c2.gif','../letras/d2.gif','../letras/e2.gif','../letras/f2.gif','../letras/g2.gif','../letras/h2.gif','../letras/i2.gif','../letras/j2.gif','../letras/k2.gif','../letras/l2.gif','../letras/m2.gif','../letras/o2.gif','../letras/p2.gif','../letras/q2.gif','../letras/r2.gif','../letras/s2.gif','../letras/t2.gif','../letras/u2.gif','../letras/v2.gif','../letras/w2.gif','../letras/x2.gif','../letras/y2.gif','../letras/n2.gif','../letras/&ntilde;2.gif')">
    <form id="form1" runat="server">
    <table width='100%' border='0' cellpadding='0' cellspacing='0' bordercolor='#111111' style='border-collapse: collapse'>
      <tr align='center' valign='middle'>
         <th width='20' height='20' scope='col'><a href='a.htm' title='A' target='adentro'><img src='../letras/a1.gif' name='Image11'  onMouseOver="MM_swapImage('Image11','','../letras/a2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='b.htm' title='B' target='adentro'><img src='../letras/b1.gif' name='Image12'  onMouseOver="MM_swapImage('Image12','','../letras/b2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='c.htm' title='C' target='adentro'><img src='../letras/c1.gif' name='Image13'  onMouseOver="MM_swapImage('Image13','','../letras/c2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='d.htm' title='D' target='adentro'><img src='../letras/d1.gif' name='Image14'  onMouseOver="MM_swapImage('Image14','','../letras/d2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='e.htm' title='E' target='adentro'><img src='../letras/e1.gif' name='Image15'  onMouseOver="MM_swapImage('Image15','','../letras/e2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='f.htm' title='F' target='adentro'><img src='../letras/f1.gif' name='Image16'  onMouseOver="MM_swapImage('Image16','','../letras/f2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='g.htm' title='G' target='adentro'><img src='../letras/g1.gif' name='Image17'  onMouseOver="MM_swapImage('Image17','','../letras/g2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='h.htm' title='H' target='adentro'><img src='../letras/h1.gif' name='Image18'  onMouseOver="MM_swapImage('Image18','','../letras/h2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='Faltante.htm' title='I' target='adentro'><img src='../letras/i1.gif' name='Image19'  onMouseOver="MM_swapImage('Image19','','../letras/i2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='j.htm' title='J' target='adentro'><img src='../letras/j1.gif' name='Image110'  onMouseOver="MM_swapImage('Image110','','../letras/j2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='faltante.htm' title='K' target='adentro'><img src='../letras/k1.gif' name='Image111'  onMouseOver="MM_swapImage('Image111','','../letras/k2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='l.htm' title='L' target='adentro'><img src='../letras/l1.gif' name='Image112'  onMouseOver="MM_swapImage('Image112','','../letras/l2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='m.htm' title='M' target='adentro'><img src='../letras/m1.gif' name='Image113'  onMouseOver="MM_swapImage('Image113','','../letras/m2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='n.htm' title='N' target='adentro'><img src='../letras/n1.gif' name='Image114'  onMouseOver="MM_swapImage('Image114','','../letras/n2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='o.htm' title='O' target='adentro'><img src='../letras/o1.gif' name='Image115'  onMouseOver="MM_swapImage('Image115','','../letras/o2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='p.htm' title='P' target='adentro'><img src='../letras/p1.gif' name='Image116'  onMouseOver="MM_swapImage('Image116','','../letras/p2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='faltante.htm' title='Q' target='adentro'><img src='../letras/q1.gif' name='Image117'  onMouseOver="MM_swapImage('Image117','','../letras/q2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='r.htm' title='R' target='adentro'><img src='../letras/r1.gif' name='Image118'  onMouseOver="MM_swapImage('Image118','','../letras/r2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='s.htm' title='S' target='adentro'><img src='../letras/s1.gif' name='Image119'  onMouseOver="MM_swapImage('Image119','','../letras/s2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='t.htm' title='T' target='adentro'><img src='../letras/t1.gif' name='Image120'  onMouseOver="MM_swapImage('Image120','','../letras/t2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='u.htm' title='U' target='adentro'><img src='../letras/u1.gif' name='Image121'  onMouseOver="MM_swapImage('Image121','','../letras/u2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='v.htm' title='V' target='adentro'><img src='../letras/v1.gif' name='Image122'  onMouseOver="MM_swapImage('Image122','','../letras/v2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='faltante.htm' title='W' target='adentro'><img src='../letras/w1.gif' name='Image123'  onMouseOver="MM_swapImage('Image123','','../letras/w2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='faltante.htm' title='X' target='adentro'><img src='../letras/x1.gif' name='Image124'  onMouseOver="MM_swapImage('Image124','','../letras/x2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='y.htm' title='Y' target='adentro'><img src='../letras/y1.gif' name='Image125'  onMouseOver="MM_swapImage('Image125','','../letras/y2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>
         <th width='20' height='20' scope='col'><a href='z.htm' title='Z' target='adentro'><img src='../letras/z1.gif' name='Image1'  onMouseOver="MM_swapImage('Image1','','../letras/z2.gif',1)" onMouseOut="MM_swapImgRestore()" border='0'></a></th>

         </tr>
      <tr align='center' valign='middle'> </tr>
    </table>
    <p align='center' class='Titulo'>&Iacute;NDICE DE CULTIVOS</p>
    <p align='center' class="Titulo"><asp:Label ID="lblLetter" runat="server"></asp:Label></p>
    <div style='overflow:auto; '>
        <asp:Repeater ID="rptCultivos" runat="server" OnItemDataBound="rptCultivos_ItemDataBound">
        <ItemTemplate>
            <p class='Cultivo'><a href="#" id="displayChildren_0" target='_self' runat="server"><%# DataBinder.Eval(Container.DataItem, "Cultivo").ToString().ToUpper() %></a></p>
            <ul>
                <div id="z_divLevelChildren_0" runat="server" style="text-align:left; display:none;">
                <asp:Repeater ID="rptProd" runat="server" >
                    <ItemTemplate>
                        
                            <p class="producto"><a class='links' href='..\productos\<%# Eval("Productid")+"_"+Eval("PharmaFormID")+".htm" %>' style='text-decoration:none'> <b><%# Eval("PRODUCTO") %></b>. <%# Eval("PharmaForm") %>. <b><%# Eval("LABORATORIO") %></b></a></p>
                        
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            </ul>
        </ItemTemplate>
        </asp:Repeater>
        </div>
    </form>
</body>
</html>
