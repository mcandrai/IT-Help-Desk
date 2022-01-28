
/*check data registration*/
function ValidationRegistration() {
    var form = document.forms["registration"].checkValidity();
    event.preventDefault();
    if (form) {
        StoreRegistration();
    }
}

/*store data registration*/
function StoreRegistration() {

    var data = new Object();

    data.firstName = $('#firstName').val();
    data.lastName = $('#lastName').val()
    data.gender = $('#gender').val();
    data.phone = $('#phone').val();
    data.email = $('#email').val();
    data.password = $('#password').val();


    $.ajax({
        type: 'POST',
        url: 'employees/register',
        data: data,
        success: function (data) {
            if (data.status != 200) {
                $('#register-alert-success').hide();
                $('#alert-text-danger').text(data.message);
                $('#register-alert-danger').show();

            } else {
                $('#register-alert-danger').hide();
                $('#alert-text-success').text(data.message);
                $('#register-alert-success').show();
                document.getElementById("registration").reset();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}