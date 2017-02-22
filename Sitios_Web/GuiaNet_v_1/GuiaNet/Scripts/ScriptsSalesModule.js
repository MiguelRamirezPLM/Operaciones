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
                if (data == false) {
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

    $("#productname").val('');
    $("#register").val('');
    $("#barcode").val('');

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

function open_popup_createproductH() {
    $('#insertproductsH').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $('.popup-overlay').height($(window).height());
    return false;
}

function close_popup_createproductH() {
    $('#insertproductsH').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();

    $("#productname").val('');
    $("#register").val('');
    $("#barcode").val('');

    return false;
}



/*              CAMBIOS             */


/*  ESPECIALIDADES  */
function insert_editionclientspecialities(value) {
    console.log(value);
    var tr = $(value).parents("tr:first");
    if (tr.find(".edtclsp").is(':checked')) {
        var spid = tr.find("#SpecialityId").val();
        var clid = $("#ClientId").val();
        var edition = $("#EditionId").val();
        var adv = $("#advers").val();
        var qtt = $("#quantity").val();
        var type = "ANUNCIANTE";
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/inseditionclientspecialitiesnewedition/",
            traditional: true,
            data: { Speciality: spid, Client: clid, Edition: edition, Advers: adv, Quantity: qtt, TypeName: type },
            success: function (data) {
                if (data == false) {
                    tr.find(".Participant").removeAttr("checked");
                }
            }
        });
    }
    else if (tr.find(".edtclsp").is(":not(:checked)")) {

        tr.find("#btndeletespeciality").trigger("click");
    }
    $("#bloqueo").hide();
}

function insert_editionclientspecialitiessuc(value) {
    console.log(value);
    var tr = $(value).parents("tr:first");
    if (tr.find(".edtclspsuc").is(':checked')) {
        var spid = tr.find("#SpecialityIdsuc").val();
        var clid = $("#branchid").val();
        var edition = $("#EditionId").val();
        var adv = $("#advers").val();
        var qtt = $("#quantity").val();
        var type = "SUCURSAL";
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/inseditionclientspecialitiesnewedition/",
            traditional: true,
            data: { Speciality: spid, Client: clid, Edition: edition, Advers: adv, Quantity: qtt, TypeName: type },
            success: function (data) {
                if (data == false) {
                    tr.find(".Participant").removeAttr("checked");
                }
            }
        });
    }
    else if (tr.find(".edtclspsuc").is(":not(:checked)")) {

        tr.find("#btndeletespecialitysuc").trigger("click");
    }
    $("#bloqueo").hide();
}

function insert_editionclientcategories(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".categorieshom").is(':checked')) {
        var catid = tr.find("#CategoryIdNodo3").val();
        var clid = $("#ClientId").val();
        var edition = $("#EditionId").val();

        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/inserteditionclientcategoriesnewedition/",
            traditional: true,
            data: { Category: catid, Client: clid, Edition: edition },
            success: function (data) {
                if (data == false) {
                    tr.find(".categorieshom").removeAttr("checked");
                }
            }
        });
    }
    else if (tr.find(".categorieshom").is(":not(:checked)")) {

        tr.find("#btndeletecategory").trigger("click");
    }
    $("#bloqueo").hide();
}

function insert_editionclientheterogeneouscategories(value) {
    var tr = $(value).parents("tr:first");
    if (tr.find(".categorieshet").is(':checked')) {
        var hetcatid = tr.find("#HeterogeneousCategoryIdNodo").val();

        console.log(!hetcatid.trim());

        if (!hetcatid.trim() == true) {

            hetcatid = tr.find("#HeterogeneousCategoryId").val();
        }

        var clid = $("#ClientId").val();
        var edition = $("#EditionId").val();
        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/inserteditionclientheterogeneouscategoriesnewedition/",
            traditional: true,
            data: { HeterogeneousCategory: hetcatid, Client: clid, Edition: edition },
            success: function (data) {
                if (data == false) {
                    tr.find(".Participant").removeAttr("checked");
                }
            }
        });
    }
    else if (tr.find(".categorieshet").is(":not(:checked)")) {

        tr.find("#btndeletecategoryhet").trigger("click");
    }
    $("#bloqueo").hide();
}

function edt_expdescript(value) {

    var tr = $(value).parents("tr:first");

    tr.find("#lblExpireDescription").hide();
    tr.find("#edtExpireDescription").show();
}

function close_edt_expdescript(value) {
    $("#bloqueo").show();
    var tr = $(value).parents("tr:first");

    var Id = tr.find("#BrandId").val();
    var ExpD = $(value).val();
    var CId = $("#ClientId").val();
    var EId = $("#EditionId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        url: "../SalesModule/editclientbrands/",
        data: { Brand: Id, Client: CId, Edition: EId, ExpireDescription: ExpD },
        success: function (data) {
            if (data == true) {

                tr.find("#lblExpireDescription").text(ExpD);

                tr.find("#lblExpireDescription").show();
                tr.find("#edtExpireDescription").hide();

                $("#bloqueo").hide();
            }
        }
    })

}


/*  MARCAS  */
function insert_clientbrandsne(value) {
    console.log(value);
    var tr = $(value).parents("tr:first");
    if (tr.find(".clbrands").is(':checked')) {
        var brndid = tr.find("#BrandId").val();
        var clid = $("#ClientId").val();
        var edition = $("#EditionId").val();

        $("#bloqueo").show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../SalesModule/insertclientbrandsnewedition/",
            traditional: true,
            data: { Brand: brndid, Client: clid, Edition: edition },
            success: function (data) {
                if (data == false) {
                }
            }
        });
    }
    else if (tr.find(".clbrands").is(":not(:checked)")) {

        tr.find("#btndeletebrand").trigger("click");
    }
    $("#bloqueo").hide();
}

/*              FIN             */



function open_popup_chat() {
    $('#chatform').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $('.popup-overlay').height($(window).height());
    return false;
}

function close_popup_chat() {
    $('#chatform').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();

    $("#textemail").val('');
    $("#register").val('');
    $("#barcode").val('');

    return false;
}

function send_email() {
    $("#bloqueo").show();
    $('#chatform').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();

    var textemail = $("#textemail").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if (!textemail.trim() == true) {
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>-> No puede env&iacute;ar un Requerimiento en Blanco</p>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();

        $('#chatform').fadeIn('slow');
        $('.popup-overlay').fadeIn('slow');
        $('.popup-overlay').height($(window).height());
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../Email/Index/",
            traditional: true,
            data: { Message: textemail },
            success: function (data) {
                if (data == true) {
                    $('#chatform').fadeOut('slow');
                    $('.popup-overlay').fadeOut('slow');
                    $("#bloqueo").hide();

                    $("#textemail").val('');
                }
                else {
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>-> El c&oacute;digo de error es :" + data + "</p>"
                    apprise("" + d + "", { 'animate': true });

                    $('#chatform').fadeIn('slow');
                    $('.popup-overlay').fadeIn('slow');
                    $('.popup-overlay').height($(window).height());
                    return false;
                }
            }
        });
    }
}

function checkctnomal(ClientId, EditionId) {
    $.ajax({
        type: "POST",
        dataType: "Json",
        url: "../SalesModule/insertclnormal/",
        data: { Client: ClientId, Edition: EditionId },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    })
}

function onchargepage() {
    var session = sessionStorage.TypeId;

    console.log(session);

    session = "a" + session;

    var item = document.getElementById(session);

    $("#selectitem").val($(item).val())
}



function getlevel4(value) {

    sessionStorage.HomogeneousCategoryId = value;

    sessionStorage.ListItems = 1;

    var exp = "Expand_" + value;

    var elmexp = document.getElementById(exp);

    var clp = "Collapse_" + value;

    var elmclp = document.getElementById(clp);

    $(elmexp).hide();
    $(elmclp).show();

    var lstid = "ListL2_" + value;

    var elmls = document.getElementById(lstid);

    //for (var i = 0; i <= 100; i++) {
    //    $(elmls).append($("<li></li>").append("Hola " + i + " veces"));
    //}

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../InformationLaboratory/getlevel4",
        data: { HomogeneousCategory: value },
        success: function (data) {
            //$(elmls).append($("<li></li>").append(val.LeafCategory));
            $(elmls).empty();
            $.each(data, function (index, val) {
                $(elmls).append($("<li></li>").append("<input type='checkbox' onclick='checkCategories($(this))' value='" + val.LeafCategoryId + "'/><span>&nbsp;&nbsp;" + val.LeafCategory + "</span>"));
            });
        }
    })
}


function getlevel4SM(value) {

    sessionStorage.HomogeneousCategoryIdSM = value;

    sessionStorage.ListItems = 1;

    var exp = "Expand_" + value;

    var elmexp = document.getElementById(exp);

    var clp = "Collapse_" + value;

    var elmclp = document.getElementById(clp);

    $(elmexp).hide();
    $(elmclp).show();

    var lstid = "ListL2_" + value;

    var elmls = document.getElementById(lstid);

    //for (var i = 0; i <= 100; i++) {
    //    $(elmls).append($("<li></li>").append("Hola " + i + " veces"));
    //}

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../SalesModule/getlevel4",
        data: { HomogeneousCategory: value },
        success: function (data) {
            //$(elmls).append($("<li></li>").append(val.LeafCategory));
            $(elmls).empty();
            $.each(data, function (index, val) {
                $(elmls).append($("<li></li>").append("<input type='checkbox' onclick='checkCategoriesSM($(this))' value='" + val.LeafCategoryId + "'/><span>&nbsp;&nbsp;" + val.LeafCategory + "</span>"));
            });
        }
    })
}

function collapselevel4(value) {

    sessionStorage.HomogeneousCategoryId = null;

    var exp = "Expand_" + value;

    var elmexp = document.getElementById(exp);

    var clp = "Collapse_" + value;

    var elmclp = document.getElementById(clp);

    $(elmclp).hide();
    $(elmexp).show();

    var lstid = "ListL2_" + value;

    var elmls = document.getElementById(lstid);

    $(elmls).empty();

}

//function checkCategories(item) {

//    var Id = $(item).val();
//    var CId = $("#clientid").val();
//    var PId = $("#ProductId").val();
//    var HCId = sessionStorage.HomogeneousCategoryId;

//    var d = "";
//    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

//    if ($(item).is(":checked")) {
//        $.ajax({
//            Type: "POST",
//            dataType: "Json",
//            url: "../InformationLaboratory/insertProductLeafCategories",
//            data: { Client: CId, Product: PId, Category: Id, HomogeneousCategory: HCId },
//            success: function (data) {
//                if (data == "_exist") {
//                    d += "<p class='labels'></p>";
//                    d += "<p class='labels'>- La Categor&iacute;a ya esta asociada al producto.</p>";
//                    apprise("" + d + "", { 'animate': true });
//                    $(item).prop("checked", false);
//                }
//                if (data == "_cpnotexist") {
//                    d += "<p class='labels'></p>";
//                    d += "<p class='labels'>- Ocurrio un Error en el Producto asociado al Cliente.</p>";
//                    apprise("" + d + "", { 'animate': true });
//                    $(item).prop("checked", false);
//                }
//            }
//        })

//    }
//    else if ($(item).is(":not(:checked)")) {

//        $.ajax({
//            Type: "POST",
//            dataType: "Json",
//            url: "../InformationLaboratory/deleteProductLeafCategories",
//            data: { Client: CId, Product: PId, Category: Id, HomogeneousCategory: HCId },
//            success: function (data) {

//            }
//        })
//    }
//}

function deleteProductLeafCategories(item) {
    $("#bloqueo").show();
    var tr = $(item).parents("tr:first");

    var CId = $("#clientid").val();
    var PId = $("#ProductId").val();
    var Id = tr.find("#LeafCategoryIdL4").val();
    var HCId = tr.find("#HomogeneousCategoryIdL3").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../InformationLaboratory/deleteProductLeafCategories",
        data: { Client: CId, Product: PId, Category: Id, HomogeneousCategory: HCId },
        success: function (data) {
            if (data == false) {
                d += "<p class='labels'></p>";
                d += "<p class='labels'>- Ocurrio un Error. Contactar al Administrador.</p>";
                apprise("" + d + "", { 'animate': true });

                $("#bloqueo").hide();
            }
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })

}

function searchCategories() {

    $("#bloqueo").show();

    var value = $("#TextSearch").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../InformationLaboratory/searchCategories",
        data: { CategoryName: value },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    })

}

function getproductstatus(item) {

    var tr = $(item).parents("tr:first");

    var value = $(item).val();

    var CId = $("#ClientId").val();

    var PId = $("#inputProductId");

    $(PId).val(value);

    var name = tr.find("#lblName").text();

    var PName = $("#lblProductName");

    $(PName).text(name);

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../InformationLaboratory/getproductstatus",
        data: { Product: value, Client: CId },
        success: function (data) {
            $("#ProductStatus").trigger("click");
            $("#results").empty();
            $.each(data, function (index, val) {
                $("#results")
                 .append($("<div></div>").append(val.StatusName))
            });

        }
    })
}

function insertproductstatus(Status) {
    var PId = $("#inputProductId").val();
    var CId = $("#ClientId").val();
    var STId = $(Status).val();

    if ($(Status).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../InformationLaboratory/insertproductstatus",
            data: { Product: PId, Client: CId, Status: STId, Operation: "Insert" },
            success: function (data) {
            }
        })
    }
    else if ($(Status).is(":not(:checked)")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../InformationLaboratory/insertproductstatus",
            data: { Product: PId, Client: CId, Status: STId, Operation: "Delete" },
            success: function (data) {
            }
        })
    }
}


var list = [];


function checkCategories(item) {

    var Id = $(item).val();
    var CId = $("#clientid").val();
    var PId = $("#ProductId").val();
    var HCId = sessionStorage.HomogeneousCategoryId;

    if ($(item).is(":checked")) {

        list.push({
            'Id': Id,
            'CId': CId,
            'PId': PId,
            'HCId': HCId
        });
    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(list, "Id", "HCId", "PId", "CId", Id, HCId, PId, CId);

        if (index == null) {
        } else if (index >= 0) {

            list.splice(index, 1);
        }
    }
}

function checkCategoriesSM(item) {

    var Id = $(item).val();
    var CId = $("#clientid").val();
    var PId = $("#ProductId").val();
    var HCId = sessionStorage.HomogeneousCategoryIdSM;

    if ($(item).is(":checked")) {

        list.push({
            'Id': Id,
            'CId': CId,
            'PId': PId,
            'HCId': HCId
        });

        console.log(list);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(list, "Id", "HCId", "PId", "CId", Id, HCId, PId, CId);

        if (index == null) {
        } else if (index >= 0) {

            list.splice(index, 1);
            console.log(list);
        }
    }
}

function functiontofindIndexByKeyValue(arraytosearch, Id, HCId, PId, CId, valuetosearch, valueHCId, valuePId, valueCId) {
    for (var i = 0; i < arraytosearch.length; i++) {

        if (arraytosearch[i][Id] == valuetosearch) {
            if (arraytosearch[i][HCId] == valueHCId) {
                if (arraytosearch[i][PId] == valuePId) {
                    if (arraytosearch[i][CId] == valueCId) {
                        return i;
                    }
                }
            }
        }
    }
    return null;
};

function saveCategories() {
    var jsonresponse = JSON.stringify(list);
    console.log(list);
    var size = $(list).size();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../InformationLaboratory/saveCategories",
        data: { ListItems: jsonresponse, ArraySize: size },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    })
}


function saveCategoriesSM() {
    $("#bloqueo").show();

    var jsonresponse = JSON.stringify(list);
    console.log(list);
    var size = $(list).size();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../SalesModule/saveCategories",
        data: { ListItems: jsonresponse, ArraySize: size },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    })
}

function deleteProductLeafCategoriesSM(item) {
    $("#bloqueo").show();
    var tr = $(item).parents("tr:first");

    var CId = $("#clientid").val();
    var PId = $("#ProductId").val();
    var Id = tr.find("#LeafCategoryIdL4").val();
    var HCId = tr.find("#HomogeneousCategoryIdL3").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../SalesModule/deleteProductLeafCategories",
        data: { Client: CId, Product: PId, Category: Id, HomogeneousCategory: HCId },
        success: function (data) {
            if (data == false) {
                d += "<p class='labels'></p>";
                d += "<p class='labels'>- Ocurrio un Error. Contactar al Administrador.</p>";
                apprise("" + d + "", { 'animate': true });

                $("#bloqueo").hide();
            }
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })

}

function searchCategoriesSM() {

    $("#bloqueo").show();

    var value = $("#TextSearch").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../SalesModule/searchCategories",
        data: { CategoryName: value },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    })

}

function getproductstatusSM(item) {

    var tr = $(item).parents("tr:first");

    var value = $(item).val();

    var CId = $("#ClientId").val();

    var PId = $("#inputProductId");

    $(PId).val(value);

    var name = tr.find("#lblName").text();

    var PName = $("#lblProductName");

    $(PName).text(name);

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../SalesModule/getproductstatus",
        data: { Product: value, Client: CId },
        success: function (data) {
            $("#ProductStatus").trigger("click");
            $("#results").empty();
            $.each(data, function (index, val) {
                $("#results")
                 .append($("<div></div>").append(val.StatusName))
            });

        }
    })
}

function checkmentionatedProducts(item) {

    var tr = $(item).parents("tr:first");

    var PId = tr.find("#lblProductid").val();
    var CId = $("#ClientId").val();
    var EId = $("#EditionId").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../SalesModule/insertmentionatedproducts",
            data: { Product: PId, Client: CId, Edition: EId },
            success: function (data) {
                if (data == "_existContent") {
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Operaci&oacute;n Invalida!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto tiene Contenido. Contactar al Administrador!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $(item).prop("checked", false);
                }
                if (data == true) {
                    tr.find(".Participant").prop("checked", false);

                    tr.find("#clasifbtn").show();

                }

            }
        })
    }
    else if ($(item).is(":not(:checked)")) {

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../SalesModule/deletementionatedproducts",
            data: { Product: PId, Client: CId, Edition: EId },
            success: function (data) {
                //if (data == "_existContent") {
                //    d += "<p class='labels' style='text-align:center'>ERROR!!</p>";
                //    d += "<p class='labels'>- El Producto tiene contenido asociado.</p>";
                //    d += "<p class='labels' style='text-align:center'>Contactar al Administrador.</p>";
                //    apprise("" + d + "", { 'animate': true });
                //    $(item).prop("checked", false);
                //}
                if (data == true) {
                    $(item).prop("checked", false);
                    tr.find("#clasifbtn").hide();

                }

            }
        })
    }

}