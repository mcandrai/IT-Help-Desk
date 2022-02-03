Triggerdata();
function Triggerdata() {
    $.ajax({
        url: 'https://localhost:44323/accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        GetProfile(result.nik)
    }).fail(error => {
        console.log(error);
    })
}

function GetProfile(nik) {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44359/api/employees/Get-Register-All/' + nik,
    }).done((data) => {
        $.each(data, function (key, val) {
            console.log(val);
            $("#nik").val(val.nik);
            $("#fullname").val(val.fullName);
            $("#email").val(val.email);
            $("#phone").val(val.phone);
            $("#gender").val(val.gender);
            $("#role").val(val.role);
        });
    }).fail((error) => {
        console.log(error);
    })

}