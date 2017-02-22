function login(User, Pass) {
    var usr = $(User).val();
    var psw = $(Pass).val();

    if ((!usr.trim() == false) && (!psw.trim() == false)) {
        $("#bloqueo").show();
    }
}

function show_loader() {
    $("#bloqueo").show();
}

function getBooks(CountryId) {
    $('#Books').empty();
    $('#Edition').empty();
    $('#clients').empty();
    document.getElementById("CountryId").value = CountryId;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Filters/getBooks",
        traditional: true,
        data: { country: CountryId },
        success: function (data) {

            console.log(data);

            $('#Books')
               .append($("<option></option>")
               .attr("value", 0)
               .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Books')
                .append($("<option></option>")
                .attr("value", val.BookId)
                .text(val.BookName));
            });
        }
    });
}

function getEditionType(BookId) {

    $('#EditionType').empty();
    $('#Edition').empty();
    $('#clients').empty();

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Filters/getEditionType/",
        traditional: true,
        success: function (data) {
            $('#EditionType')
               .append($("<option></option>")
               .attr("value", 0)
               .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#EditionType')
                .append($("<option></option>")
                .attr("value", val.EditionTypeId)
                .text(val.TypeName));
            });
        }
    });
}

function getEditions(BookId) {
    document.getElementById("BookId").value = BookId;
    var idcountry = $("#CountryId").val();
    var book = $("#BookId").val();
    $('#Edition').empty();
    $('#clients').empty();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Filters/getEditions",
        traditional: true,
        data: { Country: idcountry, Book: book },
        success: function (data) {
            console.log(data);

            $('#Editions').empty();
            $('#Editions')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Editions')
                .append($("<option></option>")
                .attr("value", val.EditionId)
                .text(val.NumberEdition));
            });
        }
    });
}

function getDivisions(Edition) {
    document.getElementById("EditionId").value = Edition;

    var idcountry = $("#CountryId").val();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Filters/getDivisions",
        traditional: true,
        data: { Country: idcountry },
        success: function (data) {
            $('#Divisions').empty();
            $('#Divisions')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Divisions')
                .append($("<option></option>")
                .attr("value", val.DivisionId)
                .text(val.DivisionName));
            });
        }
    });
}

function getresults(DivisionId) {
    document.getElementById("DivisionId").value = DivisionId;
    $('#InsertParam').trigger('click');
    $("#bloqueo").show();
}

function GetResultCountry(DivisionId) {

    document.getElementById("DivisionId").value = DivisionId;

    $('#InsertParam').trigger('click');
    $("#bloqueo").show();

}

function getDivisionsD(CountryId) {
    
    document.getElementById("CountryId").value = CountryId;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Filters/getDivisions",
        traditional: true,
        data: { Country: CountryId },
        success: function (data) {
            $('#Divisions').empty();
            $('#Divisions')
                .append($("<option></option>")
                .attr("value", 0)
                .text("Seleccione..."));
            $.each(data, function (index, val) {
                $('#Divisions')
                .append($("<option></option>")
                .attr("value", val.DivisionId)
                .text(val.DivisionName));
            });
        }
    });
}