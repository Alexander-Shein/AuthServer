import {Router, ActivatedRoute, Params} from "@angular/router";
import {OnInit} from "@angular/core";
import {Consts} from "../../consts";


export abstract class AuthBaseComponent implements OnInit {

    constructor(
        protected route: ActivatedRoute,
        protected router: Router
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
            window.location.href = this.redirectUrl;
        } else {
            this.router
                .navigate(['/dashboard']);
        }
    }

}