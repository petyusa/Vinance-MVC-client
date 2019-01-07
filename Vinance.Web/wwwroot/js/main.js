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

    function getColors() {
        const bodyStyles = window.getComputedStyle(document.body);
        return [
            bodyStyles.getPropertyValue("--blue"),
            bodyStyles.getPropertyValue("--green"),
            bodyStyles.getPropertyValue("--pink"),
            bodyStyles.getPropertyValue("--yellow"),
            bodyStyles.getPropertyValue("--info"),
            bodyStyles.getPropertyValue("--indigo"),
            bodyStyles.getPropertyValue("--red"),
            bodyStyles.getPropertyValue("--teal"),
            bodyStyles.getPropertyValue("--orange"),
            bodyStyles.getPropertyValue("--warning"),
            bodyStyles.getPropertyValue("--cyan"),
            bodyStyles.getPropertyValue("--success"),
            bodyStyles.getPropertyValue("--primary"),
            bodyStyles.getPropertyValue("--danger"),
            bodyStyles.getPropertyValue("--secondary"),
            bodyStyles.getPropertyValue("--purple"),
            bodyStyles.getPropertyValue("--gray")
        ];
    }

    function createChart(chartId, labels, amounts) {
        const canvasDiv = document.getElementById(chartId);
        canvasDiv.height = 400;
        const ctx = canvasDiv.getContext('2d');
        const data = {
            labels: labels,
            datasets: [{
                data: amounts,
                backgroundColor: getColors()
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

    function createDailyBalanceChart(chartId, labels, balances) {
        const canvasDiv = document.getElementById(chartId);
        canvasDiv.height = 400;
        const ctx = canvasDiv.getContext('2d');
        const data = {
            labels: labels.map((item) => {
                return moment(new Date(item)).format("YY-MM-DD");
            }),
            datasets: [{
                label: "Egyenleg",
                data: balances,
                backgroundColor: getColors()[0]
            }]
        };
        const options = {
            maintainAspectRatio: false,
            elements: {
                point: {
                    radius: 0
                }
            },
            scales: {
                yAxes: [
                    {
                        display: true,
                        ticks: {
                            beginAtZero: true,
                            callback: function (value, index, values) {
                                return value + " Ft";
                            }
                        }
                    }]
            }
        };
        const myPieChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
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
                backgroundColor: getColors()[i]
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
        createStackedChart: createStackedChart,
        createDailyBalanceChart: createDailyBalanceChart
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

function setTheme(theme) {
    document.cookie = `theme=${theme}`;
    location.reload();
}

$(function () {
    const theme = document.cookie.replace(/(?:(?:^|.*;\s*)theme\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (theme === "dark") {
        $("#theme-change-btn").bootstrapToggle("off");
    } else {
        $("#theme-change-btn").bootstrapToggle("on");
    }

    $("#theme-change-btn").change(function () {
        if ($(this).prop("checked")) {
            setTheme("light");
        } else {
            setTheme("dark");
        }
    });
});