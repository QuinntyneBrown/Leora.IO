
require("./local-storage-manager-provider");
require("./store");

require("./add-or-update");
require("./fetch");
require("./invoke-async");
require("./component-extension");
require("./safe-digest");

let coreApp = (<any>angular.module("app.core", [
    "ngSanitize",
    "localStorageManager",
    "store",   

    "addOrUpdate",
    "invokeAsync",
    "fetch",
    "safeDigest"
]));
