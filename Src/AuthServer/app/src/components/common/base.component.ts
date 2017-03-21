import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "./spinner/services/spinner.service";


export abstract class BaseComponent {

    constructor(
        protected notificationsService: NotificationsService,
        protected spinnerService: SpinnerService
    ) {}

    protected handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

}