function insert_participant(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".Participant").is(':checked')) {
        tr.find('.Mentionated').removeAttr('checked')
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();

       
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/insertparticipantproducts/",
            traditional: true,
            data: { productid: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == false) {
                    tr.find(".Participant").removeAttr("checked");
                }
            }
        });
    }
    else if (tr.find(".Participant").is(":not(:checked)")) {
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();

        var d = "";
        d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/deleteparticipantproducts/",
            traditional: true,
            data: { ProductId: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == true) {
                    setTimeout('document.location.reload()');
                }
                if (data == "existcontent")
                {
                    tr.find('.Participant').prop('checked', true);

                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bullet; El producto tiene Contenido. Contactar al administrador</p>"
                    apprise("" + d + "", { 'animate': true });
                }
            }
        });
    }
    $("#bloqueo").hide();
}

function insert_mentionated(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".Mentionated").is(':checked')) {
        tr.find('.Participant').removeAttr('checked')
        tr.find(".sidef").removeAttr("checked");
        tr.find(".changes").removeAttr("checked");
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();

        var d = "";
        d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/insertmentionatedproducts/",
            traditional: true,
            data: { productid: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == false) {
                    tr.find(".Mentionated").removeAttr("checked");
                }
                if (data == "existcontent") {

                    tr.find(".Mentionated").removeAttr("checked");
                    tr.find('.Participant').prop('checked', true);


                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bullet; El producto tiene Contenido. Contactar al administrador</p>"
                    apprise("" + d + "", { 'animate': true });
                }
            }
        });
    }
    else if (tr.find(".Mentionated").is(":not(:checked)")) {
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/deletementionatedproducts/",
            traditional: true,
            data: { ProductId: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == true) {
                    setTimeout('document.location.reload()');
                }
            }
        });
    }
    $("#bloqueo").hide();
}

function createproduct(value) {
    $("#bloqueo").show();
    $('#insertproducts').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    var name = $("#insertnameSM").val();
    var desc = $("#insertdescriptionSM").val();
    var pf = $("#insertPharmaFSM").val();
    var cat = $("#insertcatSM").val();
    var div = $("#DivisionId").val();
    var edition = $("#EditionId").val();
    var register = $("#insertregisterSM").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
    var PartProd = false;
    if ($("#PartProd").is(':checked')) {
        PartProd = true;
    }
    var MentProd = false;
    if ($("#MentProd").is(':checked')) {
        MentProd = true;
    }
    if ((!name.trim() == false) && (pf != "0") && (cat != "0")) {
        $.ajax({
            type: "POST",
            dataType: "Json",
            url: "../SalesModule/insertproduct/",
            traditional: true,
            data: { Product: name, description: desc, division: div, editionid: edition, pharmaform: pf, category: cat, Mentionated: MentProd, Participant: PartProd, RegisterSanitary: register },
            success: function (data) {
                if (data == true) {
                    setTimeout('document.location.reload()');
                }
                if (data == "existregister") {
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>-> El Registro Sanitario ya Existe para un Producto</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                    $('#insertproducts').fadeIn('slow');
                    $('.popup-overlay').fadeIn('slow');
                    $('.popup-overlay').height($(window).height());
                    return false;
                }
                if(data == false) {
                    d += "<p class='labels'></p>";
                    d += "<p class='labels'>- El Producto ya existe</p>";
                    apprise("" + d + "", { 'animate': true });
                    $('#insertproducts').fadeIn('slow');
                    $('.popup-overlay').fadeIn('slow');
                    $('.popup-overlay').height($(window).height());
                    return false;
                }
            }
        });
    }
    else {
        if (!name.trim() == true) {
            d += "<p class='labels'>- Falta Nombre de Producto</p>";
        }
        if (cat == "0") {
            d += "<p class='labels'>- Falta seleccionar Categor&iacute;a</p>";
        }
        if (pf == "0") {
            d += "<p class='labels'>- Falta seleccionar Forma Farmac&eacute;utica</p>";
        }
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
        $('#insertproducts').fadeIn('slow');
        $('.popup-overlay').fadeIn('slow');
        $('.popup-overlay').height($(window).height());
        return false;
    }
}

function remove_mentionated() {
    if ($("#PartProd").is(':checked')) {
        $('#MentProd').removeAttr('checked');
    }

    if ($("#MentProd").is(':checked')) {
        $('#PartProd').removeAttr('checked');
    }
}

function remove_participant() {
    if ($("#MentProd").is(':checked')) {
        $('#PartProd').removeAttr('checked');
    }
}

function open_popup_createproduct() {
    $('#insertproducts').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $('.popup-overlay').height($(window).height());
    return false;
}

function close_popup_createproduct() {
    $('#insertproducts').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();

    $("#insertnameSM").val('');
    $("#insertdescriptionSM").val('');
    $("#insertregisterSM").val('');
    $("#insertPharmaFSM").val(document.getElementById("0"))
    $("#insertcatSM").val(document.getElementById("0"))

    $("#PartProd").removeAttr("checked");
    $("#MentProd").removeAttr("checked");

    return false;
}

function insertreferencesidef(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".sidef").is(':checked')) {
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();
        var d = "";
        d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/insertreferencesidef/",
            traditional: true,
            data: { productid: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == false) {
                    tr.find(".sidef").removeAttr("checked");
                    d += "<p class='labels'></p>";
                    d += "<p class='labels'><li class='labels'> El Producto no existe como Participante</li></p>";
                    apprise("" + d + "", { 'animate': true });
                }
            }
        });
    }
    else if (tr.find(".sidef").is(":not(:checked)")) {
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/deletereferencesidef/",
            traditional: true,
            data: { ProductId: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == true) {
                    tr.find(".sidef").removeAttr("checked");
                }
            }
        });
    }
    $("#bloqueo").hide();
}

function insertproductchanges(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".changes").is(':checked')) {
        //tr.find('.Mentionated').removeAttr('checked')
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();
        var d = "";
        d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/insertproductchanges/",
            traditional: true,
            data: { productid: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == false) {
                    tr.find(".changes").removeAttr("checked");
                    //d += "<p class='labels'></p>";
                    //d += "<p class='labels'><li class='labels'> Error al Realizar la Marcación.</li></p>";
                    //apprise("" + d + "", { 'animate': true });
                }
            }
        });
    }
    else if (tr.find(".changes").is(":not(:checked)")) {
        var prodId = tr.find("#lblProductid").val();
        var div = $("#DivisionId").val();
        var edition = $("#EditionId").val();
        var pf = tr.find("#PharmaFormIdSM").val();
        var cat = tr.find("#CategoryIdSM").val();
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/deleteproductchanges/",
            traditional: true,
            data: { ProductId: prodId, division: div, editionid: edition, pharmaform: pf, category: cat },
            success: function (data) {
                if (data == true) {
                    tr.find(".changes").removeAttr("checked");
                }
            }
        });
    }
    $("#bloqueo").hide();
}

function open_popup_createdivision() {
    $('#insertaddressSMD').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $('.popup-overlay').height($(window).height());
    return false;
}

function close_popup_createdivision() {
    $('#insertaddressSMD').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();


    $("#insertstreetSM").val('');
    $("#insertsuburbSM").val('');
    $("#insertcpSM").val('');
    $("#insertcitySM").val('');
    $("#insertstateSM").val('');
    $("#insertladaSM").val('');
    $("#inserttelephoneSM").val('');
    $("#insertemailSM").val('');
    $("#insertwebSM").val('');
    $("#insertfaxSM").val('');
    return false;
}

function createdivisioninformation(value) {
    $("#bloqueo").show();
    $('#insertaddressSMD').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    var street = $("#insertstreetSM").val();
    var suburb = $("#insertsuburbSM").val();
    var cp = $("#insertcpSM").val();
    var city = $("#insertcitySM").val();
    var state = $("#insertstateSM").val();
    var lada = $("#insertladaSM").val();
    var tel = $("#inserttelephoneSM").val();
    var mail = $("#insertemailSM").val();
    var web = $("#insertwebSM").val();
    var fax = $("#insertfaxSM").val();
    var div = $("#DivisionId").val();
    var edition = $("#EditionId").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
    if ((!street.trim() == false) || (!suburb.trim() == false) || (!cp.trim() == false) || (!city.trim() == false) || (!state.trim() == false) || (!lada.trim() == false) || (!tel.trim() == false) || (!mail.trim() == false) || (!web.trim() == false) || (!fax.trim() == false)) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/createaddress/",
            traditional: true,
            data: { division: div, editionid: edition, Street: street, Suburb: suburb, CP: cp, City: city, State: state, Lada: lada, Telephone: tel, Email: mail, Web: web, Fax: fax },
            success: function (data) {
                if (data == true) {
                    setTimeout('document.location.reload()');
                }
                else {
                    d += "<p class='labels'></p>";
                    d += "<p class='labels'>- Ocurrio un Error. Vuelva a intentarlo</p>";
                    apprise("" + d + "", { 'animate': true });
                    $('#insertaddressSMD').fadeIn('slow');
                    $('.popup-overlay').fadeIn('slow');
                    $('.popup-overlay').height($(window).height());
                    return false;
                }
            }
        });
    }
    else {
        d += "<p class='labels'></p>";
        d += "<p class='labels'>- No pueden quedar todos los campos Vac&iacute;os</p>";
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
        $('#insertaddressSMD').fadeIn('slow');
        $('.popup-overlay').fadeIn('slow');
        $('.popup-overlay').height($(window).height());
        return false;
    }
}

function load(btn) {
    $("#bloqueo").show();
}
