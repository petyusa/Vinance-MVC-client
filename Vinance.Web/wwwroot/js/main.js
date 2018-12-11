var vinance = (function () {
    function showAlert(data) {
        const icon = data.class === "success" ? "check" : "alert-triangle";
        const alertHtml = `
            <div class="alert alert-${data.class} alert-dismissible fade show">
                <span class="mr-2"><i data-feather="${icon}"></i></span><span>${data.message}</span>
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

    function createChart(chartId, labels, amounts) {
        const canvasDiv = document.getElementById(chartId);
        canvasDiv.height = 400;
        const ctx = canvasDiv.getContext('2d');
        const data = {
            labels: labels,
            datasets: [{
                data: amounts,
                backgroundColor: [
                    "#B58900",
                    "#6610f2",
                    "#D33682",
                    "#fd7e14",
                    "#20c997",
                    "#839496",
                    "#268BD2",
                    "#CB4B16",
                    "#FDF6E3",
                    "#2AA198",
                    "#073642",
                    "#6f42c1",
                    "#e83e8c"
                ]
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

    return {
        showAlert: showAlert,
        initDatePicker: initializeDatetimepicker,
        createChart: createChart
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