
/*Function to validate email*/
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
                    SendOTP();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


/*Function to store otp*/
function SendOTP() {

    var data = new Object();

    data.email = $('#email').val();

    $.ajax({
        type: 'POST',
        url: 'accounts/forgot-password',
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
                document.getElementById("formOTP").reset();
                document.getElementById("formOTP").classList.remove('was-validated');

                setTimeout(function () {
                    window.location.href = 'reset-password';
                }, 3000);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}