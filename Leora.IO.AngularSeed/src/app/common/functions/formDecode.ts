module app.common {

    "use strict";

    export var formDecode = (data:any) => {

        var result = {};

        data = data.replace("+", " ");

        var properties = data.split("&");

        properties.forEach((property) => {            
            result[property.split("=")[0]] = property.split("=")[1];
        });

        return result;
    }

    angular.module("app.common").value("formDecode", formDecode);
} 