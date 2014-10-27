function onSuccess() {
    $(".InputNameClass").val('')
    $(".InputValueClass").val('')
    $(".InputTagClass").val('')

    $(".updateButton").button({
        icons: {
            primary: "ui-icon-refresh"
        },
        text: false
    });

    $(".removeButton").button({
        icons: {
            primary: "ui-icon-minusthick"
        },
        text: false
    });
}

function UpdateButtons() {
    $(".addButton").button({
        icons: {
            primary: "ui-icon-plusthick"
        },
        text: false
    });
    $(".updateButton").button({
        icons: {
            primary: "ui-icon-refresh"
        },
        text: false
    });
    $(".removeButton").button({
        icons: {
            primary: "ui-icon-minusthick"
        },
        text: false
    });
}

function UpdatePrevNextButtons(){
    $("#ButtonNext").button({
        icons: {
            primary: "ui-icon-circle-triangle-e"
        },
        text: false
    });
    $("#ButtonPrev").button({
        icons: {
            primary: "ui-icon-circle-triangle-w"
        },
        text: false
    });
}

function UpdateMonthStatistics() {
    UpdateStatistics("/Home/GetMonthData", '#monthStat', 'Month');
}

function UpdateWeekStatistics() {
    UpdateStatistics("/Home/GetWeekData", '#weekStat', 'Week');
}

function UpdateYearStatistics() {
    UpdateStatistics("/Home/GetYearData", '#yearStat', 'Year');
}

function UpdateStatistics(currentUrl, target, caption) {
    $.ajax({
        url: currentUrl,
        contentType: 'application/html; charset=utf-8',
        type: 'Post',
        dataType: 'json'
    })
            .success(function (result) {

                $(target).highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false
                    },
                    title: {
                        text: caption
                    },
                    tooltip: {
                        pointFormat: '{series.name} - {point}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true
                            },
                            showInLegend: false
                        }
                    },
                    series: [{
                        type: 'pie',
                        name: 'Browser share',
                        data: result
                    }]
                });
            });

}