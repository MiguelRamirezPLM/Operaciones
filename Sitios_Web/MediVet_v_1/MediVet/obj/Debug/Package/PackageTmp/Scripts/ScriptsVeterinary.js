function GetTherapeuticUsesByLetter(letter) {

    var ltr = $(letter).attr("Id");
    var Pid = $("#ProductIdV").val();
    var pf = $("#PharmaFormIdV").val();
    var cat = $("#CategoryIdV").val();
    var d = "";
    d += "<div align='center'><img src='../Images/alerta.png' /> </div>";

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Veterinary/getTherapeuticUses/",
        traditional: true,
        data: { Letter: ltr, Product: Pid, PharmaForm: pf, Category: cat },
        success: function (data) {
            if(data == true)
            {
                setTimeout('document.location.reload()');
            }
            else if (data == "exist")
            {
                d += "<p class='labels'></p>";
                d += "<p class='labels'>- El Uso ya esta asociado al Producto</p>";
                apprise("" + d + "", { 'animate': true });
                $("#bloqueo").hide();
            }
        }
    });
}

function load(btn) {
    $("#bloqueo").show();
}

function getletter(Letter) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Veterinary/getletter/",
        traditional: true,
        data: { Letters: Letter },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    });
}

function cleanletter(Letter) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Veterinary/cleanletter/",
        traditional: true,
        data: { Letters: Letter },
        success: function (data) {
            setTimeout('document.location.reload()');
        }
    });
}