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
        $("._TipodeProducto").click(function () {
            var _ProducTypeId = $("#_ProductTypeIdAdd").val();
            if (_ProducTypeId.trim() == "2") {
                $("#_ProductParent").show();
            }
            else {
                $("#_ProductParent").hide();
            }
        });
    });
    $(document).ready(function () {
        $("._CambiosGuardados").click(function () {
            swal("¡Producto desasociado del índice!", "", "success")
        });
    });
    $(document).ready(function () {
        $("._CambiosGuardados2").click(function () {
            swal("¡Producto desasociado de la sección!", "", "success")
        });
    });
    $(document).ready(function () {
        $("#enlaceajaxÍndices").click(function (evento) {
            $("#destinoÍndices").load("/Ventas/AgregarÍndicesProducto", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajaxSecciones").click(function (evento) {
            $("#destinoSecciones").load("/Ventas/AgregarSeccionesProducto", function () {
            });
        });
    })
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
            swal("¡Producto desasociado!", "", "success");
        });
    });
    $(document).ready(function () {
        $("#enlaceajax").click(function (evento) {
            $("#destino").load("/Ventas/EditarProductos", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajax2").click(function (evento) {
            $("#destino2").load("/Ventas/EditarEdicionesParticipantesProducto", function () {
            });
        });
    })
    $(document).ready(function () {
        $("#enlaceajax3").click(function (evento) {
            $("#destino3").load("/Ventas/AgregarProductoNuevaEdición", function () {
            });
        });
    })
    $(document).ready(function () {
        $(".AgregarProducto").click(function () {
            var _CompanyId = $("#_CompanyIdAdd").val();
            var _ProductNameAdd = $("#_ProductNameAdd").val();
            var _ProductTypeIdAdd = $("#_ProductTypeIdAdd").val();
            var _DescriptionAdd = $("#_DescriptionAdd").val();
            var _ProductIdParentAdd = $("#_ProductIdParentAdd").val();
            var _EditionIdAddProduct = $("#_EditionIdAddProduct").val();
            if (_ProductNameAdd.trim() == false && _ProductTypeIdAdd == "Seleccione...") {
                swal("¡Atención!", "¡Los campos marcados con * son obligatorios!", "error");
            }
            else if (_ProductTypeIdAdd == "Seleccione...") {
                swal("¡Atención!", "¡El campo 'Tipo de producto' no puede quedar vacio!", "error");
            }
            else if (_ProductNameAdd.trim() == false) {
                swal("¡Atención!", "¡El campo 'Nombre del producto' no puede quedar vacio!", "error");
            }
            if (_ProductTypeIdAdd.trim() == "2") {
                if (_ProductNameAdd.trim() == false && _ProductTypeIdAdd == "Seleccione..." && _ProductIdParentAdd == "Seleccione...") {
                    swal("¡Atención!", "¡Los campos marcados con * son obligatorios!", "error");
                }
                else if (_ProductTypeIdAdd == "Seleccione...") {
                    swal("¡Atención!", "¡El campo 'Tipo de producto' no puede quedar vacio!", "error");
                }
                else if (_ProductNameAdd.trim() == false) {
                    swal("¡Atención!", "¡El campo 'Nombre del producto' no puede quedar vacio!", "error");
                }
                else if (_ProductIdParentAdd == "Seleccione...") {
                    swal("¡Atención!", "¡El campo 'Producto asociado' no puede quedar vacio!", "error");
                }
                else if (_ProductNameAdd.trim() != false && _ProductTypeIdAdd != "Seleccione...") {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/Ventas/AgregarProductoNuevo",
                        traditional: true,
                        data: { CompanyId: _CompanyId, ProductName: _ProductNameAdd, ProductTypeId: _ProductTypeIdAdd, Description: _DescriptionAdd, ProductIdParent: _ProductIdParentAdd, EditionId: _EditionIdAddProduct },
                        success: function (data) {
                            if (data == true) {
                                swal("¡Producto agregado!", "", "success")
                                setTimeout(function () { location.reload(1); }, 300);
                            }
                            else {
                                swal("¡Atención!", "¡No puede agregar este producto, por alguna de las siguientes circunstancias: 1.- El producto ya existe, 2.- Si el producto tiene un producto asociado solamente debe de ser de tipo 'AGREGADO'!", "error");
                            }
                        }
                    });
                }
            }
            else if (_ProductNameAdd.trim() != false && _ProductTypeIdAdd != "Seleccione...") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/AgregarProductoNuevo",
                    traditional: true,
                    data: { CompanyId: _CompanyId, ProductName: _ProductNameAdd, ProductTypeId: _ProductTypeIdAdd, Description: _DescriptionAdd, ProductIdParent: _ProductIdParentAdd, EditionId: _EditionIdAddProduct },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Producto agregado!", "", "success")
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal("¡Atención!", "¡No puede agregar este producto, por alguna de las siguientes circunstancias: 1.- El producto ya existe, 2.- Si el producto tiene un producto asociado solamente debe de ser de tipo 'AGREGADO'!", "error");
                        }
                    }
                });
            }
        });
    });
function _Eliminar_Sección_Producto(a, b)
{
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Ventas/EliminarSecciónProducto",
        traditional: true,
        data: { SectionId: a, ProductId: b },
        success: function (data) {
            if (data == true) {
                swal("¡Producto desasociado!", "", "success")
                setTimeout(function () { location.reload(1); }, 300);
            }
        }
    });
}
function _Eliminar_Índice_Producto(a, b)
{
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Ventas/EliminarProductoÍndice",
        traditional: true,
        data: { IndexId: a, ProductId: b },
        success: function (data) {
            if (data == true) {
                swal("¡Producto desasociado!", "", "success")
                setTimeout(function () { location.reload(1); }, 300);
            }
        }
    });
}
    $(document).ready(function () {
        $(".Producto_Participante").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Producto_Participante").is(':checked')) {
                var _ProdId = $(this).attr("Id");
                var _EditsId = $("#_EditionId").val();
                tr.find("#ProductId").text(_ProdId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/InsertarProductoParticipante",
                    traditional: true,
                    data: { EditionId: _EditsId, ProductId: _ProdId },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Cambios guardados!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal({
                                title: "¡Atención!",
                                text: "¡El producto no puede participar en esta edición!",
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
            else if (tr.find(".Producto_Participante").is(":not(:checked)")) {
                var _ProdId = $(this).attr("Id");
                var _EditsId = $("#_EditionId").val();
                tr.find("#ProductId").text(_ProdId);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/EliminarProductoParticipante",
                    traditional: true,
                    data: { EditionId: _EditsId, ProductId: _ProdId },
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
        $(".Producto_Nuevo").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Producto_Nuevo").is(':checked')) {
                var _EditionIdNew = $("#_EditionIdNew").val();
                var _ProductIdNew = $(this).attr("Id");
                tr.find("#ProductId").text(_ProductIdNew);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/ProductoNuevo",
                    traditional: true,
                    data: { EditionId: _EditionIdNew, ProductId: _ProductIdNew },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Producto nuevo!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal({
                                title: "¡Atención!",
                                text: "¡Las casilla no puede quedar vacia!",
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
            else {
                swal({
                    title: "¡Atención!",
                    text: "¡Las casilla no puede quedar vacia!",
                    type: "warning",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                }, function () {
                    setTimeout(function () { location.reload(1); }, 1);
                })
            }
        });
    });
    $(document).ready(function () {
        $(".Producto_Con_Cambios").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Producto_Con_Cambios").is(':checked')) {
                var _EditionIChanges = $("#_EditionIChanges").val();
                var _ProductIdChanges = $(this).attr("Id");
                tr.find("#ProductId").text(_ProductIdChanges);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/ProductoConCambios",
                    traditional: true,
                    data: { EditionId: _EditionIChanges, ProductId: _ProductIdChanges },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Producto con cambios!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal({
                                title: "¡Atención!",
                                text: "¡Las casilla no puede quedar vacia!",
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
            else {
                swal({
                    title: "¡Atención!",
                    text: "¡Las casilla no puede quedar vacia!",
                    type: "warning",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                }, function () {
                    setTimeout(function () { location.reload(1); }, 1);
                })
            }
        });
    });
    $(document).ready(function () {
        $(".Producto_Sin_Cambios").click(function () {
            var tr = $(this).parents("tr:first");
            if (tr.find(".Producto_Sin_Cambios").is(':checked')) {
                var _EditionIdNotChanges = $("#_EditionIdNotChanges").val();
                var _ProductIdNotChanges = $(this).attr("Id");
                tr.find("#ProductId").text(_ProductIdNotChanges);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Ventas/ProductoSinCambios",
                    traditional: true,
                    data: { EditionId: _EditionIdNotChanges, ProductId: _ProductIdNotChanges },
                    success: function (data) {
                        if (data == true) {
                            swal("¡Producto sin cambios!", "", "success");
                            setTimeout(function () { location.reload(1); }, 300);
                        }
                        else {
                            swal({
                                title: "¡Atención!",
                                text: "¡Las casilla no puede quedar vacia!",
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
            else {
                swal({
                    title: "¡Atención!",
                    text: "¡Las casilla no puede quedar vacia!",
                    type: "warning",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                }, function () {
                    setTimeout(function () { location.reload(1); }, 1);
                })
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