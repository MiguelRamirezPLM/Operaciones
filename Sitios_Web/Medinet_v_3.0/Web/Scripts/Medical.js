function ChangeProductType(item) {

    var tr = $(item).parents("tr:first");

    var PTId = tr.find("#ProductTypeId").val();
    var PId = tr.find("#ProductId").val();
    var vl = $(item).val();

    var Message = "";

    Message += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
    Message += "<label> ¿Esta seguro que desea cambiar el tipo de Producto? </label>";

    apprise("" + Message + "", { 'verify': true }, function (r) {

        if (r) {

            $.ajax({
                Type: "POST",
                dataType: "Json",
                url: "../Medical/UpdateProductType",
                data: { Product: PId, ProductType: vl },
                success: function (data) {
                    if (data == true) {

                        $("#messageheader").text("Tipo de Producto");

                        $("#lblMessageModal").text("El tipo de Producto se actualizo correctamente.");

                        $("#btnProductType").trigger("click");

                    }
                    else if (data == false) {

                        var Id = "O_" + PTId;

                        var itm = document.getElementById(Id);

                        tr.find("#ProductTypes").val($(itm).val());
                    }
                }
            })
        }
        else {

            var Id = "O_" + PTId;

            var itm = document.getElementById(Id);

            tr.find("#ProductTypes").val($(itm).val());

        }
    });
}

function getlevel2SM(TherapeuticId) {

    var ListId = "#ListL2_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelTwoATCEphMRA",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            //$(ListId).empty();
            //$.each(data, function (index, val) {
            //    $(ListId).append($("<li></li>")
            //        .append("<span class='glyphicon glyphicon-plus' id='Expand_" + val.TherapeuticId + "' onclick='getlevel3SM(" + val.TherapeuticId + ")' style='cursor:pointer'></span>" +
            //                        "<span class='glyphicon glyphicon-minus' style='display:none' id='Collapse_" + val.TherapeuticId + "' onclick='collapselevel3(" + val.TherapeuticId + ")'></span>" +
            //                        "&nbsp;&nbsp;&nbsp;<span style='font-weight:bold'><b>" + val.TherapeuticKey + "</b></span> - <span style='font-weight:100'>" + val.SpanishDescription + "</span>" +
            //                        "<ul id='ListL3_" + val.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>"));
            //});


            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });

        }
    })
}

function collapselevel2(TherapeuticId) {
    var ListId = "#ListL2_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}

function getlevel3SM(TherapeuticId) {

    var ListId = "#ListL3_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelThreeATCEphMRA",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            //$(ListId).empty();
            //$.each(data, function (index, val) {
            //    $(ListId).append($("<li></li>")
            //        .append("<span class='glyphicon glyphicon-plus' id='Expand_" + val.TherapeuticId + "' onclick='getlevel4SM(" + val.TherapeuticId + ")' style='cursor:pointer'></span>" +
            //                        "<span class='glyphicon glyphicon-minus' style='display:none' id='Collapse_" + val.TherapeuticId + "' onclick='collapselevel3(" + val.TherapeuticId + ")'></span>" +
            //                        "<span style='font-weight:bold'><b>" + val.TherapeuticKey + "</b></span> - <span style='font-weight:100'>" + val.SpanishDescription + "</span>" +
            //                        "<ul id='ListL4_" + val.TherapeuticId + "' style='list-style: none;margin-left:50px'></ul>"));
            //});

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });

        }
    })
}

function collapselevel3(TherapeuticId) {
    var ListId = "#ListL3_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}


function getlevel4SM(TherapeuticId) {

    var ListId = "#ListL4_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelFourATCEphMRA",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });


        }
    })
}

function collapselevel4(TherapeuticId) {
    var ListId = "#ListL4_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}


function getlevel5SM(TherapeuticId) {

    var ListId = "#ListL5_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelFiveATCEphMRA",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });


        }
    })
}

function collapselevel5(TherapeuticId) {
    var ListId = "#ListL5_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}


var ListAddATCEphMRA = [];

var ListRemoveATCEphMRA = [];

function AddATCEphMRA(item) {

    var TId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListAddATCEphMRA.push({
            'TId': TId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListAddATCEphMRA);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListAddATCEphMRA, "TId", "PId", "PFId", TId, PId, PFId);

        if (index == null) {
        } else if (index >= 0) {

            ListAddATCEphMRA.splice(index, 1);

        }
    }
}

function AddATCEphMRARemove(item) {

    var TId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListRemoveATCEphMRA.push({
            'TId': TId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListRemoveATCEphMRA);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListRemoveATCEphMRA, "TId", "PId", "PFId", TId, PId, PFId);

        if (index == null) {
        }
        else if (index >= 0) {
            ListRemoveATCEphMRA.splice(index, 1);
            console.log(ListRemoveATCEphMRA);
        }
    }
}

function functiontofindIndexByKeyValue(arraytosearch, TId, PId, PFId, valuetTId, valuePId, valuePFId) {

    for (var i = 0; i < arraytosearch.length; i++) {

        if (arraytosearch[i][TId] == valuetTId) {
            if (arraytosearch[i][PId] == valuePId) {
                if (arraytosearch[i][PFId] == valuePFId) {
                    return i;
                }
            }
        }
    }
    return null;
};

function SaveAddATCEphMRA() {
    // alert("click");
    $("#myBar").css("background-color", "#286090");

    var Size = $(ListAddATCEphMRA).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddATCEphMRA);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveAddATCEphMRA",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    console.log(s);

                    console.log(Size);

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })

    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function RemoveATCEphMRA() {

    $("#myBar").css("background-color", "#f7726e");

    var Size = $(ListRemoveATCEphMRA).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListRemoveATCEphMRA);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/RemoveATCEphMRA",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                    d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                    d += "<br/>";
                    d += "<ul style='list-style:none'>";
                    $.each(data, function (index, val) {
                        d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                    });
                    d += "</ul>";

                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para quitar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }


}


/*              CONTRAINDICATIONS           */

function GetPhysiologicalContraindications(DivisionId, CategoryId, PharmaFormId, ProductId, ActiveSubstanceId, EditionId) {

    //$.ajax({
    //    Type: "POST",
    //    dataType: "Json",
    //    url: "../Medical/GetPhysiologicalContraindications",
    //    data: { Division: DivisionId, Category: CategoryId, PharmaForm: PharmaFormId, Product: ProductId, ActiveSubstance: ActiveSubstanceId },
    //    success: function (data) {

    //    }
    //})

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetPhysiologicalContraindicationsAsoc",
        data: { Division: DivisionId[0], Category: CategoryId[0], PharmaForm: PharmaFormId[0], Product: ProductId[0], ActiveSubstance: ActiveSubstanceId[0], Edition: EditionId[0] },
        success: function (data) {
            $("#PhysiologicalContraindicationsAsoc").empty();

            $("#PhysiologicalContraindicationsAsoc").append("<tr>" +
                                                            "<td>" +
                                                            "<span>Sustancia</span>" +
                                                            "</td>" +
                                                            "<td>" +
                                                            "<span>Contraindicacion</span>" +
                                                            "</td>" +
                                                            "<td class=\"text-center\">" +
                                                            "<button class=\"btn btn-danger btn-sm\"><span class=\"glyphicon glyphicon-remove\"></span></button>" +
                                                            "</td>" +
                                                            "</tr>");
        }
    })
}




/*              CIE - 10           */


function GetLevelTwoICD(ICDId) {

    $("#bloqueo").show();

    var ListId = "#ListL2_" + ICDId;
    var ExpandBTN = "#Expand_" + ICDId;
    var CollapseBTN = "#Collapse_" + ICDId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelTwoICD",
        data: { ICD: ICDId },
        success: function (data) {
            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });
            $("#bloqueo").hide();
        }
    })
}

function GetLevelThreeICD(ICDId) {

    $("#bloqueo").show();

    var ListId = "#ListL3_" + ICDId;
    var ExpandBTN = "#Expand_" + ICDId;
    var CollapseBTN = "#Collapse_" + ICDId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelThreeICD",
        data: { ICD: ICDId },
        success: function (data) {
            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });
            $("#bloqueo").hide();
        }
    })

}

var ListAddCIE = [];

var ListRemoveCIE = [];

function AddCIE10(item) {

    var ICDId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListAddCIE.push({
            'TId': ICDId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListAddCIE);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListAddCIE, "TId", "PId", "PFId", ICDId, PId, PFId);

        if (index == null) {
        } else if (index >= 0) {

            ListAddCIE.splice(index, 1);

            console.log(ListAddCIE);
        }
    }
}

function AddCIE10Remove(item) {

    var ICDId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListRemoveCIE.push({
            'TId': ICDId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListRemoveCIE);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListRemoveCIE, "TId", "PId", "PFId", ICDId, PId, PFId);

        if (index == null) {
        }
        else if (index >= 0) {
            ListRemoveCIE.splice(index, 1);
            console.log(ListRemoveCIE);
        }
    }
}

function SaveCIE10() {

    var Size = $(ListAddCIE).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddCIE);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveCIE10",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })

    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function RemoveCIE10() {

    var Size = $(ListRemoveCIE).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListRemoveCIE);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/RemoveCIE10",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                    d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                    d += "<br/>";
                    d += "<ul style='list-style:none'>";
                    $.each(data, function (index, val) {
                        d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                    });
                    d += "</ul>";

                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para quitar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}



/*              ATC OMS           */


function getlevel2OMS(TherapeuticId) {

    var ListId = "#ListL2_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelTwoATCOMS",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            //$(ListId).empty();
            //$.each(data, function (index, val) {
            //    $(ListId).append($("<li></li>")
            //        .append("<span class='glyphicon glyphicon-plus' id='Expand_" + val.TherapeuticId + "' onclick='getlevel3SM(" + val.TherapeuticId + ")' style='cursor:pointer'></span>" +
            //                        "<span class='glyphicon glyphicon-minus' style='display:none' id='Collapse_" + val.TherapeuticId + "' onclick='collapselevel3(" + val.TherapeuticId + ")'></span>" +
            //                        "&nbsp;&nbsp;&nbsp;<span style='font-weight:bold'><b>" + val.TherapeuticKey + "</b></span> - <span style='font-weight:100'>" + val.SpanishDescription + "</span>" +
            //                        "<ul id='ListL3_" + val.TherapeuticId + "' style='list-style: none;margin-left:30px'></ul>"));
            //});


            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });

        }
    })
}

function collapselevel2OMS(TherapeuticId) {
    var ListId = "#ListL2_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}

function getlevel3OMS(TherapeuticId) {

    var ListId = "#ListL3_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelThreeATCOMS",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            //$(ListId).empty();
            //$.each(data, function (index, val) {
            //    $(ListId).append($("<li></li>")
            //        .append("<span class='glyphicon glyphicon-plus' id='Expand_" + val.TherapeuticId + "' onclick='getlevel4SM(" + val.TherapeuticId + ")' style='cursor:pointer'></span>" +
            //                        "<span class='glyphicon glyphicon-minus' style='display:none' id='Collapse_" + val.TherapeuticId + "' onclick='collapselevel3(" + val.TherapeuticId + ")'></span>" +
            //                        "<span style='font-weight:bold'><b>" + val.TherapeuticKey + "</b></span> - <span style='font-weight:100'>" + val.SpanishDescription + "</span>" +
            //                        "<ul id='ListL4_" + val.TherapeuticId + "' style='list-style: none;margin-left:50px'></ul>"));
            //});

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });

        }
    })
}

function collapselevel3OMS(TherapeuticId) {
    var ListId = "#ListL3_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}

function getlevel4OMS(TherapeuticId) {

    var ListId = "#ListL4_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelFourATCOMS",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });


        }
    })
}

function collapselevel4OMS(TherapeuticId) {
    var ListId = "#ListL4_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}

function getlevel5OMS(TherapeuticId) {

    var ListId = "#ListL5_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelFiveATCOMS",
        data: { Therapeutic: TherapeuticId },
        success: function (data) {

            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });


        }
    })
}

function collapselevel5OMS(TherapeuticId) {
    var ListId = "#ListL5_" + TherapeuticId;
    var ExpandBTN = "#Expand_" + TherapeuticId;
    var CollapseBTN = "#Collapse_" + TherapeuticId;

    $(ListId).empty();
    $(ExpandBTN).show();
    $(CollapseBTN).hide();

}

var ListAddATCOMS = [];

var ListRemoveATCOMS = [];

function AddATCOMS(item) {

    var TId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListAddATCOMS.push({
            'TId': TId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListAddATCOMS);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListAddATCOMS, "TId", "PId", "PFId", TId, PId, PFId);

        if (index == null) {
        } else if (index >= 0) {

            ListAddATCOMS.splice(index, 1);

            console.log(ListAddATCOMS);

        }
    }
}

function AddATCOMSRemove(item) {

    var TId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListRemoveATCOMS.push({
            'TId': TId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListRemoveATCOMS);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListRemoveATCOMS, "TId", "PId", "PFId", TId, PId, PFId);

        if (index == null) {
        }
        else if (index >= 0) {
            ListRemoveATCOMS.splice(index, 1);

            console.log(ListRemoveATCOMS);
        }
    }
}

function SaveAddATCOMS() {

    var Size = $(ListAddATCOMS).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddATCOMS);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveAddATCOMS",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    console.log(s);

                    console.log(Size);

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })

    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function RemoveATCOMS() {

    var Size = $(ListRemoveATCOMS).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListRemoveATCOMS);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/RemoveATCOMS",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                    d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                    d += "<br/>";
                    d += "<ul style='list-style:none'>";
                    $.each(data, function (index, val) {
                        d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                    });
                    d += "</ul>";

                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para quitar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}



/*              INTERACTIONS           */

function GetRowsPerPageInteractions() {

    $("#bloqueo").show();

    var AS = $("#RowsPerPageActiveSubstances").val();
    var PG = $("#RowsPerPagePharmacologicalGroups").val();
    var OE = $("#RowsPerPageOtherElements").val();

    sessionStorage.AS = AS;
    sessionStorage.PG = PG;
    sessionStorage.OE = OE;

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/Pager",
        data: { Page: AS, PagePG: PG, PageOE: OE },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function GetInteracionMenu(item) {

    var Id = $(item).attr("Id");

    sessionStorage.InteractionMenu = Id;

}

function load() {

    var Id = sessionStorage.InteractionMenu;

    if ((Id != null) && (Id != undefined)) {

        Id = "#" + Id;

        $(Id).trigger("click");
    }

    var AS = sessionStorage.AS;
    var PG = sessionStorage.PG;
    var OE = sessionStorage.OE;
    var Ind = sessionStorage.Ind;

    if ((AS != null) && (AS != undefined)) {

        AS = "#IdAS_" + AS;

        $("#RowsPerPageActiveSubstances").val($(AS).val());
    }

    if ((PG != null) && (PG != undefined)) {

        PG = "#IdPG_" + PG;

        $("#RowsPerPagePharmacologicalGroups").val($(PG).val());

    }

    if ((OE != null) && (OE != undefined)) {

        OE = "#IdOE_" + OE;

        $("#RowsPerPageOtherElements").val($(OE).val());
    }

    if ((Ind != null) && (Ind != undefined)) {

        Ind = "#IdInd_" + Ind;

        $("#RowsPerPageIndications").val($(Ind).val());
    }
}

function AddProductSubstanceInteractions(value, SubstanceInteract) {

    $("#bloqueo").show();

    var size = $(".SubstanceInteract").size();
    var DId = $("#DivisionId").val();
    var CId = $("#CategoryId").val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    var JsonList = [];

    if ((SubstanceInteract == null) || (SubstanceInteract == undefined)) {
        for (var i = 0; i <= size - 1; i++) {
            if ((i != 0) || (i != undefined)) {
                var item = $(".SubstanceInteract")[i];

                var SubstanceInteract = $(item).find("#inpActiveSubstanceId").val();

                JsonList.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": SubstanceInteract,
                        "ActiveSubstance": value
                    });
            }
        }
    }
    else {
        JsonList.push(
                        {
                            "Division": DId,
                            "Category": CId,
                            "PharmaForm": PFId,
                            "Product": PId,
                            "SubstanceInteract": SubstanceInteract,
                            "ActiveSubstance": value
                        });
    }



    var jsonresponse = JSON.stringify(JsonList);

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/AddProductSubstanceInteractions",
        data: { Size: size, SubstanceInteractions: jsonresponse },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {

                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                d += "<br/>"
                d += "<div>"
                d += "<ul>"

                $.each(data, function (index, val) {
                    d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                });

                d += "</ul>"
                d += "</div>"

                apprise("" + d + "", { 'animate': true });

                JsonList = [];
                JsonListTI = [];

                $("#bloqueo").hide();
            }
        }
    })
}

function CheckActiveSubstancesByProduct(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");


    var value = tr.find("#lblActiveSubstanceId").val();
    var Substance = tr.find("#spnDescription").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelAddASINT();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckActiveSubstancesByProduct",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, ActiveSubstance: value },
        success: function (data) {
            if (data.Data == true) {
                $("#messageheaderASINT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductId\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryId\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionId\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormId\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceId\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: <label style='font-weight:bold'>" + Substance + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddSubstancestoInteractions($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                });

                $("#DivASINT").append(Content);
                $("#btnASINT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                AddProductSubstanceInteractions(value, null);
            }
        }
    })
}

function CancelAddASINT() {

    $("#DivASINT").empty();

}

var JsonListTI = [];

function AddSubstancestoInteractions(item) {

    var ASId = $("#MdlActiveSubstanceId").val();
    var value = $(item).val();
    var PId = $("#MdlProductId").val();
    var CId = $("#MdlCategoryId").val();
    var DId = $("#MdlDivisionId").val();
    var PFId = $("#MdlPharmaFormId").val();

    if ($(item).is(":checked")) {

        JsonListTI.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(JsonListTI, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            JsonListTI.splice(index, 1);
        }
    }
}

function RemoveItemOfJsonListTI(arraytosearch, Division, Category, PharmaForm, Product, SubstanceInteract, ActiveSubstance, DId, CId, PFId, PId, value, ASId) {
    for (var i = 0; i < arraytosearch.length; i++) {
        if (arraytosearch[i][Division] == DId) {
            if (arraytosearch[i][Category] == CId) {
                if (arraytosearch[i][PharmaForm] == PFId) {
                    if (arraytosearch[i][Product] == PId) {
                        if (arraytosearch[i][SubstanceInteract] == value) {
                            if (arraytosearch[i][ActiveSubstance] == ASId) {
                                return i;
                            }
                        }
                    }
                }
            }
        }
    }
    return null;
};

function AddSubstanceInteractionsTI() {

    $("#bloqueo").show();

    var size = $(JsonListTI).size();
    var jsonresponse = JSON.stringify(JsonListTI);

    if ((size != 0) || (size != "0")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/AddProductSubstanceInteractions",
            data: { Size: size, SubstanceInteractions: jsonresponse },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                    d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                    d += "<br/>"
                    d += "<div>"
                    d += "<ul>"

                    $.each(data, function (index, val) {
                        d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                    });

                    d += "</ul>"
                    d += "</div>"

                    apprise("" + d + "", { 'animate': true });

                    JsonList = [];
                    JsonListTI = [];

                    $("#bloqueo").hide();
                }
            }
        })
    } else {

        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro.</label> <br/>"
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
}

function DeleteSubstanceInteracions(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblSubstanceInteractionIdAsoc").val();
    var value = tr.find("#lblActiveSubstanceIdAsoc").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteProductSubstanceInteractions",
        data: { ActiveSubstance: value, SubstanceInteraction: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}


/*              PHARMACOLOGICAL GROUPS              */

function ChecktPharmacologicalGroupsByProduct(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");


    var value = tr.find("#lblPharmaGroupId").val();
    var Group = tr.find("#lblGroupName").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelAddAPGNT();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckPharmacologicalGroupsByProduct",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, PharmacologicalGroup: value },
        success: function (data) {
            if (data.Data == true) {
                $("#messageheaderPGINT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdPG\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdPG\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: <label style='font-weight:bold'>" + Group + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddPharmaGroupInteractionsTI($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    console.log(val.Description);

                });

                $("#DivPGINT").append(Content);
                $("#btnPGINT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                AddProductPharmacologicalGroupsInteractions(value, null);
            }
        }
    })
}

function AddProductPharmacologicalGroupsInteractions(value, SubstanceInteract) {

    $("#bloqueo").show();

    var size = $(".SubstanceInteract").size();
    var DId = $("#DivisionId").val();
    var CId = $("#CategoryId").val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    var JsonList = [];


    if ((SubstanceInteract == null) || (SubstanceInteract == undefined)) {
        for (var i = 0; i <= size - 1; i++) {
            if ((i != 0) || (i != undefined)) {
                var item = $(".SubstanceInteract")[i];

                var SubstanceInteract = $(item).find("#inpActiveSubstanceId").val();

                JsonList.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": SubstanceInteract,
                        "ActiveSubstance": value
                    });
            }
        }
    }
    else {

        JsonList.push(
                        {
                            "Division": DId,
                            "Category": CId,
                            "PharmaForm": PFId,
                            "Product": PId,
                            "SubstanceInteract": SubstanceInteract,
                            "PharmaGroupId": value
                        });
    }

    var jsonresponse = JSON.stringify(JsonList);

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/AddProductPharmacologicalGroupsInteractions",
        data: { Size: size, SubstanceInteractions: jsonresponse },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {

                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                d += "<br/>"
                d += "<div>"
                d += "<ul>"

                $.each(data, function (index, val) {
                    d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                });

                d += "</ul>"
                d += "</div>"
                apprise("" + d + "", { 'animate': true });

                JsonList = [];
                JsonListTI = [];

                $("#bloqueo").hide();
            }
        }
    })
}

var JsonListTIPG = [];

function AddPharmaGroupInteractionsTI(item) {

    var ASId = $("#MdlActiveSubstanceIdPG").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdPG").val();
    var CId = $("#MdlCategoryIdPG").val();
    var DId = $("#MdlDivisionIdPG").val();
    var PFId = $("#MdlPharmaFormIdPG").val();

    if ($(item).is(":checked")) {

        JsonListTIPG.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(JsonListTIPG);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(JsonListTIPG, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            JsonListTIPG.splice(index, 1);

            console.log(JsonListTIPG);
        }
    }

}

function AddPharmaGroupInteractions() {

    $("#bloqueo").show();

    var size = $(JsonListTIPG).size();
    var jsonresponse = JSON.stringify(JsonListTIPG);

    if ((size != 0) || (size != "0")) {

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/AddProductPharmacologicalGroupsInteractions",
            data: { Size: size, SubstanceInteractions: jsonresponse },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                    d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                    d += "<br/>"
                    d += "<div>"
                    d += "<ul>"

                    $.each(data, function (index, val) {
                        d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                    });

                    d += "</ul>"
                    d += "</div>"
                    apprise("" + d + "", { 'animate': true });

                    JsonList = [];
                    JsonListTIPG = [];

                    $("#bloqueo").hide();

                }
            }
        })

    } else {

        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro.</label> <br/>"
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
}

function DeletePharmaGroupInteractions(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblPharmaGroupIdPG").val();
    var value = tr.find("#lblActiveSubstanceIdPG").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteProductSubstanceInteractions",
        data: { ActiveSubstance: value, SubstanceInteraction: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function CancelAddAPGNT() {

    $("#DivPGINT").empty();

}

function DeletePharmacologicalInteracions(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblPharmaGroupIdPG").val();
    var value = tr.find("#lblActiveSubstanceIdPG").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeletePharmacologicalInteracions",
        data: { ActiveSubstance: value, PharmacologicalInteraction: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}


/*          OTHER ELEMENTS      */

function CheckOtherElementsByProduct(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = tr.find("#lblElementId").val();
    var Element = tr.find("#lblElementName").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelAddAOENT();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckOtherElementsByProduct",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, OtherElement: value },
        success: function (data) {
            if (data.Data == true) {

                $("#messageheaderOEINT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdOE\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdOE\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdOE\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdOE\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdOE\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <label style='font-weight:bold'>" + Element + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddOtherElementsInteractionsTI($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    console.log(val.Description);

                });

                $("#DivOEINT").append(Content);
                $("#btnOEINT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                AddProductOtherElementInteractions(value, null);
            }
        }
    })
}

function AddProductOtherElementInteractions(value, SubstanceInteract) {

    var size = $(".SubstanceInteract").size();
    var DId = $("#DivisionId").val();
    var CId = $("#CategoryId").val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    var JsonList = [];


    if ((SubstanceInteract == null) || (SubstanceInteract == undefined)) {
        for (var i = 0; i <= size - 1; i++) {
            if ((i != 0) || (i != undefined)) {
                var item = $(".SubstanceInteract")[i];

                var SubstanceInteract = $(item).find("#inpActiveSubstanceId").val();

                JsonList.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": SubstanceInteract,
                        "ActiveSubstance": value
                    });
            }
        }
    }
    else {

        JsonList.push(
                        {
                            "Division": DId,
                            "Category": CId,
                            "PharmaForm": PFId,
                            "Product": PId,
                            "SubstanceInteract": SubstanceInteract,
                            "PharmaGroupId": value
                        });
    }

    var jsonresponse = JSON.stringify(JsonList);

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/AddProductOtherElementInteractions",
        data: { Size: size, OtherElementInteractions: jsonresponse },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                d += "<br/>"
                d += "<div>"
                d += "<ul>"

                $.each(data, function (index, val) {
                    d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                });

                d += "</ul>"
                d += "</div>"
                apprise("" + d + "", { 'animate': true });

                JsonList = [];
                JsonListTIOE = [];

                $("#bloqueo").hide();
            }
        }
    })
}

var JsonListTIOE = [];

function AddOtherElementsInteractionsTI(item) {

    var ASId = $("#MdlActiveSubstanceIdOE").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdOE").val();
    var CId = $("#MdlCategoryIdOE").val();
    var DId = $("#MdlDivisionIdOE").val();
    var PFId = $("#MdlPharmaFormIdOE").val();

    if ($(item).is(":checked")) {

        JsonListTIOE.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(JsonListTIOE);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(JsonListTIOE, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            JsonListTIOE.splice(index, 1);

            console.log(JsonListTIOE);
        }
    }

}

function AddOtherElementsInteractions() {

    $("#bloqueo").show();

    var size = $(JsonListTIOE).size();
    var jsonresponse = JSON.stringify(JsonListTIOE);

    if ((size != 0) || (size != "0")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/AddProductOtherElementInteractions",
            data: { Size: size, OtherElementInteractions: jsonresponse },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                    d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                    d += "<br/>"
                    d += "<div>"
                    d += "<ul>"

                    $.each(data, function (index, val) {
                        d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                    });

                    d += "</ul>"
                    d += "</div>"

                    apprise("" + d + "", { 'animate': true });

                    JsonList = [];
                    JsonListTIOE = [];

                    $("#bloqueo").hide();

                }
            }
        })
    } else {

        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro.</label> <br/>"
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
}

function CancelAddAOENT() {

    $("#DivOEINT").empty();

}

function DeleteOtherElementInteracions(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblElementIdAsoc").val();
    var value = tr.find("#lblActiveSubstanceIdOE").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteOtherElementInteracions",
        data: { ActiveSubstance: value, OtherElement: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function NoInteractions() {

    $("#bloqueo").show();

    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    var d = "";
    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

    d += "<label> Ser&aacute;n eliminados los registros asociados al producto.</label> <br/>"
    d += "<br/>"
    d += "<p style='text-align:center'> ¿Desea continuar con la operaci&oacute;n?</> <br/>"

    apprise("" + d + "", { 'verify': true }, function (r) {
        if (r) {
            $.ajax({
                Type: "POST",
                dataType: "Json",
                url: "../Medical/NoInteractions",
                data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
                success: function (data) {
                    if (data == true) {
                        setTimeout("document.location.reload()");
                    } else {
                        $("#bloqueo").hide();
                    }
                }
            })
        } else {
            $("#bloqueo").hide();
        }
    })
}



/*              CIE - 10           */


function GetLevelTwoCICD(ICDId) {

    $("#bloqueo").show();

    var ListId = "#ListL2_" + ICDId;
    var ExpandBTN = "#Expand_" + ICDId;
    var CollapseBTN = "#Collapse_" + ICDId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelTwoCICD",
        data: { ICD: ICDId },
        success: function (data) {
            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });
            $("#bloqueo").hide();
        }
    })
}

function GetLevelThreeCICD(ICDId) {

    $("#bloqueo").show();

    var ListId = "#ListL3_" + ICDId;
    var ExpandBTN = "#Expand_" + ICDId;
    var CollapseBTN = "#Collapse_" + ICDId;

    $(ListId).empty();
    $(ExpandBTN).hide();
    $(CollapseBTN).show();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/GetLevelThreeCICD",
        data: { ICD: ICDId },
        success: function (data) {
            $(ListId).empty();
            $.each(data, function (index, val) {
                $(ListId).append($("<li>" + val + "</li>"));
            });
            $("#bloqueo").hide();
        }
    })

}

var ListAddCCIE = [];

var ListRemoveCCIE = [];

function AddCCIE10(item) {

    var ICDId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListAddCIE.push({
            'TId': ICDId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListAddCIE);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListAddCIE, "TId", "PId", "PFId", ICDId, PId, PFId);

        if (index == null) {
        } else if (index >= 0) {

            ListAddCIE.splice(index, 1);

            console.log(ListAddCIE);
        }
    }
}

function AddCCIE10Remove(item) {

    var ICDId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListRemoveCIE.push({
            'TId': ICDId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListRemoveCIE);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValue(ListRemoveCIE, "TId", "PId", "PFId", ICDId, PId, PFId);

        if (index == null) {
        }
        else if (index >= 0) {
            ListRemoveCIE.splice(index, 1);
            console.log(ListRemoveCIE);
        }
    }
}

function SaveCCIE10() {

    var Size = $(ListAddCIE).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddCIE);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveCIE10",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })

    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function RemoveCCIE10() {

    var Size = $(ListRemoveCIE).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListRemoveCIE);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/RemoveCIE10",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                    d += "<label> Los siguientes usos, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                    d += "<br/>";
                    d += "<ul style='list-style:none'>";
                    $.each(data, function (index, val) {
                        d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                    });
                    d += "</ul>";

                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para quitar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}


/*          INDICATIONS         */

function AddProductIndications(value) {

    $("#bloqueo").show();

    var PId = $("#ProductId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/AddProductIndications",
        data: { Product: PId, Indication: value },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function RemoveProductIndications(value) {

    $("#bloqueo").show();

    var PId = $("#ProductId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/RemoveProductIndications",
        data: { Product: PId, Indication: value },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function GetRowsPerPageIndications() {

    $("#bloqueo").show();

    var Ind = $("#RowsPerPageIndications").val();

    sessionStorage.Ind = Ind;

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/PagerInd",
        data: { Page: Ind },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}



/*      CONTRAINDICATIONS       */

var ListAddCIECNT = [];

function CheckCIE10Contraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = $(item).val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();
    var Element = $(item).attr("Id");

    CancelAddCIECNT();

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/CheckSubstancesContraIndications",
            data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, PharmacologicalGroup: value },
            success: function (data) {

                if (data.Data == true) {

                    $("#messageheaderCIECNT").text("Aviso");

                    var Content = "";
                    Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdCIECNT\" />";
                    Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdCIECNT\"/>";
                    Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdCIECNT\"/>";
                    Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdCIECNT\"/>";
                    Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlICDIdCIECNT\"/></div>";
                    Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <span style='font-weight:bold'>" + Element + "</span></span><br /><br />";

                    $.each(data.Items, function (index, val) {
                        Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddCIE10Contraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    });

                    $("#DivCIECNT").append(Content);
                    $("#btnCIECNT").trigger("click");

                    $("#bloqueo").hide();
                }
                else if (data.Data == false) {
                    //AddProductOtherElementInteractions(value, null);
                }
            }
        })
    }
    else if ($(item).is(":not(:checked)")) {

        var size = $(ListAddCIECNT).size();

        for (var e = 0; e <= size - 1; e++) {

            var index = functiontofindIndexByKeyValueCIECNT(ListAddCIECNT, "ICDId", "PId", "PFId", value, PId, PFId);

            if (index == null) {
            } else if (index >= 0) {

                ListAddCIECNT.splice(index, 1);
            }
        }

        console.log(ListAddCIECNT);

        $("#bloqueo").hide();
    }
}

function CancelAddCIECNT() {

    $("#DivCIECNT").empty();

}

function AddCIE10Contraindications(item) {

    var ICDId = $("#MdlICDIdCIECNT").val();
    var ASId = $(item).val();
    var PFId = $("#PharmaFormId").val();
    var PId = $("#ProductId").val();

    if ($(item).is(":checked")) {

        ListAddCIECNT.push({
            'ICDId': ICDId,
            'ASId': ASId,
            'PFId': PFId,
            'PId': PId
        });

        console.log(ListAddCIECNT);

    }
    else if ($(item).is(":not(:checked)")) {

        var index = functiontofindIndexByKeyValueCIECNTELM(ListAddCIECNT, "ASId", "ICDId", "PId", "PFId", ASId, ICDId, PId, PFId);

        if (index == null) {
        } else if (index >= 0) {

            ListAddCIECNT.splice(index, 1);

            console.log(ListAddCIECNT);
        }
    }
}

function functiontofindIndexByKeyValueCIECNT(arraytosearch, ICDId, PId, PFId, valuetICDId, valuePId, valuePFId) {

    for (var i = 0; i < arraytosearch.length; i++) {

        if (arraytosearch[i][ICDId] == valuetICDId) {
            if (arraytosearch[i][PId] == valuePId) {
                if (arraytosearch[i][PFId] == valuePFId) {
                    return i;
                }
            }
        }
    }
    return null;
};

function functiontofindIndexByKeyValueCIECNTELM(arraytosearch, ASId, ICDId, PId, PFId, valueASId, valuetICDId, valuePId, valuePFId) {

    for (var i = 0; i < arraytosearch.length; i++) {

        if (arraytosearch[i][ASId] == valueASId) {
            if (arraytosearch[i][ICDId] == valuetICDId) {
                if (arraytosearch[i][PId] == valuePId) {
                    if (arraytosearch[i][PFId] == valuePFId) {
                        return i;
                    }
                }
            }
        }
    }
    return null;
};

function SaveCIE10Contraindications() {

    $("#bloqueo").show();

    $("#messageheaderComments").empty();
    $("#DivComments").empty();

    var Size = $(ListAddCIECNT).size();
    var DId = $("#DivisionId").val();
    var CId = $("#CategoryId").val();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddCIECNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveCIE10Contraindications",
            data: { List: Json, size: Size, Division: DId, Category: CId },
            success: function (data) {
                if (data.Data == true) {
                    setTimeout("document.location.reload()");
                }
                else if (data.Data == "_errorInd") {
                    $("#messageheaderComments").append("Error al asociar");
                    $("#DivComments").append("No se pudo realizar la asociación debido a que: <span style=\"font-weight:bold;font-style:italic\">" + data.Node + "</span> está indicado al medicamento en CIE-10</label>");
                    $("#btnComments").trigger("click");
                }
                else if (data.Data == "_errorIndParent") {
                    $("#messageheaderComments").append("Error al asociar");
                    $("#DivComments").append("<label>No se pudo realizar la asociación debido a que: " + data.Element + " Padre de: " + data.Node + " está indicado al medicamento en CIE-10</label>");
                    $("#btnComments").trigger("click");
                }
                else if (data.Data == "Error") {
                    $("#messageheaderComments").append("Error al asociar");
                    $("#DivComments").append("<label>Ha ocurrido un error, contacte al administrador proporcionando el siguiente error:<br /> <span style=\"font-weight:bold;font-style:italic;color:#c9302c\">" + data.Message + "</span></label>");
                    $("#btnComments").trigger("click");
                }
                else if (data.Data == "ExistElementsAsoc") {
                    $("#messageheaderComments").text("Error al asociar");

                    var Content = "";
                    Content += "<span> Ya existe asociacion entre elementos.</span><br /><br />";

                    $.each(data.Lists, function (index, val) {
                        Content += "<label style='cursor:pointer'>&nbsp;&nbsp; Sustancia: " + val.ActiveSubstance + " - Elemento: " + val.Element + "</label><br />";
                    });

                    $("#DivComments").append(Content);
                    $("#btnComments").trigger("click");

                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }

}


var ListAddPCCNT = [];

function CheckPhysiologicalContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = $(item).val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();
    var Element = tr.find("#spnPhysContraindicationName").text();

    CancelPhysiologicalContraindications();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckPhysiologicalContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, PharmacologicalGroup: value },
        success: function (data) {

            if (data.Data == true) {

                $("#messageheaderPCCNT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdPhysC\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdPhysC\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdPhysC\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdPhysC\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdPhysC\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <label style='font-weight:bold'>" + Element + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddPhysiologicalContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                });

                $("#DivPCCNT").append(Content);
                $("#btnPCCNT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}

function AddPhysiologicalContraindications(item) {

    var ASId = $("#MdlActiveSubstanceIdPhysC").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdPhysC").val();
    var CId = $("#MdlCategoryIdPhysC").val();
    var DId = $("#MdlDivisionIdPhysC").val();
    var PFId = $("#MdlPharmaFormIdPhysC").val();

    if ($(item).is(":checked")) {

        ListAddPCCNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(ListAddPCCNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(ListAddPCCNT, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddPCCNT.splice(index, 1);

            console.log(ListAddPCCNT);
        }
    }
}
function SavePhysiologicalContraindications() {

    $("#bloqueo").show();

    var Size = $(ListAddPCCNT).size();

    if ((Size != 0) && (Size != "0")) {
        $('#bloqueo').show();

        var Json = JSON.stringify(ListAddPCCNT);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SavePhysiologicalContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function CancelPhysiologicalContraindications() {

    $("#DivPCCNT").empty();

}

function DeletePhysiologicalContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ACTS = tr.find("#PCActiveSubstanceId").val();
    var Phys = tr.find("#PCPhysContraindicationId").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeletePhysiologicalContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, ActiveSubstance: ACTS, PhysContraindication: Phys },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            } else {
                $("#bloqueo").hide();
            }
        }
    })


}


var ListAddPharCCNT = [];

function CheckPharmacologicalContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = $(item).val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();
    var Element = tr.find("#spnPharmaContraindicationName").text();

    CancelPharmacologicalContraindications();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckPharmacologicalContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, Pharmacological: value },
        success: function (data) {

            if (data.Data == true) {

                $("#messageheaderPharCCNT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdPharCCNT\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdPharCCNT\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdPharCCNT\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdPharCCNT\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdPharCCNT\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <label style='font-weight:bold'>" + Element + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddPharmacologicalContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                });

                $("#DivPharCCNT").append(Content);
                $("#btnPharCCNT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}

function CancelPharmacologicalContraindications() {

    $("#DivPharCCNT").empty();

}

function AddPharmacologicalContraindications(item) {

    var ASId = $("#MdlActiveSubstanceIdPharCCNT").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdPharCCNT").val();
    var CId = $("#MdlCategoryIdPharCCNT").val();
    var DId = $("#MdlDivisionIdPharCCNT").val();
    var PFId = $("#MdlPharmaFormIdPharCCNT").val();

    if ($(item).is(":checked")) {

        ListAddPharCCNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(ListAddPharCCNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(ListAddPharCCNT, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddPharCCNT.splice(index, 1);

            console.log(ListAddPharCCNT);
        }
    }
}

function SavePharmacologicalContraindications() {

    $("#bloqueo").show();

    var Size = $(ListAddPharCCNT).size();

    if ((Size != 0) && (Size != "0")) {

        var Json = JSON.stringify(ListAddPharCCNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SavePharmacologicalContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function DeletePharmacologicalContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ACTS = tr.find("#PharmaCActiveSubstanceId").val();
    var Phys = tr.find("#PharmaCPharmaContraindicationId").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeletePharmacologicalContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, ActiveSubstance: ACTS, PhysContraindication: Phys },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            } else {
                $("#bloqueo").hide();
            }
        }
    })


}


var ListAddHyperCNT = [];

function CheckHyperContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = $(item).val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();
    var Element = tr.find("#spnPharmaContraindicationName").text();

    CancelHyperContraindications();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckHyperContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, Pharmacological: value },
        success: function (data) {

            if (data.Data == true) {

                $("#messageheaderHyperCNT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdHyperCNT\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdHyperCNT\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdHyperCNT\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdHyperCNT\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdHyperCNT\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <label style='font-weight:bold'>" + Element + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddHyperContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                });

                $("#DivHyperCNT").append(Content);
                $("#btnHyperCNT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}

function CancelHyperContraindications() {

    $("#DivHyperCNT").empty();

}

function SaveHyperContraindications() {

    $('#bloqueo').show();

    var Size = $(ListAddHyperCNT).size();

    if ((Size != 0) && (Size != "0")) {


        var Json = JSON.stringify(ListAddHyperCNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveHyperContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function AddHyperContraindications(item) {

    var ASId = $("#MdlActiveSubstanceIdHyperCNT").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdHyperCNT").val();
    var CId = $("#MdlCategoryIdHyperCNT").val();
    var DId = $("#MdlDivisionIdHyperCNT").val();
    var PFId = $("#MdlPharmaFormIdHyperCNT").val();

    if ($(item).is(":checked")) {

        ListAddHyperCNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(ListAddHyperCNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(ListAddHyperCNT, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddHyperCNT.splice(index, 1);

            console.log(ListAddHyperCNT);
        }
    }
}

function DeleteHyperContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ACTS = tr.find("#HyperActiveSubstanceId").val();
    var Phys = tr.find("#HyperHypersensibilityId").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteHyperContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, ActiveSubstance: ACTS, PhysContraindication: Phys },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            } else {
                $("#bloqueo").hide();
            }
        }
    })
}

function AddPharmaGroupContraindications() {

    $("#bloqueo").show();

    var size = $(JsonListTIPG).size();
    var jsonresponse = JSON.stringify(JsonListTIPG);

    if ((size != 0) || (size != "0")) {

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/AddProductPharmacologicalGroupsContraindications",
            data: { Size: size, SubstanceInteractions: jsonresponse },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";

                    d += "<label> Ya existe asociacion entre elementos.</label> <br/>"
                    d += "<br/>"
                    d += "<div>"
                    d += "<ul>"

                    $.each(data, function (index, val) {
                        d += "<li><span style=\"font-weight:bold\">" + val.Element + "</span> ya esta asociado a la sustancia <span style=\"font-weight:bold\">" + val.ActiveSubstance + "</span></li>";
                    });

                    d += "</ul>"
                    d += "</div>"
                    apprise("" + d + "", { 'animate': true });

                    JsonList = [];
                    JsonListTIPG = [];

                    $("#bloqueo").hide();

                }
            }
        })

    } else {

        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro.</label> <br/>"
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
}

function DeletePharmaGroupContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblPharmaGroupIdPGCNT").val();
    var value = tr.find("#lblActiveSubstanceIdPGCNT").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteProductPharmacologicalGroupsContraindications",
        data: { ActiveSubstance: value, SubstanceInteraction: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function CheckPharmacologicalGroupsContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = tr.find("#lblPharmaGroupId").val();
    var Group = tr.find("#lblGroupName").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelAddAPGNT();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckPharmacologicalGroupsContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, Pharmacological: value },
        success: function (data) {
            if (data.Data == true) {
                $("#messageheaderPGINT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdPG\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdPG\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdPG\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: <label style='font-weight:bold'>" + Group + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddPharmaGroupInteractionsTI($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    console.log(val.Description);

                });

                $("#DivPGINT").append(Content);
                $("#btnPGINT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}


var ListAddASCNT = [];

function CheckActiveSubstancesContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = tr.find("#lblActiveSubstanceIdCNT").val();
    var Group = tr.find("#spnDescriptionASCNT").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelActiveSubstancesContraindications();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckActiveSubstancesContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, Pharmacological: value },
        success: function (data) {
            if (data.Data == true) {
                $("#messageheaderASCNT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdAS\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdAS\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdAS\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdAS\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdAS\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: <label style='font-weight:bold'>" + Group + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddActiveSubstancesContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    console.log(val.Description);

                });

                $("#DivASCNT").append(Content);
                $("#btnASCNT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}

function CancelActiveSubstancesContraindications() {

    $("#DivASCNT").empty();

}

function AddActiveSubstancesContraindications(item) {

    var ASId = $("#MdlActiveSubstanceIdAS").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdAS").val();
    var CId = $("#MdlCategoryIdAS").val();
    var DId = $("#MdlDivisionIdAS").val();
    var PFId = $("#MdlPharmaFormIdAS").val();

    if ($(item).is(":checked")) {

        ListAddASCNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(ListAddASCNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(ListAddASCNT, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddASCNT.splice(index, 1);

            console.log(ListAddASCNT);
        }
    }
}

function SaveActiveSubstancesContraindications() {

    $('#bloqueo').show();

    var Size = $(ListAddASCNT).size();

    if ((Size != 0) && (Size != "0")) {

        var Json = JSON.stringify(ListAddASCNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveActiveSubstancesContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function DeleteActiveSubstancesContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#lblSubsContraindicationIdASCNT").val();
    var value = tr.find("#lblActiveSubstanceIdASCNT").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteProductActiveSubstancesContraindications",
        data: { ActiveSubstance: value, SubstanceInteraction: ASId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}


var ListAddOECNT = [];

function CheckOtherElementsContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var value = tr.find("#lblElementId").val();
    var Group = tr.find("#lblElementName").text()
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    CancelActiveSubstancesContraindications();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/CheckOtherElementsContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, Element: value },
        success: function (data) {
            if (data.Data == true) {
                $("#messageheaderOECNT").text("Aviso");

                var Content = "";
                Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdOECNT\" />";
                Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdOECNT\"/>";
                Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdOECNT\"/>";
                Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdOECNT\"/>";
                Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdOECNT\"/></div>";
                Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: <label style='font-weight:bold'>" + Group + "</label></span><br /><br />";

                $.each(data.Items, function (index, val) {
                    Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddOtherElementsContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    console.log(val.Description);

                });

                $("#DivOECNT").append(Content);
                $("#btnOECNT").trigger("click");

                $("#bloqueo").hide();
            }
            else if (data.Data == false) {
                setTimeout("document.location.reload()");
            }
            else if (data.Data == "_notdata") {
                var d = "";
                d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    })
}

function CancelOtherElementsContraindications() {

    $("#DivOECNT").empty();

}

function AddOtherElementsContraindications(item) {

    var ASId = $("#MdlActiveSubstanceIdOECNT").val();
    var value = $(item).val();
    var PId = $("#MdlProductIdOECNT").val();
    var CId = $("#MdlCategoryIdOECNT").val();
    var DId = $("#MdlDivisionIdOECNT").val();
    var PFId = $("#MdlPharmaFormIdOECNT").val();

    if ($(item).is(":checked")) {

        ListAddOECNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "SubstanceInteract": value,
                        "ActiveSubstance": ASId
                    });

        console.log(ListAddOECNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTI(ListAddOECNT, "Division", "Category", "PharmaForm", "Product", "SubstanceInteract", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddOECNT.splice(index, 1);

            console.log(ListAddOECNT);
        }
    }
}

function SaveOtherElementsContraindications() {

    $('#bloqueo').show();

    var Size = $(ListAddOECNT).size();

    if ((Size != 0) && (Size != "0")) {


        var Json = JSON.stringify(ListAddOECNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveOtherElementsContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
}

function DeleteOtherElementsContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var OEId = tr.find("#lblElementIdOECNT").val();
    var value = tr.find("#lblActiveSubstanceIdOECNT").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteOtherElementsContraindications",
        data: { ActiveSubstance: value, Element: OEId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function GetAccordionId(item) {

    var Id = $(item).attr("Id");

    sessionStorage.GetAccordionId = Id;

}

function LoadAccordionId() {

    var Id = sessionStorage.GetAccordionId;

    if ((Id != null) && (Id != undefined)) {

        Id = Id.replace("cnt_", "");

        var elem = document.getElementById(Id);

        $(elem).addClass("in");
    }
}


/*      ADD COMMENTS        */

function CancelAddComment() {

    $("#txtAddComment").val("");

}

function CheckCommentsContraindications() {

    $("#bloqueo").show();



    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();
    var Element = $("#txtAddComment").val();

    CancelCommentsContraindications();

    if (!Element.trim() == true) {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> El campo no puede quedar vac&iacute;o.</label> <br/>"

        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
    else {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/CheckCommentsContraindications",
            data: { Product: PId },
            success: function (data) {

                console.log(data);

                if (data.Data == true) {

                    $("#messageheaderCMTCNT").text("Aviso");

                    var Content = "";
                    Content += "<div style=\"display:none\"><input type=\"text\" value=\"" + PId + "\" id=\"MdlProductIdCMTCNT\" />";
                    Content += "<input type=\"text\" value=\"" + CId + "\" id=\"MdlCategoryIdCMTCNT\"/>";
                    Content += "<input type=\"text\" value=\"" + DId + "\" id=\"MdlDivisionIdCMTCNT\"/>";
                    Content += "<input type=\"text\" value=\"" + PFId + "\" id=\"MdlPharmaFormIdCMTCNT\"/></div>";
                    //Content += "<input type=\"text\" value=\"" + value + "\" id=\"MdlActiveSubstanceIdCMTCNT\"/>";
                    Content += "<span>El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con el Elemento: <label style='font-weight:bold'>" + Element + "</label></span><br /><br />";

                    $.each(data.Items, function (index, val) {
                        Content += "<label style='cursor:pointer'><input type='checkbox' value=\"" + val.ActiveSubstanceId + "\" onclick=\"AddCommentsContraindications($(this))\" />&nbsp;&nbsp;" + val.Description + "</label><br />";
                    });

                    $("#DivCMTCNT").append(Content);
                    $("#btnCMTCNT").trigger("click");

                    $("#bloqueo").hide();
                }
                else if (data.Data == false) {
                    setTimeout("document.location.reload()");
                }
                else if (data.Data == "_notdata") {
                    var d = "";
                    d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                    d += "<label> El registro seleccionado ya ha sido asociado a la(s) sustancia(s) del Producto.</label> <br/>"

                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
}

function CancelCommentsContraindications() {

    $("#DivCMTCNT").empty();

}

var ListAddCMTCNT = [];

function AddCommentsContraindications(item) {

    var value = $("#txtAddComment").val();
    var ASId = $(item).val();
    var PId = $("#MdlProductIdCMTCNT").val();
    var CId = $("#MdlCategoryIdCMTCNT").val();
    var DId = $("#MdlDivisionIdCMTCNT").val();
    var PFId = $("#MdlPharmaFormIdCMTCNT").val();

    if ($(item).is(":checked")) {

        ListAddCMTCNT.push(
                    {
                        "Division": DId,
                        "Category": CId,
                        "PharmaForm": PFId,
                        "Product": PId,
                        "ActiveSubstance": ASId,
                        "Comment": value
                    });

        console.log(ListAddCMTCNT);
    }
    else if ($(item).is(":not(:checked)")) {

        var index = RemoveItemOfJsonListTIComments(ListAddCMTCNT, "Division", "Category", "PharmaForm", "Product", "Comment", "ActiveSubstance", DId, CId, PFId, PId, value, ASId);

        if (index == null) {
        }
        else if (index >= 0) {

            ListAddCMTCNT.splice(index, 1);

            console.log(ListAddCMTCNT);
        }
    }
}

function RemoveItemOfJsonListTIComments(arraytosearch, Division, Category, PharmaForm, Product, Comment, ActiveSubstance, DId, CId, PFId, PId, value, ASId) {
    for (var i = 0; i < arraytosearch.length; i++) {
        if (arraytosearch[i][Division] == DId) {
            if (arraytosearch[i][Category] == CId) {
                if (arraytosearch[i][PharmaForm] == PFId) {
                    if (arraytosearch[i][Product] == PId) {
                        if (arraytosearch[i][Comment] == value) {
                            if (arraytosearch[i][ActiveSubstance] == ASId) {
                                return i;
                            }
                        }
                    }
                }
            }
        }
    }
    return null;
};

function SaveCommentsContraindications() {

    $('#bloqueo').show();

    var Size = $(ListAddCMTCNT).size();

    if ((Size != 0) && (Size != "0")) {

        var Json = JSON.stringify(ListAddCMTCNT);

        console.log(Json);

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Medical/SaveCommentsContraindications",
            data: { List: Json, size: Size },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {

                    var s = $(data).size();

                    if (s != Size) {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";
                        apprise("" + d + "", { 'animate': true }, function (r) {
                            if (r) {
                                setTimeout("document.location.reload()");
                            }
                            else {
                                alert("error");
                            }
                        })
                        $("#bloqueo").hide();
                    }
                    else {
                        var d = "";
                        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
                        d += "<label> Los siguientes registros, ya estan asociados al Producto / Forma Farmac&eacute;utica</label> <br/>"
                        d += "<br/>";
                        d += "<ul style='list-style:none'>";
                        $.each(data, function (index, val) {
                            d += "<li><span>&bull;&nbsp;<b>" + val + "</b></span></li>";
                        });
                        d += "</ul>";

                        apprise("" + d + "", { 'animate': true });
                        $("#bloqueo").hide();
                    }
                }
            }
        })
    }
    else {
        var d = "";
        d += "<div class='text-center'><h1 style='color: #337ab7;'><span class='glyphicon glyphicon-warning-sign'></span> AVISO</h1></div> <br>";
        d += "<label> No se ha seleccionado ningun registro para asociar.</label> <br/>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }

}

function DeleteCommentsContraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var SId = tr.find("#lblActiveSubstanceIdCMTCNT").val();
    var PCId = tr.find("#lblProductCommentIdCMTCNT").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteCommentsContraindications",
        data: { ActiveSubstance: SId, ProductComment: PCId, Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}


function DeleteAllCommentsContraindications() {

    $("#bloqueo").show();

    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteAllCommentsContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function DeleteCIE10Contraindications(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var ASId = tr.find("#spnActiveSubstanceId").val();
    var ICDId = tr.find("#lblICDId").val();
    var PId = $("#ProductId").val();
    var CId = $("#CategoryId").val();
    var DId = $("#DivisionId").val();
    var PFId = $("#PharmaFormId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Medical/DeleteICDContraindications",
        data: { Product: PId, Category: CId, Division: DId, PharmaForm: PFId, ActiveSubstance: ASId, ICD: ICDId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            } else {
                $("#bloqueo").hide();
            }
        }
    })
}