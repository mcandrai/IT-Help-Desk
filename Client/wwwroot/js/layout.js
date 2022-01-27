
Triggerdata();
function Triggerdata() {
    $.ajax({
        url: 'accounts/GenerateJWTNIK',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        console.log(result.name);
        var userName = document.getElementById("userName");
        userName.textContent = result.name;
    }).fail(error => {
    })
}
