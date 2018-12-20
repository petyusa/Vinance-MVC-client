var vinance = (function () {
    function showAlert(data) {
        const icon = data.class === "success" ? "fas fa-check" : "fas fa-exclamation-triangle";
        const alertHtml = `
            <div class="alert alert-${data.class} alert-dismissible fade show">
                <span class="mr-2"><i class="${icon}"></i></span><span>${data.message}</span>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>`;
        const myAlert = $("#alert-div");
        myAlert.empty();
        myAlert.append(alertHtml);
        myAlert.fadeIn();
        setTimeout(() => {
            myAlert.fadeOut();
        }, 2000);
    }

    function initializeDatetimepicker(datetimepickerId) {
        const input = $(`#${datetimepickerId}`);
        input.datetimepicker({
            format: "L"
        });
    }

    const backgroundColors = [
        "#B58900",
        "#6610f2",
        "#D33682",
        "#fd7e14",
        "#20c997",
        "#839496",
        "#268BD2",
        "#CB4B16",
        "green",
        "#2AA198",
        "#073642",
        "#6f42c1",
        "#e83e8c"
    ];

    function createChart(chartId, labels, amounts) {
        const canvasDiv = document.getElementById(chartId);
        canvasDiv.height = 400;
        const ctx = canvasDiv.getContext('2d');
        const data = {
            labels: labels,
            datasets: [{
                data: amounts,
                backgroundColor: backgroundColors
            }]
        };
        const options = {
        };
        const myPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    }

    function createStackedChart(chartId, labels, transactionCategories) {
        const canvasDiv = document.getElementById(chartId);
        const ctx = canvasDiv.getContext('2d');
        const data = {
            labels: labels.map((item) => {
                return moment(new Date(item)).format("YY-MMM");
            }),
            datasets: getDataSets(transactionCategories)
        };
        const myPieChart = new Chart(ctx,
            {
                type: 'bar',
                data: data,
                options: {
                    responsive: true,
                    scales: {
                        xAxes: [{
                            stacked: true
                        }],
                        yAxes: [{
                            stacked: true,
                            ticks: {
                                callback: function (value, index, values) {
                                    return value + " Ft";
                                }
                            }
                        }]
                    }
                }
            });
    }

    function getDataSets(arr) {
        const labels = getLabels(arr);
        const dataSets = [];
        for (let i = 0; i < labels.length; i++) {
            dataSets.push({
                stack: "stack",
                label: labels[i],
                data: getDataForCategory(arr, labels[i]),
                backgroundColor: backgroundColors[i]
            });
        }
        return dataSets;
    }

    function getLabels(arr) {
        const labels = [];
        for (let i = 0; i < arr.length; i++) {
            for (let j = 0; j < arr[i].length; j++) {
                if (!labels.includes(arr[i][j].Name)) {
                    labels.push(arr[i][j].Name);
                }
            }
        }
        return labels.sort();
    }

    function getDataForCategory(arr, categoryName) {
        const data = [];
        for (let i = 0; i < arr.length; i++) {
            for (let j = 0; j < arr[i].length; j++) {
                if (arr[i][j].Name === categoryName) {
                    data.push(arr[i][j].Balance);
                }
            }
        }
        return data;
    }

    return {
        showAlert: showAlert,
        initDatePicker: initializeDatetimepicker,
        createChart: createChart,
        createStackedChart: createStackedChart
    };
})();


$.validator.addMethod("notequalto", function (value, element, params) {
    const other = $(`#${params.other}`);
    return other.val() !== value;
});

$.validator.unobtrusive.adapters.add("notequalto", ["other"], function (options) {
    const params = {
        other: options.params.other
    };
    options.rules["notequalto"] = params;
    options.messages["notequalto"] = options.message;
});