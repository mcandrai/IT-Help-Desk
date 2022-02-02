$(document).ready(function () {
    $('#ticketTable').DataTable({
        "order": [[0,"desc"]],
        "ajax": {
            'url': 'https://localhost:44359/api/Tickets/View-Ticket-HelpDesk',
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
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },
            {
                className: 'buttonPdf',
                text: '<i class="fa fa-file-pdf-o"></i>',
                extend: 'pdfHtml5',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },

        ],
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return (meta.row + meta.settings._iDisplayStart + 1);
                }
            },
            {
                'data': 'categoryName'
            },
            {
                'data': 'employeeName'
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
                    var actionButton = `<a class="btn btn-sm btn-warning" href="ticket-detail/${data.id}" role="button"><i class="fas fa-comment-dots" aria-hidden='true'></i></a>
                                        <button class="btn btn-sm btn-primary" data-toggle="modal" data-target="#modalEscalation" data-whatever="${data.id}"><i class="fas fa-arrow-circle-up" aria-hidden='true'></i></button>
                                        <button class="btn btn-sm btn-success" data-toggle="modal" data-target="#modalDone" data-whatever="${data.id}"> <i class="fas fa-check-circle" aria-hidden='true'></i></button>
                                        <button class="btn btn-sm btn-dark" data-toggle="modal" data-target="#modalReport" data-whatever="${data.id}"> <i class="fas fa-share-square" aria-hidden='true'></i></button>
                                        `
                    return actionButton;
                }
            }
        ]
    });
});


function UpdateTicket(id) {
    var ticketTable = $('#ticketTable').DataTable();
    var ticketData = new Object();
    ticketData.id = id;
    $.ajax({
        type: 'POST',
        url: 'tickets/UpdateTicket',
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

function alertErrorReport() {
    Swal.fire({
        icon: 'error',
        text: 'Failed Report Ticket, Done Ticket First!',
    })
}

function alertSuccessReport() {
    Swal.fire({
        icon: 'success',
        text: 'Successfully Report ticket!',
    })
}

function alertSuccessUpdate() {
    Swal.fire({
        icon: 'success',
        text: 'Successfully Escalation Ticket!',
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
        <button class="btn btn-primary" onClick="UpdateTicket(${recipient})">Move</button>`
    $("#ticketData").html(data);
});


function closeEscalationModal() {
    $('#modalEscalation').modal('hide');
}


function UpdateTicketDatabase(id) {
    var ticketTable = $('#ticketTable').DataTable();
    var ticketData = new Object();
    ticketData.id = id;
    $.ajax({
        type: 'POST',
        url: 'tickets/UpdateTicketDatabase',
        data: ticketData,
        success: function (data) {
            closeDoneModal();
            ticketTable.ajax.reload();
            alertSuccessUpdate();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError();
        }
    })

}
$('#modalDone').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');
    var modal = $(this);
    modal.find('#doneTicket').text(recipient);
    var data = '';
    data = `
<button type="button" class="btn btn-secondary" onclick=" closeDoneModal()">Cancel</button>
<button class="btn btn-primary" onClick="UpdateTicketDatabase(${recipient})">Finish</button>`
    $("#doneData").html(data);
});


function closeDoneModal() {
    $('#modalDone').modal('hide');
}


function ReportTicket(id) {
    var ticketTable = $('#ticketTable').DataTable();
    var ticketData = new Object();
    ticketData.id = id;
    $.ajax({
        type: 'POST',
        url: 'tickets/UpdateTicketDone',
        data: ticketData,
        success: function (data) {
            if (!data) {
                closeReportModal();
                ticketTable.ajax.reload();
                alertErrorReport();
            }
            else {
            closeReportModal();
            ticketTable.ajax.reload();
                alertSuccessReport();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            alertError();
        }
    })

}
$('#modalReport').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');
    var modal = $(this);
    modal.find('#reportTicket').text(recipient);
    var data = '';
    data = `
<button type="button" class="btn btn-secondary" onclick=" closeReportModal()">Cancel</button>
<button class="btn btn-primary" onClick="ReportTicket(${recipient})">Finish</button>`
    $("#ReportData").html(data);
});


function closeReportModal() {
    $('#modalReport').modal('hide');
}