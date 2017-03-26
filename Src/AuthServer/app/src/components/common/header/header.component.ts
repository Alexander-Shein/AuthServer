import {Component, OnInit} from "@angular/core";
import {MenuItem} from "./models/menu-item";
import {Params, ActivatedRoute} from "@angular/router";
import {Consts} from "../../consts";
import {AuthenticationService} from "../../features/auth/services/authentication.service";


@Component({
    selector: 'au-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private authenticationService: AuthenticationService
    ) { }

    public display: boolean = true;
    public isLoggedId: boolean;

    public ghostMenuItems: MenuItem[] = [
        new MenuItem('AuthGuardian', 'verified_user', 'landing'),
        new MenuItem('contact', 'contact_phone', 'contact'),
        new MenuItem('log in', 'lock_outline', 'log-in'),
        new MenuItem('sign up', 'person_add', 'sign-up')
    ];

    public loggedInMenuItems: MenuItem[] = [
        new MenuItem('AuthGuardian', 'verified_user', 'dashboard'),
        new MenuItem('contact', 'contact_phone', 'contact'),
        new MenuItem('log out', 'lock_open', 'log-out')
    ];

    public ngOnInit(): void {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.display = !params[Consts.RedirectUrl];
            });

        this.isLoggedId = this.authenticationService.isLoggedIn();

        this.authenticationService
            .observeIsLoggedIn()
            .subscribe((value:boolean) => this.isLoggedId = value);
    }
}