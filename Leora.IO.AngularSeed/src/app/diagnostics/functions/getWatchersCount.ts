module app.diagnostics {

    "use strict";

    angular.module("app.diagnostics").value("getWatchersCount", () => {

        var i: any;
        var data: any;
        var scope: any;
        var count = 0;
        var all = document.all;
        var len = all.length;
        var test:any = {};

        var mostWatchers = 0;

        function countScopeWatchers(scope:any, element:any) {
            test[scope.$id] = true;
            var n = scope.$$watchers.length;
            count += n;
            if (n > mostWatchers) {
                //console.log('most watchers', n);
                //console.log(element);
                mostWatchers = n;
            }
        }

        for (i = 0; i < len; i += 1) {
            var el = angular.element(all[i]);
            data = el.data();
            scope = data.$scope || data.$isolateScope;
            if (scope && scope.$$watchers) {
                if (!test[scope.$id]) {
                    countScopeWatchers(scope, el);
                }
            }
        }
        console.log('this page has', count, 'angular watchers');
        return count;        

    });
}