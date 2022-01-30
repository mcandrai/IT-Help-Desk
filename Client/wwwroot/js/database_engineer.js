$(document).ready(function () {
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
                'render': function (data) {
                    var link = `<a href="ticket-detail/${data.id}">${data.id}</a>`
                    return link;
                }
            },
            {
                'data': 'categoryName'
            },
            {
                'data': 'employeeName',
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
            {
                'data': null,
                'bSortable': false,
                'render': function (data) {
                    var actionButton = `
                                        <button class="btn btn-sm btn-primary" data-toggle="modal" data-target="#modalEscalation" data-whatever="${data.id}"><i class="fas fa-check-circle" aria-hidden='true'></i></button>
                                       `
                    return actionButton;
                }
            }
        ]
    });
});


function UpdateTicketDatabase(id) {
    var ticketTable = $('#ticketTable').DataTable();
    var ticketData = new Object();
    ticketData.id = id;
    $.ajax({
        type: 'POST',
        url: 'tickets/UpdateTicketDatabase',
        data: ticketData,
        success: function (data) {
            closeEscalationModal();
            ticketTable.ajax.reload();
            alertSuccessUpdate();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError();
        }
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
        text: 'Successfully Done Ticket!',
    })
}




$('#modalEscalation').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');  
    var modal = $(this);
    modal.find('#escalationTicket').text(recipient);
    var data = '';
    data = `
<button type="button" class="btn btn-secondary" onclick=" closeEscalationModal()">Cancel</button>
<button class="btn btn-primary" onClick="UpdateTicketDatabase(${recipient})">Finish</button>`
    $("#ticketData").html(data);
});


function closeEscalationModal() {
    $('#modalEscalation').modal('hide');
}