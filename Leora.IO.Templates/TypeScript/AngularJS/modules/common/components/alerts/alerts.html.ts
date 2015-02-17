angular.module("common").run(["$templateCache", ($templateCache) => {
	$templateCache.put("/app/common/components/alerts/alerts.html",
		"<div data-ng-click=\"wtf()\">"+
		"    <div ng-repeat=\"alert in currentAlerts\" class=\"alert alert-{{alert.type}}\">"+
		"        {{ alert.message }}"+
		"        <div class=\"close\">"+
		"            <span class=\"glyphicon glyphicon-remove\">close</span>"+
		"        </div>"+
		"    </div>"+
		"</div>"+
		""
	);
}]);
