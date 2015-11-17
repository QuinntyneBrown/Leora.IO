module app.common {

    "use strict";

    export var getStringFromUrl = (url:string) => {

        var request = new XMLHttpRequest();

        var response = null;

        request.onreadystatechange = () => {
            if (request.readyState == xhrReadyState.completed) {
                if (request.status == httpStatusCode.success) {
                    response = request.responseText;
                }
            }
        }

        request.open("GET", url, false);

        request.send(null);

        return response;
    }

    angular.module("app.common").value("getStringFromUrl", getStringFromUrl);
} 