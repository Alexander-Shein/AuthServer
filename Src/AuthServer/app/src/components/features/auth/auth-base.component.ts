import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {Router, ActivatedRoute, Params} from "@angular/router";
import {OnInit} from "@angular/core";
import {Consts} from "../../consts";
import {BaseComponent} from "../../common/base.component";
import {AuthenticationService} from "./services/authentication.service";


export abstract class AuthBaseComponent extends BaseComponent  implements OnInit {

    constructor(
        protected route: ActivatedRoute,
        protected router: Router,
        protected authenticationService: AuthenticationService,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(notificationsService, spinnerService);
    }

    public redirectUrl: string;

    public ngOnInit() {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.redirectUrl = params[Consts.RedirectUrl] || '';
            });
    }

    protected handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

    protected redirectAfterLogin() {
        if (this.redirectUrl) {
            if (this.redirectUrl.includes('?')) {
                this.redirectUrl += '$';
            } else {
                this.redirectUrl += '?';
            }

            this.redirectUrl += 'accessToken=' + this.authenticationService.getToken().accessToken;
            window.location.href = this.redirectUrl;
        } else {
            this.router.navigate(['/dashboard']);
            this.spinnerService.hide();
        }
    }

}