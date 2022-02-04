
/*Function to validate email and otp*/
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    ChangePassword();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

/*Function to store new password*/
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
                Swal.fire({
                    icon: 'error',
                    text: data.message,
                })

            } else {
                Swal.fire({
                    icon: 'success',
                    text: data.message,
                })
                document.getElementById("formReset").reset();
                document.getElementById("formReset").classList.remove('was-validated');

                setTimeout(function () {
                    window.location.href = 'login';
                }, 3000);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}