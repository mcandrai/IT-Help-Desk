Triggerdata();
function Triggerdata() {
    $.ajax({
        url: 'accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        var userName = document.getElementById("userName");
        userName.textContent = result.name;
    }).fail(error => {
        console.log(error)
    })
}
