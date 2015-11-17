/// <reference path="../../../../scripts/typings/typescriptapp.d.ts" />

module app.ui {

    export class FormGroup {
        constructor(private metadataService: common.IMetadataService) {

        }

        public static instance = (metadataService: common.IMetadataService) => {
            return new FormGroup(metadataService);
        }

        public restrict: string = "E";

        public compile = (element: ng.IAugmentedJQuery, attributes: ng.IAttributes) => {

            var metadata = this.metadataService.getLocalMetadataForEntity(attributes["entity"]);

            element.replaceWith(getTemplate(metadata));

            function getTemplate(metadata: any) {

                var template: string;

                var label: string;

                for (var i = 0; i < metadata.attributes.length; i++) {
                    if (metadata.attributes[i].propertyName && metadata.attributes[i].propertyName.toLowerCase() === attributes["propertyName"].toLowerCase()) {
                        if (metadata.attributes[i].attributeName == "DisplayAttribute") {
                            label = metadata.attributes[i].name;
                        }
                    }
                }

                template = '<div class="form-group">' +
                '<label>' + label + '</label>' +
                '<input class="form-control" type="input" data-ng-model="' + attributes["ngModel"] + '" ' + getCustomValidationAttributes(metadata).join(" ") + ' ></input>' +
                '<span class="help-block"></span>' +
                '</div>';

                return template;
            }

            function getCustomValidationAttributes(metadata: any) {
                var customValidationAttributes = [];
                customValidationAttributes.push(isRequired(metadata) ? "required='required'" : "");
                return customValidationAttributes;
            }

            function isRequired(metadata) {
                for (var i = 0; i < metadata.attributes.length; i++) {
                    if (metadata.attributes[i].propertyName && metadata.attributes[i].propertyName.toLowerCase() === attributes["propertyName"].toLowerCase()) {
                        if (metadata.attributes[i].attributeName == "RequiredAttribute") {
                            if (metadata.attributes[i].isRequired) {
                                return true;
                            }
                        }
                    }
                }
            }



            return this.link;
        }

        public link = (scope: any, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) => {

        }
    }


    angular.module("app.ui").directive("formGroup", ["metadataService", FormGroup.instance]);
} 