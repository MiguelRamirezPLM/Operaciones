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
        $('#Caracter2').keyup(function () {
            var valThis = $(this).val().toLowerCase();
            $('.ListaProductos2').each(function () {
                var text = $(this).text().toLowerCase();
                (text.indexOf(valThis) == 0) ? $(this).show() : $(this).hide();
            });
        });
    });
    $(document).ready(function () {
        $(".GuardarCambios").click(function () {
            swal("¡Distribuidor asociado!", "", "success");
        });
    });
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
        $(".Distribuidor_Participante").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Distribuidor_Participante").is(':checked')) {
                var _DistributionId = $(this).attr("Id");
                var _EditionId = $("#_EditionId").val();
                var _CompanyId = $("#_CompanyId").val();
                tr.find("#DistributionId").text(_DistributionId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/DistribuidorParticipante",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId, DistributionId: _DistributionId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cambios guardados!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal({
                                title: "¡Atención!",
                                text: "¡El distribuidor no puede participar, dado que el cliente al que lo quiere asociar aún no participa en la edición!",
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
            else if (tr.find(".Distribuidor_Participante").is(":not(:checked)")) {
                var tr = $(this).parents("tr:first");
                var _DistributionId = $(this).attr("Id");
                var _EditionId = $("#_EditionId").val();
                var _CompanyId = $("#_CompanyId").val();
                tr.find("#DistributionId").text(_DistributionId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/EliminarDistribuidorParticipante",
                    traditional: true,
                    data: { EditionId: _EditionId, CompanyId: _CompanyId, DistributionId: _DistributionId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cambios guardados!", "", "success");
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
                $('.ListaEdiciones').each(function () {
                    var text = $(this).text().toLowerCase();
                    (text.indexOf(valThis) == 0) ? $(this).show : $(this).hide();
                });
            });
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