function show_loader() {
    $("#bloqueo").show();
}

function getpillbookinformation(pillbook) {
    document.getElementById("PillBookId").value = pillbook;
    if (pillbook != "0") {
        $('#InsertParam').trigger('click');
        $("#bloqueo").show();
    }
}

function create_pillbook() {
    var pbname = $("#PBName").val();
    var pbcode = $("#PBCode").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if ((!pbname.trim() == true) || (!pbcode.trim() == true)) {

        var message = "Ningun Campo puede quedar vac&iacute;o";
        d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
        apprise("" + d + "", { 'animate': true });
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../PillBook/createpillbook/",
            data: { PBName: pbname, PBCode: pbcode },
            success: function (data) {
                if (data == true) {
                    setTimeout('document.location.reload()');
                }
                if (data == "errorcode") {
                    var message = "El c&oacute;digo de PillBook ya existe";

                    d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                    apprise("" + d + "", { 'animate': true });
                }
                if (data == "errorpillbook") {
                    var message = "El Nombre de PillBook ya existe";
                    d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                    apprise("" + d + "", { 'animate': true });
                }
            }
        })
    }
}

function open_popup_createPB() {
    $('#insertproducts').fadeIn('slow');
    $('.popup-overlay').fadeIn('slow');
    $("#bloqueo").show();

    return false;
}

function close_popup_createPB() {
    $('#insertproducts').fadeOut('slow');
    $('.popup-overlay').fadeOut('slow');
    $("#bloqueo").hide();

    $("#PBName").val('');
    $("#PBCode").val('');

    return false;
}


/*      EDITAR PILLBOOK     */
function function_edit() {
    var pbname = $("#editpb").val();
    sessionStorage.pbname = pbname;

    $("#lblpb").hide();
    $("#editpb").show();
    $("#btneditpb").hide();

    $("#btnsavepb").show();
    $("#btncancelpb").show();


}

function function_canceledit() {
    $("#lblpb").show();
    $("#editpb").hide();
    $("#btneditpb").show();

    $("#btnsavepb").hide();
    $("#btncancelpb").hide();

    var pbname = sessionStorage.pbname;

    $("#editpb").val(pbname);
}

function editpillbook() {
    $("#bloqueo").show();
    var pbname = $("#editpb").val();
    var pbid = $("#PBId").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if (!pbname.trim() == true) {
        var message = "El Campo no puede quedar vac&iacute;o";
        d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
        apprise("" + d + "", { 'animate': true });
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../PillBook/editpillbook/",
            data: { PBName: pbname, PBId: pbid },
            success: function (data) {
                if (data == true) {
                    $("#lblpb").text(pbname);
                    $("#editpb").val(pbname);

                    $("#lblpb").show();
                    $("#editpb").hide();
                    $("#btneditpb").show();

                    $("#btnsavepb").hide();
                    $("#btncancelpb").hide();

                    $("#bloqueo").hide();
                }
                else if (data == "errorpillbook") {
                    var message = "El Nombre de PillBook ya existe";
                    d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
}

/*         FIN         */


/*      POPUP       */
function open_popupaddproduct() {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getproducts/",
        data: { PillBook: PBId },
        success: function (data) {
            $('#tablecontent').empty();
            $('#tablecontent')
                .append($("<tr></tr>")
                    .append($("<td style='text-align:center;width:10%' class='labels'></td>")
                    .text(""))
                     .append($("<td style='text-align:center;width:26.6%' class='labels'><h2 style='color: #065977'>Marca</h2></td>"))
                     .append($("<td style='text-align:center;width:26.6%' class='labels'><h2 style='color: #065977'>Forma Farmac&eacute;utica</h2></td>"))
                     .append($("<td style='text-align:center;width:26.6%' class='labels'><h2 style='color: #065977'>Sustancias Activas</h2></td>")
                ));
            $.each(data, function (index, val) {
                $('#tablecontent')
                .append($("<tr></tr>")
                    .append($("<td style='text-align:center;width:10%; font-size:14px' class='labels'></td>").append("<input type='checkbox' onclick='addactivesubstance($(this))' id='ProductId' value=" + val.ProductId + " />"))
                            .append($("<td style='width:26.6%'></td>").append(val.Brand))
                            .append($("<td style='width:26.6%'></td>").append(val.PharmaForm))
                            .append($("<td style='width:26.6%'></td>").append(val.Substances))
                            .append($("<input type=text onclick='addactivesubstance($(this))' id='PharmaFormId' style='display:none' value=" + val.PharmaFormId + " />")));
            })
            $("#bloqueo").hide();
            $('#addsubstances').show();
            $("#openpopupaddsubstance").hide();
            $("#closepopupaddsubstance").show();
        }
    })
}

function close_popupaddsubstance() {
    $("#bloqueo").show();
    setTimeout('document.location.reload()');
}

function addactivesubstance(ASId) {

    var tr = $(ASId).parents("tr:first");
    var pfid = tr.find("#PharmaFormId").val();
    var pdid = tr.find("#ProductId").val();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addproduct/",
        data: { PillBook: PBId, PharmaForm: pfid, Product: pdid },
        success: function (data) {

        }
    })
}

/*         FIN         */


/*      POPUP       */
function open_popupaddtherapeutics() {
    $("#addtherapeutics").show();
    $("#closepopupaddtherapeutics").show();
    $("#openpopupaddtherapeutics").hide();
}

function close_popupaddtherapeutics() {
    $("#bloqueo").show();
    var v = false;
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/activetherapeuticstree/",
        data: { flag: v },
        success: function (data) {
            $("#bloqueo").show();
            setTimeout('document.location.reload()');
        }
    })

}

/*         FIN         */


/*         ADD PILLBOOKATC         */

function addpillbookatc(TherapeuticId) {
    var pb = $("#PBId").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addpillbookatc/",
        data: { PBId: pb, Therapeutic: TherapeuticId },
        success: function (data) {
            if (data == false) {
                var element = document.getElementById(TherapeuticId);
                $(element).removeAttr("checked");

                var message = "El Uso Terapeutico ya existe asociado al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
            }
        }
    })
}

/*         FIN         */

/*          TARGETS         */
function activesubstances(Aas) {

    var AasId = $(Aas).attr("Id");

    sessionStorage.divId = AasId;

    $("#cie").removeClass("labelin");
    $("#cie").addClass("labelout");

    $(Aas).removeClass("labelout");

    $("#sa").removeClass("labelout");
    $("#sa").addClass("labelin");

    $("#saPLM").removeClass("labelin");
    $("#saPLM").addClass("labelout");

    $("#atc").removeClass("labelin");
    $("#atc").addClass("labelout");

    $("#atcoms").removeClass("labelin");
    $("#atcoms").addClass("labelout");

    $("#brans").removeClass("labelin");
    $("#brans").addClass("labelout");

    $("#divacts").show();
    $("#divatcoms").hide();
    $("#divactsPLM").hide();
    $("#divatc").hide();
    $("#divcie").hide();
    $("#divbrans").hide();
}

function activesubstancesPLM(AasPLM) {

    var AasId = $(AasPLM).attr("Id");

    sessionStorage.divId = AasId;

    $("#cie").removeClass("labelin");
    $("#cie").addClass("labelout");

    $(AasPLM).removeClass("labelout");

    $("#sa").removeClass("labelin");
    $("#sa").addClass("labelout");

    $("#saPLM").removeClass("labelout");
    $("#saPLM").addClass("labelin");

    $("#atc").removeClass("labelin");
    $("#atc").addClass("labelout");

    $("#atcoms").removeClass("labelin");
    $("#atcoms").addClass("labelout");

    $("#brans").removeClass("labelin");
    $("#brans").addClass("labelout");


    $("#divactsPLM").show();
    $("#divacts").hide();
    $("#divatc").hide();
    $("#divatcoms").hide();
    $("#divcie").hide();
    $("#divbrans").hide();

}

function atc(Aatc) {

    var AatcId = $(Aatc).attr("Id");

    sessionStorage.divId = AatcId;

    $("#sa").removeClass("labelin");
    $("#sa").addClass("labelout");

    $("#saPLM").removeClass("labelin");
    $("#saPLM").addClass("labelout");

    $("#cie").removeClass("labelin");
    $("#cie").addClass("labelout");

    $(Aatc).removeClass("labelout");
    $(Aatc).addClass("labelin");

    $("#brans").removeClass("labelin");
    $("#brans").addClass("labelout");

    $("#atcoms").removeClass("labelin");
    $("#atcoms").addClass("labelout");

    $("#divatc").show();
    $("#divatcoms").hide();
    $("#divactsPLM").hide();
    $("#divacts").hide();
    $("#divcie").hide();
    $("#divbrans").hide();
}

function atcoms(Aatcoms) {

    var AatcId = $(Aatcoms).attr("Id");

    sessionStorage.divId = AatcId;

    $("#sa").removeClass("labelin");
    $("#sa").addClass("labelout");

    $("#saPLM").removeClass("labelin");
    $("#saPLM").addClass("labelout");

    $("#cie").removeClass("labelin");
    $("#cie").addClass("labelout");

    $(Aatcoms).removeClass("labelout");
    $(Aatcoms).addClass("labelin");

    $("#atc").removeClass("labelin");
    $("#atc").addClass("labelout");

    $("#brans").removeClass("labelin");
    $("#brans").addClass("labelout");

    $("#divatcoms").show();
    $("#divatc").hide();
    $("#divactsPLM").hide();
    $("#divacts").hide();
    $("#divcie").hide();
    $("#divbrans").hide();
}

function brands(Bbrand) {
    var BbrandId = $(Bbrand).attr("Id");

    sessionStorage.divId = BbrandId;

    $("#sa").removeClass("labelin");
    $("#sa").addClass("labelout");

    $("#saPLM").removeClass("labelin");
    $("#saPLM").addClass("labelout");

    $("#cie").removeClass("labelin");
    $("#cie").addClass("labelout");

    $("#atc").removeClass("labelin");
    $("#atc").addClass("labelout");

    $("#atcoms").removeClass("labelin");
    $("#atcoms").addClass("labelout");

    $(Bbrand).removeClass("labelout");
    $(Bbrand).addClass("labelin");

    $("#divbrans").show();
    $("#divatcoms").hide();
    $("#divactsPLM").hide();
    $("#divatc").hide();
    $("#divacts").hide();
    $("#divcie").hide();
}

function cie(Ccie) {
    var CcieId = $(Ccie).attr("Id");

    sessionStorage.divId = CcieId;

    $("#brans").removeClass("labelin");
    $("#brans").addClass("labelout");

    $("#sa").removeClass("labelin");
    $("#sa").addClass("labelout");

    $("#saPLM").removeClass("labelin");
    $("#saPLM").addClass("labelout");

    $("#atc").removeClass("labelin");
    $("#atc").addClass("labelout");

    $("#atcoms").removeClass("labelin");
    $("#atcoms").addClass("labelout");

    $(Ccie).removeClass("labelout");
    $(Ccie).addClass("labelin");

    $("#divcie").show();
    $("#divatcoms").hide();
    $("#divactsPLM").hide();
    $("#divatc").hide();
    $("#divacts").hide();
    $("#divbrans").hide();
}

function onloadpage() {
    var sessionDivId = sessionStorage.divId;

    sessiontarget = sessionStorage.targets;

    if (sessiontarget == "attributes") {
        var attributes = document.getElementById(sessiontarget);

        $(attributes).trigger("click");
    }

    if (sessiontarget == "pbds") {
        var pbds = document.getElementById(sessiontarget);

        $(pbds).trigger("click");
    }

    if (sessionDivId == undefined) {
        $("#sa").removeClass("labelout");
        $("#sa").addClass("labelin");
    }

    if (sessionDivId == null) {
        $("#sa").removeClass("labelout");
        $("#sa").addClass("labelin");
    }

    if (sessionDivId == "sa") {
        var sa = document.getElementById(sessionDivId);

        $(sa).trigger("click");
    }

    if (sessionDivId == "saPLM") {
        var saPLM = document.getElementById(sessionDivId);

        $(saPLM).trigger("click");
    }

    if (sessionDivId == "atc") {
        var atc = document.getElementById(sessionDivId);

        $(atc).trigger("click");
    }

    if (sessionDivId == "atcoms") {
        var atcoms = document.getElementById(sessionDivId);

        $(atcoms).trigger("click");
    }

    if (sessionDivId == "brans") {

        var brans = document.getElementById(sessionDivId);

        $(brans).trigger("click");
    }

    if (sessionDivId == "cie") {
        var cie = document.getElementById(sessionDivId);

        $(cie).trigger("click");
    }
}

/*          FIN         */

function getCIElevel1(level1) {

    $("#bloqueo").show();

    var Id = $(level1).attr("Id");

    $(level1).hide();

    var imgid = "#collapselist_" + Id;

    $(imgid).show();

    var ICD = "#ICD_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getCIElevel2/",
        data: { ICDId: Id },
        success: function (data) {
            $(ICD).empty();
            $(ICD).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(ICD)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel1(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselist_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#ICD_" + level;

    $(ICD).empty();
}

function getCIElevel2(level2) {

    $("#bloqueo").show();

    var Id = $(level2).attr("Id");

    $(level2).hide();

    var imgid = "#collapselist_" + Id;

    $(imgid).show();

    var ICD = "#ICDl2_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getCIElevel3/",
        data: { ICDId: Id },
        success: function (data) {
            $(ICD).empty();
            $(ICD).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(ICD)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel2(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselist_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#ICDl2_" + level;

    $(ICD).empty();
}

function addpillbookcie(cie) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addpillbookcie/",
        data: { PillBook: PBId, ICD: cie },
        success: function (data) {
            if (data == true) {
                $("#bloqueo").hide();
            }
            if (data == false) {
                var element = document.getElementById(cie);
                $(element).removeAttr("checked");

                var message = "El Registro ya existe asociado al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });

                $("#bloqueo").hide();
            }
        }
    })
}

function open_popupaddcie() {
    $("#closepopupaddcie").show();
    $("#openpopupaddcie").hide();
    $("#addcie").show();
}

function close_popupaddcie() {
    $("#bloqueo").show();
    setTimeout('document.location.reload()');
}

function open_getsubstances() {
    $("#addinnasubstances").show();
    $("#openpopupaddinnsubstance").hide();
    $("#closepopupaddinnsubstance").show();
}

function close_getsubstances() {
    $("#addinnasubstances").hide();
    $("#openpopupaddinnsubstance").show();
    $("#closepopupaddinnsubstance").hide();
}

function autocompleteinnasubstances(string) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getinnasubstances/",
        data: { term: string },
        success: function (data) {
            $('#tablecontentinnasubstances').empty();
            $('#tablecontentinnasubstances')
                .append($("<tr></tr>")
                    .append($("<td style='text-align:center;width:10%' class='labels'></td>")
                    .text(""))
                     .append($("<td style='text-align:center;' class='labels'></td>")
                .text("")));
            $.each(data, function (index, val) {
                $('#tablecontentinnasubstances')
                .append($("<tr></tr>")
                    .append($("<td style='text-align:center; font-size:14px' class='labels'></td>").append("<input type='checkbox' onclick='addinnaactivesubstances($(this).val())' id= " + val.INNActiveSubstanceId + " value=" + val.INNActiveSubstanceId + " />"))
                            .append($("<td></td>").append(val.Description)));
            })
            $("#bloqueo").hide();
            $('#addsubstances').show();
            $("#openpopupaddsubstance").hide();
            $("#closepopupaddsubstance").show();
        }
    })
}

function addinnaactivesubstances(INNActiveSubstanceId) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addpillbookinnasubstances/",
        data: { PillBook: PBId, INNAId: INNActiveSubstanceId },
        success: function (data) {
            if (data == true) {
                $("#bloqueo").hide();
            }
            if (data == false) {
                var element = document.getElementById(INNActiveSubstanceId);
                $(element).removeAttr("checked");

                var message = "El Registro ya existe asociado al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })

}

function gettargetname(target) {

    var Id = $(target).attr("Id");

    sessionStorage.targets = Id;
}

function insertpillbookcontent(inpag, txtinpag) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/inserteditionpillbookcontent/",
        data: { Attribute: inpag, Content: txtinpag, PillBook: PBId },
        success: function (data) {
            //if(data == true)
            //{
            $("#bloqueo").hide();
            //}
        }
    })
}

function getcontent(inpag, txtinpag) {

    $("#bloqueo").show();

    var txtId = $(txtinpag).attr("Id");
    var attrid = $(inpag).val();
    var PBId = $("#PBId").val();

    txtId = "#" + txtId;

    $("#txtinpag").val('');

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getcontent/",
        data: { Attribute: attrid, PillBook: PBId },
        success: function (data) {
            $(txtId).val(data[0].Content);
            $("#bloqueo").hide();
        }
    })
}

function check_true(check, check1, inpag) {
    var element = $(check).attr("Id");
    var value = $(check).val();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/insertpillbookicon/",
        data: { Attribute: inpag, PillBookIcon: value, PillBook: PBId },
        success: function (data) {
            if (data = true) {
                $(check1).removeAttr("checked");
            }
        }
    })
}

function check_false(check, check1, inpag) {
    var element = $(check).attr("Id");
    var value = $(check).val();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/insertpillbookicon/",
        data: { Attribute: inpag, PillBookIcon: value, PillBook: PBId },
        success: function (data) {
            if (data = true) {
                $(check1).removeAttr("checked");
            }
        }
    })
}


function getcatalog(input1, input2) {

    var Id = $(input1).attr("Id");

    if (Id == "PillBooks") {
        $("#selectPB").show();
        $("#searchpb").show();
        $("#selectEC").hide();
        $("#searchenc").hide();

        var fdd = document.getElementById("visds");

        $(fdd).addClass("scrollbar1");

        var fdd = document.getElementById("visd");

        $(fdd).removeClass("scrollbar1");

        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../PillBook/getpillbooks/",
            success: function (data) {
                $("#selectPB").empty();
                $.each(data, function (index, val) {
                    $("#selectPB")
                    .append($("<tr></tr>")
                    .append($("<td style='text-align:center; font-size:14px;width:10%' class='labels'></td>").append("<input type='checkbox' onclick='asociatepillbook($(this))' id='PillBookId' value=" + val.PillBookId + " />"))
                            .append($("<td style='width:90%'></td>").append(val.PillBookName)));
                })
                $("#bloqueo").hide();
            }
        })
    }

    if (Id == "Encyclopedias") {
        $("#selectEC").show();
        $("#searchenc").show();
        $("#selectPB").hide();
        $("#searchpb").hide();

        var fdd = document.getElementById("visd");

        $(fdd).addClass("scrollbar1");

        var fdd = document.getElementById("visds");

        $(fdd).removeClass("scrollbar1");

        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../PillBook/getencyclopedias/",
            success: function (data) {
                $("#selectEC").empty();
                $.each(data, function (index, val) {
                    $("#selectEC")
                    .append($("<tr></tr>")
                    .append($("<td style='text-align:center; font-size:14px;width:10%' class='labels'></td>").append("<input type='checkbox' onclick='asociateencyclopedia($(this))' id='EncyclopediaId' value=" + val.EncyclopediaId + " />"))
                            .append($("<td style='width:90%'></td>").append(val.EncyclopediaName)));
                })
                $("#bloqueo").hide();
            }
        })
    }

    $(input2).removeAttr("checked");
}

function popup_insertpillBookassociated(inpag) {

    $('#selectoptions').fadeIn('slow');

    $("#attributeid").val(inpag);
}

function asociatepillbook(pillbook) {
    var Id = $(pillbook).val();
    var PBId = $("#PBId").val();
    var attr = $("#attributeid").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/asociatepillbook/",
        data: { Attribute: attr, PillBook: PBId, PillBookAsociate: Id },
        success: function (data) {
            if (data == false) {
                //$(pillbook).removeAttr("checked");

                //var message = "El PillBook ya existe asociado al PillBook seleccionado";
                //d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                //d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                //apprise("" + d + "", { 'animate': true });

                sessionStorage.PillBookName = data;

                $("#bloqueo").hide();
            }
            if (data == "errorpbc") {
                var message = "Debe ingresar contenido al PillBook antes de continuar...";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
            else {
                sessionStorage.PillBookName = data;
            }
        }
    })
}

function asociateencyclopedia(encyclopedy) {
    var Id = $(encyclopedy).val();
    var PBId = $("#PBId").val();
    var attr = $("#attributeid").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/asociateencyclopedia/",
        data: { Attribute: attr, PillBook: PBId, EncyclopediAsociate: Id },
        success: function (data) {
            if (data == false) {
                $(encyclopedy).removeAttr("checked");

                var message = "La enciclopedia ya existe asociada al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
            if (data == "errorpbc") {
                var message = "Debe ingresar contenido al PillBook antes de continuar...";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
            else {
                sessionStorage.PillBookName = data;
            }
        }
    })
}

function closeoptions() {
    $('#selectoptions').fadeOut('slow');

    var visds = document.getElementById("visds");
    $(visds).removeClass("scrollbar1");
    $("#selectPB").empty();
    $("#PillBooks").removeAttr("checked");
    $("#searchenc").hide();
    $("#searchenc").val('');

    var visd = document.getElementById("visd");
    $(visd).removeClass("scrollbar1");
    $("#selectEC").empty();
    $("#Encyclopedias").removeAttr("checked");
    $("#searchpb").hide();
    $("#searchpb").val('');

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/cleanlist/",
        success: function (data) {

        }
    })
}

function searchpbcat(value) {
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/seachpillbook/",
        data: { PillBookName: value },
        success: function (data) {
            $("#selectPB").empty();
            $.each(data, function (index, val) {
                $("#selectPB")
                .append($("<tr></tr>")
                .append($("<td style='text-align:center; font-size:14px;width:10%' class='labels'></td>").append("<input type='checkbox' onclick='asociatepillbook($(this))' id='PillBookId' value=" + val.PillBookId + " />"))
                        .append($("<td style='width:90%'></td>").append(val.PillBookName)));
            })
            $("#bloqueo").hide();
        }
    })
}

function searchenccat(value) {
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/seachencyclopedia/",
        data: { Encyclopedia: value },
        success: function (data) {
            $("#selectEC").empty();
            $.each(data, function (index, val) {
                $("#selectEC")
                .append($("<tr></tr>")
                .append($("<td style='text-align:center; font-size:14px;width:10%' class='labels'></td>").append("<input type='checkbox' onclick='asociateencyclopedia($(this))' id='EncyclopediaId' value=" + val.EncyclopediaId + " />"))
                        .append($("<td style='width:90%'></td>").append(val.EncyclopediaName)));
            })
            $("#bloqueo").hide();
        }
    })
}


/*      DELETE REGISTERS CATALOGS*/

function deleteinnactivesubstances(INNActiveSubstance) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletepillbookinnasubstances/",
        data: { PillBook: PBId, INNAId: INNActiveSubstance },
        success: function (data) {
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })
}

function deletepillbookatc(Thera) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletepillbookatc/",
        data: { PillBook: PBId, Therapeutic: Thera },
        success: function (data) {
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })
}

function deletepillbookproducts(Prod, value2) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    var PF = $(value2)[0];

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletepillbookproducts/",
        data: { PillBook: PBId, Product: Prod, PharmaForm: PF },
        success: function (data) {
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })
}

function deletepillbookcie(CIE) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletepillbookcie/",
        data: { PillBook: PBId, ICD: CIE },
        success: function (data) {
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })
}

/*              FIN             */


function getcheckoptions(attr, checkTrue, checkFalse) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();

    var checkTrueId = $(checkTrue).attr("Id");
    var checkFalseId = $(checkFalse).attr("Id");

    checkTrueId = "#" + checkTrueId;
    checkFalseId = "#" + checkFalseId;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getcheckoptions/",
        data: { PillBook: PBId, Attribute: attr },
        success: function (data) {
            if (data == "SI") {
                $(checkFalseId).removeAttr("checked");
                $(checkTrueId).prop("checked", true);
                $("#bloqueo").hide();
            }
            if (data == "NO") {
                $(checkTrueId).removeAttr("checked");
                $(checkFalseId).prop("checked", true);
                $("#bloqueo").hide();
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function pastevalue(value) {

    var el = $(value).get(0);
    var pos = 0;
    pos = el.selectionStart;

    var session = sessionStorage.PillBookName;
    if ((session != null) && (session != undefined)) {

        InsertAtCursor(value, session);

    }
}

function InsertAtCursor(Fields, Value) {

    var Field = $(Fields).get(0);
    var startPos = Field.selectionStart;
    var endPos = Field.selectionEnd;
    var session = sessionStorage.PillBookName;
    if ((session != null) && (session != undefined) && (session != "undefined")) {
        Field.value = Field.value.substring(0, startPos) + Value +
                    Field.value.substring(endPos, Field.value.length);

        sessionStorage.PillBookName = undefined;
    }

}

function getreferences() {

    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getreferences/",
        data: { PillBook: PBId },
        success: function (data) {
            $("#references").empty();
            $.each(data, function (index, val) {
                $("#references")
                .append($("<tr></tr>")
                        .append($("<td style='width:10%'></td>").append("Referencia"))
                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='clinicalreference'></textarea>").append(val.ClinicalReference)))
                        .append($("<td style='width:5%;text-align:center'></td>").append("URL"))
                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='referencesource'></textarea><input type='text' value=" + val.ClinicalReferenceId + " id='ClinicalReferenceId'  style='display:none'>").append(val.ReferenceSource)))
                        .append($("<td style='width:5%;text-align:center'></td>").append("<button class='btn btn-info' onclick='editreferencepb($(this))'>Guardar</button></br></br><button class='btn btn-warning' onclick='deletereference($(this).val())' value=" + val.ClinicalReferenceId + ">Eliminar</button>")));
            })
        }
    })
}

function savereference(reference, urlreference) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
    if (!reference.trim() == true) {
        var message = "El campo 'Referencia' no puede quedar vac&iacute;o";
        d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../PillBook/savereference/",
            data: { ClinicalReference: reference, URL: urlreference, PillBook: PBId },
            success: function (data) {
                if (data == true) {
                    $.ajax({
                        type: "POST",
                        dataType: "Json",
                        traditional: true,
                        url: "../PillBook/getreferences/",
                        data: { PillBook: PBId },
                        success: function (data) {
                            $("#references").empty();
                            $.each(data, function (index, val) {
                                $("#references")
                                .append($("<tr></tr>")
                                       .append($("<td style='width:10%'></td>").append("Referencia"))
                                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='clinicalreference'></textarea>").append(val.ClinicalReference)))
                                        .append($("<td style='width:5%;text-align:center'></td>").append("URL"))
                                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='referencesource'></textarea><input type='text' value=" + val.ClinicalReferenceId + "  style='display:none' id='ClinicalReferenceId'>").append(val.ReferenceSource)))
                                        .append($("<td style='width:5%;text-align:center'></td>").append("<button class='btn btn-info' onclick='editreferencepb($(this))'>Guardar</button></br></br><button class='btn btn-warning' onclick='deletereference($(this).val())' value=" + val.ClinicalReferenceId + ">Eliminar</button>")));
                            })
                        }
                    })
                    $("#hideformref").trigger("click");
                }
                $("#bloqueo").hide();
            }
        })
    }
}

function showformaddref() {
    $("#showformref").hide();
    $("#hideformref").show();
    $("#traddref").show();
}

function hideformaddref() {
    $("#showformref").show();
    $("#hideformref").hide();
    $("#traddref").hide();
    $("#reference").val('');
    $("#urlreference").val('');
}

function editreferencepb(values) {
    var PBId = $("#PBId").val();
    var tr = $(values).parents("tr:first");
    var cr = tr.find("#clinicalreference").val();
    var rfs = tr.find("#referencesource").val();
    var crid = tr.find("#ClinicalReferenceId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/editreferencepb/",
        data: { ClinicalReference: cr, ReferenceSource: rfs, ClinicarRId: crid },
        success: function (data) {
            if (data == true) {
                $.ajax({
                    type: "POST",
                    dataType: "Json",
                    traditional: true,
                    url: "../PillBook/getreferences/",
                    data: { PillBook: PBId },
                    success: function (data) {
                        $("#references").empty();
                        $.each(data, function (index, val) {
                            $("#references")
                            .append($("<tr></tr>")
                                    .append($("<td style='width:10%'></td>").append("Referencia"))
                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='clinicalreference'></textarea>").append(val.ClinicalReference)))
                        .append($("<td style='width:5%;text-align:center'></td>").append("URL"))
                        .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='referencesource'></textarea><input type='text' style='display:none' value=" + val.ClinicalReferenceId + " id='ClinicalReferenceId'>").append(val.ReferenceSource)))
                        .append($("<td style='width:5%;text-align:center'></td>").append("<button class='btn btn-info' onclick='editreferencepb($(this))'>Guardar</button></br></br><button class='btn btn-warning' onclick='deletereference($(this).val())' value=" + val.ClinicalReferenceId + ">Eliminar</button>")));
                        })
                    }
                })
            }
            $("#bloqueo").hide();
        }
    })
}

function deletereference(reference) {
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletereferences/",
        data: { PillBook: PBId, ClinicarRId: reference },
        success: function (data) {
            $.ajax({
                type: "POST",
                dataType: "Json",
                traditional: true,
                url: "../PillBook/getreferences/",
                data: { PillBook: PBId },
                success: function (data) {
                    $("#references").empty();
                    $.each(data, function (index, val) {
                        $("#references")
                        .append($("<tr></tr>")
                                .append($("<td style='width:10%'></td>").append("Referencia"))
                                .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='clinicalreference'></textarea>").append(val.ClinicalReference)))
                                .append($("<td style='width:5%;text-align:center'></td>").append("URL"))
                                .append($("<td style='width:40%'></td>").append($("<textarea class='form-focus' style='height:100px' id='referencesource'></textarea><input type='text' value=" + val.ClinicalReferenceId + " id='ClinicalReferenceId'  style='display:none'>").append(val.ReferenceSource)))
                                .append($("<td style='width:5%;text-align:center'></td>").append("<button class='btn btn-info' onclick='editreferencepb($(this))'>Guardar</button></br></br><button class='btn btn-warning' onclick='deletereference($(this).val())' value=" + val.ClinicalReferenceId + ">Eliminar</button>")));
                    })
                }
            })
        }
    })
}






/*      SUBSTANCESCONTROLLER        */

function searchinnsubstancessc(description) {
    $("#bloqueo").show();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../Substances/getinnsubstances/",
        data: { Description: description },
        success: function (data) {
            $("#innasubstancessc").empty();
            $.each(data, function (index, val) {
                $("#innasubstancessc")
                .append($("<tr></tr>")
                .append($("<td style='width:10%;text-align:center'></td>").append("<input type='checkbox' onclick='getinnsubstances($(this))' value='" + val.INNActiveSubstanceId + "' Id='" + val.INNActiveSubstanceId + "' class='chksc' />"))
                .append($("<td style='width:90%'></td>").append(val.Description)));
            })
            $("#bloqueo").hide();
        }
    })
}

function searchplmsubstancessc(description) {
    $("#bloqueo").show();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../Substances/getplmsubstances/",
        data: { Description: description },
        success: function (data) {
            $("#plmactivesubstances").empty();
            $.each(data, function (index, val) {
                $("#plmactivesubstances")
                .append($("<tr></tr>")
                .append($("<td style='width:10%;text-align:center'></td>").append("<input type='checkbox' onclick='addplmtoinnsubstances($(this))' value='" + val.ActiveSubstanceId + "' Id='ActiveSubstanceId' class='chkscplm' />"))
                .append($("<td style='width:90%'></td>").append(val.Description)));
            })
            $("#bloqueo").hide();
        }
    })

}

function getinnsubstances(Id) {
    $(".chksc").prop("checked", false);

    var element = $(Id).attr("Id");

    $(Id).prop("checked", true);

    sessionStorage.INNId = element;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../Substances/getplminnsubstances/",
        data: { INNActiveSubstance: element },
        success: function (data) {
            $("#inaplmsubstancessc").empty();
            $.each(data, function (index, val) {
                $("#inaplmsubstancessc")
                .append($("<tr style='vertical-align:top'></tr>")
                .append($("<td style='width:90%'></td>").append(val.Description))
                .append($("<td style='width:10%;text-align:center'></td>").append("<button value='" + val.ActiveSubstanceId + "' class='btn btn-info' onclick='deleteplminnsubstances($(this).val())'>X</button>")));
            })
        }
    })

}

function addplmtoinnsubstances(ASIds) {
    $("#bloqueo").show();
    var Id = sessionStorage.INNId;
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    var tr = $(ASIds).parents("tr:first");

    var ASId = $(ASIds).val();

    if (tr.find(".chkscplm").is(':checked'))
    {
        $.ajax({
            type: "POST",
            dataType: "Json",
            traditional: true,
            url: "../Substances/addplmtoinnsubstances/",
            data: { ActiveSubstances: ASId, INNActiveSubstance: Id },
            success: function (data) {
                if (data == true) {
                    $.ajax({
                        type: "POST",
                        dataType: "Json",
                        traditional: true,
                        url: "../Substances/getplminnsubstances/",
                        data: { INNActiveSubstance: Id },
                        success: function (data) {
                            $("#inaplmsubstancessc").empty();
                            $.each(data, function (index, val) {
                                $("#inaplmsubstancessc")
                                .append($("<tr style='vertical-align:top'></tr>")
                                .append($("<td style='width:90%'></td>").append(val.Description))
                                .append($("<td style='width:10%;text-align:center'></td>").append("<button value='" + val.ActiveSubstanceId + "' class='btn btn-info' onclick='deleteplminnsubstances($(this).val())'>X</button>")));
                            })
                        }
                    })
                    $("#bloqueo").hide();
                }
                if (data == false) {
                    var as = document.getElementById(ASId);
                    $(as).removeAttr("checked");
                    var message = "La sustancia PLM seleccionada ya existe asociada a la sustancia INN";
                    d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        deleteplminnsubstances(ASId);
    }
}

function deleteplminnsubstances(ASId) {
    var Id = sessionStorage.INNId;
    $("#bloqueo").show();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../Substances/deleteplminnsubstances/",
        data: { ActiveSubstances: ASId, INNActiveSubstance: Id },
        success: function (data) {
            if (data == true) {
                $.ajax({
                    type: "POST",
                    dataType: "Json",
                    traditional: true,
                    url: "../Substances/getplminnsubstances/",
                    data: { INNActiveSubstance: Id },
                    success: function (data) {
                        $("#inaplmsubstancessc").empty();
                        $.each(data, function (index, val) {
                            $("#inaplmsubstancessc")
                            .append($("<tr style='vertical-align:top'></tr>")
                            .append($("<td style='width:90%'></td>").append(val.Description))
                            .append($("<td style='width:10%;text-align:center'></td>").append("<button value='" + val.ActiveSubstanceId + "' class='btn btn-info' onclick='deleteplminnsubstances($(this).val())'>X</button>")));
                        })
                    }
                })
                $("#bloqueo").hide();
            }
        }
    })
}

function closepillbook() {
   // $("#bloqueo").show();
    var PillBook = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addactivepbf/",
        data: { PBId: PillBook },
        success: function (data) {
            if (data == true) {
                $("#btnsuccessop").show();
                $("#closepb").show();
                $("#btnwarningcp").hide();
            }
            //$("#bloqueo").hide();
        }
    })
}

function openpillbook() {
   // $("#bloqueo").show();
    var PillBook = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/openpillbook/",
        data: { PBId: PillBook },
        success: function (data) {
            if (data == true) {
                $("#btnsuccessop").hide();
                $("#closepb").hide();
                $("#btnwarningcp").show();
            }
            //$("#bloqueo").hide();
        }
    })
}


function messageclosepb() {

    var d = "";
    d += "<div align='center' style='width:300px;text-align:center;'><img src='../Images/alerta.png' /> </div>";

    var message = "El PillBook esta cerrado";

    d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>&iexcl;&iexcl;Aviso!!</p>"
    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic;font-size:15px'>&bull; " + message + "</p>"
    apprise("" + d + "", { 'animate': true });
}


/*      THERAPEUTICS OMS*/

/*      POPUP       */
function open_popupaddtherapeuticsOMS() {
    $("#addtherapeuticsOMS").show();
    $("#closepopupaddtherapeuticsOMS").show();
    $("#openpopupaddtherapeuticsOMS").hide();
}

function close_popupaddtherapeuticsOMS() {
    setTimeout('document.location.reload()');
}

/*         FIN         */


/*         GET LEVELS OF TREE         */

function getlevel1TOMS(level1) {

    $("#bloqueo").show();

    var Id = $(level1).attr("Id");

    $(level1).hide();

    var imgid = "#collapselistOMS_" + Id;

    $(imgid).show();

    var OMS = "#OMS_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getOMSlevel2/",
        data: { Toms: Id },
        success: function (data) {
            $(OMS).empty();
            $(OMS).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(OMS)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel1OMS(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistOMS_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#OMS_" + level;

    $(ICD).empty();
}

function collapselevel2OMS(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistOMS_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#OMSl2_" + level;

    $(ICD).empty();
}

function getlevel2TOMS(level2) {

    $("#bloqueo").show();

    var Id = $(level2).attr("Id");

    $(level2).hide();

    var imgid = "#collapselistOMS_" + Id;

    $(imgid).show();

    var OMS = "#OMSl2_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getOMSlevel3/",
        data: { Toms: Id },
        success: function (data) {
            $(OMS).empty();
            $(OMS).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(OMS)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel3OMS(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistOMS_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#OMSl3_" + level;

    $(ICD).empty();
}

function getlevel3TOMS(level3) {

    $("#bloqueo").show();

    var Id = $(level3).attr("Id");

    $(level3).hide();

    var imgid = "#collapselistOMS_" + Id;

    $(imgid).show();

    var OMS = "#OMSl3_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getOMSlevel4/",
        data: { Toms: Id },
        success: function (data) {
            $(OMS).empty();
            $(OMS).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(OMS)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel4OMS(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistOMS_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#OMSl4_" + level;

    $(ICD).empty();
}

function getlevel4TOMS(level3) {

    $("#bloqueo").show();

    var Id = $(level3).attr("Id");

    $(level3).hide();

    var imgid = "#collapselistOMS_" + Id;

    $(imgid).show();

    var OMS = "#OMSl4_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getOMSlevel5/",
        data: { Toms: Id },
        success: function (data) {
            $(OMS).empty();
            $(OMS).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(OMS)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel5OMS(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistOMS_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ICD = "#OMSl4_" + level;

    $(ICD).empty();
}

function addpillbookatcoms(ATC) {
    var PBId = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addpillbookatcoms/",
        data: { ATCId: ATC, PillBook: PBId },
        success: function (data) {
            if (data == false) {
                var d = "";
                d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
                var message = "El Registro ya existe asociado al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }

    })
}

function deletepillbookatcoms(Thera) {
    $("#bloqueo").show();
    var PBId = $("#PBId").val();
    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/deletepillbookatcoms/",
        data: { PillBook: PBId, Therapeutic: Thera },
        success: function (data) {
            if (data == true) {
                setTimeout('document.location.reload()');
            }
        }
    })
}

/*         FIN         */



/*          GET LEVELS OF ATC TREE          */
function getlevel2ATC(level1) {

    $("#bloqueo").show();

    var Id = $(level1).attr("Id");

    $(level1).hide();

    var imgid = "#collapselistATC_" + Id;

    $(imgid).show();

    var ATC = "#ATC_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getATClevel2/",
        data: { Atc: Id },
        success: function (data) {
            $(ATC).empty();
            $(ATC).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(ATC)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel1ATC(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistATC_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ATC = "#ATC_" + level;

    $(ATC).empty();
}

function getlevel3ATC(level1) {

    $("#bloqueo").show();

    var Id = $(level1).attr("Id");

    $(level1).hide();

    var imgid = "#collapselistATC_" + Id;

    $(imgid).show();

    var ATC = "#ATCl2_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getATClevel3/",
        data: { Atc: Id },
        success: function (data) {
            $(ATC).empty();
            $(ATC).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(ATC)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel2ATC(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistATC_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ATC = "#ATCl2_" + level;

    $(ATC).empty();
}

function getlevel4ATC(level1) {

    $("#bloqueo").show();

    var Id = $(level1).attr("Id");

    $(level1).hide();

    var imgid = "#collapselistATC_" + Id;

    $(imgid).show();

    var ATC = "#ATCl3_" + Id;

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/getATClevel3/",
        data: { Atc: Id },
        success: function (data) {
            $(ATC).empty();
            $(ATC).append($("<li></li>").text())
            $.each(data, function (index, val) {
                $(ATC)
                .append($("<li></li>")
                    .append(val.SpanishDescription));
            })
            $("#bloqueo").hide();
        }
    })
}

function collapselevel3ATC(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistATC_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ATC = "#ATCl3_" + level;

    $(ATC).empty();
}

function collapselevel4ATC(level1) {

    var Id = $(level1).attr("Id");

    var level = Id.replace("collapselistATC_", "");

    var img = document.getElementById(level);

    $(img).show();

    var imgcollapse = document.getElementById(Id);

    $(imgcollapse).hide();

    var ATC = "#ATCl4_" + level;

    $(ATC).empty();
}

function addpillbookatcephmra(ATC) {
    var PillBook = $("#PBId").val();

    $.ajax({
        type: "POST",
        dataType: "Json",
        traditional: true,
        url: "../PillBook/addpillbookatc/",
        data: { PBId: PillBook, Therapeutic: ATC },
        success: function (data) {
            if (data == false) {
                var d = "";
                d += "<div align='center'><img src='../Images/alerta.png' /> </div>";
                var message = "El Registro ya existe asociado al PillBook";
                d += "<p style='width:300px;text-align:center;font-size:30px;color:#05606d;font-style:italic'>Error!!</p>"
                d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; " + message + "</p>"
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }

    })
}

//function deletepillbookatcoms(Thera) {
//    $("#bloqueo").show();
//    var PBId = $("#PBId").val();
//    $.ajax({
//        type: "POST",
//        dataType: "Json",
//        traditional: true,
//        url: "../PillBook/deletepillbookatcoms/",
//        data: { PillBook: PBId, Therapeutic: Thera },
//        success: function (data) {
//            if (data == true) {
//                setTimeout('document.location.reload()');
//            }
//        }
//    })
//}