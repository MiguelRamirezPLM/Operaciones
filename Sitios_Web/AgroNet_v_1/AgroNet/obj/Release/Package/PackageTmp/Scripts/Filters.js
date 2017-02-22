function getbook(CountryId) {
    $('#bookn').empty();
    $('#Edition').empty();
    $('#divisions').empty();
    document.getElementById("CountryId").value = CountryId;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Ventas/books/",
        traditional: true,
        data: { country: CountryId },
        success: function (data) {
            $('#bookn')
               .append($("<option></option>")
               .attr("value", 0)
               .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#bookn')
                .append($("<option></option>")
                .attr("value", val.BookId)
                .text(val.BookName));
            });
        }
    });
}

function geteditions(book) {
    document.getElementById("BookId").value = book;
    var idcountry = $("#pais").val();
    $('#divisions').empty();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Ventas/Edition/",
        traditional: true,
        data: { country: idcountry, bookid: book },
        success: function (data) {
            $('#Edition').empty();
            $('#Edition')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Edition')
                .append($("<option></option>")
                .attr("value", val.EditionId)
                .text(val.NumberEdition));
            });
        }
    });
}

function getclients() {
    var idcountry = $("#pais").val();
    var edition = $("#Edition").val();
    document.getElementById("EditionId").value = edition;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Ventas/Divisions/",
        traditional: true,
        data: { country: idcountry },
        success: function (data) {
            $('#divisions').empty();
            $('#divisions')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#divisions')
                .append($("<option></option>")
                .attr("value", val.DivisionId)
                .text(val.DivisionName));
            });
        }
    });
}

function getresults(clientid) {
    document.getElementById("DivisionId").value = clientid;
    $('#InsertParam').trigger('click');
    $("#bloqueo").show();
}