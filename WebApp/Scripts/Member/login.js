var updateTargetId = "login-form";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var loginFailedTitle = "登入失敗";
function loginFailedTitleSetter(value) {
    loginFailedTitle = value;
}

var loginFailedMsg = "系統暫時無回應，請稍後再試。";
function loginFailedMsgSetter(value) {
    loginFailedMsg = value;
}

var loginedTitle = "登入成功";
function loginedTitleSetter(value) {
    loginedTitle = value;
}

var loginedMsg = "登入成功,歡迎您回來";
function loginedMsgSetter(value) {
    loginedMsg = value;
}

var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}

var returnUrl = "/";
function returnUrlSetter(value) {
    returnUrl = value;
}

function loginBegin() {  
    Loading(updateTargetId);
}
function loginSuccess() {
    StopLoading(updateTargetId);
}
function loginFailed() {
    StopLoading(updateTargetId);

    var settings = {
        type: 'error',
        title: loginFailedTitle,
        text: loginFailedMsg,

        showConfirmButton: true
    };

    swal(settings);
    
}

function loginError(callback) {
    var settings = {
        type: 'error',
        title: loginFailedTitle,
        text: loginFailedMsg,

        showConfirmButton: true
    };
    if (callback) {
        swal(settings, callback);
    } else {
        swal(settings);
    }
   
}

function logined() {
    var settings = {
        type: 'success',
        title: loginedTitle,
        text: loginedMsg,

        showConfirmButton: true
    };

    swal(settings, function () {
        returnBackPage();
    });
    
}

function requiresVerification(user) {
    loginError(function () {
        var url= '/Member/NeedRegisterConfirm?userName=' + encodeURIComponent(user);
        window.location.href = url;
    });
}

function locked() {
    loginError(function () {
        var url = '/Member/Lockout?userName=' + encodeURIComponent(user);
        window.location.href = url;
    });
}

function returnBackPage() {    
    window.location.href = returnUrl;
}
