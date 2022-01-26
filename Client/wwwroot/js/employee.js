
/*get data employee*/
$(document).ready(function () {
    $('#employeeTable').DataTable({
        "scrollX": true,
        "oLanguage": { "sSearch": "" },
        language: {
            searchPlaceholder: "Find record employee...."
        },
        "ajax": {
            'url': 'https://localhost:44321/API/employees/register',
            'dataType': 'json',
            'dataSrc': 'data'
        },
        dom: 'Bfrtip',
        buttons: [
            {
                className: 'buttonExcel',
                text: '<i class="fa fa-file-excel-o"></i>',
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },
            {
                className: 'buttonPdf',
                text: '<i class="fa fa-file-pdf-o"></i>',
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }

        ],
        'columns': [{
            'data': 'fullName'
        },
        {
            'data': 'phone'
        },
        {
            'data': 'email'
        },
        {
            'data': 'brithDate'
        },
        {
            'data': 'accountRole',
            'render': function (data) {
                var roleData = '';
                $.each(data, function (key, val) {
                    roleData += `${val}`
                });
                return roleData;
            }
        },
        {
            'data': null,
            'bSortable': false,
            'render': function (data) {
                var actionButton = `<button class="btn btn-sm btn-primary"><i class="fas fa-info-circle" aria-hidden='true'></i></button>
                                    <button class="btn btn-sm btn-success" data-toggle="modal" data-target="#modalEditEmployee" data-whatever="${data.nik}"><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>
                                    <button class="btn btn-sm btn-danger" onclick="alertConfirmation('${data.nik}')"><i class="fas fa-trash-alt" aria-hidden='true'></i></button>`
                return actionButton;
            }
        }
        ]
    });
});


/*get data university*/
function getUniversity() {
    $.ajax({
        url: 'https://localhost:44321/API/universities'
    }).done((data) => {
        var universitySelect = '';
        $.each(data, function (key, val) {
            universitySelect += `<option value=${val.universityId}>${val.universityName}</option>`
        });
        $("#universityId").html(universitySelect);

    }).fail((error) => {
        console.log(error);
    })
}


/*validation save employee*/
(function () {
    'use strict';
    window.addEventListener('load', function () {
   
        var forms = document.getElementsByClassName('needs-validation');
      
        var validation = Array.prototype.filter.call(forms, function (form) {
            document.getElementById('formEmployee').addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    console.log(true);
                    // storeEmployee();
                }

                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


/*save employee*/
function storeEmployee() {

    var employeeData = new Object();

    employeeData.firstName= $('#firstName').val();
    employeeData.lastName= $('#lastName').val()
    employeeData.gender= $('#gender').val();
    employeeData.brithDate = $('#brithDate').val();
    employeeData.phone = $('#phone').val();
    employeeData.universityId = $('#universityId').val();
    employeeData.degree = $('#degree').val();
    employeeData.gpa = $('#gpa').val();
    employeeData.email = $('#email').val();
    employeeData.password = $('#password').val();

    var employeeTable = $('#employeeTable').DataTable();

    $.ajax({
        type: 'POST',
        url: 'https://localhost:44321/API/employees/register',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(employeeData),
        success: function (data) {
            closeEmployeeModal();
            alertSuccess(data.message);
            employeeTable.ajax.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError(error.message);
        }
    })

}

/*get employee by nik*/
function getEmployeeByNIK(nik) {
    var employeeDataByNIK = new Object();
    employeeDataByNIK.nik = nik;
    $.ajax({
        type: 'POST',
        url: 'https://localhost:44321/API/employees/register-nik',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(employeeDataByNIK),
        success: function (result) {
            includeFormEdit(result.data)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
        }
    })
}

/*include in form edit employee*/
function includeFormEdit(data) {
    document.getElementById("firstNameEdit").value = data.firstName;
    document.getElementById("lastNameEdit").value = data.lastName;
    document.getElementById("genderEdit").value = data.gender;
    var date = new Date(data.brithDate + "Z").toISOString().substring(0,10);
    document.getElementById("brithDateEdit").value = date;
    document.getElementById("phoneEdit").value = data.phone;
    document.getElementById("emailEdit").value = data.email;
}

/*call method modal add employee is open*/
$('#modalAddEmployee').on('show.bs.modal', function () {
    getUniversity();
});

/*call method modal edit employee is open*/
$('#modalEditEmployee').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever')
    getEmployeeByNIK(recipient);
});


/*close modal*/
function closeEmployeeModal() {
    document.getElementById("formEmployee").reset();
    document.getElementById("formEmployee").classList.remove('was-validated');
    $('#modalAddEmployee').modal('hide');
}

function closeEmployeeModalEdit() {
    document.getElementById("formEditEmployee").reset()
    document.getElementById("formEditEmployee").classList.remove('was-validated');
    $('#modalEditEmployee').modal('hide');
}


/*show sweetalert confirmation delete employee*/
function alertConfirmation(nik) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-danger ml-3',
            cancelButton: 'btn btn-outline-secondary'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        imageUrl: '/image/trash.png',
        imageWidth: 50,
        imageHeight: 50,
        text: 'Are you sure to delete employee NIK : '+nik+' ?',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            deleteEmployeeByNIK(nik);
        }
    })
}

/*get employee by nik*/
function deleteEmployeeByNIK(nik) {
    var employeeDataByNIK = new Object();
    employeeDataByNIK.nik = nik;
    var employeeTable = $('#employeeTable').DataTable();
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:44321/API/employees/register',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(employeeDataByNIK),
        success: function (result) {
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
            employeeTable.ajax.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
        }
    })
}