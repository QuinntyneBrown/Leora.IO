module app.common {

    "use strict";

    export var convertObjectToCamelCase = (object: any, newObject: any) => {

        for (var property in object) {

            newObject[property[0].toLowerCase() + property.substr(1)] = object[property];

            for (var childObject in object[property]) {
                convertObjectToCamelCase(childObject, newObject[property[0].toLowerCase() + property.substr(1)]);
            }
        }

        return newObject;
    }

    angular.module("app.common").value("convertObjectToCamelCase", convertObjectToCamelCase);
} 