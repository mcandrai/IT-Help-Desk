var options = {
    series: [{
        data: [20, 45, 12]
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
        categories: ['Low', 'Mid', 'High'],
    }
};

var chartBar = new ApexCharts(document.querySelector("#priortyBar"), options);
chartBar.render();


var options = {
    series: [44, 55, 41],
    chart: {
        type: 'donut',
    },
    labels: ["New", "Waiting Reply", "Replied"],
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