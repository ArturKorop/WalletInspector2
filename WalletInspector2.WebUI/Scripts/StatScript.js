function ShowMonthsChart(data) {

    var chartData = [];
    for (var i = 0; i < data.length; i++) {
        chartData.push(data[i].TotalValue);
    }

    $('#YearPerMonthStat').highcharts({
        chart: {
            type: 'line'
        },
        title: {
            text: 'Statistic per monthes'
        },        
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Total value (uah)'
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true,
                    format: '<p style="font-size:0.7em;">{point.y:.0f}</p>'
                },
                enableMouseTracking: false
            }
        },
        series: [{
            data: chartData,
            name: '2014'
        }]
    });
}

function ShowFullYearChart(source) {
    var colors = Highcharts.getOptions().colors,
    tagData = [],
    expenseData = [],
    i,
    j,
    drillDataLen,
    tag,
    brightness,
    data = source.Tags;
    
    for (i = 0; i < data.length; i++) {

        tag = data[i];
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



    $('#FullYearStat').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: 'Year: ' + source.TotalValue + 'uah'
        },
        tooltip: {
            pointFormat: '<b>{point.y:.1f} uah - {point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<p style="font-size:0.8em;">{point.name}: {point.y:.0f}</p>',
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
                    return this.y > 1 ? '<b>' + this.point.name + ':</b> ' + '<br>' + this.y + '</br>' + '%' : null;
                }
            }
        }]
    });
}

function ShowFullMonthChart(source, title) {
    var colors = Highcharts.getOptions().colors,
    tagData = [],
    expenseData = [],
    i,
    j,
    drillDataLen,
    tag,
    brightness,
    data = source.Tags;

    for (i = 0; i < data.length; i++) {

        tag = data[i];
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



    $('#fullMonthStat').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: title + ': ' + source.TotalValue + 'uah'
        },
        tooltip: {
            pointFormat: '<b>{point.y:.1f} uah - {point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<p style="font-size:0.8em;">{point.name}: {point.y:.0f}</p>',
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
                    return this.y > 1 ? '<b>' + this.point.name + ':</b> ' + '<br>' + this.y + '</br>' + '%' : null;
                }
            }
        }]
    });
}