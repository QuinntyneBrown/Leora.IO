module CommonModule {

    "use strict";

    class Alerts {

        constructor(private alerting) {

        }

        public static componentId: string = "alerts";

        public restrict: string = "E";

        public replace: boolean = true;

		public scope = {};

        public templateUrl: string = "/app/common/components/alerts/alerts.html";

        public link = (scope, element, attributes) => {

            scope.removeAlert = function (alert) {
                this.alerting.removeAlert(alert);
            };

            scope.currentAlerts = this.alerting.currentAlerts;
        }

    }

    angular.module("common").directive(["alerting",Alerts.componentId,(alerting) => new Alerts(alerting)]);

}
