import { IDispatcher } from "./store";

export class BaseActionCreator {
    constructor(public $location: angular.ILocationService,
        public service,
        public dispatcher: IDispatcher,
        public guid,
        private addOrUpdateAction,
        private allAction,
        private removeAction,
        private setCurrentAction
    ) { }

    getById = options => {
        var newId = this.guid();
        this.service.getById({ id: options.id }).then(results => {
            var action = new this.addOrUpdateAction(newId, results);
            this.dispatcher.dispatch(action);
        });
        return newId;
    }

    all = () => {
        var newId = this.guid();
        this.service.get().then(results => {
            var action = new this.allAction(newId, results);
            this.dispatcher.dispatch(action);
        });
        return newId;
    }

    addOrUpdate = options => {
        var newId = this.guid();
        this.service.add({ data: options.data }).then(results => {
            var action = new this.addOrUpdateAction(newId, results);
            this.dispatcher.dispatch(action);
        });
        return newId;
    }

    remove = options => {
        var newId = this.guid();
        this.service.remove({
            id: options.entity.id
        }).then(results => {
            this.dispatcher.dispatch(new this.removeAction(newId, options.entity));
        });
        return newId;
    }

    edit = options => this.dispatcher.dispatch(new this.setCurrentAction(options.entity));
    

    create = () => this.dispatcher.dispatch(new this.setCurrentAction(null));
    

    

}
