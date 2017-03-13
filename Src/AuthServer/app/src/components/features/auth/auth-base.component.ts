import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {Router} from "@angular/router";


export abstract class AuthBaseComponent {

    constructor(
        protected router: Router,
        protected notificationsService: NotificationsService,
        protected spinnerService: SpinnerService
    ) {}

    public redirectUrl: string;

    protected handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

    protected redirectAfterLogin() {
        if (this.redirectUrl) {
            window.location.href = this.redirectUrl;
        } else {
            this.router.navigate(['/dashboard']);
            this.spinnerService.hide();
        }
    }

}