var updateTargetId = "profilesUpdate-form";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var profilesUpdateFailedTitle = "更新會員資料失敗";
function profilesUpdateFailedTitleSetter(value) {
    profilesUpdateFailedTitle = value;
}

var profilesUpdateFailedMsg = "系統暫時無回應，請稍後再試。";
function profilesUpdateFailedMsgSetter(value) {
    profilesUpdateFailedMsg = value;
}

var profilesUpdatedTitle = "更新會員資料成功";
function profilesUpdatedTitleSetter(value) {
    profilesUpdatedTitle = value;
}

var profilesUpdatedMsg = "";
function profilesUpdatedMsgSetter(value) {
    profilesUpdatedMsg = value;
}

var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}

var returnUrl = "/";

function profilesUpdateBegin() {
    //Loading(updateTargetId);
}
function profilesUpdateSuccess() {
    StopLoading(updateTargetId);
}
function profilesUpdateFailed() {
    StopLoading(updateTargetId);

    var settings = {
        type: 'error',
        title: profilesUpdateFailedTitle,
        text: profilesUpdateFailedMsg,

        showConfirmButton: true
    };

    swal(settings);

}

function profilesUpdateError(callback) {
    var settings = {
        type: 'error',
        title: profilesUpdateFailedTitle,
        text: profilesUpdateFailedMsg,

        showConfirmButton: true
    };
    if (callback) {
        swal(settings, callback);
    } else {
        swal(settings);
    }

}

function profilesUpdated() {
    var settings = {
        type: 'success',
        title: profilesUpdatedTitle,
        text: profilesUpdatedMsg,

        showConfirmButton: true
    };

    swal(settings, function () {
        returnBackPage();
    });

}


function returnBackPage() {
    window.location.href = returnUrl;
}
