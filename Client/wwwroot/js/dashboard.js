$.ajax({
    type: 'GET',
    url: 'https://localhost:44359/API/employees/system/report',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        document.getElementById("count-ticket").innerHTML = result.data.tickets;
        document.getElementById("count-employee").innerHTML = result.data.employees;
        document.getElementById("count-problem").innerHTML = result.data.categories;
        document.getElementById("count-priority").innerHTML = result.data.priorities;
    },
    error: function (jqXHR, textStatus, errorThrown) {
        alertError(errorThrown);
    }
})

let dataPriority = [];
let namePriority = [];

let dataStatus = [];
let nameStatus = [];

$.ajax({
    type: 'GET',
    url: 'https://localhost:44359/API/employees/system/report-priority',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        $.each(result.data, function (key, val) {
            dataPriority.push(val.basePriority);
            namePriority.push(val.priorityName);
        });
     
        BarPriority();

    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(jqXHR);
    }
})


function BarPriority() {
    var options = {
        series: [{
            data: dataPriority,
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
            categories: namePriority,
        }
    };

    var chartBar = new ApexCharts(document.querySelector("#priortyBar"), options);
    chartBar.render();
}

$.ajax({
    type: 'GET',
    url: 'https://localhost:44359/API/employees/system/report-statuses',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        $.each(result.data, function (key, val) {
            dataStatus.push(val.baseStatus);
            nameStatus.push(val.statusName);
        });

        PieStatus();

    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(jqXHR);
    }
})


function PieStatus() {
    var options = {
        series: dataStatus,
        chart: {
            type: 'donut',
        },
        labels: nameStatus,
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

    var chartPie = new ApexCharts(document.querySelector("#ticketDonut"), options);
    chartPie.render();
}
