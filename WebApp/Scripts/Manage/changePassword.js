var updateTargetId = "changePassword-form";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var changePasswordFailedTitle = "變更密碼失敗";
function changePasswordFailedTitleSetter(value) {
    changePasswordFailedTitle = value;
}

var changePasswordFailedMsg = "系統暫時無回應，請稍後再試。";
function changePasswordFailedMsgSetter(value) {
    changePasswordFailedMsg = value;
}

var changePasswordedTitle = "變更密碼成功";
function changePasswordedTitleSetter(value) {
    changePasswordedTitle = value;
}

var changePasswordedMsg = "";
function changePasswordedMsgSetter(value) {
    changePasswordedMsg = value;
}

var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}

var returnUrl = "/";

function changePasswordBegin() {
    //Loading(updateTargetId);
}
function changePasswordSuccess() {
    StopLoading(updateTargetId);
}
function changePasswordFailed() {
    StopLoading(updateTargetId);

    var settings = {
        type: 'error',
        title: changePasswordFailedTitle,
        text: changePasswordFailedMsg,

        showConfirmButton: true
    };

    swal(settings);

}

function changePasswordError(callback) {
    var settings = {
        type: 'error',
        title: changePasswordFailedTitle,
        text: changePasswordFailedMsg,

        showConfirmButton: true
    };
    if (callback) {
        swal(settings, callback);
    } else {
        swal(settings);
    }

}

function changePassworded() {
    var settings = {
        type: 'success',
        title: changePasswordedTitle,
        text: changePasswordedMsg,

        showConfirmButton: true
    };

    swal(settings, function () {
        returnBackPage();
    });

}


function returnBackPage() {
    window.location.href = returnUrl;
}
