
/*show report data*/

$.ajax({
    type: 'GET',
    url: 'https://localhost:44300/employees/report',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        console.log(result);
        document.getElementById("count-employee").innerHTML = result.data.employee;
        document.getElementById("count-account").innerHTML = result.data.account;
        document.getElementById("count-education").innerHTML = result.data.education;
        document.getElementById("count-university").innerHTML = result.data.university;
    },
    error: function (jqXHR, textStatus, errorThrown) {
        alertError(errorThrown);
    }
})


/*show bar chart*/

let dataEmployee = [];
let dataUniversity = [];
let dataGenderName = [];
let dataGender = [];

$.ajax({
    type: 'GET',
    url: 'https://localhost:44321/API/employees/register/base-university',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        $.each(result.data, function (key, val) {
            dataEmployee.push(val.baseUniversity);
            dataUniversity.push(val.universityName);
        });
        createBar();
    },
    error: function (jqXHR, textStatus, errorThrown) {
        alertError(errorThrown);
    }
})

$.ajax({
    type: 'GET',
    url: 'https://localhost:44321/API/employees/register/base-gender',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        $.each(result.data, function (key, val) {
            dataGenderName.push(val.nameGender);
            dataGender.push(val.baseGender);
        });
        createDonut();
    },
    error: function (jqXHR, textStatus, errorThrown) {
        alertError(errorThrown);
    }
})

function createBar() {
    var options = {
        series: [{
            data: dataEmployee
        }],
        chart: {
            type: 'bar',
            height: 350
        },
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: false,
            }
        },
        dataLabels: {
            enabled: false
        },
        xaxis: {
            categories: dataUniversity,
        }
    };

    var chart = new ApexCharts(document.querySelector("#employee-bar"), options);
    chart.render();
}


/*show donut chart*/
function createDonut() {
    var options = {
        series: dataGender,
        chart: {
            type: 'donut',
        },
        labels: dataGenderName,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#employee-donut"), options);
    chart.render();
}

