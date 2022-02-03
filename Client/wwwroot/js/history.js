$(document).ready(function () {
    $('#ticketTable').DataTable({
        "order": [[0,"desc"]],
        "ajax": {
            'url': 'tickets/View-Ticket-History',
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
                    var actionButton = `<a class="btn btn-sm btn-warning" href="ticket-history/${data.id}" role="button"><i class="fas fa-comment-dots" aria-hidden='true'></i></a>`
                    return actionButton;
                }
            }
        ]
    });
});
