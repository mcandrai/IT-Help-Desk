Triggerdata();
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
            'url': 'tickets/View-Ticket-User/' + nik,
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
                'data': null,
                'render': function (data) {
                    var link = `<a href="ticket-detail/${data.id}">${data.id}</a>`
                    return link;
                }
            },
            {
                'data': 'categoryName'
            },
            {
                'data': null,
                'render': function (data) {
                    var name = `<span class="label badge-pill ${data.priorityName}">${data.priorityName}</span>`
                        return name;
                }
            },
            {
                'data': null,
                'render': function (data) {
                    var status = `<span class="${data.statusName}">${data.statusName}</span>`
                    return status;
                }
            },
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

let nik_ticket;
function StoreTicket() {
    var ticketData = new Object();

    ticketData.categoryId = parseInt($('#categoryId').val());
    ticketData.nik = nik_ticket;
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
        var result = data.nik;
        nik_ticket = result;
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
        text: 'Successfully sent ticket!',
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
