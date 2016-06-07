angular.module("safeDigest", []).value("safeDigest", scope => {
    if (!scope.$$phase && (scope.$root && !scope.$root.$$phase))
        scope.$digest();
});