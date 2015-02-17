module CommonModule {

    "use strict";

    class AlertItem {

        constructor() {

        }

        public static componentId: string = "alertItem";

        public restrict: string = "E";

        public replace: boolean = true;

		public scope = {
		    alert:"="
		};

        public templateUrl: string = "/app/common/components/alertItem/alertItem.html";

        public link = (scope, element, attributes) => {
            scope.removeAlert = function (alert) {
                this.alerting.removeAlert(alert);
            };

        }

    }

    angular.module("common").directive(AlertItem.componentId,() => new AlertItem());

}
