var updateTargetId = "apply-form";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var registerFailedTitle = "註冊失敗";
function registerFailedTitleSetter(value) {
    registerFailedTitle = value;
}

var registerFailedMsg = "系統暫時無回應，請稍後再試。";
function registerFailedMsgSetter(value) {
    registerFailedMsg = value;
}

//var registeredTitle = "註冊成功";
//function registeredTitleSetter(value) {
//    registeredTitle = value;
//}

//var registeredMsg = "註冊成功,歡迎您的加入。";
//function registeredMsgSetter(value) {
//    registeredMsg = value;
//}

var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}

//ajax-form callbacks
//function registerBegin() {
//    Loading(updateTargetId, '');
//}
//function registerSuccess() {
//    StopLoading(updateTargetId);
//}
//function registerFailed() {
//    StopLoading(updateTargetId);

//    var settings = {
//        type: 'error',
//        title: registerFailedTitle,
//        text: registerFailedMsg,

//        showConfirmButton: true
//    };

//    swal(settings);

//}
//end ajax-form callbacks

function registerError(callback) {
    var settings = {
        type: 'error',
        title: registerFailedTitle,
        text: registerFailedMsg,

        showConfirmButton: true
    };
    if (callback) {
        swal(settings, callback);
    } else {
        swal(settings);
    }

}

//function registered() {
//    var settings = {
//        type: 'success',
//        title: registeredTitle,
//        text: registeredMsg,

//        showConfirmButton: true
//    };

//    swal(settings, function () {
//        returnBackPage();
//    });

//}

//function requiresVerification(user) {
//    registerError(function () {
//        var url = '/Member/NeedRegisterConfirm?userName=' + encodeURIComponent(user);
//        window.location.href = url;
//    });
//}

//function locked() {
//    registerError(function () {
//        window.location.href = '/Member/Lockout';
//    });
//}

//function returnBackPage() {
//    window.location.href = returnUrl;
//}
