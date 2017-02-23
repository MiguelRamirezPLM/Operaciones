    $(document).ready(function () {
        $(".Cliente_Participante").click(function () {
            var _Client_Participant = $("#_Client_Participant").val();
            var _Edition_Client_Participant = $("#_Edition_Client_Participant").val();
            var _Client_Type_Participant = $("#_Client_Type_Participant").val();
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Ventas/AgregarClienteEdición",
                traditional: true,
                data: { EditionId: _Edition_Client_Participant, CompanyId: _Client_Participant, CompanyTypeId: _Client_Type_Participant },
                success: function (data) {
                    if (data == true) {
                        swal("¡Cliente agregado a la edición!", "", "success");
                        setTimeout(function () { location.reload(1); }, 300);
                    }
                    else {
                        swal({
                            title: "¡Atención!",
                            text: "¡El cliente aún no puede participar en esta edición, ya que el cliente del que es dependiente aún no participa!",
                            type: "warning",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Entendido",
                            closeOnConfirm: false,
                        },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal("Entendido", "", "success");
                                setTimeout(function () { location.reload(1); }, 1);
                            }
                        });
                    }
                }
            });
        });
    });
    $(document).ready(function () {
        $(".dropdown").hover(
            function () {
                $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideDown("400");
                $(this).toggleClass('open');
            },
            function () {
                $('.dropdown-menu', this).not('.in .dropdown-menu').stop(true, true).slideUp("400");
                $(this).toggleClass('open');
            }
        );
    });
    $(document).ready(function () {
        $("._CambiosGuardados").click(function () {
            swal("¡Marca desasociada de la sección!", "", "success")
        });
    });
    $(document).ready(function () {
        $("._CambiosGuardados2").click(function () {
            swal("¡Marca desasociada del índice!", "", "success")
        });
    });
    $(document).ready(function () {
        $("#EnlaceAjaxSecciones").click(function (evento) {
            $("#DestinoSecciones").load("/Ventas/AgregarSeccionesMarca", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#EnlaceAjaxÍndices").click(function (evento) {
            $("#DestinoÍndices").load("/Ventas/AgregarÍndicesMarca", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajax").click(function (evento) {
            $("#destino").load("/Ventas/AgregarNuevaMarcaEdición", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajax2").click(function (evento) {
            $("#destino2").load("/Ventas/EditarMarca", function () {
            });
        });
    })
    $(document).ready(function () {
        $(".Marca_Participante").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Marca_Participante").is(':checked')) {
                var _BrandId = $(this).attr("Id");
                var _CompanyId = $("#_CompanyId").val();
                var _EditionId = $("#_EditionId").val();
                tr.find("#BrandId").text(_BrandId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/InsertarMarcaParticipante",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId, BrandId: _BrandId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cambios guardados!", "", "success");
                            setTimeout(function () { location.reload(1); }, 1);
                        }
                        else
                        {
                            swal({
                                title: "¡Atención!",
                                text: "¡La marca no puede participar en ésta edición, dado que él cliente aún no participa en ella!",
                                type: "warning",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Aceptar",
                                closeOnConfirm: false,
                            }, function () {
                                setTimeout(function () { location.reload(1); }, 1);
                            })
                        }
                    }
                });
            }
            else if (tr.find(".Marca_Participante").is(":not(:checked)")) {
                var _BrandId = $(this).attr("Id");
                var _CompanyId = $("#_CompanyId").val();
                var _EditionId = $("#_EditionId").val();
                swal({
                    title: "¿Desea desasociar la marca de la edición?",
                    text: "¡Si la marca sólo participa en una edición ésta se desasociará del cliente, de la sección e índice dónde sea participante, de lo contrario sólo dejará de participar en la edición!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Cancelar",
                    cancelButtonText: "Desasociar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                function (isConfirm) {
                    if (isConfirm == false) {
                        tr.find("#BrandId").text(_BrandId);
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "/Ventas/EliminarMarcaParticipante",
                            traditional: true,
                            data: { EditionId: _EditionId, CompanyId: _CompanyId, BrandId: _BrandId },
                            success: function (data) {
                                if (data == true) {
                                    swal("¡Cambios guardados!", "", "success");
                                    setTimeout(function () { location.reload(1); }, 1);
                                }
                                else {
                                    setTimeout(function () { location.reload(1); }, 1);
                                }
                            }
                        });;
                    }
                    else {
                        swal("Cancelado", "", "error");
                        setTimeout(function () { location.reload(1); }, 1);
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $("#_AgregarMarca").click(function () {
            var _BrandNameAdd = $("#_BrandNameAdd").val();
            var _CompanyId = $("#_CompanyId").val();
            var _EditionIdAddBrand = $("#_EditionIdAddBrand").val();
            if (_BrandNameAdd.trim() != "") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarNuevaMarca",
                    traditional: true,
                    data: { _CompanyId: _CompanyId, _BrandName: _BrandNameAdd, _EditionId: _EditionIdAddBrand },
                    success: function (data) {
                        if (data == false) {
                            swal("¡Atención!", "¡La marca ya existe!", "error");
                        }
                        else {
                            swal("¡Marca agregada!", "", "success");
                            setTimeout(function () { location.reload(1); }, 800);
                        }
                    }
                });
            }
            else if (_BrandNameAdd.trim() == "") {
                swal("¡Atención!", "¡El campo 'Nombre de la marca' no puede quedar vacio!", "error");
            }
        });
    });
    $(document).ready(function () {
        $("#_AsociarMarcaCliente").click(function () {
            var _AsBrand = $("#_BrandId").val();
            var _CompanyId = $("#_CompanyId").val();
            var _AsEditionId = $("#_AsEditionId").val();
            if (_AsBrand != "Seleccione...") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarMarcaCliente",
                    traditional: true,
                    data: { CompanyId: _CompanyId, BrandId: _AsBrand, EditionId: _AsEditionId },
                    success: function (data) {
                        if (data == false) {
                            swal("¡Atención!", "¡La marca seleccionada ya esta asociada al cliente!", "error");
                        }
                        else {
                            swal("¡Marca asociada al cliente!", "", "success");
                            setTimeout(function () { location.reload(1); }, 800);
                        }
                    }
                });
            }
            else if (_AsBrand == "Seleccione...") {
                swal("¡Atención!", "¡El campo 'Nombre de la marca' no puede quedar vacio!", "error");
            }
        });
    });
    $(document).ready(function () {
        $(".GuardarCambios").click(function () {
            swal("¡Marca desasociada!", "", "success");
        });
    });
                      function SaveBrands(a) {
        var valInput = a;
        var txtInput = $("#" + a).val();
        if (txtInput != false) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Ventas/ModificarMarca",
                traditional: true,
                data: {BrandId: valInput, BrandName: txtInput },
                success: function (data) {
                    if (data == true) {
                        swal("¡Cambios guardados!", "", "success");
                    }
                    $("#" + a).prop("disabled", true);
                }
            });
        }
        else if (txtInput.trim() == false) {
            swal("¡Atención!", "¡El campo modificado no puede quedar vacio!", "error");
        }
    }
                      function enableInput(a) {
    $("#" + a).attr("disabled", false);
    $("#" + a).focus();
}
    $(document).ready(function () {
        $('#Caracter').keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaClientes').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(document).ready(function () {
        $("#CaracterEdición").keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaEdiciones').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(function () {
        $(".eliminando").click(function () {
            $("#cargandoeliminando").show();
        });
        $("#cargandoeliminando").hide();
    });
    $(function () {
        $(".carga").click(function () {
            $("#cargando").show();
        });
        $("#cargando").hide();
    });
        $(function () {
            $(".Tiempo").click(function () {
                $("#cargandoTiempo").show();
            });
            $("#cargandoTiempo").hide();
        });
    