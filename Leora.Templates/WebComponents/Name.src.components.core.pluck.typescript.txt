export interface IPluckOptions {
    items: Array<any>;
    value: number;
    key?: string;
}

export var pluck = (options: IPluckOptions) => {
    var item = {};
    for (var i = 0; i < options.items.length; i++) {
        if (options.value == options.items[i][options.key || "id"]) { item = angular.copy(options.items[i]); }
    }
    return item;
};