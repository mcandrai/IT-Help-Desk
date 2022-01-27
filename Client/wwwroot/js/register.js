
function ValidationRegistration() {
    var form = document.forms["registration"].checkValidity();
    event.preventDefault();
    if (form) {
        StoreRegistration();
    }
}

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
        url: 'Employees/Register',
        data: data,
        success: function (data) {
            if (data.status != 200) {
                $('#alert-text-danger').text(data.message);
                $('#register-alert-danger').show();

            } else {
                $('#alert-text-success').text(data.message);
                $('#register-alert-success').show();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}