window.onload = function () {
    const defaultLiffId = "1654985975-rkz7v3pE";
    initializeLiff(defaultLiffId);
}

function initializeLiff(myLiffId) {
    liff
        .init({
            liffId: myLiffId
        })
        .then(() => {
            // start to use LIFF's api
            initializeApp();
        })
        .catch((err) => {
            document.getElementById("liffAppContent").classList.add('hidden');
            document.getElementById("liffInitErrorMessage").classList.remove('hidden');
        });
}

function initializeApp() {
    // check if the user is logged in/out, and disable inappropriate button
    if (liff.isLoggedIn()) {
        //有Login就藏起來
        $('#NotLogin').addClass("hideform");
        var accessToken = liff.getAccessToken();
        $('#TokenId').val(accessToken);
        $.ajax({
            beforeSend: function (xhr) {
                xhr.setRequestHeader("requestverificationtoken",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            type: "Post",
            url: "/Home/GetUserBookingData",
            data: { TokenId: accessToken },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            async: false,
            success: function (data) {
                //如果已經預約了
                if (data.result) {
                    $('#Canel').removeClass("hideform");
                    $('#CanelTokenId').val(accessToken);
                    $('#BookingName').val(data.username);
                    $('#BookingService').val(data.service);
                    $('#BookingToStoreDateTime').val(data.tostoredatetime);
                }
                else {
                    $('#Reservation').removeClass("hideform");
                }
            }
        });

    } else {
        $('#NotLogin').removeClass("hideform");
    }
}
