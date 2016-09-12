export const addOrUpdate = (options: { items: Array<{ id: number }>, item: { id: number } }) => {
    let exists = false;
    options.items = options.items || [];
    for (let i = 0; i < options.items.length; i++) {
        if (options.items[i].id === options.item.id) {
            options.items[i] = options.item;
            exists = true;
        }
    }
    if (!exists) { options.items.push(options.item); }
}