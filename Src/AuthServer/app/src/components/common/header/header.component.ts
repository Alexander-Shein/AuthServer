import {Component, OnInit} from "@angular/core";
import {MenuItem} from "./models/menu-item";
import {Params, ActivatedRoute} from "@angular/router";
import {Consts} from "../../consts";


@Component({
    selector: 'au-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    public menuItems: MenuItem[] = [
        new MenuItem('my account', 'verified_user', 'home'),
        new MenuItem('contact', 'contact_phone', 'contact'),
        new MenuItem('log in', '', 'log-in'),
        new MenuItem('sign up', '', 'sign-up'),
        new MenuItem('dashboard', '', 'dashboard')
    ];

    constructor(private route: ActivatedRoute) {
    }

    public display: boolean = true;

    public ngOnInit(): void {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.display = !params[Consts.RedirectUrl];
            });
    }
}