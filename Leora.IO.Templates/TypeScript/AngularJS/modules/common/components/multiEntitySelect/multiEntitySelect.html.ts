angular.module("common").run(["$templateCache", ($templateCache) => {
	$templateCache.put("/app/common/components/multiEntitySelect/multiEntitySelect.html",
		"<div class=\"form-group\">"+
		"    <div>"+
		"        <label>"+
		"            {{ entityNamePlural }}"+
		"        </label>"+
		"    </div>"+
		""+
		"    <select ng-model=\"selectedId\""+
		"            data-ng-options=\"e.id as e.name for e in vm.entities\"></select>"+
		""+
		"    <div>"+
		"        <ul>"+
		"            <li data-ng-repeat=\"entity in parentEntities\">"+
		"                <a>{{ entity.name }}</a>"+
		"            </li>"+
		"        </ul>"+
		"    </div>"+
		"</div>"
	);
}]);
