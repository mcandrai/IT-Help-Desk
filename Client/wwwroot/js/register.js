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
                    StoreRegistration();
                }
                form.classList.add('was-validated');

            }, false);
        });
    }, false);
})();


function StoreRegistration() {

    var data = new Object();

    data.firstName = $('#firstName').val();
    data.lastName = $('#lastName').val();

    if (document.getElementById('gender-female').checked) {
        data.gender = $('#gender-female').val();
    } else {
        data.gender = $('#gender-male').val();
    }

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
                document.getElementById("formRegistration").reset();
                document.getElementById("formRegistration").classList.remove('was-validated');

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