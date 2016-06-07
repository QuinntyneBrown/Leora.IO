var addOrUpdate = function (options) {
    var exists = false;
    options.items = options.items || [];
    for (var i = 0; i < options.items.length; i++) {
        if (options.items[i].id === options.item.id) {
            options.items[i] = options.item;
            exists = true;
        }
    }
    if (!exists) { options.items.push(options.item); }
}

angular.module("addOrUpdate",[]).value("addOrUpdate", addOrUpdate);