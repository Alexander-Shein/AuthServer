import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {Router, ActivatedRoute, Params} from "@angular/router";
import {OnInit} from "@angular/core";
import {Consts} from "../../consts";
import {AuthenticationService} from "./services/authentication.service";


export abstract class AuthBaseComponent implements OnInit {

    constructor(
        protected route: ActivatedRoute,
        protected router: Router,
        protected authenticationService: AuthenticationService,
        protected spinnerService: SpinnerService
    ) { }

    public redirectUrl: string;

    public ngOnInit() {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.redirectUrl = params[Consts.RedirectUrl] || '';
            });
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
            this.router
                .navigate(['/dashboard'])
                .then(() => this.spinnerService.hide());
        }
    }

}