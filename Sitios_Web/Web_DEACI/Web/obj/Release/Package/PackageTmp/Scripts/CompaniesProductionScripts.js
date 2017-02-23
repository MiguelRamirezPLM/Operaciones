     $(document).ready(function () {
        $(".Laboratorios").click(function () {
            swal({ title: "Generando...", text: "Reporte excel laboratorios análisis clínicos y diagnóstico por imagen", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $(".Secciones_por_Edición").click(function () {
            swal({ title: "Generando...", text: "Reporte excel productos por sección", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $(".Nombre_Comercial").click(function () {
            swal({ title: "Generando...", text: "Índice general por nombre comercial", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $(".Imagenología").click(function () {
            swal({ title: "Generando...", text: "Índice general de imagenología y radiodiagnóstico", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $(".Genéricos").click(function () {
            swal({ title: "Generando...", text: "Índice general por genéricos", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $(".Índice_Marcas").click(function () {
            swal({ title: "Generando...", text: "Índice de marcas", timer: 4000, showConfirmButton: false, imageUrl: "/Images/loaddi.gif" });
        });
    });
    $(document).ready(function () {
        $("._EstadoSelect").change(function () {
            var _StateId = $("#_StateId").val();
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Producción/ObtenerCiudades",
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
                    url: "/Producción/AgregarDirección",
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
                    url: "/Producción/AgregarTeléfono",
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
        $("#EnlaceAjaxEditarDirecciones").click(function (evento) {
            $("#DestinoDirecciones").load("/Producción/EditarDirecciónes", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajaxT").click(function (evento) {
            $("#destino2").load("/Producción/EditarTeléfonos", function () {
            });
        });
    })
    $(document).ready(function () {
        $("._EliminarAnuncio").click(function () {
            swal("¡Anuncio desasociado de la edición!", "", "success")
        });
    });
    $(document).ready(function () {
        $("#EnlaceAjaxEditarAnuncio").click(function (evento) {
            $("#DestinoEnlaceAjaxEditarAnuncio").load("/Producción/EditarAnuncio", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajaxEditCompany").click(function (evento) {
            $("#destino").load("/Producción/EditarEdicionesParticipantes", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajax").click(function (evento) {
            $("#destino").load("/Producción/AgregarSeccionesCliente", function () {
            });
        });
    })
    $(document).ready(function () {
        $("._CambiosGuardados").click(function () {
            swal("¡Cliente desasociado de la sección!", "", "success")
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
        $('#Caracter').keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaClientes').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(document).ready(function () {
        $("#CaracterEdiciones").keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $(".ListaEdiciones").each(function () {
                var text = $(this).text().toLocaleLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(function () {
        $(".carga").click(function () {
            $("#cargando").show();
        });
        $("#cargando").hide();
    });