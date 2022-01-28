Triggerdata();
function Triggerdata() {
    $.ajax({
        url: 'https://localhost:44323/accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        var userName = document.getElementById("userName");
        userName.textContent = result.name;
    }).fail(error => {
        console.log(error)
    })
}
