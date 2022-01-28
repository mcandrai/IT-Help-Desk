var full_url = document.URL; 
var url_array = full_url.split('/')
var last_segment = url_array[url_array.length-1];  
console.log(last_segment);
Triggerdata();
let nik = "";
function Triggerdata() {
    $.ajax({
        url: 'https://localhost:44323/accounts/get-data-login',
        dataType: "json",
        dataSrc: ""
    }).done(result => {
        nik = result.nik;
        console.log(nik);
    }).fail(error => {
        console.log(error);
    })
}
getDetailTicket(last_segment);

function createMessage() {
    var ticketData = new Object();
    ticketData.nik = nik;
    ticketData.messageId = last_segment;
    ticketData.messageText = $('#messageText').val()
    console.log(ticketData);
    $.ajax({
        type: 'POST',
        url: 'https://localhost:44359/api/Messages/Create-Message',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(ticketData),
        success: function (data) {
            closeTicketModal();
            alertSuccess();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var error = jqXHR.responseJSON;
            console.log(error);
        }
    })
}
function getDetailTicket(last_segment){
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44359/api/Tickets/View-Ticket-Detail?ID=' + last_segment,
    }).done((data) => {
        console.log(data);
        document.getElementById("userfullname").innerHTML = data.userName;
        document.getElementById("idTicket").innerHTML = data.id;
        document.getElementById("message").innerHTML = data.message;
        document.getElementById("createAt").innerHTML = data.createAt;
        document.getElementById("priority").innerHTML = data.priorityName;
        document.getElementById("status").innerHTML = data.statusName;
        document.getElementById("category").innerHTML = data.categoryName;
        GetMessageDetail();
    }).fail((error) => {
        console.log(error);
    })

}

function GetMessageDetail() {
    $.ajax({
        url: 'https://localhost:44359/api/Tickets/View-Message-Detail?id=' + last_segment
    }).done((data) => {
        var categorySelect = '';
        $.each(data, function (key, val) {
            categorySelect += `<p>Tanggal:${val.createAt}'</p>
                               <p>Role:${val.name}'</p>
                               <p>Message:${val.messageText}'</p>`
        });
        $("#categoryId").html(categorySelect);

    }).fail((error) => {
        console.log(error);
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
                    createMessage();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

/*Nampilin Nama di user profil*/
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

function closeTicketModal() {
    document.getElementById("formTicket").reset();
    document.getElementById("formTicket").classList.remove('was-validated');
    $('#modalCreateTicket').modal('hide');
}