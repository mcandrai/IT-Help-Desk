﻿Triggerdata();
function Triggerdata() {
    $.ajax({
        url: 'accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        GetData(result.nik)
    }).fail(error => {
        console.log(error);
    })
}

function GetData(nik) {
    $('#ticketTable').DataTable({
        "ajax": {
            'url': 'https://localhost:44359/api/Tickets/View-Ticket-User?nik=' + nik,
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
        'columns': [
            {
                'data': 'id'
            },
            {
                'data': 'categoryName'
            },
            {
                'data': null,
                'render': function () {
                    var value = "under maintance";
                    return value;
                }
            },
            {
                'data': 'statusName'
            },
          
            {
                'data': null,
                'bSortable': false,
                'render': function (data) {
                    //var actionButton = `<button class="btn btn-sm btn-primary" data-toggle="modal" data-target="#modalDetailEmployee" data-whatever="${data.id}"><i class="fas fa-info-circle" aria-hidden='true'></i></button>
                    //                    <button class="btn btn-sm btn-success" data-toggle="modal" data-target="#modalUpdateEmployee" data-whatever="${data.id}"><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>
                    //                    <button class="btn btn-sm btn-danger" onclick="deleteAlert(${data.id})"><i class="fas fa-trash-alt" aria-hidden='true'></i></button>`
                    //return actionButton;
                    var button = 'under maintance';
                    return button;
                }
            }
        ]
    });

}


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
                    StoreTicket();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


function StoreTicket() {
    var ticketData = new Object();

    ticketData.categoryId = parseInt($('#categoryId').val());
    ticketData.nik = $('#nik').val();
    ticketData.message = $('#message').val();


    var ticketTable = $('#ticketTable').DataTable();

    $.ajax({
        type: 'POST',
        url: 'tickets/create-ticket',
        data: ticketData,
        success: function (data) {
            closeTicketModal();
            ticketTable.ajax.reload();
            alertSuccess();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            console.log(error);
        }
    })

}


function GetCategory() {
    $.ajax({
        url: 'https://localhost:44359/api/Categories'
    }).done((data) => {
        var categorySelect = '';
        $.each(data, function (key, val) {
            categorySelect += `<option value=${val.id}>${val.name}</option>`
        });
        $("#categoryId").html(categorySelect);

    }).fail((error) => {
        console.log(error);
    })
}

function GetNIK() {
    $.ajax({
        url: 'accounts/get-data-login'
    }).done((data) => {
        var nik = data.nik;
        document.getElementById("nik").value = nik;
    }).fail((error) => {
        console.log(error);
    })
}


function alertError() {
    Swal.fire({
        icon: 'error',
        text: 'Failed save data!',
    })
}


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

$('#modalCreateTicket').on('show.bs.modal', function () {
    GetCategory();
    GetNIK();
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
