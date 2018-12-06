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