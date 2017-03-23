import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router, Params} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";
import {Consts} from "../../../consts";


@Component({
    selector: 'au-external-log-in-callback',
    template: ''
})
export class ExternalLogInCallbackPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
    }

    public ngOnInit(): void {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                let accessToken: string = params[Consts.AccessToken];

                this.authenticationService
                    .setToken({
                        accessToken: accessToken
                    });

                this.router
                    .navigate(['/dashboard']);
            });
    }
}