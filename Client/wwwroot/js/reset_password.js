

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
                document.getElementById("formReset").reset();
                document.getElementById("formReset").classList.remove('was-validated');
            }
            $(document).ready(function () {
                window.setTimeout(function () {
                    $(".alert").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    })
}