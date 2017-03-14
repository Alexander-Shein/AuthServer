import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {Router, ActivatedRoute, Params} from "@angular/router";
import {OnInit} from "@angular/core";
import {Consts} from "../../consts";


export abstract class AuthBaseComponent implements OnInit {

    constructor(
        protected route: ActivatedRoute,
        protected router: Router,
        protected notificationsService: NotificationsService,
        protected spinnerService: SpinnerService
    ) {}

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
            window.location.href = this.redirectUrl;
        } else {
            this.router.navigate(['/dashboard']);
            this.spinnerService.hide();
        }
    }

}