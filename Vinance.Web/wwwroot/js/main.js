function showAlert(data) {
    const alertHtml = `
            <div class="alert alert-${data.class} alert-dismissible fade show">
                ${data.message}
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
`;
    const myAlert = $("#alert-div");
    myAlert.empty();
    myAlert.append(alertHtml);
    myAlert.fadeIn();
    setTimeout(() => {
        myAlert.fadeOut();
    }, 2000);
}