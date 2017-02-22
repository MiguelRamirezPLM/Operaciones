function GetTherapeuticUsesByLetter(letter) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Veterinary/getTherapeuticUses",
        traditional: true,
        data: { Letter: letter },
        success: function (data) {
            if (data == false) {
            }
        }
    });
}

function load(btn) {
    $("#bloqueo").show();
}
