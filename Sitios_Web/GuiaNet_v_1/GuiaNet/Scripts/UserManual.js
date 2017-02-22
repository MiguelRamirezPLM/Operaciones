/*                                                                              VENTAS                                                                                  */

//Productos
function getcontentbyid(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 1) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Producto</p><br>";
        d += "<p class='paragraphcontent'>Para agregar un Producto nuevo al Cliente seleccionado, siga los siguientes pasos.<br>";
        d += "<p class='paragraphcontent'>&nbsp;&nbsp; &bull; Dar clic en el bot&oacute;n <i>''Agregar Producto''</i>.</p> <br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/btninsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>&nbsp;&nbsp; &bull; Se abrir&aacute; el Formulario <i>''Agregar Producto''</i>, donde deber&aacute; ingresar el <i>''Nombre del Producto''</i> y dar clic en Insertar.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/forminsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>&nbsp;&nbsp; &bull; Si el campo est&aacute; vac&iacute;o, el sistema arrojar&aacute; un mensaje de error, notificando que el campo no puede quedar vac&iacute;o.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/errorinsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>&nbsp;&nbsp; &bull; Si el Producto ya existe para el cliente seleccionado, GuiaNet mostrar&aacute; un mensaje, notificando que ya existe el Producto.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/existinsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>&nbsp;&nbsp; &bull; Si todo est&aacute; correcto, el Producto ser&aacute; insertado correctamente para el Cliente seleccionado y la tabla ser&aacute; actualizada con el nuevo Producto.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/prodins.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }
    if (Id == 2) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Producto usando la busqueda Predictiva</p><br>";
        d += "<p class='paragraphcontent'>Al insertar un Producto al cliente seleccionado, tiene la opci&oacute;n de agregar un Producto al Cliente seleccionado de otro cliente, por medio de la b&uacute;squeda predictiva que incorpora el sistema, esta se activa al momento de detectar tres caracteres en el campo <i>''Nombre de Producto''</i>, mostrando los Productos que comiencen con el texto escrito en el campo, as&iacute; como al Cliente al que pertenecen, separados por <i>''Comas ('','')''</i>.<br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/bspredinsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ol class='paragraphcontent'><li>Campo <i>''Nombre de Producto''</i> con tres caracteres.</li> <li>Productos que comienzan con el texto introducido en el Campo <i>''Nombre de Producto''</i>, separados por una <i>'',''</i> el Cliente al que pertenecen.</li></ol>";
        d += "<p class='paragraphcontent'>Para insertar un Producto de otro cliente desde la b&uacute;squeda predictiva basta con seleccionar el producto y dar clic en Insertar, si el producto no existe para el Cliente lo insertar&aacute; correctamente. En caso contrario mostrar&aacute; un mensaje dependiendo el error, (''Ver Punto <span style='cursor:pointer; color:#065977' id='spn' onclick='clicreference();'><i><b><u>Agregar Producto</u></b></i></span>'').</p>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
    if (Id == 3) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Editar informaci&oacute;n de un Producto</p><br>";
        d += "<p class='paragraphcontent'><i><b>GuiaNet</b></i>, ofrece al usuario la opci&oacute;n de Editar el <i>''Nombre del Producto'', ''Registro Sanitario'' y ''C&oacute;digo de Barras''</i>. El proceso de Edici&oacute;n consta de los siguientes pasos:</p>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto a Editar.</li><li>En la columna <i>''Editar nombre de Producto''</i> de la Tabla de Productos dar clic en el bot&oacute;n <i>''Editar''</i> que tiene el s&iacute;mbolo <img src='../Images/edit.png' style='height:25px;width:25px;'>. </li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/edtprodsm.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, se habilitar&aacute; la casilla <i>''Nombre de Producto''</i>, <i>''Registro Sanitario''</i> y <i>''C&oacute;digo de Barras''</i>, del producto a editar, el bot&oacute;n <i>''Editar''</i> cambiara a <i>''Guardar''</i> ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y <i>''Cancelar''</i> ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/fieldsactedprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizar las modificaciones que requiera y presionar el bot&oacute;n <i>''Guardar''</i> ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), las casillas regresar&aacute;n a su forma original y los cambios se habr&aacute;n guardado.</li></ul>";
        d += "<ul class='paragraphcontent'><li>Si desea cancelar la edici&oacute;n del producto, presionar bot&oacute;n <i>''Cancelar''</i> ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ), las casillas regresar&aacute;n a su forma original y ning&uacute;n cambio ser&aacute; guardado.</li></ul>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
    if (Id == 4) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Marcar Productos como Participantes</p><br>";
        d += "<p class='paragraphcontent'>Para marcar un producto como participante, el proceso se describe a continuaci&oacute;n.</p>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto que ser&aacute; marcado como Participante.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/sppart.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dirigirse a la Columna <i>''Participante''</i> y marcar la casilla correspondiente al Producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/chkpprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al quedar marcadas las casillas, el Producto aparecer&aacute; como Participante en la Edici&oacute;n.</li></ul>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
    if (Id == 5) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Des marcar Productos como Participantes</p><br>";
        d += "<p class='paragraphcontent'>Para desmarcar un producto como participante en la edici&oacute;n, el proceso se describe a continuaci&oacute;n.</p>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto que ser&aacute; desmarcado como Participante en la edici&oacute;n.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/chkpprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dirigirse a la Columna <i>''Participante''</i> y desmarcar la casilla correspondiente al Producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/sppart.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al quedar desmarcadas las casillas, el Producto ya no aparecer&aacute; como Participante en la Edici&oacute;n.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
    if (Id == 6) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Buscar Producto</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, cuenta con un buscador de productos, que realiza la B&uacute;squeda de los productos que comiencen con el texto introducido en el buscador, del Cliente seleccionado.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/searchptext.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La b&uacute;squeda se realiza de la siguiente forma:</p>";
        d += "<ul class='paragraphcontent'><li>Introducir el texto a buscar.</li> <li>Presionar el bot&oacute;n del buscador ( <img src='../Images/buscar.png' style='height:30px;width:30px;'> ).</li> <li>Se actualizar&aacute; la tabla de productos con los resultados de la b&uacute;squeda.</li> </ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/resultsearch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ol class='paragraphcontent'><li>Muestra la Cantidad de Resultados que se obtuvieron con el texto de b&uacute;squeda.</li><li>Muestra los Productos que se obtuvieron como resultado de la B&uacute;squeda.</li></ol>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
    if (Id == 7) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de Productos</p><br>";
        d += "<p class='paragraphcontent'>El usuario tendr&aacute; la posibilidad de generar reportes de los productos Participantes del Cliente  seleccionado.</p>";
        d += "<ul class='paragraphcontent'><li>Para generar el reporte el usuario tendr&aacute; que dar clic en icono <i>''Reporte de Productos por Cliente''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/getrptprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>El reporte se generar&aacute; con los productos que est&eacute;n marcados como Participantes.</li></ul><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/reportstructure.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo.</td><td class='paragraphcontent'>Logo del sistema <i>''GuiaNet''</i></td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo.</td><td class='paragraphcontent'>Logo PLM.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra r&oacute;tulo del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Edici&oacute;n.</td><td class='paragraphcontent'>Muestra la edici&oacute;n en la que se est&aacute; generando el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>Cliente.</td><td class='paragraphcontent'>Muestra el Nombre de Cliente, del que se est&aacute; generando el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Total de Productos.</td><td class='paragraphcontent'>Muestra el total de productos, participantes del cliente seleccionado.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Contenido.</td><td class='paragraphcontent'>Muestra los Productos participantes del Cliente. La letra <i>''P''</i>, indica que el producto se encuentra como participante.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Productos Hospitalarios
function getcontentHPproducts(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 8) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Producto</p><br>";
        d += "<p class='paragraphcontent'>Para agregar un Producto nuevo al Cliente seleccionado, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n <i>''Agregar Producto''</i></li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/btnaddprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Aparecera el siguiente formulario.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/formaddprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En el Formulario <i>''Agregar Producto''</i> debe Ingresar el <i>''Nombre del Producto''</i>, seleccionar <i>Forma Farmac&eacute;utica</i>, <i>Presentaci&oacute;n</i> y <i>Sustancia Activa</i>.</li></ul>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n <i>''Insertar''</i></li></ul>";
        d += "<ul class='paragraphcontent'><li>Si alguno de los campos est&aacute; vac&iacute;o, el sistema arrojara un mensaje de error, enlistando los campos que no se han llenado y/o seleccionado.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/errorinsprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si el Producto ya existe para el cliente seleccionado, <i>GuiaNet</i> mostrar&aacute; un mensaje, notificando que ya existe el Producto.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/errorexistprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si todo est&aacute; correcto, el Producto ser&aacute; insertado correctamente para el Cliente seleccionado y la tabla ser&aacute; actualizada con el nuevo Producto.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/insprod.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 9) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Producto usando la busqueda Predictiva</p><br>";
        d += "<p class='paragraphcontent'>Al insertar un Producto al cliente seleccionado, tiene la opci&oacute;n de agregar un Producto al Cliente seleccionado de otro cliente, por medio de la b&uacute;squeda predictiva que incorpora el sistema, esta se activa al momento de detectar tres caracteres en el campo <i>''Nombre de Producto''<i>, sin tener que dar clic en alg&uacute;n bot&oacute;n, mostrando los Productos que comiencen con el texto escrito en el campo, as&iacute; como al Cliente al que pertenecen, separados por <i>''Comas ('','')''.</i></p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/searchpred.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ol class='paragraphcontent'><li>Campo <i>''Nombre de Producto''</i> con tres caracteres.</li><li>Productos que comienzan con el texto introducido en el Campo <i>''Nombre de Producto''</i>, separados por una <i>'',''</i> el Cliente y la Forma Farmac&eacute;utica que pertenece al Producto.</li></ol>";
        d += "<p class='paragraphcontent'>Para insertar un Producto de otro cliente desde la b&uacute;squeda predictiva basta con seleccionar el producto y dar clic en Insertar, si el producto no existe para el Cliente lo insertar&aacute; correctamente. En caso contrario mostrar&aacute; un mensaje dependiendo el error, (''Ver Punto <span style='cursor:pointer; color:#065977' id='spn' onclick='clicreferencePH();'><i><b><u>Agregar Producto</u></b></i></span>'')</p>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 10) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Editar Informaci&oacute;n de un Producto</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, ofrece al usuario la opci&oacute;n de Editar el Nombre del Producto. El proceso de Edici&oacute;n consta de los siguientes pasos:</p>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto a Editar.</li>";
        d += "<li>En la columna <i>''Editar nombre de Producto''</i> de la Tabla de Productos dar clic en el bot&oacute;n <i>''Editar''</i> que tiene el s&iacute;mbolo <img src='../Images/edit.png' style='height:25px;width:25px;'>.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/btneditprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, se habilitar&aacute; las casillas <i>''Nombre de Producto''</i>, <i>''Registro Sanitario''</i> y <i>''C&oacute;digo de Barras''</i>  del producto a editar, el bot&oacute;n <i>''Editar''</i> cambiar&aacute; a <i>''Guardar''</i> ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y <i>''Cancelar''</i> ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/edtprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizar las modificaciones que requiera y presionar el bot&oacute;n <i>''Guardar''</i> ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), las casillas regresar&aacute;n a su forma original y los cambios se habr&aacute;n guardado.</li></ul>";
        d += "<ul class='paragraphcontent'><li>Si desea cancelar la edici&oacute;n del producto, presionar bot&oacute;n <i>''Cancelar''</i> ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ), las casillas regresar&aacute;n a su forma original y ning&uacute;n cambio ser&aacute; guardado.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 11) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Marcar Productos como Participantes</p><br>";
        d += "<p class='paragraphcontent'>Para marcar un producto como participante, el proceso se describe a continuaci&oacute;n.</p><br>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto que ser&aacute; marcado como Participante.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/prodtocheckpp.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dirigirse a la Columna <i>''Participante''</i> y marcar la casilla correspondiente al Producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/checkppprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al quedar marcadas las casillas, el Producto aparecer&aacute; como Participante en la Edici&oacute;n.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 12) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Des marcar Productos como Participantes</p><br>";
        d += "<p class='paragraphcontent'>Para desmarcar un producto como participante en la edici&oacute;n, el proceso se describe a continuaci&oacute;n.</p>";
        d += "<ul class='paragraphcontent'><li>Localizar el Producto que ser&aacute; desmarcado como Participante en la edici&oacute;n.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/checkppprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dirigirse a la Columna <i>''Participante''</i> y desmarcar la casilla correspondiente al Producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/prodtocheckpp.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al quedar desmarcadas las casillas, el Producto ya no aparecer&aacute; como Participante en la Edici&oacute;n.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 13) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Buscar un Producto</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, cuenta con un buscador de productos, que realiza la B&uacute;squeda de los productos que comiencen con el texto introducido en el buscador, del Cliente seleccionado.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/texttosearchprod.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La b&uacute;squeda se realiza de la siguiente forma:</p>";
        d += "<ul class='paragraphcontent'><li>Introducir el texto a buscar.</li> <li>Presionar el bot&oacute;n del buscador ( <img src='../Images/buscar.png' style='height:30px;width:30px;'> ).</li> <li>Se actualizar&aacute; la tabla de productos con los resultados de la b&uacute;squeda.</li> </ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/resulttosearch.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 14) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de Productos</p><br>";
        d += "<p class='paragraphcontent'>El usuario tendr&aacute; la posibilidad de generar reportes de los productos Participantes del Cliente seleccionado.</p>";
        d += "<ul class='paragraphcontent'><li>Para generar el reporte el usuario tendr&aacute; que dar clic en icono <i>''Reporte de Medicamentos por Cliente''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/btnrpt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>El reporte se generar&aacute; con los productos que est&eacute;n marcados como Participantes.</li></ul><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/PH/rptPH.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo.</td><td class='paragraphcontent'>Logo del sistema <i>''GuiaNet''</i></td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo.</td><td class='paragraphcontent'>Logo PLM.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra r&oacute;tulo del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Edici&oacute;n.</td><td class='paragraphcontent'>Muestra la edici&oacute;n en la que se est&aacute; generando el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>Cliente.</td><td class='paragraphcontent'>Muestra el Nombre de Cliente, del que se est&aacute; generando el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Contenido.</td><td class='paragraphcontent'>Muestra los Productos participantes del Cliente. La letra <i>''P''</i>, indica que el producto se encuentra como participante.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Categorias
function getcontentcategoriesHom(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 15) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Asociar Categor&iacute;as al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para asociar una categor&iacute;a al cliente seleccionado, siga los siguientes pasos.</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la categor&iacute;a que asociara al cliente.</i></li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/getcats.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic sobre la Categor&iacute;a que asociara al Cliente.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/clickoncategory.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La categor&iacute;a que haya seleccionado se reflejara en la Tabla de Categor&iacute;as asociadas.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/asociatecat.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 16) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar Categor&iacute;as asociadas al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para desasociar una categor&iacute;a al cliente seleccionado, siga los siguientes pasos.</p><br>";
        d += "<ul class='paragraphcontent'><li>En la tabla de <i>''Categor&iacute;as Asociadas al Cliente''</i>, ubicar la categor&iacute;a asociada al cliente que desea eliminar.</li></ul>";
        d += "<ul class='paragraphcontent'><li>Dirigirse a la columna <i>''Eliminar''</i> y dar clic en el bot&oacute;n correspondiente a la categor&iacute;a que desea eliminar.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/deletecat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La categor&iacute;a que haya seleccionado ya no se reflejara en la Tabla de Categor&iacute;as asociadas y el cliente seleccionado no aparecer&aacute; con esa categor&iacute;a.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/deletedcat.png'/ style='width:90%;heigth:90%'></p><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 17) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Buscador de Categor&iacute;as</p><br>";
        d += "<p class='paragraphcontent'>La secci&oacute;n categor&iacute;as incorpora un buscador de categor&iacute;as, el buscador funciona escribiendo el t&eacute;rmino a buscar y dar clic en el bot&oacute;n b&uacute;squeda  con el s&iacute;mbolo , el &aacute;rbol de categor&iacute;as mostrar&aacute; solamente los resultados de la b&uacute;squeda. La b&uacute;squeda se realiza en cualquiera de los cuatro niveles de las categor&iacute;as.</p>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/catssearch.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Buscador de Categor&iacute;as</td><td class='paragraphcontent'>Texto introducido en el buscador, este caso <i>''analizadores de flujo de gas''</i>.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>&Aacute;rbol de Categor&iacute;as</td><td class='paragraphcontent'>El &aacute;rbol de categor&iacute;as, donde solamente muestra los resultados de la b&uacute;squeda.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Tabla de Categor&iacute;as</td><td class='paragraphcontent'>Muestra la categor&iacute;a obtenida de la b&uacute;squeda asociada al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 18) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Buscador de Categor&iacute;as asociadas al Cliente</p><br>";
        d += "<p class='paragraphcontent'>Adem&aacute;s del buscador de categor&iacute;as, la secci&oacute;n incorpora un buscador de las categor&iacute;as asociadas al cliente, el buscador funciona escribiendo el t&eacute;rmino a buscar y dar clic en el bot&oacute;n b&uacute;squeda con el s&iacute;mbolo ( <img src='../Images/buscar.png' style='height:30px;width:30px;'> ), la tabla de <i>''Categor&iacute;a asociadas al cliente''</i> se actualizara con los resultados de la b&uacute;squeda.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/fieldtosearch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/HOMCAT/fieldtosearchresult.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Campo de b&uacute;squeda.</td><td class='paragraphcontent'>Texto introducido en el buscador, este caso <i>''analizadores de flujo de gas''</i>.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Tabla de categor&iacute;as asociadas.</td><td class='paragraphcontent'>La tabla de Categor&iacute;as asociadas, donde solamente muestra los resultados de la b&uacute;squeda.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Especialidades
function getcontentespecialities(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 19) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Asociar especialidades al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para asociar alguna especialidad al cliente, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar o Buscar la Especialidad que ser&aacute; asociada al cliente:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/selectesp.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ), de la columna <i>''Agregar''</i>. Al realizar esto, la especialidad ya habr&aacute; sido asociada al cliente. La especialidad asociada, ya no aparecer&aacute; en la tabla de Especialidades Disponibles.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espasoc.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 20) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar especialidades asociadas al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para desasociar alguna especialidad al cliente, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la Tabla <i>''Especialidades Asociadas''</i>, Ubicar o Buscar la Especialidad que ser&aacute; desasociada del cliente:</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ), de la columna <i>''Eliminar''</i>. Al realizar esto, la especialidad ya habr&aacute; sido desasociada del cliente. La especialidad desasociada, volver&aacute; a aparecer en la tabla de Especialidades Disponibles.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/selectesp.png'/ style='width:90%;heigth:90%'></p><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 21) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Buscador de Especialidades</p><br>";
        d += "<p class='paragraphcontent'>Si se desea realizar una b&uacute;squeda de alguna Especialidad Disponible o Asociada, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el buscador de la Tabla que desea ubicar la Especialidad (<i>''Especialidades Disponibles'' o ''Especialidades Asociadas''</i>).</li><li>En el campo del Buscador, escribir el texto a buscar.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/searchesps.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li><b>NOTA:</b> El buscador se acciona al introducir en el campo, 3 caracteres o m&aacute;s.</li><li><b>NOTA 2:</b> Cuando el campo queda vac&ioacute;o, es decir, sin ning&uacute;n car&aacute;cter, regresa la tabla a su condici&oacute;n original (Todos los registros).</li></ul>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 22) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Interfaz Asociar especialidades a Sucursal</p><br>";
        d += "<p class='paragraphcontent'>La interfaz de la secci&oacute;n, se explica a continuaci&oacute;n: </p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/interfazesp.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>R&oacute;tulo</td><td class='paragraphcontent'>Muestra el nombre de la sucursal que actualmente se est&eacute; trabajando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Buscador de Especialidades.</td><td class='paragraphcontent'>Realiza una b&uacute;squeda de las especialidades disponibles.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Tabla de Especialidades Disponibles.</td><td class='paragraphcontent'>Muestra las Especialidades que est&aacute;n disponibles para su asociaci&oacute;n con la sucursal.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Buscador de Especialidades Asociadas a la sucursal.</td><td class='paragraphcontent'>Realiza una b&uacute;squeda de las especialidades asociadas a la sucursal.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Tabla de Especialidades Asociadas a la sucursal.</td><td class='paragraphcontent'>Muestra las Especialidades que est&eacute;n asociadas a la sucursal.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 23) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Asociar especialidades a Sucursal</p><br>";
        d += "<p class='paragraphcontent'>Al estar ubicado en un cliente, el usuario, adem&aacute;s de poder asociar Especialidades a un Cliente, Tambi&eacute;n tendr&aacute; la posibilidad de asociar Especialidades a alguna de las sucursales participantes del Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para asociar alguna especialidad a la sucursal, siga los siguientes pasos: </p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar o Buscar la Especialidad que ser&aacute; asociada a la sucursal:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ), de la columna <i>''Agregar''</i>. Al realizar esto, la especialidad ya habr&aacute; sido asociada a la sucursal. La especialidad asociada, ya no aparecer&aacute; en la tabla de Especialidades Disponibles.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espsucasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "";
        d += "";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 24) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar especialidades asociadas a la Sucursal.</p><br>";
        d += "<p class='paragraphcontent'>Para desasociar alguna especialidad de la sucursal, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la Tabla <i>''Especialidades Asociadas''</i>, Ubicar o Buscar la Especialidad que ser&aacute; desasociada de la sucursal:</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espsucasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ), de la columna <i>''Eliminar''</i>. Al realizar esto, la especialidad ya habr&aacute; sido desasociada de la sucursal. La especialidad desasociada, volver&aacute; a aparecer en la tabla de Especialidades Disponibles.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/espsuc.png'/ style='width:90%;heigth:90%'></p><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 25) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar referencia de Anuncios.</p><br>";
        d += "<p class='paragraphcontent'>Para agregar referencia de anuncios para las especialidades asociadas al Cliente, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la especialidad a agregar referencia de anuncios en la tabla <i>''Especialidades Asociadas''</i>.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/adversref.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En la columna <i>''Cantidad''</i>, deber&aacute; escribir la cantidad de anuncios que llevar&aacute; y en la Columna <i>''Descripci&oacute;n''</i>, deber&aacute; escribir la descripci&oacute;n de los anuncios.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/ESP/adversqtty.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li><b>NOTA: </b>Para agregar referencia de anuncios en Especialidades asociadas a alguna sucursal, es el mismo proceso, solamente que en la pesta&ntilde;a <i>''Asociar Especialidades a la Sucursal''<i>.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Marcas
function getcontentbrands(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 26) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Asociar Marcas al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para asociar alguna marca al cliente, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar o Buscar la marca que ser&aacute; asociada al cliente:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/getbrnasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ), de la columna <i>''Agregar''</i>. Al realizar esto, la Marca ya habr&aacute; sido asociada al cliente. La Marca asociada, ya no aparecer&aacute; en la tabla de Marca Disponibles.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/brndasoc.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 27) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar Marcas asociadas al Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para desasociar alguna marca al cliente, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la Tabla <i>''Marcas Asociadas''</i>, Ubicar o Buscar la marca que ser&aacute; desasociada del cliente:</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/dltbrndasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ), de la columna <i>''Eliminar''</i>. Al realizar esto, la marca ya habr&aacute; sido desasociada del cliente. La marca desasociada, volver&aacute; a aparecer en la tabla de Marcas Disponibles.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/getbrnasoc.png'/ style='width:90%;heigth:90%'></p><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 28) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Marcar tipo de Marca para el Cliente (Distribuidor o Representante).</p><br>";
        d += "<p class='paragraphcontent'>En la Tabla <i>''Marcas Asociadas''</i>, se encuentran dos columnas <i>''Distribuidor'' y ''Representante''</i>, las cuales corresponden al tipo de marca con la que participara el Cliente en la Edici&oacute;n.</p><br>";
        d += "<ul class='paragraphcontent'><li>Para Marcar como Distribuidor al cliente de la Marca, solo hay que seleccionar el campo correspondiente a la marca, cuando este palomeado el campo, el cliente tendr&aacute; la marca como Distribuidor.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/brnddis.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Para Marcar como Representante al cliente de la Marca, solo hay que seleccionar el campo correspondiente a la marca, cuando este palomeado el campo, el cliente tendr&aacute; la marca como Representante.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANDS/brndrep.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Sucursales
function getcontentbranch(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 29) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar sucursal al Cliente seleccionado.</p><br>";
        d += "<p class='paragraphcontent'>Para agregar una Sucursal al cliente seleccionado, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el bot&oacute;n <i>''Agregar Sucursal''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/btnaddbranch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Aparecer&aacute; el siguiente formulario, donde deber&aacute; escribir <i>''Nombre de Sucursal'' y ''Nombre Corto''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/frmaddbranch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><b>NOTA: </b>El campo <i>''Nombre de Sucursal''</i>, tiene un asterisco rojo (<b style='color:red;font-size:20px'> * </b>), lo que indica que &eacute;l campo es obligatorio y no puede quedar vac&iacute;o.</p><br>";
        d += "<ul class='paragraphcontent'><li>Cuando haya escrito los datos en los campos, el siguiente paso es dar clic en el bot&oacute;n Guardar ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li>";
        d += "<li>Si todo esta correcto, la sucursal agregada aparecer&aacute; en la tabla.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/insbranch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si la Sucursal ya existe para el Cliente, el sistema, mostrar&aacute; un mensaje de error.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/errbranchexist.png'/ style='width:90%;heigth:90%'></p><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 30) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Editar Sucursal.</p><br>";
        d += "<p class='paragraphcontent'>Para editar alguna Sucursal, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'>";
        d += "<li>Ubicar la sucursal que desea editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''</i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la sucursal.</li>";
        d += "<li>Se habilitaran los campos de las columnas <i>''Nombre de Sucursal'' y ''Nombre Corto''</i>, el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li>";
        d += "</ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/edtaddrcl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez, habilitados los campos de las columnas <i>''Nombre de Sucursal'' y ''Nombre Corto''</i>, puede proceder a realizar las modificaciones al nombre de la sucursal, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/saveaddr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La(s) modificacion(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/resulteditaddr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li></ul>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 31) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar direcci&oacute;n Matriz</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, permitir&aacute; al Usuario Agregar una Direcci&oacute;n a la Matriz (Cliente) o Editar alguna Direcci&oacute;n ya existente para la Matriz (Cliente). Para entrar a la secci&oacute;n de las Direcciones de la Matriz (Cliente), deber&aacute; ubicar y dar clic en el bot&oacute;n <i>''Ver/Editar Direcci&oacute;n Matriz''</i>. Una vez que rea redirigido tendr&aacute; dos Opciones, las cuales se describen a continuaci&oacute;n:</p><br>";
        d += "<ol class='paragraphcontent'>";
        d += "<li>Agregar una Direcci&oacute;n a la Matriz (Cliente).";
        d += "<ul>";
        d += "<li>Ubicar y dar clic en el bot&oacute;n <i>''Agregar Direcci&oacute;n''</i>.</li>";
        d += "<li>Aparecer&aacute; el siguiente formulario, donde deber&aacute; llenar los campos <i>''Calle'', ''Colonia'', ''Ciudad'', ''C&oacute;digo Postal'', ''Email'', ''Web''.</i></li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/frminsaddrcl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Al haber llenado los campos necesarios, el siguiente paso es dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), para agregar la direcci&oacute;n. La nueva direcci&oacute;n aparecer&aacute; en la tabla de direcciones.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addrins.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";
        d += "<li>Para Editar alguna direcci&oacute;n de la Matriz (Cliente), siga los siguientes pasos:";
        d += "<ul>";
        d += "<li>Ubicar la direcci&oacute;n que desea editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''</i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la direcci&oacute;n.</li>";
        d += "<li>Se habilitaran todos los campos para su edici&oacute;n, el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/edtaddrscls.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Una vez, habilitados los campos, puede proceder a realizar las modificaciones necesarias, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/btnsaveadd.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>La(s) modificacion(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addsave.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li>";
        d += "</ul>";
        d += "</li>";
        d += "</ol><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 32) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar tel&eacute;fonos de direcc&oacute;n Matriz</p><br>";
        d += "<p class='paragraphcontent'>Adem&aacute;s de Agregar y Editar Direcciones, el Usuario tiene la posibilidad de agregar y Editar el o los tel&eacute;fonos que tenga una direcci&oacute;n, para realizar estas operaciones, el proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el bot&oacute;n ( <img src='../Images/buscar.png' style='height:25px;width:25px;'> ) correspondiente a la Direcci&oacute;n de la columna <i>''Ver/Editar Tel&eacute;fonos''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/btnviewphn.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n ser&aacute; redirigido a la siguiente pantalla:.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/screenphn.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En esta pantalla tendr&aacute; dos opciones disponibles para su operaci&oacute;n, las cuales se describen a continuaci&oacute;n:</li></ul><br>";
        d += "<ol class='paragraphcontent'>";
        d += "<li>Agregar un Tel&eacute;fono a la Direcci&oacute;n:";
        d += "<ul class='paragraphcontent'>";
        d += "<li>Ubicar y dar clic en el bot&oacute;n <i>''Agregar Tel&eacute;fono''</i>.</li>";
        d += "<li>Aparecer&aacute; un formulario en donde el usuario deber&aacute; llenar los campos que son <i>''Tipo de Tel&eacute;fono'', ''Lada'' y ''N&uacute;mero''</i>.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addphncl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Al tener los campos llenos, el siguiente paso es dar clic en el bot&oacute;n  ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), para que el tel&eacute;fono sea agregado a la direcci&oacute;n.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addedphncl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";
        d += "<li>Editar alg&uacute;n tel&eacute;fono de la Direcci&oacute;n.";
        d += "<ul>";
        d += "<li>Ubicar el tel&eacute;fono que desea Editar.</li>";
        d += "<li>Dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), de la columna <i>''Editar''</i> del tel&eacute;fono correspondiente. Los campos <i>''Lada'' y ''Tel&eacute;fono''</i> se habilitar&aacute;n para que pueda realizar las modificaciones.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/edtphncl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Realizar las modificaciones necesarias y dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y los cambios se guardar&aacute;n.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/edtphone.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Los cambios se reflejaran en la tabla de tel&eacute;fonos.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/phnedt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";
        d += "</ol>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 33) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar direcci&oacute;n Sucursal</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, permitir&aacute; al Usuario Agregar una Direcci&oacute;n a alguna Sucursal o Editar alguna Direcci&oacute;n ya existente para la Sucursal. Para entrar a la secci&oacute;n de las Direcciones de la Sucursal, deber&aacute; ubicar dentro de la tabla a la sucursal que desea y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) de la columna <i>''Ver/Editar Direcci&oacute;n Sucursal''</i>. Una vez que rea redirigido tendr&aacute; dos Opciones, las cuales se describen a continuaci&oacute;n:</p><br>";
        d += "<ol class='paragraphcontent'>";
        d += "<li>Agregar una Direcci&oacute;n a la Sucursal";
        d += "<ul>";
        d += "<li>Ubicar y dar clic en el bot&oacute;n <i>''Agregar Direcci&oacute;n''</i>.</li>";
        d += "<li>Aparecer&aacute; el siguiente formulario, donde deber&aacute; llenar los campos <i>''Calle'', ''Colonia'', ''Ciudad'', ''C&oacute;digo Postal'', ''Email'', ''Web''</i>.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addadrssuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Al haber llenado los campos necesarios, el siguiente paso es dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), para agregar la direcci&oacute;n. La nueva direcci&oacute;n aparecer&aacute; en la tabla de direcciones.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/adrssuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";
        d += "<li>Para Editar alguna direcci&oacute;n de la Sucursal, siga los siguientes pasos:";
        d += "<ul>";
        d += "<li>Ubicar la direcci&oacute;n que desea editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''</i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la direcci&oacute;n.</li>";
        d += "<li>Se habilitaran todos los campos para su edici&oacute;n, el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/fieldsactiveaddr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Una vez, habilitados los campos, puede proceder a realizar las modificaciones necesarias, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/btnsaveaddrsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>La(s) modificaci&oacute;n(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/saveaddrsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li>";
        d += "</ul>";
        d += "</li>";
        d += "</ol><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 34) {
        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar tel&eacute;fonos de direcc&oacute;n Sucursal</p><br>";
        d += "<p class='paragraphcontent'>Adem&aacute;s de Agregar y Editar Direcciones a la sucursal, el Usuario tiene la posibilidad de agregar y Editar el o los tel&eacute;fonos que tenga una direcci&oacute;n, para realizar estas operaciones, el proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el bot&oacute;n ( <img src='../Images/buscar.png' style='height:25px;width:25px;'> ) correspondiente a la Direcci&oacute;n de la columna <i>''Ver / Editar Tel&eacute;fonos''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/edtphonessuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n ser&aacute; redirigido a la siguiente pantalla:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/screenphnsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>En esta pantalla tendr&aacute; dos opciones disponibles para su operaci&oacute;n, las cuales se describen a continuaci&oacute;n:</p><br>";
        d += "<ol class='paragraphcontent'>";
        d += "<li>Agregar un Tel&eacute;fono a la Direcci&oacute;n:";
        d += "<ul class='paragraphcontent'>";
        d += "<li>Ubicar y dar clic en el bot&oacute;n <i>''Agregar Tel&eacute;fono''</i>.</li>";
        d += "<li>Aparecer&aacute; un formulario en donde el usuario deber&aacute; llenar los campos que son <i>''Tipo de Tel&eacute;fono'', ''Lada'' y ''N&uacute;mero''</i>.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addphnsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Al tener los campos llenos, el siguiente paso es dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), para que el tel&eacute;fono sea agregado a la direcci&oacute;n.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/addedphnsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";

        d += "<li>Editar alg&uacute;n tel&eacute;fono de la Direcci&oacute;n.";
        d += "<ul class='paragraphcontent'>";
        d += "<li>Ubicar el tel&eacute;fono que desea Editar.</li>";
        d += "<li>Dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), de la columna <i>''Editar''</i> del tel&eacute;fono correspondiente. Los campos <i>''Lada'' y ''Tel&eacute;fono'' se habilitar&aacute;n para que pueda realizar las modificaciones.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/editphonesuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Realizar las modificaciones necesarias y dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y los cambios se guardar&aacute;n.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/fieldsactivephn.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Los cambios se reflejaran en la tabla de tel&eacute;fonos.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/SM/BRANCH/tablephnsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "</ul>";
        d += "</li>";
        d += "</ol>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


function getmenues(element1, element2) {

    $(".simg").show();
    $(".himg").hide();

    var Id = $(element1).attr("Id");

    $(element1).hide();
    $(element2).show();

    Id = Id.replace("simg", "");

    var ul = "#level2Id" + Id + "";

    $(".listlevel").hide();

    $(ul).show();

}


function closemenues(element1, element2) {
    $(".simg").show();
    $(".himg").hide();

    $(element2).hide();
    $(element1).show();

    $(".listlevel").hide();
}


function clicreference() {
    $("#1ab").trigger("click");
}


function clicreferencePH() {
    $("#8ab").trigger("click");
}


function searchcnt(element, menu) {
    $.ajax({
        url: "../Help/search/",
        type: "POST",
        dataType: "json",
        data: { term: element, file: menu },
        success: function (data) {
            $("#divcontentusermanual").empty();
            $.each(data, function (index, val) {
                $("#divcontentusermanual")
                .append("<br>")
                    .append(val.content);
            })
        }
    })
}



/*                                                                              PRODUCCION                                                                              */

function clicreferenceacp() {
    $("#3ab").trigger("click");
}


//Products
function getcontentproductsPROD(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 1) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar ProductShot a Producto</p><br>";
        d += "<p class='paragraphcontent'>Para agregar un ProductShot a un Producto, siga los siguientes pasos:.</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el producto al que desea asociar ProductShot, y dar clic en la Imagen de la columna <i>''SIDEF''</i> del producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/clicsidef.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li><i>GuiaNet</i>, lo redirigir&aacute; a la siguiente venta, la cual se describe a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/screenimgs.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Formulario <i>''Agregar ProductShot''</i>.</td><td class='paragraphcontent'>Contiene los campos para que pueda agregar ProductShot al producto seleccionado.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>R&oacute;tulo</td><td class='paragraphcontent'>Mostrar&aacute; el nombre del Producto seleccionado.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Tabla</td><td class='paragraphcontent'>Mostrar&aacute; el nombre de imagen, el tama&ntilde;o y un previo del ProductShot, asociado al producto seleccionado.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";
        d += "<p class='paragraphcontent'>Una vez en esta pantalla, los pasos para agregar un ProductShot, son los siguientes:</p><br>";
        d += "<ul class='paragraphcontent'><li>Deber&aacute; seleccionar un tama&ntilde;o de imagen.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/selectsize.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez seleccionado el tama&ntilde;o de la imagen, dar clic en el bot&oacute;n <i>''Seleccione''</i>, aparecer&aacute; un explorador de archivos, en donde el usuario ubicar&aacute; la imagen que desea asociar al producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/selectimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ya seleccionada la imagen, dar clic en el bot&oacute;n <i>''Abrir''</i>.</li>";
        d += "<li>Ahora, el nombre de la imagen aparece en el campo <i>''Seleccionar Imagen''</i>, y el siguiente paso es dar clic en el bot&oacute;n <i>''Insertar''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/btninsimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La imagen habr&aacute; sido asociada al producto seleccionado y la tabla se actualiza mostrando la imagen asociada.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/imgins.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Columna <i>''Nombre de Imagen''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de la Imagen, que lo compone el nombre del Cliente y el nombre del Producto, as&iacute; como su extensi&oacute;n.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Columna <i>''Tama&ntilde;o''</i>.</td><td class='paragraphcontent'>Mostrar&aacute; el tama&ntilde;o que haya sido seleccionado para insertar la imagen.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Columna <i>''Imagen''</i>.</td><td class='paragraphcontent'>Mostrar&aacute; un previo de la imagen asociada al producto.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";
        d += "<ul class='paragraphcontent'><li>En caso de que el usuario desee insertar una imagen, sin haber seleccionado alg&uacute;n tama&ntilde;o, <i>GuiaNet</i>, mostrar&aacute; el siguiente mensaje de error.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/otrimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/errnotsize.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si selecciona el Tama&ntilde;o de la imagen pero no selecciona la imagen, <i>GuiaNet</i>, mostrar&aacute; el siguiente mensaje de error.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/notimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/errnotimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><b>NOTA:</b> Para actualizar una imagen hacer el proceso de <i>''Agregar ProductShot a Producto''</i></p><br>";
        d += "<p class='paragraphcontent'>Al regresar a la pantalla principal de <i>''Productos Participantes''</i>, se mostrar&aacute; la imagen que se ha asociado al Producto.</i></p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/insimg.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 2) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Producto</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, incorpora la funcionalidad de poder asociar un archivo PDF a Productos Participantes. El proceso de asociaci&oacute;n se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el Producto al que desea asociar un archivo PDF.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/prdaddpdf.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si el producto no tiene ning&uacute;n archivo PDF asociado, en la columna <i>''Asociar Archivo PDF''</i>, aparecer&aacute; el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ), dar clic en el bot&oacute;n correspondiente a la fila del producto.</li>";
        d += "<li>Al dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ), dentro de la misma columna aparecer&aacute; un peque&ntilde;o formulario para cargar el archivo PDF.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/actfrminspdf.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al aparecer el formulario, dar clic en el bot&oacute;n <i>''Seleccione...''</i>, al dar clic, aparecer&aacute; un explorador de archivos, en donde el usuario ubicar&aacute; el archivo PDF que desea asociar al producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/selectpdf.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ya seleccionado el archivo, dar clic en el bot&oacute;n <i>''Abrir''</i>.</li>";
        d += "<li>Ahora el nombre del Archivo PDF, aparecer&aacute; en el campo del formulario.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/frmslcpdf.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>El siguiente paso es dar clic en el bot&oacute;n <i>''Insertar''</i>.</i>.</li>";
        d += "<li>Ya asociado el archivo PDF al producto, el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ) cambiar&aacute; al bot&oacute;n ( <img src='../Images/pdfproductionfilecontent.png' style='height:30px;width:30px;'> ), adem&aacute;s de que en la columna <i>''Ver Archivo PDF''</i>, aparecer&aacute; el bot&oacute;n ( <img src='../Images/lookfiles.png' style='height:30px;width:30px;'> ), para visualizar el archivo asociado.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/inspdf.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n ( <img src='../Images/lookfiles.png' style='height:30px;width:30px;'> ), se abrir&aacute; el archivo PDF que tenga asociado el producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PP/pdf.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 3) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Asociar HTML a Productos</p><br>";
        d += "<p class='paragraphcontent'>Se ha incorporado a GuiaNet, la opci&oacute;n de que el usuario pueda agregar contenido HTML, Insertar atributos nuevos resultantes de la formaci&oacute;n, asociar atributos con atributos maestros.<br>";
        d += "<ul class='paragraphcontent'><li>Para entrar a la ventana donde podr&aacute; realizar estas operaciones, debe ubicar el producto al que desea asociar contenido, en la columna <i>''Asociar HTML''</i>, dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:30px;width:30px;'> ) correspondiente al producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/btnhtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, GuiaNet, lo redirigir&aacute; a la siguiente ventana, la cual se describe a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/screenhtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Posici&oacute;n en el Sistema.</td><td class='paragraphcontent'>Muestra la Posici&oacute;n actual del Usuario en el Sistema.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>R&oacute;tulos.</td><td class='paragraphcontent'>Muestra los datos de los filtros que se llenaron de la ventana anterior, que son Pa&iacute;s, Obra, Edici&oacute;n, Cliente y adicional, el nombre del producto al que se asociar&aacute; contenido.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Conjunto de Pesta&ntilde;as.</td><td class='paragraphcontent'>Cada una de las pesta&ntilde;as, tiene una funci&oacute;n diferente, dependiendo de su t&iacute;tulo.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Bot&oacute;n Regresar.</td><td class='paragraphcontent'>Regresa al usuario a la ventana principal de <i>''Productos Participantes''</i>.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";
        d += "<ul class='paragraphcontent'><li>Al entrar por primera vez a la ventana de <i>''Asociar HTML''</i>, la pesta&ntilde;a <i>''Cargar HTML''</i>, estar&aacute; habilitada. </li></ul><br>";
        d += "<p class='paragraphcontent'>Para comenzar con la carga de contenidos. El proceso se describe a continuaci&oacute;n:<br>";
        d += "<ul class='paragraphcontent'><li>En el formulario de la pesta&ntilde;a <i>''Cargar HTML''</i>, dar clic en el bot&oacute;n <i>''Seleccione...''</i>, al dar clic, aparecer&aacute; un explorador de archivos, en donde el usuario ubicar&aacute; el archivo HTML que desea procesar para el Producto seleccionado.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/selecthtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez seleccionado el archivo, dar clic en el bot&oacute;n <i>''Abrir''</i>, al dar clic en el bot&oacute;n, el nombre del archivo aparecer&aacute; en el campo de seleccionar archivo.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/htmlname.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>El siguiente paso es dar clic en el bot&oacute;n <i>''Insertar''</i>. Si el archivo HTML est&aacute; formado correctamente, se insertar&aacute; el contenido de HTML y XML, de lo contrario, <i>GuiaNet</i>, arrojar&aacute; un mensaje de error.</li>";
        d += "<li>Si todo esta correcto, el sistema mostrar&aacute; el R&oacute;tulo <i>''El Producto ya tiene Contenido''</i>, que har&aacute; referencia a que el producto ya tiene contenido en ParticipantProducts.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/inshtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizando este proceso, y si todo esta correcto, ser&aacute; todo lo que se realizar&aacute; en esta pesta&ntilde;a y podremos pasar a la pesta&ntilde;a <i>''Atributos de HTML''</i>.</li>";
        d += "<li>Si existe alg&uacute;n error, deber&aacute; de revisar en el archivo HTML que desee asociar, que todas las etiquetas est&eacute;n correctas, y volver a repetir el proceso.</li>";
        d += "<li>Ahora, pasamos a la siguiente pesta&ntilde;a que es <i>''Atributos de HTML''</i>, al dar clic en esta opci&oacute;n, aparecer&aacute; su contenido de la pesta&ntilde;a.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/attributes.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En el contenido de la Pesta&ntilde;a, aparece una tabla con los atributos que resultaron del archivo HTML ingresado anteriormente, frente a cada uno de los atributos tienen una referencia ( <img src='../Images/content.png' style='height:30px;width:30px;'> ) en la columna <i>''Existe en BD''</i>, de que el atributo existe actualmente en la BD.</li>";
        d += "<li>En este procedimiento, el Usuario, revisar&aacute; estos rubros, si alguno est&aacute; mal escrito, deber&aacute; corregirlo en el archivo HTML cargado anteriormente, y volver a repetir el proceso.</li>";
        d += "<li>Si todos los atributos son correctos, entonces se proceder&aacute; a insertar los atributos que no est&eacute;n en la base de datos.</li>";
        d += "<li>Para realizar este procedimiento de inserci&oacute;n, dar clic en el bot&oacute;n <i>''Verificar''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/verifyattributes.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, los atributos quedar&aacute;n insertados en la BD, ahora, todos los atributos del archivo HTML, tendr&aacute;n la referencia de que existen en la BD.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/attributesins.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizando esta operaci&oacute;n, el siguiente paso es asociar los atributos que acabamos de agregar, con los atributos maestros. Deber&aacute; dar clic en la pesta&ntilde;a <i>''Asociar a Atributos Maestros''</i>, y aparecer&aacute; su contenido de esta pesta&ntilde;a.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/attibutegroup.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Dentro de esta pesta&ntilde;a, encontr&aacute;remos una tabla con tres columnas, <i>''Atributos de Formaci&oacute;n''</i>, que son los atributos resultantes del archivo HTML, <i>''Atributos Maestros Asociados''</i>que son los atributos maestros que tenga asociados cada uno de los atributos de la  formaci&oacute;n, y <i>''Atributos Maestros''</i>, en esta columna contendr&aacute; los atributos maestros actualmente en la BD.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/asociateattr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Para asociar un Atributo Nuevo de Formaci&oacute;n con un Atributo Maestro, en la columna <i>''Atributos Maestros''</i>, deber&aacute; seleccionar el atributo maestro que desee asociar al Atributo Nuevo de Formaci&oacute;n.</li>";
        d += "<li>Al realizar esta operaci&oacute;n, los atributos habr&aacute;n quedado asociados, y el nombre del Atributo maestro aparecer&aacute; en la Columna <i>''Atributos Maestros Asociados''</i>, correspondiente a la fila del Atributo de Formaci&oacute;n.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/attrasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Es necesario asociar todos los Atributos de Formaci&oacute;n con Atributos Maestros para Continuar. De lo contrario, no podr&aacute; agregar ning&uacute;n contenido por atributos.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/allattrasoc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez que est&eacute;n todos los atributos asociados, podr&aacute; Continuar a la Siguiente pesta&ntilde;a, que es <i>''Insertar Contenidos por Atributos''</i>, al dar clic en esta opci&oacute;n, aparecer&aacute; su contenido de la pesta&ntilde;a.</li>";
        d += "<li>Ahora, estando en esta pantalla, aparece el r&oacute;tulo <i>''Todo est&aacute; Listo!!!, Ahora puede cargar el Contenido''</i>, el siguiente paso en esta pantalla, es dar clic en el bot&oacute;n <i>''Cargar Contenido''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/btnchargecnt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, <i>GuiaNet</i> comenzar&aacute; a realizar el proceso de segmentar el contenido por Atributos, al terminar, se visualizar&aacute; el contenido por atributos en la pesta&ntilde;a <i>''Ver Contenido por Atributos''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/cnthtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Adem&aacute;s, de poder ver el Contenido segmentado por Atributos, el Usuario tambi&eacute;n tiene la opci&oacute;n de ver el contenido HTML completo. Al dar clic en la pesta&ntilde;a <i>''Ver HTML''</i>, se visualizara contenido del archivo HTML.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/parsehtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>De igual manera, el Usuario puede ver la estructura del archivo XML, dando clic en la pesta&ntilde;a <i>''Ver XML''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/cntxml.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 4) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Segmentar Productos</p><br>";
        d += "<p class='paragraphcontent'>El proceso para la carga de archivos HTML se describe a continuaci&oacute;n:<br>";
        d += "<ul class='paragraphcontent'><li>En el formulario de la pesta&ntilde;a <i>''Cargar HTML''</i>, dar clic en el bot&oacute;n <i>''Seleccione...''</i>, al dar clic, aparecer&aacute; un explorador de archivos, en donde el usuario ubicar&aacute; el archivo HTML que desea segmentar por productos.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/selectcompletehtml.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ya seleccionado el archivo, dar clic en el bot&oacute;n <i>''Abrir''</i>.</li>";
        d += "<li>Al dar clic en el bot&oacute;n, el nombre del archivo HTML, aparecer&aacute; en el campo <i>''Seleccione Archivo''</i>.</li>";
        d += "<li>El siguiente paso es dar clic en el bot&oacute;n <i>''Insertar''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/completehtmlname.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><b>NOTA:</b> Si el archivo HTML, est&aacute; correctamente formado, el proceso se completar&aacute;, y podremos seguir a la siguiente pesta&ntilde;a <i>''Resumen''</i>. De lo contrario, <i>GuiaNet</i> arrojar&aacute; un mensaje de error en el archivo, donde deber&aacute; corregir el archivo HTML y repetir el proceso.<br>";
        d += "<ul class='paragraphcontent'><li>En la pesta&ntilde;a <i>''Resumen''</i>, se mostrar&aacute; el nombre de los archivos segmentados y la cantidad de Atributos por cada archivo.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/qttyattr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ahora, puede continuar en la pesta&ntilde;a <i>''Asociar Archivo HTML a Producto''</i>.</li>";
        d += "<li>En esta pesta&ntilde;a, se muestra una tabla que contiene los Productos Participantes en la edici&oacute;n seleccionada, con el nombre del Producto, con referencias si ya tiene contenido HTML y contenido asociado por atributos, el cliente al que pertenece cada producto, as&iacute; como los productos resultantes de la segmentaci&oacute;n.</li>";
        d += "<li>Para realizar la asociaci&oacute;n de un archivo resultante a un Producto, siga los siguientes pasos:";
        d += "<ul><li>Ubicar el producto al que desea asociar el archivo HTML.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/prodwithcnt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>En la columna <i>''HTML de Productos''</i>, desplegar las opciones.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/selecthtmlfromlist.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Seleccionar el nombre del archivo HTML.</li></ul></li><br>";
        d += "<li>En el caso que el producto ya tenga asociado contenido. Mostrar&aacute; el siguiente mensaje de confirmaci&oacute;n.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/msgreplacecnt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si desea cancelar la operaci&oacute;n dar clic en el bot&oacute;n <i>''X''</i>.</li>";
        d += "<li>Al dar clic en el bot&oacute;n <i>''Ok''</i>, comenzar&aacute; el proceso de asociaci&oacute;n.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/rplcnt.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al terminar el proceso de asociaci&oacute;n, en la columna <i>''Contenido HTML''</i>, tendr&aacute; la referencia de que tiene contenido HTML, en la columna <i>''Contenido asociado por Atributos''</i> no tendr&aacute; ninguna referencia, esto quiere decir, que el proceso a&uacute;n no ha terminado.</li>";
        d += "<li>Para continuar con el proceso, ver punto <span style='cursor:pointer; color:#065977' id='spn' onclick='clicreferenceacp();'><i><b><u>Agregar Producto</u></b></i></span> <i>''6.3 Asociar HTML a Producto''</i> (Al regresar a este punto, el usuario deber&aacute; comenzar el proceso desde la pesta&ntilde;a <i>''Atributos de HTML''</i>).</li>";
        d += "<li>Al terminar el proceso, se mostrar&aacute;n las dos referencias de contenido en el producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/HTML/cntattributes.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 5) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Paginar Clientes</p><br>";
        d += "<p class='paragraphcontent'>Para agregar la p&aacute;gina del cliente en la obra, siga los siguientes pasos:<br>";
        d += "<ul class='paragraphcontent'><li>Ubicar al cliente que quiere agregar la p&aacute;gina.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PAGES/selectclient.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ya ubicado el Cliente, en la columna <i>''P&aacute;gina''</i>, escribir la pagina en el campo.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/PAGES/savepage.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al quitar el cursor del campo, quedar&aacute; guardada la p&aacute;gina para el cliente.</li></ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }
}


//Especialidades
function getcontentspecialitiesPROD(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 6) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Especialidades asociadas a los Clientes.</p><br>";
        d += "<ul class='paragraphcontent'><li>Al completar los filtros, aparecer&aacute;n los clientes participantes en la Edici&oacute;n. La ventana se describe a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/ESP/screenesp.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Posici&oacute;n.</td><td class='paragraphcontent'>Muestra la posici&oacute;n actual del usuario en el sistema.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Filtros.</td><td class='paragraphcontent'>Contiene los filtros por Pa&iacute;s, Obra, Edici&oacute;n y Cliente. Cuando se hayan seleccionado estos datos, los productos participantes correspondientes se mostrar&aacute;n en la tabla.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulos.</td><td class='paragraphcontent'>Mostrar&aacute; los datos que se hayan seleccionado en los filtros, los cuales son Pa&iacute;s, Obra, Esici&oacute;n y Cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Reporte de especialidades por Cliente.</td><td class='paragraphcontent'>Generar&aacute; un reporte de las especialidades que tenfa asociadas el Cliente o Sucursal seleccionado.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>Pesta&ntilde;as</td><td class='paragraphcontent'>Cambio de pantalla, dependiendo la selecci&oacute;n</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Tabla de Trabajo</td><td class='paragraphcontent'>Muestra los resultados de la selecci&oacute;n de filtros.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Columna <i>''Especialidades''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de la especialidad asociada al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Cantidad''</i>.</td><td class='paragraphcontent'>Muestra la Cantidad de anuncios que contendr&aacute; la especialidad asociada.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Descripci&oacute;n''</i>.</td><td class='paragraphcontent'>Muestra la Descripci&oacute;n de los anuncios que contendr&aacute; la especialidad asociada.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));


    }

    if (Id == 7) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar ProductShot a Producto</p><br>";
        d += "<p class='paragraphcontent'>Para entrar a las especialidades asociadas a Sucursales del Cliente seleccionado siga los siguientes pasos</p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez completados los filtros. Dar clic en la pesta&ntilde;a <i>''Especialidades de Sucursal''</i>.</li>";
        d += "<li>Al cambiar la ventana, el siguiente paso es seleccionar la sucursal.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/ESP/selectbranchesp.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al seleccionar la sucursal, la ventana se actualizar&aacute; mostrando las especialidades asociadas a la sucursal.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/ESP/screenespbranch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La interfaz es la misma que con los Clientes, con la diferencia que se muestra el nombre de la sucursal seleccionada y el filtro para cambiar de sucursal.</li></ul><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 8) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reportes de Especialidades asociadas a Clientes o Sucursales.</p><br>";
        d += "<p class='paragraphcontent'>El usuario tiene la posibilidad de generar reportes de las especialidades que tenga asociadas un Cliente o sucursal. Siga los siguientes pasos para generar un reporte:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la Pesta&ntilde;a <i>''Especialidades de Clientes''</i>, ubicar y dar clic en el icono ( <img src='../Images/pdfespecialitiesbyclientpng.png' style='height:30px;width:30px;'> ), al realizar esta acci&oacute;n, se generar&aacute; el reporte. La estructura se describe a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/ESP/rptespcl.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>R&oacute;tulo Especialidades.</td><td class='paragraphcontent'>Muestra el total de especialidades actualmente asociadas al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Descripci&oacute;n''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de la especialidad asociada al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Cantidad''</i>.</td><td class='paragraphcontent'>Muestra la Cantidad de los anuncios que contendr&aacute; la especialidad asociada.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>10</td><td class='paragraphcontent'>Columna <i>''Descripci&oacute;n de Anuncios''</i>.</td><td class='paragraphcontent'>Muestra la Descripci&oacute;n de los anuncios que contendr&aacute; la especialidad asociada.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";
        d += "<p class='paragraphcontent'>En el caso de que se requiera generar el reporte de especialidades asociadas a alguna sucursal del cliente seleccionado, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la pesta&ntilde;a <i>''Especialidades de sucursal''</i>, ubicar y dar clic en el bot&oacute;n ( <img src='../Images/pdfespecialitiesbybranchpng.png' style='height:30px;width:30px;'> ), al realizar la acci&oacute;n, se generar&aacute; el reporte, la estructura es similar a la anterior, con la diferencia que aparece el nombre del Cliente, as&iacute; como de la sucursal seleccionada.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/ESP/rptespsuc.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }
}


//Marcas
function getcontentbrandsPROD(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 9) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Cambiar cantidad de resultados por ventana</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, ofrece al usuario la posibilidad de cambiar la cantidad de registros que se muestren por pantalla, para realizar esta función, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el selector de Resultados.</li>";
        d += "<li>Al dar clic se desplegaran las opciones disponibles para aplicar, que son:";
        d += "<ol>";
        d += "<li><b>Seleccione:</b> Es el valor predeterminado del selector.</li>";
        d += "<li><b>10:</b> Mostrara diez resultados por pantalla.</li>";
        d += "<li><b>50:</b> Mostrara cincuenta  resultados por pantalla.</li>";
        d += "<li><b>100:</b> Mostrara cien resultados por pantalla.</li>";
        d += "<li><b>Ilimitado:</b> Mostrara todos los resultados obtenidos.</li>";
        d += "</ol>";
        d += "</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/selectquantity.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al seleccionar cualquiera de estas opciones, la tabla se actualizará mostrando la cantidad de los registros seleccionados.</li>";
        d += "<li>Al escoger alguna de las opciones disponibles, en el selector quedará marcada la opción seleccionada.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/qttyslct.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));


    }

    if (Id == 10) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de marcas asociadas al Cliente</p><br>";
        d += "<p class='paragraphcontent'>Para generar un reporte de las Marcas asociadas al cliente seleccionado, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el icono ( <img src='../Images/pdfbrandsbyclientpng.png' style='height:30px;width:30px;'> ), al realizar esta acci&oacute;n se generar&aacute; el reporte, del cual,  se describe a continuaci&oacute;n su estructura.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/rptstruct.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>R&oacute;tulo Marcas.</td><td class='paragraphcontent'>Muestra el total de Marcas actualmente asociadas al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Marcas''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de la Marca asociada al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Tipo''</i>.</td><td class='paragraphcontent'>Muestra el Tipo de Marca, <i>''Representante Exclusivo'' y ''Distribuidor Autorizado''</i>.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>10</td><td class='paragraphcontent'>Columna <i>''Logo''</i>.</td><td class='paragraphcontent'>Muestra una referencia de las Marcas que ya tienen un logo asociado. </td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 11) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Editar nombre de Marca</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, ofrece al usuario la posibilidad de editar el nombre de alguna Marca, el proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la Marca que desee editar el nombre.</li>";
        d += "<li>Una vez, ubicada la Marca, dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:30px;width:30px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/fieldsactedtbrn.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, se habilitar&aacute; la casilla de la Marca, correspondiente a la columna <i>''Nombre de Marca''</i>, el bot&oacute;n ( <img src='../Images/edit.png' style='height:30px;width:30px;'> ) cambiar&aacute; por ( <img src='../Images/save.png' style='height:30px;width:30px;'> ) y ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/edtfields.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizar las modificaciones necesarias y dar clic ( <img src='../Images/save.png' style='height:30px;width:30px;'> ) para guardar los cambios.</li>";
        d += "<li>Si desea cancelar la modificaci&oacute;n, dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ), y ning&uacute;n cambio ser&aacute; guardado.</li></ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 12) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar im&aacute;genes a Marcas</p><br>";
        d += "<p class='paragraphcontent'>Para agregar im&aacute;genes en sus diferentes tama&ntilde;os a las marcas asociadas al cliente seleccionado, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la Marca que desea agregar im&aacute;genes.</li>";
        d += "<li>Una vez, ubicada la Marca, dar clic en el bot&oacute;n de la Columna <i>''Imagen de Marca''</i> correspondiente a la marca que desea agregar im&aacute;genes.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/clcimgaddimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n, <i>GuiaNet</i>, redirigir&aacute; al usuario a la siguiente ventana, donde agregar&aacute; las im&aacute;genes en sus diferentes tama&ntilde;os.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/screenaddimg.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Formulario.</td><td class='paragraphcontent'>Contiene los Campos para seleccionar tama&ntilde;o e imagen a insertar.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el nombre de la Marca seleccionada.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Bot&oacute;n Regresar.</td><td class='paragraphcontent'>Regresar a la pantalla anterior.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Tabla.</td><td class='paragraphcontent'>Muestra las im&aacute;genes asociadas a la marca, la tabla consta de Nombre de Imagen, Tama&ntilde;o, vista previa de las im&aacute;genes y la opci&oacute;n para eliminar las im&aacute;genes asociadas.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        d += "<ul class='paragraphcontent'><li>Para insertar im&aacute;genes a la marca, siga los siguientes pasos:";
        d += "<ul><li>En el formulario para agregar im&aacute;genes, seleccionar el tama&ntilde;o con el cual desea insertar la imagen.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/clcimgaddimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Despu&eacute;s de seleccionar el Tama&ntilde;o, el siguiente paso es seleccionar la imagen a insertar.</li>";
        d += "<li>Dar clic en el bot&oacute;n <i>''Seleccionar...''</i>.</li>";
        d += "<li>Se abrir&aacute; un explorador de archivos, donde el usuario deber&aacute; ubicar la imagen que desea asociar y dar clic en el bot&oacute;n <i>''Abrir''</i>.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/selectimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>Al dar clic en <i>''Abrir''</i>, aparecer&aacute; el nombre de la imagen en el cuadro <i>''Seleccionar Imagen''</i>.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/frminsimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<li>El siguiente paso es dar clic en el bot&oacute;n <i>''Insertar''</i>.</li>";
        d += "<li>Al dar clic en <i>''Insertar''</i>, la imagen se insertar&aacute; y aparecer&aacute; en la tabla de Im&aacute;genes asociadas.</li><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/imgins.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><b>NOTA:</b> Para actualizar una imagen hacer el proceso de <i>''Agregar im&aacute;genes a Marcas''</i></p>";
        d += "</ul></li></ul><br>";
        d += "<p class='paragraphcontent'>Al dar clic en el bot&oacute;n <i>''Regresar''</i>, regresar&aacute; a la pantalla anterior, y en la columna <i>''Imagen de Marca''</i>, aparecer&aacute; un previo de la imagen agregada a la marca.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/images.png'/ style='width:90%;heigth:90%'></p><br>";
        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 13) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar im&aacute;genes asociadas a Marcas</p><br>";
        d += "<p class='paragraphcontent'>El usuario podr&aacute; eliminar im&aacute;genes que est&eacute;n asociadas a alguna marca. El proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la imagen que desea eliminar de la Marca, y dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:30px;width:30px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/btndeleteimg.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n la imagen ser&aacute; eliminada de la marca.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANDS/deletedimg.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }
}


//Categorías
function getcontentcategoriesPROD(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 14) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Categor&iacute;as asociadas a Clientes o Sucursales.</p><br>";
        d += "<p class='paragraphcontent'>Una vez completados los filtros, aparecer&aacute;n los resultados de la selecci&oacute;n, en la siguiente ventana, la cual se describe a continuaci&oacute;n:</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/CAT/screencats.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Filtros.</td><td class='paragraphcontent'>Contiene los filtros para cambiar de Cliente y/o Edici&oacute;n.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra los datos de la selecci&oacute;n de filtros.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Tabla de Resultados.</td><td class='paragraphcontent'>Muestra las Categor&iacute;as con Sub Categor&iacute;as asociadas al cliente y la edici&oacute;n seleccionadas.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Reporte.</td><td class='paragraphcontent'>Generar reporte de Categor&iacute;as asociadas al cliente seleccionado.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));


    }

    if (Id == 15) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reportes de Categor&iacute;as asociadas a Clientes.</p><br>";
        d += "<p class='paragraphcontent'>Para generar un reporte de las Categor&iacute;as asociadas al Cliente. Dar clic en el bot&oacute;n ( <img src='../Images/pdfhetcategoriesbyclientpng.png' style='height:25px;width:25px;'>  )</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/CAT/btnreport.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n:</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/CAT/report.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>R&oacute;tulo Categor&iacute;as.</td><td class='paragraphcontent'>Muestra el total de Categor&iacute;as actualmente asociadas al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;as''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de la Categor&iacute;a asociada al cliente.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Sub Categor&iacute;a''</i>.</td><td class='paragraphcontent'>Muestra el Tipo de la Sub Categor&iacute;a asociada al cliente.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));


    }
}


//Sucursales
function getcontentbranchPROD(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 16) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Editar Sucursal.</p><br>";
        d += "<p class='paragraphcontent'>Para Editar alguna Sucursal, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la sucursal que desea editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''</i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la sucursal.</li>";
        d += "<li>Se habilitaran los campos de las columnas <i>''Nombre de Sucursal''</i> y <i>''Nombre Corto''</i>, el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li>";
        d += "</ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/fieldsactivateedtbranch.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez, habilitados los campos de las columnas <i>''Nombre de Sucursal''</i> y <i>''Nombre Corto''</i>, puede proceder a realizar las modificaciones al nombre de la sucursal, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/btnsavechanges.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La(s) modificaci&oacute;n(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/savedchanges.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li></ul><br>";
        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 17) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar direcci&oacute;n Matriz.</p><br>";
        d += "<p class='paragraphcontent'>Para Editar alguna direcci&oacute;n de la Matriz (Cliente), siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar la direcci&oacute;n que desea editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''</i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la direcci&oacute;n.</li>";
        d += "<li>Se habilitaran todos los campos para su edici&oacute;n, , el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li>";
        d += "</ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/fieldsactivatededtaddr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez, habilitados los campos, puede proceder a realizar las modificaciones necesarias, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/btnsaveaddress.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La(s) modificaci&oacute;n(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li>";
        d += "<li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li>";
        d += "</ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 18) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar tel&eacute;fonos de direcc&oacute;n Matriz.</p><br>";
        d += "<p class='paragraphcontent'>Adem&aacute;s de editar Direcciones, el Usuario tiene la posibilidad de Editar el o los tel&eacute;fonos que tenga una direcci&oacute;n, para realizar estas operaciones, el proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) correspondiente a la Direcci&oacute;n de la columna <i>''Ver / Editar Tel&eacute;fonos''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/clcbtnphones.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n ser&aacute; redirigido a la siguiente pantalla:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/screenphones.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>El proceso de edici&oacute;n para un tel&eacute;fono, se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el tel&eacute;fono que desea Editar.</li>";
        d += "<li>Dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), de la columna <i>''Editar''</i> del tel&eacute;fono correspondiente. Los campos “Lada” y “Tel&eacute;fono” se habilitar&aacute;n para que pueda realizar las modificaciones.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/clceditphone.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Realizar las modificaciones necesarias y dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y los cambios se guardar&aacute;n.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/clcsavechanges.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Los cambios se reflejaran en la tabla de tel&eacute;fonos.</li></ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 19) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar direcci&oacute;n Sucursal.</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, permitir&aacute; al Usuario Editar alguna Direcci&oacute;n ya existente para la Sucursal. Para entrar a la secci&oacute;n de las Direcciones de la Sucursal, deber&aacute; ubicar dentro de la tabla a la sucursal que desea y dar clic en el bot&oacute;n ( <img src='../Images/buscar.png' style='height:25px;width:25px;'> ) de la columna <i>''Ver / Editar Direcci&oacute;n Sucursal''<i>.</p><br>";
        d += "<p class='paragraphcontent'>Para Editar alguna direcci&oacute;n de la Sucursal, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el tel&eacute;fono que desea Editar.</li>";
        d += "<li>Dirigirse a la Columna <i>''Editar''<i>, y dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ) correspondiente a la fila de la direcci&oacute;n.</li>";
        d += "<li>Se habilitaran todos los campos para su edici&oacute;n, el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), cambia y aparecer&aacute;n los botones ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/activefieldsedtaddr.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez, habilitados los campos, puede proceder a realizar las modificaciones necesarias, al terminar presionar el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/btnsavechangesaddrsuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>La(s) modificaci&oacute;n(es), se habr&aacute;n realizado y se ver&aacute;n reflejadas en autom&aacute;tico en la tabla y los botones regresaran a su condici&oacute;n original.</li>";
        d += "<li>Si desea cancelar el proceso de modificaci&oacute;n, solamente necesita dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ) y ning&uacute;n cambio ser&aacute; guardado.</li></ul><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }

    if (Id == 20) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ver / Editar tel&eacute;fonos de direcc&oacute;n Sucursal.</p><br>";
        d += "<p class='paragraphcontent'>Adem&aacute;s de editar Direcciones a la sucursal, el Usuario tiene la posibilidad de editar el o los tel&eacute;fonos que tenga una direcci&oacute;n, para realizar estas operaciones, el proceso se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el bot&oacute;n ( <img src='../Images/buscar.png' style='height:25px;width:25px;'> ) correspondiente a la Direcci&oacute;n de la columna <i>''Ver / Editar Tel&eacute;fonos''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/clcbtnphonesbysuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic en el bot&oacute;n ser&aacute; redirigido a la siguiente pantalla:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/screenphonessuc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>El proceso de edici&oacute;n de tel&eacute;fonos, se describe a continuaci&oacute;n: </p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el tel&eacute;fono que desea Editar.</li>";
        d += "<li>Dar clic en el bot&oacute;n ( <img src='../Images/edit.png' style='height:25px;width:25px;'> ), de la columna <i>''Editar''</i> del tel&eacute;fono correspondiente. Los campos <i>''Lada'' y ''Tel&eacute;fono''</i> se habilitar&aacute;n para que pueda realizar las modificaciones.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/PROD/BRANCH/fieldsactivateedtphone.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<ul class='paragraphcontent'><li>Realizar las modificaciones necesarias y dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ) y los cambios se guardar&aacute;n.</li>";
        d += "<li>Los cambios se reflejaran en la tabla de tel&eacute;fonos.</li>";
        d += "<li>Si desea cancelar el proceso de edici&oacute;n del tel&eacute;fono dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ), y ning&uacute;n cambio ser&aacute; guardado.</li></ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));

    }
}


function searchcntPROD(element, menu) {
    $.ajax({
        url: "../Help/searchPROD/",
        type: "POST",
        dataType: "json",
        data: { term: element, file: menu },
        success: function (data) {
            $("#divcontentusermanual").empty();
            $.each(data, function (index, val) {
                $("#divcontentusermanual")
                .append("<br>")
                    .append(val.content);
            })
        }
    })
}


/*                                                                          LABORATORIO DE INFORMACIÓN                                                                  */

function getcontentproductsLI(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 1) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de Productos con Categor&iacute;a.</p><br>";
        d += "<p class='paragraphcontent'>Para generar un reporte de los Productos con sus Categor&iacute;as, debe ubicar y dar clic en la opci&oacute;n <i>''Reporte de Productos con Categor&iacute;a''</i> ( <img src='../Images/pdfprodcutcategoriespng.png' style='height:25px;width:25px;'> ).</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/clcrptpwcli.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n:</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/rptpcc.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Columna <i>''Producto''</i>.</td><td class='paragraphcontent'>Muestra el Nombre de los Productos.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 1''</i>.</td><td class='paragraphcontent'>Muestra el Primer nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 2''</i>.</td><td class='paragraphcontent'>Muestra el Segundo nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>10</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 3''</i>.</td><td class='paragraphcontent'>Muestra el Tercer nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>11</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 4''</i>.</td><td class='paragraphcontent'>Muestra el Cuarto nivel del &aacute;rbol de categor&iacute;as, y es al que se encuentran asociados los productos.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";


        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 2) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de Categor&iacute;as por Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para generar un reporte de las Categor&iacute;as por Cliente, debe ubicar y dar clic en la opci&oacute;n <i>''Reporte de Categor&iacute;as por Cliente''</i> ( <img src='../Images/pdfcategoriesbyclientpng.png' style='height:25px;width:25px;'> ).</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/clcrptcbc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n:</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/rptcbc.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 1''</i>.</td><td class='paragraphcontent'>Muestra el Primer nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 2''</i>.</td><td class='paragraphcontent'>Muestra el Segundo nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>9</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 3''</i>.</td><td class='paragraphcontent'>Muestra el Tercer nivel del &aacute;rbol de categor&iacute;as.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>10</td><td class='paragraphcontent'>Columna <i>''Categor&iacute;a Nivel 4''</i>.</td><td class='paragraphcontent'>Muestra el Cuarto nivel del &aacute;rbol de categor&iacute;as, y es al que se encuentran asociados los productos.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 3) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Reporte de Productos por Cliente.</p><br>";
        d += "<p class='paragraphcontent'>Para generar un reporte de las Categor&iacute;as por Cliente, debe ubicar y dar clic en la opci&oacute;n <i>''Reporte de Categor&iacute;as por Cliente''</i> ( <img src='../Images/pdfcategoriesbyclientpng.png' style='height:25px;width:25px;'> ).</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/clcrptpbyc.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>La estructura del reporte se describe a continuaci&oacute;n:</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/REP/rptpbyc.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Logo de Sistema.</td><td class='paragraphcontent'>Muestra el logo de GuiaNet.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Logo PLM.</td><td class='paragraphcontent'>Muestra el logo de PLM</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>R&oacute;tulo.</td><td class='paragraphcontent'>Muestra el tipo de reporte que se esta generando.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>R&oacute;tulo Cliente.</td><td class='paragraphcontent'>Muestra el Nombre del Cliente del cual se genera el reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>R&oacute;tulo Edici&oacute;n</td><td class='paragraphcontent'>Muestra la edici&oacute;n seleccionada de la cual se genera el reporte</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>6</td><td class='paragraphcontent'>Tabla de Trabajo.</i>.</td><td class='paragraphcontent'>Muestra los resultados del reporte.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>7</td><td class='paragraphcontent'>Columna <i>''Producto''</i>.</td><td class='paragraphcontent'>Muestra el Nombre del Producto.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>8</td><td class='paragraphcontent'>Columna <i>''Tipo''</i>.</td><td class='paragraphcontent'>Muestra una letra <i>''P''</i>, que indica que el producto es Participante en la edici&oacute;n.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

}


function getcontentclasificationLI(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 4) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ventana Clasificaci&oacute;n.</p><br>";
        d += "<p class='paragraphcontent'>Para realizar la clasificaci&oacute;n de alg&uacute;n producto con sus Categor&iacute;as, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar el Producto que desee clasificar.</li>";
        d += "<li>En la columna <i>''Clasificar''</i>, dar clic en el bot&oacute;n ( <img src='../Images/clasification.png' style='height:25px;width:25px;'> ), correspondiente a la fila del producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/screenprods.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'>El sistema lo redirigir&aacute; a la siguiente pantalla. La cual se describe a continuaci&oacute;n.</p><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/screencats.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>R&oacute;tulos.</td><td class='paragraphcontent'>Muestra la Informaci&oacute;n del Pa&iacute;s, Obra, Edici&oacute;n, Cliente y Nombre de Producto que se est&eacute; trabajando para clasificar.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Buscador Categor&iacute;as.</td><td class='paragraphcontent'>Realiza una b&uacute;squeda con el t&eacute;rmino introducido en el campo del buscador.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>&aacute;rbol de Categor&iacute;as con nueva estructura 4 niveles.</td><td class='paragraphcontent'>Muestra el &aacute;rbol completo de las categor&iacute;as actuales en la BD. El &aacute;rbol cambia al realizar una b&uacute;squeda.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Bot&oacute;n regresar.</td><td class='paragraphcontent'>Regresa a la ventana con los productos participantes.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>Buscador de Categor&iacute;as Asociadas.</td><td class='paragraphcontent'>Realiza una b&uacute;squeda de las categor&iacute;as asociadas al cliente, con el t&eacute;rmino introducido en el campo del buscador.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 5) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar categor&iacute;a a Producto.</p><br>";
        d += "<p class='paragraphcontent'>Para agregar una Categor&iacute;a a un Producto, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar o Usar el buscador de Categor&iacute;as para encontrar la categor&iacute;a que desea asociar al producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/selectcategory.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez ubicada la categor&iacute;a, basta con dar clic en el nombre de la Categor&iacute;a, en este caso <i>''Todos los Analizadores de Flujo de Gas''</i>.</li>";
        d += "<li>La Categor&iacute;a quedara asociada al producto y aparecer&aacute; en la tabla <i>''Categor&iacute;as Asociadas al Producto''</i>, la categor&iacute;a asociada aparecer&aacute; en la Tabla con sus niveles anteriores.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/catasociated.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><i><b>NOTA: </b>Solamente las Categor&iacute;as a nivel 4 son las &uacute;nicas que se pueden asociar al Producto.</i></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 6) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar categor&iacute;a asociada a Producto.</p><br>";
        d += "<p class='paragraphcontent'>Para eliminar una categor&iacute;a que est&eacute; asociada a un producto, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la tabla de <i>''Categor&iacute;as Asociadas al Producto''</i>, ubicar o buscar la categor&iacute;a que desea des asociar del producto seleccionado.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/catasociated.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En la columna <i>''Eliminar''</i>, dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ), y la categor&iacute;a desaparecer&aacute; de la tabla <i>''Categor&iacute;as Asociadas al Producto''</i>,  esto quiere decir, que ya no se encuentra asociada al producto.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/CLASIF/deletedcat.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

}


function getcontentrecategorizationLI(Id) {

    $("#divcontentusermanual").empty();

    Id = "" + Id + "";
    if (Id.includes("ab")) {
        Id = Id.replace("ab", "");
    }

    if (Id == 7) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Ventana Recategorizaci&oacute;n.</p><br>";
        d += "<p class='paragraphcontent'>Para entrar a la Secci&oacute;n de <i>''Re categorizaci&oacute;n''</i>, siga los siguientes pasos.</p><br>";
        d += "<ul class='paragraphcontent'><li>Dar clic en el Bot&oacute;n <i>''Laboratorio de Informaci&oacute;n''</i> que se encuentra en la parte superior.</li></ul><br>";
        d += "<ul class='paragraphcontent'><li>Se desplegara el men&uacute; de las secciones del M&oacute;dulo y Dar clic en la opci&oacute;n <i>''Re categorizaci&oacute;n''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/btnmenu.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al cargar la ventana, aparecer&aacute; de esta forma, las partes se describen a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/screenrecat.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Posici&oacute;n</td><td class='paragraphcontent'>Muestra la posici&oacute;n actual del usuario en el sistema.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>Pesta&ntilde;as de Opciones.</td><td class='paragraphcontent'>Muestra las pesta&ntilde;as disponibles.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Buscador.</td><td class='paragraphcontent'>Realiza una b&uacute;squeda de las categor&iacute;as que coincidan con el texto a buscar.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>4</td><td class='paragraphcontent'>Bot&oacute;n agregar Categor&iacute;a</td><td class='paragraphcontent'>Al dar clic, aparecer&aacute; un formulario para agregar nuevas categor&iacute;as, desde su primer nivel hasta el cuarto nivel.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>5</td><td class='paragraphcontent'>&Aacute;rbol de Categor&iacute;as.</td><td class='paragraphcontent'>Muestra el &aacute;rbol completo de las categor&iacute;as actualmente en la BD, con sus cuatro niveles de categor&iacute;as.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 8) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Proceso de Re Categorizaci&oacute;n.</p><br>";
        d += "<p class='paragraphcontent'>Para realizar el proceso de re categorizaci&oacute;n, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>Ubicar y dar clic en el nombre de la Categor&iacute;a Homog&eacute;nea a la que absorber&aacute; a las categor&iacute;as anteriores. Siempre cuidando que sea Categor&iacute;a a nivel 4, de lo contrario <i>GuiaNet</i>, arrojara un mensaje de error.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/selectcattorecat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al dar clic, el nombre de la categor&iacute;a, cambiar&aacute; de color, y aparecer&aacute; el buscador de las categor&iacute;as Heterog&eacute;neas.</li></ul>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/catselected.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Para re categorizar alguna Categor&iacute;a Heterog&eacute;nea a una Homog&eacute;nea, debe de realizar una b&uacute;squeda de la categor&iacute;a que desea re categorizar.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/selecthomcattorecat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez ubicada la Categor&iacute;a Heterog&eacute;nea, para completar el proceso de re categorizaci&oacute;n, basta con dar clic en el nombre de la Categor&iacute;a Heterog&eacute;nea.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/selecthomcat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ahora, la categor&iacute;a Heterog&eacute;nea no aparece en el &aacute;rbol de Categor&iacute;as Heterog&eacute;neas y ha sido absorbida por la Categor&iacute;a Homog&eacute;nea seleccionada.</li>";
        d += "<li>GuiaNet, ofrece al usuario, la posibilidad de revisar las categor&iacute;as que hayan sido re categorizadas a la Categor&iacute;a Homog&eacute;nea. Solamente dar Clic en la pesta&ntilde;a <i>''Categor&iacute;as con Asociaciones''</i>, mientras tenga alguna Categor&iacute;a Homog&eacute;nea seleccionada. La estructura de la pesta&ntilde;a se describe a continuaci&oacute;n:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/tabcatasociated.png'/ style='width:90%;heigth:90%'></p><br>";

        d += "<table style='width:100%'>";
        d += "<thead style='background-color:#065977'>";
        d += "<tr>";
        d += "<th class='paragraphcontent' style='width:10%'>#</th><th class='paragraphcontent' style='width:20%'>Nombre</th><th class='paragraphcontent' style='width:70%'>Descripci&oacute;n</th>";
        d += "</tr>";
        d += "</thead>";
        d += "<tbody>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>1</td><td class='paragraphcontent'>Posici&oacute;n actual en las Pesta&ntilde;as</td><td class='paragraphcontent'>Muestra la posici&oacute;n actual del usuario en las Pesta&ntilde;as de la secci&oacute;n, re categorizaci&oacute;n.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>2</td><td class='paragraphcontent'>R&oacute;tulo Categor&iacute;a</td><td class='paragraphcontent'>Muestra el nombre de la Categor&iacute;a seleccionada.</td>";
        d += "</tr>";
        d += "<tr>";
        d += "<td class='paragraphcontent'>3</td><td class='paragraphcontent'>Tabla de Categor&iacute;as</td><td class='paragraphcontent'>Contiene las categor&iacute;as Heterog&eacute;neas que se encuentran absorbidas por las categor&iacute;as Homog&eacute;neas.</td>";
        d += "</tr>";
        d += "</tbody>";
        d += "</table><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 9) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Eliminar Categor&iacute;a ya Re Categorizada.</p><br>";
        d += "<p class='paragraphcontent'>Para realizar un <i>''Rollback''</i>, o regresar a un estado anterior las categor&iacute;as, siga los siguientes pasos:</p><br>";
        d += "<ul class='paragraphcontent'><li>En la Pesta&ntilde;a <i>''Categor&iacute;as con Asociaciones''</i>, ubicar la Categor&iacute;a Heterog&eacute;nea que desea regresar a su estado original, es decir, antes de re categorizar. En la columna <i>''Eliminar''</i>, dar clic en el bot&oacute;n ( <img src='../Images/cancel.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/btndeletecarrecat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Al realizar esta operaci&oacute;n, la Categor&iacute;a Heterog&eacute;nea, ya no aparecer&aacute; en la Tabla de la pesta&ntilde;a <i>''Categor&iacute;as con Asociaciones''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/catdeleted.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>Ahora la Categor&iacute;a Heterog&eacute;nea, aparecer&aacute; nuevamente en el &aacute;rbol de Categor&iacute;as Heterog&eacute;neas de la pesta&ntilde;a <i>''Re categorizar''</i></li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/recatnew.png'/ style='width:90%;heigth:90%'></p><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

    if (Id == 10) {

        var d = "";

        d += "<br>";
        d += "<p class='paragraphtitle'>Agregar Categor&iacute;as nuevas.</p><br>";
        d += "<p class='paragraphcontent'><i>GuiaNet</i>, en la secci&oacute;n de Re Categorizaci&oacute;n ofrece al usuario la posibilidad de agregar nuevas categor&iacute;as a la estructura de Categor&iacute;as. El proceso de agregado se describe a continuaci&oacute;n:</p><br>";
        d += "<ul class='paragraphcontent'><li>Dependiendo a qu&eacute; nivel sea al que quiera agregar la nueva Categor&iacute;a, ser&aacute; en donde ubicara el bot&oacute;n ( <img src='../Images/expand.png' style='height:25px;width:25px;'> ).</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/catselected.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En este caso, para ejemplo se dar&aacute; clic en el Bot&oacute;n superior, para agregar Categor&iacute;as desde el primer nivel.</li>";
        d += "<li>Al dar clic en el bot&oacute;n ( <img src='../Images/expand.png' style='height:25px;width:25px;'> ) superior, aparecer&aacute; el siguiente formulario, donde el usuario los campos de <i>''Nombre de Categor&iacute;a Nivel 1''</i> hasta llegar a <i>''Nombre de Categor&iacute;a Nivel 4''</i>.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/frmnewcat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<p class='paragraphcontent'><i><b>NOTA:</b> Ning&uacute;n campo de Categor&iacute;as puede quedar vac&iacute;o. De lo contrario, GuiaNet, arrojar&aacute; un mensaje de error.</i></p><br>";
        d += "<ul class='paragraphcontent'><li>Una vez, llenos los campos de los niveles de las Categor&iacute;as, el paso siguiente, es dar clic en el bot&oacute;n ( <img src='../Images/save.png' style='height:25px;width:25px;'> ), si todo esta correcto, las categor&iacute;as se agregar&aacute;n y aparecer&aacute;n en el &aacute;rbol de categor&iacute;as.</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/insertedcats.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>En caso de que la categor&iacute;a ya exista, <i>GuiaNet</i> mostrar&aacute; el siguiente mensaje:</li></ul><br>";
        d += "<p style='text-align:center'><img src='../ManualImages/LI/RECAT/errorexitscat.png'/ style='width:90%;heigth:90%'></p><br>";
        d += "<ul class='paragraphcontent'><li>El mensaje de error mostrar&aacute; el nombre de la Categor&iacute;a que ya existe en la BD.</li></ul><br>";

        $("#divcontentusermanual").empty();
        $("#divcontentusermanual").append($.parseHTML(d));
    }

}


function searchcntLI(element, menu) {
    $.ajax({
        url: "../Help/searchLI/",
        type: "POST",
        dataType: "json",
        data: { term: element, file: menu },
        success: function (data) {
            $("#divcontentusermanual").empty();
            $.each(data, function (index, val) {
                $("#divcontentusermanual")
                .append("<br>")
                    .append(val.content);
            })
        }
    })
}