
/*check email*/
function ValidationEmail() {
    var form = document.forms["otp"].checkValidity();
    event.preventDefault();
    if (form) {
       SendOTP();
    }
}

/*store data registration*/
function SendOTP() {

    var data = new Object();

    data.email = $('#email').val();


    $.ajax({
        type: 'POST',
        url: 'accounts/forgot-password',
        data: data,
        success: function (data) {
            if (data.status != 200) {
                $('#forgot-alert-success').hide();
                $('#alert-text-danger').text(data.message);
                $('#forgot-alert-danger').show();

            } else {
                $('#forgot-alert-danger').hide();
                $('#alert-text-success').text(data.message);
                $('#forgot-alert-success').show();
                document.getElementById("otp").reset();


                setTimeout(function () {
                    window.location.href = 'ResetPassword';
                }, 3000);
            
             
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}