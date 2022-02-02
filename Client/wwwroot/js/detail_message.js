var full_url = document.URL;
var url_array = full_url.split('/')
var last_segment = url_array[url_array.length - 1];

Triggerdata();

let nik = "";
function Triggerdata() {
    $.ajax({
        url: 'https://localhost:44323/accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        nik = result.nik;
    }).fail(error => {
        console.log(error);
    })
}

GetDetailTicket(last_segment);

function GetDetailTicket(last_segment) {
    $.ajax({
        type: 'GET',
        url: 'tickets/View-Ticket-Detail/' + last_segment,
    }).done((data) => {
        document.getElementById("ticket-id").innerHTML = "Ticket " + data.id + " " + data.categoryName;
        var employee = `<div class="p-2">${data.userName} <span class="badge badge-secondary">Employee</span></div>`;
        $("#userfullname").html(employee);
        //var date = new Date(data.createAt + "Z").toISOString().substring(0, 10);
        //let result = date.replace(/-/g, ",");
        //document.getElementById("createdate").innerHTML = new Date(result).toLocaleString('en-GB');
        document.getElementById("message").innerHTML = data.message;
        GetMessageDetail();
    }).fail((error) => {
        console.log(error);
    })

}

function GetMessageDetail() {
    $.ajax({
        url: 'https://localhost:44359/api/Tickets/View-Message-Detail/' + last_segment
    }).done((data) => {
        var messageDetail = '';
        $.each(data, function (key, val) {
            messageDetail += ` <div class="card shadow-sm bg-light mb-3">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div class="p-2">${val.fullName} <span class="badge ${val.name}">${val.name}</span></div>
                        <div class="p-2 small"> ${val.createAt}</div>
                    </div>
                    <div class="d-flex flex-column">
                        <div class="p-2">
                           ${val.messageText}
                        </div>
                    </div>
                </div>
            </div>`
        });
        $("#detail-message").html(messageDetail);

    }).fail((error) => {
        console.log(error);
    })
}

function CreateMessage() {
    var ticketData = new Object();
    ticketData.nik = nik;
    ticketData.messageId = last_segment;
    ticketData.messageText = $('#messageText').val()
    $.ajax({
        type: 'POST',
        url: 'https://localhost:44323/messages/Create-Message',
        data: ticketData,
        success: function (data) {
            document.getElementById("formTicket").reset();
            document.getElementById("formTicket").classList.remove('was-validated');
            GetMessageDetail();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            console.log(error);
        }
    })
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
                    CreateMessage();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


