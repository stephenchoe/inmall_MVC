var updateTargetId = "";
function updateTargetIdSetter(value) {
    updateTargetId = value;
}

var cartUpdateFailedTitle = "更新購物車失敗";
function cartUpdateFailedTitleSetter(value) {
    cartUpdateFailedTitle = value;
}

var cartUpdateFailedMsg = "系統暫時無回應，請稍後再試。";
function cartUpdateFailedMsgSetter(value) {
    cartUpdateFailedMsg = value;
}


var statusType = "error";
function statusTypeSetter(value) {
    statusType = value;
}


function cartUpdateBegin() {  
    Loading(updateTargetId);
}
function cartUpdateSuccess() {
    StopLoading(updateTargetId);
}
function cartUpdateFailed() {
    StopLoading(updateTargetId);

    var settings = {
        type: 'error',
        title: cartUpdateFailedTitle,
        text: cartUpdateFailedMsg,

        showConfirmButton: true
    };

    swal(settings);

}

//function itemUpdateError(callback) {
//    var settings = {
//        type: 'error',
//        title: itemUpdateFailedTitle,
//        text: itemUpdateFailedMsg,

//        showConfirmButton: true
//    };
//    if (callback) {
//        swal(settings, callback);
//    } else {
//        swal(settings);
//    }

//}

//function cartUpdated() {
//    var settings = {
//        type: 'success',
//        title: itemUpdateedTitle,
//        text: itemUpdateedMsg,

//        showConfirmButton: true
//    };

//    swal(settings, function () {
//        returnBackPage();
//    });

//}


//function returnBackPage() {
//    window.location.href = returnUrl;
//}
