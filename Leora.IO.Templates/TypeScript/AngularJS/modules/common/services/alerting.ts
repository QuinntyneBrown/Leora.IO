module CommonModule {

    class Alerting {

        constructor(private $timeout) {
            
            
        }

        public static serviceId = "alerting";

        public alertTypes = ["warning", "info", "danger", "success"];

        public currentAlerts = [];

        public addWarning = (message) => {
            this.addAlert("warning", message);
        };

        public addInfo = (message) => {
            this.addAlert("info", message);
        };  

        public addDanger = (message) => {
            this.addAlert("danger", message);
        };

        public addSuccess = (message) => {
            this.addAlert("success", message);
        };

        public errorHandler = (description) => {
            return () => {
                this.addDanger(description);
            };
        };

        public removeAlert = (alert) => {
            for (var i = 0; i < this.currentAlerts.length; i++) {
                if (this.currentAlerts[i] == alert) {
                    this.currentAlerts.splice(i, 1);
                    break;
                }
            }
        };

        public addAlert = (type, message) => {
            var alert = { type: type, message: message };
            this.currentAlerts.push(alert);

            this.$timeout(() => {
                this.removeAlert(alert);
            },10000);
        };

    }

    angular.module("common").factory(Alerting.serviceId, ["$timeout",($timeout) => new Alerting($timeout)]);
} 