module CommonModule {

    class MultiEntitySelect {

        constructor() {

        }

        public static componentId: string = "multiEntitySelect";

        public restrict: string = "E";

        public replace: boolean = true;

        public templateUrl: string = "/app/common/components/multiEntitySelect/multiEntitySelect.html";

        public scope = {
            parentEntities: "=",
            entityService: "="
        }

        public link = (scope, element, attributes) => {

            scope.parentEntities = scope.parentEntities || [];

            scope.entityNamePlural = attributes.entityNamePlural;

            scope.$watch("selectedId",() => {
                scope.processSelectedIdChange();
                scope.selectedId = null;
            });

            scope.processSelectedIdChange = () => {
                if (scope.selectedId) {
                    for (var i = 0; i < scope.parentEntities.length; i++) {
                        if (scope.parentEntities[i].id == scope.selectedId) {
                            scope.parentEntities.splice(i, 1);
                            return;
                        }
                    }

                    for (var i = 0; i < scope.vm.entities.length; i++) {
                        if (scope.vm.entities[i].id == scope.selectedId) {
                            scope.parentEntities.push(scope.vm.entities[i]);
                        }
                    }
                }
            }
            return scope.entityService.getAll().then((results) => {

                scope.vm = {
                    entities: results
                };

            });
        }
    }

    angular.module("common").directive(MultiEntitySelect.componentId,() => new MultiEntitySelect());
}