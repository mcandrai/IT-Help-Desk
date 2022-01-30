
/*check data email and otp*/
function ValidationOTP() {
    var form = document.forms["reset"].checkValidity();
    event.preventDefault();
    if (form) {
        ChangePassword();
    }
}

/*change password*/
function ChangePassword() {

    var data = new Object();

    data.email = $('#email').val();
    data.password = $('#password').val();
    data.otpCode = $('#otpCode').val();

    $.ajax({
        type: 'POST',
        url: 'accounts/change-password',
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
                document.getElementById("reset").reset();

                $(document).ready(function () {
                    window.setTimeout(function () {
                        $(".alert").fadeTo(500, 0).slideUp(500, function () {
                            $(this).remove();
                        });
                    }, 3000);
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}