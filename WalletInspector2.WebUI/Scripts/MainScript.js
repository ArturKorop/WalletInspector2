function onSuccessAddExpense() {
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

    UpdateWeekStatistics();
    UpdateMonthStatistics();
}

function onSuccessRemoveExpense() {
    UpdateWeekStatistics();
    UpdateMonthStatistics();
}

function onSuccessUpdateExpense() {
    UpdateWeekStatistics();
    UpdateMonthStatistics();
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
    $.ajax({
        url: "/Home/GetMonthData",
        contentType: 'application/html; charset=utf-8',
        type: 'Post',
        dataType: 'json'
    })
            .success(function (result) {

                var colors = Highcharts.getOptions().colors,
                tagData = [],
                expenseData = [],
                i,
                j,
                drillDataLen,
                tag,
                brightness;

                for (i = 0; i < result.length; i++) {

                    tag = result[i];
                    tagData.push({
                        name: tag.Name,
                        y: tag.TotalAmount,
                        color: colors[i]
                    })

                    drillDataLen = tag.Expenses.length;
                    for (j = 0; j < drillDataLen; j++) {
                        brightness = 0.2 - (j / drillDataLen) / 5;
                        expenseData.push({
                            name: tag.Expenses[j].Name,
                            y: tag.Expenses[j].TotalAmount,
                            color: Highcharts.Color(colors[i]).brighten(brightness).get()
                        });
                    }
                }

                $('#monthStat').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Month'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.y:.1f}uah</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<p style="font-size:0.8em;">{point.name} {point.percentage:.1f}%:</p><p> {point.y:.1f} uah</p>',
                                style: {
                                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                }
                            },
                            showInLegend: false
                        }
                    },
                    series: [{
                        name: "Tags",
                        data: tagData,
                        size: '60%',
                        dataLabels: {
                            formatter: function () {
                                return this.y > 5 ? this.point.name : null;
                            },
                            color: 'white',
                            distance: -30
                        }
                    }, {
                        name: 'Expenses',
                        data: expenseData,
                        size: '80%',
                        innerSize: '60%',
                        dataLabels: {
                            formatter: function () {
                                // display only if larger than 1
                                return this.y > 1 ? '<b>' + this.point.name + ':</b> ' + this.y + '%' : null;
                            }
                        }
                    }]
                });
            });

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
                        pointFormat: '{series.name}: <b>{point.y:.1f}uah</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<p style="font-size:.8em;">{point.name} {point.percentage:.1f}%:</p> <p>{point.y:.1f} uah</p>',
                                style: {
                                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                }
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