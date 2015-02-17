module CommonModule {

    "use strict";

    class EntityAdminMenu {

        constructor() {

        }

        public static componentId: string = "entityAdminMenu";

        public restrict: string = "E";

        public replace: boolean = true;

		public scope = {};

        public templateUrl: string = "/app/common/components/entityAdminMenu/entityAdminMenu.html";

        public link = (scope, element, attributes) => {
            scope.entityNameLowerCase = attributes.entityName.toLowerCase().replace(" ", "");
            scope.entityName = attributes.entityName;
            scope.entityNamePluralized = attributes.entityNamePluralized;
            
        }

    }

    angular.module("common").directive(EntityAdminMenu.componentId,() => new EntityAdminMenu());

}
