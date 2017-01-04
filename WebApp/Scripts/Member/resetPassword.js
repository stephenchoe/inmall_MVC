var updateTargetId = "resetPassword-form";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var resetPasswordFailedTitle = "重設密碼失敗";
function resetPasswordFailedTitleSetter(value) {
    resetPasswordFailedTitle = value;
}

var resetPasswordFailedMsg = "系統暫時無回應，請稍後再試。";
function resetPasswordFailedMsgSetter(value) {
    resetPasswordFailedMsg = value;
}

var resetPasswordedTitle = "重設密碼成功";
function resetPasswordedTitleSetter(value) {
    resetPasswordedTitle = value;
}

var resetPasswordedMsg = "";
function resetPasswordedMsgSetter(value) {
    resetPasswordedMsg = value;
}

var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}

var returnUrl = "/";

function resetPasswordBegin() {
    //Loading(updateTargetId);
}
function resetPasswordSuccess() {
    StopLoading(updateTargetId);
}
function resetPasswordFailed() {
    StopLoading(updateTargetId);

    var settings = {
        type: 'error',
        title: resetPasswordFailedTitle,
        text: resetPasswordFailedMsg,

        showConfirmButton: true
    };

    swal(settings);

}

function resetPasswordError(callback) {
    var settings = {
        type: 'error',
        title: resetPasswordFailedTitle,
        text: resetPasswordFailedMsg,

        showConfirmButton: true
    };
    if (callback) {
        swal(settings, callback);
    } else {
        swal(settings);
    }

}

function resetPassworded() {
    var settings = {
        type: 'success',
        title: resetPasswordedTitle,
        text: resetPasswordedMsg,

        showConfirmButton: true
    };

    swal(settings, function () {
        returnBackPage();
    });

}


function returnBackPage() {
    window.location.href = returnUrl;
}
