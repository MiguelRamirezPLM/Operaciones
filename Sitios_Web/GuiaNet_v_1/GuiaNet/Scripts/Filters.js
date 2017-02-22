function getbook(CountryId) {
    $('#bookn').empty();
    $('#Edition').empty();
    $('#clients').empty();
    document.getElementById("CountryId").value = CountryId;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../SalesModule/books/",
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
    $('#clients').empty();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../SalesModule/editions/",
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
        url: "../SalesModule/getclients/",
        traditional: true,
        data: { country: idcountry },
        success: function (data) {
            $('#clients').empty();
            $('#clients')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#clients')
                .append($("<option></option>")
                .attr("value", val.ClientId)
                .text(val.CompanyName));
            });
        }
    });
}


function getclientsspecialities() {
    var idcountry = $("#pais").val();
    var edition = $("#Edition").val();
    document.getElementById("EditionId").value = edition;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../SalesModule/getclientspecialities/",
        traditional: true,
        data: { country: idcountry },
        success: function (data) {
            console.log(data);
            $('#clients').empty();
            $('#clients')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#clients')
                .append($("<option></option>")
                .attr("value", val.ClientId)
                .text(val.CompanyName));
            });
        }
    });
}

function getresults(clientid) {
    document.getElementById("ClientId").value = clientid;
    $('#InsertParam').trigger('click');
    $("#bloqueo").show();
}