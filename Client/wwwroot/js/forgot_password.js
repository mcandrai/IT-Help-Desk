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
                document.getElementById("formOTP").reset();
                document.getElementById("formOTP").classList.remove('was-validated');

                setTimeout(function () {
                    window.location.href = 'reset-password';
                }, 3000);
            
             
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