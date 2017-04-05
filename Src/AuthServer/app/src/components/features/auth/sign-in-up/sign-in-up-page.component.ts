import {Component, OnInit, ViewEncapsulation} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {ActivatedRoute, Router} from "@angular/router";


@Component({
    selector: 'au-sign-in-up',
    templateUrl: './sign-in-up-page.component.html',
    styleUrls: ['./sign-in-up-page.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class SignInUpPageComponent implements OnInit {

    constructor(
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.selectedIndex = this.router.url.indexOf('sign-up') != -1 ? 1 : 0;
    }

    public selectedIndex: number;

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
            });
    }

    public app: AppVm;

}