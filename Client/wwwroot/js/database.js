﻿/*get data all employee*/
$(document).ready(function () {
    /*var token = sessionStorage.getItem("JWToken");
    console.log(token);*/
    $('#ticketTable').DataTable({
        "ajax": {
            'url': 'https://localhost:44359/api/Tickets/View-Ticket-Database',
            'error': function (jqXHR) {
                console.log(jqXHR);
            },
            'dataType': 'json',
            'dataSrc': ''
        },
        dom: 'Bfrtip',
        buttons: [
            {
                className: 'buttonExcel',
                text: '<i class="fa fa-file-excel-o"></i>',
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4,5,6]
                }
            },
            {
                className: 'buttonPdf',
                text: '<i class="fa fa-file-pdf-o"></i>',
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4,5,6]
                }
            }

        ],
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return (meta.row + meta.settings._iDisplayStart + 1);
                }
            },
            {
                'data': 'id'
            },
            {
                'data': 'messageText'
            },
            {
                'data': 'updateAt'
            },
            {
                'data': 'createAt'
            },
            {
                'data': 'statusName'
            },
            {
                'data': 'categoryName'
            },
            {
                'data': null,
                'bSortable': false,
                'render': function (data) {
                    var actionButton = `<button class="btn btn-sm btn-primary" data-toggle="modal" data-target="#modalDetailEmployee" data-whatever="${data.id}"><i class="fas fa-info-circle" aria-hidden='true'></i></button>
                                        <button class="btn btn-sm btn-success" data-toggle="modal" data-target="#modalUpdateEmployee" data-whatever="${data.id}"><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>
                                        <button class="btn btn-sm btn-danger" onclick="deleteAlert(${data.id})"><i class="fas fa-trash-alt" aria-hidden='true'></i></button>`
                    return actionButton;
                }
            }
        ]
    });
});


/*validation save employee*/
(function () {
    'use strict';
    window.addEventListener('load', function () {

        var forms = document.getElementsByClassName('needs-validation');

        var validation = Array.prototype.filter.call(forms, function (form) {
            document.getElementById('formTicket').addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    storeTicket();
                }

                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


/*validation update employee*/
(function () {
    'use strict';
    window.addEventListener('load', function () {

        var forms = document.getElementsByClassName('needs-validation-update');

        var validation = Array.prototype.filter.call(forms, function (form) {
            document.getElementById('formUpdateEmployee').addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    updateEmployee();
                }

                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


/* store ticket*/
function storeTicket() {

    var ticketData = new Object();

    ticketData.createAt = $('#createAt').val();
    ticketData.updateAt = $('#updateAt').val()
    ticketData.categoryId = parseInt($('#categoryId').val());
    ticketData.nik = $('#nik').val();
    ticketData.message = $('#message').val();

    console.log(ticketData);
    var employeeTable = $('#ticketTable').DataTable();

    $.ajax({
        type: 'POST',
        url: 'tickets/CreateTicket',
        data: ticketData,
       /* dataType: "json",
        contentType: 'application/json',*/
        success: function (data) {
            console.log(data);
            closeTicketModal();
            employeeTable.ajax.reload();
            alertSuccess();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError();
        }
    })

}

/* store update employee*/
function updateEmployee() {

    var employeeData = new Object();

    employeeData.nik = $('#nik_edit').val();
    employeeData.firstName = $('#firstName_edit').val();
    employeeData.lastName = $('#lastName_edit').val()
    employeeData.gender = $('#gender_edit').val();
    employeeData.brithDate = $('#brithDate_edit').val();
    employeeData.phone = $('#phone_edit').val();
    employeeData.universityId = $('#universityId_edit').val();
    employeeData.degree = $('#degree_edit').val();
    employeeData.gpa = $('#gpa_edit').val();
    employeeData.email = $('#email_edit').val();

    var employeeTable = $('#employeeMasterTable').DataTable();
    $.ajax({
        type: 'PUT',
        url: 'update',
        data: employeeData,
        success: function (data) {
            closeUpdateEmployeeModal();
            employeeTable.ajax.reload();
            alertSuccessUpdate();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError();
        }
    })

}


/*get data employee by nik*/
function getEmployeeByNIK(nik) {
    $.ajax({
        type: 'GET',
        url: 'Detail/'+nik,
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (result) {
            mappingFormDetail(result)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
        }
    })
}


/*get data employee by nik update*/
function getEmployeeUpdateByNIK(nik) {
    $.ajax({
        type: 'GET',
        url: 'Detail/' + nik,
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (result) {
            mappingFormUpdate(result)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
        }
    })
}


/*get data university*/
function getCategory() {
    $.ajax({
        url: 'https://localhost:44359/api/Categories'
    }).done((data) => {
        var categorySelect = '';
        console.log(data);
        $.each(data, function (key, val) {
            categorySelect += `<option value=${val.id}>${val.name}</option>`
        });
        $("#categoryId").html(categorySelect);

    }).fail((error) => {
        console.log(error);
    })
}


/*get data university update*/
function getUniversityUpdate() {
    $.ajax({
        url: 'https://localhost:44300/Universities/GetAll'
    }).done((data) => {
        var universitySelect = '';
        $.each(data, function (key, val) {
            universitySelect += `<option value=${val.universityId}>${val.universityName}</option>`
        });
        $("#universityId_edit").html(universitySelect);

    }).fail((error) => {
        console.log(error);
    })
}


/*delete data employee by nik*/
function deleteEmployee(nik) {
    var employeeTable = $('#employeeMasterTable').DataTable();
    $.ajax({
        type: 'DELETE',
        url: 'Remove/'+nik,
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
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

/*show sweetalert confirmation delete employee*/
function deleteAlert(nik) {
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
        text: 'Are you sure to delete employee NIK : ' + nik + ' ?',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            deleteEmployee(nik);
        }
    })
}

/*show sweetalert error*/
function alertError() {
    Swal.fire({
        icon: 'error',
        text: 'Failed save data!',
    })
}

/*show sweetalert success*/
function alertSuccess() {
    Swal.fire({
        icon: 'success',
        text: 'Successfully save data!',
    })
}

function alertSuccessUpdate() {
    Swal.fire({
        icon: 'success',
        text: 'Successfully update data!',
    })
}


/*helper if modal detail show*/
$('#modalDetailEmployee').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) 
    var recipient = button.data('whatever')
    getEmployeeByNIK(recipient);
});

/*helper if modal update show*/
$('#modalUpdateEmployee').on('show.bs.modal', function (event) {
    getUniversityUpdate();
    var button = $(event.relatedTarget)
    var recipient = button.data('whatever')
    getEmployeeUpdateByNIK(recipient);
});


/*helper include modal detail show*/
function mappingFormDetail(data){
        document.getElementById("firstName_detail").value = data.firstName;
        document.getElementById("lastName_detail").value = data.lastName;
        document.getElementById("gender_detail").value = data.gender;
        var date = new Date(data.brithDate + "Z").toISOString().substring(0, 10);
        document.getElementById("brithDate_detail").value = date;
        document.getElementById("phone_detail").value = data.phone;
        document.getElementById("UniversityId_detail").value = data.universityName;
        document.getElementById("degree_detail").value = data.degree;
        document.getElementById("gpa_detail").value = data.gpa;
        document.getElementById("email_detail").value = data.email;
}


/*helper include modal update */
function mappingFormUpdate(data) {
    document.getElementById("nik_edit").value = data.nik;
    document.getElementById("firstName_edit").value = data.firstName;
    document.getElementById("lastName_edit").value = data.lastName;
    document.getElementById("gender_edit").value = data.gender;
    var date = new Date(data.brithDate + "Z").toISOString().substring(0, 10);
    document.getElementById("brithDate_edit").value = date;
    document.getElementById("phone_edit").value = data.phone;
    document.getElementById("universityId_edit").value = data.universityId;
    document.getElementById("degree_edit").value = data.degree;
    document.getElementById("gpa_edit").value = data.gpa;
    document.getElementById("email_edit").value = data.email;
}

/*helper close modal detail */
function closeDetail() {
    document.getElementById("formDetail").reset();
    $('#modalDetailEmployee').modal('hide');
}

/*helper open modal insert */
$('#modalCreateTicket').on('show.bs.modal', function () {
    getCategory();
});

/*helper close modal insert*/
function closeTicketModal() {
    document.getElementById("formTicket").reset();
    document.getElementById("formTicket").classList.remove('was-validated');
    $('#modalCreateTicket').modal('hide');
}

function closeUpdateEmployeeModal() {
    document.getElementById("formUpdateEmployee").reset();
    document.getElementById("formUpdateEmployee").classList.remove('was-validated');
    $('#modalUpdateEmployee').modal('hide');
}
