

/*Function for validation check registration data*/
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

/*Function to save registration data*/
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
                Swal.fire({
                    icon: 'error',
                    text: data.message,
                })
            } else {
                Swal.fire({
                    icon: 'success',
                    text: data.message,
                })
                document.getElementById("formRegistration").reset();
                document.getElementById("formRegistration").classList.remove('was-validated');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }

    })
}