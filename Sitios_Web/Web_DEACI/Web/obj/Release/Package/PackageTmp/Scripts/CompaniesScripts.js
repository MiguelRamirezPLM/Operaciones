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
        $("._EstadoSelect").change(function () {
            var _StateId = $("#_StateId").val();
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Ventas/ObtenerCiudades",
                traditional: true,
                data: { id: _StateId },
                success: function (data) {
                    $("#_CityIdEdit").empty();
                    $("#_CityIdEdit")
                       .append($("<option></option>")
                       .attr("value", 0)
                       .text("Seleccione..."));
                    $.each(data, function (index, val) {
                        $("#_CityIdEdit")
                        .append($("<option></option>")
                        .attr("value", val.CityId)
                        .text(val.Name));
                    });
                }
            })
        });
    });
    $(document).ready(function () {
        $(".Confirmación").click(function () {
            var _EditionIdClose = $("#_EditionIdClose").val();
            var _CompanyIdClose = $("#_CompanyIdClose").val();
            var _UserIdClose = $("#_UserIdClose").val();
            swal({
                title: "¿Desea cerrar el ingreso de datos?",
                text: "¡Si cierra el ingreso de datos ya no podra realizar cambios! Esto se podra rehabilitar bajo la consideración del usuario 'Administrador'!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Cancelar",
                showLoaderOnConfirm: "Cancelar",
                cancelButtonText: "Cerrar ingreso de datos",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function (isConfirm) {
                if (isConfirm == false) {
                    $.ajax({
                        async : true,
                        type: "POST",
                        dataType: "json",
                        url: "/Ventas/CierreDatos",
                        traditional: true,
                        data: { EditionId: _EditionIdClose, CompanyId: _CompanyIdClose, UserId: _UserIdClose },
                        success: function (data) {
                            if (data == true) {
                                swal("¡Cliente cerrado!", "", "success");
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
        });
    });
    $(document).ready(function () {
        $(".ConfirmaciónOpen").click(function () {
            var _EditionIdClose = $("#_EditionIdClose").val();
            var _CompanyIdClose = $("#_CompanyIdClose").val();
            var _UserIdClose = $("#_UserIdClose").val();
            swal({
                title: "¿Desea abrir el ingreso de datos?",
                text: "¡Si abre el ingreso de datos se podran realizar nuevas modificaciones!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Cancelar",
                showLoaderOnConfirm: "Cancelar",
                cancelButtonText: "Abrir",
                closeOnConfirm: false,
                closeOnCancel: false
            },
            function (isConfirm) {
                if (isConfirm == false) {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/Ventas/AperturaDatos",
                        traditional: true,
                        data: { EditionId: _EditionIdClose, CompanyId: _CompanyIdClose, UserId: _UserIdClose },
                        success: function (data) {
                            if (data == true) {
                                swal("¡Cliente abierto!", "", "success");
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
        });
    });
    $(document).ready(function () {
        $("._AgregarAnuncioExistente").click(function () {
            var _CompanyIdExist = $("#_CompanyIdExist").val();
            var _AdvIdExist = $("#_AdvIdExist").val();
            var _EditionIdExist = $("#_EditionIdExist").val();
            if (_AdvIdExist == "Seleccione...") {
                swal("¡Atención!", "¡El campo 'Nombre del anuncio' no puede quedar vacio'!", "error");
            }
            else {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarAnuncioExistente",
                    traditional: true,
                    data: { CompanyId: _CompanyIdExist, AdvId: _AdvIdExist, EditionId: _EditionIdExist },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Anuncio asociado al cliente!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal("¡Atención!", "¡El anuncio seleccionado ya esta asociado al cliente!", "error");
                        }
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $("._AgregarAnuncio").click(function () {
            var _CompanyIdAdver = $("#_CompanyIdAdver").val();
            var _HiredSpace = $("#_HiredSpace").val();
            var _EditionIdAdver = $("#_EditionIdAdver").val();
            if (_HiredSpace.trim() == false) {
                swal("¡Atención!", "¡El campo 'Nombre del espacio contratado' no puede quedar vacio'!", "error");
            }
            else if (_HiredSpace.trim() != false) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarAnuncio",
                    traditional: true,
                    data: { CompanyId: _CompanyIdAdver, HiredSpace: _HiredSpace, EditionId: _EditionIdAdver },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Anuncio agregado!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal("¡Atención!", "¡El nombre del anuncio ingresado ya existe!", "error");
                        }
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $(".Cliente_Participante").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Cliente_Participante").is(':checked')) {
                var _CompanyId = $(this).attr("Id");
                var _EditionId = $("#_EditionId").val();
                var _CompanyTypeId = $("#_CompanyTypeId").val();
                tr.find("#CompanyId").text(_CompanyId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarClienteEdición",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId, CompanyTypeId: _CompanyTypeId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cliente agregado a la edición!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else
                        {
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
            }
            else if (tr.find(".Cliente_Participante").is(":not(:checked)")) {
                $(function () {
                    $("#cargandoEliminandoCliente").show();
                });
                var _CompanyId = $(this).attr("Id");
                var _EditionId = $("#_EditionId").val();
                tr.find("#CompanyId").text(_CompanyId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/EliminarEdiciónCliente",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cliente desasociado de la edición!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            setTimeout(function () { location.reload(1); }, 1);
                        }
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $("._AgregarClienteEdición").click(function () {
            var _CompanyIdAdd = $("#_CompanyIdAdd").val();
            var _EditionIdAdd = $("#_EditionIdAdd").val();
            var _HtmlFileAdd = $("#_HtmlFileAdd").val();
            var _HtmlContentAdd = $("#_HtmlContentAdd").val();
            if (_EditionIdAdd == "Seleccione...") {
                swal("¡Atención!", "¡Los campos marcados con * son obligatorios!", "error");
            }
            else if (_EditionIdAdd != "Seleccione...") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarClienteEdición",
                    traditional: true,
                    data: { CompanyId: _CompanyIdAdd, EditionId: _EditionIdAdd, HtmlFile: _HtmlFileAdd, HtmlContent: _HtmlContentAdd },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cliente agregado a la edición!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal("¡Atención!", "¡El cliente no puede participar en la edición seleccionada!", "error");
                        }
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $("._AgregarClienteEdiciónParticipante").click(function () {
            var _CompanyIdAddParticipant = $("#_CompanyIdAddParticipant").val();
            var _EditionIdAddParticipant = $("#_EditionIdAddParticipant").val();
            var _CompanyTypeIdAddParticipant = $("#_CompanyTypeIdAddParticipant").val();
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Ventas/AgregarClienteEdición",
                traditional: true,
                data: { CompanyId: _CompanyIdAddParticipant, EditionId: _EditionIdAddParticipant, CompanyTypeId: _CompanyTypeIdAddParticipant },
                success: function (data) {
                    if (data == true) {
                        swal("¡Cliente agregado a la edición!", "", "success")
                        setTimeout(function () { location.reload(1); }, 300);
                    }
                    else {
                        swal("¡Atención!", "¡El no puede participar en esta edición!", "error");
                    }
                }
            });
        });
    });
    $(document).ready(function () {
        $(".SaveEdit").click(function () {
            var tr = $(this).parents("tr:first");
            var _Id = $("#_CompanyIdEdit").val();
            var _NombreCliente = $("._CompanyNameEdit").val();
            var _NombreCorto = $("._CompanyShorthNameEdit").val();
            var _TipoCliente = $("#_CompanyTypeIdEdit").val();
            var _ClienteAsociado = $("#_CompanyIdParentEdit").val();
            var _EditionId = $("#_EditionIdEdit").val();
            var _HtmlFile = $("#_HtmlFileEdit").val();
            var _HtmlContent = $("#_HtmlContentEdit").val();
            var _Respaldo = $("#_RespaldoEdit").val();
            var _Page = $("#_PageEdit").val();
            var _Active = $("#_ActiveEdit").val();
            if (_NombreCliente.trim() == false && _TipoCliente == "Seleccione...") {
                swal("¡Atención!", "Los campos marcados con * son obligatrios!", "error");
            }
            else if (_NombreCliente.trim() == false) {
                swal("¡Atención!", "¡El campo 'Nombre del cliente' no puede quedar vacio!", "error");
            }
            else if (_TipoCliente == "Seleccione...") {
                swal("¡Atención!", "¡El campo 'Tipo de cliente' no puede quedar vacio!", "error");
            }
            else {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/EditarCliente",
                    traditional: true,
                    data: {
                        CompanyId: _Id,
                        CompanyName: _NombreCliente, CompanyShortName: _NombreCorto,
                        CompanytypeId: _TipoCliente, CompanyIdParent: _ClienteAsociado,
                        EditionId: _EditionId, HtmlFile: _HtmlFile, HtmlContent: _HtmlContent,
                        Respaldo: _Respaldo, Page: _Page, Active: _Active
                    },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cambios guardados!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                    }
                });
            }
        });
    });
    function saveTels(a) {
        var valInput = a;
        var txtInput = $("#" + a).val();
        var _CompanyId = $("#_CompanyId").val();
        if (txtInput != false) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Ventas/EditarTeléfono",
                traditional: true,
                data: { CompanyId: _CompanyId, PhoneTypeEdit: valInput, PhoneValueEdit: txtInput },
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
function lastinformation(_EditionId, _CompanyId, _CompanyTypeId)
{
    swal({
        title: "¿Desea cargar la misma información de la edición anterior?",
        text: "Se cargaran, marcas, productos, distribuidores y anuncios que este relacionados con el cliente",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Cancelar",
        showLoaderOnConfirm: "Cancelar",
        cancelButtonText: "Aceptar",
        closeOnConfirm: false,
        closeOnCancel: false
    },
    function (isConfirm) {
        if (isConfirm == false) {
            $.ajax({
                async : true,
                type: "POST",
                dataType: "json",
                url: "/Ventas/MismaInformación",
                traditional: true,
                data: { EditionId: _EditionId, CompanyId: _CompanyId, CompanyTypeId: _CompanyTypeId },
                success: function (data) {
                    if (data == true) {
                        swal("¡Información cargada!", "", "success");
                        setTimeout(function () { location.reload(1); }, 1);
                    }
                    else
                    {
                        swal("¡El cliente no participo en la edición anterior!", "", "error");
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
    $(document).ready(function () {
        $("._AgregarTeléfono").click(function () {
            var tr = $(this).parents("tr:first");
            var _CompanyId = $("#_CompanyId").val();
            var _PhoneTypeIdAdd = $("#_PhoneTypeIdAdd").val();
            var _PhoneValueAdd = $("#_PhoneValueAdd").val();
            var _EditionId = $("#_EditionId").val();
            if (_PhoneValueAdd.trim() == false && _PhoneTypeIdAdd == "Seleccione...") {
                swal("¡Atención!", "¡Los campos marcados con * son obligatorios!", "error");
            }
            else if (_PhoneTypeIdAdd == "Seleccione...") {
                swal("¡Atención!", "¡El campo 'Tipo de teléfono' no puede quedar vacio!", "error");
            }
            else if (_PhoneValueAdd.trim() == false) {
                swal("¡Atención!", "¡El campo 'Teléfono' no puede quedar vacio!", "error");
            }
            else if (_PhoneValueAdd.trim != false && _PhoneTypeIdAdd != "Seleccione...") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarTeléfono",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId, PhoneTypeId: _PhoneTypeIdAdd, PhoneValue: _PhoneValueAdd },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Teléfono agregado!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal("¡Atención!", "¡El tipo de teléfono ingresado ya existe!", "error");
                        }
                    }
                });
            }
        });
    });
    $(document).ready(function () {
        $("._AgregarDirección").click(function () {
            var _CompanyId = $("#_CompanyId").val();
            var _Address = $("#_Address").val();
            var _Suburb = $("#_Suburb").val();
            var _Ubication = $("#_Ubication").val();
            var _ZipCode = $("#_ZipCode").val();
            var _Email = $("#_Email").val();
            var _Web = $("#_Web").val();
            var _Contact = $("#_Contact").val();
            var _CityId = $("#_CityIdEdit").val();
            var _Location = $("#_Location").val();
            if (_Address.trim() == false) {
                swal("¡Atención!", "¡El campo 'Dirección' no puede quedar vacio!", "error");
            }
            else
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarDirección",
                    traditional: true,
                    data: {
                        CompanyId: _CompanyId, Address: _Address, Suburb: _Suburb, Ubication: _Ubication,
                        ZipCode: _ZipCode, Email: _Email, Web: _Web, Contact: _Contact, CityId: _CityId, Location: _Location
                    },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Dirección agregada!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                    }
                });
        });
    });
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
        $('#CaracterEdiciones').keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaEdiciones').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(document).ready(function () {
        $('#Caracter2').keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaClientes2').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(document).ready(function () {
        $("._EliminarAnuncio").click(function () {
            swal("¡Anuncio desasociado de la edición!", "", "success")
        });
    });
    $(document).ready(function () {
        $("._CambiosGuardados").click(function () {
            swal("¡Cliente desasociado!", "", "success")
        });
    });
    $(document).ready(function () {
        $("._EliminarAnuncioSección").click(function () {
            swal("¡Anuncio eliminado!", "", "success")
        });
    });
    $(document).ready(function () {
        $("._CambiosGuardadosSección").click(function () {
            swal("¡Cliente desasociado de la sección!", "", "success")
        });
    });
    $(document).ready(function () {
        $(".TeléfonoEliminado").click(function () {
            swal("¡Teléfono eliminado!", "", "success")
        });
    });
    $(document).ready(function () {
        $(".DirecciónEliminada").click(function () {
            swal("¡Dirección eliminada!", "", "success")
        });
    });
    $(document).ready(function () {
        $("#enlaceajaxEditCompany").click(function (evento) {
            $("#destino").load("/Ventas/EditarEdicionesParticipantes", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#EnlaceAjaxAgregarSeccionesCliente").click(function (evento) {
            $("#DestinoAjaxAgregarSeccionesCliente").load("/Ventas/AgregarSeccionesCliente", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#EnlaceAjaxAgregarNuevoAnuncioSección").click(function (evento) {
            $("#DestinoAjaxAgregarNuevoAnuncioSección").load("/Ventas/AgregarNuevoAnuncioSección", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#EnlaceAjaxAsociarAnuncioSección").click(function (evento) {
            $("#DestinoAjaxAsociarAnuncioSección").load("/Ventas/AsociarAnuncioSección", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#EnlaceAjaxEditarDirecciones").click(function (evento) {
            $("#DestinoDirecciones").load("/Ventas/EditarDirecciónes", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#Enlaceajaxanuncio").click(function (evento) {
            $("#destinoanuncio").load("/Ventas/EditarAnuncio", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajaxT").click(function (evento) {
            $("#destino2").load("/Ventas/EditarTeléfonos", function () {
            });
        });
    })
    $(function () {
        $(".carga").click(function () {
            $("#cargando").show();
        });
        $("#cargando").hide();
    });
    $(function () {
        $(".eliminando").click(function () {
            $("#cargandoeliminando").show();
        });
        $("#cargandoeliminando").hide();
    });