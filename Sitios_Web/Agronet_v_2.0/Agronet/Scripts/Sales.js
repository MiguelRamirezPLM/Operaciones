function OpenFormAddProduct() {

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/GetPharmaForms",
        success: function (data) {
            $('#PharmaForms').empty();
            $('#PharmaForms')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#PharmaForms')
                .append($("<option></option>")
                .attr("value", val.PharmaFormId)
                .text(val.PharmaForm));
            });

        }
    })

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/GetCategories",
        success: function (data) {
            $('#Categories').empty();
            $('#Categories')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Categories')
                .append($("<option></option>")
                .attr("value", val.CategoryId)
                .text(val.CategoryName));
            });
        }
    })

    $("#btnAddProduct").trigger("click");

}

function SaveProduct() {

    $("#bloqueo").show();

    var pn = $("#txtProductName").val();
    var pf = $("#PharmaForms").val();
    var ct = $("#Categories").val();
    var PP = false;
    var MP = false;
    var DId = $("#DivisionId").val();
    var EId = $("#EditionId").val();
    var CId = $("#CountryId").val();
    var RS = $("#txtRegister").val();
    var PD = $("#txtDescription").val();
    var PId = $("#txtProductIdAC").val();

    if ($("#checkPPAddProduct").is(":checked")) {
        PP = true;
        console.log("PP " + PP);
    }

    if ($("#checkMPAddProduct").is(":checked")) {
        MP = true;

        console.log("MP " + MP);
    }

    if ((!pn.trim() == false) && (pf != 0) && (pf != null) && (pf != "0") && (pf != undefined) && (ct != 0) && (ct != null) && (ct != "0") && (ct != undefined)) {

        $("#bloqueo").show();

        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/AddProduct",
            data: {
                Product: pn,
                PharmaForm: pf,
                Category: ct,
                Participant: PP,
                Mentionated: MP,
                Division: DId,
                Edition: EId,
                Country: CId,
                Register: RS,
                Description: PD,
                ProductI: PId
            },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
                else {
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else {
        if (!pn.trim() == true) {
            $('#divProductName').addClass('has-error');
            $('.errorP').show();
            $("#bloqueo").hide();
        }

        if ((pf == 0) || (pf == null) || (pf == "0") || (pf == undefined)) {
            $('#divPharmaForm').addClass('has-error');
            $('.errorPF').show();
            $("#bloqueo").hide();
        }

        if ((ct == 0) || (ct == null) || (ct == "0") || (ct == undefined)) {
            $('#divCategories').addClass('has-error');
            $('.errorC').show();
            $("#bloqueo").hide();
        }
    }
}

function checkPPAddProduct(item) {

    if ($("#checkMPAddProduct").is(":checked")) {
        $("#checkMPAddProduct").prop("checked", false);
    }
}

function checkMPAddProduct(item) {

    if ($("#checkPPAddProduct").is(":checked")) {
        $("#checkPPAddProduct").prop("checked", false);
    }
}

function CancelSaveProduct() {
    $("#txtProductName").val('');
    $("#txtRegister").val('');
    $("#txtDescription").val('');

    $('#divProductName').removeClass('has-error');
    $('.errorP').hide();

    $('#divPharmaForm').removeClass('has-error');
    $('.errorPF').hide();

    $('#divCategories').removeClass('has-error');
    $('.errorC').hide();
}

function OpenFormAddPharmaForm() {

    var DId = $("#DivisionId").val();
    var EId = $("#EditionId").val();
    var CId = $("#CountryId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/GetProductsToAddPharmaForm",
        data: { CountryId: CId, EditionId: EId, DivisionId: DId },
        success: function (data) {
            $('#AddProducts').empty();
            $('#AddProducts')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#AddProducts')
                .append($("<option></option>")
                .attr("value", val.ProductId)
                .text(val.ProductName));
            });

        }
    })
    $("#btnAddPharmaForm").trigger("click");

}

function ShowPharmaFormtoAdd(item) {

    var PId = $(item).val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/GetPharmaFormToAdd",
        data: { Product: PId },
        success: function (data) {
            $('#AddPharmaForms').empty();
            $('#AddPharmaForms')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#AddPharmaForms')
                .append($("<option></option>")
                .attr("value", val.PharmaFormId)
                .text(val.PharmaForm));
            });
        }
    })
}

function ShowCategoriestoAdd(item) {

    var PFId = $(item).val();
    var PId = $("#AddProducts").val();
    var DId = $("#DivisionId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/GetCategoriesToAdd",
        data: { Product: PFId, PharmaForm: PFId, Division: DId },
        success: function (data) {
            $('#AddCategories').empty();
            $('#AddCategories')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#AddCategories')
                .append($("<option></option>")
                .attr("value", val.CategoryId)
                .text(val.CategoryName));
            });
        }
    })
}

function AddPharmaForm() {

    var PId = $("#AddProducts").val();
    var PFId = $("#AddPharmaForms").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();
    var CId = $("#AddCategories").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/AddPharmaForm",
        data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })
}

function CancelAddPharmaForm() {
    $('#AddPharmaForms').empty();
    $('#AddCategories').empty();

    $('#btnAddPharmaFormfrm').hide();
}

function SIDEF(item) {

    var tr = $(item).parents("tr:first");

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    var PId = tr.find("#lblProductId").val();
    var PFId = tr.find("#lblPharmaFormId").val();
    var CId = tr.find("#lblCategoryId").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/SIDEF",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Insert" },
            success: function (data) {
                if (data == false) {
                    $(item).prop("checked", false);
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto no esta marcado como Participante!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else if ($(item).is(":not(:checked)")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/SIDEF",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Delete" },
            success: function (data) {
                setTimeout("document.location.reload()");
            }
        })
    }
}

function Participant(item) {

    var tr = $(item).parents("tr:first");

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    var PId = tr.find("#lblProductId").val();
    var PFId = tr.find("#lblPharmaFormId").val();
    var CId = tr.find("#lblCategoryId").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/Participant",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Insert" },
            success: function (data) {
                if (data == false) {
                    $(item).prop("checked", false);
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto no esta marcado como Participante!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
                else if (data == true) {
                    tr.find("#Mentionated").prop("checked", false);
                }
            }
        })
    }
    else if ($(item).is(":not(:checked)")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/Participant",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Delete" },
            success: function (data) {
                if (data == true) {
                    tr.find("#Mentionated").prop("checked", false);
                    tr.find("#SIDEF").prop("checked", false);
                    tr.find("#ProductChanges").prop("checked", false);
                    tr.find("#NewProducts").prop("checked", false);
                }
            }
        })
    }
}

function Mentionated(item) {

    var tr = $(item).parents("tr:first");

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    var PId = tr.find("#lblProductId").val();
    var PFId = tr.find("#lblPharmaFormId").val();
    var CId = tr.find("#lblCategoryId").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/MentionatedProducts",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Insert" },
            success: function (data) {
                if (data == false) {
                    $(item).prop("checked", false);
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto no esta marcado como Participante!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
                else if (data == true) {
                    tr.find("#Participant").prop("checked", false);
                    tr.find("#ProductChanges").prop("checked", false);
                    tr.find("#SIDEF").prop("checked", false);
                }
            }
        })
    }
    else if ($(item).is(":not(:checked)")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/MentionatedProducts",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Delete" },
            success: function (data) {
            }
        })
    }
}

function ProductChanges(item) {

    var tr = $(item).parents("tr:first");

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    var PId = tr.find("#lblProductId").val();
    var PFId = tr.find("#lblPharmaFormId").val();
    var CId = tr.find("#lblCategoryId").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();

    if ($(item).is(":checked")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/ProductChanges",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Insert" },
            success: function (data) {
                if (data == false) {
                    $(item).prop("checked", false);
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto es Nuevo, no puede tener cambios!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
                else if (data == "_errorPP") {
                    $(item).prop("checked", false);
                    d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
                    d += "<p></p>"
                    d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Producto no esta marcado como Participante!!!</p>"
                    apprise("" + d + "", { 'animate': true });
                    $("#bloqueo").hide();
                }
            }
        })
    }
    else if ($(item).is(":not(:checked)")) {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/ProductChanges",
            data: { Product: PId, PharmaForm: PFId, Edition: EId, Division: DId, Category: CId, Operation: "Delete" },
            success: function (data) {
            }
        })
    }
}


/***********          PRODUCTS        **************/


function ShowEditForms(item) {

    var tr = $(item).parents("tr:first");

    tr.find(".show-data").hide();
    tr.find(".edit-data").show();

}

function CancelEditForms(item) {

    var tr = $(item).parents("tr:first");

    tr.find(".edit-data").hide();
    tr.find(".show-data").show();

}

function SaveEditProduct(item) {

    var tr = $(item).parents("tr:first");

    var PId = tr.find("#lblProductId").val();
    var PName = tr.find("#trtxtProductName").val();
    var PDesc = tr.find("#trtxtDescription").val();
    var PReg = tr.find("#trtxtRegister").val();
    var DId = $("#DivisionId").val();
    var CId = $("#CountryId").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if (!PName.trim() == true) {

        d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
        d += "<p></p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; El Nombre de Producto no puede quedar vac&iacute;o!!!</p>"
        apprise("" + d + "", { 'animate': true });
        $("#bloqueo").hide();
    }
    else {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Products/EditProducts",
            data: { Product: PId, ProductName: PName, Description: PDesc, Register: PReg, Division: DId, Country: CId },
            success: function (data) {
                if (data == true) {
                    tr.find("#lblProductName").text(PName);
                    tr.find("#lblDescription").text(PDesc);
                    tr.find("#lblRegister").text(PReg);

                    tr.find(".edit-data").hide();
                    tr.find(".show-data").show();
                }
            }
        })
    }
}

function rowsperpage(item) {

    $("#bloqueo").show();

    var vl = $(item).val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Products/RowsPerPage",
        data: { Value: vl },
        success: function (data) {
            setTimeout("document.location.reload()");
        }
    })

}

function SearchProductSM() {

    var ProductName = $("#SearchProduct").val();

    window.location.href = '../Products/Index?ProductName=' + ProductName + ''

}

function AutocompleteProducts(items) {

    var Country = $("#CountryId").val();

    $(items).autocomplete({
        appendTo: $(items).parent(),
        source: function (request, response) {
            $.ajax({
                url: "../Products/Search",
                type: "POST",
                dataType: "Json",
                data: { term: request.term, country: Country },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.String, value: item.String };

                    }))
                }
            })
        },

        select: function (event, ui) {

            $("#txtProductIdAC").val(ui.item.id);

        },
        minLength: 3
    });


}




/**********         DIVISIONS       ***************/

function ShowEditDivs() {

    $(".show-dataD").hide();
    $(".edit-dataD").show();

}

function CancelEditDivs() {

    $(".edit-dataD").hide();
    $(".show-dataD").show();

}

function SaveEditDivs() {

    var Dname = $("#txtDivisionName").val();
    var Sname = $("#txtShortName").val();
    var DId = $("#lblDivisionId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Laboratories/EditDivisions",
        data: { Division: DId, DivisionName: Dname, ShortName: Sname },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
        }
    })

}



/**********         ADRESSES       ***************/

function ShowEditAddress(item) {

    var tr = $(item).parents("tr:first");

    tr.find(".show-dataA").hide();
    tr.find(".edit-dataA").show();

    sessionStorage.a = tr.find("#lblAddress").text();
    sessionStorage.s = tr.find("#lblSuburb").text();
    sessionStorage.l = tr.find("#lblLocation").text();
    sessionStorage.z = tr.find("#lblZipCode").text();
    sessionStorage.t = tr.find("#lblTelephone").text();
    sessionStorage.la = tr.find("#lblLada").text();
    sessionStorage.f = tr.find("#lblFax").text();
    sessionStorage.w = tr.find("#lblWeb").text();
    sessionStorage.c = tr.find("#lblCity").text();
    sessionStorage.st = tr.find("#lblState").text();
    sessionStorage.e = tr.find("#lblEmail").text();

}

function CancelEditAddress(item) {

    var tr = $(item).parents("tr:first");

    tr.find(".show-dataA").show();
    tr.find(".edit-dataA").hide();

    tr.find("#trtxtAddress").val(sessionStorage.a)
    tr.find("#trtxtSuburb").val(sessionStorage.s)
    tr.find("#trtxtLocation").val(sessionStorage.l)
    tr.find("#trtxtZipCode").val(sessionStorage.z)
    tr.find("#trtxtTelephone").val(sessionStorage.t)
    tr.find("#trtxtLada").val(sessionStorage.la)
    tr.find("#trtxtFax").val(sessionStorage.f)
    tr.find("#trtxtWeb").val(sessionStorage.w)
    tr.find("#trtxtCity").val(sessionStorage.c)
    tr.find("#trtxtState").val(sessionStorage.st)
    tr.find("#trtxtEmail").val(sessionStorage.e)

}

function SaveEditAddress(item) {

    var tr = $(item).parents("tr:first");

    var a = tr.find("#trtxtAddress").val();
    var s = tr.find("#trtxtSuburb").val();
    var l = tr.find("#trtxtLocation").val();
    var z = tr.find("#trtxtZipCode").val();
    var t = tr.find("#trtxtTelephone").val();
    var la = tr.find("#trtxtLada").val();
    var f = tr.find("#trtxtFax").val();
    var w = tr.find("#trtxtWeb").val();
    var c = tr.find("#trtxtCity").val();
    var st = tr.find("#trtxtState").val();
    var e = tr.find("#trtxtEmail").val();
    var DIId = tr.find("#lblDivisionInformationId").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if ((!a.trim() == true) &&
        (!s.trim() == true) &&
        (!l.trim() == true) &&
        (!z.trim() == true) &&
        (!t.trim() == true) &&
        (!la.trim() == true) &&
        (!f.trim() == true) &&
        (!w.trim() == true) &&
        (!c.trim() == true) &&
        (!st.trim() == true) &&
        (!e.trim() == true)) {
        d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
        d += "<p></p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; Ningun campo puede quedar vac&iacute;o!!!</p>"
        apprise("" + d + "", { 'animate': true });
    }
    else {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Laboratories/EditAddresses",
            data: { Address: a, Suburb: s, Location: l, ZipCode: z, Telephone: t, Lada: la, Fax: f, Web: w, City: c, State: st, Email: e, DivisionInformation: DIId },
            success: function (data) {
                if (data == true) {
                    tr.find("#lblAddress").text(a);
                    tr.find("#lblSuburb").text(s);
                    tr.find("#lblLocation").text(l);
                    tr.find("#lblZipCode").text(z);
                    tr.find("#lblTelephone").text(t);
                    tr.find("#lblLada").text(la);
                    tr.find("#lblFax").text(f);
                    tr.find("#lblWeb").text(w);
                    tr.find("#lblCity").text(c);
                    tr.find("#lblState").text(st);
                    tr.find("#lblEmail").text(e);

                    tr.find(".show-dataA").show();
                    tr.find(".edit-dataA").hide();
                }
            }
        })
    }


}

function AddDivisionInformation() {

    var a = $("#txtAddress").val();
    var s = $("#txtSuburb").val();
    var l = $("#txtLocation").val();
    var z = $("#txtZipCode").val();
    var t = $("#txtTelephone").val();
    var la = $("#txtLada").val();
    var f = $("#txtFax").val();
    var w = $("#txtWeb").val();
    var c = $("#txtCity").val();
    var st = $("#txtState").val();
    var e = $("#txtEmail").val();
    var DId = $("#lblDivisionId").val();


    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if ((!a.trim() == true) &&
        (!s.trim() == true) &&
        (!l.trim() == true) &&
        (!z.trim() == true) &&
        (!t.trim() == true) &&
        (!la.trim() == true) &&
        (!f.trim() == true) &&
        (!w.trim() == true) &&
        (!c.trim() == true) &&
        (!st.trim() == true) &&
        (!e.trim() == true)) {
        d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
        d += "<p></p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; Ningun campo puede quedar vac&iacute;o!!!</p>"
        apprise("" + d + "", { 'animate': true });
    }
    else {
        $.ajax({
            Type: "POST",
            dataType: "Json",
            url: "../Laboratories/AddAddress",
            data: { Address: a, Suburb: s, Location: l, ZipCode: z, Telephone: t, Lada: la, Fax: f, Web: w, City: c, State: st, Email: e, Division: DId },
            success: function (data) {
                if (data == true) {
                    setTimeout("document.location.reload()");
                }
            }
        })
    }
}





/********************************           CLASIFICATION           *********************************************/

function AddProductSubstances(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var ASId = tr.find("#ActiveSubstanceId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/AddProductSubstances",
        data: { Product: PId, ActiveSubstance: ASId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function DeleteProductSubstances(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var ASId = tr.find("#ActiveSubstanceIdAsc").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/DeleteProductSubstances",
        data: { Product: PId, ActiveSubstance: ASId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function AddProductCrops(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var CId = tr.find("#CropId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/AddProductCrops",
        data: { Product: PId, Crop: CId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function DeleteProductCrops(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var CId = tr.find("#CropIdAsc").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/DeleteProductCrops",
        data: { Product: PId, Crop: CId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })

}

function Show_List(item) {

    var Id = $(item).attr("Id");

    Id = Id.replace("S_", "");
    Id = Id.replace("H_", "");

    var ListId = "#List_" + Id;
    var S = "#S_" + Id;
    var H = "#H_" + Id;

    $(S).hide();
    $(H).show();
    $(ListId).fadeIn("slow");

}

function Hide_List(item) {

    var Id = $(item).attr("Id");

    Id = Id.replace("S_", "");
    Id = Id.replace("H_", "");

    var ListId = "#List_" + Id;
    var S = "#S_" + Id;
    var H = "#H_" + Id;

    $(H).hide();
    $(S).show();
    $(ListId).fadeOut("slow");

}

function AddProductAgrochemicalUses(item) {

    $("#bloqueo").show();

    var PId = $("#ProductId").val();
    var AUId = item.selector;

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/AddProductAgrochemicalUses",
        data: { Product: PId, AgrochemicalUse: AUId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function DeleteProductAgrochemicalUses(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var AUId = tr.find("#AgrochemicalUseIdAsc").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/DeleteProductAgrochemicalUses",
        data: { Product: PId, AgrochemicalUse: AUId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function AddProductSeeds(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var SId = tr.find("#SeedId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/AddProductSeeds",
        data: { Product: PId, Seed: SId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}

function DeleteProductSeeds(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PId = $("#ProductId").val();
    var SId = tr.find("#SeedIdAsc").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Clasification/DeleteProductSeeds",
        data: { Product: PId, Seed: SId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })

}

function DeleteAddresses(item) {

    $("#bloqueo").show();

    var Id = $(item).val();
    var DId = $("#DivisionId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Laboratories/DeleteAddresses",
        data: { Address: Id, Division: DId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })

}

function AddProductShot(item) {

    $('#txtFileName').val($(item).val())

}

function SaveProductShot() {

    $("#bloqueo").show();

    var SId = $("#SelectProductShotSize").val();
    var Image = $('#txtFileName').val();
    var PId = $("#ProductId").val();
    var PFId = $("#PharmaFormId").val();
    var CTId = $("#CategoryId").val();
    var EId = $("#EditionId").val();
    var DId = $("#DivisionId").val();
    var CId = $("#CountryId").val();

    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    if ((SId == 0) || (SId == "0") || (SId == undefined) || (SId == null)) {
        d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
        d += "<p></p>"
        d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; Debe seleccionar tama&ntilde;o de imagen!!!</p>"
        apprise("" + d + "", { 'animate': true });

        $("#bloqueo").hide();
    }
    else {
        if (!Image.trim() == true) {

            d += "<label style='width:300px;text-align:center;color:#05606d;font-style:italic;font-size:20px'>Error!!!</label>"
            d += "<p></p>"
            d += "<p style='width:300px;text-align:justify;color:#05606d;font-style:italic'>&bull; Debe seleccionar una Imagen!!!</p>"
            apprise("" + d + "", { 'animate': true });

            $("#bloqueo").hide();
        }
        else {
            $("#SendProductShot").ajaxSubmit({
                type: "POST",
                url: "../Production/SaveProductShot",
                data: { Size: SId, Product: PId, PharmaForm: PFId, Category: CTId, Division: DId, Edition: EId, Country: CId },
                success: function (data) {
                    if (data == true) {
                        setTimeout('document.location.reload()');
                    }
                }
            })
        }
    }
}

function CanceladdProductShot() {
    $('#txtFileName').val('');
    var elm = document.getElementById("val_0");

    $("#SelectProductShotSize").val($(elm).val());
}

function RemoveProductShots(item) {

    $("#bloqueo").show();

    var tr = $(item).parents("tr:first");

    var PIId = tr.find("#lblProductImageId").val();
    var IMId = tr.find("#lblImageSizeId").val();

    $.ajax({
        Type: "POST",
        dataType: "Json",
        url: "../Production/RemoveProductShots",
        data: { ProductImage: PIId, Size: IMId },
        success: function (data) {
            if (data == true) {
                setTimeout("document.location.reload()");
            }
            else {
                $("#bloqueo").hide();
            }
        }
    })
}