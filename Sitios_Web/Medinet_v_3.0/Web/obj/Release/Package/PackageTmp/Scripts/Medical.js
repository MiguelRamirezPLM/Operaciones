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