

function InitSubjectsChart() {
    var ctx1 = document.getElementById("SubjectsCanvas");

    var myBarChart = new Chart(ctx1, {
        type: 'bar',
        options: {
            global: {
                responsive: true,
                maintainAspectRatio: false
            }
        },
        data: {
            labels: app.topSubjectsChartLabels,
            datasets: [
                {
                    type: 'bar',
                    label: "Subjects",
                    backgroundColor: [
                            "#BDC3C7",
                            "#9B59B6",
                            "#E74C3C",
                            "#26B99A",
                            "#3498DB"
                    ],
                    borderWidth: 1,
                    data: app.topSubjectsChartValues,
                }
            ]
        },
    });
}

function InitLearningObjectiveChart() {
    var options = {
        legend: false,
        responsive: false
    };

    new Chart(document.getElementById("LearningObjectiveCanvas"), {
        type: 'doughnut',
        tooltipFillColor: "rgba(51, 51, 51, 0.55)",
        data: {
            labels: app.topLearningObjectsChartLabels,
            datasets: [{
                data: app.topLearningObjectsChartValues,
                backgroundColor: [
                  "#BDC3C7",
                  "#9B59B6",
                  "#E74C3C",
                  "#26B99A",
                  "#3498DB"
                ],
                hoverBackgroundColor: [
                  "#CFD4D8",
                  "#B370CF",
                  "#E95E4F",
                  "#36CAAB",
                  "#49A9EA"
                ]
            }]
        },
        options: options
    });
}

function Initdatatable() {
    var handleDataTableButtons = function () {
        if ($("#datatable-buttons").length) {
            $("#datatable-buttons").DataTable({
                dom: "Bfrtip",
                buttons: [
                  {
                      extend: "copy",
                      className: "btn-sm"
                  },
                  {
                      extend: "csv",
                      className: "btn-sm"
                  },
                  {
                      extend: "excel",
                      className: "btn-sm"
                  },
                  {
                      extend: "pdfHtml5",
                      className: "btn-sm"
                  },
                  {
                      extend: "print",
                      className: "btn-sm"
                  },
                ],
                responsive: true
            });
        }
    };

    TableManageButtons = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            }
        };
    }();

    TableManageButtons.init();
}

function InitOverviewChart() {

    var data1 = new Array();
    var data2 = new Array();
    app.progressData.forEach(function (element) {
        var arr = new Array(element.Time, element.Value);
        data1.push(arr);
    });

    app.correctData.forEach(function (element) {
        var arr = new Array(element.Time, element.Value);
        data2.push(arr);
    });

    $("#canvas_dahs").length && $.plot($("#canvas_dahs"),
     [data1, data2],
     {
         series: {
             lines: {
                 show: false,
                 fill: true
             },
             splines: {
                 show: true,
                 tension: 0.4,
                 lineWidth: 1,
                 fill: 0.4
             },
             points: {
                 radius: 0,
                 show: true
             },
             shadowSize: 2
         },
         grid: {
             verticalLines: true,
             hoverable: true,
             clickable: true,
             tickColor: "#d5d5d5",
             borderWidth: 1,
             color: '#fff'
         },
         colors: ["rgba(38, 185, 154, 0.38)", "rgba(3, 88, 106, 0.38)"],
         xaxis: {
             tickColor: "rgba(51, 51, 51, 0.06)",
             mode: "time",
             tickLength: 10,
             axisLabel: "Date",
             axisLabelUseCanvas: true,
             axisLabelFontSizePixels: 12,
             axisLabelFontFamily: 'Verdana, Arial',
             axisLabelPadding: 10
         },
         yaxis: {
             ticks: 8,
             tickColor: "rgba(51, 51, 51, 0.06)",
         },
         tooltip: false
     });
}