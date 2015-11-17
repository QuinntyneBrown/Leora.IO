module app.ui {

    "use strcit";

    export class ModalServiceProvider implements IModalServiceProvider {
        constructor() {
            
        }

        private $modal: any;

        public $get = ["$modal","$rootScope", ($modal: any, $rootScope, any) => {

            $rootScope.$on("showModal", (event: any, object: any) => {
                this.showModal({
                    templateUrl: object.templateUrl
                }, null);
            });

            this.$modal = $modal;

            return {
                showModal: this.showModal,
                isOpen: false              
            };
        }];

        private modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: '/src/app/ui/modal/default.html'
        };

        private modalOptions = {
            closeButtonText: 'Close',
            actionButtonText: 'OK',
            headerText: 'Proceed?',
            bodyText: 'Perform this action?'
        };

        public showModal = (customModalDefaults, customModalOptions) => {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        };

        public show = (customModalDefaults, customModalOptions) => {

            var tempModalDefaults:any = {};
            var tempModalOptions: any = {};

            angular.extend(tempModalDefaults, this.modalDefaults, customModalDefaults);

            angular.extend(tempModalOptions, this.modalOptions, customModalOptions);

            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = ($rootScope, $scope, $modalInstance) => {


                $scope.modalOptions = tempModalOptions;
                $scope.modalOptions.ok = (result) => {
                    $modalInstance.close('ok');
                };
                $scope.modalOptions.close = (result) => {
                    $modalInstance.close('cancel');
                };

                $rootScope.$on("closeModal", () => {
                    $modalInstance.close('ok');
                });

                $rootScope.$on("$locationChangeStart",() => {
                    $modalInstance.close('cancel');
                });
                };

            tempModalDefaults.controller.$inject = ['$rootScope','$scope', '$modalInstance'];
        }

        return this.$modal.open(tempModalDefaults).result;
    }
    }

    angular.module("app.ui").provider("modalService", [ModalServiceProvider])

    .run(["modalService", (modalService) => {}]);



} 